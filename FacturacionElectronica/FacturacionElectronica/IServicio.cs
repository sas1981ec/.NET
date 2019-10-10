using System.ServiceModel;

namespace FacturacionElectronica
{
    [ServiceContract]
    public interface IServicio
    {
        [OperationContract]
        Respuesta Firmar(string xml,string nombreCertificado, string claveCertificado);

        [OperationContract]
        Respuesta EnviarSri(string xmlFirmado);

        [OperationContract]
        RespuestaAutorizacion AutorizarSri(string claveAcceso);
    }
}
