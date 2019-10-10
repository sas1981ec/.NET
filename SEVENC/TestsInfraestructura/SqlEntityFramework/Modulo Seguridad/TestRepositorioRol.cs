using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad;
using System.Linq;
using SEVENC.Dominio.Dominio.Filtros;
using System.Linq.Expressions;
using SEVENC.Dominio.Entidades;
using System.Collections.Generic;

namespace SEVENC.Infraestructura.TestsInfraestructura.SqlEntityFramework.Modulo_Seguridad
{
    [TestClass]
    public class TestRepositorioRol : IDisposable
    {
        private RepositorioRol _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioRol();
        }

        [TestMethod]
        public void TestObtenerRoles()
        {
            const int numeroMinimoItems = 3;
            var filtro = new FiltroPruebaRol();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestCrearRol()
        {
            var item = new Rol
            {
                Descripcion = "XXXXXXX",
                Nombre = "PRUEBA 3"
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
        public void TestActualizarRol()
        {
            const int id = 1;
            const string nombre = "ACTU";
            var filtro = new FiltroPruebaRolActualizar(id);
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

        [TestMethod]
        public void TestAsignarOperacionesARolCuandoNoExisteRol()
        {
            const int idRol = 66;
            try
            {
                _repositorio.AsignarOperacionesARol(new List<int>(), idRol);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual($"No existe rol {idRol}", ex.Message);
                return;
            }
            Assert.Fail("Test falló.");
        }

        [TestMethod]
        public void TestAsignarOperacionesARolOk()
        {
            const int idRol = 1;
            var idsOperaciones = new List<int> { 2, 3 };
            try
            {
                _repositorio.AsignarOperacionesARol(idsOperaciones, idRol);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            }
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

    internal class FiltroPruebaRol : Filtros<Rol>
    {
        public override Expression<Func<Rol, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Rol>(s => !s.EstaActivo);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaRolActualizar : Filtros<Rol>
    {
        private readonly int _id;

        internal FiltroPruebaRolActualizar(int id)
        {
            _id = id;
        }

        public override Expression<Func<Rol, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Rol>(s => s.IdRol == _id);
            return filtro.SastifechoPor();
        }
    }
}
