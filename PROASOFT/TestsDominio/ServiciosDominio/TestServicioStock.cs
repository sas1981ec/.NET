using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using PROASOFT.CapaDominio.ServiciosDominio;

namespace PROASOFT.CapaDominio.TestsDominio.ServiciosDominio
{
    [TestClass]
    public class TestServicioStock
    {
        private ServicioStock _servicio;
        private Mock<IRepositorioItem> _mockRepositorioItem;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioItem = new Mock<IRepositorioItem>();
            _servicio = new ServicioStock(_mockRepositorioItem.Object);
        }

        [TestMethod]
        public void TestObtenerStockItems()
        {
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConStocks(It.IsAny<FiltroItem>())).Returns(new List<ITEM>());
            var resultado = _servicio.ObtenerStockItems();
            Assert.AreEqual(0, resultado.Count());
        }
    }
}
