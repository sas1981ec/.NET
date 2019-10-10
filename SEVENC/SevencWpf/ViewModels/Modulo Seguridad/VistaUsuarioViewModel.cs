using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views.Modulo_Seguridad;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaUsuarioViewModel : ViewModelEspecializado
    {
        #region Campos
        private Usuario _usuarioSeleccionado;
        private ObservableCollection<Usuario> _usuarios;
        private ObservableCollection<Rol> _roles;
        private int _indicePagina;
        private int _idUsuario;
        private string _email;
        private string _userName;
        private string _nombresApellidos;
        private Visibility _esVisibleCrearUsuario;
        private Visibility _esVisibleActualizarUsuario;
        private Visibility _esVisibleGenerarContrasena;
        private Visibility _esVisibleAsignarRoles;
        private Visibility _esVisibleDesbloquearUsuario;
        #endregion

        #region Constructor
        public VistaUsuarioViewModel()
        {
            Autorizar();
        }
        #endregion

        #region Propiedades
        public ICommand BuscarUsuarios
        {
            get
            {
                return new RelayCommand(CargarUsuarios, PuedoBuscarUsuarios);
            }
        }

        public ICommand CargarMasUsuarios
        {
            get
            {
                return new RelayCommand(MasUsuarios, PuedoCargarMasUsuarios);
            }
        }

        public ICommand NuevoUsuario
        {
            get
            {
                return new RelayCommand(CrearUsuario, PuedoCrearUsuario);
            }
        }

        public ICommand EditarUsuario
        {
            get
            {
                return new RelayCommand(ActualizarUsuario, PuedoActualizarUsuario);
            }
        }

        public ICommand EliminarUsuario
        {
            get
            {
                return new RelayCommand(BorrarUsuario, PuedoBorrarUsuario);
            }
        }

        public ICommand GenerarContrasena
        {
            get
            {
                return new RelayCommand(Generar, PuedoGenerarContrasena);
            }
        }

        public ICommand AsignarRoles
        {
            get
            {
                return new RelayCommand(AsignarRolesAUsuario, PuedoAsignarRoles);
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

        public Usuario UsuarioSeleccionado
        {
            get { return _usuarioSeleccionado; }
            set
            {
                if (_usuarioSeleccionado == value) return;
                _usuarioSeleccionado = value;
                if (_usuarioSeleccionado == null) return;
                OnPropertyChanged("UsuarioSeleccionado");
                CargarRoles();
            }
        }

        public ObservableCollection<Rol> Roles
        {
            get { return _roles; }
            set
            {
                if (_roles == value) return;
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        public int IdUsuario
        {
            get { return _idUsuario; }
            set
            {
                if (_idUsuario == value) return;
                _idUsuario = value;
                OnPropertyChanged("IdUsuario");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string NombresApellidos
        {
            get { return _nombresApellidos; }
            set
            {
                if (_nombresApellidos == value) return;
                _nombresApellidos = value;
                OnPropertyChanged("NombresApellidos");
            }
        }

        public Visibility EsVisibleCrearUsuario
        {
            get { return _esVisibleCrearUsuario; }
            set
            {
                if (_esVisibleCrearUsuario == value) return;
                _esVisibleCrearUsuario = value;
                OnPropertyChanged("EsVisibleCrearUsuario");
            }
        }

        public Visibility EsVisibleActualizarUsuario
        {
            get { return _esVisibleActualizarUsuario; }
            set
            {
                if (_esVisibleActualizarUsuario == value) return;
                _esVisibleActualizarUsuario = value;
                OnPropertyChanged("EsVisibleActualizarUsuario");
            }
        }

        public Visibility EsVisibleAsignarRoles
        {
            get { return _esVisibleAsignarRoles; }
            set
            {
                if (_esVisibleAsignarRoles == value) return;
                _esVisibleAsignarRoles = value;
                OnPropertyChanged("EsVisibleAsignarRoles");
            }
        }

        public Visibility EsVisibleGenerarContrasena
        {
            get { return _esVisibleGenerarContrasena; }
            set
            {
                if (_esVisibleGenerarContrasena == value) return;
                _esVisibleGenerarContrasena = value;
                OnPropertyChanged("EsVisibleGenerarContrasena");
            }
        }

        public Visibility EsVisibleDesbloquearUsuario
        {
            get { return _esVisibleDesbloquearUsuario; }
            set
            {
                if (_esVisibleDesbloquearUsuario == value) return;
                _esVisibleDesbloquearUsuario = value;
                OnPropertyChanged("EsVisibleDesbloquearUsuario");
            }
        }
        #endregion

        #region Metodos
        private void CargarRoles()
        {
            Roles = new ObservableCollection<Rol>(Servicio.ObtenerRolesPorIdUsuario(UsuarioSeleccionado.IdUsuario));
        }

        private bool PuedoBuscarUsuarios()
        {
            return true;
        }

        private void CargarUsuarios()
        {
            _indicePagina = 0;
            Usuarios = new ObservableCollection<Usuario>(Servicio.ObtenerUsuariosPorCriteriosBusqueda(ObtenerCriteriosBusqueda(), _indicePagina));
            UsuarioSeleccionado = Usuarios.FirstOrDefault();
            _indicePagina++;
            GestionAuditoria.IdOperacion = 11;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarConsulta("Usuario"));
        }

        private Dictionary<Busqueda, string> ObtenerCriteriosBusqueda()
        {
            var resultado = new Dictionary<Busqueda, string>();
            var manejadorBusqueda = new ManejadorBusquedaPorId();
            manejadorBusqueda.AplicarFiltro(ref resultado, this);
            return resultado;
        }

        private bool PuedoCargarMasUsuarios()
        {
            return _indicePagina > 0;
        }

        private void MasUsuarios()
        {
            var masUsuarios = new ObservableCollection<Usuario>(Servicio.ObtenerUsuariosPorCriteriosBusqueda(ObtenerCriteriosBusqueda(), _indicePagina));
            if (masUsuarios.Count() == 0)
                _indicePagina = 0;
            else
            {
                Usuarios = new ObservableCollection<Usuario>(Usuarios.Concat(masUsuarios));
                _indicePagina++;
                GestionAuditoria.IdOperacion = 11;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarConsulta("Usuario"));
            }
        }

        private bool PuedoCrearUsuario()
        {
            return true;
        }

        private bool PuedoActualizarUsuario()
        {
            return UsuarioSeleccionado != null;
        }

        private bool PuedoBorrarUsuario()
        {
            return UsuarioSeleccionado != null;
        }

        private bool PuedoGenerarContrasena()
        {
            return UsuarioSeleccionado != null;
        }

        private bool PuedoAsignarRoles()
        {
            return UsuarioSeleccionado != null;
        }

        private void CrearUsuario()
        {
            var usuario = new Usuario();
            var edicionUsuario = new VistaEdicionUsuario
            {
                DataContext = new VistaEdicionUsuarioViewModel(Servicio, true, usuario)
            };
            var resultado = edicionUsuario.ShowDialog();
            if (resultado.HasValue && resultado.Value)  
            {
                GestionAuditoria.IdOperacion = 6;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarCreacion(usuario, "Usuario", usuario.IdUsuario.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarUsuarios();
        }

        private void ActualizarUsuario()
        {
            var usuarioViejo = UsuarioSeleccionado.Clone();
            var edicionUsuario = new VistaEdicionUsuario { DataContext = new VistaEdicionUsuarioViewModel(Servicio, false, UsuarioSeleccionado) };
            var resultado = edicionUsuario.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 7;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarActualizacion(UsuarioSeleccionado, usuarioViejo, "Usuario", UsuarioSeleccionado.IdUsuario.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void BorrarUsuario()
        {
            var mbr = MessageBox.Show($"Esta seguro de eliminar el usuario - {UsuarioSeleccionado.NombreCompleto}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var usuarioViejo = UsuarioSeleccionado.Clone();
            UsuarioSeleccionado.EstaEliminado = true;
            Servicio.ActualizarUsuario(UsuarioSeleccionado);
            GestionAuditoria.IdOperacion = 7;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(UsuarioSeleccionado, usuarioViejo, "Usuario", UsuarioSeleccionado.IdUsuario.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CierreEdicion(true);
        }

        private void Generar()
        {
            Servicio.GenerarContrasena(UsuarioSeleccionado.IdUsuario);
            GestionAuditoria.IdOperacion = 8;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarAsignacion(new List<object>(), new List<string> { UsuarioSeleccionado.Contrasena }, "Usuario", UsuarioSeleccionado.IdUsuario.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AsignarRolesAUsuario()
        {
            var asignacion = new VistaAsignacionRoles
            {
                DataContext = new VistaAsignacionRolesViewModel(Servicio, UsuarioSeleccionado)
            };
            CierreEdicion(asignacion.ShowDialog());
        }

        internal override void Autorizar()
        {
            EsVisibleCrearUsuario = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(6) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleActualizarUsuario = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(7) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleGenerarContrasena = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(8) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleAsignarRoles = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(9) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleDesbloquearUsuario = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(10) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }

    internal abstract class ManejadorBusqueda
    {
        protected ManejadorBusqueda Siguiente;

        protected ManejadorBusqueda EstablecerSiguiente(ManejadorBusqueda siguiente)
        {
            Siguiente = siguiente;
            return Siguiente;
        }

        internal abstract void AplicarFiltro(ref Dictionary<Busqueda, string> filtros, VistaUsuarioViewModel viewModel);
    }

    internal class ManejadorBusquedaPorId : ManejadorBusqueda
    {
        internal override void AplicarFiltro(ref Dictionary<Busqueda, string> filtros, VistaUsuarioViewModel viewModel)
        {
            if (viewModel.IdUsuario > 0)
                filtros.Add(Busqueda.PorId, viewModel.IdUsuario.ToString());
            EstablecerSiguiente(new ManejadorBusquedaPorEmail());
            Siguiente.AplicarFiltro(ref filtros, viewModel);
        }
    }

    internal class ManejadorBusquedaPorEmail : ManejadorBusqueda
    {
        internal override void AplicarFiltro(ref Dictionary<Busqueda, string> filtros, VistaUsuarioViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.Email))
                filtros.Add(Busqueda.PorEmail, viewModel.Email);
            EstablecerSiguiente(new ManejadorBusquedaPorUserName());
            Siguiente.AplicarFiltro(ref filtros, viewModel);
        }
    }

    internal class ManejadorBusquedaPorUserName : ManejadorBusqueda
    {
        internal override void AplicarFiltro(ref Dictionary<Busqueda, string> filtros, VistaUsuarioViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.UserName))
                filtros.Add(Busqueda.PorUserName, viewModel.UserName);
            EstablecerSiguiente(new ManejadorBusquedaPorNombresApellidos());
            Siguiente.AplicarFiltro(ref filtros, viewModel);
        }
    }

    internal class ManejadorBusquedaPorNombresApellidos : ManejadorBusqueda
    {
        internal override void AplicarFiltro(ref Dictionary<Busqueda, string> filtros, VistaUsuarioViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.NombresApellidos))
                filtros.Add(Busqueda.PorNombresApellidos, viewModel.NombresApellidos);
        }
    }
}
