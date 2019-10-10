using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Operacion;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioRol : IRol
    {
        private readonly IRepositorioRol _repositorioRol;
        private readonly IRepositorioOperacion _repositorioOperacion;

        public ServicioRol(IRepositorioRol repositorioRol, IRepositorioOperacion repositorioOperacion)
        {
            _repositorioRol = repositorioRol;
            _repositorioOperacion = repositorioOperacion;
        }

        public IEnumerable<Rol> ObtenerRoles()
        {
            return _repositorioRol.ObtenerObjetos(new FiltroRol()).OrderBy(r => r.Nombre);
        }

        public IEnumerable<Operacion> ObtenerOperacionesPorIdRol(int idRol)
        {
            return _repositorioOperacion.ObtenerObjetos(new FiltroOperacionPorIdRol(idRol)).OrderBy(o => o.Nombre);
        }

        public void CrearRol(Rol rol)
        {
            _repositorioRol.Agregar(rol);
        }

        public void ActualizarRol(Rol rol)
        {
            var rolAModificar = _repositorioRol.ObtenerObjetos(new FiltroRolPorId(rol.IdRol)).FirstOrDefault();
            if (rolAModificar == null)
                throw new ApplicationException($"No existe rol {rol.IdRol}");
            if (!rolAModificar.Concurrencia.SequenceEqual(rol.Concurrencia))
                throw new ApplicationException("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.");
            rolAModificar.EstaActivo = rol.EstaActivo;
            rolAModificar.Descripcion = rol.Descripcion;
            rolAModificar.EstaEliminado = rol.EstaEliminado;
            rolAModificar.Nombre = rol.Nombre;
            _repositorioRol.Actualizar(rolAModificar);
        }

        public void AsignarOperacionesARol(IEnumerable<int> idsOperaciones, int idRol)
        {
            _repositorioRol.AsignarOperacionesARol(idsOperaciones, idRol);
        }

        public void LiberarRecursos()
        {
            _repositorioRol.LiberarRecursos();
            _repositorioOperacion.LiberarRecursos();
        }
    }

    public class FiltroRol : Filtros<Rol>
    {
        public override Expression<Func<Rol, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Rol>(r => !r.EstaEliminado && !r.EsSistema);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroRolPorId : Filtros<Rol>
    {
        private int _idRol;

        public FiltroRolPorId(int idRol)
        {
            _idRol = idRol;
        }

        public override Expression<Func<Rol, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Rol>(r => r.IdRol == _idRol);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroRolPorIdUsuario : Filtros<Rol>
    {
        private int _idUsuario;

        public FiltroRolPorIdUsuario(int idUsuario)
        {
            _idUsuario = idUsuario;
        }

        public override Expression<Func<Rol, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Rol>(r => !r.EstaEliminado && r.Usuarios.Any(u => u.IdUsuario == _idUsuario));
            return filtro.SastifechoPor();
        }
    }

    public class FiltroOperacionPorIdRol : Filtros<Operacion>
    {
        private int _idRol;

        public FiltroOperacionPorIdRol(int idRol)
        {
            _idRol = idRol;
        }

        public override Expression<Func<Operacion, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Operacion>(o => o.Roles.Any(r => r.IdRol == _idRol));
            return filtro.SastifechoPor();
        }
    }
}
