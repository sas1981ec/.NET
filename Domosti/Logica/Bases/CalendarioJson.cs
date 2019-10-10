using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class CalendarioJson
    {
        [DataMember]
        internal string d { get; set; }
        [DataMember]
        internal string n { get; set; }
    }
}
