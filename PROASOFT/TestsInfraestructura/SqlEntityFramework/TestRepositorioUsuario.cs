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
    public class TestRepositorioUsuario : IDisposable
    {
        private RepositorioUsuario _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioUsuario();
        }

        [TestMethod]
        public void TestObtenerUsuarios()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPruebaUsuario();
            var items = _repositorio.ObtenerUsuariosConRoles(filtro);
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

    internal class FiltroPruebaUsuario : Filtros<USUARIO>
    {
        public override Expression<Func<USUARIO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<USUARIO>(u => u.ID_USUARIO > 0);
            return filtro.SastifechoPor();
        }
    }
}
