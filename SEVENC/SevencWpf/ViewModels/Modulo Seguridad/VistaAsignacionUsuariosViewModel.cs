using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaAsignacionUsuariosViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly Empresa _empresa;
        private ObservableCollection<Usuario> _usuarios;
        private bool? _dialogResult;
        private List<object> _idsViejos;
        #endregion

        #region Constructor
        public VistaAsignacionUsuariosViewModel(ServicioWcfClient servicio, Empresa empresa)
        {
            _servicio = servicio;
            _empresa = empresa;
            CargarUsuarios();
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
                return string.Format("Asignar Usuarios a - {0}", _empresa.NombreComercial);
            }
        }

        public ObservableCollection<Usuario> Usuarios
        {
            get
            {
                return _usuarios;
            }
            set
            {
                if (_usuarios == value) return;
                _usuarios = value;
                OnPropertyChanged("Usuarios");
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
        private void CargarUsuarios()
        {
            Usuarios = new ObservableCollection<Usuario>(_servicio.ObtenerUsuarios());
            ChequearUsuariosAsociadasAEmpresa();
            _idsViejos = new List<object>();
            foreach (var item in Usuarios.Where(u => u.EstaChequeado).Select(u => u.IdUsuario))
                _idsViejos.Add(item);
        }

        private void ChequearUsuariosAsociadasAEmpresa()
        {
            var usuarioesDeEmpresa = _servicio.ObtenerUsuariosPorIdEmpresa(_empresa.IdEmpresa);
            foreach (var usuario in Usuarios)
            {
                if (usuarioesDeEmpresa.Any(s => s.IdUsuario == usuario.IdUsuario))
                    usuario.EstaChequeado = true;
            }
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private void Grabar()
        {
            if (!Usuarios.Any(a => a.EstaChequeado))
                return;
            _servicio.AsignarUsuariosAEmpresa(Usuarios.Where(u => u.EstaChequeado).Select(u => u.IdUsuario).ToList(), _empresa.IdEmpresa);
            GestionAuditoria.IdOperacion = 5;
            if (GestionAuditoria.PuedoAuditar())
            {
                var ids = new List<object>();
                foreach (var item in Usuarios.Where(u => u.EstaChequeado).Select(u => u.IdUsuario))
                    ids.Add(item);
                Auditar(GestionAuditoria.AuditarAsignacion(ids, _idsViejos, "EmpresaUsuario", _empresa.IdEmpresa.ToString()));
            }
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
