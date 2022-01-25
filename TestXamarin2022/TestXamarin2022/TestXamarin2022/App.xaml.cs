using System;
using TestXamarin2022.Services;
using TestXamarin2022.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarin2022
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
