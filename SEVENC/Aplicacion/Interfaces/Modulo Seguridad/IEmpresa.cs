using SEVENC.Dominio.Entidades;
using System.Collections.Generic;
using System.ServiceModel;

namespace SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad
{
    [ServiceContract]
    public interface IEmpresa : IBase
    {
        [OperationContract]
        IEnumerable<Empresa> ObtenerEmpresas();

        [OperationContract]
        void CrearEmpresa(Empresa empresa);

        [OperationContract]
        void ActualizarEmpresa(Empresa empresa);

        [OperationContract]
        IEnumerable<Sucursal> ObtenerSucursalesPorIdEmpresa(byte idEmpresa);

        [OperationContract]
        IEnumerable<Usuario> ObtenerUsuariosPorIdEmpresa(byte idEmpresa);

        [OperationContract]
        void AsignarSucursalesAEmpresa(IEnumerable<short> idsSucursales, byte idEmpresa);

        [OperationContract]
        void AsignarUsuariosAEmpresa(IEnumerable<int> idsUsuarios, byte idEmpresa);
    }
}
