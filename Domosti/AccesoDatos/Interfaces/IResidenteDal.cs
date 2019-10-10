using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IResidenteDal
    {
        IEnumerable<Residente> ObtenerResidentesConViviendasYVisitantes();
        IEnumerable<Residente> ObtenerResidentesConViviendasYCiudadelas();
        Residente ObtenerResidenteConNotificaciones(int idResidente);
        void CrearResidente(Residente residenteNuevo);
        void ActualizarResidente(Residente residenteModificado);
        void AsignarViviendasAResidente(int idResidente, IEnumerable<int> idViviendas);
        void ActualizarFotoResidente(FotoResidente fotoResidenteModificada);
        FotoResidente ObtenerFotoResidente(long idResidente);
    }
}
