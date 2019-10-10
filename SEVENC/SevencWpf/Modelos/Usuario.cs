using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SevencWpf.ServicioWcf
{
    public partial class Usuario : ICloneable
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

        public string NombreCompleto
        {
            get
            {
                return $"{Apellidos} {Nombres}";
            }
        }

        public BitmapImage FotoImagen
        {
            get
            {
                if (Foto == null || Foto.Count() == 1)
                {
                    var imagen = new BitmapImage(new Uri(@"/Recursos/Imagenes/usuario.png", UriKind.Relative));
                    return imagen;
                }
                using (var stream = new MemoryStream(Foto))
                {
                    var imagen = new BitmapImage();
                    imagen.BeginInit();
                    imagen.CacheOption = BitmapCacheOption.OnLoad;
                    imagen.StreamSource = stream;
                    imagen.EndInit();
                    return imagen;
                }
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
