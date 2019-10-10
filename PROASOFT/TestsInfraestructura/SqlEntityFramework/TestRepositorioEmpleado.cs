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
    public class TestRepositorioEmpleado : IDisposable
    {
        private RepositorioEmpleado _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioEmpleado();
        }

        [TestMethod]
        public void TestObtenerEmpleados()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPruebaEmpleado();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearEmpleado()
        {
            var item = new EMPLEADO
            {
                APELLIDOS = "MACAO",
                IDENTIFICACION = "12346790",
                NOMBRES = "XAVIER"
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
        public void TestActualizarEmpleado()
        {
            const int id = 1;
            var filtro = new FiltroPruebaEmpleadoActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.ESTA_ACTIVO = true;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(true, item.ESTA_ACTIVO);
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

    internal class FiltroPruebaEmpleado : Filtros<EMPLEADO>
    {
        public override Expression<Func<EMPLEADO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<EMPLEADO>(e => e.ID_EMPLEADO > 0);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaEmpleadoActualizar : Filtros<EMPLEADO>
    {
        private readonly int _id;

        internal FiltroPruebaEmpleadoActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<EMPLEADO, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<EMPLEADO>(e => e.ID_EMPLEADO == _id);
            return filtro.SastifechoPor();
        }
    }
}
