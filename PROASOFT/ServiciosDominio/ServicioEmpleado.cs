using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioEmpleado : IEmpleado
    {
        private readonly IRepositorioEmpleado _repositorioEmpleado;

        public ServicioEmpleado(IRepositorioEmpleado repositorioEmpleado)
        {
            _repositorioEmpleado = repositorioEmpleado;
        }

        public IEnumerable<EMPLEADO> ObtenerEmpleados()
        {
            return _repositorioEmpleado.ObtenerObjetos(new FiltroEmpleado()).OrderBy(e => e.APELLIDOS).ThenBy(e => e.NOMBRES);
        }

        public void CrearEmpleado(EMPLEADO empleado)
        {
            _repositorioEmpleado.Agregar(empleado);
        }

        public void ActualizarEmpleado(EMPLEADO empleado)
        {
            var empleadoAModificar = _repositorioEmpleado.ObtenerObjetos(new FiltroEmpleadoActualizar(empleado.ID_EMPLEADO)).FirstOrDefault();
            if (empleadoAModificar == null)
                throw new ApplicationException($"Empleado de id : {empleado.ID_EMPLEADO} no existe");
            empleadoAModificar.APELLIDOS = empleado.APELLIDOS;
            empleadoAModificar.ESTA_ACTIVO = empleado.ESTA_ACTIVO;
            empleadoAModificar.IDENTIFICACION = empleado.IDENTIFICACION;
            empleadoAModificar.NOMBRES = empleado.NOMBRES;
            _repositorioEmpleado.Actualizar(empleadoAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorioEmpleado.LiberarRecursos();
        }
    }

    public class FiltroEmpleado : IFiltros<EMPLEADO>
    {
        public Expression<Func<EMPLEADO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<EMPLEADO>(e => e.ID_EMPLEADO > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroEmpleadoActualizar : Filtros<EMPLEADO>
    {
        private readonly int _id;

        internal FiltroEmpleadoActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<EMPLEADO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<EMPLEADO>(e => e.ID_EMPLEADO == _id);
            return filtro.SastifechoPor();
        }
    }
}
