using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class EdicionItemProduccionViewModel : ViewModelBase
    {
        //#region Campos
        //private ITEM_PRODUCCION _itemProduccion;
        //private ObservableCollection<DETALLE_ITEM_PRODUCCION> _detallesItemProduccion;
        //private DETALLE_ITEM_PRODUCCION _detalleItemProduccionSeleccionado;
        //private bool? _dialogResult;
        //#endregion

        //#region Constructor
        //public EdicionItemProduccionViewModel()
        //{
        //    _itemProduccion = new ITEM_PRODUCCION();
        //    DetallesItemProduccion = new ObservableCollection<DETALLE_ITEM_PRODUCCION>();
        //}
        //#endregion

        //#region Propiedades
        //public ICommand ComandoAgregarDetalle
        //{
        //    get
        //    {
        //        return new RelayCommand(AgregarDetalle, PuedoAgregarDetalle);
        //    }
        //}

        //public ICommand ComandoQuitarDetalle
        //{
        //    get
        //    {
        //        return new RelayCommand(QuitarDetalle, PuedoQuitarDetalle);
        //    }
        //}

        //public ICommand ComandoGrabar
        //{
        //    get
        //    {
        //        return new RelayCommand(Grabar, PuedoGrabar);
        //    }
        //}

        //public ICommand ComandoCancelar
        //{
        //    get
        //    {
        //        return new RelayCommand(Cancelar);
        //    }
        //}

        //public string Titulo
        //{
        //    get
        //    {
        //        return "Nuevo Item Producción";
        //    }
        //}

        //public ITEM_PRODUCCION ItemProduccion
        //{
        //    get
        //    {
        //        return _itemProduccion;
        //    }
        //    set
        //    {
        //        if (_itemProduccion == value) return;
        //        _itemProduccion = value;
        //        OnPropertyChanged("ItemProduccion");
        //    }
        //}


        //public ObservableCollection<DETALLE_ITEM_PRODUCCION> DetallesItemProduccion
        //{
        //    get
        //    {
        //        return _detallesItemProduccion;
        //    }
        //    set
        //    {
        //        if (_detallesItemProduccion == value) return;
        //        _detallesItemProduccion = value;
        //        OnPropertyChanged("DetallesItemProduccion");
        //    }
        //}


        //public DETALLE_ITEM_PRODUCCION DetalleItemProduccionSeleccionado
        //{
        //    get
        //    {
        //        return _detalleItemProduccionSeleccionado;
        //    }
        //    set
        //    {
        //        if (_detalleItemProduccionSeleccionado == value) return;
        //        _detalleItemProduccionSeleccionado = value;
        //        OnPropertyChanged("DetalleItemProduccionSeleccionado");
        //    }
        //}

        //public bool? DialogResult
        //{
        //    get
        //    {
        //        return _dialogResult;
        //    }
        //    set
        //    {
        //        if (_dialogResult == value) return;
        //        _dialogResult = value;
        //        OnPropertyChanged("DialogResult");
        //    }
        //}
        //#endregion

        //#region Metodos
        //private bool PuedoAgregarDetalle()
        //{
        //    return true;
        //}

        //private bool PuedoQuitarDetalle()
        //{
        //    return DetalleItemProduccionSeleccionado != null;
        //}

        //private void Cancelar()
        //{
        //    DialogResult = false;
        //}

        //private bool PuedoGrabar()
        //{
        //    return DetallesItemProduccion.Count() > 0;
        //}

        //private void AgregarDetalle()
        //{
        //    var ventanaDetalle = new EdicionDetalleItemProduccion
        //    {
        //        DataContext = new EdicionDetalleItemProduccionViewModel(true, new DETALLE_ITEM_PRODUCCION())
        //    };
        //    var resultado = ventanaDetalle.ShowDialog();
        //    if (resultado.HasValue && resultado.Value && !DetalleRepetido(((EdicionDetalleItemProduccionViewModel)ventanaDetalle.DataContext).DetalleItemProduccion.ID_ITEM))
        //        DetallesItemProduccion.Add(((EdicionDetalleItemProduccionViewModel)ventanaDetalle.DataContext).DetalleItemProduccion);
        //}

        //private bool DetalleRepetido(int id)
        //{
        //    if (DetallesItemProduccion.Any(dip => dip.ID_ITEM == id))
        //    {
        //        MessageBox.Show("Detalle repetido.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        return true;
        //    }
        //    return false;
        //}

        //private void QuitarDetalle()
        //{
        //    DetallesItemProduccion.Remove(DetalleItemProduccionSeleccionado);
        //}

        //private void Grabar()
        //{
        //    var ctx = new XmlApplicationContext("~/Springs/SpringItemProduccion.xml");
        //    var administradorItemProduccion = (IItemProduccion)ctx["AdministradorItemProduccion"];
        //    _itemProduccion.DETALLES_ITEM_PRODUCCION = DetallesItemProduccion;
        //    _itemProduccion.ID_USUARIO = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario;
        //    foreach (var item in DetallesItemProduccion)
        //        item.ITEM = null;
        //    administradorItemProduccion.CrearItemProduccion(_itemProduccion);
        //    administradorItemProduccion.LiberarRecursos();
        //    MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
        //    DialogResult = true;
        //}
        //#endregion
    }
}
