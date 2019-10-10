using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaInfraestructura.SqlEntityFramework.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios
{
    public abstract class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        protected ModeloProasoftContainer Contexto { get; private set; }
        private bool _disposed;

        public Repositorio()
        {
            Contexto = new ModeloProasoftContainer();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                Contexto.Dispose();
            _disposed = true;
        }

        public void Agregar(TEntity objeto)
        {
            Contexto.Entry(objeto).State = EntityState.Added;
            Contexto.SaveChanges();
        }

        public void Actualizar(TEntity objeto)
        {
            Contexto.Entry(objeto).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public IEnumerable<TEntity> ObtenerObjetos(IFiltros<TEntity> filtro)
        {
            return (Contexto.Set<TEntity>().Where(filtro.SastifechoPor()).AsEnumerable());
        }

        public IEnumerable<TEntity> ObtenerObjetosPorPagineo<S>(IFiltros<TEntity> filtro, int paginaIndice, int elementosPorPagina, System.Linq.Expressions.Expression<Func<TEntity, S>> expresionOrdenamiento, bool ascendente)
        {
            return ascendente ? (Contexto.Set<TEntity>().Where(filtro.SastifechoPor()).OrderBy(expresionOrdenamiento).Skip(paginaIndice * elementosPorPagina).Take(elementosPorPagina).AsEnumerable()) :
                                (Contexto.Set<TEntity>().Where(filtro.SastifechoPor()).OrderByDescending(expresionOrdenamiento).Skip(paginaIndice * elementosPorPagina).Take(elementosPorPagina).AsEnumerable());
        }

        public void LiberarRecursos()
        {
            Dispose();
        }
    }
}
