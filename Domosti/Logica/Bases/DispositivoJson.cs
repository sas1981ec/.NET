using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class DispositivoJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string nombre { get; set; }
    }
}
