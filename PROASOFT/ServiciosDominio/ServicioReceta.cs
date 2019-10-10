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
    public class ServicioReceta : IReceta
    {
        private readonly IRepositorioReceta _repositorio;

        public ServicioReceta(IRepositorioReceta repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<RECETA> ObtenerRecetas()
        {
            return _repositorio.ObtenerRecetasConItems(new FiltroReceta()).OrderBy(r => r.NOMBRE);
        }

        public void CrearReceta(RECETA receta)
        {
            _repositorio.Agregar(receta);
        }

        public void ActualizarReceta(RECETA receta)
        {
            var recetaAModificar = _repositorio.ObtenerObjetos(new FiltroRecetaPorId(receta.ID_RECETA)).FirstOrDefault();
            if (recetaAModificar == null)
                throw new ApplicationException($"No existe receta cuyo id es {receta.ID_RECETA}");
            recetaAModificar.ESTA_ACTIVA = receta.ESTA_ACTIVA;
            recetaAModificar.NOMBRE = receta.NOMBRE;
            _repositorio.Actualizar(recetaAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorio.LiberarRecursos();
        }
    }

    public class FiltroReceta : IFiltros<RECETA>
    {
        public Expression<Func<RECETA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<RECETA>(r => r.ID_RECETA > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroRecetaPorId : IFiltros<RECETA>
    {
        private readonly int _id;

        public FiltroRecetaPorId(int id)
        {
            _id = id;
        }

        public Expression<Func<RECETA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<RECETA>(r => r.ID_RECETA == _id);
            return filtro.SastifechoPor();
        }
    }
}
