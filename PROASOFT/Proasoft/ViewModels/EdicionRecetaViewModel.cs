using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class EdicionRecetaViewModel : ViewModelBase
    {
        #region Campos
        private RECETA _receta;
        private ObservableCollection<DETALLE_RECETA> _detalles;
        private DETALLE_RECETA _detalleSeleccionado;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionRecetaViewModel(bool esNuevo, RECETA receta)
        {
            EsNuevo = esNuevo;
            _receta = receta;           
            Detalles = esNuevo ? new ObservableCollection<DETALLE_RECETA>() : new ObservableCollection<DETALLE_RECETA>(receta.DETALLES_RECETA);
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
                return EsNuevo ? "Nueva Receta" : $"Editar Receta {_receta.NOMBRE}";
            }
        }

        public RECETA Receta
        {
            get
            {
                return _receta;
            }
            set
            {
                if (_receta == value) return;
                _receta = value;
                OnPropertyChanged("RECETA");
            }
        }


        public ObservableCollection<DETALLE_RECETA> Detalles
        {
            get
            {
                return _detalles;
            }
            set
            {
                if (_detalles == value) return;
                _detalles = value;
                OnPropertyChanged("Detalles");
            }
        }


        public DETALLE_RECETA DetalleSeleccionado
        {
            get
            {
                return _detalleSeleccionado;
            }
            set
            {
                if (_detalleSeleccionado == value) return;
                _detalleSeleccionado = value;
                OnPropertyChanged("DetalleSeleccionado");
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

        public bool EsNuevo { get; }
        #endregion

        #region Metodos
        private bool PuedoAgregarDetalle()
        {
            return true;
        }

        private bool PuedoQuitarDetalle()
        {
            return DetalleSeleccionado != null;
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return !string.IsNullOrWhiteSpace(Receta.NOMBRE) && Detalles.Count() > 0;
        }

        private void AgregarDetalle()
        {
            var ventanaDetalle = new EdicionDetalleReceta
            {
                DataContext = new EdicionDetalleRecetaViewModel()
            };
            var resultado = ventanaDetalle.ShowDialog();
            if (resultado.HasValue && resultado.Value && !DetalleRepetido(((EdicionDetalleRecetaViewModel)ventanaDetalle.DataContext).DetalleReceta.ID_ITEM))
                Detalles.Add(((EdicionDetalleRecetaViewModel)ventanaDetalle.DataContext).DetalleReceta);
        }

        private bool DetalleRepetido(int id)
        {
            if (Detalles.Any(dc => dc.ID_ITEM == id))
            {
                MessageBox.Show("Detalle repetido.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return true;
            }
            return false;
        }

        private void QuitarDetalle()
        {
            Detalles.Remove(DetalleSeleccionado);
        }

        private void Grabar()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringReceta.xml");
            var administradorReceta = (IReceta)ctx["AdministradorReceta"];           
            _receta.DETALLES_RECETA = Detalles;
            foreach (var item in Detalles)
                item.ITEM = null;
            if (EsNuevo)
                administradorReceta.CrearReceta(_receta);
            else
                administradorReceta.ActualizarReceta(_receta);
            administradorReceta.LiberarRecursos();
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
