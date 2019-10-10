using System;

namespace SevencWpf.ServicioWcf
{
    public partial class Empresa : ICloneable
    {
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
