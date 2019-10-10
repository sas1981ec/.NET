using SevencWpf.ViewModels.Modulo_Seguridad;
using System.Windows;
using System.Windows.Controls;

namespace SevencWpf.Views.Modulo_Seguridad
{
    public partial class VistaOperacion : Page
    {
        public VistaOperacion()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaOperacionViewModel();
        }
    }
}
