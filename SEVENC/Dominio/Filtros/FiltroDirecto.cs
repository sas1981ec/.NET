using System;
using System.Linq.Expressions;

namespace SEVENC.Dominio.Dominio.Filtros
{
    public sealed class FiltroDirecto<TEntity> : Filtros<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> _criterioComparacion;

        public FiltroDirecto(Expression<Func<TEntity, bool>> criterioComparacion)
        {
            _criterioComparacion = criterioComparacion ?? throw new ArgumentNullException("criterioComparacion");
        }

        public override Expression<Func<TEntity, bool>> SastifechoPor()
        {
            return _criterioComparacion;
        }
    }
}
