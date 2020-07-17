using MusicStore.Models;
using MusicStore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public Login()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<LoginViewModel>(this, "Ingresar1", async (sender) =>
            {
                RootPage.Master = new MenuPage();
                await RootPage.NavigateFromMenu((int)MenuItemType.Browse);

            });
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Ingresar2", async (sender, arg) =>
            {
                await DisplayAlert("Alerta", arg, "OK");
            });
        }
    }
}