using SevencWpf.ViewModels.Modulo_Seguridad;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SevencWpf.Views.Modulo_Seguridad
{
    public partial class VistaUsuario : Page
    {
        public VistaUsuario()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new VistaUsuarioViewModel();
        }

        private void Expandir(object sender, RoutedEventArgs e)
        {
            ((Expander)sender).Height = 115;
            ListaUsuarios.Height = 500;
            ListaRoles.Height = 500;
        }

        private void Colapsar(object sender, RoutedEventArgs e)
        {
            ((Expander)sender).Height = 35;
            ListaUsuarios.Height = 580;
            ListaRoles.Height = 580;
        }

        private void SoloNumeros(object sender, KeyEventArgs e)
        {
            PaginaBase.SoloNumeros(sender, e);
        }

        private void TxtPanelPasting(object sender, DataObjectPastingEventArgs e)
        {
            PaginaBase.TxtPanelPasting(sender, e);
        }
    }
}
