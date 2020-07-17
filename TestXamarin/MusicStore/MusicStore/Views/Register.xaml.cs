using MusicStore.Models;
using MusicStore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public Register()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<RegisterViewModel>(this, "Grabar", async (sender) =>
            {
                await DisplayAlert("Alerta", "Usuario Registrado exitosamente !!!!!!!!!. Ahora ya puede loguearse", "OK");
                RootPage.Master = new MenuPage();
                await RootPage.NavigateFromMenu((int)MenuItemType.Login);
            });

            MessagingCenter.Subscribe<RegisterViewModel>(this, "Contrasena1", (a) =>
            {
                DisplayAlert("Alerta", "La contraseña no puede tener menos de 6 caracteres.", "OK");
            });
            MessagingCenter.Subscribe<RegisterViewModel>(this, "Contrasena2", (a) =>
            {
                DisplayAlert("Alerta", "Las contraseñas no coinciden.", "OK");
            });
            MessagingCenter.Subscribe<RegisterViewModel>(this, "Email", (a) =>
            {
                DisplayAlert("Alerta", "El formato de email es inválido.", "OK");
            });
            MessagingCenter.Subscribe<RegisterViewModel>(this, "EmailDuplicado", (a) =>
            {
                DisplayAlert("Alerta", "El email ya existe.", "OK");
            });
            MessagingCenter.Subscribe<RegisterViewModel, string>(this, "Error", async (sender, arg) =>
            {
                await DisplayAlert("Alerta", arg, "OK");
            });
        }
    }
}