using Proasoft.InfraestructuraVM;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;

namespace Proasoft.ViewModels
{
    internal class EdicionDetalleCompraViewModel : ViewModelBase
    {
        #region Campos
        private DETALLE_COMPRA _detalleCompra;
        private ObservableCollection<ITEM> _items;
        private ITEM _itemSeleccionado;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionDetalleCompraViewModel(bool esNuevo, DETALLE_COMPRA detalleCompra)
        {
            EsNuevo = esNuevo;
            _detalleCompra = detalleCompra;
            CargarItems();
            if (!esNuevo)
                ItemSeleccionado = Items.FirstOrDefault(i => i.ID_ITEM == detalleCompra.ID_ITEM);
        }
        #endregion

        #region Propiedades
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
                return EsNuevo ? "Nuevo Detalle Compra" : $"Editar Detalle Compra - {_detalleCompra.ITEM.NOMBRE}";
            }
        }

        public DETALLE_COMPRA DetalleCompra
        {
            get
            {
                return _detalleCompra;
            }
            set
            {
                if (_detalleCompra == value) return;
                _detalleCompra = value;
                OnPropertyChanged("DetalleCompra");
            }
        }

        public ObservableCollection<ITEM> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ITEM ItemSeleccionado
        {
            get
            {
                return _itemSeleccionado;
            }
            set
            {
                if (_itemSeleccionado == value) return;
                _itemSeleccionado = value;
                OnPropertyChanged("ItemSeleccionado");
            }
        }

        public bool EsNuevo { get; }

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
        private void CargarItems()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            Items = new ObservableCollection<ITEM>(administradorCompra.ObtenerItems().OrderBy(i => i.NOMBRE));
            administradorCompra.LiberarRecursos();
            ItemSeleccionado = Items.FirstOrDefault();
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return true;
        }

        private void Grabar()
        {
            if (EsNuevo)
            {
                DetalleCompra.ID_ITEM = ItemSeleccionado.ID_ITEM;
                DetalleCompra.ITEM = ItemSeleccionado;
                DialogResult = true;
                return;
            }
            var ctx = new XmlApplicationContext("~/Springs/SpringCompra.xml");
            var administradorCompra = (ICompra)ctx["AdministradorCompra"];
            _detalleCompra.COMPRA.ID_USUARIO = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario;
            administradorCompra.ModificarDetalleCompra(_detalleCompra, ((LoginData)App.Current.Resources["LoginData"]).Roles);
            administradorCompra.LiberarRecursos();
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
