using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    internal class VillaJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string calle { get; set; }
        [DataMember]
        internal string villa { get; set; }
        [DataMember]
        internal string manzana { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
        [DataMember]
        internal string idResidente { get; set; }
    }
}
