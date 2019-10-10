using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Empresa;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Sucursal;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioEmpresa : IEmpresa
    {
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioSucursal _repositorioSucursal;

        public ServicioEmpresa(IRepositorioEmpresa repositorioEmpresa, IRepositorioUsuario repositorioUsuario, IRepositorioSucursal repositorioSucursal)
        {
            _repositorioEmpresa = repositorioEmpresa;
            _repositorioUsuario = repositorioUsuario;
            _repositorioSucursal = repositorioSucursal;
        }

        public IEnumerable<Empresa> ObtenerEmpresas()
        {
            return _repositorioEmpresa.ObtenerObjetos(new FiltroEmpresa()).OrderBy(e => e.NombreComercial);
        }

        public IEnumerable<Sucursal> ObtenerSucursalesPorIdEmpresa(byte idEmpresa)
        {
            return _repositorioSucursal.ObtenerObjetos(new FiltroSucursalPorIdEmpresa(idEmpresa)).OrderBy(s => s.Nombre);
        }

        public IEnumerable<Usuario> ObtenerUsuariosPorIdEmpresa(byte idEmpresa)
        {
            return _repositorioUsuario.ObtenerObjetos(new FiltroUsuarioPorIdEmpresa(idEmpresa)).OrderBy(u => u.Apellidos).ThenBy(u => u.Nombres);
        }

        public void CrearEmpresa(Empresa empresa)
        {
            _repositorioEmpresa.Agregar(empresa);
        }

        public void ActualizarEmpresa(Empresa empresa)
        {
            var empresaAModificar = _repositorioEmpresa.ObtenerObjetos(new FiltroEmpresaPorId(empresa.IdEmpresa)).FirstOrDefault();
            if (empresaAModificar == null)
                throw new ApplicationException($"No existe empresa {empresa.IdEmpresa}");
            if(!empresaAModificar.Concurrencia.SequenceEqual(empresa.Concurrencia))
                throw new ApplicationException("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.");
            empresaAModificar.EstaActiva = empresa.EstaActiva;
            empresaAModificar.EstaEliminada = empresa.EstaEliminada;
            empresaAModificar.IdRepresentanteLegal = empresa.IdRepresentanteLegal;
            empresaAModificar.NombreComercial = empresa.NombreComercial;
            empresaAModificar.NombreRepresentanteLegal = empresa.NombreRepresentanteLegal;
            empresaAModificar.RazonSocial = empresa.RazonSocial;
            empresaAModificar.RUC = empresa.RUC;
            _repositorioEmpresa.Actualizar(empresaAModificar);
        }

        public void AsignarSucursalesAEmpresa(IEnumerable<short> idsSucursales, byte idEmpresa)
        {
            _repositorioEmpresa.AsignarSucursalesAEmpresa(idsSucursales, idEmpresa);
        }

        public void AsignarUsuariosAEmpresa(IEnumerable<int> idsUsuarios, byte idEmpresa)
        {
            _repositorioEmpresa.AsignarUsuariosAEmpresa(idsUsuarios, idEmpresa);
        }

        public void LiberarRecursos()
        {
            _repositorioEmpresa.LiberarRecursos();
            _repositorioSucursal.LiberarRecursos();
            _repositorioUsuario.LiberarRecursos();
        }
    }

    public class FiltroEmpresa : Filtros<Empresa>
    {
        public override Expression<Func<Empresa, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Empresa>(e => !e.EstaEliminada && e.IdEmpresa != 1);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroEmpresaPorId : Filtros<Empresa>
    {
        private byte _idEmpresa;

        public FiltroEmpresaPorId(byte idEmpresa)
        {
            _idEmpresa = idEmpresa;
        }

        public override Expression<Func<Empresa, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Empresa>(e => e.IdEmpresa == _idEmpresa);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroSucursalPorIdEmpresa : Filtros<Sucursal>
    {
        private byte _idEmpresa;

        public FiltroSucursalPorIdEmpresa(byte idEmpresa)
        {
            _idEmpresa = idEmpresa;
        }

        public override Expression<Func<Sucursal, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Sucursal>(s => !s.EstaEliminada && s.Empresas.Any(e => e.IdEmpresa == _idEmpresa));
            return filtro.SastifechoPor();
        }
    }

    public class FiltroUsuarioPorIdEmpresa : Filtros<Usuario>
    {
        private byte _idEmpresa;

        public FiltroUsuarioPorIdEmpresa(byte idEmpresa)
        {
            _idEmpresa = idEmpresa;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(u => !u.EstaEliminado && !u.EsSistema && u.Empresas.Any(e => e.IdEmpresa == _idEmpresa));
            return filtro.SastifechoPor();
        }
    }
}
