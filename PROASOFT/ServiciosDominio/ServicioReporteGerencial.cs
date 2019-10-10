using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
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
    //public class ServicioReporteGerencial : IReporteGerencial
    //{
    //    private readonly IRepositorioProduccion _repositorioProduccion;
    //    private readonly IRepositorioItemProduccion _repositorioItemProduccion;
    //    private readonly IRepositorioDetalleCompra _repositorioDetalleCompra;

    //    public ServicioReporteGerencial(IRepositorioProduccion repositorioProduccion, IRepositorioItemProduccion repositorioItemProduccion, IRepositorioDetalleCompra repositorioDetalleCompra)
    //    {
    //        _repositorioProduccion = repositorioProduccion;
    //        _repositorioItemProduccion = repositorioItemProduccion;
    //        _repositorioDetalleCompra = repositorioDetalleCompra;
    //    }

    //    public Tuple<IEnumerable<Produccion>, IEnumerable<Verificador>> ObtenerCuadreDiario(DateTime fecha)
    //    {
    //        var produccion = _repositorioProduccion.ObtenerProduccionesConDetallesYRecetas(new FiltroProduccionPorFecha(fecha)).FirstOrDefault();
    //        var itemsProduccion = _repositorioItemProduccion.ObtenerItemsProduccionConDetalles(new FiltroItemProduccionPorFecha(fecha));
    //        if (produccion == null || itemsProduccion.Count() == 0)
    //            throw new ApplicationException("No existen datos para la consulta.");
    //        var listaProduccion = new List<Produccion>();
    //        var listaVerificador = new List<Verificador>();
    //        foreach (var item in produccion.DETALLES_PRODUCCION)
    //        {
    //            var costo = new decimal(0);
    //            var detallesProduccion = new List<DetalleProduccion>();
    //            foreach (var dato in item.RECETA.DETALLES_RECETA)
    //            {
    //                detallesProduccion.Add(new DetalleProduccion
    //                {
    //                    IdItem = dato.ID_ITEM,
    //                    NombreItem = dato.ITEM.NOMBRE,
    //                    CantidadQueSeDebioProducirEnGramos = dato.CANTIDAD_GRAMOS * item.CANTIDAD
    //                });
    //                costo = costo + Convert.ToDecimal(((dato.CANTIDAD_GRAMOS * item.CANTIDAD / 1000) * Convert.ToDouble(dato.ITEM.COSTO_POR_KILOGRAMO)));
    //            }
    //            listaProduccion.Add(new Produccion
    //            {
    //                NombreProducto = item.RECETA.NOMBRE,
    //                Cantidad = item.CANTIDAD,
    //                CostoProduccionTotal = costo,
    //                DetallesProduccion = detallesProduccion
    //            });
    //        }
    //        foreach (var itemProduccion in itemsProduccion)
    //        {
    //            foreach (var item in itemProduccion.DETALLES_ITEM_PRODUCCION)
    //            {
    //                if (listaVerificador.Any(lv => lv.NombreItem == item.ITEM.NOMBRE))
    //                {
    //                    var lv = listaVerificador.FirstOrDefault(l => l.NombreItem == item.ITEM.NOMBRE);
    //                    lv.CantidadIngresada = lv.CantidadIngresada + (item.CANTIDAD * item.ITEM.CANTIDAD);
    //                    lv.CantidadProducida = lv.CantidadProducida + (listaProduccion.Sum(lp => lp.DetallesProduccion.FirstOrDefault(dp => dp.IdItem == item.ID_ITEM) == null ? 0 : lp.DetallesProduccion.FirstOrDefault(dp => dp.IdItem == item.ID_ITEM).CantidadQueSeDebioProducirEnGramos) / item.ITEM.MEDIDA.VALOR);
    //                }
    //                else
    //                {
    //                    listaVerificador.Add(new Verificador
    //                    {
    //                        NombreItem = item.ITEM.NOMBRE,
    //                        Medida = item.ITEM.MEDIDA.ETIQUETA,
    //                        CantidadIngresada = item.CANTIDAD * item.ITEM.CANTIDAD,
    //                        CantidadProducida = listaProduccion.Sum(lp => lp.DetallesProduccion.FirstOrDefault(dp => dp.IdItem == item.ID_ITEM) == null ? 0 : lp.DetallesProduccion.FirstOrDefault(dp => dp.IdItem == item.ID_ITEM).CantidadQueSeDebioProducirEnGramos) / item.ITEM.MEDIDA.VALOR
    //                    });
    //                }
    //            }
    //        }

    //        return new Tuple<IEnumerable<Produccion>, IEnumerable<Verificador>>(listaProduccion,listaVerificador);
    //    }

    //    public void LiberarRecursos()
    //    {
    //        _repositorioProduccion.LiberarRecursos();
    //        _repositorioItemProduccion.LiberarRecursos();
    //        _repositorioDetalleCompra.LiberarRecursos();
    //    }
    //}

    //public class FiltroDetalleCompraPorIdItem : IFiltros<DETALLE_COMPRA>
    //{
    //    private readonly int _idItem;

    //    public FiltroDetalleCompraPorIdItem(int idItem)
    //    {
    //        _idItem = idItem;
    //    }

    //    public Expression<Func<DETALLE_COMPRA, bool>> SastifechoPor()
    //    {
    //        var filtro = new FiltroDirecto<DETALLE_COMPRA>(dc => dc.ID_ITEM == _idItem);
    //        return filtro.SastifechoPor();
    //    }
    //}

    //public class FiltroProduccionPorFecha : IFiltros<PRODUCCION>
    //{
    //    private readonly DateTime _fecha;

    //    public FiltroProduccionPorFecha(DateTime fecha)
    //    {
    //        _fecha = fecha;
    //    }

    //    public Expression<Func<PRODUCCION, bool>> SastifechoPor()
    //    {
    //        var filtro = new FiltroDirecto<PRODUCCION>(p => p.FECHA.Year == _fecha.Year && p.FECHA.Month == _fecha.Month && p.FECHA.Day == _fecha.Day);
    //        return filtro.SastifechoPor();
    //    }
    //}

    //public class FiltroItemProduccionPorFecha : IFiltros<ITEM_PRODUCCION>
    //{
    //    private readonly DateTime _fecha;

    //    public FiltroItemProduccionPorFecha(DateTime fecha)
    //    {
    //        _fecha = fecha;
    //    }

    //    public Expression<Func<ITEM_PRODUCCION, bool>> SastifechoPor()
    //    {
    //        var filtro = new FiltroDirecto<ITEM_PRODUCCION>(ip => ip.FECHA.Year == _fecha.Year && ip.FECHA.Month == _fecha.Month && ip.FECHA.Day == _fecha.Day);
    //        return filtro.SastifechoPor();
    //    }
    //}
}
