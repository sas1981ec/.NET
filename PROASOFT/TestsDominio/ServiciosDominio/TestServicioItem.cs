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
    public class TestServicioItem
    {
        private ServicioItem _servicio;
        private Mock<IRepositorioItem> _mockRepositorioItem;
        private Mock<IRepositorioMedida> _mockRepositorioMedida;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioItem = new Mock<IRepositorioItem>();
            _mockRepositorioMedida = new Mock<IRepositorioMedida>();
            _servicio = new ServicioItem(_mockRepositorioItem.Object, _mockRepositorioMedida.Object);
        }

        [TestMethod]
        public void TestObtenerItems()
        {
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConMedida(It.IsAny<FiltroItemGeneral>())).Returns(new List<ITEM>());
            var resultado = _servicio.ObtenerItems();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestObtenerMedidas()
        {
            _mockRepositorioMedida.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroMedida>())).Returns(new List<MEDIDA>());
            var resultado = _servicio.ObtenerMedidas();
            Assert.AreEqual(0, resultado.Count());
        }

        [TestMethod]
        public void TestCrearItem()
        {
            _mockRepositorioItem.Setup(m => m.Agregar(It.IsAny<ITEM>()));
            _servicio.CrearItem(new ITEM());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActualizarItemCuandoNoExisteId()
        {
            _mockRepositorioItem.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroItemPorId>())).Returns(new List<ITEM>());
            try
            {
                _servicio.ActualizarItem(new ITEM());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No existe item cuyo id es 0", ex.Message);
                return;
            }
            Assert.Fail("Test Falló.");
        }

        [TestMethod]
        public void TestActualizarItemOk()
        {
            _mockRepositorioItem.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroItemPorId>())).Returns(new List<ITEM> { new ITEM()});
            _mockRepositorioItem.Setup(m => m.Actualizar(It.IsAny<ITEM>()));
            _servicio.ActualizarItem(new ITEM());
            Assert.IsTrue(true);
        }

        //[TestMethod]
        //public void TestExisteEnBodegaItemNuevoCuandoNoExisteId()
        //{
        //    _mockRepositorioItem.Setup(m => m.ObtenerItemConDetallesComprasYDetallesItemProduccion(It.IsAny<FiltroItemPorId>()));
        //    try
        //    {
        //        _servicio.ExisteEnBodegaItem(0, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe item cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestExisteEnBodegaItemNuevo()
        //{
        //    _mockRepositorioItem.Setup(m => m.ObtenerItemConDetallesComprasYDetallesItemProduccion(It.IsAny<FiltroItemPorId>())).Returns(new ITEM { DETALLES_COMPRAS = new List<DETALLE_COMPRA> { new DETALLE_COMPRA()}, DETALLES_ITEM_PRODUCCION = new List<DETALLE_ITEM_PRODUCCION> { new DETALLE_ITEM_PRODUCCION()} });
        //    var resultado = _servicio.ExisteEnBodegaItem(0, 2);
        //    Assert.IsFalse(resultado);
        //}

        //[TestMethod]
        //public void TestExisteEnBodegaItemModificarCuandoNoExisteId()
        //{
        //    _mockRepositorioItem.Setup(m => m.ObtenerItemConDetallesComprasYDetallesItemProduccion(It.IsAny<FiltroItemPorId>()));
        //    try
        //    {
        //        _servicio.ExisteEnBodegaItem(0, 0, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe item cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestExisteEnBodegaItemModificar()
        //{
        //    _mockRepositorioItem.Setup(m => m.ObtenerItemConDetallesComprasYDetallesItemProduccion(It.IsAny<FiltroItemPorId>())).Returns(new ITEM { DETALLES_COMPRAS = new List<DETALLE_COMPRA> { new DETALLE_COMPRA { CANTIDAD= 5}, new DETALLE_COMPRA(), new DETALLE_COMPRA() }, DETALLES_ITEM_PRODUCCION = new List<DETALLE_ITEM_PRODUCCION> { new DETALLE_ITEM_PRODUCCION() } });
        //    var resultado = _servicio.ExisteEnBodegaItem(0, 0, 2);
        //    Assert.IsTrue(resultado);
        //}
    }
}
