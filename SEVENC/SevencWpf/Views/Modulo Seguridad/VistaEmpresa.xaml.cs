using SevencWpf.ViewModels.Modulo_Seguridad;
using System.Windows;
using System.Windows.Controls;

namespace SevencWpf.Views.Modulo_Seguridad
{
    public partial class VistaEmpresa : Page
    {
        public VistaEmpresa()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaEmpresaViewModel();
        }
    }
}
