using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Usuario;
using Moq;
using System.Collections.Generic;
using SEVENC.Dominio.Entidades;
using System.Linq;
using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Rol;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.SesionUsuario;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioUsuario
    {
        private ServicioUsuario _servicio;
        private Mock<IRepositorioUsuario> _mockUsuario;
        private Mock<IRepositorioRol> _mockRol;
        private Mock<IRepositorioSesionUsuario> _mockSesionUsuario;

        [TestInitialize]
        public void Inicializar()
        {
            _mockUsuario = new Mock<IRepositorioUsuario>();
            _mockRol = new Mock<IRepositorioRol>();
            _mockSesionUsuario = new Mock<IRepositorioSesionUsuario>();
            _servicio = new ServicioUsuario(_mockUsuario.Object, _mockRol.Object, _mockSesionUsuario.Object);
        }

        [TestMethod]
        public void TestObtenerUsuarios()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuario>())).Returns(new List<Usuario>());
            var resultado = _servicio.ObtenerUsuarios();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerUsuariosPorCriteriosBusquedaCuandoNoTiene()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuario>())).Returns(new List<Usuario>());
            var resultado = _servicio.ObtenerUsuariosPorCriteriosBusqueda(new Dictionary<Busqueda, string>(), 1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerUsuariosPorCriteriosBusqueda()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuario>())).Returns(new List<Usuario>());
            var criteriosBusqueda = new Dictionary<Busqueda, string>();
            criteriosBusqueda.Add(Busqueda.PorId, "1");
            var resultado = _servicio.ObtenerUsuariosPorCriteriosBusqueda(criteriosBusqueda, 1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerRolesPorIdUsuario()
        {
            _mockRol.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRolPorIdUsuario>())).Returns(new List<Rol>());
            var resultado = _servicio.ObtenerRolesPorIdUsuario(1);
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearUsuario()
        {
            _mockUsuario.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            _servicio.CrearUsuario(new Usuario());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarUsuarioCuandoNoExisteId()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario>());
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            try
            {
                _servicio.ActualizarUsuario(new Usuario());
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("No existe usuario 0", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarUsuarioCuandoYaCambiaronLosDatos()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario> { new Usuario { Concurrencia = new byte[5] } });
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            try
            {
                _servicio.ActualizarUsuario(new Usuario { Concurrencia = new byte[4] });
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Los datos que desea modificar han cambiado. Por favor refresque o actualice su pantalla.", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestActualizarUsuario()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario> { new Usuario { Concurrencia = new byte[5] } });
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            _servicio.ActualizarUsuario(new Usuario { Concurrencia = new byte[5] });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestCambiarContrasenaCuandoNoExisteId()
        {
            const int idUsuario = 1;
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario>());
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            try
            {
                _servicio.CambiarContrasena(idUsuario, "");
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual($"No existe usuario {idUsuario}", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestCambiarContrasena()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario> { new Usuario() });
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            _servicio.CambiarContrasena(0, "");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestGenerarContrasenaCuandoNoExisteId()
        {
            const int idUsuario = 1;
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario>());
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            try
            {
                _servicio.GenerarContrasena(idUsuario);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual($"No existe usuario {idUsuario}", ex.Message);
                return;
            }
            Assert.Fail("Falló test.");
        }

        [TestMethod]
        public void TestGenerarContrasena()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorId>())).Returns(new List<Usuario> { new Usuario() });
            _mockUsuario.Setup(m => m.Actualizar(It.IsAny<Usuario>()));
            _servicio.GenerarContrasena(0);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestExisteUserName()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorUserName>())).Returns(new List<Usuario> { new Usuario() });
            Assert.IsTrue(_servicio.ExisteUserName(""));
        }

        [TestMethod]
        public void TestNoExisteUserName()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(new FiltroUsuarioPorUserName(""))).Returns(new List<Usuario> { new Usuario() });
            Assert.IsFalse(_servicio.ExisteUserName(""));
        }

        [TestMethod]
        public void TestExisteEmail()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioPorEmail>())).Returns(new List<Usuario> { new Usuario() });
            Assert.IsTrue(_servicio.ExisteEmail(""));
        }

        [TestMethod]
        public void TestNoExisteEmail()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(new FiltroUsuarioPorEmail(""))).Returns(new List<Usuario> { new Usuario() });
            Assert.IsFalse(_servicio.ExisteUserName(""));
        }

        [TestMethod]
        public void TestAsignarRolesAUsuario()
        {
            _mockUsuario.Setup(m => m.AsignarRolesAUsuarios(It.IsAny<IEnumerable<int>>(), It.IsAny<int>()));
            _servicio.AsignarRolesAUsuario(new List<int>(), 1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestLoginCuandoUsuarioContrasenaEsInvalido()
        {
            _mockUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new List<Usuario>());
            var resultado = _servicio.Login("","","");
            Assert.IsFalse(resultado.FueOk);
            Assert.AreEqual("Usuario y/o contraseña inválidas.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestLoginCuandoUsuarioEstaInactivo()
        {
            _mockUsuario.Setup(m => m.ObtenerUsuarioParaLogin(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new Usuario());
            var resultado = _servicio.Login("", "", "");
            Assert.IsFalse(resultado.FueOk);
            Assert.AreEqual("Su usuario no esta activo para usar el software.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestLoginCuandoUsuarioEstaBloqueado()
        {
            _mockUsuario.Setup(m => m.ObtenerUsuarioParaLogin(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new Usuario { EstaActivo = true, EstaBloqueado = true});
            var resultado = _servicio.Login("", "", "");
            Assert.IsFalse(resultado.FueOk);
            Assert.AreEqual("Su usuario está bloqueado.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestLoginOk()
        {
            _mockUsuario.Setup(m => m.ObtenerUsuarioParaLogin(It.IsAny<FiltroUsuarioParaLogin>())).Returns(new Usuario { EstaActivo = true, Nombres = "PP", Apellidos = "Caceres" });
            var resultado = _servicio.Login("", "", "");
            Assert.IsTrue(resultado.FueOk);
            Assert.AreEqual("Ok.", resultado.Mensaje);
        }

        [TestMethod]
        public void TestCerrarSesion()
        {
            const long id = 11;
            try
            {
                _mockSesionUsuario.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroSesionUsuarioPorId>())).Returns(new List<SesionUsuario> { new SesionUsuario() });
                _servicio.CerrarSesion(id);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(true);
        }
    }
}
