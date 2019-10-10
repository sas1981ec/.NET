using Proasoft.InfraestructuraVM;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class EdicionDetalleRecetaViewModel : ViewModelBase
    {
        #region Campos
        private DETALLE_RECETA _detalleReceta;
        private ObservableCollection<ITEM> _items;
        private ITEM _itemSeleccionado;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionDetalleRecetaViewModel()
        {
            CargarItems();
            DetalleReceta = new DETALLE_RECETA();
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
                return "Nuevo Detalle Receta";
            }
        }

        public DETALLE_RECETA DetalleReceta
        {
            get
            {
                return _detalleReceta;
            }
            set
            {
                if (_detalleReceta == value) return;
                _detalleReceta = value;
                OnPropertyChanged("DetalleReceta");
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
            DetalleReceta.ID_ITEM = ItemSeleccionado.ID_ITEM;
            DetalleReceta.ITEM = ItemSeleccionado;
            DialogResult = true;
        }
        #endregion
    }
}
