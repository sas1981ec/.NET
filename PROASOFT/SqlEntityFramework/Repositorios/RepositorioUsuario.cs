using System.Collections.Generic;
using System.Linq;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioUsuario : Repositorio<USUARIO>, IRepositorioUsuario
    {
        public IEnumerable<USUARIO> ObtenerUsuariosConRoles(IFiltros<USUARIO> filtro)
        {
            return Contexto.USUARIOS.Include("ROLES").Where(filtro.SastifechoPor());
        }
    }
}
