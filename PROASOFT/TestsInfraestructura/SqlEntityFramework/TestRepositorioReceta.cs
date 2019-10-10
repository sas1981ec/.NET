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
    public class TestRepositorioReceta : IDisposable
    {
        private RepositorioReceta _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioReceta();
        }

        [TestMethod]
        public void TestObtenerRecetas()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaReceta();
            var items = _repositorio.ObtenerRecetasConItems(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearReceta()
        {
            //var item = new RECETA
            //{
            //    ESTA_ACTIVA = false,
            //    NOMBRE = "EMPANADA QUESO",
            //    DETALLES_RECETA = new List<DETALLE_RECETA>
            //    {
            //        new DETALLE_RECETA
            //        {
            //            CANTIDAD_GRAMOS = 100.5,
            //            ID_ITEM = 1
            //        },
            //        new DETALLE_RECETA
            //        {
            //            CANTIDAD_GRAMOS = 20.65,
            //            ID_ITEM = 2
            //        }
            //    }
            //};
            //try
            //{
            //    _repositorio.Agregar(item);
            //    Assert.IsTrue(true);
            //}
            //catch (Exception ex)
            //{
            //    Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            //}
        }

        [TestMethod]
        public void TestActualizarReceta()
        {
            const int id = 1;
            var filtro = new FiltroPruebaRecetaActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.ESTA_ACTIVA = true;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(true, item.ESTA_ACTIVA);
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

    internal class FiltroPruebaReceta : Filtros<RECETA>
    {
        public override Expression<Func<RECETA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<RECETA>(r => r.ID_RECETA > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaRecetaActualizar : Filtros<RECETA>
    {
        private readonly int _id;

        internal FiltroPruebaRecetaActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<RECETA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<RECETA>(r => r.ID_RECETA == _id);
            return filtro.SastifechoPor();
        }
    }
}
