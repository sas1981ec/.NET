using SEVENC.Aplicacion.Aplicacion.Contratos.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IUsuario : IBase
    {
        [OperationContract]
        IEnumerable<Usuario> ObtenerUsuarios();

        [OperationContract]
        IEnumerable<Usuario> ObtenerUsuariosPorCriteriosBusqueda(Dictionary<Busqueda, string> criteriosBusqueda, int indicePagina);

        [OperationContract]
        IEnumerable<Rol> ObtenerRolesPorIdUsuario(int idUsuario);

        [OperationContract]
        void CrearUsuario(Usuario usuario);

        [OperationContract]
        void ActualizarUsuario(Usuario usuario);

        [OperationContract]
        void CambiarContrasena(int idUsuario, string contrasena);

        [OperationContract]
        void GenerarContrasena(int idUsuario);

        [OperationContract]
        bool ExisteUserName(string userName);

        [OperationContract]
        bool ExisteEmail(string email);

        [OperationContract]
        void AsignarRolesAUsuario(IEnumerable<int> idsRoles, int idUsuario);

        [OperationContract]
        LoginData Login(string userName, string contrasena, string ip);

        [OperationContract]
        void CerrarSesion(long idSesion);
    }

    public enum Busqueda
    {
        PorId,
        PorUserName,
        PorEmail,
        PorNombresApellidos
    }
}
