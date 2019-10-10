using System;
using System.Linq.Expressions;

namespace SEVENC.Dominio.Dominio.Filtros
{
    public interface IFiltros<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> SastifechoPor();
    }
}
