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
    internal class EdicionDetalleProduccionViewModel : ViewModelBase
    {
        #region Campos
        private DETALLE_PRODUCCION _detalleProduccion;
        private ObservableCollection<RECETA> _productos;
        private EMPLEADO _empleadoSeleccionado;
        private ObservableCollection<EMPLEADO> _empleados;
        private RECETA _productoSeleccionado;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public EdicionDetalleProduccionViewModel(bool esNuevo, DETALLE_PRODUCCION detalleProduccion)
        {
            EsNuevo = esNuevo;
            _detalleProduccion = detalleProduccion;
            CargarEmpleados();
            CargarProductos();
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
                return EsNuevo ? "Nueva Producción" : $"Editar Producción {_detalleProduccion.RECETA.NOMBRE}";
            }
        }

        public DETALLE_PRODUCCION DetalleProduccion
        {
            get
            {
                return _detalleProduccion;
            }
            set
            {
                if (_detalleProduccion == value) return;
                _detalleProduccion = value;
                OnPropertyChanged("DetalleProduccion");
            }
        }

        public ObservableCollection<RECETA> Productos
        {
            get
            {
                return _productos;
            }
            set
            {
                if (_productos == value) return;
                _productos = value;
                OnPropertyChanged("Productos");
            }
        }

        public EMPLEADO EmpleadoSeleccionado
        {
            get
            {
                return _empleadoSeleccionado;
            }
            set
            {
                if (_empleadoSeleccionado == value) return;
                _empleadoSeleccionado = value;
                OnPropertyChanged("EmpleadoSeleccionado");
            }
        }

        public ObservableCollection<EMPLEADO> Empleados
        {
            get
            {
                return _empleados;
            }
            set
            {
                if (_empleados == value) return;
                _empleados = value;
                OnPropertyChanged("Empleados");
            }
        }

        public RECETA ProductoSeleccionado
        {
            get
            {
                return _productoSeleccionado;
            }
            set
            {
                if (_productoSeleccionado == value) return;
                _productoSeleccionado = value;
                OnPropertyChanged("ProductoSeleccionado");
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
        private void CargarEmpleados()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
            var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
            Empleados = new ObservableCollection<EMPLEADO>(administradorProduccion.ObtenerEmpleados());
            administradorProduccion.LiberarRecursos();
            if (EsNuevo)
                EmpleadoSeleccionado = Empleados.FirstOrDefault();
            else
                EmpleadoSeleccionado = Empleados.FirstOrDefault(e => e.ID_EMPLEADO == _detalleProduccion.ID_EMPLEADO);
        }

        private void CargarProductos()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
            var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
            Productos = new ObservableCollection<RECETA>(administradorProduccion.ObtenerRecetas());
            administradorProduccion.LiberarRecursos();
            if (EsNuevo)
                ProductoSeleccionado = Productos.FirstOrDefault();
            else
                ProductoSeleccionado = Productos.FirstOrDefault(p => p.ID_RECETA == _detalleProduccion.ID_RECETA);
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
            var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
            var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
            if (EsNuevo)
            {
                DetalleProduccion.ID_RECETA = ProductoSeleccionado.ID_RECETA;
                DetalleProduccion.RECETA = ProductoSeleccionado;
                DetalleProduccion.ID_EMPLEADO = EmpleadoSeleccionado.ID_EMPLEADO;
                DetalleProduccion.EMPLEADO = EmpleadoSeleccionado;
                DialogResult = true;
            }
            else
            {
                //administradorProduccion.ActualizarProduccion(DetalleProduccion);
                MessageBox.Show("Proceso Ok.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            administradorProduccion.LiberarRecursos();
        }
        #endregion
    }
}
