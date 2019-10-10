using Proasoft.ViewModels;
using System.Windows;

namespace Proasoft.Views
{
    public partial class VistaPrincipal : Window
    {
        public VistaPrincipal()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            FrmContenedor.Height = SystemParameters.PrimaryScreenHeight - SystemParameters.MinimizedWindowHeight;
            FrmContenedor.Width = SystemParameters.PrimaryScreenWidth - SystemParameters.MinimizedWindowWidth;

            DataContext = new VistaPrincipalViewModel(this, FrmContenedor);
        }

        private void WindowStateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
        }
    }
}
