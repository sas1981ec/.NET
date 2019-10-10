using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioDetalleCompra : IRepositorio<DETALLE_COMPRA>
    {
        IEnumerable<DETALLE_COMPRA> ObtenerDetallesCompraConItem(IFiltros<DETALLE_COMPRA> filtro);
    }
}
