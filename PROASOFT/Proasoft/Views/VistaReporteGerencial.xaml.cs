using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class VistaReporteGerencial : Page
    {
        public VistaReporteGerencial()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaReporteGerencialViewModel();
        }
    }
}
