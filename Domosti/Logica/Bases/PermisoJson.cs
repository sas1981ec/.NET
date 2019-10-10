using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    public class PermisoJson
    {
        [DataMember]
        internal string idPermiso { get; set; }

        [DataMember]
        internal string idVisitantes { get; set; }

        [DataMember]
        internal string fechaIni { get; set; }

        [DataMember]
        internal string fechaFin { get; set; }

        [DataMember]
        internal string idMotivo { get; set; }

        [DataMember]
        internal string estado { get; set; }

        [DataMember]
        internal string tienePermisoContinuo { get; set; }

        [DataMember]
        internal string detalleAdicional { get; set; }
    }
}
