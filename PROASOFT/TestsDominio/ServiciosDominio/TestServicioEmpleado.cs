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
    public class TestServicioEmpleado
    {
        private ServicioEmpleado _servicio;
        private Mock<IRepositorioEmpleado> _mockRepositorio;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorio = new Mock<IRepositorioEmpleado>();
            _servicio = new ServicioEmpleado(_mockRepositorio.Object);
        }

        [TestMethod]
        public void TestObtenerEmpleados()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpleado>())).Returns(new List<EMPLEADO> { new EMPLEADO() });
            var resultado = _servicio.ObtenerEmpleados();
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestCrearEmpleado()
        {
            _mockRepositorio.Setup(m => m.Agregar(It.IsAny<EMPLEADO>()));
            _servicio.CrearEmpleado(new EMPLEADO());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarEmpleadoCuandoNoExisteId()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpleadoActualizar>())).Returns(new List<EMPLEADO>());
            try
            {
                _servicio.ActualizarEmpleado(new EMPLEADO());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Empleado de id : 0 no existe", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestActualizarEmpleadoOk()
        {
            _mockRepositorio.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpleadoActualizar>())).Returns(new List<EMPLEADO> { new EMPLEADO() });
            _mockRepositorio.Setup(m => m.Actualizar(It.IsAny<EMPLEADO>()));
            _servicio.ActualizarEmpleado(new EMPLEADO());
            Assert.IsTrue(true);
        }
    }
}
