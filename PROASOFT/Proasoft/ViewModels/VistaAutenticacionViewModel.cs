using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using Spring.Context.Support;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaAutenticacionViewModel : ViewModelBase
    {
        #region Campos
        private readonly Window _vistaAutenticacion;
        private string _usuario;
        private string _contrasena;
        private bool? _dialogResult;

        #endregion

        #region Constructor
        public VistaAutenticacionViewModel(VistaAutenticacion vistaAutenticacion)
        {
            _vistaAutenticacion = vistaAutenticacion;
        }
        #endregion

        #region Propiedades
        public ICommand ComandoLogin
        {
            get
            {
                return new RelayCommand(Login, PuedoHacerLogin);
            }
        }

        public string Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                if (_usuario == value) return;
                _usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        public string Contrasena
        {
            get
            {
                return _contrasena;
            }
            set
            {
                if (_contrasena == value) return;
                _contrasena = value;
                OnPropertyChanged("Contrasena");
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
        private bool PuedoHacerLogin()
        {
            return !string.IsNullOrWhiteSpace(Usuario) && !string.IsNullOrWhiteSpace(Contrasena);
        }

        private void Login()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringLogin.xml");
            var administradorLogin= (ILogin)ctx["AdministradorLogin"];
            var resultado = administradorLogin.Login(Usuario, Contrasena);
            if (resultado.FueOk)
            {
                Application.Current.Resources.Add("LoginData", resultado);
                var vistaPrincipal = new VistaPrincipal();
                vistaPrincipal.Show();
                _vistaAutenticacion.Close();
            }
            else
                MessageBox.Show(resultado.Mensaje, Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
            administradorLogin.LiberarRecursos();
        }
        #endregion
    }
}
