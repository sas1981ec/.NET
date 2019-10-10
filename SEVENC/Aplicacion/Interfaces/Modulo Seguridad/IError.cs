using SEVENC.Dominio.Entidades;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IError : IBase
    {
        [OperationContract]
        int CrearError(Error error);
    }
}
