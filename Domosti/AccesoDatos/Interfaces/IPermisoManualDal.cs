using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IPermisoManualDal
    {
        IEnumerable<PermisoManual> ObtenerPermisosManuales();
        void CrearPermisoManual(PermisoManual permisoManual);
    }
}
