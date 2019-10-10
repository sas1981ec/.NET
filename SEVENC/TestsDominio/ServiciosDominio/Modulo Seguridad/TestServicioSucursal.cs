using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Sucursal;
using Moq;
using System.Collections.Generic;
using SEVENC.Dominio.Entidades;
using System.Linq;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioSucursal
    {
        private ServicioSucursal _servicio;
        private Mock<IRepositorioSucursal> _mockSucursal;

        [TestInitialize]
        public void Inicializar()
        {
            _mockSucursal = new Mock<IRepositorioSucursal>();
            _servicio = new ServicioSucursal(_mockSucursal.Object);
        }

        [TestMethod]
        public void TestObtenerSucursales()
        {
            _mockSucursal.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSucursal>())).Returns(new List<Sucursal>());
            var resultado = _servicio.ObtenerSucursales();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearSucursal()
        {
            _mockSucursal.Setup(m => m.Agregar(It.IsAny<Sucursal>()));
            _servicio.CrearSucursal(new Sucursal());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarSucursalCuandoNoExisteId()
        {
            _mockSucursal.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSucursalPorId>())).Returns(new List<Sucursal>());
            _mockSucursal.Setup(m => m.Actualizar(It.IsAny<Sucursal>()));
            try
            {
                _servicio.ActualizarSucursal(new Sucursal());
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe sucursal 0", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarSucursalCuandoYaCambiaronLosDatos()
        {
            _mockSucursal.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSucursalPorId>())).Returns(new List<Sucursal> { new Sucursal { Concurrencia = new byte[5] } });
            _mockSucursal.Setup(m => m.Actualizar(It.IsAny<Sucursal>()));
            try
            {
                _servicio.ActualizarSucursal(new Sucursal { Concurrencia = new byte[4] });
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarSucursal()
        {
            _mockSucursal.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSucursalPorId>())).Returns(new List<Sucursal> { new Sucursal { Concurrencia = new byte[5] } });
            _mockSucursal.Setup(m => m.Actualizar(It.IsAny<Sucursal>()));
            _servicio.ActualizarSucursal(new Sucursal { Concurrencia = new byte[5] });
            Assert.IsTrue(true);
        }
    }
}
