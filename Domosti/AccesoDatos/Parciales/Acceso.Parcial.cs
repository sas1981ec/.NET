namespace Domosti.CapaDatos.Modelos
{
    public partial class Acceso
    {
        public string FechaAccesoConHora
        {
            get
            {
                return FechaAcceso.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public bool EsManual { private get; set; }

        public string TipoAcceso {
            get { return EsManual ? "Manual" : "Sistema"; }
        }
    }
}
