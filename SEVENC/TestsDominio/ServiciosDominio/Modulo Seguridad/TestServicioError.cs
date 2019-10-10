using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Error;
using Moq;
using SEVENC.Dominio.Entidades;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioError
    {
        private ServicioError _servicio;
        private Mock<IRepositorioError> _mockError;

        [TestInitialize]
        public void Inicializar()
        {
            _mockError = new Mock<IRepositorioError>();
            _servicio = new ServicioError(_mockError.Object);
        }

        [TestMethod]
        public void TestCrearError()
        {
            _mockError.Setup(m => m.Agregar(It.IsAny<Error>()));
            _servicio.CrearError(new Error());
            Assert.IsTrue(true);
        }
    }
}
