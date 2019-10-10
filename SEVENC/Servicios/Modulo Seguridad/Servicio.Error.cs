using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using SEVENC.ServiciosDistribuidos.Servicios.Aspectos;
using Spring.Context.Support;
using System;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios
{
    public partial class ServicioWcf
    {
        public int CrearError(Error error)
        {
            IError administradorError = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringServicioError.xml");
                administradorError = (IError)ctx["AdministradorError"];
                error.Fecha = DateTime.Now;
                error.Tipo = "E";
                administradorError.CrearError(error);
                return error.IdError;
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ThrowsExceptionParaLogError.LoguearError($"Mensaje : {ex.Message}///Excepción Interna : {ex.InnerException}///Pila de Seguimiento : {ex.StackTrace}///Fuente : {ex.Source}///Link : {ex.HelpLink}");
                throw;
            }
            finally
            {
                if (administradorError != null)
                    administradorError.LiberarRecursos();
            }
        }
    }
}