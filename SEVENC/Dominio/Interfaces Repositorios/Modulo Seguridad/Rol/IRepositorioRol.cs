using System.Collections.Generic;

namespace SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol
{
    public interface IRepositorioRol : IRepositorio<Entidades.Rol>
    {
        void AsignarOperacionesARol(IEnumerable<int> idsOperaciones, int idRol);
    }
}
