using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using SEVENC.ServiciosDistribuidos.Servicios.Aspectos;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios
{
    public partial class ServicioWcf
    {
        public IEnumerable<Operacion> ObtenerOperaciones()
        {
            IOperacion administradorOperacion = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringOperacion.xml");
                administradorOperacion = (IOperacion)ctx["AdministradorOperacion"];
                var operaciones = administradorOperacion.ObtenerOperaciones();
                return operaciones.ToList();
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
                if (administradorOperacion != null)
                    administradorOperacion.LiberarRecursos();
            }
        }

        public void ActualizarOperacion(Operacion operacion)
        {
            IOperacion administradorOperacion = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringOperacion.xml");
                administradorOperacion = (IOperacion)ctx["AdministradorOperacion"];
                administradorOperacion.ActualizarOperacion(operacion);
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
                if (administradorOperacion != null)
                    administradorOperacion.LiberarRecursos();
            }
        }
    }
}