using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaDominio.ServiciosDominio;

namespace PROASOFT.CapaDominio.TestsDominio.ServiciosDominio
{
    [TestClass]
    public class TestServicioLogin
    {
        private ServicioLogin _servicio;
        private Mock<IRepositorioUsuario> _mockRepositorioUsuario;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioUsuario = new Mock<IRepositorioUsuario>();
            _servicio = new ServicioLogin(_mockRepositorioUsuario.Object);
        }

        [TestMethod]
        public void TestLoginCuandoEsInvalido()
        {
            _mockRepositorioUsuario.Setup(m => m.ObtenerUsuariosConRoles(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new List<USUARIO>());
            var resultado = _servicio.Login("", "");
            Assert.IsFalse(resultado.FueOk);
            Assert.AreEqual("Usuario y/o contraseña inválidas.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestLoginOk()
        {
            _mockRepositorioUsuario.Setup(m => m.ObtenerUsuariosConRoles(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new List<USUARIO> { new USUARIO { APELLIDOS = "", NOMBRES = "", ROLES = new List<ROL> { new ROL()} } });
            var resultado = _servicio.Login("", "");
            Assert.IsTrue(resultado.FueOk);
            Assert.AreEqual("Usuario Autenticado.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestCambiarContrasenaCuandoNoExisteIdUsuario()
        {
            _mockRepositorioUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<USUARIO>());
            try
            {
                _servicio.CambiarContrasena(0, "", "");
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe usuario cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestCambiarContrasenaCuandoContrasenaAntiguaEsIncorrecta()
        {
            _mockRepositorioUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<USUARIO> { new USUARIO { CONTRASENA = "PP"} });
            try
            {
                _servicio.CambiarContrasena(0, "ds", "");
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("La contraseña antigua no es la correcta.", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestCambiarContrasenaOk()
        {
            _mockRepositorioUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<USUARIO> { new USUARIO { CONTRASENA = "pK1ssYTpaMFRTclTaody5S/Pn7szEFzw/prCuLrWDs1H4qTYRZZT5r+ff2R/J/9Q7QwvVxud2gqPXkLFF1EZDw==", NOMBRES = "", ROLES = new List<ROL> { new ROL() } } });
            _servicio.CambiarContrasena(0, "Unidad0000", "");
            Assert.IsTrue(true);
        }
    }
}
