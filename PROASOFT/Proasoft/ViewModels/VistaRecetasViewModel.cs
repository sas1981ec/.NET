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
    internal class VistaRecetasViewModel : ViewModelBase
    {
        #region Campos
        private RECETA _recetaSeleccionada;
        private ObservableCollection<RECETA> _recetas;
        private ObservableCollection<DETALLE_RECETA> _detallesReceta;
        private Visibility _esVisibleNuevaReceta;
        private Visibility _esVisibleEditarReceta;
        #endregion

        #region Constructor
        public VistaRecetasViewModel()
        {
            Autorizar();
            CargarRecetas();
        }
        #endregion

        #region Propiedades
        public ICommand NuevaReceta
        {
            get
            {
                return new RelayCommand(CrearReceta, PuedoCrearReceta);
            }
        }

        public ICommand EditarReceta
        {
            get
            {
                return new RelayCommand(ActualizarReceta, PuedoActualizarReceta);
            }
        }

        public ObservableCollection<RECETA> Recetas
        {
            get { return _recetas; }
            set
            {
                if (_recetas == value) return;
                _recetas = value;
                OnPropertyChanged("Recetas");
            }
        }

        public ObservableCollection<DETALLE_RECETA> DetallesReceta
        {
            get { return _detallesReceta; }
            set
            {
                if (_detallesReceta == value) return;
                _detallesReceta = value;
                OnPropertyChanged("DetallesReceta");
            }
        }

        public RECETA RecetaSeleccionada
        {
            get { return _recetaSeleccionada; }
            set
            {
                if (_recetaSeleccionada == value) return;
                _recetaSeleccionada = value;
                if (_recetaSeleccionada == null) return;
                OnPropertyChanged("RecetaSeleccionada");
                CargarDetalles();
            }
        }

        public Visibility EsVisibleNuevaReceta
        {
            get { return _esVisibleNuevaReceta; }
            set
            {
                if (_esVisibleNuevaReceta == value) return;
                _esVisibleNuevaReceta = value;
                OnPropertyChanged("EsVisibleNuevaReceta");
            }
        }

        public Visibility EsVisibleEditarReceta
        {
            get { return _esVisibleEditarReceta; }
            set
            {
                if (_esVisibleEditarReceta == value) return;
                _esVisibleEditarReceta = value;
                OnPropertyChanged("EsVisibleEditarReceta");
            }
        }
        #endregion

        #region Metodos
        private void CargarRecetas()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringReceta.xml");
            var administradorReceta = (IReceta)ctx["AdministradorReceta"];
            Recetas = new ObservableCollection<RECETA>(administradorReceta.ObtenerRecetas().OrderBy(i => i.NOMBRE));
            administradorReceta.LiberarRecursos();
        }

        private void CargarDetalles()
        {
            DetallesReceta = new ObservableCollection<DETALLE_RECETA>(RecetaSeleccionada.DETALLES_RECETA);
        }

        private bool PuedoCrearReceta()
        {
            return true;
        }

        private bool PuedoActualizarReceta()
        {
            return RecetaSeleccionada != null;
        }

        private void CrearReceta()
        {
            var edicionReceta = new EdicionReceta
            {
                DataContext = new EdicionRecetaViewModel(true, new RECETA { ESTA_ACTIVA = true})
            };
            var resultado = edicionReceta.ShowDialog();
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarRecetas();
        }

        private void ActualizarReceta()
        {
            var edicionReceta = new EdicionReceta { DataContext = new EdicionRecetaViewModel(false, RecetaSeleccionada) };
            var resultado = edicionReceta.ShowDialog();
            CierreEdicion(resultado);
        }

        private void Autorizar()
        {
            EsVisibleNuevaReceta = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleEditarReceta = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
