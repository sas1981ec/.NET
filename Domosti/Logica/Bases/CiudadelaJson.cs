using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class CiudadelaJson
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string nombre { get; set; }
        [DataMember]
        internal string tipo { get; set; }
        [DataMember]
        internal List<VillaJson> villas { get; set; }
    }
}
