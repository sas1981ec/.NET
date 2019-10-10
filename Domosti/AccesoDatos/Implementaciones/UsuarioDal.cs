using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class UsuarioDal : Liberador, IUsuarioDal
    {
        public UsuarioDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return Contexto.Usuarios.Include("Ciudadela").ToList();
        }

        public void ActualizarUsuario(Usuario usuarioModificado)
        {
            Contexto.Entry(usuarioModificado).State = EntityState.Modified;
            Contexto.SaveChanges();
        }
    }
}
