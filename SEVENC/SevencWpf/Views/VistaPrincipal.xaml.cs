using SevencWpf.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SevencWpf.Views
{
    public partial class VistaPrincipal : Window
    {
        private bool _estaMoviendoRectangulo;
        private double _posicionInicialXRectangulo;
        private double _posicionInicialXFrame;

        public VistaPrincipal()
        {
            InitializeComponent();
            MouseMove += ViewPrincipalMouseMove;
        }

        private void ViewPrincipalMouseMove(object sender, MouseEventArgs e)
        {
            if (!_estaMoviendoRectangulo)
            {
                _posicionInicialXRectangulo = e.GetPosition(Rectangulo).X;
                _posicionInicialXFrame = e.GetPosition(FrmContenedor).X;
                return;
            }
            Rectangulo.Margin = new Thickness(Rectangulo.Margin.Left + (e.GetPosition(Rectangulo).X - _posicionInicialXRectangulo), Rectangulo.Margin.Top, Rectangulo.Margin.Right, Rectangulo.Margin.Bottom);
            FrmContenedor.Margin = new Thickness(FrmContenedor.Margin.Left + (e.GetPosition(FrmContenedor).X - _posicionInicialXFrame), FrmContenedor.Margin.Top, FrmContenedor.Margin.Right, FrmContenedor.Margin.Bottom);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            FrmContenedor.Height = SystemParameters.PrimaryScreenHeight - SystemParameters.MinimizedWindowHeight;
            DataContext = new VistaPrincipalViewModel(FrmContenedor);
        }

        private void RectangleMouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.ScrollWE;
        }

        private void RectangleMouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            Rectangulo.Fill = Brushes.Black;
        }

        private void RectangleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangulo.Fill = Brushes.DarkOrange;
            _estaMoviendoRectangulo = true;
        }

        private void RectanguloMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangulo.Fill = Brushes.Black;
            _estaMoviendoRectangulo = false;
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
