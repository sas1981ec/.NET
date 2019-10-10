using System.Collections.Generic;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class AccesoDal : Liberador, IAccesoDal
    {
        public AccesoDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public IEnumerable<Acceso> ObtenerAccesos()
        {
            return Contexto.Accesos.Include("Permiso.Vivienda.Ciudadela").Include("Permiso.Residente").Include("Permiso.Visitante").Include("Permiso.MotivoAcceso").ToList();
        }
    }
}
