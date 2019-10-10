using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IViviendaBl
    {
        IEnumerable<Vivienda> ObtenerViviendas(int idCiudadela);
        IEnumerable<Vivienda> ObtenerViviendasPorCalleYPorVilla(int idCiudadela,string calle, short villa);
        IEnumerable<Vivienda> ObtenerViviendasPorResidente(int idResidente);
        Vivienda ObtenerViviendasPorId(int idVivienda);
        void CrearVivienda(Vivienda viviendaNueva);
        void ActualizarVivienda(Vivienda viviendaModificada);
        void EliminarVivienda(Vivienda viviendaEliminada);
    }
}
