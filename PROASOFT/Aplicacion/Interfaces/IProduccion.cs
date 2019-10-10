using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IProduccion : IBaseReceta, IBaseEmpleado, IBase
    {
        IEnumerable<PRODUCCION> ObtenerOrdenesProducciones(DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<PRODUCCION> ObtenerProducciones(DateTime fechaDesde, DateTime fechaHasta);
        Tuple<bool, Dictionary<ITEM, Tuple<double, bool>>> ExisteEnBodega(Dictionary<int, short> idRecetasCantidades);
        int CrearProduccion(PRODUCCION produccion, Dictionary<ITEM, Tuple<double, bool>> itemCantidad);
        void CrearProduccionReal(PRODUCCION produccion);
    }
}
