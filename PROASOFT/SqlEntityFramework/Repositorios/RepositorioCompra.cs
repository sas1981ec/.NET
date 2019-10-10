using System.Collections.Generic;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public class RepositorioCompra : Repositorio<COMPRA>, IRepositorioCompra
    {
        public IEnumerable<COMPRA> ObtenerComprasConUsuarioProveedor(IFiltros<COMPRA> filtro)
        {
            return Contexto.COMPRAS.Include("USUARIO").Include("PROVEEDOR").Where(filtro.SastifechoPor());
        }
    }
}
