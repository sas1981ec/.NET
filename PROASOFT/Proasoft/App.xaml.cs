using System;
using System.Windows;

namespace Proasoft
{
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            Current.Resources.Add("NombreAplicacion", "PROASOFT");
            SplashScreen();
        }

        private void SplashScreen()
        {
            var splash = new SplashScreen("/Recursos/Imagenes/Splash.png");
            splash.Show(false, true);
            splash.Close(new TimeSpan(0, 0, 7));
        }

        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if(e.Exception is ApplicationException)
                MessageBox.Show(e.Exception.Message, Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
            e.Handled = true;
        }
    }
}
