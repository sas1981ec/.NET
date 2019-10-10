using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioStock : IStock
    {
        private readonly IRepositorioItem _repositorioItem;

        public ServicioStock(IRepositorioItem repositorioItem)
        {
            _repositorioItem = repositorioItem;
        }

        public IEnumerable<Stock> ObtenerStockItems()
        {
            var resultado = new List<Stock>();
            var items = _repositorioItem.ObtenerItemsConStocks(new FiltroItem());
            foreach (var item in items)
            {
                resultado.Add(new Stock
                {
                    NombreItem = item.NOMBRE,
                    EtiquetaMedida = item.MEDIDA.ETIQUETA,
                    CantidadBodegaProduccion = item.STOCK_PRODUCCION.CANTIDAD + item.STOCK_PRODUCCION.CANTIDAD_ORDEN_PRODUCCION,
                    CantidadBodedaPrincipal = item.STOCK_BODEGA_PRINCIPAL.CANTIDAD
                });
            }
            return resultado;
        }

        public void LiberarRecursos()
        {
            _repositorioItem.LiberarRecursos();
        }
    }
}
