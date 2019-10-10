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
    public class TestRepositorioOperacion : IDisposable
    {
        private RepositorioOperacion _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioOperacion();
        }

        [TestMethod]
        public void TestObtenerOperaciones()
        {
            const int numeroMinimoItems = 3;
            var filtro = new FiltroPruebaOperacion();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestActualizarOperacion()
        {
            const int id = 3;
            const string nombre = "ConsultarEmpresa";
            var filtro = new FiltroPruebaOperacionActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.Nombre = nombre;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(nombre, item.Nombre);
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

    internal class FiltroPruebaOperacion : Filtros<Operacion>
    {
        public override Expression<Func<Operacion, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Operacion>(o => o.EstaActiva);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaOperacionActualizar : Filtros<Operacion>
    {
        private readonly int _id;

        internal FiltroPruebaOperacionActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<Operacion, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Operacion>(o => o.IdOperacion == _id);
            return filtro.SastifechoPor();
        }
    }
}
