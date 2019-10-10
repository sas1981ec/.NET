using Proasoft.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proasoft.Views
{
    public partial class VistaAutenticacion : Window
    {
        public VistaAutenticacion()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaAutenticacionViewModel(this);
            TxtUsuario.Focus();
        }

        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtContrasena.Text = PswContrasena.Password;
            var expresion = TxtContrasena.GetBindingExpression(TextBox.TextProperty);
            if (expresion != null)
                expresion.UpdateSource();
        }

        private void PasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ((VistaAutenticacionViewModel)(DataContext)).ComandoLogin.Execute(null);
        }
    }
}
