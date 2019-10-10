using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioCompra : ICompra
    {
        private readonly IRepositorioCompra _repositorioCompra;
        private readonly IRepositorioDetalleCompra _repositorioDetalleCompra;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IRepositorioProveedor _repositorioProveedor;

        public ServicioCompra(IRepositorioCompra repositorioCompra, IRepositorioDetalleCompra repositorioDetalleCompra, IRepositorioItem repositorioItem, IRepositorioProveedor repositorioProveedor)
        {
            _repositorioCompra = repositorioCompra;
            _repositorioDetalleCompra = repositorioDetalleCompra;
            _repositorioItem = repositorioItem;
            _repositorioProveedor = repositorioProveedor;
        }

        public IEnumerable<COMPRA> ObtenerCompras(DateTime fechaDesde, DateTime fechaHasta)
        {
            return _repositorioCompra.ObtenerComprasConUsuarioProveedor(new FiltroCompraFechas(fechaDesde, fechaHasta));
        }

        public IEnumerable<DETALLE_COMPRA> ObtenerDetallesCompra(int idCompra)
        {
            return _repositorioDetalleCompra.ObtenerDetallesCompraConItem(new FiltroDetalleCompraIdCompra(idCompra));
        }

        public void RegistrarNuevaCompra(COMPRA compra)
        {
            compra.FECHA = DateTime.Now;
            _repositorioCompra.Agregar(compra);
        }

        public void ConfirmarCompra(int idCompra, short idUsuario)
        {
            using (var transaccion = new TransactionScope())
            {
                var compra = _repositorioCompra.ObtenerObjetos(new FiltroCompra(idCompra)).FirstOrDefault();
                if (compra == null)
                    throw new ApplicationException($"No existe compra cuyo id es {idCompra}");
                compra.ESTA_CONFIRMADA = true;
                compra.ID_USUARIO = idUsuario;
                _repositorioCompra.Actualizar(compra);
                ActualizarCostoItems(compra); 
                transaccion.Complete();
            }
        }

        private void ActualizarCostoItems(COMPRA compra)
        {
            var items = _repositorioItem.ObtenerItemsConStocks(new FiltroItem());
            foreach (var detalleCompra in compra.DETALLES_COMPRAS)
            {
                var item = items.FirstOrDefault(i => i.ID_ITEM == detalleCompra.ID_ITEM);
                item.STOCK_BODEGA_PRINCIPAL.CANTIDAD = item.STOCK_BODEGA_PRINCIPAL.CANTIDAD + detalleCompra.CANTIDAD;
                var cantidad = item.STOCK_BODEGA_PRINCIPAL.CANTIDAD;
                item.COSTO_POR_KILOGRAMO = cantidad == detalleCompra.CANTIDAD ? Convert.ToDecimal(detalleCompra.PRECIO / detalleCompra.CANTIDAD)
                    : ((Convert.ToDecimal(cantidad - detalleCompra.CANTIDAD) * item.COSTO_POR_KILOGRAMO) + (detalleCompra.PRECIO)) / Convert.ToDecimal(cantidad);
                _repositorioItem.Actualizar(item);
            }
        }

        public void ModificarDetalleCompra(DETALLE_COMPRA detalleCompra, IEnumerable<short> idRoles)
        {
            using (var transaccion = new TransactionScope())
            {
                var detalleCompraAModificar = _repositorioDetalleCompra.ObtenerObjetos(new FiltroDetalleCompra(detalleCompra.ID_DETALLE)).FirstOrDefault();
                if (detalleCompraAModificar == null)
                    throw new ApplicationException($"No existe detalle compra cuyo id es {detalleCompra.ID_DETALLE}");
                if (!idRoles.ToList().Contains(2) && (DateTime.Now - detalleCompra.COMPRA.FECHA).Days >= 1)
                    throw new ApplicationException($"No se puede modificar la compra.");
                detalleCompraAModificar.CANTIDAD = detalleCompra.CANTIDAD;
                detalleCompraAModificar.PRECIO = detalleCompra.PRECIO;
                _repositorioDetalleCompra.Actualizar(detalleCompraAModificar);
                ActualizarUsuario(detalleCompraAModificar.ID_COMPRA, detalleCompra.COMPRA.ID_USUARIO);
                transaccion.Complete();
            }
        }

        private void ActualizarUsuario(int idCompra, short idUsuario)
        {
            var compra = _repositorioCompra.ObtenerObjetos(new FiltroCompra(idCompra)).FirstOrDefault();
            if(compra == null)
                throw new ApplicationException($"No existe compra cuyo id es {idCompra}");
            compra.ID_USUARIO = idUsuario;
            _repositorioCompra.Actualizar(compra);
        }

        public IEnumerable<ITEM> ObtenerItems()
        {
            return _repositorioItem.ObtenerItemsConMedida(new FiltroItem()).OrderBy(i => i.NOMBRE);
        }

        public IEnumerable<PROVEEDOR> ObtenerProveedores()
        {
            return _repositorioProveedor.ObtenerObjetos(new FiltroProveedorActivo()).OrderBy(p => p.RAZON_SOCIAL);
        }

        public void LiberarRecursos()
        {
            _repositorioCompra.LiberarRecursos();
            _repositorioDetalleCompra.LiberarRecursos();
            _repositorioItem.LiberarRecursos();
            _repositorioProveedor.LiberarRecursos();
        }
    }

    public class FiltroCompraFechas : IFiltros<COMPRA>
    {
        private readonly DateTime _fechaDesde;
        private readonly DateTime _fechaHasta;

        public FiltroCompraFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
        }

        public Expression<Func<COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<COMPRA>(c => c.FECHA >= _fechaDesde && c.FECHA <= _fechaHasta);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroDetalleCompraIdCompra : IFiltros<DETALLE_COMPRA>
    {
        private readonly int _idCompra;

        public FiltroDetalleCompraIdCompra(int idCompra)
        {
            _idCompra = idCompra;
        }

        public Expression<Func<DETALLE_COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<DETALLE_COMPRA>(dc => dc.ID_COMPRA == _idCompra);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroCompra : IFiltros<COMPRA>
    {
        private readonly int _idCompra;

        public FiltroCompra(int idCompra)
        {
            _idCompra = idCompra;
        }

        public Expression<Func<COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<COMPRA>(c => c.ID_COMPRA == _idCompra);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroDetalleCompra : IFiltros<DETALLE_COMPRA>
    {
        private readonly long _id;

        public FiltroDetalleCompra(long id)
        {
            _id = id;
        }

        public Expression<Func<DETALLE_COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<DETALLE_COMPRA>(dc => dc.ID_DETALLE == _id);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroItem : IFiltros<ITEM>
    {
        public Expression<Func<ITEM, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM>(i => i.ESTA_ACTIVO);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroProveedorActivo : IFiltros<PROVEEDOR>
    {
        public Expression<Func<PROVEEDOR, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PROVEEDOR>(p => p.ESTA_ACTIVO);
            return filtro.SastifechoPor();
        }
    }
}
