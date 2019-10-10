using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaRegistroComprasViewModel : ViewModelBase
    {
        #region Campos
        private COMPRA _compraSeleccionada;
        private DETALLE_COMPRA _detalleCompraSeleccionada;
        private ObservableCollection<COMPRA> _compras;
        private ObservableCollection<DETALLE_COMPRA> _detallesCompras;
        private string _fechaDesde;
        private string _fechaHasta;
        private Visibility _esVisibleNuevaCompra;
        private Visibility _esVisibleEditarDetalleCompra;
        private Visibility _esVisibleConfirmarCompra;
        #endregion

        #region Constructor
        public VistaRegistroComprasViewModel()
        {
            SetearFechas();
            Autorizar();
            CargarCompras();
        }
        #endregion

        #region Propiedades
        public ICommand BuscarCompras
        {
            get
            {
                return new RelayCommand(CargarCompras, PuedoBuscarCompras);
            }
        }

        public ICommand NuevaCompra
        {
            get
            {
                return new RelayCommand(CrearCompra, PuedoCrearCompra);
            }
        }

        public ICommand EditarDetalleCompra
        {
            get
            {
                return new RelayCommand(ActualizarDetalleCompra, PuedoActualizarDetalleCompra);
            }
        }

        public ICommand ConfirmarCompra
        {
            get
            {
                return new RelayCommand(Confirmar, PuedoConfirmarCompra);
            }
        }

        public ObservableCollection<COMPRA> Compras
        {
            get { return _compras; }
            set
            {
                if (_compras == value) return;
                _compras = value;
                OnPropertyChanged("Compras");
            }
        }

        public COMPRA CompraSeleccionada
        {
            get { return _compraSeleccionada; }
            set
            {
                if (_compraSeleccionada == value) return;
                _compraSeleccionada = value;
                if (_compraSeleccionada == null) return;
                OnPropertyChanged("CompraSeleccionada");
                CargarDetallesCompra();
            }
        }

        public ObservableCollection<DETALLE_COMPRA> DetallesCompra
        {
            get { return _detallesCompras; }
            set
            {
                if (_detallesCompras == value) return;
                _detallesCompras = value;
                OnPropertyChanged("DetallesCompra");
            }
        }

        public DETALLE_COMPRA DetalleCompraSeleccionada
        {
            get { return _detalleCompraSeleccionada; }
            set
            {
                if (_detalleCompraSeleccionada == value) return;
                _detalleCompraSeleccionada = value;
                OnPropertyChanged("DetalleCompraSeleccionada");
            }
        }

        public string FechaDesde
        {
            get
            {
                return _fechaDesde;
            }
            set
            {
                if (_fechaDesde == value)
                    return;
                _fechaDesde = value;
                OnPropertyChanged("FechaDesde");
            }
        }

        public string FechaHasta
        {
            get
            {
                return _fechaHasta;
            }
            set
            {
                if (_fechaHasta == value)
                    return;
                _fechaHasta = value;
                OnPropertyChanged("FechaHasta");
            }
        }

        public Visibility EsVisibleNuevaCompra
        {
            get { return _esVisibleNuevaCompra; }
            set
            {
                if (_esVisibleNuevaCompra == value) return;
                _esVisibleNuevaCompra = value;
                OnPropertyChanged("EsVisibleNuevaCompra");
            }
        }

        public Visibility EsVisibleEditarDetalleCompra
        {
            get { return _esVisibleEditarDetalleCompra; }
            set
            {
                if (_esVisibleEditarDetalleCompra == value) return;
                _esVisibleEditarDetalleCompra = value;
                OnPropertyChanged("EsVisibleEditarDetalleCompra");
            }
        }

        public Visibility EsVisibleConfirmarCompra
        {
            get { return _esVisibleConfirmarCompra; }
            set
            {
                if (_esVisibleConfirmarCompra == value) return;
                _esVisibleConfirmarCompra = value;
                OnPropertyChanged("EsVisibleConfirmarCompra");
            }
        }
        #endregion

        #region Metodos
        private void SetearFechas()
        {
            FechaDesde = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            FechaHasta = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        }

        private void CargarDetallesCompra()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            DetallesCompra = new ObservableCollection<DETALLE_COMPRA>(administradorCompra.ObtenerDetallesCompra(CompraSeleccionada.ID_COMPRA));
            administradorCompra.LiberarRecursos();
        }

        private bool PuedoBuscarCompras()
        {
            return true;
        }

        private void CargarCompras()
        {
            Compras = null;
            DetallesCompra = null;
            if (!PuedoCargarCompras())
            {
                MessageBox.Show("No se puede consultar compras cuyo rango de fechas sea mayor a 3 meses.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            Compras = new ObservableCollection<COMPRA>(administradorCompra.ObtenerCompras(ObtenerFecha(FechaDesde), ObtenerFecha(FechaHasta)));
            administradorCompra.LiberarRecursos();
        }

        private bool PuedoCargarCompras()
        {
            return (ObtenerFecha(FechaHasta) - ObtenerFecha(FechaDesde)).TotalDays <= 90;
        }

        private bool PuedoCrearCompra()
        {
            return true;
        }

        private bool PuedoConfirmarCompra()
        {
            return CompraSeleccionada != null && !CompraSeleccionada.ESTA_CONFIRMADA;
        }

        private bool PuedoActualizarDetalleCompra()
        {
            return DetalleCompraSeleccionada != null && !CompraSeleccionada.ESTA_CONFIRMADA;
        }

        private void CrearCompra()
        {
            var edicionCompra = new EdicionCompra { DataContext = new EdicionCompraViewModel() };
            var resultado = edicionCompra.ShowDialog();
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            DetallesCompra = null;
            CargarCompras();
        }

        private void ActualizarDetalleCompra()
        {
            DetalleCompraSeleccionada.COMPRA = CompraSeleccionada;
            var edicionDetalleCompra = new EdicionDetalleCompra { DataContext = new EdicionDetalleCompraViewModel(false, DetalleCompraSeleccionada) };
            var resultado = edicionDetalleCompra.ShowDialog();
            CierreEdicion(resultado);
        }

        private void Confirmar()
        {
            var resultado = MessageBox.Show($"¿Está seguro de confirmar la compra?{Environment.NewLine}Al confirmar los insumos entraran en la bodega principal.{Environment.NewLine}Una vez confirmado no se podrá realizar cambios en la compra.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
                var administradorCompra = (ICompra)ctx["AdministradorCompra"];
                administradorCompra.ConfirmarCompra(CompraSeleccionada.ID_COMPRA, ((LoginData)App.Current.Resources["LoginData"]).IdUsuario);
                administradorCompra.LiberarRecursos();
                MessageBox.Show("Proceso Ok.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                DetallesCompra = null;
                CargarCompras();
            }
        }

        private void Autorizar()
        {
            EsVisibleNuevaCompra = Visibility.Visible;
            EsVisibleEditarDetalleCompra = Visibility.Visible;
            EsVisibleConfirmarCompra = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
