using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class VistaRecetas : Page
    {
        public VistaRecetas()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaRecetasViewModel();
        }
    }
}
