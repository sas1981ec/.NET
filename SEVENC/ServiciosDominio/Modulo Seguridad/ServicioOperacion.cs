using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Operacion;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioOperacion : IOperacion
    {
        private readonly IRepositorioOperacion _repositorioOperacion;

        public ServicioOperacion(IRepositorioOperacion repositorioOperacion)
        {
            _repositorioOperacion = repositorioOperacion;
        }

        public IEnumerable<Operacion> ObtenerOperaciones()
        {
            return _repositorioOperacion.ObtenerObjetos(new FiltroOperacion()).OrderBy(s => s.Nombre);
        }

        public void ActualizarOperacion(Operacion operacion)
        {
            var operacionAModificar = _repositorioOperacion.ObtenerObjetos(new FiltroOperacionPorId(operacion.IdOperacion)).FirstOrDefault();
            if (operacionAModificar == null)
                throw new ApplicationException($"No existe operacion {operacion.IdOperacion}");
            operacionAModificar.EstaActiva = operacion.EstaActiva;
            operacionAModificar.EsAuditable = operacion.EsAuditable;
            _repositorioOperacion.Actualizar(operacionAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorioOperacion.LiberarRecursos();
        }
    }

    public class FiltroOperacion : Filtros<Operacion>
    {
        public override Expression<Func<Operacion, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Operacion>(o => o.IdOperacion > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroOperacionPorId : Filtros<Operacion>
    {
        private int _idOperacion;

        public FiltroOperacionPorId(int idOperacion)
        {
            _idOperacion = idOperacion;
        }

        public override Expression<Func<Operacion, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Operacion>(o => o.IdOperacion == _idOperacion);
            return filtro.SastifechoPor();
        }
    }
}
