using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class MotivoAccesoJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string nombre { get; set; }
    }
}
