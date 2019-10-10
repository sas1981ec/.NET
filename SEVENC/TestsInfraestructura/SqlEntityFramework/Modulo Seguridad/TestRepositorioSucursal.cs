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
    public class TestRepositorioSucursal : IDisposable
    {
        private RepositorioSucursal _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioSucursal();
        }

        [TestMethod]
        public void TestObtenerSucursals()
        {
            const int numeroMinimoItems = 3;
            var filtro = new FiltroPruebaSucursal();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerSucursalsMediantePagineo()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPruebaSucursal();
            var items1 = _repositorio.ObtenerObjetosPorPagineo(filtro, 0, numeroMinimoItems, e => e.Nombre, true);
            Assert.IsTrue(items1.Count() >= numeroMinimoItems);
            var items2 = _repositorio.ObtenerObjetosPorPagineo(filtro, 1, numeroMinimoItems, e => e.Nombre, true);
            Assert.IsTrue(items2.Count() >= numeroMinimoItems - 1);
            Assert.AreEqual("Sur", items2.FirstOrDefault().Nombre);
        }

        [TestMethod]
        public void TestCrearSucursal()
        {
            var item = new Sucursal
            {
                Direccion = "Por Ahi",
                EsMatriz = true,
                EstaActiva = false,
                EstaEliminada = false,
                Nombre = "FIRST"
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
        public void TestActualizarSucursal()
        {
            const int id = 1;
            const string mensaje = "Carlos Stalin Alvarado Sánchez";
            var filtro = new FiltroPruebaSucursalActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.Nombre = mensaje;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(mensaje, item.Nombre);
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

    internal class FiltroPruebaSucursal : Filtros<Sucursal>
    {
        public override Expression<Func<Sucursal, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Sucursal>(s => s.EstaActiva);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaSucursalActualizar : Filtros<Sucursal>
    {
        private readonly int _id;

        internal FiltroPruebaSucursalActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<Sucursal, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Sucursal>(s => s.IdSucursal == _id);
            return filtro.SastifechoPor();
        }
    }
}
