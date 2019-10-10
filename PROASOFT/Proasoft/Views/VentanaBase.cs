using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proasoft.Views
{
    public class VentanaBase : Window
    {
        protected void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        protected void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        protected void SoloNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab) return;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
