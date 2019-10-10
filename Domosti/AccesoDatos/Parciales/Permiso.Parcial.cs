
namespace Domosti.CapaDatos.Modelos
{

    public partial class Permiso
    {
        public string FechaInicialConHora
        {
            get
            {
                return FechaInicial.ToString("dd/MM/yyyy HH:mm");
            }
        }
        public string FechaFinConHora
        {
            get
            {
                return FechaFin.ToString("dd/MM/yyyy HH:mm");
            }
        }
        public string Observaciones { get; set; }
    }

}
