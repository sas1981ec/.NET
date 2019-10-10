using SevencWpf.ServicioWcf;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SevencWpf.InfraestructuraVM
{
    internal class GestionAuditoria
    {
        internal int IdOperacion { get; set; }

        internal bool PuedoAuditar()
        {
            return ((LoginData)App.Current.Resources["LoginData"]).Operaciones.Any(o => o.Key == IdOperacion && o.Value);
        }

        internal Auditoria AuditarConsulta(string entidad)
        {
            var detallesAuditoria = new List<DetalleAuditoria>
            {
                new DetalleAuditoria
                {
                    Campo = "",
                    ClaveEntidad = "",
                    Entidad = entidad,
                    ValorAntiguo = "",
                    ValorNuevo = ""
                }
            };
            var auditoria = new Auditoria
            {
                IdOperacion = IdOperacion,
                IdUsuario = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario,
                DetallesAuditorias = detallesAuditoria
            };
            return auditoria;
        }

        internal Auditoria AuditarCreacion(object entidad, string nombreEntidad, string claveEntidad)
        {
            var propiedades = entidad.GetType().GetProperties();
            var auditoria = new Auditoria
            {
                IdOperacion = IdOperacion,
                IdUsuario = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario,
                DetallesAuditorias = new List<DetalleAuditoria>()
            };
            foreach (var propiedad in propiedades)
            {
                if (propiedad.PropertyType.FullName != null && (propiedad.PropertyType.FullName.Contains("System")))
                {
                    var detalle = new DetalleAuditoria
                    {
                        Entidad = nombreEntidad,
                        ClaveEntidad = claveEntidad,
                        Campo = propiedad.Name,
                        ValorAntiguo = "",
                        ValorNuevo =
                            propiedad.GetValue(entidad, null) == null
                                ? ""
                                : propiedad.GetValue(entidad, null).ToString()
                    };
                    auditoria.DetallesAuditorias.Add(detalle);
                }
            }
            return auditoria;
        }

        internal Auditoria AuditarActualizacion(object entidad, object entidadVieja, string nombreEntidad, string claveEntidad)
        {
            var propiedades = entidad.GetType().GetProperties();
            var auditoria = new Auditoria
            {
                IdOperacion = IdOperacion,
                IdUsuario = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario,
                DetallesAuditorias = new List<DetalleAuditoria>()
            };
            foreach (var propiedad in propiedades)
            {
                if (propiedad.PropertyType.FullName != null && (propiedad.PropertyType.FullName.Contains("System") 
                    && CambioPropiedad(entidadVieja, propiedad, NuevoValor(entidad, propiedad))))
                {
                    var detalle = new DetalleAuditoria
                    {
                        Entidad = nombreEntidad,
                        ClaveEntidad = claveEntidad,
                        Campo = propiedad.Name,
                        ValorAntiguo = ObtenerValorViejo(entidadVieja, propiedad),
                        ValorNuevo = NuevoValor(entidad, propiedad)
                    };
                    auditoria.DetallesAuditorias.Add(detalle);
                }
            }
            return auditoria;
        }

        private bool CambioPropiedad(object entidaVieja, PropertyInfo propiedadInfo, string valorPropiedad)
        {
            var propiedades = entidaVieja.GetType().GetProperties();
            return (from propiedad in propiedades where propiedad.Name == propiedadInfo.Name select NuevoValor(entidaVieja, propiedad) != valorPropiedad).FirstOrDefault();
        }

        private string NuevoValor(object entidad, PropertyInfo propiedad)
        {
            return propiedad.GetValue(entidad, null) == null ? "" : propiedad.GetValue(entidad, null).ToString();
        }

        private string ObtenerValorViejo(object entidadVieja, PropertyInfo propiedadInfo)
        {
            var propiedades = entidadVieja.GetType().GetProperties();
            foreach (var propiedad in propiedades.Where(propiedad => propiedad.Name == propiedadInfo.Name))
                return NuevoValor(entidadVieja, propiedad);
            return "";
        }

        internal Auditoria AuditarAsignacion(IEnumerable<object> nuevosValores, IEnumerable<object> viejosValores, string nombreEntidad, string claveEntidad)
        {
            var auditoria = new Auditoria
            {
                IdOperacion = IdOperacion,
                IdUsuario = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario,
                DetallesAuditorias = new List<DetalleAuditoria>()
            };
            foreach (var valor in viejosValores)
            {
                var detalle = new DetalleAuditoria
                {
                    Entidad = nombreEntidad,
                    ClaveEntidad = claveEntidad,
                    Campo = "",
                    ValorAntiguo = valor.ToString(),
                    ValorNuevo = ""
                };
                auditoria.DetallesAuditorias.Add(detalle);
            }
            foreach (var valor in nuevosValores)
            {
                var detalle = new DetalleAuditoria
                {
                    Entidad = nombreEntidad,
                    ClaveEntidad = claveEntidad,
                    Campo = "",
                    ValorAntiguo = "",
                    ValorNuevo = valor.ToString(),
                };
                auditoria.DetallesAuditorias.Add(detalle);
            }
            return auditoria;
        }
    }
}
