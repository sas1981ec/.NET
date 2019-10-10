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
    public class ServicioItemProduccion : IItemProduccion
    {
        internal readonly IRepositorioItemProduccion Repositorio;
        internal readonly IRepositorioDetalleItemProduccion RepositorioDetalle;
        internal readonly IRepositorioStockProduccion RepositorioStockProduccion;
        internal readonly IRepositorioItem RepositorioItem;
        internal readonly IRepositorioProduccion RepositorioProduccion;
        private EstadoIngresoBodegaProduccion _estado;
        internal ITEM_PRODUCCION ItemProduccion;
        internal DETALLE_ITEM_PRODUCCION DetalleItemProduccion;
        internal int Diferencia;

        public ServicioItemProduccion(IRepositorioItemProduccion repositorio, IRepositorioDetalleItemProduccion repositorioDetalle, IRepositorioStockProduccion repositorioStockProduccion, IRepositorioItem repositorioItem, IRepositorioProduccion repositorioProduccion)
        {
            Repositorio = repositorio;
            RepositorioDetalle = repositorioDetalle;
            RepositorioStockProduccion = repositorioStockProduccion;
            RepositorioItem = repositorioItem;
            RepositorioProduccion = repositorioProduccion;
        }

        public IEnumerable<ITEM_PRODUCCION> ObtenerItemsProduccion(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Repositorio.ObtenerItemsProduccionConDetalles(new FiltroItemProduccion(fechaDesde, fechaHasta));
        }

        public void CrearItemProduccion(ITEM_PRODUCCION itemProduccion)
        {
            using (var transaccion = new TransactionScope())
            {
                ItemProduccion = itemProduccion;
                SetearEstado(new EstadoIngresoItemProduccion());
                transaccion.Complete();
            }
        }

        public void ActualizarItemProduccion(DETALLE_ITEM_PRODUCCION detalleItemProduccion)
        {
            using (var transaccion = new TransactionScope())
            {
                DetalleItemProduccion = detalleItemProduccion;
                SetearEstado(new EstadoActualizarItemProduccion());
                transaccion.Complete();
            }
        }

        public void LiberarRecursos()
        {
            Repositorio.LiberarRecursos();
            RepositorioDetalle.LiberarRecursos();
            RepositorioStockProduccion.LiberarRecursos();
            RepositorioItem.LiberarRecursos();
            RepositorioProduccion.LiberarRecursos();
        }

        internal void SetearEstado(EstadoIngresoBodegaProduccion estado)
        {
            _estado = estado;
            _estado.EjecutarProceso(this);
        }
    }

    public class FiltroItemProduccion : IFiltros<ITEM_PRODUCCION>
    {
        private readonly DateTime _fechaDesde;
        private readonly DateTime _fechaHasta;

        public FiltroItemProduccion(DateTime fechaDesde, DateTime fechaHasta)
        {
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
        }

        public Expression<Func<ITEM_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM_PRODUCCION>(ip => ip.FECHA >= _fechaDesde && ip.FECHA <= _fechaHasta);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroItemProduccionHoy : IFiltros<ITEM_PRODUCCION>
    {
        public Expression<Func<ITEM_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM_PRODUCCION>(ip => ip.FECHA.Day == DateTime.Now.Day && ip.FECHA.Month == DateTime.Now.Month && ip.FECHA.Year == DateTime.Now.Year);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroDetalleItemProduccionPorId : IFiltros<DETALLE_ITEM_PRODUCCION>
    {
        private readonly int _id;

        public FiltroDetalleItemProduccionPorId(int id)
        {
            _id = id;
        }

        public Expression<Func<DETALLE_ITEM_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<DETALLE_ITEM_PRODUCCION>(dip => dip.ID_DETALLE == _id);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroItemProduccionPorId : IFiltros<ITEM_PRODUCCION>
    {
        private readonly int _id;

        public FiltroItemProduccionPorId(int id)
        {
            _id = id;
        }

        public Expression<Func<ITEM_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM_PRODUCCION>(ip => ip.ID == _id);
            return filtro.SastifechoPor();
        }
    }

    internal abstract class EstadoIngresoBodegaProduccion
    {
        internal abstract void EjecutarProceso(ServicioItemProduccion servicio);
    }

    internal class EstadoIngresoItemProduccion : EstadoIngresoBodegaProduccion
    {
        internal override void EjecutarProceso(ServicioItemProduccion servicio)
        {
            var item = servicio.RepositorioProduccion.ObtenerObjetos(new FiltroProduccionHoy()).FirstOrDefault();
            if (item == null)
            {
                servicio.ItemProduccion.FECHA = DateTime.Now;
                servicio.Repositorio.Agregar(servicio.ItemProduccion);
                servicio.SetearEstado(new EstadoActualizarStock());
            }
            else
                throw new ApplicationException($"No se puede ingresar items a produccion ya que la producción del día fue ingresada.");
        }
    }

    internal class EstadoActualizarStock : EstadoIngresoBodegaProduccion
    {
        internal override void EjecutarProceso(ServicioItemProduccion servicio)
        {
            var stockProduccion = servicio.RepositorioStockProduccion.ObtenerObjetos(new FiltroStockProduccion());
            var items = servicio.RepositorioItem.ObtenerItemsConMedida(new FiltroItemGeneral());
            foreach (var detalle in servicio.ItemProduccion.DETALLES_ITEM_PRODUCCION)
            {
                var item = items.FirstOrDefault(i => i.ID_ITEM == detalle.ID_ITEM);
                var stock = stockProduccion.FirstOrDefault(sp => sp.ITEM.ID_ITEM == detalle.ID_ITEM);
                var totalItem = detalle.CANTIDAD * item.CANTIDAD * item.MEDIDA.VALOR;
                if (stock == null)
                    CrearStockProduccion(servicio, new STOCK_PRODUCCION { ITEM = new ITEM { ID_ITEM = detalle.ID_ITEM }, CANTIDAD_GRAMOS = totalItem });
                else
                    ActualizarStockProduccion(servicio, stock, totalItem);
            }
        }

        private void CrearStockProduccion(ServicioItemProduccion servicio, STOCK_PRODUCCION stockProduccion)
        {
            servicio.RepositorioStockProduccion.CrearStockProduccion(stockProduccion);
        }

        private void ActualizarStockProduccion(ServicioItemProduccion servicio, STOCK_PRODUCCION stockProduccion, double totalItem)
        {
            var stock = servicio.RepositorioStockProduccion.ObtenerObjetos(new FiltroStockProduccionPorId(stockProduccion.ID)).FirstOrDefault();
            if (stock == null)
                throw new ApplicationException($"No existe Stock Producción cuyo id es {stockProduccion.ID}");
            stock.CANTIDAD_GRAMOS = stock.CANTIDAD_GRAMOS + totalItem;
            servicio.RepositorioStockProduccion.Actualizar(stock);
        }
    }

    internal class EstadoActualizarItemProduccion : EstadoIngresoBodegaProduccion
    {
        internal override void EjecutarProceso(ServicioItemProduccion servicio)
        {
            var detalleItemProduccionAModificar = servicio.RepositorioDetalle.ObtenerObjetos(new FiltroDetalleItemProduccionPorId(servicio.DetalleItemProduccion.ID_DETALLE)).FirstOrDefault();
            if (detalleItemProduccionAModificar == null)
                throw new ApplicationException($"No existe detalle item de producción cuyo id es {servicio.DetalleItemProduccion.ID_DETALLE}");
            if (DateTime.Now > DateTime.Parse(servicio.DetalleItemProduccion.ITEM_PRODUCCION.FECHA.ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59))
                throw new ApplicationException($"No se puede modificar el detalle item de producción.");
            servicio.Diferencia = detalleItemProduccionAModificar.CANTIDAD - servicio.DetalleItemProduccion.CANTIDAD;
            detalleItemProduccionAModificar.CANTIDAD = servicio.DetalleItemProduccion.CANTIDAD;
            servicio.RepositorioDetalle.Actualizar(detalleItemProduccionAModificar);
            ActualizarUsuario(servicio, detalleItemProduccionAModificar.ID_ITEM_PRODUCCION, servicio.DetalleItemProduccion.ITEM_PRODUCCION.ID_USUARIO);
            servicio.SetearEstado(new EstadoActualizarUnStock());
        }

        private void ActualizarUsuario(ServicioItemProduccion servicio, int id, short idUsuario)
        {
            var itemProduccion = servicio.Repositorio.ObtenerObjetos(new FiltroItemProduccionPorId(id)).FirstOrDefault();
            if (itemProduccion == null)
                throw new ApplicationException($"No existe item de producción cuyo id es {id}");
            itemProduccion.ID_USUARIO = idUsuario;
            servicio.Repositorio.Actualizar(itemProduccion);
        }
    }

    internal class EstadoActualizarUnStock : EstadoIngresoBodegaProduccion
    {
        internal override void EjecutarProceso(ServicioItemProduccion servicio)
        {
            var stockProduccion = servicio.RepositorioStockProduccion.ObtenerObjetos(new FiltroStockProduccion()).FirstOrDefault(sp => sp.ITEM.ID_ITEM == servicio.DetalleItemProduccion.ID_ITEM);
            var item = servicio.RepositorioItem.ObtenerItemsConMedida(new FiltroItemGeneral()).FirstOrDefault(i => i.ID_ITEM == servicio.DetalleItemProduccion.ID_ITEM);
            if(stockProduccion == null || item == null)
                throw new ApplicationException("Data inconsistente.");
            var totalItem = servicio.Diferencia * item.CANTIDAD * item.MEDIDA.VALOR;
            ActualizarStockProduccion(servicio, stockProduccion, totalItem);
        }

        private void ActualizarStockProduccion(ServicioItemProduccion servicio, STOCK_PRODUCCION stockProduccion, double totalItem)
        {
            var stock = servicio.RepositorioStockProduccion.ObtenerObjetos(new FiltroStockProduccionPorId(stockProduccion.ID)).FirstOrDefault();
            if (stock == null)
                throw new ApplicationException($"No existe Stock Producción cuyo id es {stockProduccion.ID}");
            stock.CANTIDAD_GRAMOS = stock.CANTIDAD_GRAMOS - totalItem;
            servicio.RepositorioStockProduccion.Actualizar(stock);
        }
    }
}
