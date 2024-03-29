//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEVENC.Dominio.Entidades
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    
    [DataContract(IsReference=true)]
    public partial class Sucursal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sucursal()
        {
            this.Empresas = new HashSet<Empresa>();
        }
    
    
        [DataMember]
        public short IdSucursal { get; set; }
    
        [DataMember]
        public string Nombre { get; set; }
    
        [DataMember]
        public string Direccion { get; set; }
    
        [DataMember]
        public bool EstaActiva { get; set; }
    
        [DataMember]
        public bool EstaEliminada { get; set; }
    
        [DataMember]
        public byte[] Concurrencia { get; set; }
    
        [DataMember]
        public bool EsMatriz { get; set; }
    
        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
