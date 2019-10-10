using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Domosti.CapaNegocios.Bases
{
    public enum EnumeracionBusquedaPermisos
    {
        PorIdentificacionVisitante,
        PorNombreVisitante,
        PorIdentificacionResidente,
        PorNombreResidente
    }
    public enum EnumeracionBusquedaAcceso
    {
        PorIdentificacionVisitante,
        PorNombreVisitante,
        PorIdentificacionResidente,
        PorNombreResidente,
        PorManzana,
        PorVilla,
        PorFecha,
        PorMotivo
    }
    public class ClaseBase
    {
        protected string HashPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            var hash = SHA512.Create().ComputeHash(data);
            var passwordHashed = Convert.ToBase64String(hash);
            var result = passwordHashed;
            return result;
        }
        protected void EnviarNotificacion(string deviceId, string mensaje, string titulo)
        {
            var tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AIzaSyAYu_8AgRtZVmRP2GD0iGiQ4Ax5c2IPYDc"));
            tRequest.Headers.Add(string.Format("Sender: id={0}", "jamcoa-3897b"));
            tRequest.ContentType = "application/json";
            var data = new
            {
                to = deviceId,
                notification = new
                {
                    body = mensaje,
                    title = titulo
                }
            };
            Envio(data, tRequest);
        }

        private void Envio(object data, WebRequest tRequest)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            var byteArray = Encoding.UTF8.GetBytes(json);
            using (var dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (var tResponse = tRequest.GetResponse())
                {
                    using (var dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse == null) return;
                        var tReader = new StreamReader(dataStreamResponse);
                        tReader.ReadToEnd();
                    }
                }
            }
        }

        protected Stream ObtenerSalida(string codigo, string respuesta, string mensaje)
        {
            var salida = new Salida { codigo = codigo, respuesta = respuesta, mensaje = mensaje };
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Salida));
            ser.WriteObject(stream, salida);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            return EnviarStream(sr.ReadToEnd());
        }
        protected Stream ObtenerSalidaDispositivo(List<DispositivoJson> respuesta, string mensaje)
        {
            var salida = new SalidaDispositivo { codigo = "2300", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaNotificacion(List<NotificacionJson> respuesta, string mensaje)
        {
            var salida = new SalidaNotificacion { codigo = "2400", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaCalendario(List<CalendarioJson> respuesta, string mensaje)
        {
            var salida = new SalidaCalendario { codigo = "1200", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaPermiso(List<PermisoJson> respuesta, string mensaje)
        {
            var salida = new SalidaPermiso { codigo = "1300", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaVisitante(List<VisitanteJson> respuesta, string mensaje)
        {
            var salida = new SalidaVisitante { codigo = "800", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaMotivoAcceso(List<MotivoAccesoJson> respuesta, string mensaje)
        {
            var salida = new SalidaMotivoAcceso { codigo = "2200", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaVilla(List<CiudadelaJson> respuesta, string mensaje)
        {
            var salida = new SalidaVillas { codigo = "300", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        protected Stream ObtenerSalidaUsuarioApp(List<UsuarioAppJson> respuesta, string mensaje)
        {
            var salida = new SalidaUsuarioApp { codigo = "2100", respuesta = respuesta, mensaje = mensaje };
            var json = JsonConvert.SerializeObject(salida);
            return EnviarStream(json);
        }
        private Stream EnviarStream(string json)
        {
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
    }
}
