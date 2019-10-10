using System.Collections.Generic;
using System.IO;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaNegocios.Interfaces
{
    public interface IResidenteBl
    {
        IEnumerable<Residente> ObtenerResidentes(int idCiudadela);
        IEnumerable<Residente> ObtenerResidentesPorNombre(int idCiudadela,string nombreResidente);
        Residente ObtenerResidentePorId(int residenteId);
        Stream ObtenerNotificaciones(int idResidente);
        Stream ObtenerVillas(string email);
        void CrearResidente(Residente residenteNuevo, int ciudadelaId);
        void ActualizarResidente(Residente residenteModificado, FotoResidente fotoResidenteModificada);
        void AsignarViviendasAResidente(int idResidente, IEnumerable<int> idViviendas);
        void EliminarResidente(Residente residenteEliminado);
        bool ExisteIdentificacion(string tipo, string identificacion, int idCiudadela);
        bool ExisteIdentificacion(string tipo, string identificacion,long idResidente, int idCiudadela);
        FotoResidente ObtenerFotoResidente(long idResidente);
    }
}
