using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Contratos;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IUsuarioBl
    {
        LoginData Login(string usuario, string contrasena);
        void CambiarContrasena(Usuario usuario);
        Usuario ObtenerUsuario(string userName);
    }
}
