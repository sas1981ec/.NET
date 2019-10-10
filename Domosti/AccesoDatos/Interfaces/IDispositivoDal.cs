using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IDispositivoDal
    {
        IEnumerable<Dispositivo> ObtenerDispositivosParaReporteCxC();
        IEnumerable<Dispositivo> ObtenerDispositivosParaAdministracion();
        IEnumerable<Dispositivo> ObtenerDispositivos();
        void ActualizarDispositivo(Dispositivo dispositivoAModificar);
    }
}
