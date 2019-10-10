using System;
using Domosti.CapaDatos.Bases;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Implementaciones
{
    public class ErrorDal : Liberador, IErrorDal
    {
        public ErrorDal()
        {
            Contexto = new ModeloDomostiContainer();
        }

        public void CrearError(Error error)
        {
            try
            {
                Contexto.Errores.Add(error);
                Contexto.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
