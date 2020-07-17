using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MusicStore.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            MostrarMapa(-2.155538, -79.8910639);
        }

        private void MostrarMapa(double latitud, double longitud)
        {
            base.OnAppearing();

            MapaGeo.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(latitud, longitud), Distance.FromKilometers(0.5)
                    )
                );
            var pos = new Position(latitud, longitud);

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Test de XAMARIN"
            };

            MapaGeo.Pins.Add(pin);
        }
    }
}