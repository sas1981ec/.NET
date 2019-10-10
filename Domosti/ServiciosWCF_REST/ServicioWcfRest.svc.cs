using System.IO;
using Domosti.CapaNegocios.Interfaces;
using Spring.Context.Support;

namespace Domosti.CapaServicios
{
    public class ServicioWcfRest : IServicioWcfRest
    {
        #region Residentes
        public Stream ObtenerVillas(string email)
        {
            var ctx = new XmlApplicationContext("~/Springs/ResidenteSpring.xml");
            var residenteAdmin = (IResidenteBl)ctx["ResidenteAdmin"];
            return residenteAdmin.ObtenerVillas(email);
        }
        public Stream ObtenerNotificaciones(int idResidente)
        {
            var ctx = new XmlApplicationContext("~/Springs/ResidenteSpring.xml");
            var residenteAdmin = (IResidenteBl)ctx["ResidenteAdmin"];
            return residenteAdmin.ObtenerNotificaciones(idResidente);
        }
        #endregion

        #region Dispositivos
        public Stream ObtenerDispositivos(string idResidente)
        {
            var ctx = new XmlApplicationContext("~/Springs/DispositivoSpring.xml");
            var dispositivoAdmin = (IDispositivosBl)ctx["DispositivoAdmin"];
            return dispositivoAdmin.ObtenerDispositivos(idResidente);
        }
        public Stream InactivarDispositivo(string idDispositivo, string idResidente, string idVilla)
        {
            return ReactivarDrv(idDispositivo, idResidente, idVilla);
        }
        #endregion

        #region Persona
        public Stream Login(string email, string clave, string idDevice, string imei, string iccid)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.Login(email, clave, idDevice, imei, iccid);
        }
        public Stream RestaurarContrasena(string email)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.RestaurarContrasena(email);
        }
        public Stream AsociarDispositivo(string email, string nombreDispositivo, string idDevice, string imei, string iccid, string token)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.AsociarDispositivo(email, nombreDispositivo, idDevice, imei, iccid, token);
        }
        public Stream ActualizarToken(string idDispositivo, string token)
        {
            var ctx = new XmlApplicationContext("~/Springs/DispositivoSpring.xml");
            var dispositivoAdmin = (IDispositivosBl)ctx["DispositivoAdmin"];
            return dispositivoAdmin.ActualizarToken(idDispositivo, token);
        }
        public Stream ObtenerUsuarioApp(string idDispositivo)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.ObtenerUsuarioApp(idDispositivo);
        }
        public Stream CrearUsuarioApp(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string contrasena, string fechaNacimiento)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.CrearUsuarioApp(tipoIdentificacion, identificacion, nombres, apellidos, email,contrasena,fechaNacimiento);
        }
        public Stream ActualizarUsuarioApp(string idResidente, string nombres, string apellidos, string fechaNacimiento)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.ActualizarUsuarioApp(idResidente, nombres, apellidos, fechaNacimiento);
        }
        public Stream CambiarContrasena(string idResidente, string contrasenaVieja, string contrasenaNueva)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.CambiarContrasena(idResidente, contrasenaVieja, contrasenaNueva);
        }
        public Stream ObtenerVisitantes(string idResidente)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.ObtenerVisitantesPorResidente(idResidente);
        }
        public Stream CrearVisitante(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono, string idResidente)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.CrearVisitante(tipoIdentificacion, identificacion, nombres, apellidos, email, telefono, idResidente);
        }
        public Stream ActualizarVisitante(string idVisitante, string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.ActualizarVisitante(idVisitante,tipoIdentificacion, identificacion, nombres, apellidos, email, telefono);
        }
        public Stream EliminarVisitante(string idVisitante)
        {
            var ctx = new XmlApplicationContext("~/Springs/PersonaSpring.xml");
            var personaAdmin = (IPersonaBl)ctx["PersonaAdmin"];
            return personaAdmin.EliminarVisitante(idVisitante);
        }
        #endregion

        #region DRV
        public Stream ObtenerDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var ctx = new XmlApplicationContext("~/Springs/DrvSpring.xml");
            var drvAdmin = (IDrvBl)ctx["DrvAdmin"];
            return drvAdmin.ObtenerDrv(idDispositivo, idResidente, idVilla);
        }
        public Stream CrearDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var ctx = new XmlApplicationContext("~/Springs/DrvSpring.xml");
            var drvAdmin = (IDrvBl)ctx["DrvAdmin"];
            return drvAdmin.CrearDrv(idDispositivo, idResidente, idVilla);
        }
        public Stream ReactivarDrv(string idDispositivo, string idResidente, string idVilla)
        {
            var ctx = new XmlApplicationContext("~/Springs/DrvSpring.xml");
            var drvAdmin = (IDrvBl)ctx["DrvAdmin"];
            return drvAdmin.ReactivarDrv(idDispositivo, idResidente, idVilla);
        }
        #endregion

        #region Permiso
        public Stream ObtenerPermisosPorMesYAnio(string idResidente, string mes, string anio, string estados, string idVisitante)
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.ObtenerPermisosPorMesYAnio(idResidente, mes, anio, estados, idVisitante);
        }
        public Stream ObtenerPermisosPorDia(string idResidente, string dia, string mes, string anio, string estados, string idVisitante)
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.ObtenerPermisosPorDia(idResidente, dia, mes, anio, estados, idVisitante);
        }
        public Stream CrearPermiso(string fechaIni, string fechaFin, string idResidente, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes, string idVivienda)
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.CrearPermiso(fechaIni, fechaFin, idResidente, idMotivo, tienePermisoContinuo, idDispositivo, detalleAdicional, idVisitantes, idVivienda);
        }
        public Stream ActualizarPermiso(string idPermiso, string fechaIni, string fechaFin, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes)
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.ActualizarPermiso(idPermiso, fechaIni, fechaFin, idMotivo, tienePermisoContinuo, idDispositivo, detalleAdicional, idVisitantes);
        }
        public Stream EliminarPermiso(string idPermiso)
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.EliminarPermiso(idPermiso);
        }
        public Stream ObtenerMotivosAcceso()
        {
            var ctx = new XmlApplicationContext("~/Springs/PermisoSpring.xml");
            var permisoAdmin = (IPermisoBl)ctx["PermisoAdmin"];
            return permisoAdmin.ObtenerMotivosAcceso();
        }
        #endregion
    }
}
