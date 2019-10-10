using SEVENC.Dominio.Dominio.Filtros;
using System.Collections.Generic;

namespace SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario
{
    public interface IRepositorioUsuario : IRepositorio<Entidades.Usuario>
    {
        void AsignarRolesAUsuarios(IEnumerable<int> idsRoles, int idUsuario);
        Entidades.Usuario ObtenerUsuarioParaLogin(IFiltros<Entidades.Usuario> filtro);
    }
}
