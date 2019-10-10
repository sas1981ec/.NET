using System.Linq;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Contratos;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class UsuarioBl : ClaseBase, IUsuarioBl
    {
        private readonly IUsuarioDal _dal;

        public UsuarioBl(IUsuarioDal dal)
        {
            _dal = dal;
        }

        public LoginData Login(string usuario, string contrasena)
        {
            contrasena = HashPassword(contrasena);
            var resultado = new LoginData();
            var usuarios = _dal.ObtenerUsuarios();
            if (usuarios.Count(u => u.UserName == usuario) == 0)
                resultado.Mensaje = "No existe el usuario.";
            else if (usuarios.Count(u => u.UserName == usuario && u.Contrasena == contrasena) == 0)
                resultado.Mensaje = "Contraseña incorrecta.";
            else
            {
                resultado.EstaAutenticado = true;
                resultado.Usuario = usuarios.FirstOrDefault(u => u.UserName == usuario && u.Contrasena == contrasena);
            }
            return resultado;
        }
        public void CambiarContrasena(Usuario usuario)
        {
            var usuarioAModificar = ObtenerUsuario(usuario.UserName);
            usuarioAModificar.Contrasena = HashPassword(usuario.Contrasena);
            _dal.ActualizarUsuario(usuarioAModificar);
        }
        public Usuario ObtenerUsuario(string userName)
        {
            return _dal.ObtenerUsuarios().FirstOrDefault(u => u.UserName == userName);
        }
    }
}
