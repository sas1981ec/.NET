using SEVENC.Dominio.General;
using SevencWpf.ServicioWcf;
using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Windows;

namespace SevencWpf
{
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            Current.Resources.Add("NombreAplicacion", "SEVENC");
        }

        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is FaultException)
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message, Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            try
            {
                var mensaje = string.Format("Mensaje : {0}\nInner Exception : {1}\nStackTrace : {2}\nSource : {3}", e.Exception.Message, e.Exception.InnerException, e.Exception.StackTrace, e.Exception.Source);
                var servicio = ObtenerServicio();
                var idError = servicio.CrearError(new Error
                {
                    Detalle = mensaje,
                    Mensaje = e.Exception.Message
                });
                servicio.Close();
                MessageBox.Show(string.Format("Ha ocurrido un inconveniente.\nReportelo con el código: {0}", idError), Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                var mensaje = string.Format("Mensaje : {0}\nInner Exception : {1}\nStackTrace : {2}\nSource : {3}", ex.Message, ex.InnerException, ex.StackTrace, ex.Source);
                LoguearError(mensaje);
            }
            e.Handled = true;
        }

        private void LoguearError(string mensaje)
        {
            try
            {
                var ruta = Path.Combine(ConfigurationManager.AppSettings["RutaLog"], ConfigurationManager.AppSettings["NombreArchivoLog"]);
                var sw = new StreamWriter(ruta, true);
                sw.WriteLine("Fecha: {0}. Mensaje: {1}", DateTime.Now, mensaje);
                sw.Close();
                MessageBox.Show("Ha ocurrido un inconveniente.\nContactarse con el Departamento de IT.", Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error y no se ha podido loguearlo.", Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            var servicio = ObtenerServicio();
            servicio.CerrarSesion(((LoginData)Current.Resources["LoginData"]).IdSesion);
            servicio.Close();
        }

        private ServicioWcfClient ObtenerServicio()
        {
            var servicio = new ServicioWcfClient();
            servicio.ClientCredentials.UserName.UserName = Encriptar.HashPassword("EraDigital");
            servicio.ClientCredentials.UserName.Password = Encriptar.HashPassword("M@ch1n3L3@rn1ng");
            return servicio;
        }
    }
}
