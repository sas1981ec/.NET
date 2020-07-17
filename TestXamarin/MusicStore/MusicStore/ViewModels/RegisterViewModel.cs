using MusicStore.Models;
using MusicStore.Views;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicStore.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Variables
        private string _identificacion;
        private string _email;
        private string _sitioWeb;
        private string _direccion;
        private string _nombres;
        private string _apellidos;
        private string _telefono;
        private string _contrasena;
        private string _repetirContrasena;
        private bool _estaOcupado;
        private bool _esVisibleContrasena;
        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            ComandoRegresar = new Command(RegresarAsync);
            ComandoGrabar = new Command(GrabarAsync, PuedoGrabar);
            PropertyChanged += (e, v) => { ComandoGrabar.ChangeCanExecute(); };
            EstablecerVisibilidadContrasena();
        }

        protected virtual void EstablecerVisibilidadContrasena()
        {
            EsVisibleContrasena = true;
        }
        #endregion

        #region Propiedades
        protected MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public string Identificacion
        {
            get
            {
                return _identificacion;
            }
            set
            {
                _identificacion = value;
                OnPropertyChanged();
            }
        }

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

        public string SitioWeb
        {
            get
            {
                return _sitioWeb;
            }
            set
            {
                _sitioWeb = value;
                OnPropertyChanged();
            }
        }

        public string Direccion
        {
            get
            {
                return _direccion;
            }
            set
            {
                _direccion = value;
                OnPropertyChanged();
            }
        }

        public string Nombres
        {
            get
            {
                return _nombres;
            }
            set
            {
                _nombres = value;
                OnPropertyChanged();
            }
        }

        public string Apellidos
        {
            get
            {
                return _apellidos;
            }
            set
            {
                _apellidos = value;
                OnPropertyChanged();
            }
        }

        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
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

        public string RepetirContrasena
        {
            get
            {
                return _repetirContrasena;
            }
            set
            {
                _repetirContrasena = value;
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

        public bool EsVisibleContrasena
        {
            get
            {
                return _esVisibleContrasena;
            }
            set
            {
                _esVisibleContrasena = value;
                OnPropertyChanged();
            }
        }

        public virtual string Titulo { get { return "Nuevo usuario"; } }

        public Command ComandoRegresar { get; set; }

        public Command ComandoGrabar { get; set; }
        #endregion

        #region Metodos
        protected virtual async void RegresarAsync()
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.Login);
        }

        protected virtual bool PuedoGrabar(object obj)
        {
            return !string.IsNullOrWhiteSpace(Identificacion) && !string.IsNullOrWhiteSpace(Email)  && !string.IsNullOrWhiteSpace(Nombres) && !string.IsNullOrWhiteSpace(Apellidos) && !string.IsNullOrWhiteSpace(Contrasena) && !string.IsNullOrWhiteSpace(RepetirContrasena);
        }

        protected virtual async void GrabarAsync(object obj)
        {
            if (EstaOcupado)
                return;
            EstaOcupado = true;
            if (await ValidaGrabarAsync())
            {
                try
                {
                    var user = new User
                    {
                        Apellidos = Apellidos,
                        Contraseña = Contrasena,
                        Correo = Email,
                        Identificacion = Identificacion,
                        Nombres = Nombres
                    };
                    await App.Database.SaveUser(user);
                    LimpiarCampos();
                    MessagingCenter.Send(this, "Grabar");
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(this, "Error", ex.Message);
                }


            }
            EstaOcupado = false;
        }

        protected void LimpiarCampos()
        {
            Identificacion = null;
            Nombres = null;
            Apellidos = null;
            Email = null;
            Contrasena = null;
            RepetirContrasena = null;
            Direccion = null;
            SitioWeb = null;
            Telefono = null;
        }

        private async Task<bool> ValidaGrabarAsync()
        {
            if (Contrasena.Length < 6)
            {
                MessagingCenter.Send(this, "Contrasena1");
                return false;
            }
            if (RepetirContrasena != Contrasena)
            {
                MessagingCenter.Send(this, "Contrasena2");
                return false;
            }
            var expresionRegular = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!expresionRegular.IsMatch(Email))
            {
                MessagingCenter.Send(this, "Email");
                return false;
            }
            var count = await App.Database.GetUserByEmail(Email);
            if(count > 0)
            {
                MessagingCenter.Send(this, "EmailDuplicado");
                return false;
            }
            return true;
        }
        #endregion
    }

    public class NumericValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x));

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }
}
