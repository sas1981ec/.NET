using System.Collections.Generic;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Linq;
using PROASOFT.CapaDominio.Dominio.Filtros;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioDetalleCompra : Repositorio<DETALLE_COMPRA>, IRepositorioDetalleCompra
    {
        public IEnumerable<DETALLE_COMPRA> ObtenerDetallesCompraConItem(IFiltros<DETALLE_COMPRA> filtro)
        {
            return Contexto.DETALLES_COMPRAS.Include("ITEM.MEDIDA").Where(filtro.SastifechoPor());
        }
    }
}
