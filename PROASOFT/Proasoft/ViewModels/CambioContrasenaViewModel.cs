using Proasoft.InfraestructuraVM;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using Spring.Context.Support;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class CambioContrasenaViewModel : ViewModelBase
    {
        #region Campos
        private string _contrasenaAntigua;
        private string _contrasenaNueva;
        private string _contrasenaNueva2;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public CambioContrasenaViewModel()
        {
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

        public string ContrasenaAntigua
        {
            get
            {
                return _contrasenaAntigua;
            }
            set
            {
                if (_contrasenaAntigua == value) return;
                _contrasenaAntigua = value;
                OnPropertyChanged("ContrasenaAntigua");
            }
        }

        public string ContrasenaNueva
        {
            get
            {
                return _contrasenaNueva;
            }
            set
            {
                if (_contrasenaNueva == value) return;
                _contrasenaNueva = value;
                OnPropertyChanged("ContrasenaNueva");
            }
        }

        public string ContrasenaNueva2
        {
            get
            {
                return _contrasenaNueva2;
            }
            set
            {
                if (_contrasenaNueva2 == value) return;
                _contrasenaNueva2 = value;
                OnPropertyChanged("ContrasenaNueva2");
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
            return !string.IsNullOrWhiteSpace(ContrasenaAntigua) && !string.IsNullOrWhiteSpace(ContrasenaNueva) && !string.IsNullOrWhiteSpace(ContrasenaNueva2);
        }

        private void Grabar()
        {
            if (ContrasenaNueva != ContrasenaNueva2)
            {
                MessageBox.Show("Las contraseñas no coinciden.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            var ctx = new XmlApplicationContext("~/Springs/SpringLogin.xml");
            var administradorLogin = (ILogin)ctx["AdministradorLogin"];
            administradorLogin.CambiarContrasena(((LoginData)App.Current.Resources["LoginData"]).IdUsuario, ContrasenaAntigua, ContrasenaNueva);
            administradorLogin.LiberarRecursos();
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
