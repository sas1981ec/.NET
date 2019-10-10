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
        public IEnumerable<Empresa> ObtenerEmpresas()
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                var empresas = administradorEmpresa.ObtenerEmpresas();
                return empresas.ToList();
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
                if(administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public void CrearEmpresa(Empresa empresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                administradorEmpresa.CrearEmpresa(empresa);
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public void ActualizarEmpresa(Empresa empresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                administradorEmpresa.ActualizarEmpresa(empresa);
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public IEnumerable<Sucursal> ObtenerSucursalesPorIdEmpresa(byte idEmpresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                var sucursales = administradorEmpresa.ObtenerSucursalesPorIdEmpresa(idEmpresa);
                return sucursales.ToList();
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public IEnumerable<Usuario> ObtenerUsuariosPorIdEmpresa(byte idEmpresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                var usuarios = administradorEmpresa.ObtenerUsuariosPorIdEmpresa(idEmpresa);
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public void AsignarSucursalesAEmpresa(IEnumerable<short> idsSucursales, byte idEmpresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                administradorEmpresa.AsignarSucursalesAEmpresa(idsSucursales, idEmpresa);
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }

        public void AsignarUsuariosAEmpresa(IEnumerable<int> idsUsuarios, byte idEmpresa)
        {
            IEmpresa administradorEmpresa = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringEmpresa.xml");
                administradorEmpresa = (IEmpresa)ctx["AdministradorEmpresa"];
                administradorEmpresa.AsignarUsuariosAEmpresa(idsUsuarios, idEmpresa);
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
                if (administradorEmpresa != null)
                    administradorEmpresa.LiberarRecursos();
            }
        }
    }
}