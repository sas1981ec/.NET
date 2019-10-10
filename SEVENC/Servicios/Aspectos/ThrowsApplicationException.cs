using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using Spring.Aop;
using Spring.Context.Support;
using System;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios.Aspectos
{
    public class ThrowsApplicationException : IThrowsAdvice
    {
        public void AfterThrowing(ApplicationException ex)
        {
            var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringError.xml");
            var administradorError = (IError)ctx["AdministradorError"];
            var error = new Dominio.Entidades.Error
            {
                Detalle = $"Mensaje : {ex.Message}///Excepción Interna : {ex.InnerException}///Pila de Seguimiento : {ex.StackTrace}///Fuente : {ex.Source}///Link : {ex.HelpLink}",
                Fecha = DateTime.Now,
                Mensaje = ex.Message,
                Tipo = "A"
            };
            administradorError.CrearError(error);
            administradorError.LiberarRecursos();
            throw new FaultException(ex.Message);
        }
    }
}