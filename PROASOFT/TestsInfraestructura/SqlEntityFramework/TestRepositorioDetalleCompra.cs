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
    public class TestRepositorioDetalleCompra : IDisposable
    {
        private RepositorioDetalleCompra _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioDetalleCompra();
        }

        [TestMethod]
        public void TestObtenerObtenerDetallesCompraConItem()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaDetalleCompra();
            var items = _repositorio.ObtenerDetallesCompraConItem(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
            Assert.IsNotNull(items.FirstOrDefault().ITEM);
        }

        [TestMethod]
        public void TestActualizarDetalleCompra()
        {
            const long id = 1;
            var filtro = new FiltroPruebaDetalleCompraActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.CANTIDAD = 3;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(3, item.CANTIDAD);
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

    internal class FiltroPruebaDetalleCompra : Filtros<DETALLE_COMPRA>
    {
        public override Expression<Func<DETALLE_COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<DETALLE_COMPRA>(dc => dc.ID_COMPRA > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaDetalleCompraActualizar : Filtros<DETALLE_COMPRA>
    {
        private readonly long _id;

        internal FiltroPruebaDetalleCompraActualizar(long id)
        {
            _id = id;
        }

        public override Expression<Func<DETALLE_COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<DETALLE_COMPRA>(dc => dc.ID_DETALLE == _id);
            return filtro.SastifechoPor();
        }
    }
}
