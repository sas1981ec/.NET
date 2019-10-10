using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaInfraestructura.SqlEntityFramework.Repositorios;

namespace PROASOFT.CapaInfraestructura.TestsInfraestructura.SqlEntityFramework
{
    [TestClass]
    public class TestRepositorioCompra : IDisposable
    {
        private RepositorioCompra _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioCompra();
        }

        [TestMethod]
        public void TestObtenerCompras()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaCompra();
            var items = _repositorio.ObtenerComprasConUsuarioProveedor(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearCompra()
        {
            var item = new COMPRA
            {
                FECHA = DateTime.Now,
                ID_USUARIO = 1,
                DETALLES_COMPRAS = new List<DETALLE_COMPRA>
                {
                    new DETALLE_COMPRA
                    {
                        CANTIDAD = 2,
                        ID_ITEM = 1,
                        PRECIO = new decimal(5.25)
                    }
                }
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
        public void TestActualizarCompra()
        {
            const int id = 1;
            var filtro = new FiltroPruebaCompraActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.ID_USUARIO = 2;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(2, item.ID_USUARIO);
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

    internal class FiltroPruebaCompra : Filtros<COMPRA>
    {
        public override Expression<Func<COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<COMPRA>(c => c.ID_COMPRA > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaCompraActualizar : Filtros<COMPRA>
    {
        private readonly int _id;

        internal FiltroPruebaCompraActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<COMPRA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<COMPRA>(c => c.ID_COMPRA == _id);
            return filtro.SastifechoPor();
        }
    }
}
