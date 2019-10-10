using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioLogin : ILogin
    {
        private readonly IRepositorioUsuario _repositorio;

        public ServicioLogin(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public LoginData Login(string userName, string contrasena)
        {
            var usuario = _repositorio.ObtenerUsuariosConRoles(new FiltroUsuarioParaLogin(userName, HashPassword(contrasena))).FirstOrDefault();
            if (usuario == null)
                return new LoginData { Mensaje = "Usuario y/o contraseña inválidas." };
            else
                return new LoginData
                {
                    FueOk = true,
                    IdUsuario = usuario.ID_USUARIO,
                    Mensaje = "Usuario Autenticado.",
                    NombreUsuario = $"{usuario.NOMBRES} {usuario.APELLIDOS}",
                    UserName = userName,
                    Roles = usuario.ROLES.Select(r => r.ID_ROL).ToList()
                };
        }

        private string HashPassword(string contrasena)
        {
            var data = Encoding.UTF8.GetBytes(contrasena);
            var hash = SHA512.Create().ComputeHash(data);
            var passwordHashed = Convert.ToBase64String(hash);
            var result = passwordHashed;
            return result;
        }

        public void CambiarContrasena(int idUsuario, string contrasenaVieja, string contrasenaNueva)
        {
            var usuario = _repositorio.ObtenerObjetos(new FiltroUsuarioPorId(idUsuario)).FirstOrDefault();
            if (usuario == null)
                throw new ApplicationException($"No existe usuario cuyo id es {idUsuario}");
            if(usuario.CONTRASENA != HashPassword(contrasenaVieja))
                throw new ApplicationException("La contraseña antigua no es la correcta.");
            usuario.CONTRASENA = HashPassword(contrasenaNueva);
            _repositorio.Actualizar(usuario);
        }

        public void LiberarRecursos()
        {
            _repositorio.LiberarRecursos();
        }
    }

    public class FiltroUsuarioParaLogin : IFiltros<USUARIO>
    {
        private readonly string _userName;
        private readonly string _contrasena;

        public FiltroUsuarioParaLogin(string userName, string contrasena)
        {
            _userName = userName;
            _contrasena = contrasena;
        }

        public Expression<Func<USUARIO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<USUARIO>(u => u.USER_NAME == _userName && u.CONTRASENA == _contrasena && u.ESTA_ACTIVO);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioPorId : IFiltros<USUARIO>
    {
        private readonly int _idUsuario;

        public FiltroUsuarioPorId(int idUsuario)
        {
            _idUsuario = idUsuario;
        }

        public Expression<Func<USUARIO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<USUARIO>(u => u.ID_USUARIO == _idUsuario);
            return filtro.SastifechoPor();
        }
    }
}
