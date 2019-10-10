using System;
using System.Security.Cryptography;
using System.Text;

namespace SEVENC.Dominio.General
{
    public static class Encriptar
    {
        public static string HashPassword(string contrasena)
        {
            var data = Encoding.UTF8.GetBytes(contrasena);
            var hash = SHA512.Create().ComputeHash(data);
            var passwordHashed = Convert.ToBase64String(hash);
            var result = passwordHashed;
            return result;
        }
    }
}
