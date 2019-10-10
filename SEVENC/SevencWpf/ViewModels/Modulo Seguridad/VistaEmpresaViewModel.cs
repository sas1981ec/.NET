using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views.Modulo_Seguridad;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaEmpresaViewModel : ViewModelEspecializado
    {
        #region Campos
        private Empresa _empresaSeleccionada;
        private Sucursal _sucursalSeleccionada;
        private ObservableCollection<Empresa> _empresas;
        private ObservableCollection<Sucursal> _sucursales;
        private ObservableCollection<Usuario> _usuarios;
        private Visibility _esVisibleCrearEmpresa;
        private Visibility _esVisibleActualizarEmpresa;
        private Visibility _esVisibleAsignarSucursales;
        private Visibility _esVisibleAsignarUsuarios;
        #endregion

        #region Constructor
        public VistaEmpresaViewModel()
        {
            Autorizar();
            CargarEmpresas();
        }
        #endregion

        #region Propiedades
        public ICommand NuevaEmpresa
        {
            get
            {
                return new RelayCommand(CrearEmpresa, PuedoCrearEmpresa);
            }
        }

        public ICommand EditarEmpresa
        {
            get
            {
                return new RelayCommand(ActualizarEmpresa, PuedoActualizarEmpresa);
            }
        }

        public ICommand EliminarEmpresa
        {
            get
            {
                return new RelayCommand(BorrarEmpresa, PuedoBorrarEmpresa);
            }
        }

        public ICommand AsignarSucursales
        {
            get
            {
                return new RelayCommand(AsignarSucursalesAEmpresa, PuedoAsignarSucursales);
            }
        }

        public ICommand AsignarUsuarios
        {
            get
            {
                return new RelayCommand(AsignarUsuariosAEmpresa, PuedoAsignarUsuarios);
            }
        }

        public ObservableCollection<Empresa> Empresas
        {
            get { return _empresas; }
            set
            {
                if (_empresas == value) return;
                _empresas = value;
                OnPropertyChanged("Empresas");
            }
        }

        public ObservableCollection<Sucursal> Sucursales
        {
            get { return _sucursales; }
            set
            {
                if (_sucursales == value) return;
                _sucursales = value;
                OnPropertyChanged("Sucursales");
            }
        }

        public ObservableCollection<Usuario> Usuarios
        {
            get { return _usuarios; }
            set
            {
                if (_usuarios == value) return;
                _usuarios = value;
                OnPropertyChanged("Usuarios");
            }
        }

        public Empresa EmpresaSeleccionada
        {
            get { return _empresaSeleccionada; }
            set
            {
                if (_empresaSeleccionada == value) return;
                _empresaSeleccionada = value;
                if (_empresaSeleccionada == null) return;
                OnPropertyChanged("EmpresaSeleccionada");
                CargarSucursales();
                CargarUsuarios();
            }
        }

        public Sucursal SucursalSeleccionada
        {
            get { return _sucursalSeleccionada; }
            set
            {
                if (_sucursalSeleccionada == value) return;
                _sucursalSeleccionada = value;
                if (_sucursalSeleccionada == null) return;
                OnPropertyChanged("SucursalSeleccionada");
            }
        }

        public Visibility EsVisibleCrearEmpresa
        {
            get { return _esVisibleCrearEmpresa; }
            set
            {
                if (_esVisibleCrearEmpresa == value) return;
                _esVisibleCrearEmpresa = value;
                OnPropertyChanged("EsVisibleCrearEmpresa");
            }
        }

        public Visibility EsVisibleActualizarEmpresa
        {
            get { return _esVisibleActualizarEmpresa; }
            set
            {
                if (_esVisibleActualizarEmpresa == value) return;
                _esVisibleActualizarEmpresa = value;
                OnPropertyChanged("EsVisibleActualizarEmpresa");
            }
        }

        public Visibility EsVisibleAsignarSucursales
        {
            get { return _esVisibleAsignarSucursales; }
            set
            {
                if (_esVisibleAsignarSucursales == value) return;
                _esVisibleAsignarSucursales = value;
                OnPropertyChanged("EsVisibleAsignarSucursales");
            }
        }

        public Visibility EsVisibleAsignarUsuarios
        {
            get { return _esVisibleAsignarUsuarios; }
            set
            {
                if (_esVisibleAsignarUsuarios == value) return;
                _esVisibleAsignarUsuarios = value;
                OnPropertyChanged("EsVisibleAsignarUsuarios");
            }
        }
        #endregion

        #region Metodos
        private void CargarEmpresas()
        {
            Empresas = new ObservableCollection<Empresa>(Servicio.ObtenerEmpresas());
            EmpresaSeleccionada = Empresas.FirstOrDefault();
            GestionAuditoria.IdOperacion = 3;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarConsulta("Empresa"));
        }

        private void CargarSucursales()
        {
            Sucursales = new ObservableCollection<Sucursal>(Servicio.ObtenerSucursalesPorIdEmpresa(EmpresaSeleccionada.IdEmpresa));
            SucursalSeleccionada = Sucursales.FirstOrDefault();
        }

        private void CargarUsuarios()
        {
            Usuarios = new ObservableCollection<Usuario>(Servicio.ObtenerUsuariosPorIdEmpresa(EmpresaSeleccionada.IdEmpresa));
        }

        private bool PuedoCrearEmpresa()
        {
            return true;
        }

        private bool PuedoActualizarEmpresa()
        {
            return EmpresaSeleccionada != null;
        }

        private bool PuedoBorrarEmpresa()
        {
            return EmpresaSeleccionada != null;
        }

        private bool PuedoAsignarSucursales()
        {
            return EmpresaSeleccionada != null;
        }

        private bool PuedoAsignarUsuarios()
        {
            return EmpresaSeleccionada != null;
        }

        private void CrearEmpresa()
        {
            var empresa = new Empresa();
            var edicionEmpresa = new VistaEdicionEmpresa
            {
                DataContext = new VistaEdicionEmpresaViewModel(Servicio, true, empresa)
            };
            var resultado = edicionEmpresa.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 1;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarCreacion(empresa, "Empresa", empresa.IdEmpresa.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            Sucursales = null;
            Usuarios = null;
            CargarEmpresas();
        }

        private void ActualizarEmpresa()
        {
            var empresaVieja = EmpresaSeleccionada.Clone();
            var edicionEmpresa = new VistaEdicionEmpresa { DataContext = new VistaEdicionEmpresaViewModel(Servicio, false, EmpresaSeleccionada) };
            var resultado = edicionEmpresa.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 2;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarActualizacion(EmpresaSeleccionada, empresaVieja, "Empresa", EmpresaSeleccionada.IdEmpresa.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void BorrarEmpresa()
        {
            var mbr = MessageBox.Show($"Esta seguro de eliminar la empresa - {EmpresaSeleccionada.NombreComercial}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var empresaVieja = EmpresaSeleccionada.Clone();
            EmpresaSeleccionada.EstaEliminada = true;
            Servicio.ActualizarEmpresa(EmpresaSeleccionada);
            GestionAuditoria.IdOperacion = 2;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(EmpresaSeleccionada, empresaVieja, "Empresa", EmpresaSeleccionada.IdEmpresa.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CierreEdicion(true);
        }

        private void AsignarSucursalesAEmpresa()
        {
            var asignacion = new VistaAsignacionSurcusales
            {
                DataContext = new VistaAsignacionSucursalesViewModel(Servicio, EmpresaSeleccionada)
            };
            CierreEdicion(asignacion.ShowDialog());
        }

        private void AsignarUsuariosAEmpresa()
        {
            var asignacion = new VistaAsignacionUsuarios
            {
                DataContext = new VistaAsignacionUsuariosViewModel(Servicio, EmpresaSeleccionada)
            };
            CierreEdicion(asignacion.ShowDialog());
        }

        internal override void Autorizar()
        {
            EsVisibleCrearEmpresa = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(1) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleActualizarEmpresa = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(2) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleAsignarSucursales = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(4) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleAsignarUsuarios = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(5) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
