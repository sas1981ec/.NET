using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaEdicionSucursalViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly bool _esNuevo;
        private Sucursal _sucursal;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public VistaEdicionSucursalViewModel(ServicioWcfClient servicio, bool esNuevo, Sucursal sucursal)
        {
            _servicio = servicio;
            _esNuevo = esNuevo;
            _sucursal = sucursal;
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
                return _esNuevo ? "Nueva Sucursal" : string.Format("Editar Sucursal - {0}", _sucursal.Nombre);
            }
        }

        public Sucursal Sucursal
        {
            get
            {
                return _sucursal;
            }
            set
            {
                if (_sucursal == value) return;
                _sucursal = value;
                OnPropertyChanged("Sucursal");
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
            return !string.IsNullOrWhiteSpace(Sucursal.Direccion) && !string.IsNullOrWhiteSpace(Sucursal.Nombre);
        }

        private void Grabar()
        {
            if (_esNuevo)
                _servicio.CrearSucursal(_sucursal);
            else
                _servicio.ActualizarSucursal(_sucursal);
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
