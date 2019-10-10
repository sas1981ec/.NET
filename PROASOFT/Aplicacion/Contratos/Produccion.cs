using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Contratos
{
    public class Produccion
    {
        public string NombreProducto { get; set; }

        public short Cantidad { get; set; }

        public decimal CostoProduccionUnitaria { get { return CostoProduccionTotal / Cantidad; } }

        public decimal CostoProduccionTotal { get; set; }

        public IEnumerable<DetalleProduccion> DetallesProduccion { get; set; }
    }

    public class DetalleProduccion
    {
        public int IdItem { get; set; }

        public string NombreItem { get; set; }

        public double CantidadQueSeDebioProducirEnGramos { get; set; }
    }
}
