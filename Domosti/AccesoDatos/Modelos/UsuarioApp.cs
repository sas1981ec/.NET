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
    
    
    public partial class UsuarioApp : Persona
    {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsuarioApp()
        {
            this.Residentes = new HashSet<Residente>();
            this.Dispositivos = new HashSet<Dispositivo>();
        }
    
        public System.DateTime FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
    
        public virtual ICollection<Residente> Residentes { get; set; }
        public virtual ICollection<Dispositivo> Dispositivos { get; set; }
    }
}
