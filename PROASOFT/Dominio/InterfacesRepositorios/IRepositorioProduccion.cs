using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;

namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioProduccion : IRepositorio<PRODUCCION>
    {
        IEnumerable<PRODUCCION> ObtenerProduccionConDetalles(IFiltros<PRODUCCION> filtro);
        IEnumerable<PRODUCCION> ObtenerProduccionesConDetallesYRecetas(IFiltros<PRODUCCION> filtro);
    }
}
