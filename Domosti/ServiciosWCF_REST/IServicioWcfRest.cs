using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Domosti.CapaServicios
{
    [ServiceContract]
    public interface IServicioWcfRest
    {
        #region Residentes
        [WebInvoke(UriTemplate = "/ObtenerVillas", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerVillas(string email);

        [WebInvoke(UriTemplate = "/ObtenerNotificaciones", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerNotificaciones(int idResidente);
        #endregion

        #region Dispositivos
        [WebInvoke(UriTemplate = "/ObtenerDispositivos", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerDispositivos(string idResidente);

        [WebInvoke(UriTemplate = "/InactivarDispositivo", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream InactivarDispositivo(string idDispositivo, string idResidente, string idVilla);
        #endregion

        #region Persona
        [WebInvoke(UriTemplate = "/Login", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream Login(string email, string clave, string idDevice, string imei, string iccid);

        [WebInvoke(UriTemplate = "/RestaurarContrasena", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream RestaurarContrasena(string email);

        [WebInvoke(UriTemplate = "/AsociarDispositivo", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream AsociarDispositivo(string email, string nombreDispositivo, string idDevice, string imei, string iccid, string token);

        [WebInvoke(UriTemplate = "/ActualizarToken", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ActualizarToken(string idDispositivo, string token);

        [WebInvoke(UriTemplate = "/ObtenerUsuarioApp", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerUsuarioApp(string idDispositivo);

        [WebInvoke(UriTemplate = "/CrearUsuarioApp", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream CrearUsuarioApp(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string contrasena, string fechaNacimiento);

        [WebInvoke(UriTemplate = "/ActualizarUsuarioApp", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ActualizarUsuarioApp(string idResidente, string nombres, string apellidos, string fechaNacimiento);

        [WebInvoke(UriTemplate = "/CambiarContrasena", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream CambiarContrasena(string idResidente, string contrasenaVieja, string contrasenaNueva);

        [WebInvoke(UriTemplate = "/ObtenerVisitantes", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerVisitantes(string idResidente);

        [WebInvoke(UriTemplate = "/CrearVisitante", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream CrearVisitante(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono, string idResidente);

        [WebInvoke(UriTemplate = "/ActualizarVisitante", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ActualizarVisitante(string idVisitante, string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono);

        [WebInvoke(UriTemplate = "/EliminarVisitante", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream EliminarVisitante(string idVisitante);
        #endregion

        #region DRV
        [WebInvoke(UriTemplate = "/ObtenerDrv", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerDrv(string idDispositivo, string idResidente, string idVilla);

        [WebInvoke(UriTemplate = "/CrearDrv", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream CrearDrv(string idDispositivo, string idResidente, string idVilla);

        [WebInvoke(UriTemplate = "/ReactivarDrv", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ReactivarDrv(string idDispositivo, string idResidente, string idVilla);
        #endregion

        #region Permiso
        [WebInvoke(UriTemplate = "/ObtenerPermisosPorMesYAnio", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerPermisosPorMesYAnio(string idResidente, string mes, string anio, string estados, string idVisitante);

        [WebInvoke(UriTemplate = "/ObtenerPermisosPorDia", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerPermisosPorDia(string idResidente, string dia, string mes, string anio, string estados, string idVisitante);

        [WebInvoke(UriTemplate = "/CrearPermiso", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream CrearPermiso(string fechaIni, string fechaFin, string idResidente, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes, string idVivienda);

        [WebInvoke(UriTemplate = "/ActualizarPermiso", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ActualizarPermiso(string idPermiso, string fechaIni, string fechaFin, string idMotivo, string tienePermisoContinuo, string idDispositivo, string detalleAdicional, string idVisitantes);

        [WebInvoke(UriTemplate = "/EliminarPermiso", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream EliminarPermiso(string idPermiso);

        [WebInvoke(UriTemplate = "/ObtenerMotivosAcceso", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        Stream ObtenerMotivosAcceso();
        #endregion
    }
}
