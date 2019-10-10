using System;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class ErrorBl : IErrorBl
    {
        private readonly IErrorDal _dal;
        public ErrorBl(IErrorDal dal)
        {
            _dal = dal;
        }
        public void RegistrarErrorServicio(string mensaje, string detalle)
        {
            _dal.CrearError(new Error{Tipo = "S", Fecha = DateTime.Now.ToUniversalTime().AddHours(-5),Mensaje = mensaje, Detalle = detalle});
        }

        public void RegistrarErrorWeb(string mensaje, string detalle)
        {
            _dal.CrearError(new Error { Tipo = "W", Fecha = DateTime.Now.ToUniversalTime().AddHours(-5), Mensaje = mensaje, Detalle = detalle });
        }
    }
}
