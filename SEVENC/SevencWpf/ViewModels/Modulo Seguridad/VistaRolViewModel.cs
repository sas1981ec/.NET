using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views.Modulo_Seguridad;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaRolViewModel : ViewModelEspecializado
    {
        #region Campos
        private Rol _rolSeleccionado;
        private ObservableCollection<Rol> _roles;
        private ObservableCollection<Operacion> _operaciones;
        private Visibility _esVisibleCrearRol;
        private Visibility _esVisibleActualizarRol;
        private Visibility _esVisibleAsignarOperaciones;
        #endregion

        #region Constructor
        public VistaRolViewModel()
        {
            Autorizar();
            CargarRoles();
        }
        #endregion

        #region Propiedades
        public ICommand NuevoRol
        {
            get
            {
                return new RelayCommand(CrearRol, PuedoCrearRol);
            }
        }

        public ICommand EditarRol
        {
            get
            {
                return new RelayCommand(ActualizarRol, PuedoActualizarRol);
            }
        }

        public ICommand EliminarRol
        {
            get
            {
                return new RelayCommand(BorrarRol, PuedoBorrarRol);
            }
        }

        public ICommand AsignarOperaciones
        {
            get
            {
                return new RelayCommand(AsignarOperacionesARol, PuedoAsignarOperaciones);
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

        public ObservableCollection<Operacion> Operaciones
        {
            get { return _operaciones; }
            set
            {
                if (_operaciones == value) return;
                _operaciones = value;
                OnPropertyChanged("Operaciones");
            }
        }

        public Rol RolSeleccionado
        {
            get { return _rolSeleccionado; }
            set
            {
                if (_rolSeleccionado == value) return;
                _rolSeleccionado = value;
                if (_rolSeleccionado == null) return;
                OnPropertyChanged("RolSeleccionado");
                CargarOperaciones();
            }
        }

        public Visibility EsVisibleCrearRol
        {
            get { return _esVisibleCrearRol; }
            set
            {
                if (_esVisibleCrearRol == value) return;
                _esVisibleCrearRol = value;
                OnPropertyChanged("EsVisibleCrearRol");
            }
        }

        public Visibility EsVisibleActualizarRol
        {
            get { return _esVisibleActualizarRol; }
            set
            {
                if (_esVisibleActualizarRol == value) return;
                _esVisibleActualizarRol = value;
                OnPropertyChanged("EsVisibleActualizarRol");
            }
        }

        public Visibility EsVisibleAsignarOperaciones
        {
            get { return _esVisibleAsignarOperaciones; }
            set
            {
                if (_esVisibleAsignarOperaciones == value) return;
                _esVisibleAsignarOperaciones = value;
                OnPropertyChanged("EsVisibleAsignarOperaciones");
            }
        }
        #endregion

        #region Metodos
        private void CargarOperaciones()
        {
            Operaciones = new ObservableCollection<Operacion>(Servicio.ObtenerOperacionesPorIdRol(RolSeleccionado.IdRol));
        }

        private void CargarRoles()
        {
            Roles = new ObservableCollection<Rol>(Servicio.ObtenerRoles());
            RolSeleccionado = Roles.FirstOrDefault();
            GestionAuditoria.IdOperacion = 18;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarConsulta("Rol"));
        }

        private bool PuedoCrearRol()
        {
            return true;
        }

        private bool PuedoActualizarRol()
        {
            return RolSeleccionado != null;
        }

        private bool PuedoBorrarRol()
        {
            return RolSeleccionado != null;
        }

        private bool PuedoAsignarOperaciones()
        {
            return RolSeleccionado != null;
        }

        private void CrearRol()
        {
            var rol = new Rol();
            var edicionRol = new VistaEdicionRol
            {
                DataContext = new VistaEdicionRolViewModel(Servicio, true, rol)
            };
            var resultado = edicionRol.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 15;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarCreacion(rol, "Rol", rol.IdRol.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarRoles();
        }

        private void ActualizarRol()
        {
            var rolViejo = RolSeleccionado.Clone();
            var edicionRol = new VistaEdicionRol { DataContext = new VistaEdicionRolViewModel(Servicio, false, RolSeleccionado) };
            var resultado = edicionRol.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 16;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarActualizacion(RolSeleccionado, rolViejo, "Rol", RolSeleccionado.IdRol.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void BorrarRol()
        {
            var mbr = MessageBox.Show($"Esta seguro de eliminar el rol - {RolSeleccionado.Nombre}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var rolViejo = RolSeleccionado.Clone();
            RolSeleccionado.EstaEliminado = true;
            Servicio.ActualizarRol(RolSeleccionado);
            GestionAuditoria.IdOperacion = 16;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(RolSeleccionado, rolViejo, "Rol", RolSeleccionado.IdRol.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CierreEdicion(true);
        }

        private void AsignarOperacionesARol()
        {
            var asignacion = new VistaAsignacionOperaciones
            {
                DataContext = new VistaAsignacionOperacionesViewModel(Servicio, RolSeleccionado)
            };
            CierreEdicion(asignacion.ShowDialog());
        }

        internal override void Autorizar()
        {
            EsVisibleCrearRol = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(15) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleActualizarRol = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(16) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleAsignarOperaciones = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(17) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
