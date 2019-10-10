using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IStock : IBase
    {
        IEnumerable<Stock> ObtenerStockItems();
    }
}
