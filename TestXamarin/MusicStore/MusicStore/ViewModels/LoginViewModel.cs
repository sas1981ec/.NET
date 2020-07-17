using MusicStore.Models;
using MusicStore.Views;
using Xamarin.Forms;

namespace MusicStore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Variables
        private string _email;
        private string _contrasena;
        private bool _estaOcupado;
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            ComandoIngresar = new Command(IngresarAsync, PuedoIngresar);
            ComandoNuevoCliente = new Command(NuevoClienteAsync);
            PropertyChanged += (e, v) => { ComandoIngresar.ChangeCanExecute(); };
        }
        #endregion

        #region Propiedades
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
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
                _contrasena = value;
                OnPropertyChanged();
            }
        }

        public bool EstaOcupado
        {
            get
            {
                return _estaOcupado;
            }
            set
            {
                _estaOcupado = value;
                OnPropertyChanged();
            }
        }

        public Command ComandoIngresar { get; set; }

        public Command ComandoNuevoCliente { get; set; }
        #endregion

        #region Metodos
        private async void NuevoClienteAsync()
        {
            if (EstaOcupado)
                return;
            await RootPage.NavigateFromMenu((int)MenuItemType.Registro);
        }
        private bool PuedoIngresar(object obj)
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Contrasena);
        }

        private async void IngresarAsync(object obj)
        {
            if (EstaOcupado)
                return;
            EstaOcupado = true;

            var result = await App.Database.Login(Email, Contrasena);
            if (result != null)
            {
                LimpiarCampos();
                Application.Current.Resources.Add("Logueado","true");
                MessagingCenter.Send(this, "Ingresar1");
            }
            else
                MessagingCenter.Send(this, "Ingresar2", "Usuario no existe!!!.");
            EstaOcupado = false;
        }

        private void LimpiarCampos()
        {
            Email = null;
            Contrasena = null;
        }
        #endregion
    }
}

