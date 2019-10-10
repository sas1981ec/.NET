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
    public class TestRepositorioStockProduccion : IDisposable
    {
        private RepositorioStockProduccion _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioStockProduccion();
        }

        [TestMethod]
        public void TestObtenerStockProduccion()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaStockProduccion();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearStockProduccion()
        {
            //var item = new STOCK_PRODUCCION
            //{
            //    CANTIDAD_GRAMOS = 100,
            //    ITEM = new ITEM
            //    {
            //        ID_ITEM = 1
            //    }
            //};
            //try
            //{
            //    _repositorio.CrearStockProduccion(item);
            //    Assert.IsTrue(true);
            //}
            //catch (Exception ex)
            //{
            //    Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            //}
        }

        [TestMethod]
        public void TestActualizarStockProduccion()
        {
            //const int id = 1;
            //var filtro = new FiltroPruebaStockProduccionActualizar(id);
            //var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            //if (item == null)
            //    Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            //item.CANTIDAD_GRAMOS = 10;
            //_repositorio.Actualizar(item);
            //item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            //if (item == null)
            //    Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            //Assert.AreEqual(10, item.CANTIDAD_GRAMOS);
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

    internal class FiltroPruebaStockProduccion : Filtros<STOCK_PRODUCCION>
    {
        public override Expression<Func<STOCK_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<STOCK_PRODUCCION>(sp => sp.ID > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaStockProduccionActualizar : Filtros<STOCK_PRODUCCION>
    {
        private readonly int _id;

        internal FiltroPruebaStockProduccionActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<STOCK_PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<STOCK_PRODUCCION>(sp => sp.ID == _id);
            return filtro.SastifechoPor();
        }
    }
}
