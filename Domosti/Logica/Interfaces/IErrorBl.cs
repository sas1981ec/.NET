namespace Domosti.CapaNegocios.Interfaces
{
    public interface IErrorBl
    {
        void RegistrarErrorServicio(string mensaje, string detalle);
        void RegistrarErrorWeb(string mensaje, string detalle);
    }
}
