using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios;

namespace PROASOFT.CapaInfraestructura.TestsInfraestructura.SqlEntityFramework
{
    [TestClass]
    public class TestRepositorioProveedor : IDisposable
    {
        private RepositorioProveedor _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioProveedor();
        }

        [TestMethod]
        public void TestObtenerProveedores()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaProveedor();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearProveedor()
        {
            var item = new PROVEEDOR
            {
                RAZON_SOCIAL = "PP & MOCHI",
                RUC = "12346790",
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
        public void TestActualizarProveedor()
        {
            const int id = 1;
            var filtro = new FiltroPruebaProveedorActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.ESTA_ACTIVO = true;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(true, item.ESTA_ACTIVO);
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

    internal class FiltroPruebaProveedor : Filtros<PROVEEDOR>
    {
        public override Expression<Func<PROVEEDOR, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PROVEEDOR>(p => p.ID_PROVEEDOR > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaProveedorActualizar : Filtros<PROVEEDOR>
    {
        private readonly int _id;

        internal FiltroPruebaProveedorActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<PROVEEDOR, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PROVEEDOR>(p => p.ID_PROVEEDOR == _id);
            return filtro.SastifechoPor();
        }
    }
}
