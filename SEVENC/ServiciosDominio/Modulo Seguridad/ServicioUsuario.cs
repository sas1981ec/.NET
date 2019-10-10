using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Filtros;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario;
using SEVENC.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using SEVENC.Aplicacion.Aplicacion.Contratos.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.SesionUsuario;
using SEVENC.Dominio.General;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioUsuario : IUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioRol _repositorioRol;
        private readonly IRepositorioSesionUsuario _repositorioSesionUsuario;

        public ServicioUsuario(IRepositorioUsuario repositorioUsuario, IRepositorioRol repositorioRol, IRepositorioSesionUsuario repositorioSesionUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioRol = repositorioRol;
            _repositorioSesionUsuario = repositorioSesionUsuario;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return _repositorioUsuario.ObtenerObjetos(new FiltroUsuario()).OrderBy(u => u.Apellidos).ThenBy(u => u.Nombres);
        }

        public IEnumerable<Usuario> ObtenerUsuariosPorCriteriosBusqueda(Dictionary<Busqueda, string> criteriosBusqueda, int indicePagina)
        {
            IFiltros<Usuario> filtro = new FiltroUsuario();
            if (criteriosBusqueda.Count() != 0)
            {
                var manejadorBusquedaPorId = new ManejadorBusquedaPorId();
                foreach (var criterioBusqueda in criteriosBusqueda)
                    manejadorBusquedaPorId.ManejarFiltros(criterioBusqueda, ref filtro);
            }
            return _repositorioUsuario.ObtenerObjetosPorPagineo(filtro, indicePagina, 5, u => u.Apellidos, true);
        }

        public IEnumerable<Rol> ObtenerRolesPorIdUsuario(int idUsuario)
        {
            return _repositorioRol.ObtenerObjetos(new FiltroRolPorIdUsuario(idUsuario)).OrderBy(r => r.Nombre);
        }

        public void CrearUsuario(Usuario usuario)
        {
            var contrasena = Membership.GeneratePassword(10, 0);
            usuario.Contrasena = Encriptar.HashPassword(contrasena);
            _repositorioUsuario.Agregar(usuario);
            //SendEmail(usuario.Email, "Bienvenido a SEVENC",
                  //  $"Estimado {usuario.Apellidos} {usuario.Nombres}<p>Lo han registrado a SEVENC, para acceder al sistema utilice las siguientes credenciales:<p>Usuario :{usuario.UserName}<p>Contraseña:{contrasena}<p>Gracias y Bienvenido a SEVENC</p>");
        }



        private void SendEmail(string para, string asunto, string mensaje)
        {
            using (var message = new MailMessage(ConfigurationManager.AppSettings["De"], para) { Subject = asunto, SubjectEncoding = Encoding.UTF8, Body = mensaje, Priority = MailPriority.High, IsBodyHtml = true })
            {
                message.Body += Environment.NewLine;
                message.BodyEncoding = Encoding.UTF8;

                using (var client = new SmtpClient(ConfigurationManager.AppSettings["Host"], Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"])))
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["De"], ConfigurationManager.AppSettings["ContrasenaCorreo"]);
                    client.Timeout = 10000;
                    client.Send(message);
                }
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            var usuarioAModificar = _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorId(usuario.IdUsuario)).FirstOrDefault();
            if (usuarioAModificar == null)
                throw new ApplicationException($"No existe usuario {usuario.IdUsuario}");
            if (!usuarioAModificar.Concurrencia.SequenceEqual(usuario.Concurrencia))
                throw new ApplicationException("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.");
            usuarioAModificar.Apellidos = usuario.Apellidos;
            usuarioAModificar.Email = usuario.Email;
            usuarioAModificar.EstaActivo = usuario.EstaActivo;
            usuarioAModificar.EstaBloqueado = usuario.EstaBloqueado;
            usuarioAModificar.Foto = usuario.Foto;
            usuarioAModificar.Nombres = usuario.Nombres;
            usuarioAModificar.EstaEliminado = usuario.EstaEliminado;
            _repositorioUsuario.Actualizar(usuarioAModificar);
        }

        public void CambiarContrasena(int idUsuario, string contrasena)
        {
            var usuarioAModificar = _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorId(idUsuario)).FirstOrDefault();
            if (usuarioAModificar == null)
                throw new ApplicationException($"No existe usuario {idUsuario}");
            usuarioAModificar.Contrasena = Encriptar.HashPassword(contrasena);
            _repositorioUsuario.Actualizar(usuarioAModificar);
        }

        public void GenerarContrasena(int idUsuario)
        {
            var contrasena = Membership.GeneratePassword(10, 0);
            var usuarioAModificar = _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorId(idUsuario)).FirstOrDefault();
            if (usuarioAModificar == null)
                throw new ApplicationException($"No existe usuario {idUsuario}");
            usuarioAModificar.Contrasena = Encriptar.HashPassword(contrasena);
            _repositorioUsuario.Actualizar(usuarioAModificar);
            //SendEmail(usuarioAModificar.Email, "Cambio de Contraseña SEVENC",
              //      $"Estimado {usuarioAModificar.Apellidos} {usuarioAModificar.Nombres}<p>Se ha restablecido su contraseña, para acceder al sistema utilice las siguientes credenciales:<p>Usuario :{usuarioAModificar.UserName}<p>Contraseña:{contrasena}<p>Gracias y Bienvenido a SEVENC</p>");
        }

        public bool ExisteUserName(string userName)
        {
            return _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorUserName(userName)).Any();
        }

        public bool ExisteEmail(string email)
        {
            return _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorEmail(email)).Any();
        }

        public void AsignarRolesAUsuario(IEnumerable<int> idsRoles, int idUsuario)
        {
            _repositorioUsuario.AsignarRolesAUsuarios(idsRoles, idUsuario);
        }

        public LoginData Login(string userName, string contrasena, string ip)
        {
            var usuario = _repositorioUsuario.ObtenerUsuarioParaLogin(new FiltroUsuarioParaLogin(userName, Encriptar.HashPassword(contrasena)));
            if (usuario == null)
                return new LoginData { Mensaje = "Usuario y/o contraseña inválidas." };
            else if (!usuario.EstaActivo)
                return new LoginData { Mensaje = "Su usuario no esta activo para usar el software." };
            else if (usuario.EstaBloqueado)
                return new LoginData { Mensaje = "Su usuario está bloqueado." };
            else if (usuario.Empresas.Count == 0)
                return new LoginData { Mensaje = "Su usuario no tiene empresas asociadas." };
            else if (usuario.Roles.Count == 0)
                return new LoginData { Mensaje = "Su usuario no tiene roles asociados." };
            else
            {
                var sesion = new SesionUsuario { FechaInicio = DateTime.Now, IdUsuario = usuario.IdUsuario, Ip = ip };
                _repositorioSesionUsuario.Agregar(sesion);
                return new LoginData
                {
                    Mensaje = "Ok.",
                    FueOk = true,
                    IdUsuario = usuario.IdUsuario,
                    UserName = userName,
                    NombreUsuario = $"{usuario.Apellidos} {usuario.Nombres}",
                    IdSesion = sesion.IdSesion,
                    Empresas = usuario.Empresas,
                    Operaciones = ObtenerOperaciones(usuario.Roles)
                };
            }
        }

        private Dictionary<int, bool> ObtenerOperaciones(ICollection<Rol> roles)
        {
            var resultado = new Dictionary<int, bool>();
            foreach (var rol in roles)
            {
                foreach (var operacion in rol.Operaciones)
                {
                    if (!resultado.ContainsKey(operacion.IdOperacion))
                        resultado.Add(operacion.IdOperacion, operacion.EsAuditable);
                }
            }
            return resultado;
        }

        public void CerrarSesion(long idSesion)
        {
            var sesion = _repositorioSesionUsuario.ObtenerObjetos(new FiltroSesionUsuarioPorId(idSesion)).FirstOrDefault();
            sesion.FechaFin = DateTime.Now;
            _repositorioSesionUsuario.Actualizar(sesion);
        }

        public void LiberarRecursos()
        {
            _repositorioUsuario.LiberarRecursos();
            _repositorioRol.LiberarRecursos();
            _repositorioSesionUsuario.LiberarRecursos();
        }
    }

    public class FiltroUsuario : Filtros<Usuario>
    {
        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => !u.EsSistema && !u.EstaEliminado);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioPorId : Filtros<Usuario>
    {
        private int _idUsuario;

        public FiltroUsuarioPorId(int idUsuario)
        {
            _idUsuario = idUsuario;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => u.IdUsuario == _idUsuario);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioPorUserName : Filtros<Usuario>
    {
        private string _userName;

        public FiltroUsuarioPorUserName(string userName)
        {
            _userName = userName;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => u.UserName == _userName && !u.EstaEliminado);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioPorEmail: Filtros<Usuario>
    {
        private string _email;

        public FiltroUsuarioPorEmail(string email)
        {
            _email = email;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => u.Email == _email && !u.EstaEliminado);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioParaLogin : Filtros<Usuario>
    {
        private string _userName;
        private string _contrasena;

        public FiltroUsuarioParaLogin(string userName, string contrasena)
        {
            _userName = userName;
            _contrasena = contrasena;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => u.UserName == _userName && u.Contrasena == _contrasena && !u.EstaEliminado);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroSesionUsuarioPorId : Filtros<SesionUsuario>
    {
        private long _idSesion;

        public FiltroSesionUsuarioPorId(long idSesion)
        {
            _idSesion = idSesion;
        }

        public override Expression<Func<SesionUsuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<SesionUsuario>(su => su.IdSesion == _idSesion);
            return filtro.SastifechoPor();
        }
    }

    internal abstract class ManejadorBusqueda
    {
        protected ManejadorBusqueda Siguiente;

        protected ManejadorBusqueda EstablecerSiguiente(ManejadorBusqueda siguiente)
        {
            Siguiente = siguiente;
            return Siguiente;
        }

        internal abstract void ManejarFiltros(KeyValuePair<Busqueda, string> item, ref IFiltros<Usuario> filtro);
    }

    internal class ManejadorBusquedaPorId : ManejadorBusqueda
    {
        internal override void ManejarFiltros(KeyValuePair<Busqueda, string> item, ref IFiltros<Usuario> filtro)
        {
            if (item.Key == Busqueda.PorId)
            {
                
                var idUsuario = Convert.ToUInt32(item.Value);
                var filtroDerecha = new FiltroDirecto<Usuario>(u => u.IdUsuario == idUsuario);
                filtro = new FiltroAnd<Usuario>(filtro, filtroDerecha);
                return;
            }
            EstablecerSiguiente(new ManejadorBusquedaPorUserName());
            Siguiente.ManejarFiltros(item, ref filtro);
        }
    }

    internal class ManejadorBusquedaPorUserName : ManejadorBusqueda
    {
        internal override void ManejarFiltros(KeyValuePair<Busqueda, string> item, ref IFiltros<Usuario> filtro)
        {
            if (item.Key == Busqueda.PorUserName)
            {
                var filtroDerecha = new FiltroDirecto<Usuario>(u => u.UserName == item.Value);
                filtro = new FiltroAnd<Usuario>(filtro, filtroDerecha);
                return;
            }
            EstablecerSiguiente(new ManejadorBusquedaPorUserEmail());
            Siguiente.ManejarFiltros(item, ref filtro);
        }
    }

    internal class ManejadorBusquedaPorUserEmail : ManejadorBusqueda
    {
        internal override void ManejarFiltros(KeyValuePair<Busqueda, string> item, ref IFiltros<Usuario> filtro)
        {
            if (item.Key == Busqueda.PorEmail)
            {
                var filtroDerecha = new FiltroDirecto<Usuario>(u => u.Email == item.Value);
                filtro = new FiltroAnd<Usuario>(filtro, filtroDerecha);
                return;
            }
            EstablecerSiguiente(new ManejadorBusquedaPorNombresApellidos());
            Siguiente.ManejarFiltros(item, ref filtro);
        }
    }

    internal class ManejadorBusquedaPorNombresApellidos : ManejadorBusqueda
    {
        internal override void ManejarFiltros(KeyValuePair<Busqueda, string> item, ref IFiltros<Usuario> filtro)
        {
            if (item.Key == Busqueda.PorNombresApellidos)
            {
                var filtroDerecha = new FiltroDirecto<Usuario>(u => u.Apellidos.Contains(item.Value) || u.Nombres.Contains(item.Value));
                filtro = new FiltroAnd<Usuario>(filtro, filtroDerecha);
                return;
            }
        }
    }
}
