using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using Moq;
using System.Collections.Generic;
using SEVENC.Dominio.Entidades;
using System.Linq;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Operacion;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioRol
    {
        private ServicioRol _servicio;
        private Mock<IRepositorioRol> _mockRol;
        private Mock<IRepositorioOperacion> _mockOperacion;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRol = new Mock<IRepositorioRol>();
            _mockOperacion = new Mock<IRepositorioOperacion>();
            _servicio = new ServicioRol(_mockRol.Object, _mockOperacion.Object);
        }

        [TestMethod]
        public void TestObtenerRoles()
        {
            _mockRol.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRol>())).Returns(new List<Rol>());
            var resultado = _servicio.ObtenerRoles();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerOperacionesPorIdRol()
        {
            _mockOperacion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroOperacionPorIdRol>())).Returns(new List<Operacion>());
            var resultado = _servicio.ObtenerOperacionesPorIdRol(1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearRol()
        {
            _mockRol.Setup(m => m.Agregar(It.IsAny<Rol>()));
            _servicio.CrearRol(new Rol());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarRolCuandoNoExisteId()
        {
            _mockRol.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRolPorId>())).Returns(new List<Rol>());
            _mockRol.Setup(m => m.Actualizar(It.IsAny<Rol>()));
            try
            {
                _servicio.ActualizarRol(new Rol());
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe rol 0", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarRolCuandoYaCambiaronLosDatos()
        {
            _mockRol.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRolPorId>())).Returns(new List<Rol> { new Rol { Concurrencia = new byte[5] } });
            _mockRol.Setup(m => m.Actualizar(It.IsAny<Rol>()));
            try
            {
                _servicio.ActualizarRol(new Rol { Concurrencia = new byte[4] });
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarRol()
        {
            _mockRol.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRolPorId>())).Returns(new List<Rol> { new Rol { Concurrencia = new byte[5] } });
            _mockRol.Setup(m => m.Actualizar(It.IsAny<Rol>()));
            _servicio.ActualizarRol(new Rol { Concurrencia = new byte[5] });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestAsignarRolesAUsuario()
        {
            _mockRol.Setup(m => m.AsignarOperacionesARol(It.IsAny<IEnumerable<int>>(), It.IsAny<int>()));
            _servicio.AsignarOperacionesARol(new List<int>(), 1);
            Assert.IsTrue(true);
        }
    }
}
