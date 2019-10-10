using System;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaDominio.ServiciosDominio;

namespace PROASOFT.CapaDominio.TestsDominio.ServiciosDominio
{
    [TestClass]
    public class TestServicioProveedor
    {
        private ServicioProveedor _servicio;
        private Mock<IRepositorioProveedor> _mockRepositorio;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorio = new Mock<IRepositorioProveedor>();
            _servicio = new ServicioProveedor(_mockRepositorio.Object);
        }

        [TestMethod]
        public void TestObtenerProveedors()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProveedor>())).Returns(new List<PROVEEDOR> { new PROVEEDOR() });
            var resultado = _servicio.ObtenerProveedores();
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestCrearProveedor()
        {
            _mockRepositorio.Setup(m => m.Agregar(It.IsAny<PROVEEDOR>()));
            _servicio.CrearProveedor(new PROVEEDOR());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarProveedorCuandoNoExisteId()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProveedorActualizar>())).Returns(new List<PROVEEDOR>());
            try
            {
                _servicio.ActualizarProveedor(new PROVEEDOR());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Proveedor de id : 0 no existe", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestActualizarProveedorOk()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProveedorActualizar>())).Returns(new List<PROVEEDOR> { new PROVEEDOR() });
            _mockRepositorio.Setup(m => m.Actualizar(It.IsAny<PROVEEDOR>()));
            _servicio.ActualizarProveedor(new PROVEEDOR());
            Assert.IsTrue(true);
        }
    }
}
