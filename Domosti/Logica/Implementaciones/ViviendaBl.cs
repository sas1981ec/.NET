using System.Collections.Generic;
using System.Linq;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class ViviendaBl : IViviendaBl
    {
        private readonly IViviendaDal _dal;

        public ViviendaBl(IViviendaDal dal)
        {
            _dal = dal;
        }
        public IEnumerable<Vivienda> ObtenerViviendas(int idCiudadela)
        {
            return _dal.ObtenerViviendasConResidentes().Where(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela && !v.EsSistema).OrderBy(v => v.Manzana).ThenBy(v => v.Villa).ThenBy(v => v.Calle).ToList();
        }

        public IEnumerable<Vivienda> ObtenerViviendasPorCalleYPorVilla(int idCiudadela, string calle, short villa)
        {
            if(calle != "" && villa != 0)
                return _dal.ObtenerViviendasConResidentes().Where(v => !v.EstaEliminada && !v.EsSistema && v.IdCiudadela == idCiudadela && v.Calle.ToUpper().Contains(calle.ToUpper()) && v.Villa == villa).OrderBy(v => v.Manzana).ThenBy(v => v.Villa).ThenBy(v => v.Calle).ToList();
            if(calle != "" && villa == 0)
                return _dal.ObtenerViviendasConResidentes().Where(v => !v.EstaEliminada && !v.EsSistema && v.IdCiudadela == idCiudadela && v.Calle.ToUpper().Contains(calle.ToUpper())).OrderBy(v => v.Manzana).ThenBy(v => v.Villa).ThenBy(v => v.Calle).ToList();
            return _dal.ObtenerViviendasConResidentes().Where(v => !v.EstaEliminada && !v.EsSistema && v.IdCiudadela == idCiudadela && v.Villa == villa).OrderBy(v => v.Manzana).ThenBy(v => v.Villa).ThenBy(v => v.Calle).ToList();
        }
        public IEnumerable<Vivienda> ObtenerViviendasPorResidente(int idResidente)
        {
            return _dal.ObtenerViviendasConResidentes().Where(v => !v.EstaEliminada && !v.EsSistema && v.Residentes.Any(r => r.IdPersona == idResidente)).OrderBy(v => v.Manzana).ThenBy(v => v.Villa).ThenBy(v => v.Calle).ToList();
        }
        public Vivienda ObtenerViviendasPorId(int idVivienda)
        {
            return _dal.ObtenerViviendas().FirstOrDefault(v => v.IdVivienda == idVivienda);
        }
        public void CrearVivienda(Vivienda viviendaNueva)
        {
            _dal.CrearVivienda(viviendaNueva);
        }
        public void ActualizarVivienda(Vivienda viviendaModificada)
        {
            _dal.ActualizarVivienda(viviendaModificada);
        }
        public void EliminarVivienda(Vivienda viviendaEliminada)
        {
            viviendaEliminada.EstaEliminada = true;
            _dal.ActualizarVivienda(viviendaEliminada);
        }
    }
}
