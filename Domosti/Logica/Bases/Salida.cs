using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domosti.CapaNegocios.Bases
{
    [DataContract]
    internal class Salida
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal string respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaDispositivo
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<DispositivoJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaNotificacion
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<NotificacionJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaVillas
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<CiudadelaJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaDrv
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<DrvJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaVisitante
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<VisitanteJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaCalendario
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<CalendarioJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaPermiso
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<PermisoJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaMotivoAcceso
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<MotivoAccesoJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
    [DataContract]
    internal class SalidaUsuarioApp
    {
        [DataMember]
        internal string codigo { get; set; }
        [DataMember]
        internal List<UsuarioAppJson> respuesta { get; set; }
        [DataMember]
        internal string mensaje { get; set; }
    }
}
