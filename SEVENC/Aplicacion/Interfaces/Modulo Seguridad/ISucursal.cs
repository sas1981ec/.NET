using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface ISucursal : IBase
    {
        [OperationContract]
        IEnumerable<Sucursal> ObtenerSucursales();

        [OperationContract]
        void CrearSucursal(Sucursal sucursal);

        [OperationContract]
        void ActualizarSucursal(Sucursal sucursal);
    }
}
