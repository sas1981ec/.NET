using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Error;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioError : IError
    {
        private readonly IRepositorioError _repositorioError;

        public ServicioError(IRepositorioError repositorioError)
        {
            _repositorioError = repositorioError;
        }

        public int CrearError(Error error)
        {
            _repositorioError.Agregar(error);
            return error.IdError;
        }

        public void LiberarRecursos()
        {
            _repositorioError.LiberarRecursos();
        }
    }
}
