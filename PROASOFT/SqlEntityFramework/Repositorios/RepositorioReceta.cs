using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioReceta : Repositorio<RECETA>, IRepositorioReceta
    {
        public IEnumerable<RECETA> ObtenerRecetasConItems(IFiltros<RECETA> filtro)
        {
            return Contexto.RECETAS.Include("DETALLES_RECETA.ITEM.MEDIDA").Where(filtro.SastifechoPor());
        }
    }
}
