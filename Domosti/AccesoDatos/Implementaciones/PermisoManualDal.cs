using System.Collections.Generic;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class PermisoManualDal : Liberador, IPermisoManualDal
    {
        public PermisoManualDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<PermisoManual> ObtenerPermisosManuales()
        {
            return Contexto.PermisosManuales.Include("Residente").Include("MotivoAcceso").Include("Vivienda.Ciudadela").ToList();
        }
        public void CrearPermisoManual(PermisoManual permisoManual)
        {
            Contexto.PermisosManuales.Add(permisoManual);
            Contexto.SaveChanges();
        }
    }
}
