using System;
using System.Linq;
using System.Linq.Expressions;

namespace PROASOFT.CapaDominio.Dominio.Filtros
{
    public sealed class FiltroNot<TEntity> : Filtros<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> _criterioOriginal;

        public FiltroNot(IFiltros<TEntity> criterioOriginal)
        {
            if (criterioOriginal == null)
                throw new ArgumentNullException("criterioOriginal");

            _criterioOriginal = criterioOriginal.SastifechoPor();
        }

        public FiltroNot(Expression<Func<TEntity, bool>> filtroOriginal)
        {
            _criterioOriginal = filtroOriginal ?? throw new ArgumentNullException("filtroOriginal");
        }

        public override Expression<Func<TEntity, bool>> SastifechoPor()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_criterioOriginal.Body), _criterioOriginal.Parameters.Single());
        }
    }
}
