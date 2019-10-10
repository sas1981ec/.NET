using System.Runtime.Serialization;

namespace FacturacionElectronica
{
    [DataContract]
    public class Respuesta
    {
        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string Dato { get; set; }

        [DataMember]
        public bool FueOk { get; set; }
    }

    [DataContract]
    public class RespuestaAutorizacion
    {
        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public object[] Dato { get; set; }

        [DataMember]
        public bool FueOk { get; set; }
    }
}