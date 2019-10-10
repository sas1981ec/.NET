using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class ResidenteBl : ClaseBase, IResidenteBl
    {
        private readonly IResidenteDal _dal;
        private readonly IPersonaDal _dalPersona;
        private readonly IViviendaDal _dalVivienda;

        public ResidenteBl(IResidenteDal dal, IPersonaDal dalPersona, IViviendaDal dalVivienda)
        {
            _dal = dal;
            _dalPersona = dalPersona;
            _dalVivienda = dalVivienda;
        }

        public IEnumerable<Residente> ObtenerResidentes(int idCiudadela)
        {
            return
                _dal.ObtenerResidentesConViviendasYVisitantes()
                    .Where(r => !r.EstaEliminado && r.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela)).OrderBy(r => r.Apellidos).ThenBy(r => r.Nombres)
                    .ToList();
        }
        public IEnumerable<Residente> ObtenerResidentesPorNombre(int idCiudadela, string nombreResidente)
        {
            return
                _dal.ObtenerResidentesConViviendasYVisitantes()
                    .Where(r => !r.EstaEliminado && r.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela) && (r.Nombres.ToUpper().Contains(nombreResidente.ToUpper()) || r.Apellidos.ToUpper().Contains(nombreResidente.ToUpper())))
                    .OrderBy(r => r.Apellidos).ThenBy(r => r.Nombres).ToList();
        }
        public Residente ObtenerResidentePorId(int residenteId)
        {
            return _dal.ObtenerResidentesConViviendasYVisitantes().FirstOrDefault(r => r.IdPersona == residenteId);
        }
        public Stream ObtenerNotificaciones(int idResidente)
        {
            var residente = _dal.ObtenerResidenteConNotificaciones(idResidente);
            if(residente == null)
                return ObtenerSalida("2410", "", "No existe el residente.");
            var notificacionesJson = residente.Notificaciones.OrderByDescending(n => n.Fecha).Select(notificacion => new NotificacionJson
            {
                id = notificacion.IdNotificacion.ToString(CultureInfo.InvariantCulture), 
                tipo = notificacion.Tipo,
                mensaje = notificacion.Mensaje
            }).ToList();
            return ObtenerSalidaNotificacion(notificacionesJson, "");
        }
        public Stream ObtenerVillas(string email)
        {
            var residentes = _dal.ObtenerResidentesConViviendasYCiudadelas().Where(r => r.EstaActivo && !r.EstaEliminado && r.Email.ToUpper() == email.ToUpper() && r.Viviendas.Any(v => !v.EstaEliminada && !v.EsSistema));
            if (!residentes.Any())
                return ObtenerSalida("301",null, "Usted no está registrado como residente en ninguna urbanización o no le han asignado a una vivienda.");
            var ciudadelasJson = new List<CiudadelaJson>();
            foreach (var residente in residentes)
            {
                foreach (var vivienda in residente.Viviendas)
                {
                    var ciudadela = ciudadelasJson.FirstOrDefault(c => c.id == vivienda.IdCiudadela.ToString(CultureInfo.InvariantCulture));
                    if (ciudadela != null)
                    {
                        ciudadela.villas.Add(new VillaJson
                        {
                            id = vivienda.IdVivienda.ToString(CultureInfo.InvariantCulture),
                            calle = vivienda.Calle,
                            manzana = vivienda.Manzana.ToString(CultureInfo.InvariantCulture),
                            villa = vivienda.Villa.ToString(CultureInfo.InvariantCulture),
                            mensaje = residente.PuedeUsarApp ? "" : "Usted no puede usar la aplicación.\nConsulte con la administración de su urbanización.",
                            idResidente = residente.IdPersona.ToString(CultureInfo.InvariantCulture)
                        });
                    }
                    else
                    {
                        ciudadelasJson.Add(new CiudadelaJson
                        {
                            id = vivienda.IdCiudadela.ToString(CultureInfo.InvariantCulture),
                            nombre = vivienda.Ciudadela.Nombre,
                            tipo = vivienda.Ciudadela.Tipo,
                            villas = new List<VillaJson>
                            {
                                new VillaJson
                                {
                                    id = vivienda.IdVivienda.ToString(CultureInfo.InvariantCulture),
                                    calle = vivienda.Calle,
                                    manzana = vivienda.Manzana.ToString(CultureInfo.InvariantCulture),
                                    villa = vivienda.Villa.ToString(CultureInfo.InvariantCulture),
                                    mensaje = residente.PuedeUsarApp ? "" : "Usted no puede usar la aplicación.\nConsulte con la administración de su urbanización.",
                                    idResidente = residente.IdPersona.ToString(CultureInfo.InvariantCulture)
                                }
                            }
                        });
                    }
                }
            }
            return ObtenerSalidaVilla(ciudadelasJson, "");
        }
        public void CrearResidente(Residente residenteNuevo, int ciudadelaId)
        {
            using (var transaccion = new TransactionScope())
            {
                var usuariosApp = _dalPersona.ObtenerUsuariosApp().FirstOrDefault(u => u.Email.ToUpper() == residenteNuevo.Email.ToUpper());
                if (usuariosApp != null)
                    residenteNuevo.IdUsuarioApp = usuariosApp.IdPersona;
                _dal.CrearResidente(residenteNuevo);
                var viviendaSistema = _dalVivienda.ObtenerViviendas().FirstOrDefault(v => !v.EstaEliminada && v.EsSistema && v.IdCiudadela == ciudadelaId);
                if (viviendaSistema == null)
                    throw new ApplicationException("No existe Vivienda del sistema para asignar a Residente.");
                _dal.AsignarViviendasAResidente(residenteNuevo.IdPersona, new List<int> { viviendaSistema.IdVivienda});
                transaccion.Complete();
            }
        }
        public void ActualizarResidente(Residente residenteModificado, FotoResidente fotoResidenteModificada)
        {
            using (var transaccion = new TransactionScope())
            {
                _dal.ActualizarResidente(residenteModificado);
                var fotoResidente = ObtenerFotoResidente(residenteModificado.IdPersona);
                fotoResidente.Foto = fotoResidenteModificada.Foto;
                _dal.ActualizarFotoResidente(fotoResidente);
                transaccion.Complete();
            }
        }
        public void AsignarViviendasAResidente(int idResidente, IEnumerable<int> idViviendas)
        {
            using (var transaccion = new TransactionScope())
            {
                var residente = _dal.ObtenerResidentesConViviendasYCiudadelas().FirstOrDefault(r => r.IdPersona == idResidente);
                if (residente == null)
                    throw new ApplicationException("No existe Residente.");
                residente.Viviendas.Clear();
                _dal.ActualizarResidente(residente);
                _dal.AsignarViviendasAResidente(idResidente, idViviendas);
                transaccion.Complete();
            }
        }
        public void EliminarResidente(Residente residenteEliminado)
        {
            residenteEliminado.EstaEliminado = true;
            _dal.ActualizarResidente(residenteEliminado);
        }
        public bool ExisteIdentificacion(string tipo, string identificacion, int idCiudadela)
        {
            return _dal.ObtenerResidentesConViviendasYVisitantes().Any(r => r.TipoIdentificacion == tipo && r.Identificacion == identificacion && r.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela));
        }
        public bool ExisteIdentificacion(string tipo, string identificacion, long idResidente, int idCiudadela)
        {
            return _dal.ObtenerResidentesConViviendasYVisitantes()
                .Any(
                    r =>
                        r.TipoIdentificacion == tipo && r.Identificacion == identificacion &&
                        r.IdPersona != idResidente &&
                        r.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela));
        }
        public FotoResidente ObtenerFotoResidente(long idResidente)
        {
            return _dal.ObtenerFotoResidente(idResidente);
        }
    }
}
