using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioProveedor : IProveedor
    {
        private readonly IRepositorioProveedor _repositorioProveedor;

        public ServicioProveedor(IRepositorioProveedor repositorioProveedor)
        {
            _repositorioProveedor = repositorioProveedor;
        }

        public IEnumerable<PROVEEDOR> ObtenerProveedores()
        {
            return _repositorioProveedor.ObtenerObjetos(new FiltroProveedor()).OrderBy(e => e.RAZON_SOCIAL);
        }

        public void CrearProveedor(PROVEEDOR proveedor)
        {
            _repositorioProveedor.Agregar(proveedor);
        }

        public void ActualizarProveedor(PROVEEDOR proveedor)
        {
            var proveedorAModificar = _repositorioProveedor.ObtenerObjetos(new FiltroProveedorActualizar(proveedor.ID_PROVEEDOR)).FirstOrDefault();
            if (proveedorAModificar == null)
                throw new ApplicationException($"Proveedor de id : {proveedor.ID_PROVEEDOR} no existe");
            proveedorAModificar.ESTA_ACTIVO = proveedor.ESTA_ACTIVO;
            proveedorAModificar.RAZON_SOCIAL = proveedor.RAZON_SOCIAL;
            proveedorAModificar.RUC = proveedor.RUC;
            _repositorioProveedor.Actualizar(proveedorAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorioProveedor.LiberarRecursos();
        }
    }

    public class FiltroProveedor : IFiltros<PROVEEDOR>
    {
        public Expression<Func<PROVEEDOR, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PROVEEDOR>(p => p.ID_PROVEEDOR > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroProveedorActualizar : Filtros<PROVEEDOR>
    {
        private readonly int _id;

        internal FiltroProveedorActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<PROVEEDOR, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PROVEEDOR>(p => p.ID_PROVEEDOR == _id);
            return filtro.SastifechoPor();
        }
    }
}
