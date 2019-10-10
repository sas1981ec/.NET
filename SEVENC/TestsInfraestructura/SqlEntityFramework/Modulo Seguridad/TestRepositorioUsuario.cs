using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad;
using System.Linq;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using SEVENC.Dominio.Entidades;
using System.Collections.Generic;

namespace SEVENC.Infraestructura.TestsInfraestructura.SqlEntityFramework.Modulo_Seguridad
{
    [TestClass]
    public class TestRepositorioUsuario : IDisposable
    {
        private RepositorioUsuario _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioUsuario();
        }

        [TestMethod]
        public void TestObtenerUsuarios()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaUsuario();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerUsuariosMediantePagineo()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaUsuario();
            var items1 = _repositorio.ObtenerObjetosPorPagineo(filtro, 0, numeroMinimoItems, e => e.Apellidos, true);
            Assert.IsTrue(items1.Count() == numeroMinimoItems);
            var items2 = _repositorio.ObtenerObjetosPorPagineo(filtro, 1, numeroMinimoItems, e => e.Apellidos, true);
            Assert.IsTrue(items2.Count() == 0);
        }

        [TestMethod]
        public void TestCrearUsuario()
        {
            var item = new Usuario
            {
                Apellidos = "Alvarado",
                Contrasena = "dfasfasefsad",
                Email = "sasasjjkl@kjlkjñ.com",
                EsSistema = false,
                EstaActivo = true,
                EstaBloqueado = false,
                EstaEliminado = false,
                Foto = new byte[20],
                Nombres = "Stalin",
                UserName = "salvarado"
            };
            try
            {
                _repositorio.Agregar(item);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            }
        }

        [TestMethod]
        public void TestActualizarUsuario()
        {
            const int id = 1;
            const string nombres = "Carlos Stalin";
            var filtro = new FiltroPruebaUsuarioActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.Nombres = nombres;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(nombres, item.Nombres);
        }

        [TestMethod]
        public void TestAsignarRolesAUsuarioCuandoNoExisteUsuario()
        {
            const int idUsuario= 66;
            try
            {
                _repositorio.AsignarRolesAUsuarios(new List<int>(), idUsuario);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual($"No existe usuario {idUsuario}", ex.Message);
                return;
            }
            Assert.Fail("Test falló.");
        }

        [TestMethod]
        public void TestAsignarRolesAUsuarioOk()
        {
            const int idUsuario = 1;
            var idsRoles = new List<int> { 2, 3 };
            try
            {
                _repositorio.AsignarRolesAUsuarios(idsRoles, idUsuario);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            }
        }

        [TestMethod]
        public void TestObtenerUsuarioParaLogin()
        {
            const int numeroEmpresas = 2;
            const int numeroRoles = 2;
            const int numeroOperaciones = 3;
            var usuario = _repositorio.ObtenerUsuarioParaLogin(new FiltroPruebaUsuario());
            Assert.AreEqual(numeroEmpresas, usuario.Empresas.Count());
            Assert.AreEqual(numeroRoles, usuario.Roles.Count());
            Assert.AreEqual(numeroOperaciones, usuario.Roles.FirstOrDefault().Operaciones.Count());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                _repositorio.Dispose();
            _disposed = true;
        }
    }

    internal class FiltroPruebaUsuario : Filtros<Usuario>
    {
        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(e => e.EstaActivo);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaUsuarioActualizar : Filtros<Usuario>
    {
        private readonly int _id;

        internal FiltroPruebaUsuarioActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<Usuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Usuario>(e => e.IdUsuario == _id);
            return filtro.SastifechoPor();
        }
    }
}
