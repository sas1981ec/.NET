using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class NotificacionDal : Liberador, INotificacionDal
    {
        public NotificacionDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public void CrearNotificacion(Notificacion notificacion)
        {
            Contexto.Notificaciones.Add(notificacion);
            Contexto.SaveChanges();
        }
    }
}
