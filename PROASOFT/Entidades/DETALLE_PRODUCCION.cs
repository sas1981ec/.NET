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
    
    public partial class DETALLE_PRODUCCION
    {
        public long ID { get; set; }
        public short CANTIDAD { get; set; }
        public int ID_PRODUCCION { get; set; }
        public int ID_RECETA { get; set; }
        public int ID_EMPLEADO { get; set; }
    
        public virtual PRODUCCION PRODUCCION { get; set; }
        public virtual RECETA RECETA { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
    }
}