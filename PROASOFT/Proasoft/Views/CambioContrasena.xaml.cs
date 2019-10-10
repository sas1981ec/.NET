using System.Windows;
using System.Windows.Controls;

namespace Proasoft.Views
{
    public partial class CambioContrasena : VentanaBase
    {
        public CambioContrasena()
        {
            InitializeComponent();
            KeyDown += WindowKeyDown;
        }

        private void ActualizarTextoContrasenaAntigua(object sender, RoutedEventArgs e)
        {
            TxtContrasenaAntigua.Text = PswContrasenaAntigua.Password;
            var expresion = TxtContrasenaAntigua.GetBindingExpression(TextBox.TextProperty);
            if (expresion != null)
                expresion.UpdateSource();
        }

        private void ActualizarTextoContrasenaNueva(object sender, RoutedEventArgs e)
        {
            TxtContrasenaNueva.Text = PswContrasenaNueva.Password;
            var expresion = TxtContrasenaNueva.GetBindingExpression(TextBox.TextProperty);
            if (expresion != null)
                expresion.UpdateSource();
        }

        private void ActualizarTextoContrasenaNueva2(object sender, RoutedEventArgs e)
        {
            TxtContrasenaNueva2.Text = PswContrasenaNueva2.Password;
            var expresion = TxtContrasenaNueva2.GetBindingExpression(TextBox.TextProperty);
            if (expresion != null)
                expresion.UpdateSource();
        }
    }
}
