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
    internal class VistaItemsViewModel : ViewModelBase
    {
        #region Campos
        private ITEM _itemSeleccionado;
        private ObservableCollection<ITEM> _items;
        private Visibility _esVisibleNuevoItem;
        private Visibility _esVisibleEditarItem;
        #endregion

        #region Constructor
        public VistaItemsViewModel()
        {           
            Autorizar();
            CargarItems();
        }
        #endregion

        #region Propiedades
        public ICommand NuevoItem
        {
            get
            {
                return new RelayCommand(CrearItem, PuedoCrearItem);
            }
        }

        public ICommand EditarItem
        {
            get
            {
                return new RelayCommand(ActualizarItem, PuedoActualizarItem);
            }
        }

        public ObservableCollection<ITEM> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ITEM ItemSeleccionado
        {
            get { return _itemSeleccionado; }
            set
            {
                if (_itemSeleccionado == value) return;
                _itemSeleccionado = value;
                OnPropertyChanged("ItemSeleccionado");               
            }
        }

        public Visibility EsVisibleNuevoItem
        {
            get { return _esVisibleNuevoItem; }
            set
            {
                if (_esVisibleNuevoItem == value) return;
                _esVisibleNuevoItem = value;
                OnPropertyChanged("EsVisibleNuevoItem");
            }
        }

        public Visibility EsVisibleEditarItem
        {
            get { return _esVisibleEditarItem; }
            set
            {
                if (_esVisibleEditarItem == value) return;
                _esVisibleEditarItem = value;
                OnPropertyChanged("EsVisibleEditarItem");
            }
        }
        #endregion

        #region Metodos
        private void CargarItems()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringItem.xml");
            var administradorItem = (IItem)ctx["AdministradorItem"];
            Items = new ObservableCollection<ITEM>(administradorItem.ObtenerItems().OrderBy(i => i.NOMBRE));
            administradorItem.LiberarRecursos();
        }

        private bool PuedoCrearItem()
        {
            return true;
        }

        private bool PuedoActualizarItem()
        {
            return ItemSeleccionado != null;
        }

        private void CrearItem()
        {
            var edicionItem = new EdicionItem
            {
                DataContext = new EdicionItemViewModel(true, new ITEM { ESTA_ACTIVO = true})
            };
            var resultado = edicionItem.ShowDialog();
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarItems();
        }

        private void ActualizarItem()
        {
            var edicionItem = new EdicionItem { DataContext = new EdicionItemViewModel(false, ItemSeleccionado) };
            var resultado = edicionItem.ShowDialog();
            CierreEdicion(resultado);
        }

        private void Autorizar()
        {
            EsVisibleNuevoItem = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleEditarItem = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
