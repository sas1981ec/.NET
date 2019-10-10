using PROASOFT.CapaDominio.Entidades;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IBase
    {
        void LiberarRecursos();
    }

    public interface IBaseItem
    {
        IEnumerable<ITEM> ObtenerItems();
    }

    public interface IBaseReceta
    {
        IEnumerable<RECETA> ObtenerRecetas();
    }

    public interface IBaseProveedor
    {
        IEnumerable<PROVEEDOR> ObtenerProveedores();
    }

    public interface IBaseEmpleado  
    {
        IEnumerable<EMPLEADO> ObtenerEmpleados();
    }
}
