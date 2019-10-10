using Proasoft.InfraestructuraVM;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class EdicionItemViewModel : ViewModelBase
    {
        #region Campos
        private ITEM _item;
        private ObservableCollection<MEDIDA> _medidas;
        private MEDIDA _medidaSeleccionada;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionItemViewModel(bool esNuevo, ITEM item)
        {
            EsNuevo = esNuevo;
            _item = item;
            ObtenerMedidas();
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
                return EsNuevo ? "Nuevo Insumo" : $"Editar Insumo - {_item.NOMBRE}";
            }
        }

        public ITEM Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (_item == value) return;
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public ObservableCollection<MEDIDA> Medidas
        {
            get
            {
                return _medidas;
            }
            set
            {
                if (_medidas == value) return;
                _medidas = value;
                OnPropertyChanged("Medidas");
            }
        }

        public MEDIDA MedidaSeleccionada
        {
            get
            {
                return _medidaSeleccionada;
            }
            set
            {
                if (_medidaSeleccionada == value) return;
                _medidaSeleccionada = value;
                OnPropertyChanged("MedidaSeleccionada");
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
        private void ObtenerMedidas()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringItem.xml");
            var administradorItem = (IItem)ctx["AdministradorItem"];
            Medidas = new ObservableCollection<MEDIDA>(administradorItem.ObtenerMedidas());
            administradorItem.LiberarRecursos();
            MedidaSeleccionada = EsNuevo ? Medidas.FirstOrDefault() : Medidas.FirstOrDefault(m => m.ID_MEDIDA == _item.ID_MEDIDA);
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return !string.IsNullOrWhiteSpace(Item.NOMBRE);
        }

        private void Grabar()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringItem.xml");
            var administradorItem = (IItem)ctx["AdministradorItem"];
            if (EsNuevo)
            {
                _item.ID_MEDIDA = MedidaSeleccionada.ID_MEDIDA;
                administradorItem.CrearItem(_item);
            }
            else
                administradorItem.ActualizarItem(_item);
            administradorItem.LiberarRecursos();
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
