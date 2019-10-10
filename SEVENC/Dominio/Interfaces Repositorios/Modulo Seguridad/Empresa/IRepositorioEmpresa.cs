using System.Collections.Generic;

namespace SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Empresa
{
    public interface IRepositorioEmpresa : IRepositorio<Entidades.Empresa>
    {
        void AsignarSucursalesAEmpresa(IEnumerable<short> idsSucursales, byte idEmpresa);
        void AsignarUsuariosAEmpresa(IEnumerable<int> idsUsuarios, byte idEmpresa);
    }
}
