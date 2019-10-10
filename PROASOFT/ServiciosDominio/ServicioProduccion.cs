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
    public class ServicioProduccion : IProduccion
    {
        internal readonly IRepositorioProduccion Repositorio;
        private readonly IRepositorioReceta _repositorioReceta;
        internal readonly IRepositorioItem RepositorioItem;
        private readonly IRepositorioEmpleado _repositorioEmpleado;
        private EstadoProcesoProduccion _estado;
        internal PRODUCCION Produccion;
        internal Dictionary<ITEM, Tuple<double, bool>> ItemCantidad;

        public ServicioProduccion(IRepositorioProduccion repositorio, IRepositorioReceta repositorioReceta, IRepositorioItem repositorioItem, IRepositorioEmpleado repositorioEmpleado)
        {
            Repositorio = repositorio;
            _repositorioReceta = repositorioReceta;
            RepositorioItem = repositorioItem;
            _repositorioEmpleado = repositorioEmpleado;
        }

        public IEnumerable<PRODUCCION> ObtenerOrdenesProducciones(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Repositorio.ObtenerProduccionConDetalles(new FiltroOrdenProduccion(fechaDesde, fechaHasta));
        }

        public IEnumerable<PRODUCCION> ObtenerProducciones(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Repositorio.ObtenerProduccionConDetalles(new FiltroProduccion(fechaDesde, fechaHasta));
        }

        public Tuple<bool, Dictionary<ITEM, Tuple<double, bool>>> ExisteEnBodega(Dictionary<int, short> idRecetasCantidades)
        {
            var itemsNecesariosParaProduccion = new Dictionary<int, double>();
            foreach (var idRecetaCantidad in idRecetasCantidades)
            {
                var receta = _repositorioReceta.ObtenerRecetasConItems(new FiltroRecetaPorId(idRecetaCantidad.Key)).FirstOrDefault();
                foreach (var detalleReceta in receta.DETALLES_RECETA)
                {
                    if (itemsNecesariosParaProduccion.Any(i => i.Key == detalleReceta.ID_ITEM))
                    {
                        var valor = itemsNecesariosParaProduccion.FirstOrDefault(i => i.Key == detalleReceta.ID_ITEM).Value;
                        itemsNecesariosParaProduccion.Remove(detalleReceta.ID_ITEM);
                        itemsNecesariosParaProduccion.Add(detalleReceta.ID_ITEM, valor + (detalleReceta.CANTIDAD * idRecetaCantidad.Value));
                    }
                    else
                        itemsNecesariosParaProduccion.Add(detalleReceta.ID_ITEM, detalleReceta.CANTIDAD * idRecetaCantidad.Value);
                }
            }
            var hayStock = true;
            var diccionario = new Dictionary<ITEM, Tuple<double, bool>>();
            var items = RepositorioItem.ObtenerItemsConStocks(new FiltroItemGeneral());
            foreach (var item in items)
            {
                var itemAProducir = itemsNecesariosParaProduccion.FirstOrDefault(i => i.Key == item.ID_ITEM);
                if (itemAProducir.Value > item.STOCK_BODEGA_PRINCIPAL.CANTIDAD + item.STOCK_PRODUCCION.CANTIDAD)
                {
                    hayStock = false;
                    diccionario.Add(item, new Tuple<double, bool>(itemAProducir.Value - item.STOCK_BODEGA_PRINCIPAL.CANTIDAD - item.STOCK_PRODUCCION.CANTIDAD, false));
                }
            }
            if (hayStock)
            {
                foreach (var item in items)
                {
                    var itemAProducir = itemsNecesariosParaProduccion.FirstOrDefault(i => i.Key == item.ID_ITEM);
                    if (itemAProducir.Value > item.STOCK_PRODUCCION.CANTIDAD)
                        diccionario.Add(item, new Tuple<double, bool>(itemAProducir.Value - item.STOCK_PRODUCCION.CANTIDAD, true));
                    else
                        diccionario.Add(item, new Tuple<double, bool>(itemAProducir.Value, false));
                }
            }
            return new Tuple<bool, Dictionary<ITEM, Tuple<double, bool>>>(hayStock, diccionario);
        }

        public int CrearProduccion(PRODUCCION produccion, Dictionary<ITEM, Tuple<double, bool>> itemCantidad)
        {
            ItemCantidad = itemCantidad;
            using (var transaccion = new TransactionScope())
            {
                Produccion = produccion;
                SetearEstado(new ProcesoGrabarProduccion());
                transaccion.Complete();
            }
            return Produccion.ID_PRODUCCION;
        }

        public IEnumerable<RECETA> ObtenerRecetas()
        {
            return _repositorioReceta.ObtenerObjetos(new FiltroRecetasActiva()).OrderBy(r => r.NOMBRE);
        }

        public void CrearProduccionReal(PRODUCCION produccion)
        {
            produccion.FECHA = DateTime.Now;
            produccion.ES_REAL = true;
            Repositorio.Agregar(produccion);
        }

        public IEnumerable<EMPLEADO> ObtenerEmpleados()
        {
            return _repositorioEmpleado.ObtenerObjetos(new FiltroEmpleadoActivo()).OrderBy(e => e.APELLIDOS).ThenBy(e => e.NOMBRES);
        }

        public void LiberarRecursos()
        {
            Repositorio.LiberarRecursos();
            _repositorioReceta.LiberarRecursos();
            RepositorioItem.LiberarRecursos();
            _repositorioEmpleado.LiberarRecursos();
        }

        internal void SetearEstado(EstadoProcesoProduccion estado)
        {
            _estado = estado;
            _estado.EjecutarProceso(this);
        }
    }

    public class FiltroOrdenProduccion : IFiltros<PRODUCCION>
    {
        private readonly DateTime _fechaDesde;
        private readonly DateTime _fechaHasta;

        public FiltroOrdenProduccion(DateTime fechaDesde, DateTime fechaHasta)
        {
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
        }

        public Expression<Func<PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PRODUCCION>(p => !p.ES_REAL && p.FECHA >= _fechaDesde && p.FECHA <= _fechaHasta);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroProduccion : IFiltros<PRODUCCION>
    {
        private readonly DateTime _fechaDesde;
        private readonly DateTime _fechaHasta;

        public FiltroProduccion(DateTime fechaDesde, DateTime fechaHasta)
        {
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
        }

        public Expression<Func<PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PRODUCCION>(p => p.ES_REAL && p.FECHA >= _fechaDesde && p.FECHA <= _fechaHasta);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroRecetasActiva : IFiltros<RECETA>
    {
        public Expression<Func<RECETA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<RECETA>(r => r.ESTA_ACTIVA);
            return filtro.SastifechoPor();
        }
    }

    internal abstract class EstadoProcesoProduccion
    {
        internal abstract void EjecutarProceso(ServicioProduccion servicio);
    }

    internal class ProcesoGrabarProduccion : EstadoProcesoProduccion
    {
        internal override void EjecutarProceso(ServicioProduccion servicio)
        {
            servicio.Produccion.FECHA = DateTime.Now;
            servicio.Repositorio.Agregar(servicio.Produccion);
            servicio.SetearEstado(new EstadoActualizarStock());
        }
    }

    internal class EstadoActualizarStock : EstadoProcesoProduccion
    {
        internal override void EjecutarProceso(ServicioProduccion servicio)
        {
            foreach (var itemCantidad in servicio.ItemCantidad)
            {
                var item = servicio.RepositorioItem.ObtenerItemConStocks(new FiltroItemPorId(itemCantidad.Key.ID_ITEM));
                item.STOCK_BODEGA_PRINCIPAL.CANTIDAD = item.STOCK_BODEGA_PRINCIPAL.CANTIDAD - (itemCantidad.Value.Item2 ? itemCantidad.Value.Item1 : 0);
                item.STOCK_PRODUCCION.CANTIDAD = item.STOCK_PRODUCCION.CANTIDAD - (itemCantidad.Value.Item2 ? 0 : itemCantidad.Value.Item1);
                item.STOCK_PRODUCCION.CANTIDAD_ORDEN_PRODUCCION = (itemCantidad.Value.Item2 ? itemCantidad.Value.Item1 : 0);
                servicio.RepositorioItem.Actualizar(item);
            }
        }
    }

    public class FiltroEmpleadoActivo : IFiltros<EMPLEADO>
    {
        public Expression<Func<EMPLEADO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<EMPLEADO>(e => e.ESTA_ACTIVO);
            return filtro.SastifechoPor();
        }
    }
}