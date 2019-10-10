using SEVENC.Dominio.Entidades;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IAuditoria : IBase
    {
        [OperationContract]
        void CrearAuditoria(Auditoria auditoria);
    }
}
