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
    public class TestsRepositorioEmpresa : IDisposable
    {
        private RepositorioEmpresa _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioEmpresa();
        }

        [TestMethod]
        public void TestObtenerEmpresas()
        {
            const int numeroMinimoItems = 1;
            var filtro = new FiltroPrueba();
            var items = _repositorio.ObtenerObjetos(filtro);
            Assert.IsTrue(items.Count() >= numeroMinimoItems);
        }

        [TestMethod]
        public void TestObtenerEmpresasMediantePagineo()
        {
            const int numeroMinimoItems = 2;
            var filtro = new FiltroPrueba();
            var items1 = _repositorio.ObtenerObjetosPorPagineo(filtro, 0, numeroMinimoItems, e => e.NombreComercial, true);
            Assert.IsTrue(items1.Count() >= numeroMinimoItems);
            var items2 = _repositorio.ObtenerObjetosPorPagineo(filtro, 1, numeroMinimoItems, e => e.NombreComercial, true);
            Assert.IsTrue(items2.Count() >= numeroMinimoItems);
            Assert.AreEqual("ARASCO", items1.FirstOrDefault().NombreComercial);
            Assert.AreEqual("CAEU", items2.FirstOrDefault().NombreComercial);
        }

        [TestMethod]
        public void TestCrearEmpresa()
        {
            var item = new Empresa
            {
                IdRepresentanteLegal = "0916298235",
                NombreComercial = "SOTIFU",
                NombreRepresentanteLegal = "Stalin Alvarado.",
                RazonSocial = "SOLUCIONES TECNOLOGICAS INTELIGENTES FUTURAS",
                RUC = "0992868457001"
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
        public void TestActualizarEmpresa()
        {
            const int id = 1;
            const string nombreRepresentanteLegal = "Carlos Stalin Alvarado Sánchez";
            var filtro = new FiltroPruebaActualizar(id);
            var item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            item.EstaActiva = true;
            item.NombreRepresentanteLegal = nombreRepresentanteLegal;
            _repositorio.Actualizar(item);
            item = _repositorio.ObtenerObjetos(filtro).FirstOrDefault();
            if (item == null)
                Assert.Fail("Test falló porque no existe ningún item a actualizar.");
            Assert.AreEqual(nombreRepresentanteLegal, item.NombreRepresentanteLegal);
            Assert.IsTrue(item.EstaActiva);
        }

        [TestMethod]
        public void TestAsignarSucursalesAEmpresaCuandoNoExisteEmpresa()
        {
            const byte idEmpresa = 66;
            try
            {
                _repositorio.AsignarSucursalesAEmpresa(new List<short>(), idEmpresa);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual($"No existe empresa {idEmpresa}", ex.Message);
                return;
            }
            Assert.Fail("Test falló.");
        }

        [TestMethod]
        public void TestAsignarSucursalesAEmpresaOk()
        {
            const byte idEmpresa = 1;
            var idsSucrusales = new List<short> { 2, 3 };
            try
            {
                _repositorio.AsignarSucursalesAEmpresa(idsSucrusales, idEmpresa);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            }
        }

        [TestMethod]
        public void TestAsignarUsuariosAEmpresaCuandoNoExisteEmpresa()
        {
            const byte idEmpresa = 66;
            try
            {
                _repositorio.AsignarUsuariosAEmpresa(new List<int>(), idEmpresa);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual($"No existe empresa {idEmpresa}", ex.Message);
                return;
            }
            Assert.Fail("Test falló.");
        }

        [TestMethod]
        public void TestAsignarUsuariosAEmpresaOk()
        {
            const byte idEmpresa = 1;
            var idsUsuarios = new List<int> { 1 };
            try
            {
                _repositorio.AsignarUsuariosAEmpresa(idsUsuarios, idEmpresa);
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

    internal class FiltroPrueba : Filtros<Empresa>
    {
        public override Expression<Func<Empresa, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Empresa>(e => e.EstaActiva);
            return filtro.SastifechoPor();
        }
    }

    internal class FiltroPruebaActualizar : Filtros<Empresa>
    {
        private readonly int _idProceso;

        internal FiltroPruebaActualizar(int idProceso)
        {
            _idProceso = idProceso;
        }

        public override Expression<Func<Empresa, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<Empresa>(e => e.IdEmpresa == _idProceso);
            return filtro.SastifechoPor();
        }
    }
}
