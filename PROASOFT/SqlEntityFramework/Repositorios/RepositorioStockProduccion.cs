using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioStockProduccion : Repositorio<STOCK_PRODUCCION>, IRepositorioStockProduccion
    {
        public bool CrearStockProduccion(STOCK_PRODUCCION stockProduccion)
        {
            var item = Contexto.ITEMS.FirstOrDefault(i => i.ID_ITEM == stockProduccion.ITEM.ID_ITEM);
            if (item == null)
                return false;
            stockProduccion.ITEM = item;
            Contexto.STOCK_PRODUCCION.Add(stockProduccion);
            Contexto.SaveChanges();
            return true;
        }
    }
}
