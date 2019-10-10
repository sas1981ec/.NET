using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Auditoria;
using Moq;
using SEVENC.Dominio.Entidades;

namespace SEVENC.Dominio.TestsDominio.ServiciosDominio.Modulo_Seguridad
{
    [TestClass]
    public class TestServicioAuditoria
    {
        private ServicioAuditoria _servicio;
        private Mock<IRepositorioAuditoria> _mockAuditoria;

        [TestInitialize]
        public void Inicializar()
        {
            _mockAuditoria = new Mock<IRepositorioAuditoria>();
            _servicio = new ServicioAuditoria(_mockAuditoria.Object);        }

        [TestMethod]
        public void TestCrearAuditoria()
        {
            _mockAuditoria.Setup(m => m.Agregar(It.IsAny<Auditoria>()));
            _servicio.CrearAuditoria(new Auditoria());
            Assert.IsTrue(true);
        }
    }
}
