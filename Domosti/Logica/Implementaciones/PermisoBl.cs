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
    public class PermisoBl : ClaseBase, IPermisoBl
    {
        private readonly IPermisoDal _dal;
        private readonly IMotivoAccesoDal _dalMotivoAcceso;
        private readonly IDrvDal _drvDal;
        private readonly IPersonaDal _personaDal;
        private readonly INotificacionDal _notificacionDal;

        public PermisoBl(IPermisoDal dal, IMotivoAccesoDal dalMotivoAcceso, IDrvDal drvDal, IPersonaDal personaDal, INotificacionDal notificacionDal)
        {
            _dal = dal;
            _dalMotivoAcceso = dalMotivoAcceso;
            _drvDal = drvDal;
            _personaDal = personaDal;
            _notificacionDal = notificacionDal;
        }

        public IEnumerable<Permiso> ObtenerBitacoraPermisos(int idCiudadela, Dictionary<EnumeracionBusquedaPermisos, string> filtros)
        {
            var fecha = DateTime.Now.ToUniversalTime().AddHours(-5);
            var permisos = ObtenerBitacoraPermisos(idCiudadela).Where(p => p.FechaInicial <= fecha && p.FechaFin >= fecha).ToList();
            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case EnumeracionBusquedaPermisos.PorIdentificacionResidente:
                        permisos = permisos.Where(p => p.Residente.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaPermisos.PorIdentificacionVisitante:
                        permisos = permisos.Where(p => p.Visitante.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaPermisos.PorNombreResidente:
                        permisos = permisos.Where(p => p.Residente.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || p.Residente.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaPermisos.PorNombreVisitante:
                        permisos = permisos.Where(p => p.Visitante.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || p.Visitante.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                }
            }
            return permisos.OrderBy(p => p.FechaInicial).ToList();
        }
        private IEnumerable<Permiso> ObtenerBitacoraPermisos(int idCiudadela)
        {
            return
                _dal.ObtenerPermisosConResidenteYVisitante()
                    .Where(
                        p =>
                            p.Residente.Viviendas.Any(v => v.IdCiudadela == idCiudadela && !v.EstaEliminada) &&
                            !p.Visitante.EstaEliminado && !p.Residente.EstaEliminado && p.Residente.EstaActivo && p.Estado != "V"
                            && p.Estado != "E" && (p.PermisoContinuo || p.Estado == "C"))
                    .ToList();
        }
        public void RegistrarIngresoVisitante(Permiso permiso, string observaciones)
        {
            var visitante = _personaDal.ObtenerVisitantes().FirstOrDefault(v => v.IdPersona == permiso.IdVisitante);
            if (visitante == null)
                return;
            using (var transaccion = new TransactionScope())
            {
                var permisoAModificar = ObtenerPermisoPorId(permiso.IdPermiso);
                permisoAModificar.Estado = "A";
                permisoAModificar.Accesos.Add(new Acceso { FechaAcceso = DateTime.Now.ToUniversalTime().AddHours(-5), IdPermiso = permiso.IdPermiso, Observaciones = observaciones });
                _dal.ActualizarPermiso(permisoAModificar);
                _notificacionDal.CrearNotificacion(new Notificacion
                {
                    Fecha = DateTime.Now.ToUniversalTime().AddHours(-5),
                    IdResidente = permisoAModificar.IdResidente,
                    Tipo = "I",
                    Mensaje = string.Format("Acaba de ingresar {0} a las {1}", visitante.NombreCompleto, DateTime.Now.ToUniversalTime().AddHours(-5).ToString("T"))
                });
                transaccion.Complete();
            }
            var dispositivos = _drvDal.ObtenerDrvsConDispositivos().Where(drv => drv.IdResidente == permiso.IdResidente && drv.Estado == "A").Select(drv => drv.Dispositivo).Distinct();
            foreach (var dispositivo in dispositivos)
                EnviarNotificacion(dispositivo.Token, string.Format("Acaba de ingresar {0} a las {1}", visitante.NombreCompleto, DateTime.Now.ToUniversalTime().AddHours(-5).ToString("T")), "Visitante Ingresó");
        }
        public Permiso ObtenerPermisoPorId(int permisoId)
        {
            return _dal.ObtenerPermisosConResidenteYVisitante().FirstOrDefault(p => p.IdPermiso == permisoId);
        }
        public Stream ObtenerMotivosAcceso()
        {
            var motivos = _dalMotivoAcceso.ObtenerMotivosAcceso();
            var resultado = motivos.Select(motivo => new MotivoAccesoJson
            {
                id = motivo.IdMotivoAcceso.ToString(CultureInfo.InvariantCulture), 
                nombre = motivo.Nombre
            }).ToList();
            return resultado.Any() ? ObtenerSalidaMotivoAcceso(resultado, "") : ObtenerSalida("1201", "", "No existen motivos de accesos en la base de datos.");
        }
        public Stream ObtenerPermisosPorMesYAnio(string idResidente, string mes, string anio, string estados, string idVisitante)
        {
            var residente = Convert.ToInt32(idResidente);
            var numeroMes = Convert.ToInt32(mes);
            var numeroAnio = Convert.ToInt32(anio);
            var diasMes = DateTime.DaysInMonth(numeroAnio, numeroMes);
            var fechaInicio = new DateTime(numeroAnio, numeroMes, 1);
            var fechaFin = new DateTime(numeroAnio, numeroMes, diasMes,23,59,59);
            estados = estados == "T" ? "C,E,A,V" : estados;
            var visitante = Convert.ToInt32(idVisitante);
            var permisos = visitante == 0 ? _dal.ObtenerPermisosConVisitanteYAccesos().Where(c => c.Permisos.Any(p => p.IdResidente == residente && p.FechaInicial >= fechaInicio && p.FechaInicial <= fechaFin && estados.Contains(p.Estado))) : _dal.ObtenerPermisosConVisitanteYAccesos().Where(c => c.Permisos.Any(p => p.IdResidente == residente && p.FechaInicial >= fechaInicio && p.FechaInicial <= fechaFin && estados.Contains(p.Estado) && p.IdVisitante == visitante));
            var resultado = new List<CalendarioJson>();
            for (var i = 1; i < diasMes + 1; i++)
            {
                var fecha = new DateTime(numeroAnio, numeroMes, i);
                var fechaFinDia = new DateTime(numeroAnio, numeroMes, i).AddHours(23).AddMinutes(59).AddSeconds(59);
                resultado.Add(new CalendarioJson
                {
                    d = i.ToString(CultureInfo.InvariantCulture),
                    n = permisos.Aggregate(0, (current, cabeceraPermiso) => current + (cabeceraPermiso.Permisos.Any(p => (p.FechaInicial >= fecha && p.FechaInicial <= fechaFinDia) || (p.FechaFin >= fecha && p.FechaFin <= fechaFinDia) || (p.FechaInicial <= fecha && p.FechaFin >= fechaFinDia)) ? 1 : 0)).ToString(CultureInfo.InvariantCulture)
                });
            }
            return ObtenerSalidaCalendario(resultado,"");
        }
        public Stream ObtenerPermisosPorDia(string idResidente, string dia, string mes, string anio, string estados, string idVisitante)
        {
            var residente = Convert.ToInt32(idResidente);
            var diasMes = Convert.ToInt32(dia);
            var numeroMes = Convert.ToInt32(mes);
            var numeroAnio = Convert.ToInt32(anio);
            var fechaInicio = new DateTime(numeroAnio, numeroMes, diasMes);
            var fechaFin = fechaInicio.AddHours(23).AddMinutes(59).AddSeconds(59);
            estados = estados == "T" ? "C,E,A,V" : estados;
            var visitante = Convert.ToInt32(idVisitante);
            var permisos = visitante == 0 ? _dal.ObtenerPermisosConVisitanteYAccesos().Where(c => c.Permisos.Any(p => p.IdResidente == residente && ((p.FechaInicial >= fechaInicio && p.FechaInicial <= fechaFin) || (p.FechaInicial <= fechaInicio && p.FechaFin >= fechaInicio)) && estados.Contains(p.Estado)))
                                          : _dal.ObtenerPermisosConVisitanteYAccesos().Where(c => c.Permisos.Any(p => p.IdResidente == residente && ((p.FechaInicial >= fechaInicio && p.FechaInicial <= fechaFin) || (p.FechaInicial <= fechaInicio && p.FechaFin >= fechaInicio)) && estados.Contains(p.Estado) && p.IdVisitante == visitante));
            var resultado = (from cabeceraPermiso in permisos
                let permiso = cabeceraPermiso.Permisos.FirstOrDefault()
                let visitantes = ""
                let visitantesId = cabeceraPermiso.Permisos.Aggregate(visitantes,(a,p) => a + p.IdVisitante + ",")
                where permiso != null
                select new PermisoJson
                {
                    idPermiso = cabeceraPermiso.IdCabeceraPermiso.ToString(CultureInfo.InvariantCulture),
                    estado = cabeceraPermiso.Permisos.Count() > 1 ? "" : permiso.Estado,
                    fechaFin = string.Format("{0}-{1}-{2}-{3}-{4}", permiso.FechaFin.Day.ToString("00"), permiso.FechaFin.Month.ToString("00"), permiso.FechaFin.Year, permiso.FechaFin.Hour.ToString("00"), permiso.FechaFin.Minute.ToString("00")),
                    fechaIni = string.Format("{0}-{1}-{2}-{3}-{4}", permiso.FechaInicial.Day.ToString("00"), permiso.FechaInicial.Month.ToString("00"), permiso.FechaInicial.Year, permiso.FechaInicial.Hour.ToString("00"), permiso.FechaInicial.Minute.ToString("00")), 
                    idMotivo = permiso.MotivoAcceso.IdMotivoAcceso.ToString(CultureInfo.InvariantCulture), 
                    tienePermisoContinuo = permiso.PermisoContinuo ? "1" : "0",
                    detalleAdicional = permiso.DetalleAdicional,
                    idVisitantes = visitantesId.Remove(visitantesId.Length - 1)
                }).OrderBy(r => r.fechaIni).ToList();
            return ObtenerSalidaPermiso(resultado, "");
        }
        public Stream CrearPermiso(string fechaInicial, string fechaFin, string idResidente, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes, string idVivienda)
        {
            var permisos = new List<Permiso>();
            var visitantes = idVisitantes.Split(',');
            var fechaDesde = fechaInicial.Split('-');
            var fechaHasta = fechaFin.Split('-');
            for (var i = 0; i < visitantes.Count(); i++)
            {
                permisos.Add(new Permiso
                {
                    IdDispositivo = Convert.ToInt32(idDispositivo),
                    Estado = "C",
                    FechaCreacion = DateTime.Now.ToUniversalTime().AddHours(-5),
                    FechaFin = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]), Convert.ToInt32(fechaHasta[3]), Convert.ToInt32(fechaHasta[4]),0),
                    FechaInicial = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]), Convert.ToInt32(fechaDesde[3]), Convert.ToInt32(fechaDesde[4]), 0),
                    FechaUltimaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5),
                    IdResidente = Convert.ToInt32(idResidente),
                    IdVisitante = Convert.ToInt32(visitantes[i]),
                    IdMotivoAcceso = Convert.ToInt16(idMotivo),
                    IdVivienda = Convert.ToInt32(idVivienda),
                    PermisoContinuo = tienePermisoContinuo == "1",
                    DetalleAdicional = detalleAdicional
                });
            }
            var cabeceraPermiso = new CabeceraPermiso
            {
                Permisos = permisos
            };
            _dal.CrearPermiso(cabeceraPermiso);
            return ObtenerSalida("1400", "", "");
        }
        public Stream ActualizarPermiso(string idPermiso, string fechaInicial, string fechaFin, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional,string idVisitantes)
        {
            using (var transaccion = new TransactionScope())
            {
                var idCabeceraPermiso = Convert.ToInt32(idPermiso);
                var fechaDesde = fechaInicial.Split('-');
                var fechaHasta = fechaFin.Split('-');
                var cabeceraPermiso = _dal.ObtenerPermisosConVisitanteYAccesos().FirstOrDefault(cp => cp.IdCabeceraPermiso == idCabeceraPermiso);
                if (cabeceraPermiso == null)
                    return ObtenerSalida("1510", "", "No existe el Permiso a Modificar.");
                if (cabeceraPermiso.Permisos.Any(p => p.Estado != "C"))
                    return ObtenerSalida("1520", "", "No se puede modificar el permiso, ya que un visitante ingresó o el permiso ha vencido, o está eliminado.");
                var listaIdVisitantesNuevos = new List<int>();
                var listaIdVisitantesAEliminar = new List<int>();
                var visitantes = idVisitantes.Split(',');
                for (var i = 0; i < visitantes.Count(); i++)
                {
                    var id = Convert.ToInt32(visitantes[i]);
                    if(cabeceraPermiso.Permisos.All(p => p.IdVisitante != id))
                        listaIdVisitantesNuevos.Add(id);
                }
                foreach (var permiso in cabeceraPermiso.Permisos)
                {
                    if(!idVisitantes.Contains(permiso.IdVisitante.ToString(CultureInfo.InvariantCulture)))
                        listaIdVisitantesAEliminar.Add(permiso.IdVisitante);
                    permiso.FechaUltimaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5);
                    if (permiso.FechaInicial != new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]), Convert.ToInt32(fechaDesde[3]), Convert.ToInt32(fechaDesde[4]), 0))
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "FechaIni",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.FechaInicial.ToString(CultureInfo.InvariantCulture),
                            ValorNuevo = fechaInicial
                        });
                        permiso.FechaInicial = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]), Convert.ToInt32(fechaDesde[3]), Convert.ToInt32(fechaDesde[4]), 0);
                    }
                    if (permiso.FechaFin != new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]), Convert.ToInt32(fechaHasta[3]), Convert.ToInt32(fechaHasta[4]), 0))
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "FechaFin",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.FechaFin.ToString(CultureInfo.InvariantCulture),
                            ValorNuevo = fechaFin
                        });
                        permiso.FechaFin = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]), Convert.ToInt32(fechaHasta[3]), Convert.ToInt32(fechaHasta[4]), 0);
                    }
                    if (permiso.IdMotivoAcceso != Convert.ToInt16(idMotivo))
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "IdMotivoAcceso",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.IdMotivoAcceso.ToString(CultureInfo.InvariantCulture),
                            ValorNuevo = idMotivo
                        });
                        permiso.IdMotivoAcceso = Convert.ToInt16(idMotivo);
                    }
                    if (permiso.PermisoContinuo != (tienePermisoContinuo == "1"))
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "PermisoContinuo",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.PermisoContinuo.ToString(CultureInfo.InvariantCulture),
                            ValorNuevo = (tienePermisoContinuo == "1").ToString()
                        });
                        permiso.PermisoContinuo = tienePermisoContinuo == "1";
                    }
                    if (permiso.IdDispositivo != Convert.ToInt32(idDispositivo))
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "IdDispositivo",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.IdDispositivo.ToString(CultureInfo.InvariantCulture),
                            ValorNuevo = idDispositivo
                        });
                        permiso.IdDispositivo = Convert.ToInt32(idDispositivo);
                    }
                    if (permiso.DetalleAdicional != detalleAdicional)
                    {
                        permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                        {
                            Campo = "DetalleAdicional",
                            FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                            ValorAntiguo = permiso.DetalleAdicional,
                            ValorNuevo = detalleAdicional
                        });
                        permiso.DetalleAdicional = detalleAdicional;
                    }
                    _dal.ActualizarPermiso(cabeceraPermiso);
                }
                AgregarPermisos(cabeceraPermiso, listaIdVisitantesNuevos, idDispositivo);
                EliminarPermisos(idPermiso, listaIdVisitantesAEliminar);
                transaccion.Complete();
                return ObtenerSalida("1500", "", "");
            }
        }
        private void AgregarPermisos(CabeceraPermiso cabeceraPermiso, IEnumerable<int> listaIdVisitantesNuevos, string idDispositivo)
        {
            var item = cabeceraPermiso.Permisos.FirstOrDefault();
            if(item == null)
                return;
            foreach (var id in listaIdVisitantesNuevos)
            {
                cabeceraPermiso.Permisos.Add(new Permiso
                {
                    IdDispositivo = Convert.ToInt32(idDispositivo),
                    Estado = "C",
                    FechaCreacion = DateTime.Now.ToUniversalTime().AddHours(-5),
                    FechaFin = item.FechaFin,
                    FechaInicial = item.FechaInicial,
                    FechaUltimaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5),
                    IdResidente = item.IdResidente,
                    IdVisitante = id,
                    IdMotivoAcceso = item.IdMotivoAcceso,
                    PermisoContinuo = item.PermisoContinuo,
                    DetalleAdicional = item.DetalleAdicional
                });
            }
            _dal.ActualizarPermiso(cabeceraPermiso);
        }

        private void EliminarPermisos(string idPermiso, IEnumerable<int> idVisitantes)
        {
            var idCabeceraPermiso = Convert.ToInt32(idPermiso);
            foreach (var permiso in idVisitantes.Select(idVisitante => _dal.ObtenerPermisosConResidenteYVisitante().FirstOrDefault(p => p.IdVisitante == idVisitante && p.IdCabeceraPermiso == idCabeceraPermiso)).Where(permiso => permiso != null))
                _dal.EliminarPermiso(permiso);
        }

        public Stream EliminarPermiso(string idPermiso)
        {
            var idCabeceraPermiso = Convert.ToInt32(idPermiso);
            var cabeceraPermiso = _dal.ObtenerPermisosConVisitanteYAccesos().FirstOrDefault(cp => cp.IdCabeceraPermiso == idCabeceraPermiso);
            if(cabeceraPermiso == null)
                return ObtenerSalida("1610", "", "No existe el Permiso a Eliminar.");
            if(cabeceraPermiso.Permisos.Any(p => p.Estado != "C"))
                return ObtenerSalida("1620", "", "No se puede eliminar el permiso, ya que un visitante ingresó o el permiso ha vencido, o está eliminado.");
            foreach (var permiso in cabeceraPermiso.Permisos)
            {
                permiso.Estado = "E";
                permiso.FechaUltimaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5);
                permiso.AuditoriaPermisos.Add(new AuditoriaPermiso
                {
                    Campo = "Estado",
                    FechaCambio = DateTime.Now.ToUniversalTime().AddHours(-5),
                    ValorAntiguo = "C",
                    ValorNuevo = "E"
                });
            }
            _dal.ActualizarPermiso(cabeceraPermiso);
            return ObtenerSalida("1600", "", "");
        }
    }
}
