using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IItem : IBaseItem, IBase
    {
        IEnumerable<MEDIDA> ObtenerMedidas();
        void CrearItem(ITEM item);
        void ActualizarItem(ITEM item);
    }
}
