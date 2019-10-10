using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IAccesoDal
    {
        IEnumerable<Acceso> ObtenerAccesos();
    }
}
