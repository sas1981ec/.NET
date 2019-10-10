using System;

namespace SevencWpf.ServicioWcf
{
    public partial class Rol : ICloneable
    {
        public bool EstaChequeado { get; set; }

        public string ImagenEstado
        {
            get
            {
                return EstaActivo ? "/Recursos/Imagenes/enabled.png" : "/Recursos/Imagenes/disabled.png";
            }
        }

        public string ToolTipEstado
        {
            get
            {
                return EstaActivo ? "Activo" : "No Activo";
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
