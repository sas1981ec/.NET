using SEVENC.Dominio.Dominio.Interfaces_Repositorios;
using SEVENC.Infraestructura.SqlEntityFramework.Modelo;
using System;
using System.Data.Entity;
using System.Linq;

namespace SEVENC.Infraestructura.SqlEntityFramework.Repositorios
{
    public abstract class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        protected ModeloDatosContainer Contexto { get; private set; }
        private bool _disposed;

        public Repositorio()
        {
            Contexto = new ModeloDatosContainer();
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

        public System.Collections.Generic.IEnumerable<TEntity> ObtenerObjetos(Dominio.Dominio.Filtros.IFiltros<TEntity> filtro)
        {
            return (Contexto.Set<TEntity>().Where(filtro.SastifechoPor()).AsEnumerable());
        }

        public System.Collections.Generic.IEnumerable<TEntity> ObtenerObjetosPorPagineo<S>(Dominio.Dominio.Filtros.IFiltros<TEntity> filtro, int paginaIndice, int elementosPorPagina, System.Linq.Expressions.Expression<Func<TEntity, S>> expresionOrdenamiento, bool ascendente)
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
