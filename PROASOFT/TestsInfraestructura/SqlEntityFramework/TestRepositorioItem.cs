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
    public class TestRepositorioItem : IDisposable
    {
        private RepositorioItem _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioItem();
        }

        [TestMethod]
        public void TestObtenerItems()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaItem();
            var items = _repositorio.ObtenerItemsConMedida(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerItemsConDetalles()
        {
            var filtro = new FiltroPruebaItem();
            var item = _repositorio.ObtenerItemConDetallesComprasYDetallesItemProduccion(filtro);
            Assert.IsNotNull(item);
            var item1 = _repositorio.ObtenerItemsConDetallesComprasYDetallesItemProduccion(filtro);
            Assert.IsTrue(item1.Count() > 0);
        }

        [TestMethod]
        public void TestCrearItem()
        {
            var item = new ITEM
            {
                ESTA_ACTIVO = true,
                ID_MEDIDA = 1,
                NOMBRE = "HARINA"
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
        public void TestActualizarItem()
        {
            //const int id = 1;
            //var filtro = new FiltroPruebaItemActualizar(id);
            //var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            //if (item == null)
            //    Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            //_repositorio.Actualizar(item);
            //item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            //if (item == null)
            //    Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            //Assert.AreEqual(12.5, item.CANTIDAD);
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

    internal class FiltroPruebaItem : Filtros<ITEM>
    {
        public override Expression<Func<ITEM, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM>(i => i.ID_ITEM > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaItemActualizar : Filtros<ITEM>
    {
        private readonly int _id;

        internal FiltroPruebaItemActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<ITEM, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM>(i => i.ID_ITEM == _id);
            return filtro.SastifechoPor();
        }
    }
}
