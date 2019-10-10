using PROASOFT.CapaDominio.Dominio.Filtros;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Entidades.USUARIO>
    {
        IEnumerable<Entidades.USUARIO> ObtenerUsuariosConRoles(IFiltros<Entidades.USUARIO> filtro);
    }
}
