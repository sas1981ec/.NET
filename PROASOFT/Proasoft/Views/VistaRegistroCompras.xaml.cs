using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class VistaRegistroCompras : Page
    {
        public VistaRegistroCompras()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaRegistroComprasViewModel();
        }
    }
}
