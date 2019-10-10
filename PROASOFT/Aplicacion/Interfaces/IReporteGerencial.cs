using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using System;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IReporteGerencial : IBase
    {
        Tuple<IEnumerable<Produccion>, IEnumerable<Verificador>> ObtenerCuadreDiario(DateTime fecha);
    }
}
