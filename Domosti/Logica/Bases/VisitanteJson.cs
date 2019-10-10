using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class VisitanteJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string tipoIdentificacion { get; set; }
        [DataMember]
        internal string identificacion { get; set; }
        [DataMember]
        internal string nombres { get; set; }
        [DataMember]
        internal string apellidos { get; set; }
        [DataMember]
        internal string email { get; set; }
        [DataMember]
        internal string telefono { get; set; }
        [DataMember]
        internal string estaEliminado { get; set; }
    }
}
