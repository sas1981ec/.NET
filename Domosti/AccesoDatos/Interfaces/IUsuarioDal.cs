using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IUsuarioDal
    {
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(Usuario usuarioModificado);
    }
}
