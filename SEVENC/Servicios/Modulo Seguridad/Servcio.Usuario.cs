using SEVENC.Aplicacion.Aplicacion.Contratos.Modulo_Seguridad;
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
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                var usuarios = administradorUsuario.ObtenerUsuarios();
                return usuarios.ToList();
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public IEnumerable<Usuario> ObtenerUsuariosPorCriteriosBusqueda(Dictionary<Busqueda, string> criteriosBusqueda, int indicePagina)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                var usuarios = administradorUsuario.ObtenerUsuariosPorCriteriosBusqueda(criteriosBusqueda, indicePagina);
                return usuarios.ToList();
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }


        public IEnumerable<Rol> ObtenerRolesPorIdUsuario(int idUsuario)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                var roles = administradorUsuario.ObtenerRolesPorIdUsuario(idUsuario);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void CrearUsuario(Usuario usuario)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.CrearUsuario(usuario);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.ActualizarUsuario(usuario);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void CambiarContrasena(int idUsuario, string contrasena)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.CambiarContrasena(idUsuario, contrasena);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void GenerarContrasena(int idUsuario)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.GenerarContrasena(idUsuario);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public bool ExisteEmail(string email)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                return administradorUsuario.ExisteEmail(email);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public bool ExisteUserName(string userName)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                return administradorUsuario.ExisteUserName(userName);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void AsignarRolesAUsuario(IEnumerable<int> idsRoles, int idUsuario)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.AsignarRolesAUsuario(idsRoles, idUsuario);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public LoginData Login(string userName, string contrasena, string ip)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                return administradorUsuario.Login(userName, contrasena, ip);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }

        public void CerrarSesion(long idSesion)
        {
            IUsuario administradorUsuario = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringUsuario.xml");
                administradorUsuario = (IUsuario)ctx["AdministradorUsuario"];
                administradorUsuario.CerrarSesion(idSesion);
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
                if (administradorUsuario != null)
                    administradorUsuario.LiberarRecursos();
            }
        }
    }
}