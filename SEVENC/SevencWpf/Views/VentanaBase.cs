using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SevencWpf.Views
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

    public static class PaginaBase
    {
        public static void SoloNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab) return;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public static void TxtPanelPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(typeof(string))) return;
            var pastingText = (string)e.DataObject.GetData(typeof(string));
            try
            {
                var numero = Convert.ToInt64(pastingText);
            }
            catch (Exception)
            {
                e.CancelCommand();
            }
        }
    }
}
