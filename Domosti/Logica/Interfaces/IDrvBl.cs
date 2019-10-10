using System.IO;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IDrvBl
    {
        Stream ObtenerDrv(string idDispositivo, string idResidente, string idVilla);
        Stream CrearDrv(string idDispositivo, string idResidente, string idVilla);
        Stream ReactivarDrv(string idDispositivo, string idResidente, string idVilla);
        void EnviarNotificacionesMasivas(string texto, int idCiudadela);
    }
}
