using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Sucursal;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioSucursal : ISucursal
    {
        private readonly IRepositorioSucursal _repositorioSucursal;

        public ServicioSucursal(IRepositorioSucursal repositorioSucursal)
        {
            _repositorioSucursal = repositorioSucursal;
        }

        public IEnumerable<Sucursal> ObtenerSucursales()
        {
            return _repositorioSucursal.ObtenerObjetos(new FiltroSucursal()).OrderBy(s => s.Nombre);
        }

        public void CrearSucursal(Sucursal sucursal)
        {
            _repositorioSucursal.Agregar(sucursal);
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            var sucursalAModificar = _repositorioSucursal.ObtenerObjetos(new FiltroSucursalPorId(sucursal.IdSucursal)).FirstOrDefault();
            if (sucursalAModificar == null)
                throw new ApplicationException($"No existe sucursal {sucursal.IdSucursal}");
            if (!sucursalAModificar.Concurrencia.SequenceEqual(sucursal.Concurrencia))
                throw new ApplicationException("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.");
            sucursalAModificar.EstaActiva = sucursal.EstaActiva;
            sucursalAModificar.EstaEliminada = sucursal.EstaEliminada;
            sucursalAModificar.Direccion = sucursal.Direccion;
            sucursalAModificar.EsMatriz = sucursal.EsMatriz;
            sucursalAModificar.Nombre = sucursal.Nombre;
            _repositorioSucursal.Actualizar(sucursalAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorioSucursal.LiberarRecursos();
        }
    }

    public class FiltroSucursal : Filtros<Sucursal>
    {
        public override Expression<Func<Sucursal, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Sucursal>(s => !s.EstaEliminada);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroSucursalPorId : Filtros<Sucursal>
    {
        private int _idSucursal;

        public FiltroSucursalPorId(int idSucursal)
        {
            _idSucursal = idSucursal;
        }

        public override Expression<Func<Sucursal, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Sucursal>(s => s.IdSucursal == _idSucursal);
            return filtro.SastifechoPor();
        }
    }
}
