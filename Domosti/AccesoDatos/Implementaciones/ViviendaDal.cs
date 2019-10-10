using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class ViviendaDal : Liberador, IViviendaDal
    {
        public ViviendaDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<Vivienda> ObtenerViviendas()
        {
            return Contexto.Viviendas.ToList();
        }
        public IEnumerable<Vivienda> ObtenerViviendasConResidentes()
        {
            return Contexto.Viviendas.Include("Residentes").ToList();
        }

        public void CrearVivienda(Vivienda viviendaNueva)
        {
            Contexto.Viviendas.Add(viviendaNueva);
            Contexto.SaveChanges();
        }

        public void ActualizarVivienda(Vivienda viviendaModificada)
        {
            Contexto.Entry(viviendaModificada).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
    }
}
