using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Infraestructura.SqlEntityFramework.Repositorios.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using System.Collections.Generic;

namespace SEVENC.Infraestructura.TestsInfraestructura.SqlEntityFramework.Modulo_Seguridad
{
    [TestClass]
    public class TestRepositorioAuditoria : IDisposable
    {
        private RepositorioAuditoria _repositorio;
        private bool _disposed;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new RepositorioAuditoria();
        }

        [TestMethod]
        public void TestCrearAuditoria()
        {
            var item = new Auditoria
            {
                Fecha = DateTime.Now,
                IdOperacion = 1,
                IdUsuario = 1,
                DetallesAuditorias = new List<DetalleAuditoria> { new DetalleAuditoria { Campo = "Prueba", ClaveEntidad = "ID", Entidad = "ENTITY", ValorAntiguo = "ALFA", ValorNuevo = "BETA"} },
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
}
