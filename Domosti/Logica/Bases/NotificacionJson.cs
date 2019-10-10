using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class NotificacionJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string tipo { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
}
