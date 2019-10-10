using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IPermisoDal
    {
        IEnumerable<CabeceraPermiso> ObtenerPermisosConVisitanteYAccesos();
        IEnumerable<Permiso> ObtenerPermisosConResidenteYVisitante();
        void CrearPermiso(CabeceraPermiso cabeceraPermiso);
        void CrearPermiso(Permiso permiso);
        void ActualizarPermiso(CabeceraPermiso permisoModificado);
        void ActualizarPermiso(Permiso permisoModificado);
        void EliminarPermiso(Permiso permisoAEliminar);
        void EliminarAuditoriaPermiso(AuditoriaPermiso auditoriaPermisoAEliminar);
    }
}
