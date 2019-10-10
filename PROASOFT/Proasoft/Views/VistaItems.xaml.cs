using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class VistaItems : Page
    {
        public VistaItems()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaItemsViewModel();
        }
    }
}
