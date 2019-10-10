using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad;
using System.Linq;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using SEVENC.Dominio.Entidades;

namespace SEVENC.Infraestructura.TestsInfraestructura.SqlEntityFramework.Modulo_Seguridad
{
    [TestClass]
    public class TestRepositorioError : IDisposable
    {
        private RepositorioError _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioError();
        }

        [TestMethod]
        public void TestObtenerErrors()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaError();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerErrorsMediantePagineo()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPruebaError();
            var items1 = _repositorio.ObtenerObjetosPorPagineo(filtro, 0, numeroMinimoItems, e => e.Fecha, false);
            Assert.IsTrue(items1.Count() >= numeroMinimoItems);
            var items2 = _repositorio.ObtenerObjetosPorPagineo(filtro, 1, numeroMinimoItems, e => e.Fecha, false);
            Assert.IsTrue(items2.Count() >= numeroMinimoItems);
            Assert.IsTrue(items1.FirstOrDefault().Fecha > DateTime.Now);
        }

        [TestMethod]
        public void TestCrearError()
        {
            var item = new Error
            {
                Detalle = "PP & MOCHI",
                Fecha = DateTime.Now,
                Mensaje = "TUMA",
                Tipo = "T"
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
        public void TestActualizarError()
        {
            const int id = 1;
            const string mensaje = "Carlos Stalin Alvarado Sánchez";
            var filtro = new FiltroPruebaErrorActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.Mensaje = mensaje;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(mensaje, item.Mensaje);
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

    internal class FiltroPruebaError : Filtros<Error>
    {
        public override Expression<Func<Error, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Error>(e => e.Tipo == "T");
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaErrorActualizar : Filtros<Error>
    {
        private readonly int _id;

        internal FiltroPruebaErrorActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<Error, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Error>(e => e.IdError == _id);
            return filtro.SastifechoPor();
        }
    }
}
