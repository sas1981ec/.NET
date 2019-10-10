using System;
using System.Linq.Expressions;

namespace SEVENC.Dominio.Dominio.Filtros
{
    public sealed class FiltroOr<T>: FiltroCompuesto<T> where T : class
    {
        private IFiltros<T> _filtroLadoDerecho = null;
        private IFiltros<T> _filtroLadoIzquierdo = null;

        public FiltroOr(IFiltros<T> ladoIzquierdo, IFiltros<T> ladoDerecho)
        {
            _filtroLadoIzquierdo = ladoIzquierdo ?? throw new ArgumentNullException("ladoIzquierdo");
            _filtroLadoDerecho = ladoDerecho ?? throw new ArgumentNullException("ladoDerecho");
        }

        public override IFiltros<T> FiltroLadoIzquierdo
        {
            get { return _filtroLadoIzquierdo; }
        }

        public override IFiltros<T> FiltroLadoDerecho
        {
            get { return _filtroLadoDerecho; }
        }

        public override Expression<Func<T, bool>> SastifechoPor()
        {
            Expression<Func<T, bool>> izquierdo = _filtroLadoIzquierdo.SastifechoPor();
            Expression<Func<T, bool>> derecho = _filtroLadoDerecho.SastifechoPor();
            return (izquierdo.Or(derecho));
        }
    }
}
