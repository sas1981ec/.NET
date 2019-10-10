using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios
{
    [ServiceContract]
    public interface IServicioWcf : IEmpresa, IError, ISucursal, IUsuario, IRol, IOperacion, IAuditoria
    {
    }
}
