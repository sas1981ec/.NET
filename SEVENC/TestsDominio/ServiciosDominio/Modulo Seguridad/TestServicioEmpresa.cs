using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using Moq;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Empresa;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Sucursal;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario;
using System.Collections.Generic;
using SEVENC.Dominio.Entidades;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioEmpresa
    {
        private ServicioEmpresa _servicio;
        private Mock<IRepositorioEmpresa> _mockEmpresa;
        private Mock<IRepositorioUsuario> _mockUsuario;
        private Mock<IRepositorioSucursal> _mockSucursal;

        [TestInitialize]
        public void Inicializar()
        {
            _mockEmpresa = new Mock<IRepositorioEmpresa>();
            _mockUsuario = new Mock<IRepositorioUsuario>();
            _mockSucursal = new Mock<IRepositorioSucursal>();
            _servicio = new ServicioEmpresa(_mockEmpresa.Object, _mockUsuario.Object, _mockSucursal.Object);
        }

        [TestMethod]
        public void TestObtenerEmpresas()
        {
            _mockEmpresa.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpresa>())).Returns(new List<Empresa>());
            var resultado = _servicio.ObtenerEmpresas();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerSucursalesPorIdEmpresa()
        {
            _mockSucursal.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSucursalPorIdEmpresa>())).Returns(new List<Sucursal>());
            var resultado = _servicio.ObtenerSucursalesPorIdEmpresa(1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerUsuariosPorIdEmpresa()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorIdEmpresa>())).Returns(new List<Usuario>());
            var resultado = _servicio.ObtenerUsuariosPorIdEmpresa(1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearEmpresa()
        {
            _mockEmpresa.Setup(m => m.Agregar(It.IsAny<Empresa>()));
            _servicio.CrearEmpresa(new Empresa());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarEmpresaCuandoNoExisteId()
        {
            _mockEmpresa.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpresaPorId>())).Returns(new List<Empresa>());
            _mockEmpresa.Setup(m => m.Actualizar(It.IsAny<Empresa>()));
            try
            {
                _servicio.ActualizarEmpresa(new Empresa());
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe empresa 0", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarEmpresaCuandoYaCambiaronLosDatos()
        {
            _mockEmpresa.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpresaPorId>())).Returns(new List<Empresa> { new Empresa { Concurrencia = new byte[5] } });
            _mockEmpresa.Setup(m => m.Actualizar(It.IsAny<Empresa>()));
            try
            {
                _servicio.ActualizarEmpresa(new Empresa { Concurrencia = new byte[4] });
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarEmpresa()
        {
            _mockEmpresa.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpresaPorId>())).Returns(new List<Empresa> { new Empresa { Concurrencia = new byte[4] } });
            _mockEmpresa.Setup(m => m.Actualizar(It.IsAny<Empresa>()));
            _servicio.ActualizarEmpresa(new Empresa { Concurrencia = new byte[4] });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestAsignarSucursalesAEmpresa()
        {
            _mockEmpresa.Setup(m => m.AsignarSucursalesAEmpresa(It.IsAny<IEnumerable<short>>(), It.IsAny<byte>()));
            _servicio.AsignarSucursalesAEmpresa(new List<short>(), 1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestAsignarUsuariosAEmpresa()
        {
            _mockEmpresa.Setup(m => m.AsignarUsuariosAEmpresa(It.IsAny<IEnumerable<int>>(), It.IsAny<byte>()));
            _servicio.AsignarUsuariosAEmpresa(new List<int>(), 1);
            Assert.IsTrue(true);
        }
    }
}
