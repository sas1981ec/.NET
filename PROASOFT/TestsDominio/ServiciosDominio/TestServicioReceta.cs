using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaDominio.ServiciosDominio;
using System.Linq;

namespace PROASOFT.CapaDominio.TestsDominio.ServiciosDominio
{
    [TestClass]
    public class TestServicioReceta
    {
        private ServicioReceta _servicio;
        private Mock<IRepositorioReceta> _mockRepositorioReceta;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioReceta = new Mock<IRepositorioReceta>();
            _servicio = new ServicioReceta(_mockRepositorioReceta.Object);
        }

        [TestMethod]
        public void TestObtenerRecetas()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerRecetasConItems(It.IsAny<FiltroReceta>())).Returns(new List<RECETA>());
            var resultado = _servicio.ObtenerRecetas();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearReceta()
        {
            _mockRepositorioReceta.Setup(m => m.Agregar(It.IsAny<RECETA>()));
            _servicio.CrearReceta(new RECETA());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarRecetaCuandoNoExisteId()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRecetaPorId>())).Returns(new List<RECETA>());
            try
            {
                _servicio.ActualizarReceta(new RECETA());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No existe receta cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestActualizarRecetaOk()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRecetaPorId>())).Returns(new List<RECETA> { new RECETA() });
            _mockRepositorioReceta.Setup(m => m.Actualizar(It.IsAny<RECETA>()));
            _servicio.ActualizarReceta(new RECETA());
            Assert.IsTrue(true);
        }
    }
}
