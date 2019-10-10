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
    public class TestRepositorioProduccion : IDisposable
    {
        private RepositorioProduccion _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioProduccion();
        }

        [TestMethod]
        public void TestObtenerProducciones()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaProduccion();
            var items = _repositorio.ObtenerProduccionConDetalles(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearProduccion()
        {
            var item = new PRODUCCION
            {
                FECHA = DateTime.Now,
                ID_USUARIO = 1,
                DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION>
                {
                    new DETALLE_PRODUCCION
                    {
                        CANTIDAD = 1,
                        ID_RECETA = 1
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
        public void TestActualizarProduccion()
        {
            const int id = 1;
            var filtro = new FiltroPruebaProduccionActualizar(id);
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

    internal class FiltroPruebaProduccion : Filtros<PRODUCCION>
    {
        public override Expression<Func<PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PRODUCCION>(p => p.ID_PRODUCCION > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaProduccionActualizar : Filtros<PRODUCCION>
    {
        private readonly int _id;

        internal FiltroPruebaProduccionActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<PRODUCCION, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<PRODUCCION>(p => p.ID_PRODUCCION == _id);
            return filtro.SastifechoPor();
        }
    }
}
