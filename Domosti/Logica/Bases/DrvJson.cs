using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class DrvJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string nombre { get; set; }
        [DataMember]
        internal string estado { get; set; }
    }
}
