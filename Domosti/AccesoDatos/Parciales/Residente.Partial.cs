using System.ComponentModel.DataAnnotations;

namespace Domosti.CapaDatos.Modelos
{
    [MetadataType(typeof(ResidenteMetaData))]
    public partial class Residente
    {
        public string Tipo
        {
            get { return TipoIdentificacion == "C" ? "Cédula" : TipoIdentificacion == "R" ? "R.U.C." : "Pasaporte"; }
        }
        public string NombreCompleto { get { return string.Format("{0} {1}", Apellidos, Nombres); } }
    }

    public class ResidenteMetaData
    {
        [Required(ErrorMessage = "El campo es requerido.")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(50, ErrorMessage = "Los Nombres deben tener como máximo 50 carácteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(50, ErrorMessage = "Los Apellidos deben tener como máximo 50 carácteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(30, ErrorMessage = "El Email debe tener como máximo 30 carácteres.")]
        [EmailAddress(ErrorMessage = "Formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(13, ErrorMessage = "El Celular deben tener como máximo 13 carácteres.")]
        public string TelefonoMovil { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public string FechaNacimiento { get; set; }
    }
}
