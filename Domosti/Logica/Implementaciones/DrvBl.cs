using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class DrvBl : ClaseBase, IDrvBl
    {
        private readonly IDrvDal _dal;
        private readonly INotificacionDal _notificacionDal;

        public DrvBl(IDrvDal dal, INotificacionDal notificacionDal)
        {
            _dal = dal;
            _notificacionDal = notificacionDal;
        }

        public Stream ObtenerDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var dispositivo = Convert.ToInt32(idDispositivo);
            var residente = Convert.ToInt32(idResidente);
            var villa = Convert.ToInt32(idVilla);
            var drv = _dal.ObtenerDrvs().FirstOrDefault(d => d.IdDispositivo == dispositivo && d.IdResidente == residente && d.IdVivienda == villa);
            return drv == null
                ? ObtenerSalida("402", "", "")
                : (drv.Estado == "I" ? ObtenerSalida("401", "", "") : ObtenerSalida("400", "", ""));
        }
        public Stream CrearDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var dispositivo = Convert.ToInt32(idDispositivo);
            var residente = Convert.ToInt32(idResidente);
            var villa = Convert.ToInt32(idVilla);
            var drv = new DispositivoResidenteVivienda
            {
                IdDispositivo = dispositivo,
                Estado = "A",
                IdResidente = residente,
                IdVivienda = villa,
                AuditoriaDrvs = new Collection<AuditoriaDrv>
                {
                    new AuditoriaDrv
                    {
                        Estado = "A",
                        Fecha = DateTime.Now.ToUniversalTime().AddHours(-5)
                    }
                }
            };
            _dal.CrearDrv(drv);
            return ObtenerSalida("500", "", "");
        }
        public Stream ReactivarDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var dispositivo = Convert.ToInt32(idDispositivo);
            var residente = Convert.ToInt32(idResidente);
            var villa = Convert.ToInt32(idVilla);
            var drv = _dal.ObtenerDrvs().FirstOrDefault(d => d.IdDispositivo == dispositivo && d.IdResidente == residente && d.IdVivienda == villa);
            if(drv == null)
                return ObtenerSalida("601", "", "No existe DRV");
            drv.Estado = drv.Estado == "A" ? "I" : "A";
            drv.AuditoriaDrvs.Add(new AuditoriaDrv
            {
                Estado = drv.Estado,
                Fecha = DateTime.Now.ToUniversalTime().AddHours(-5)
            });
            _dal.ActualizarDrv(drv);
            return ObtenerSalida("600", "", "");
        }
        public void EnviarNotificacionesMasivas(string texto, int idCiudadela)
        {
            var drvs = _dal.ObtenerDrvsConDispositivosYViviendas().Where(drv => drv.Vivienda.IdCiudadela == idCiudadela);
            var idResidentesEnviados = new List<int>();
            foreach (var drv in drvs)
            {
                if (!idResidentesEnviados.Contains(drv.IdResidente))
                {
                    _notificacionDal.CrearNotificacion(new Notificacion
                    {
                        Fecha = DateTime.Now.ToUniversalTime().AddHours(-5),
                        IdResidente = drv.IdResidente,
                        Tipo = "M",
                        Mensaje = string.Format("{0}", texto)
                    });
                    idResidentesEnviados.Add(drv.IdResidente);
                }
                EnviarNotificacion(drv.Dispositivo.Token, string.Format("{0}", texto), "Aviso Masivo");
            }
        }
    }
}
