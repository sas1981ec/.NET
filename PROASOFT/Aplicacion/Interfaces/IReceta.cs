using PROASOFT.CapaDominio.Entidades;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface IReceta : IBaseReceta, IBase
    {
        void CrearReceta(RECETA receta);
        void ActualizarReceta(RECETA receta);
    }
}
