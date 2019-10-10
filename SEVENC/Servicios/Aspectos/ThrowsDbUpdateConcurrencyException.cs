using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using Spring.Context.Support;
using System;
using System.ServiceModel;
using System.Data.Entity.Infrastructure;
using Spring.Aop;

namespace SEVENC.ServiciosDistribuidos.Servicios.Aspectos
{
    public class ThrowsDbUpdateConcurrencyException : IThrowsAdvice
    {
        public void AfterThrowing(DbUpdateConcurrencyException ex)
        {
            var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringError.xml");
            var administradorError = (IError)ctx["AdministradorError"];
            var error = new Dominio.Entidades.Error
            {
                Detalle = $"Mensaje : {ex.Message}///Excepción Interna : {ex.InnerException}///Pila de Seguimiento : {ex.StackTrace}///Fuente : {ex.Source}///Link : {ex.HelpLink}",
                Fecha = DateTime.Now,
                Mensaje = ex.Message,
                Tipo = "C"
            };
            administradorError.CrearError(error);
            administradorError.LiberarRecursos();
            throw new FaultException("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.");
        }
    }
}