using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Contratos;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class DispositivosBl : ClaseBase, IDispositivosBl
    {
        private readonly IDispositivoDal _dal;
        public DispositivosBl(IDispositivoDal dal)
        {
            _dal = dal;
        }

        public Stream ObtenerDispositivos(string idResidente)
        {
            var residente = Convert.ToInt32(idResidente);
            var dispositivos = _dal.ObtenerDispositivosParaAdministracion().Where(d => d.UsuarioApp.Residentes.Any(r => r.IdPersona == residente)).Distinct();
            var resultado = dispositivos.Select(dispositivo => new DispositivoJson
            {
                id = dispositivo.IdDispositivo.ToString(CultureInfo.InvariantCulture),
                nombre = dispositivo.Nombre
            }).OrderBy(d => d.nombre).ToList();
            return resultado.Count == 0 ? ObtenerSalida("2301", null, "No existen dispositivos registrados.") : ObtenerSalidaDispositivo(resultado, "");
        }
        public IEnumerable<DispositivoUsadoMes> ObtenerReporteDispositivosUsadosMes(int idCiudadela, int mes, int anio)
        {
            var resultado = new List<DispositivoUsadoMes>();
            var diasMes = DateTime.DaysInMonth(anio, mes);
            var fechaInicio = new DateTime(anio, mes, 1);
            var fechaFin = new DateTime(anio, mes, diasMes).AddHours(23).AddMinutes(59).AddSeconds(59);
            var dispositivos =
                _dal.ObtenerDispositivosParaReporteCxC()
                    .Where(
                        d =>
                            d.FechaRegistro <= fechaFin &&
                            d.DispositivoResidenteViviendas.Any(
                                drv => drv.Vivienda.IdCiudadela == idCiudadela));
            foreach (var dispositivo in dispositivos)
            {
                foreach (var drv in dispositivo.DispositivoResidenteViviendas.Where(drv => drv.Vivienda.IdCiudadela == idCiudadela))
                {
                    if (drv.AuditoriaDrvs.Any(a => a.Fecha >= fechaInicio && a.Fecha <= fechaFin))
                    {
                        var drvFechaActivacion = drv.AuditoriaDrvs.Where(adrv => adrv.Estado == "A" && adrv.Fecha <= fechaFin).OrderByDescending(adrv => adrv.Fecha).FirstOrDefault();
                        if (drvFechaActivacion == null)
                            throw new ApplicationException("No exsite fecha Activación.");
                        var drvFechaDesactivacion = drv.AuditoriaDrvs.Where(adrv => adrv.Estado == "I" && adrv.Fecha <= fechaFin).OrderByDescending(adrv => adrv.Fecha).FirstOrDefault();
                        resultado.Add(new DispositivoUsadoMes
                        {
                            IdDispositivo = string.Format("IdDevice:{0}; Imei:{1}; Iccid:{2}", dispositivo.IdDevice, dispositivo.Imei, dispositivo.IccId),
                            IdentificacionResidente = drv.Residente.Identificacion,
                            NombreCompletoResidente = drv.Residente.NombreCompleto,
                            NombreVivienda = drv.Vivienda.Nombre,
                            FechaActivacion = drvFechaActivacion.Fecha.ToString("g"),
                            FechaDesactivacion = drvFechaDesactivacion == null ? "" : drvFechaDesactivacion.Fecha.ToString("g"),
                            Valor = resultado.Any(r => r.NombreVivienda == drv.Vivienda.Nombre) ? Convert.ToDecimal(ConfigurationManager.AppSettings["CostoAdicionales"]) : Convert.ToDecimal(ConfigurationManager.AppSettings["CostoPrincipal"])
                        });
                    }

                    else
                    {
                        var auditoriaDrv = drv.AuditoriaDrvs.Where(a => a.Fecha < fechaInicio).OrderByDescending(a => a.Fecha).FirstOrDefault();
                        if (auditoriaDrv != null && auditoriaDrv.Estado == "A")
                            resultado.Add(new DispositivoUsadoMes
                            {
                                FechaActivacion = auditoriaDrv.Fecha.ToString("g"),
                                IdDispositivo = string.Format("IdDevice:{0}; Imei:{1}; Iccid:{2}", dispositivo.IdDevice, dispositivo.Imei, dispositivo.IccId),
                                IdentificacionResidente = drv.Residente.Identificacion,
                                NombreCompletoResidente = drv.Residente.NombreCompleto,
                                NombreVivienda = drv.Vivienda.Nombre,
                                FechaDesactivacion = "",
                                Valor = resultado.Any(r => r.NombreVivienda == drv.Vivienda.Nombre) ? Convert.ToDecimal(ConfigurationManager.AppSettings["CostoAdicionales"]) : Convert.ToDecimal(ConfigurationManager.AppSettings["CostoPrincipal"])
                            });
                    }
                }
            }
            return resultado.OrderBy(r => r.NombreVivienda);
        }
        public Stream ActualizarToken(string idDispositivo, string token)
        {
            var id = Convert.ToInt32(idDispositivo);
            var dispositivo = _dal.ObtenerDispositivos().FirstOrDefault(d => d.IdDispositivo == id);
            if (dispositivo == null)
                return ObtenerSalida("1801", "", "No existe dispositivo.");
            dispositivo.Token = token;
            _dal.ActualizarDispositivo(dispositivo);
            return ObtenerSalida("1800", "", "");
        }
    }
}
