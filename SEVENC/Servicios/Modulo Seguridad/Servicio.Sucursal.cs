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
        public IEnumerable<Sucursal> ObtenerSucursales()
        {
            ISucursal administradorSucursal = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringSucursal.xml");
                administradorSucursal = (ISucursal)ctx["AdministradorSucursal"];
                var sucursales = administradorSucursal.ObtenerSucursales();
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
                if (administradorSucursal != null)
                    administradorSucursal.LiberarRecursos();
            }
        }

        public void CrearSucursal(Sucursal sucursal)
        {
            ISucursal administradorSucursal = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringSucursal.xml");
                administradorSucursal = (ISucursal)ctx["AdministradorSucursal"];
                administradorSucursal.CrearSucursal(sucursal);
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
                if (administradorSucursal != null)
                    administradorSucursal.LiberarRecursos();
            }
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            ISucursal administradorSucursal = null;
            try
            {
                var ctx = new XmlApplicationContext("~/Modulo Seguridad/Springs/SpringSucursal.xml");
                administradorSucursal = (ISucursal)ctx["AdministradorSucursal"];
                administradorSucursal.ActualizarSucursal(sucursal);
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
                if (administradorSucursal != null)
                    administradorSucursal.LiberarRecursos();
            }
        }
    }
}