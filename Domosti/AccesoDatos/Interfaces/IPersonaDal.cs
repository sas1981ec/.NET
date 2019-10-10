using System.Collections.Generic;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Interfaces
{
    public interface IPersonaDal
    {
        IEnumerable<UsuarioApp> ObtenerUsuariosApp();
        void CrearUsuarioApp(UsuarioApp usuarioAppNuevo);
        void ActualizarUsuarioApp(UsuarioApp usuarioAppModificado);
        IEnumerable<Visitante> ObtenerVisitantes();
        void CrearVisitante(Visitante visitanteNuevo);
        void ActualizarVisitante(Visitante visitanteModificado);
        IEnumerable<Residente> ObtenerResidentes();
        void ActualizarResidente(Residente residenteModificado);
    }
}
