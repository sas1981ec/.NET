using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IReportesBl
    {
        IEnumerable<Acceso> ObtenerReporteAccesosGeneral(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros);
        IEnumerable<Acceso> ObtenerReporteAccesos(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros);
        IEnumerable<Acceso> ObtenerReporteAccesosManual(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros);
        IEnumerable<MotivoAcceso> ObtenerMotivos();
    }
}
