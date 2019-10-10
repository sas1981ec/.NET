using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaItemsProduccionViewModel : ViewModelBase
    {
        //#region Campos
        //private ITEM_PRODUCCION _itemProduccionSeleccionado;
        //private DETALLE_ITEM_PRODUCCION _detalleItemProduccionSeleccionado;
        //private ObservableCollection<ITEM_PRODUCCION> _itemsProduccion;
        //private ObservableCollection<DETALLE_ITEM_PRODUCCION> _detallesItemProduccion;
        //private string _fechaDesde;
        //private string _fechaHasta;
        //private bool _existeProduccion;
        //private Visibility _esVisibleNuevoItemProduccion;
        //private Visibility _esVisibleEditarDetalleItemProduccion;
        //#endregion

        //#region Constructor
        //public VistaItemsProduccionViewModel()
        //{
        //    SetearFechas();
        //    Autorizar();
        //    CargarItemsProduccion();
        //}
        //#endregion

        //#region Propiedades
        //public ICommand BuscarItemsProduccion
        //{
        //    get
        //    {
        //        return new RelayCommand(CargarItemsProduccion, PuedoBuscarItemsProduccion);
        //    }
        //}

        //public ICommand NuevoItemProduccion
        //{
        //    get
        //    {
        //        return new RelayCommand(CrearItemProduccion, PuedoCrearItemProduccion);
        //    }
        //}

        //public ICommand EditarItemProduccion
        //{
        //    get
        //    {
        //        return new RelayCommand(ActualizarDetalleItemProduccion, PuedoActualizarDetalleItemProduccion);
        //    }
        //}

        //public ObservableCollection<ITEM_PRODUCCION> ItemsProduccion
        //{
        //    get { return _itemsProduccion; }
        //    set
        //    {
        //        if (_itemsProduccion == value) return;
        //        _itemsProduccion = value;
        //        OnPropertyChanged("ItemsProduccion");
        //    }
        //}

        //public ITEM_PRODUCCION ItemProduccionSeleccionado
        //{
        //    get { return _itemProduccionSeleccionado; }
        //    set
        //    {
        //        if (_itemProduccionSeleccionado == value) return;
        //        _itemProduccionSeleccionado = value;
        //        if (_itemProduccionSeleccionado == null) return;
        //        OnPropertyChanged("ItemProduccionSeleccionado");
        //        ConsultarProduccion(ItemProduccionSeleccionado.FECHA);
        //        CargarDetallesItemProduccion();
        //    }
        //}

        //public ObservableCollection<DETALLE_ITEM_PRODUCCION> DetallesItemProduccion
        //{
        //    get { return _detallesItemProduccion; }
        //    set
        //    {
        //        if (_detallesItemProduccion == value) return;
        //        _detallesItemProduccion = value;
        //        OnPropertyChanged("DetallesItemProduccion");
        //    }
        //}

        //public DETALLE_ITEM_PRODUCCION DetalleItemProduccionSeleccionado
        //{
        //    get { return _detalleItemProduccionSeleccionado; }
        //    set
        //    {
        //        if (_detalleItemProduccionSeleccionado == value) return;
        //        _detalleItemProduccionSeleccionado = value;
        //        OnPropertyChanged("DetalleItemProduccionSeleccionado");
        //    }
        //}

        //public string FechaDesde
        //{
        //    get
        //    {
        //        return _fechaDesde;
        //    }
        //    set
        //    {
        //        if (_fechaDesde == value)
        //            return;
        //        _fechaDesde = value;
        //        OnPropertyChanged("FechaDesde");
        //    }
        //}

        //public string FechaHasta
        //{
        //    get
        //    {
        //        return _fechaHasta;
        //    }
        //    set
        //    {
        //        if (_fechaHasta == value)
        //            return;
        //        _fechaHasta = value;
        //        OnPropertyChanged("FechaHasta");
        //    }
        //}

        //public Visibility EsVisibleNuevoItemProduccion
        //{
        //    get { return _esVisibleNuevoItemProduccion; }
        //    set
        //    {
        //        if (_esVisibleNuevoItemProduccion == value) return;
        //        _esVisibleNuevoItemProduccion = value;
        //        OnPropertyChanged("EsVisibleNuevoItemProduccion");
        //    }
        //}

        //public Visibility EsVisibleEditarDetalleItemProduccion
        //{
        //    get { return _esVisibleEditarDetalleItemProduccion; }
        //    set
        //    {
        //        if (_esVisibleEditarDetalleItemProduccion == value) return;
        //        _esVisibleEditarDetalleItemProduccion = value;
        //        OnPropertyChanged("EsVisibleEditarDetalleItemProduccion");
        //    }
        //}
        //#endregion

        //#region Metodos
        //private void SetearFechas()
        //{
        //    FechaDesde = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
        //    FechaHasta = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        //}

        //private void ConsultarProduccion(DateTime fecha)
        //{
        //    var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
        //    var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
        //    _existeProduccion = administradorProduccion.ObtenerProducciones(DateTime.Parse(fecha.ToShortDateString()), DateTime.Parse(fecha.ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59)).Any();
        //    administradorProduccion.LiberarRecursos();
        //}

        //private void CargarDetallesItemProduccion()
        //{
        //    DetallesItemProduccion = new ObservableCollection<DETALLE_ITEM_PRODUCCION>(ItemProduccionSeleccionado.DETALLES_ITEM_PRODUCCION);
        //}

        //private bool PuedoBuscarItemsProduccion()
        //{
        //    return true;
        //}

        //private void CargarItemsProduccion()
        //{
        //    ItemsProduccion = null;
        //    DetallesItemProduccion = null;
        //    if (!PuedoCargarItemsProduccion())
        //    {
        //        MessageBox.Show("No se puede consultar ingreso de producción cuyo rango de fechas sea mayor a 3 meses.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        return;
        //    }
        //    var ctx = new XmlApplicationContext("~/Springs/SpringItemProduccion.xml");
        //    var administradorItemProduccion = (IItemProduccion)ctx["AdministradorItemProduccion"];
        //    ItemsProduccion = new ObservableCollection<ITEM_PRODUCCION>(administradorItemProduccion.ObtenerItemsProduccion(ObtenerFecha(FechaDesde), ObtenerFecha(FechaHasta)));
        //    administradorItemProduccion.LiberarRecursos();
        //}

        //private bool PuedoCargarItemsProduccion()
        //{
        //    return (ObtenerFecha(FechaHasta) - ObtenerFecha(FechaDesde)).TotalDays <= 90;
        //}

        //private bool PuedoCrearItemProduccion()
        //{
        //    return true;
        //}

        //private bool PuedoActualizarDetalleItemProduccion()
        //{
        //    return !_existeProduccion && DetalleItemProduccionSeleccionado != null;
        //}

        //private void CrearItemProduccion()
        //{
        //    var edicionItemProduccion = new EdicionItemProduccion { DataContext = new EdicionItemProduccionViewModel() };
        //    var resultado = edicionItemProduccion.ShowDialog();
        //    CierreEdicion(resultado);
        //}

        //private void CierreEdicion(bool? resultado)
        //{
        //    if (resultado == null || !resultado.Value) return;
        //    DetallesItemProduccion = null;
        //    CargarItemsProduccion();
        //}

        //private void ActualizarDetalleItemProduccion()
        //{
        //    var edicionDetalleItemProduccion = new EdicionDetalleItemProduccion { DataContext = new EdicionDetalleItemProduccionViewModel(false, DetalleItemProduccionSeleccionado) };
        //    var resultado = edicionDetalleItemProduccion.ShowDialog();
        //    CierreEdicion(resultado);
        //}

        //private void Autorizar()
        //{
        //    EsVisibleNuevoItemProduccion = Visibility.Visible;
        //    EsVisibleEditarDetalleItemProduccion = Visibility.Visible;
        //}
        //#endregion
    }
}
