//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domosti.CapaDatos.Modelos
{
    using System;
    using System.Collections.Generic;
    
    
    public partial class Vivienda
    {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vivienda()
        {
            this.Residentes = new HashSet<Residente>();
            this.DispositivoResidenteViviendas = new HashSet<DispositivoResidenteVivienda>();
            this.PermisosManuales = new HashSet<PermisoManual>();
            this.Permisos = new HashSet<Permiso>();
        }
    
        public int IdVivienda { get; set; }
        public short Manzana { get; set; }
        public short Villa { get; set; }
        public string Calle { get; set; }
        public string Telefono { get; set; }
        public bool EstaEliminada { get; set; }
        public bool EsSistema { get; set; }
        public int IdCiudadela { get; set; }
        public byte[] UpdateToken { get; set; }
    
        public virtual Ciudadela Ciudadela { get; set; }
        public virtual ICollection<Residente> Residentes { get; set; }
        public virtual ICollection<DispositivoResidenteVivienda> DispositivoResidenteViviendas { get; set; }
        public virtual ICollection<PermisoManual> PermisosManuales { get; set; }
        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
