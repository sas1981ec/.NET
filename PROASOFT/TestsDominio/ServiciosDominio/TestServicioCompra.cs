using System;
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
    public class TestServicioCompra
    {
        private ServicioCompra _servicio;
        private Mock<IRepositorioCompra> _mockRepositorioCompra;
        private Mock<IRepositorioDetalleCompra> _mockRepositorioDetalleCompra;
        private Mock<IRepositorioItem> _mockRepositorioItem;
        private Mock<IRepositorioProveedor> _mockRepositorioProveedor;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioCompra = new Mock<IRepositorioCompra>();
            _mockRepositorioDetalleCompra = new Mock<IRepositorioDetalleCompra>();
            _mockRepositorioItem = new Mock<IRepositorioItem>();
            _mockRepositorioProveedor = new Mock<IRepositorioProveedor>();
            _servicio = new ServicioCompra(_mockRepositorioCompra.Object, _mockRepositorioDetalleCompra.Object, _mockRepositorioItem.Object, _mockRepositorioProveedor.Object);
        }

        [TestMethod]
        public void TestObtenerCompras()
        {
            _mockRepositorioCompra.Setup(m => m.ObtenerComprasConUsuarioProveedor(It.IsAny<FiltroCompraFechas>())).Returns(new List<COMPRA> { new COMPRA()});
            var resultado = _servicio.ObtenerCompras(DateTime.Now, DateTime.Now);
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestObtenerDetallesCompra()
        {
            _mockRepositorioDetalleCompra.Setup(m => m.ObtenerDetallesCompraConItem(It.IsAny<FiltroDetalleCompraIdCompra>())).Returns(new List<DETALLE_COMPRA> { new DETALLE_COMPRA() });
            var resultado = _servicio.ObtenerDetallesCompra(0);
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestRegistrarNuevaCompra()
        {
            _mockRepositorioCompra.Setup(m => m.Agregar(It.IsAny<COMPRA>()));
            _servicio.RegistrarNuevaCompra(new COMPRA());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestConfirmarCompraCuandoNoExisteId()
        {
            _mockRepositorioCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroCompra>())).Returns(new List<COMPRA>());
            try
            {
                _servicio.ConfirmarCompra(0, 0);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No existe compra cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestConfirmarCompraOk()
        {
            _mockRepositorioCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroCompra>())).Returns(new List<COMPRA> { new COMPRA()});
            _mockRepositorioCompra.Setup(m => m.Actualizar(It.IsAny<COMPRA>()));
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConStocks(It.IsAny<FiltroItem>())).Returns(new List<ITEM>());
            _servicio.ConfirmarCompra(0, 0);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestModificarDetalleCompraCuandoNoExisteId()
        {
            _mockRepositorioDetalleCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleCompra>())).Returns(new List<DETALLE_COMPRA>());
            try
            {
                _servicio.ModificarDetalleCompra(new DETALLE_COMPRA(), new List<short>());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No existe detalle compra cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestModificarDetalleCompraCuandoNoPuedeModificar()
        {
            _mockRepositorioDetalleCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleCompra>())).Returns(new List<DETALLE_COMPRA> { new DETALLE_COMPRA() });
            try
            {
                _servicio.ModificarDetalleCompra(new DETALLE_COMPRA { COMPRA = new COMPRA { FECHA = DateTime.Now.AddDays(-2)} }, new List<short> { 1 });
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No se puede modificar la compra.", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestModificarDetalleCompraCuandoNoExisteIdCompra()
        {
            _mockRepositorioDetalleCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleCompra>())).Returns(new List<DETALLE_COMPRA> { new DETALLE_COMPRA() });
            _mockRepositorioCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroCompra>())).Returns(new List<COMPRA>());
            try
            {
                _servicio.ModificarDetalleCompra(new DETALLE_COMPRA { COMPRA = new COMPRA { FECHA = DateTime.Now } }, new List<short> { 1 });
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No existe compra cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestModificarDetalleCompraOk()
        {
            _mockRepositorioDetalleCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleCompra>())).Returns(new List<DETALLE_COMPRA> { new DETALLE_COMPRA() });
            _mockRepositorioCompra.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroCompra>())).Returns(new List<COMPRA> { new COMPRA() });
            _servicio.ModificarDetalleCompra(new DETALLE_COMPRA { COMPRA = new COMPRA { FECHA = DateTime.Now } }, new List<short> { 1 });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TesObtenerItems()
        {
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConMedida(It.IsAny<FiltroItem>())).Returns(new List<ITEM> { new ITEM() });
            var resultado = _servicio.ObtenerItems();
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TesObtenerProveedores()
        {
            _mockRepositorioProveedor.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProveedorActivo>())).Returns(new List<PROVEEDOR> { new PROVEEDOR() });
            var resultado = _servicio.ObtenerProveedores();
            Assert.IsTrue(resultado.Count() == 1);
        }
    }
}
