using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class PermisoDal : Liberador, IPermisoDal
    {
        public PermisoDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<CabeceraPermiso> ObtenerPermisosConVisitanteYAccesos()
        {
            return Contexto.CabecerasPermisos.Include("Permisos.Visitante").Include("Permisos.MotivoAcceso").Include("Permisos.Accesos").ToList();
        }
        public IEnumerable<Permiso> ObtenerPermisosConResidenteYVisitante()
        {
            return Contexto.Permisos.Include("Residente.Viviendas").Include("Visitante").ToList();
        }
        public void CrearPermiso(CabeceraPermiso cabeceraPermiso)
        {
            Contexto.CabecerasPermisos.Add(cabeceraPermiso);
            Contexto.SaveChanges();
        }
        public void CrearPermiso(Permiso permiso)
        {
            Contexto.Permisos.Add(permiso);
            Contexto.SaveChanges();
        }
        public void ActualizarPermiso(CabeceraPermiso permisoModificado)
        {
            Contexto.Entry(permisoModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
        public void ActualizarPermiso(Permiso permisoModificado)
        {
            Contexto.Entry(permisoModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
        public void EliminarPermiso(Permiso permisoAEliminar)
        {
            Contexto.Permisos.Remove(permisoAEliminar);
            Contexto.SaveChanges();
        }
        public void EliminarAuditoriaPermiso(AuditoriaPermiso auditoriaPermisoAEliminar)
        {
            Contexto.AuditoriaPermisos.Remove(auditoriaPermisoAEliminar);
            Contexto.SaveChanges();
        }
    }
}
