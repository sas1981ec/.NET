using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaNegocios.Contratos
{
    public class LoginData
    {
        public Usuario Usuario { get; set; }
        public string Mensaje { get; set; }
        public bool EstaAutenticado { get; set; }
    }
}
