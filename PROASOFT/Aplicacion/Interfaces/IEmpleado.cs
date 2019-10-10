using PROASOFT.CapaDominio.Entidades;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IEmpleado : IBaseEmpleado, IBase
    {
        void CrearEmpleado(EMPLEADO empleado);
        void ActualizarEmpleado(EMPLEADO empleado);
    }
}
