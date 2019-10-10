namespace PROASOFT.CapaAplicacion.Aplicacion.Contratos
{
    public class Stock
    {
        public string NombreItem { get; set; }

        public string EtiquetaMedida { get; set; }

        public double CantidadBodedaPrincipal { get; set; }

        public double CantidadBodegaProduccion { get; set; }

        public double Total { get { return CantidadBodedaPrincipal + CantidadBodegaProduccion; } }
    }
}
