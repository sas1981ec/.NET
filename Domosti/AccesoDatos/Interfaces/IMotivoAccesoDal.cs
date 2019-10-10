using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IMotivoAccesoDal
    {
        IEnumerable<MotivoAcceso> ObtenerMotivosAcceso();
    }
}
