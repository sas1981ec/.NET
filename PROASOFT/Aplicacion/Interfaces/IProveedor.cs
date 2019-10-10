using PROASOFT.CapaDominio.Entidades;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IProveedor : IBaseProveedor, IBase
    {
        void CrearProveedor(PROVEEDOR proveedor);
        void ActualizarProveedor(PROVEEDOR proveedor);
    }
}
