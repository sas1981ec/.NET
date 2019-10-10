using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IOperacion : IBase
    {
        [OperationContract]
        IEnumerable<Operacion> ObtenerOperaciones();

        [OperationContract]
        void ActualizarOperacion(Operacion operacion);
    }
}
