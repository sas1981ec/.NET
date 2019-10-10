using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class DrvDal : Liberador, IDrvDal
    {
        public DrvDal()
        {
            Contexto = new ModeloDomostiContainer();
        }
        public IEnumerable<DispositivoResidenteVivienda> ObtenerDrvs()
        {
            return Contexto.DispositivoResidenteViviendas.ToList();
        }
        public IEnumerable<DispositivoResidenteVivienda> ObtenerDrvsConDispositivos()
        {
            return Contexto.DispositivoResidenteViviendas.Include("Dispositivo").ToList();
        }
        public IEnumerable<DispositivoResidenteVivienda> ObtenerDrvsConDispositivosYViviendas()
        {
            return Contexto.DispositivoResidenteViviendas.Include("Dispositivo").Include("Vivienda").ToList();
        }
        public void CrearDrv(DispositivoResidenteVivienda drvNueno)
        {
            Contexto.DispositivoResidenteViviendas.Add(drvNueno);
            Contexto.SaveChanges();
        }
        public void ActualizarDrv(DispositivoResidenteVivienda drvModificado)
        {
            Contexto.Entry(drvModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
    }
}
