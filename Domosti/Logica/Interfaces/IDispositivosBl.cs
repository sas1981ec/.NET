using System.Collections.Generic;
using System.IO;
using Domosti.CapaNegocios.Contratos;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IDispositivosBl
    {
        Stream ObtenerDispositivos(string idResidente);
        IEnumerable<DispositivoUsadoMes> ObtenerReporteDispositivosUsadosMes(int idCiudadela, int mes, int anio);
        Stream ActualizarToken(string idDispositivo, string token);
    }
}
