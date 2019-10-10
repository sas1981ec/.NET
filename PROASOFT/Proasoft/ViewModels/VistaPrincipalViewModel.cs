using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaPrincipalViewModel : ViewModelBase
    {
        private readonly VistaPrincipal _vistaPrincipal;
        private readonly Frame _contenedor;
        private VistaRegistroCompras _vistaCompras;
        private VistaItems _vistaItems;
        private VistaRecetas _vistaRecetas;
        private VistaProduccion _vistaOrdenProduccion;
        private VistaProduccion _vistaProduccion;
        private VistaStock _vistaStock;
        private VistaReporteGerencial _vistaReporteGerencial;
        private Visibility _esVisibleCompras;
        private Visibility _esVisibleProductos;
        private Visibility _esVisibleItems;
        private Visibility _esVisibleRecetas;
        private Visibility _esVisibleProduccion;
        private Visibility _esVisibleOrdenProduccion;
        private Visibility _esVisibleProducciones;
        private Visibility _esVisibleReporte;
        private Visibility _esVisibleStock;
        private Visibility _esVisibleReporteCuadreDiario;

        public VistaPrincipalViewModel(VistaPrincipal vistaPrincipal, Frame contenedor)
        {
            _vistaPrincipal = vistaPrincipal;
            _contenedor = contenedor;
            Autorizar();
        }

        public ICommand ComandoCompras
        {
            get
            {
                return new RelayCommand(RegistroCompras);
            }
        }

        public ICommand ComandoItems
        {
            get
            {
                return new RelayCommand(Items);
            }
        }

        public ICommand ComandoRecetas
        {
            get
            {
                return new RelayCommand(Recetas);
            }
        }

        public ICommand ComandoOrdenProduccion
        {
            get
            {
                return new RelayCommand(OrdenProduccion);
            }
        }

        public ICommand ComandoProducciones
        {
            get
            {
                return new RelayCommand(Producciones);
            }
        }

        public ICommand ComandoStock
        {
            get
            {
                return new RelayCommand(ReporteStock);
            }
        }

        public ICommand ComandoReporteCuadreDiario
        {
            get
            {
                return new RelayCommand(CuadreDiario);
            }
        }

        public ICommand ComandoCambiarContrasena
        {
            get
            {
                return new RelayCommand(CambiarContrasena);
            }
        }

        public ICommand ComandoCerraSesion
        {
            get
            {
                return new RelayCommand(CerrarSesion);
            }
        }

        public string Bienvenido
        {
            get
            {
                return $"Bienvenido {((LoginData)App.Current.Resources["LoginData"]).NombreUsuario}";
            }
        }

        public Visibility EsVisibleCompras
        {
            get { return _esVisibleCompras; }
            set
            {
                if (_esVisibleCompras == value) return;
                _esVisibleCompras = value;
                OnPropertyChanged("EsVisibleCompras");
            }
        }

        public Visibility EsVisibleProductos
        {
            get { return _esVisibleProductos; }
            set
            {
                if (_esVisibleProductos == value) return;
                _esVisibleProductos = value;
                OnPropertyChanged("EsVisibleProductos");
            }
        }

        public Visibility EsVisibleItems
        {
            get { return _esVisibleItems; }
            set
            {
                if (_esVisibleItems == value) return;
                _esVisibleItems = value;
                OnPropertyChanged("EsVisibleItems");
            }
        }

        public Visibility EsVisibleRecetas
        {
            get { return _esVisibleRecetas; }
            set
            {
                if (_esVisibleRecetas == value) return;
                _esVisibleRecetas = value;
                OnPropertyChanged("EsVisibleRecetas");
            }
        }

        public Visibility EsVisibleProduccion
        {
            get { return _esVisibleProduccion; }
            set
            {
                if (_esVisibleProduccion == value) return;
                _esVisibleProduccion = value;
                OnPropertyChanged("EsVisibleProduccion");
            }
        }

        public Visibility EsVisibleOrdenProduccion
        {
            get { return _esVisibleOrdenProduccion; }
            set
            {
                if (_esVisibleOrdenProduccion == value) return;
                _esVisibleOrdenProduccion = value;
                OnPropertyChanged("EsVisibleOrdenProduccion");
            }
        }

        public Visibility EsVisibleProducciones
        {
            get { return _esVisibleProducciones; }
            set
            {
                if (_esVisibleProducciones == value) return;
                _esVisibleProducciones = value;
                OnPropertyChanged("EsVisibleProducciones");
            }
        }

        public Visibility EsVisibleReporte
        {
            get { return _esVisibleReporte; }
            set
            {
                if (_esVisibleReporte == value) return;
                _esVisibleReporte = value;
                OnPropertyChanged("EsVisibleReporte");
            }
        }

        public Visibility EsVisibleStock
        {
            get { return _esVisibleStock; }
            set
            {
                if (_esVisibleStock == value) return;
                _esVisibleStock = value;
                OnPropertyChanged("EsVisibleStock");
            }
        }

        public Visibility EsVisibleReporteCuadreDiario
        {
            get { return _esVisibleReporteCuadreDiario; }
            set
            {
                if (_esVisibleReporteCuadreDiario == value) return;
                _esVisibleReporteCuadreDiario = value;
                OnPropertyChanged("EsVisibleReporteCuadreDiario");
            }
        }

        private void RegistroCompras()
        {
            _vistaCompras = new VistaRegistroCompras()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaCompras);
        }

        private void Items()
        {
            _vistaItems = new VistaItems()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaItems);
        }

        private void Recetas()
        {
            _vistaRecetas = new VistaRecetas()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaRecetas);
        }

        private void OrdenProduccion()
        {
            _vistaOrdenProduccion = new VistaProduccion()
            {
                KeepAlive = false,
                DataContext = new VistaOrdenProduccionViewModel()
            };
            _contenedor.Navigate(_vistaOrdenProduccion);
        }

        private void Producciones()
        {
            _vistaProduccion = new VistaProduccion()
            {
                KeepAlive = false,
                DataContext = new VistaProduccionViewModel()
            };
            _contenedor.Navigate(_vistaProduccion);
        }

        private void ReporteStock()
        {
            _vistaStock = new VistaStock()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaStock);
        }


        private void CuadreDiario()
        {
            _vistaReporteGerencial = new VistaReporteGerencial()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaReporteGerencial);
        }


        private void Autorizar()
        {
            EsVisibleProductos = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleItems = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleRecetas = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleProduccion =  Visibility.Visible;
            EsVisibleCompras = Visibility.Visible;
            EsVisibleOrdenProduccion = Visibility.Visible;
            EsVisibleProduccion = Visibility.Visible;
            EsVisibleStock = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleReporteCuadreDiario = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleReporte = ((LoginData)App.Current.Resources["LoginData"]).Roles.ToList().Contains(2) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CambiarContrasena()
        {
            var vista = new CambioContrasena
            {
                DataContext = new CambioContrasenaViewModel()
            };
            var resultado = vista.ShowDialog();
        }

        private void CerrarSesion()
        {
            var vistaAutenticacion = new VistaAutenticacion();
            vistaAutenticacion.Show();
            _vistaPrincipal.Close();
            App.Current.Resources.Remove("LoginData");
        }
    }
}
