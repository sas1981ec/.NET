using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SEVENC.Aplicacion.Aplicacion.Contratos.Modulo_Seguridad
{
    [DataContract]
    public class LoginData
    {
        [DataMember]
        public bool FueOk { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string NombreUsuario { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public long IdSesion { get; set; }

        [DataMember]
        public IEnumerable<Empresa> Empresas { get; set; }

        [DataMember]
        public Empresa EmpresaSeleccionada { get; set; }

        [DataMember]
        public Dictionary<int, bool> Operaciones { get; set; }
    }
}
