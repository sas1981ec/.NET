using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioProduccion : Repositorio<PRODUCCION>, IRepositorioProduccion
    {
        public IEnumerable<PRODUCCION> ObtenerProduccionConDetalles(IFiltros<PRODUCCION> filtro)
        {
            return Contexto.PRODUCCIONES.Include("DETALLES_PRODUCCION.RECETA").Include("USUARIO").Include("DETALLES_PRODUCCION.EMPLEADO").Where(filtro.SastifechoPor());
        }

        public IEnumerable<PRODUCCION> ObtenerProduccionesConDetallesYRecetas(IFiltros<PRODUCCION> filtro)
        {
            return Contexto.PRODUCCIONES.Include("DETALLES_PRODUCCION.RECETA.DETALLES_RECETA.ITEM.MEDIDA").Where(filtro.SastifechoPor());
        }
    }
}
