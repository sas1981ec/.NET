using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class VistaItemsProduccion : Page
    {
        public VistaItemsProduccion()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaItemsProduccionViewModel();
        }
    }
}
