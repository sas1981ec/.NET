using Spring.Aop;
using System;
using System.IO;
using System.ServiceModel;

namespace SEVENC.ServiciosDistribuidos.Servicios.Aspectos
{
    public class ThrowsExceptionParaLogError : IThrowsAdvice
    {
        public void AfterThrowing(Exception ex)
        {
            var mensaje = $"Mensaje : {ex.Message}///Excepción Interna : {ex.InnerException}///Pila de Seguimiento : {ex.StackTrace}///Fuente : {ex.Source}///Link : {ex.HelpLink}";
            LoguearError(mensaje);
        }

        internal static void LoguearError(string mensaje)
        {
            try
            {
                var ruta = Path.Combine("Log", "Log");
                var sw = new StreamWriter(ruta, true);
                sw.WriteLine($"Fecha: {DateTime.Now}. {mensaje}");
                sw.Close();
            }
            catch (Exception)
            {
                throw new FaultException("Ha ocurrido un inconveniente y no se pudo loguearlo.\nContactarse con el Departamento de IT.");
            }
            throw new FaultException("Ha ocurrido un inconveniente.\nContactarse con el Departamento de IT.");
        }
    }
}