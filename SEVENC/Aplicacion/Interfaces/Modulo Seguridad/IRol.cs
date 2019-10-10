using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IRol : IBase
    {
        [OperationContract]
        IEnumerable<Rol> ObtenerRoles();

        [OperationContract]
        IEnumerable<Operacion> ObtenerOperacionesPorIdRol(int idRol);

        [OperationContract]
        void CrearRol(Rol rol);

        [OperationContract]
        void ActualizarRol(Rol rol);

        [OperationContract]
        void AsignarOperacionesARol(IEnumerable<int> idsOperaciones, int idRol);
    }
}
