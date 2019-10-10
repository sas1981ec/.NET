using SevencWpf.InfraestructuraVM;
using SevencWpf.ViewModels.Modulo_Seguridad;
using SevencWpf.Views.Modulo_Seguridad;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using SevencWpf.ServicioWcf;

namespace SevencWpf.ViewModels
{
    internal class VistaPrincipalViewModel : ViewModelEspecializado
    {
        private readonly Frame _contenedor;
        private VistaEmpresa _vistaEmpresa;
        private VistaSucursal _vistaSucursal;
        private VistaUsuario _vistaUsuario;
        private VistaRol _vistaRol;
        private VistaOperacion _vistaOperacion;
        private Visibility _esVisibleUsuarios;
        private Visibility _esVisibleEmpresas;
        private Visibility _esVisibleSucursales;
        private Visibility _esVisibleOperaciones;
        private Visibility _esVisibleRoles;

        public VistaPrincipalViewModel(Frame contenedor)
        {
            _contenedor = contenedor;
            Autorizar();
        }

        public ICommand ComandoUsuarios
        {
            get
            {
                return new RelayCommand(Usuarios);
            }
        }

        public ICommand ComandoEmpresas
        {
            get
            {
                return new RelayCommand(Empresas);
            }
        }

        public ICommand ComandoSucursales
        {
            get
            {
                return new RelayCommand(Sucursales);
            }
        }

        public ICommand ComandoRoles
        {
            get
            {
                return new RelayCommand(Roles);
            }
        }

        public ICommand ComandoOperaciones
        {
            get
            {
                return new RelayCommand(Operaciones);
            }
        }

        public Visibility EsVisibleUsuarios
        {
            get { return _esVisibleUsuarios; }
            set
            {
                if (_esVisibleUsuarios == value) return;
                _esVisibleUsuarios = value;
                OnPropertyChanged("EsVisibleUsuarios");
            }
        }

        public Visibility EsVisibleEmpresas
        {
            get { return _esVisibleEmpresas; }
            set
            {
                if (_esVisibleEmpresas == value) return;
                _esVisibleEmpresas = value;
                OnPropertyChanged("EsVisibleEmpresas");
            }
        }

        public Visibility EsVisibleSucursales
        {
            get { return _esVisibleSucursales; }
            set
            {
                if (_esVisibleSucursales == value) return;
                _esVisibleSucursales = value;
                OnPropertyChanged("EsVisibleSucursales");
            }
        }

        public Visibility EsVisibleRoles
        {
            get { return _esVisibleRoles; }
            set
            {
                if (_esVisibleRoles == value) return;
                _esVisibleRoles = value;
                OnPropertyChanged("EsVisibleRoles");
            }
        }

        public Visibility EsVisibleOperaciones
        {
            get { return _esVisibleOperaciones; }
            set
            {
                if (_esVisibleOperaciones == value) return;
                _esVisibleOperaciones = value;
                OnPropertyChanged("EsVisibleOperaciones");
            }
        }

        private void Empresas()
        {
            CerrarInstanciaServicio();
            _vistaEmpresa = new VistaEmpresa()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaEmpresa);
        }

        private void Sucursales()
        {
            CerrarInstanciaServicio();
            _vistaSucursal = new VistaSucursal()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaSucursal);
        }

        private void Usuarios()
        {
            CerrarInstanciaServicio();
            _vistaUsuario = new VistaUsuario()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaUsuario);
        }

        private void Roles()
        {
            CerrarInstanciaServicio();
            _vistaRol = new VistaRol()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaRol);
        }

        private void Operaciones()
        {
            CerrarInstanciaServicio();
            _vistaOperacion = new VistaOperacion()
            {
                KeepAlive = false
            };
            _contenedor.Navigate(_vistaOperacion);
        }

        private void CerrarInstanciaServicio()
        {
            if (_vistaEmpresa != null)
                ((VistaEmpresaViewModel)_vistaEmpresa.DataContext).Dispose();
            if (_vistaSucursal != null)
                ((VistaSucursalViewModel)_vistaSucursal.DataContext).Dispose();
            if (_vistaUsuario != null)
                ((VistaUsuarioViewModel)_vistaUsuario.DataContext).Dispose();
            if (_vistaRol != null)
                ((VistaRolViewModel)_vistaRol.DataContext).Dispose();
            if (_vistaOperacion != null)
                ((VistaOperacionViewModel)_vistaOperacion.DataContext).Dispose();
        }

        internal override void Autorizar()
        {
            EsVisibleUsuarios = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(11) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleEmpresas = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(3) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleSucursales = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(14) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleRoles = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(18) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleOperaciones = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(20) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
