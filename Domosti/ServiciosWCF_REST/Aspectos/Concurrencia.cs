using System;
using System.Data;
using System.ServiceModel;
using Domosti.CapaNegocios.Interfaces;
using Spring.Aop;
using Spring.Context.Support;

namespace Domosti.CapaServicios.Aspectos
{
    public class Concurrencia : IThrowsAdvice
    {
        public void AfterThrowing(OptimisticConcurrencyException ex)
        {
            var ctx = new XmlApplicationContext("~/Springs/ErrorSpring.xml");
            var errorAdmin = (IErrorBl)ctx["ErrorAdmin"];
            errorAdmin.RegistrarErrorServicio(ex.Message, string.Format("MENSAJE :{0}///EXCEPCION INTERNA : {1}///DATA : {2} ///FUENTE : {3}///SITIO_DESTINO : {4}///SEGUIMIENTO DE PILA : {5}", ex.Message, ex.InnerException, ex.Data, ex.Source, ex.TargetSite, ex.StackTrace));
            throw new FaultException<Exception>(ex, new FaultReason("Los datos que desea modificar han cambiado. Por favor regrese a la vista principal de datos e intente nuevamente."));
        }
    }
}