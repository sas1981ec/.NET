namespace PROASOFT.CapaAplicacion.Aplicacion.Contratos
{
    public class Verificador
    {
        public string NombreItem { get; set; }

        public string Medida { get; set; }

        public double CantidadIngresada { get; set; }

        public double CantidadProducida { get; set; }

        public double CantidadRestante { get { return CantidadIngresada - CantidadProducida; } }

    }
}
