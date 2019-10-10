using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Contratos
{
    public class LoginData
    {
        public bool FueOk { get; set; }

        public string Mensaje { get; set; }

        public string UserName { get; set; }

        public string NombreUsuario { get; set; }

        public short IdUsuario { get; set; }

        public IEnumerable<short> Roles { get; set; }
    }
}
