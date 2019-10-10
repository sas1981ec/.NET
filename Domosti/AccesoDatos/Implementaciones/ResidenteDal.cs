using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class ResidenteDal : Liberador, IResidenteDal
    {
        public ResidenteDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public IEnumerable<Residente> ObtenerResidentesConViviendasYVisitantes()
        {
            return Contexto.Personas.OfType<Residente>().Include("Viviendas").Include("Visitantes").ToList();
        }
        public IEnumerable<Residente> ObtenerResidentesConViviendasYCiudadelas()
        {
            return Contexto.Personas.OfType<Residente>().Include("Viviendas.Ciudadela").ToList();
        }

        public Residente ObtenerResidenteConNotificaciones(int idResidente)
        {
            return Contexto.Personas.OfType<Residente>().Include("Notificaciones").FirstOrDefault(r => r.IdPersona == idResidente);
        }
        public void CrearResidente(Residente residenteNuevo)
        {
            Contexto.Personas.Add(residenteNuevo);
            Contexto.SaveChanges();
        }

        public void AsignarViviendasAResidente(int idResidente, IEnumerable<int> idViviendas)
        {
            var residente = Contexto.Personas.OfType<Residente>().FirstOrDefault(r => r.IdPersona == idResidente);
            if (residente == null)
                throw new ApplicationException("No existe Residente.");
            foreach (var vivienda in idViviendas.Select(idVivienda => Contexto.Viviendas.FirstOrDefault(v => v.IdVivienda == idVivienda)))
            {
                if (vivienda == null)
                    throw new ApplicationException("No existe Vivienda.");
                residente.Viviendas.Add(vivienda);
            }
            Contexto.SaveChanges();
        }

        public void ActualizarResidente(Residente residenteModificado)
        {
            Contexto.Entry(residenteModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public void ActualizarFotoResidente(FotoResidente fotoResidenteModificada)
        {
            Contexto.Entry(fotoResidenteModificada).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public FotoResidente ObtenerFotoResidente(long idResidente)
        {
            return Contexto.FotosResidentes.FirstOrDefault(fr => fr.Residente.IdPersona == idResidente);
        }
    }
}
