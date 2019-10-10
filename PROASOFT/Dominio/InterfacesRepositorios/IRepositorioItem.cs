using PROASOFT.CapaDominio.Dominio.Filtros;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioItem : IRepositorio<Entidades.ITEM>
    {
        IEnumerable<Entidades.ITEM> ObtenerItemsConMedida(IFiltros<Entidades.ITEM> filtro);
        Entidades.ITEM ObtenerItemConStocks(IFiltros<Entidades.ITEM> filtro);
        IEnumerable<Entidades.ITEM> ObtenerItemsConStocks(IFiltros<Entidades.ITEM> filtro);
    }
}
