using System;

namespace SevencWpf.ServicioWcf
{
    public partial class Sucursal : ICloneable
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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
