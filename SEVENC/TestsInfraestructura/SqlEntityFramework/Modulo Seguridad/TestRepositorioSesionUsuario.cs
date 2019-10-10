using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using System.Linq;

namespace SEVENC.Infraestructura.TestsInfraestructura.SqlEntityFramework.Modulo_Seguridad
{
    [TestClass]
    public class TestRepositorioSesionUsuario : IDisposable
    {
        private RepositorioSesionUsuario _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioSesionUsuario();
        }

        [TestMethod]
        public void TestObtenerSesionesUsuarios()
        {
            const int numeroMinimoItems = 3;
            var filtro = new FiltroPruebaSesionUsuario();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerSesionUsuariosMediantePagineo()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPruebaSesionUsuario();
            var items1 = _repositorio.ObtenerObjetosPorPagineo(filtro, 0, numeroMinimoItems, su => su.FechaInicio, true);
            Assert.IsTrue(items1.Count() >= numeroMinimoItems);
            var items2 = _repositorio.ObtenerObjetosPorPagineo(filtro, 1, numeroMinimoItems, su => su.FechaInicio, true);
            Assert.IsTrue(items2.Count() >= numeroMinimoItems - 1);
        }

        [TestMethod]
        public void TestCrearSesionUsuario()
        {
            var item = new SesionUsuario
            {
                FechaInicio = DateTime.Now,
                Ip = "190.162.1.5",
                IdUsuario = 3
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
        public void TestActualizarSesionUsuario()
        {
            const int id = 1;
            var filtro = new FiltroPruebaSesionUsuarioActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.FechaFin = DateTime.Now;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.IsNotNull(item.FechaFin);
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

    internal class FiltroPruebaSesionUsuario : Filtros<SesionUsuario>
    {
        public override Expression<Func<SesionUsuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<SesionUsuario>(su => su.IdSesion > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaSesionUsuarioActualizar : Filtros<SesionUsuario>
    {
        private readonly long _id;

        internal FiltroPruebaSesionUsuarioActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<SesionUsuario, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<SesionUsuario>(su => su.IdSesion == _id);
            return filtro.SastifechoPor();
        }
    }
}
