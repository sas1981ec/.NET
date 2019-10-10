using System.Collections.Generic;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class MotivoAccesoDal : Liberador, IMotivoAccesoDal
    {
        public MotivoAccesoDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public IEnumerable<MotivoAcceso> ObtenerMotivosAcceso()
        {
            return Contexto.MotivosAccesos.ToList();
        }
    }
}
