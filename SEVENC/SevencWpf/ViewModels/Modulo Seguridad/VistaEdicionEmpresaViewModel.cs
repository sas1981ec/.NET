using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaEdicionEmpresaViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly bool _esNuevo;
        private Empresa _empresa;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public VistaEdicionEmpresaViewModel(ServicioWcfClient servicio, bool esNuevo, Empresa empresa)
        {
            _servicio = servicio;
            _esNuevo = esNuevo;
            _empresa = empresa;
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
                return _esNuevo ? "Nueva Empresa" : string.Format("Editar Empresa - {0}", _empresa.NombreComercial);
            }
        }

        public Empresa Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa == value) return;
                _empresa = value;
                OnPropertyChanged("Empresa");
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
            return !string.IsNullOrWhiteSpace(Empresa.IdRepresentanteLegal) && !string.IsNullOrWhiteSpace(Empresa.NombreComercial)
                && !string.IsNullOrWhiteSpace(Empresa.NombreRepresentanteLegal) && !string.IsNullOrWhiteSpace(Empresa.RazonSocial)
                && !string.IsNullOrWhiteSpace(Empresa.RUC);
        }

        private void Grabar()
        {
            if (_esNuevo)
                _servicio.CrearEmpresa(_empresa);
            else
                _servicio.ActualizarEmpresa(_empresa);
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
