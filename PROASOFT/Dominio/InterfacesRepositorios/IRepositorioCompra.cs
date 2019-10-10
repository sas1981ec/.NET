using PROASOFT.CapaDominio.Dominio.Filtros;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioCompra : IRepositorio<Entidades.COMPRA>
    {
        IEnumerable<Entidades.COMPRA> ObtenerComprasConUsuarioProveedor(IFiltros<Entidades.COMPRA> filtro);
    }
}
