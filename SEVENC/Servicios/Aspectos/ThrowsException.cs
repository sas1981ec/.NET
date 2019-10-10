using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using Spring.Aop;
using Spring.Context.Support;
using System;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios.Aspectos
{
    public class ThrowsException : IThrowsAdvice
    {
        public void AfterThrowing(Exception ex)
        {
            if (ex is FaultException)
                return;
            var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringError.xml");
            var administradorError = (IError)ctx["AdministradorError"];
            var error = new Dominio.Entidades.Error
            {
                Detalle = $"Mensaje : {ex.Message}///Excepción Interna : {ex.InnerException}///Pila de Seguimiento : {ex.StackTrace}///Fuente : {ex.Source}///Link : {ex.HelpLink}",
                Fecha = DateTime.Now,
                Mensaje = ex.Message,
                Tipo = "T"
            };
            administradorError.CrearError(error);
            administradorError.LiberarRecursos();
            throw new FaultException($"Ha ocurrido un inconveniente.\nReportelo con el código {error.IdError}");
        }
    }
}