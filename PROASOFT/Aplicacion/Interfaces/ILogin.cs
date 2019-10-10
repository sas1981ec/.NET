using PROASOFT.CapaAplicacion.Aplicacion.Contratos;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface ILogin : IBase
    {
        LoginData Login(string userName, string contrasena);
        void CambiarContrasena(int idUsuario, string contrasenaVieja, string contrasenaNueva);
    }
}
