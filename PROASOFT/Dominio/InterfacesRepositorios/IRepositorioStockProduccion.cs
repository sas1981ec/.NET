namespace PROASOFT.CapaDominio.Dominio.InterfacesRepositorios
{
    public interface IRepositorioStockProduccion : IRepositorio<Entidades.STOCK_PRODUCCION>
    {
        bool CrearStockProduccion(Entidades.STOCK_PRODUCCION stockProduccion);
    }
}
