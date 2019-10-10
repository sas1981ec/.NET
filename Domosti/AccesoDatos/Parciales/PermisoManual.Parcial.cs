using System.ComponentModel.DataAnnotations;

namespace Domosti.CapaDatos.Modelos
{
    [MetadataType(typeof(PermisoManualMetaData))]
    public partial class PermisoManual
    {

    }

    public class PermisoManualMetaData
    {

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(100, ErrorMessage = "El Nombre debe tener como máximo 100 carácteres.")]
        public string NombreVisitante { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        public string Observaciones { get; set; }


        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(15, ErrorMessage = "La Cedula deben tener como máximo 15 carácteres.")]
        public string CedulaVisitante { get; set; }

    }
}
