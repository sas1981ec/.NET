using System;
using System.Linq.Expressions;

namespace SEVENC.Dominio.Dominio.Filtros
{
    public abstract class Filtros<TEntity> : IFiltros<TEntity>  where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> SastifechoPor();


        public static Filtros<TEntity> operator &(Filtros<TEntity> leftSideSpecification, Filtros<TEntity> rightSideSpecification)
        {
            return new FiltroAnd<TEntity>(leftSideSpecification, rightSideSpecification);
        }

        public static Filtros<TEntity> operator |(Filtros<TEntity> leftSideSpecification, Filtros<TEntity> rightSideSpecification)
        {
            return new FiltroOr<TEntity>(leftSideSpecification, rightSideSpecification);
        }

        public static Filtros<TEntity> operator !(Filtros<TEntity> specification)
        {
            return new FiltroNot<TEntity>(specification);
        }

        public static bool operator false(Filtros<TEntity> specification)
        {
            return false;
        }

        public static bool operator true(Filtros<TEntity> specification)
        {
            return true;
        }
    }
}
