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
        public IEnumerable<Rol> ObtenerRoles()
        {
            IRol administradorRol = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringRol.xml");
                administradorRol = (IRol)ctx["AdministradorRol"];
                var roles = administradorRol.ObtenerRoles();
                return roles.ToList();
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
                if (administradorRol != null)
                    administradorRol.LiberarRecursos();
            }
        }

        public IEnumerable<Operacion> ObtenerOperacionesPorIdRol(int idRol)
        {
            IRol administradorRol = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringRol.xml");
                administradorRol = (IRol)ctx["AdministradorRol"];
                var operaciones = administradorRol.ObtenerOperacionesPorIdRol(idRol);
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
                if (administradorRol != null)
                    administradorRol.LiberarRecursos();
            }
        }

        public void CrearRol(Rol rol)
        {
            IRol administradorRol = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringRol.xml");
                administradorRol = (IRol)ctx["AdministradorRol"];
                administradorRol.CrearRol(rol);
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
                if (administradorRol != null)
                    administradorRol.LiberarRecursos();
            }
        }

        public void ActualizarRol(Rol rol)
        {
            IRol administradorRol = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringRol.xml");
                administradorRol = (IRol)ctx["AdministradorRol"];
                administradorRol.ActualizarRol(rol);
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
                if (administradorRol != null)
                    administradorRol.LiberarRecursos();
            }
        }

        public void AsignarOperacionesARol(IEnumerable<int> idsOperaciones, int idRol)
        {
            IRol administradorRol = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringRol.xml");
                administradorRol = (IRol)ctx["AdministradorRol"];
                administradorRol.AsignarOperacionesARol(idsOperaciones, idRol);
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
                if (administradorRol != null)
                    administradorRol.LiberarRecursos();
            }
        }
    }
}