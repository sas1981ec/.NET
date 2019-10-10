using System;
using System.Collections.Generic;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario;
using SEVENC.Dominio.Entidades;
using System.Linq;
using SEVENC.Dominio.Dominio.Filtros;

namespace SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public void AsignarRolesAUsuarios(IEnumerable<int> idsRoles, int idUsuario)
        {
            var usuario = Contexto.Usuarios.Include("Roles").FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                throw new ApplicationException($"No existe usuario {idUsuario}");
            usuario.Roles.Clear();
            var roles = Contexto.Roles.Where(r => idsRoles.Contains(r.IdRol));
            foreach (var rol in roles)
                usuario.Roles.Add(rol);
            Contexto.SaveChanges();
        }

        public Usuario ObtenerUsuarioParaLogin(IFiltros<Usuario> filtro)
        {
            return Contexto.Usuarios.Include("Empresas").Include("Roles.Operaciones").FirstOrDefault(filtro.SastifechoPor());
        }
    }
}
