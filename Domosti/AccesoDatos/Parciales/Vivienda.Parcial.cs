using System.ComponentModel.DataAnnotations;

namespace Domosti.CapaDatos.Modelos
{
    [MetadataType(typeof(ViviendaMetaData))]
    public partial class Vivienda
    {
        public string Nombre
        {
            get
            {
                switch (Ciudadela.Tipo)
                {
                    case "U" :
                        return string.Format("Manz:{1};Villa:{2};Calle:{0}", Calle, Manzana, Villa);
                    case "C" :
                        return string.Format("Piso:{1};Departamento:{2};D.A.:{0}", Calle, Manzana, Villa);
                    case "E" :
                        return string.Format("Piso:{1};Oficina:{2};D.A.:{0}", Calle, Manzana, Villa);
                    default :
                        return "";
                }
            }
        }
    }

    public class ViviendaMetaData
    {
        [Required(ErrorMessage = "El campo es requerido.")]
        public short Manzana { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        public short Villa { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(50, ErrorMessage = "La calle debe tener como máximo 50 carácteres.")]
        public string Calle { get; set; }

       
        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(10, ErrorMessage = "El Teléfono deben tener como máximo 10 carácteres.")]
        public string Telefono { get; set; }
    }
}