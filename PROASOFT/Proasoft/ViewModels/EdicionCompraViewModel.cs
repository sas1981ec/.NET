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
    internal class EdicionCompraViewModel : ViewModelBase
    {
        #region Campos
        private COMPRA _compra;
        private ObservableCollection<PROVEEDOR> _proveedores;
        private ObservableCollection<DETALLE_COMPRA> _detallesCompra;
        private PROVEEDOR _proveedorSeleccionado;
        private DETALLE_COMPRA _detalleCompraSeleccionado;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionCompraViewModel()
        {
            _compra = new COMPRA();
            DetallesCompra = new ObservableCollection<DETALLE_COMPRA>();
            ObtenerProveedores();
        }
        #endregion

        #region Propiedades
        public ICommand ComandoAgregarDetalle
        {
            get
            {
                return new RelayCommand(AgregarDetalle, PuedoAgregarDetalle);
            }
        }

        public ICommand ComandoQuitarDetalle
        {
            get
            {
                return new RelayCommand(QuitarDetalle, PuedoQuitarDetalle);
            }
        }

        public ICommand ComandoGrabar
        {
            get
            {
                return new RelayCommand(Grabar, PuedoGrabar);
            }
        }

        public ICommand ComandoCancelar
        {
            get
            {
                return new RelayCommand(Cancelar);
            }
        }

        public string Titulo
        {
            get
            {
                return "Nueva Compra";
            }
        }

        public COMPRA Compra
        {
            get
            {
                return _compra;
            }
            set
            {
                if (_compra == value) return;
                _compra = value;
                OnPropertyChanged("COMPRA");
            }
        }

        public ObservableCollection<PROVEEDOR> Proveedores
        {
            get
            {
                return _proveedores;
            }
            set
            {
                if (_proveedores == value) return;
                _proveedores = value;
                OnPropertyChanged("Proveedores");
            }
        }

        public ObservableCollection<DETALLE_COMPRA> DetallesCompra
        {
            get
            {
                return _detallesCompra;
            }
            set
            {
                if (_detallesCompra == value) return;
                _detallesCompra = value;
                OnPropertyChanged("DetallesCompra");
            }

        }

        public PROVEEDOR ProveedorSeleccionado
        {
            get
            {
                return _proveedorSeleccionado;
            }
            set
            {
                if (_proveedorSeleccionado == value) return;
                _proveedorSeleccionado = value;
                OnPropertyChanged("ProveedorSeleccionado");
            }
        }

        public DETALLE_COMPRA DetalleCompraSeleccionado
        {
            get
            {
                return _detalleCompraSeleccionado;
            }
            set
            {
                if (_detalleCompraSeleccionado == value) return;
                _detalleCompraSeleccionado = value;
                OnPropertyChanged("DetalleCompraSeleccionado");
            }
        }

        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            set
            {
                if (_dialogResult == value) return;
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }
        #endregion

        #region Metodos
        private void ObtenerProveedores()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            Proveedores = new ObservableCollection<PROVEEDOR>(administradorCompra.ObtenerProveedores());
            administradorCompra.LiberarRecursos();
            ProveedorSeleccionado = Proveedores.FirstOrDefault();
        }

        private bool PuedoAgregarDetalle()
        {
            return true;
        }

        private bool PuedoQuitarDetalle()
        {
            return DetalleCompraSeleccionado != null;
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return DetallesCompra.Count() > 0;
        }

        private void AgregarDetalle()
        {
            var ventanaDetalle = new EdicionDetalleCompra
            {
                DataContext = new EdicionDetalleCompraViewModel(true, new DETALLE_COMPRA())
            };
            var resultado = ventanaDetalle.ShowDialog();
            if (resultado.HasValue && resultado.Value && !DetalleRepetido(((EdicionDetalleCompraViewModel)ventanaDetalle.DataContext).DetalleCompra.ID_ITEM))
                DetallesCompra.Add(((EdicionDetalleCompraViewModel)ventanaDetalle.DataContext).DetalleCompra);
        }

        private bool DetalleRepetido(int id)
        {
            if (DetallesCompra.Any(dc => dc.ID_ITEM == id))
            {
                MessageBox.Show("Detalle repetido.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return true;
            }
            return false;
        }

        private void QuitarDetalle()
        {
            DetallesCompra.Remove(DetalleCompraSeleccionado);
        }

        private void Grabar()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            _compra.ID_USUARIO = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario;
            foreach (var item in DetallesCompra)
                item.ITEM = null;
            _compra.DETALLES_COMPRAS = DetallesCompra;
            _compra.ID_PROVEEDOR = ProveedorSeleccionado.ID_PROVEEDOR;
            administradorCompra.RegistrarNuevaCompra(_compra);
            administradorCompra.LiberarRecursos();
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
