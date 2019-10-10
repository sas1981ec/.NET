using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class UsuarioAppJson
    {
        [DataMember]
        internal string idUsuario { get; set; }
        [DataMember]
        internal string tipoIdentificacion { get; set; }
        [DataMember]
        internal string identificacion { get; set; }
        [DataMember]
        internal string nombres { get; set; }
        [DataMember]
        internal string apellidos { get; set; }
        [DataMember]
        internal string fechaNacimiento { get; set; }
    }
}
