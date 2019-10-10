using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IViviendaDal
    {
        IEnumerable<Vivienda> ObtenerViviendas();
        IEnumerable<Vivienda> ObtenerViviendasConResidentes();
        void CrearVivienda(Vivienda viviendaNueva);
        void ActualizarVivienda(Vivienda viviendaModificada);
    }
}
