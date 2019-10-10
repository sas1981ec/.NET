using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class DispositivosDal : Liberador, IDispositivoDal
    {
        public DispositivosDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<Dispositivo> ObtenerDispositivosParaAdministracion()
        {
            return Contexto.Dispositivos.Include("UsuarioApp.Residentes").ToList();
        }
        public IEnumerable<Dispositivo> ObtenerDispositivosParaReporteCxC()
        {
            return Contexto.Dispositivos.Include("DispositivoResidenteViviendas.Residente").Include("DispositivoResidenteViviendas.Vivienda").Include("DispositivoResidenteViviendas.AuditoriaDrvs").ToList();
        }

        public IEnumerable<Dispositivo> ObtenerDispositivos()
        {
            return Contexto.Dispositivos.ToList();
        }

        public void ActualizarDispositivo(Dispositivo dispositivoAModificar)
        {
            Contexto.Entry(dispositivoAModificar).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
    }
}
