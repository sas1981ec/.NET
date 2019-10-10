using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using FacturacionElectronica.Proxys.Autorizacion;
using FacturacionElectronica.Proxys.Recepcion;
using FirmaXadesNet;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Signature;
using FirmaXadesNet.Signature.Parameters;

namespace FacturacionElectronica
{
    public class Servicio : IServicio
    {
        public Respuesta Firmar(string xml, string nombreCertificado, string claveCertificado)
        {
            try
            {
                var xadesService = new XadesService();
                SignatureDocument signatureDocument;
                var parametros = ObtenerParametrosFirma();
                using (parametros.Signer = new Signer(ObtenerCertificado(nombreCertificado, claveCertificado)))
                {
                    signatureDocument = xadesService.Sign(ConvertirXmlAStream(xml), parametros);
                }

                return new Respuesta
                {
                    FueOk = true,
                    Mensaje = "Firmado OK.",
                    Dato = signatureDocument.Document.OuterXml
                };
            }
            catch (Exception ex)
            {
                return new Respuesta { Mensaje = string.Format("Mensaje : {0}, Excepción Interna: {1}, Seguimiento de Pila : {2}", ex.Message, ex.InnerException, ex.StackTrace) };
            }
        }

        private SignatureParameters ObtenerParametrosFirma()
        {
            var parametros = new SignatureParameters
            {
                SignatureMethod = SignatureMethod.RSAwithSHA1,
                SigningDate = DateTime.Now,
                SignaturePackaging = SignaturePackaging.ENVELOPED
            };
            return parametros;
        }
        private static X509Certificate2 ObtenerCertificado(string nombreCertificado, string claveCertificado)
        {
            var cert = new X509Certificate2(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + nombreCertificado, claveCertificado,
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
            return cert;
        }

        private static MemoryStream ConvertirXmlAStream(string xml)
        {
            var documentoXml = new XmlDocument();
            documentoXml.LoadXml(xml);
            var xmlStream = new MemoryStream();
            documentoXml.Save(xmlStream);
            xmlStream.Flush();
            xmlStream.Position = 0;
            return xmlStream;
        }

        public Respuesta EnviarSri(string xmlFirmado)
        {
            try
            {
                var servicioRecepcion = new RecepcionComprobantesService();
                var respuesta = servicioRecepcion.validarComprobante(Encoding.UTF8.GetBytes(xmlFirmado));
                var mensajeRespuesta = (((XmlNode[]) (((object[]) (respuesta[0]))[0]))[0]).InnerText;
                return new Respuesta
                {
                    FueOk = mensajeRespuesta == "RECIBIDA",
                    Mensaje = mensajeRespuesta == "RECIBIDA" ? "Enviado Ok." :  "",
                    Dato = mensajeRespuesta
                };
            }
            catch (TimeoutException e)
            {
                return new Respuesta { Mensaje = "TimeOut al esperar respuesta del SRI." };
            }
            catch (Exception ex)
            {
                return new Respuesta { Mensaje = string.Format("Mensaje : {0}, Excepción Interna: {1}, Seguimiento de Pila : {2}", ex.Message, ex.InnerException, ex.StackTrace) };
            }
        }

        public RespuestaAutorizacion AutorizarSri(string claveAcceso)
        {
            try
            {
                var servicio = new AutorizacionComprobantesService();
                var respuesta = servicio.autorizacionComprobante(claveAcceso);
                var mensajeRespuesta = (((XmlNode[]) (((object[]) (respuesta[0]))[0]))[2]).InnerText;
                return new RespuestaAutorizacion
                {
                    FueOk = !mensajeRespuesta.Contains("NO AUTORIZADO"),
                    Mensaje = mensajeRespuesta.Contains("NO AUTORIZADO") ? "No Autorizada." : "Autorización OK.",
                    Dato = respuesta
                };
            }
            catch (TimeoutException e)
            {
                return new RespuestaAutorizacion { Mensaje = "TimeOut al esperar respuesta del SRI." };
            }
            catch (Exception ex)
            {
                return new RespuestaAutorizacion { Mensaje = string.Format("Mensaje : {0}, Excepción Interna: {1}, Seguimiento de Pila : {2}", ex.Message, ex.InnerException, ex.StackTrace) };
            }
        }
    }
}
