using SEVENC.Dominio.General;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace SEVENC.ServiciosDistribuidos.Servicios.Validador
{
    public class ValidadorUsuarioClave : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != Encriptar.HashPassword("EraDigital"))
                throw new SecurityTokenException("Usuario y/o contraseña Incorrecto");
            if (password != Encriptar.HashPassword("M@ch1n3L3@rn1ng"))
                throw new SecurityTokenException("Usuario y/o contraseña Incorrecto");
        }
    }
}