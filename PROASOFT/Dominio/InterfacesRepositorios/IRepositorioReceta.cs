using PROASOFT.CapaDominio.Dominio.Filtros;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioReceta : IRepositorio<Entidades.RECETA>
    {
        IEnumerable<Entidades.RECETA> ObtenerRecetasConItems(IFiltros<Entidades.RECETA> filtro);
    }
}
