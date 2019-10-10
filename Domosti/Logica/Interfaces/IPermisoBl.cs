using System.Collections.Generic;
using System.IO;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IPermisoBl
    {
        IEnumerable<Permiso> ObtenerBitacoraPermisos(int idCiudadela,Dictionary<EnumeracionBusquedaPermisos,string> filtros);
        void RegistrarIngresoVisitante(Permiso permiso, string observaciones);
        Permiso ObtenerPermisoPorId(int permisoId);
        Stream ObtenerMotivosAcceso();
        Stream ObtenerPermisosPorMesYAnio(string idResidente, string mes, string anio, string estados, string idVisitante);
        Stream ObtenerPermisosPorDia(string idResidente, string dia, string mes, string anio, string estados, string idVisitante);
        Stream CrearPermiso(string fechaInicial, string fechaFin, string idResidente, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes, string idVivienda);
        Stream ActualizarPermiso(string idPermiso, string fechaInicial, string fechaFin, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes);
        Stream EliminarPermiso(string idPermiso);
    }
}
