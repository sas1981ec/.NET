using System;
using System.Linq;
using System.Transactions;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class PermisoManualBl : ClaseBase, IPermisoManualBl
    {
        private readonly IPermisoManualDal _dal;
        private readonly IDrvDal _drvDal;
        private readonly INotificacionDal _notificacionDal;

        public PermisoManualBl(IPermisoManualDal dal, IDrvDal drvDal, INotificacionDal notificacionDal)
        {
            _dal = dal;
            _drvDal = drvDal;
            _notificacionDal = notificacionDal;
        }

        public void RegistrarIngresoVisitanteManual(PermisoManual permisoManual)
        {
            using (var transaccion = new TransactionScope())
            {
                permisoManual.FechaIngreso = DateTime.Now.ToUniversalTime().AddHours(-5);
                _dal.CrearPermisoManual(permisoManual);
                _notificacionDal.CrearNotificacion(new Notificacion
                {
                    Fecha = DateTime.Now.ToUniversalTime().AddHours(-5),
                    IdResidente = permisoManual.IdResidente,
                    Tipo = "I",
                    Mensaje = string.Format("Acaba de ingresar {0} a las {1}", permisoManual.NombreVisitante, permisoManual.FechaIngreso.ToString("T"))
                });
                transaccion.Complete();
            }
            var dispositivos = _drvDal.ObtenerDrvsConDispositivos().Where(drv => drv.IdResidente == permisoManual.IdResidente && drv.Estado == "A").Select(drv => drv.Dispositivo).Distinct();
            foreach (var dispositivo in dispositivos)
                EnviarNotificacion(dispositivo.Token,string.Format("Acaba de ingresar {0} a las {1}",permisoManual.NombreVisitante,permisoManual.FechaIngreso.ToString("T")),"Visitante Ingresó");
        }
    }
}
