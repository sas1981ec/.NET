using System;

namespace SevencWpf.ServicioWcf
{
    public partial class Operacion : ICloneable
    {
        public bool EstaChequeada { get; set; }

        public string ImagenEstado
        {
            get
            {
                return EstaActiva ? "/Recursos/Imagenes/enabled.png" : "/Recursos/Imagenes/disabled.png";
            }
        }

        public string ToolTipEstado
        {
            get
            {
                return EstaActiva ? "Activa" : "No Activa";
            }
        }

        public string ImagenAuditable
        {
            get
            {
                return EsAuditable ? "/Recursos/Imagenes/enabled.png" : "/Recursos/Imagenes/disabled.png";
            }
        }

        public string ToolTipAuditable
        {
            get
            {
                return EsAuditable ? "Auditada" : "No Auditada";
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
