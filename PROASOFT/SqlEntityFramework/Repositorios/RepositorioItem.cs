using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioItem : Repositorio<ITEM>, IRepositorioItem
    {
        public IEnumerable<ITEM> ObtenerItemsConMedida(IFiltros<ITEM> filtro)//VALE
        {
            return Contexto.ITEMS.Include("MEDIDA").Where(filtro.SastifechoPor());
        }

        public ITEM ObtenerItemConStocks(IFiltros<ITEM> filtro) //VALE
        {
            return Contexto.ITEMS.Include("STOCK_BODEGA_PRINCIPAL").Include("STOCK_PRODUCCION").FirstOrDefault(filtro.SastifechoPor());
        }

        public IEnumerable<ITEM> ObtenerItemsConStocks(IFiltros<ITEM> filtro)//VALE
        {
            return Contexto.ITEMS.Include("STOCK_BODEGA_PRINCIPAL").Include("STOCK_PRODUCCION").Where(filtro.SastifechoPor());
        }
    }
}
