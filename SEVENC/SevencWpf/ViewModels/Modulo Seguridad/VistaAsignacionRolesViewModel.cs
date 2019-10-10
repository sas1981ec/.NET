using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.Generic;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaAsignacionRolesViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly Usuario _usuario;
        private ObservableCollection<Rol> _roles;
        private bool? _dialogResult;
        private List<object> _idsViejos;
        #endregion

        #region Constructor
        public VistaAsignacionRolesViewModel(ServicioWcfClient servicio, Usuario usuario)
        {
            _servicio = servicio;
            _usuario = usuario;
            CargarRoles();
        }
        #endregion

        #region Propiedades
        public ICommand ComandoGrabar
        {
            get
            {
                return new RelayCommand(Grabar);
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
                return string.Format("Asignar Roles a - {0}", _usuario.UserName);
            }
        }

        public ObservableCollection<Rol> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                if (_roles == value) return;
                _roles = value;
                OnPropertyChanged("Roles");
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
        #endregion

        #region Metodos
        private void CargarRoles()
        {
            Roles = new ObservableCollection<Rol>(_servicio.ObtenerRoles());
            ChequearRolesAsociadasAUsuario();
            _idsViejos = new List<object>();
            foreach (var item in Roles.Where(r => r.EstaChequeado).Select(r => r.IdRol))
                _idsViejos.Add(item);
        }

        private void ChequearRolesAsociadasAUsuario()
        {
            var rolesDeUsuario = _servicio.ObtenerRolesPorIdUsuario(_usuario.IdUsuario);
            foreach (var rol in Roles)
            {
                if (rolesDeUsuario.Any(r => r.IdRol == rol.IdRol))
                    rol.EstaChequeado = true;
            }
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private void Grabar()
        {
            if (!Roles.Any(s => s.EstaChequeado))
                return;
            _servicio.AsignarRolesAUsuario(Roles.Where(r => r.EstaChequeado).Select(r => r.IdRol).ToList(), _usuario.IdUsuario);
            GestionAuditoria.IdOperacion = 9;
            if (GestionAuditoria.PuedoAuditar())
            {
                var ids = new List<object>();
                foreach (var item in Roles.Where(r => r.EstaChequeado).Select(r => r.IdRol))
                    ids.Add(item);
                Auditar(GestionAuditoria.AuditarAsignacion(ids, _idsViejos, "UsuarioRol", _usuario.IdUsuario.ToString()));
            }
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
