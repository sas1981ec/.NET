using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using Moq;
using System.Collections.Generic;
using SEVENC.Dominio.Entidades;
using System.Linq;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Operacion;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioOperacion
    {
        private ServicioOperacion _servicio;
        private Mock<IRepositorioOperacion> _mockOperacion;

        [TestInitialize]
        public void Inicializar()
        {
            _mockOperacion = new Mock<IRepositorioOperacion>();
            _servicio = new ServicioOperacion(_mockOperacion.Object);
        }

        [TestMethod]
        public void TestObtenerOperaciones()
        {
            _mockOperacion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroOperacion>())).Returns(new List<Operacion>());
            var resultado = _servicio.ObtenerOperaciones();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestActualizarOperacionCuandoNoExisteId()
        {
            _mockOperacion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroOperacionPorId>())).Returns(new List<Operacion>());
            _mockOperacion.Setup(m => m.Actualizar(It.IsAny<Operacion>()));
            try
            {
                _servicio.ActualizarOperacion(new Operacion());
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe operacion 0", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarOperacion()
        {
            _mockOperacion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroOperacionPorId>())).Returns(new List<Operacion> { new Operacion() });
            _mockOperacion.Setup(m => m.Actualizar(It.IsAny<Operacion>()));
            _servicio.ActualizarOperacion(new Operacion());
            Assert.IsTrue(true);
        }
    }
}
