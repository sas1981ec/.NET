using System.Linq;
using System.Collections.Generic;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Empresa;
using SEVENC.Dominio.Entidades;
using System;

namespace SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad
{
    public class RepositorioEmpresa : Repositorio<Empresa>, IRepositorioEmpresa
    {
        public void AsignarSucursalesAEmpresa(IEnumerable<short> idsSucursales, byte idEmpresa)
        {
            var empresa = Contexto.Empresas.Include("Sucursales").FirstOrDefault(e => e.IdEmpresa == idEmpresa);
            if (empresa == null)
                throw new ApplicationException($"No existe empresa {idEmpresa}");
            empresa.Sucursales.Clear();
            var sucursales = Contexto.Sucursales.Where(s => idsSucursales.Contains(s.IdSucursal));
            foreach (var sucursal in sucursales)
                empresa.Sucursales.Add(sucursal);
            Contexto.SaveChanges();
        }

        public void AsignarUsuariosAEmpresa(IEnumerable<int> idsUsuarios, byte idEmpresa)
        {
            var empresa = Contexto.Empresas.Include("Usuarios").FirstOrDefault(e => e.IdEmpresa == idEmpresa);
            if (empresa == null)
                throw new ApplicationException($"No existe empresa {idEmpresa}");
            empresa.Usuarios.Clear();
            var usuarios = Contexto.Usuarios.Where(u => idsUsuarios.Contains(u.IdUsuario));
            foreach (var usuario in usuarios)
                empresa.Usuarios.Add(usuario);
            Contexto.SaveChanges();
        }
    }
}
