using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class PersonaDal : Liberador, IPersonaDal
    {
        public PersonaDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<Residente> ObtenerResidentes()
        {
            return Contexto.Personas.OfType<Residente>().ToList();
        }
        public IEnumerable<UsuarioApp> ObtenerUsuariosApp()
        {
            return Contexto.Personas.OfType<UsuarioApp>().Include("Dispositivos").ToList();
        }

        public void ActualizarUsuarioApp(UsuarioApp usuarioAppModificado)
        {
            Contexto.Entry(usuarioAppModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public void CrearUsuarioApp(UsuarioApp usuarioAppNuevo)
        {
            Contexto.Personas.Add(usuarioAppNuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Visitante> ObtenerVisitantes()
        {
            return Contexto.Personas.OfType<Visitante>().ToList();
        }

        public void CrearVisitante(Visitante visitanteNuevo)
        {
            Contexto.Personas.Add(visitanteNuevo);
            Contexto.SaveChanges();
        }

        public void ActualizarVisitante(Visitante visitanteModificado)
        {
            Contexto.Entry(visitanteModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
        public void ActualizarResidente(Residente residenteModificado)
        {
            Contexto.Entry(residenteModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
    }
}
