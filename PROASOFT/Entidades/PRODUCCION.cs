//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROASOFT.CapaDominio.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCCION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCCION()
        {
            this.DETALLES_PRODUCCION = new HashSet<DETALLE_PRODUCCION>();
        }
    
        public int ID_PRODUCCION { get; set; }
        public System.DateTime FECHA { get; set; }
        public short ID_USUARIO { get; set; }
        public bool ES_REAL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_PRODUCCION> DETALLES_PRODUCCION { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
