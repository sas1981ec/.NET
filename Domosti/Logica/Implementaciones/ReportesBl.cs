using System;
using System.Collections.Generic;
using System.Linq;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class ReportesBl : IReportesBl
    {
        private readonly IAccesoDal _dalAcceso;
        private readonly IMotivoAccesoDal _dalMotivo;
        private readonly IPermisoManualDal _dalPermisoManual;

        public ReportesBl(IAccesoDal dalAcceso, IMotivoAccesoDal dalMotivo, IPermisoManualDal dalPermisoManual)
        {
            _dalAcceso = dalAcceso;
            _dalMotivo = dalMotivo;
            _dalPermisoManual = dalPermisoManual;
        }
        public IEnumerable<Acceso> ObtenerReporteAccesosGeneral(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros)
        {
            var accesos = _dalAcceso.ObtenerAccesos().Where(a => !a.Permiso.Vivienda.EstaEliminada && a.Permiso.Vivienda.IdCiudadela == idCiudadela && a.Permiso.Estado != "E" && a.Permiso.Estado != "V").ToList();
            var permisosManuales = _dalPermisoManual.ObtenerPermisosManuales().Where(p => !p.Vivienda.EstaEliminada && p.Vivienda.IdCiudadela == idCiudadela);
            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case EnumeracionBusquedaAcceso.PorFecha:
                        var data = filtro.Value.Split(';');
                        var fechaInicial = Convert.ToDateTime(data[0]);
                        var fechaFin = Convert.ToDateTime(data[1]);
                        accesos = accesos.Where(a => a.FechaAcceso >= fechaInicial && a.FechaAcceso <= fechaFin).ToList();
                        permisosManuales = permisosManuales.Where(p => p.FechaIngreso >= fechaInicial && p.FechaIngreso <= fechaFin).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorManzana:
                        var manzana = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.Vivienda.Manzana == manzana).ToList();
                        permisosManuales = permisosManuales.Where(p => p.Vivienda.Manzana == manzana).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorVilla:
                        var villa = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.Vivienda.Villa == villa).ToList();
                        permisosManuales = permisosManuales.Where(p => p.Vivienda.Villa == villa).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionResidente:
                        accesos = accesos.Where(a => a.Permiso.Residente.Identificacion == filtro.Value).ToList();
                        permisosManuales = permisosManuales.Where(p => p.Residente.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionVisitante:
                        accesos = accesos.Where(a => a.Permiso.Visitante.Identificacion == filtro.Value).ToList();
                        permisosManuales = permisosManuales.Where(p => p.CedulaVisitante == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreResidente:
                        accesos = accesos.Where(a => a.Permiso.Residente.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || a.Permiso.Residente.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        permisosManuales = permisosManuales.Where(p => p.Residente.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || p.Residente.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreVisitante:
                        accesos = accesos.Where(a => a.Permiso.Visitante.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || a.Permiso.Visitante.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        permisosManuales = permisosManuales.Where(p => p.NombreVisitante.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorMotivo:
                        var idMotivo = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.IdMotivoAcceso == idMotivo).ToList();
                        permisosManuales = permisosManuales.Where(p => p.IdMotivoAcceso == idMotivo).ToList();
                        break;
                }
            }
            accesos.AddRange(permisosManuales.Select(permisoManual => new Acceso
            {
                EsManual = true, Observaciones = permisoManual.Observaciones, FechaAcceso = permisoManual.FechaIngreso, Permiso = new Permiso
                {
                    Residente = permisoManual.Residente, Visitante = new Visitante
                    {
                        Nombres = permisoManual.NombreVisitante, Apellidos = "", Identificacion = permisoManual.CedulaVisitante
                    },
                    MotivoAcceso = new MotivoAcceso
                    {
                        IdMotivoAcceso = permisoManual.IdMotivoAcceso,
                        Nombre = permisoManual.MotivoAcceso.Nombre
                    },
                    Vivienda = new Vivienda
                    {
                        IdVivienda = permisoManual.IdVivienda,
                        Manzana = permisoManual.Vivienda.Manzana,
                        Villa = permisoManual.Vivienda.Villa,
                        Calle = permisoManual.Vivienda.Calle,
                        Ciudadela = permisoManual.Vivienda.Ciudadela
                    }
                }
            }));
            return accesos.OrderBy(a => a.FechaAcceso);
        }
        public IEnumerable<Acceso> ObtenerReporteAccesos(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros)
        {
            var accesos = _dalAcceso.ObtenerAccesos().Where(a => a.Permiso.Residente.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela) && a.Permiso.Estado != "E" && a.Permiso.Estado != "V");
            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case EnumeracionBusquedaAcceso.PorFecha:
                        var data = filtro.Value.Split(';');
                        var fechaInicial = Convert.ToDateTime(data[0]);
                        var fechaFin = Convert.ToDateTime(data[1]);
                        accesos = accesos.Where(a => a.FechaAcceso >= fechaInicial && a.FechaAcceso <= fechaFin).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionResidente:
                        accesos = accesos.Where(a => a.Permiso.Residente.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionVisitante:
                        accesos = accesos.Where(a => a.Permiso.Visitante.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorManzana:
                        var manzana = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.Vivienda.Manzana == manzana).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorVilla:
                        var villa = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.Vivienda.Villa == villa).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreResidente:
                        accesos = accesos.Where(a => a.Permiso.Residente.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || a.Permiso.Residente.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreVisitante:
                        accesos = accesos.Where(a => a.Permiso.Visitante.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || a.Permiso.Visitante.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorMotivo:
                        var idMotivo = Convert.ToInt16(filtro.Value);
                        accesos = accesos.Where(a => a.Permiso.IdMotivoAcceso == idMotivo).ToList();
                        break;
                }
            }
            return accesos.OrderBy(a => a.FechaAcceso);
        }
        public IEnumerable<Acceso> ObtenerReporteAccesosManual(int idCiudadela, Dictionary<EnumeracionBusquedaAcceso, string> filtros)
        {
            var permisosManuales = _dalPermisoManual.ObtenerPermisosManuales().Where(p => p.Residente.Viviendas.Any(v => !v.EstaEliminada && v.IdCiudadela == idCiudadela));
            foreach (var filtro in filtros)
            {
                switch (filtro.Key)
                {
                    case EnumeracionBusquedaAcceso.PorFecha:
                        var data = filtro.Value.Split(';');
                        var fechaInicial = Convert.ToDateTime(data[0]);
                        var fechaFin = Convert.ToDateTime(data[1]);
                        permisosManuales = permisosManuales.Where(p => p.FechaIngreso >= fechaInicial && p.FechaIngreso <= fechaFin).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionResidente:
                        permisosManuales = permisosManuales.Where(p => p.Residente.Identificacion == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorIdentificacionVisitante:
                        permisosManuales = permisosManuales.Where(p => p.CedulaVisitante == filtro.Value).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorManzana:
                        var manzana = Convert.ToInt16(filtro.Value);
                        permisosManuales = permisosManuales.Where(p => p.Vivienda.Manzana == manzana).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorVilla:
                        var villa = Convert.ToInt16(filtro.Value);
                        permisosManuales = permisosManuales.Where(p => p.Vivienda.Villa == villa).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreResidente:
                        permisosManuales = permisosManuales.Where(p => p.Residente.Nombres.ToUpper().Contains(filtro.Value.ToUpper()) || p.Residente.Apellidos.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorNombreVisitante:
                        permisosManuales = permisosManuales.Where(p => p.NombreVisitante.ToUpper().Contains(filtro.Value.ToUpper())).ToList();
                        break;
                    case EnumeracionBusquedaAcceso.PorMotivo:
                        var idMotivo = Convert.ToInt16(filtro.Value);
                        permisosManuales = permisosManuales.Where(p => p.IdMotivoAcceso == idMotivo).ToList();
                        break;
                }
            }
            return permisosManuales.Select(permisoManual => new Acceso
            {
                EsManual = true, Observaciones = permisoManual.Observaciones, FechaAcceso = permisoManual.FechaIngreso, Permiso = new Permiso
                {
                    Residente = permisoManual.Residente, Visitante = new Visitante
                    {
                        Nombres = permisoManual.NombreVisitante, Apellidos = "", Identificacion = permisoManual.CedulaVisitante
                    },
                    MotivoAcceso = new MotivoAcceso
                    {
                        IdMotivoAcceso = permisoManual.IdMotivoAcceso, Nombre = permisoManual.MotivoAcceso.Nombre
                    },
                    Vivienda = new Vivienda
                    {
                        IdVivienda = permisoManual.IdVivienda,
                        Manzana = permisoManual.Vivienda.Manzana,
                        Villa = permisoManual.Vivienda.Villa,
                        Calle = permisoManual.Vivienda.Calle,
                        Ciudadela = permisoManual.Vivienda.Ciudadela
                    }
                }
            }).OrderBy(a => a.FechaAcceso).ToList();
        }
        public IEnumerable<MotivoAcceso> ObtenerMotivos()
        {
            return _dalMotivo.ObtenerMotivosAcceso();
        }
    }
}
