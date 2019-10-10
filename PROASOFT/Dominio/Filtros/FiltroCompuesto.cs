namespace PROASOFT.CapaDominio.Dominio.Filtros
{
    public abstract class FiltroCompuesto<TEntity> : Filtros<TEntity>  where TEntity : class
    {
        public abstract IFiltros<TEntity> FiltroLadoIzquierdo { get; }
        public abstract IFiltros<TEntity> FiltroLadoDerecho { get; }
    }
}
