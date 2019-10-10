using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IDrvDal
    {
        IEnumerable<DispositivoResidenteVivienda> ObtenerDrvs();
        IEnumerable<DispositivoResidenteVivienda> ObtenerDrvsConDispositivos();
        IEnumerable<DispositivoResidenteVivienda> ObtenerDrvsConDispositivosYViviendas();
        void CrearDrv(DispositivoResidenteVivienda drvNueno);
        void ActualizarDrv(DispositivoResidenteVivienda drvModificado);
    }
}
