namespace Proasoft.Views
{
    public partial class EdicionItem : VentanaBase
    {
        public EdicionItem()
        {
            InitializeComponent();
            KeyDown += WindowKeyDown;
        }
    }
}
