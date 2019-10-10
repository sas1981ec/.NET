using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaEdicionRolViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly bool _esNuevo;
        private Rol _rol;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public VistaEdicionRolViewModel(ServicioWcfClient servicio, bool esNuevo, Rol rol)
        {
            _servicio = servicio;
            _esNuevo = esNuevo;
            _rol = rol;
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
                return _esNuevo ? "Nueva Rol" : string.Format("Editar Rol - {0}", _rol.Nombre);
            }
        }

        public Rol Rol
        {
            get
            {
                return _rol;
            }
            set
            {
                if (_rol == value) return;
                _rol = value;
                OnPropertyChanged("Rol");
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
        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return !string.IsNullOrWhiteSpace(Rol.Descripcion) && !string.IsNullOrWhiteSpace(Rol.Nombre);
        }

        private void Grabar()
        {
            if (_esNuevo)
                _servicio.CrearRol(_rol);
            else
                _servicio.ActualizarRol(_rol);
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
