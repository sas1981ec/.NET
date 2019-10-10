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
    public class TestRepositorioMedida : IDisposable
    {
        private RepositorioMedida _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioMedida();
        }

        [TestMethod]
        public void TestObtenerMedidas()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPruebaMedida();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
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

    internal class FiltroPruebaMedida : Filtros<MEDIDA>
    {
        public override Expression<Func<MEDIDA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<MEDIDA>(m => m.ID_MEDIDA > 0);
            return filtro.SastifechoPor();
        }
    }
}
