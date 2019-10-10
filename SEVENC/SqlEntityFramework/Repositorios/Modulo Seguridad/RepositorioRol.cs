using System;
using System.Collections.Generic;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol;
using SEVENC.Dominio.Entidades;
using System.Linq;

namespace SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad
{
    public class RepositorioRol : Repositorio<Rol>, IRepositorioRol
    {
        public void AsignarOperacionesARol(IEnumerable<int> idsOperaciones, int idRol)
        {
            var rol = Contexto.Roles.Include("Operaciones").FirstOrDefault(r => r.IdRol == idRol);
            if (rol == null)
                throw new ApplicationException($"No existe rol {idRol}");
            rol.Operaciones.Clear();
            var operaciones = Contexto.Operaciones.Where(o => idsOperaciones.Contains(o.IdOperacion));
            foreach (var operacion in operaciones)
                rol.Operaciones.Add(operacion);
            Contexto.SaveChanges();
        }
    }
}
