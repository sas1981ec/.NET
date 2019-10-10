using System.IO;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IPersonaBl
    {
        Stream Login(string email, string clave, string idDevice, string imei, string iccid);
        Stream RestaurarContrasena(string email);
        Stream ObtenerUsuarioApp(string idDispositivo);
        Stream CrearUsuarioApp(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string contrasena, string fechaNacimiento);
        Stream ActualizarUsuarioApp(string idResidente, string nombres, string apellidos, string fechaNacimiento);
        Stream CambiarContrasena(string idResidente, string contrasenaVieja, string contrasenaNueva);
        Stream AsociarDispositivo(string email, string nombreDispositivo, string idDevice, string imei, string iccid, string token);
        Stream ObtenerVisitantesPorResidente(string idResidente);
        Stream CrearVisitante(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono, string idResidente);
        Stream ActualizarVisitante(string idVisitante,string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono);
        Stream EliminarVisitante(string idVisitante);
    }
}
