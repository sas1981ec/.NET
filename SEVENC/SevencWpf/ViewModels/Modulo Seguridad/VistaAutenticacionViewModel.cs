using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views;
using System.Windows;
using System.Windows.Input;
using SevencWpf.Views.Modulo_Seguridad;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaAutenticacionViewModel : ViewModelEspecializado
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
            var resultado = Servicio.Login(Usuario, Contrasena, ObtenerIp());
            if (resultado.FueOk)
            {
                Application.Current.Resources.Add("LoginData",resultado);
                if (((LoginData)App.Current.Resources["LoginData"]).Empresas.Count > 1)
                {
                    var vistaSeleccionEmpresa = new VistaSeleccionEmpresa();
                    vistaSeleccionEmpresa.DataContext = new VistaSeleccionEmpresaViewModel(vistaSeleccionEmpresa, new System.Collections.ObjectModel.ObservableCollection<Empresa>(((LoginData)App.Current.Resources["LoginData"]).Empresas));
                    vistaSeleccionEmpresa.Show();
                }
                else
                {
                    ((LoginData)App.Current.Resources["LoginData"]).EmpresaSeleccionada = ((LoginData)App.Current.Resources["LoginData"]).Empresas.FirstOrDefault();
                    var vistaPrincipal = new VistaPrincipal();
                    vistaPrincipal.Show();
                }
                _vistaAutenticacion.Close();
            }
            else
                MessageBox.Show(resultado.Mensaje, Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private string ObtenerIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "";
        }

        internal override void Autorizar()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
