using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaOrdenProduccionViewModel : ViewModelBase
    {
        #region Campos
        private PRODUCCION _produccionSeleccionada;
        private DETALLE_PRODUCCION _detalleProduccionSeleccionada;
        private ObservableCollection<PRODUCCION> _producciones;
        private ObservableCollection<DETALLE_PRODUCCION> _detallesProduccion;
        private string _fechaDesde;
        private string _fechaHasta;
        private Visibility _esVisibleNuevaProduccion;
        private Visibility _esVisibleEditarDetalleProduccion;
        private Visibility _esVisibleVerPdf;
        #endregion

        #region Constructor
        public VistaOrdenProduccionViewModel()
        {
            SetearFechas();
            Autorizar();
            CargarProducciones();
        }
        #endregion

        #region Propiedades
        public ICommand BuscarProducciones
        {
            get
            {
                return new RelayCommand(CargarProducciones, PuedoBuscarProducciones);
            }
        }

        public ICommand NuevaProduccion
        {
            get
            {
                return new RelayCommand(CrearProduccion, PuedoCrearProduccion);
            }
        }

        public ICommand EditarProduccion
        {
            get
            {
                return new RelayCommand(ActualizarDetalleProduccion, PuedoActualizarDetalleProduccion);
            }
        }

        public ICommand VerPdf
        {
            get
            {
                return new RelayCommand(VerArchivoPdf, PuedoVerPdf);
            }
        }

        public ObservableCollection<PRODUCCION> Producciones
        {
            get { return _producciones; }
            set
            {
                if (_producciones == value) return;
                _producciones = value;
                OnPropertyChanged("Producciones");
            }
        }

        public PRODUCCION ProduccionSeleccionada
        {
            get { return _produccionSeleccionada; }
            set
            {
                if (_produccionSeleccionada == value) return;
                _produccionSeleccionada = value;
                if (_produccionSeleccionada == null) return;
                OnPropertyChanged("ProduccionSeleccionada");
                CargarDetallesProduccion();
            }
        }

        public ObservableCollection<DETALLE_PRODUCCION> DetallesProduccion
        {
            get { return _detallesProduccion; }
            set
            {
                if (_detallesProduccion == value) return;
                _detallesProduccion = value;
                OnPropertyChanged("DetallesProduccion");
            }
        }

        public DETALLE_PRODUCCION DetalleProduccionSeleccionada
        {
            get { return _detalleProduccionSeleccionada; }
            set
            {
                if (_detalleProduccionSeleccionada == value) return;
                _detalleProduccionSeleccionada = value;
                OnPropertyChanged("DetalleProduccionSeleccionada");
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

        public Visibility EsVisibleNuevaProduccion
        {
            get { return _esVisibleNuevaProduccion; }
            set
            {
                if (_esVisibleNuevaProduccion == value) return;
                _esVisibleNuevaProduccion = value;
                OnPropertyChanged("EsVisibleNuevaProduccion");
            }
        }

        public Visibility EsVisibleEditarDetalleProduccion
        {
            get { return _esVisibleEditarDetalleProduccion; }
            set
            {
                if (_esVisibleEditarDetalleProduccion == value) return;
                _esVisibleEditarDetalleProduccion = value;
                OnPropertyChanged("EsVisibleEditarDetalleProduccion");
            }
        }

        public Visibility EsVisibleVerPdf
        {
            get { return _esVisibleVerPdf; }
            set
            {
                if (_esVisibleVerPdf == value) return;
                _esVisibleVerPdf = value;
                OnPropertyChanged("EsVisibleVerPdf");
            }
        }
        #endregion

        #region Metodos
        private void SetearFechas()
        {
            FechaDesde = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            FechaHasta = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        }

        private void CargarDetallesProduccion()
        {
            DetallesProduccion = new ObservableCollection<DETALLE_PRODUCCION>(ProduccionSeleccionada.DETALLES_PRODUCCION);
        }

        private bool PuedoBuscarProducciones()
        {
            return true;
        }

        private void CargarProducciones()
        {
            Producciones = null;
            DetallesProduccion = null;
            if (!PuedoCargarProducciones())
            {
                MessageBox.Show("No se puede consultar ingreso de producción cuyo rango de fechas sea mayor a 3 meses.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
            var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
            Producciones = new ObservableCollection<PRODUCCION>(administradorProduccion.ObtenerOrdenesProducciones(ObtenerFecha(FechaDesde), ObtenerFecha(FechaHasta)));
            administradorProduccion.LiberarRecursos();
        }

        private bool PuedoCargarProducciones()
        {
            return (ObtenerFecha(FechaHasta) - ObtenerFecha(FechaDesde)).TotalDays <= 90;
        }

        private bool PuedoCrearProduccion()
        {
            return true;
        }

        private bool PuedoActualizarDetalleProduccion()
        {
            return DetalleProduccionSeleccionada != null;
        }

        private bool PuedoVerPdf()
        {
            return ProduccionSeleccionada != null;
        }

        private void CrearProduccion()
        {
            var edicionProduccion = new EdicionProduccion { DataContext = new EdicionOrdenProduccionViewModel() };
            var resultado = edicionProduccion.ShowDialog();
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarProducciones();
        }

        private void ActualizarDetalleProduccion()
        {
            var edicionDetalleProduccion = new EdicionDetalleProduccion { DataContext = new EdicionDetalleProduccionViewModel(false, DetalleProduccionSeleccionada) };
            var resultado = edicionDetalleProduccion.ShowDialog();
            CierreEdicionDetalle(resultado);
        }

        private void CierreEdicionDetalle(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarDetallesProduccion();
        }

        private void VerArchivoPdf()
        {
            Process.Start($@"{AppDomain.CurrentDomain.BaseDirectory}/Archivos/{ProduccionSeleccionada.ID_PRODUCCION}.pdf");
        }

        private void Autorizar()
        {
            EsVisibleNuevaProduccion = Visibility.Visible;
            EsVisibleEditarDetalleProduccion = Visibility.Visible;
            EsVisibleVerPdf = Visibility.Visible;
        }
        #endregion
    }
}
