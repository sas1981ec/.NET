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
    public class TestServicioProduccion
    {
        private ServicioProduccion _servicio;
        private Mock<IRepositorioProduccion> _mockRepositorioProduccion;
        private Mock<IRepositorioReceta> _mockRepositorioReceta;
        private Mock<IRepositorioItem> _mockRepositorioItem;
        private Mock<IRepositorioEmpleado> _mockRepositorioEmpleado;

        [TestInitialize]
        public void Inicializar()
        {
            _mockRepositorioProduccion = new Mock<IRepositorioProduccion>();
            _mockRepositorioReceta = new Mock<IRepositorioReceta>();
            _mockRepositorioItem = new Mock<IRepositorioItem>();
            _mockRepositorioEmpleado = new Mock<IRepositorioEmpleado>();
            _servicio = new ServicioProduccion(_mockRepositorioProduccion.Object, _mockRepositorioReceta.Object, _mockRepositorioItem.Object, _mockRepositorioEmpleado.Object);
        }

        [TestMethod]
        public void TestObtenerOrdenesProducciones()
        {
            _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionConDetalles(It.IsAny<FiltroOrdenProduccion>())).Returns(new List<PRODUCCION> { new PRODUCCION() });
            var resultado = _servicio.ObtenerOrdenesProducciones(DateTime.Now, DateTime.Now);
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestObtenerProducciones()
        {
            _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionConDetalles(It.IsAny<FiltroProduccion>())).Returns(new List<PRODUCCION> { new PRODUCCION() });
            var resultado = _servicio.ObtenerProducciones(DateTime.Now, DateTime.Now);
            Assert.IsTrue(resultado.Count() == 1);
        }

        [TestMethod]
        public void TestExisteEnBodegaCuandoNoHay()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerRecetasConItems(It.IsAny<FiltroRecetaPorId>())).Returns(new List<RECETA> { new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA>
            {
                new DETALLE_RECETA
                {
                    ID_ITEM = 1,
                    CANTIDAD = 10
                },
                new DETALLE_RECETA
                {
                    ID_ITEM = 2,
                    CANTIDAD = 20
                }
            } } });
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConStocks(It.IsAny<FiltroItemGeneral>())).Returns(new List<ITEM>
            {
                new ITEM
                {
                    ID_ITEM = 1,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 2 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 0 }
                },
                new ITEM
                {
                    ID_ITEM = 2,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 1 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 0 }
                },
                new ITEM
                {
                    ID_ITEM = 3,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 5 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 0 }
                },
            });
            var datos = new Dictionary<int, short>
            {
                { 1, 1 }
            };
            var resultado = _servicio.ExisteEnBodega(datos);
            Assert.IsFalse(resultado.Item1);
        }

        [TestMethod]
        public void TestExisteEnBodegaCuandoHay()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerRecetasConItems(It.IsAny<FiltroRecetaPorId>())).Returns(new List<RECETA> { new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA>
            {
                new DETALLE_RECETA
                {
                    ID_ITEM = 1,
                    CANTIDAD = 10
                },
                new DETALLE_RECETA
                {
                    ID_ITEM = 2,
                    CANTIDAD = 20
                }
            } } });
            _mockRepositorioItem.Setup(m => m.ObtenerItemsConStocks(It.IsAny<FiltroItemGeneral>())).Returns(new List<ITEM>
            {
                new ITEM
                {
                    ID_ITEM = 1,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 20 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 10 }
                },
                new ITEM
                {
                    ID_ITEM = 2,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 10 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 20 }
                },
                new ITEM
                {
                    ID_ITEM = 3,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 5 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 0 }
                },
            });
            var datos = new Dictionary<int, short>
            {
                { 1, 1 }
            };
            var resultado = _servicio.ExisteEnBodega(datos);
            Assert.IsTrue(resultado.Item1);
        }

        [TestMethod]
        public void TestCrearProduccion()
        {
            int resultado;
            _mockRepositorioProduccion.Setup(m => m.Agregar(It.IsAny<PRODUCCION>()));
            _mockRepositorioItem.Setup(m => m.ObtenerItemConStocks(It.IsAny<FiltroItemPorId>())).Returns(new ITEM
                {
                    ID_ITEM = 1,
                    STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL{ CANTIDAD = 20 },
                    STOCK_PRODUCCION = new STOCK_PRODUCCION { CANTIDAD = 10 }
                });
            _mockRepositorioItem.Setup(m => m.Actualizar(It.IsAny<ITEM>()));
            try
            {
                resultado = _servicio.CrearProduccion(new PRODUCCION(), new Dictionary<ITEM, Tuple<double, bool>> { { new ITEM { ID_ITEM = 1 }, new Tuple<double, bool>(12, true) } });
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test Falló. {ex.Message}");
                return;
            }
            Assert.AreEqual(0, resultado);
        }

        [TestMethod]
        public void TesObtenerEmpleados()
        {
            _mockRepositorioEmpleado.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroEmpleadoActivo>())).Returns(new List<EMPLEADO> { new EMPLEADO() });
            var resultado = _servicio.ObtenerEmpleados();
            Assert.IsTrue(resultado.Count() == 1);
        }

        //[TestMethod]
        //public void TestCrearProduccionCuandoNoExisteUnInsumo()
        //{
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionHoy>())).Returns(new List<PRODUCCION>());
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION { DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION { RECETA = new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA> { new DETALLE_RECETA() } } } } } });
        //    _mockRepositorioStockProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroStockProduccion>())).Returns(new List<STOCK_PRODUCCION> { new STOCK_PRODUCCION { ITEM = new ITEM { ID_ITEM = 1} } });
        //    try
        //    {
        //        _servicio.CrearProduccion(new PRODUCCION());
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe el insumo cuyo id es 0 en la bodega de producción.", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestCrearProduccionCuandoNoExisteUnStockProduccion()
        //{
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionHoy>())).Returns(new List<PRODUCCION>());
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION { DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION { RECETA = new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA> { new DETALLE_RECETA() } } } } } });
        //    _mockRepositorioStockProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroStockProduccion>())).Returns(new List<STOCK_PRODUCCION> { new STOCK_PRODUCCION { ITEM = new ITEM()} });
        //    try
        //    {
        //        _servicio.CrearProduccion(new PRODUCCION());
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe Stock Producción cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestCrearProduccionOk()
        //{
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionHoy>())).Returns(new List<PRODUCCION>());
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION { DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION { RECETA = new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA> { new DETALLE_RECETA()} } } } } });
        //    _mockRepositorioStockProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroStockProduccion>()));
        //    Assert.IsTrue(true);
        //}

        //[TestMethod]
        //public void TestActualizarProduccionCuandoNoExisteId()
        //{
        //    _mockRepositorioDetalleProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleProduccionPorId>())).Returns(new List<DETALLE_PRODUCCION>());
        //    try
        //    {
        //        _servicio.ActualizarProduccion(new DETALLE_PRODUCCION());
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe detalle de producción cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestActualizarProduccionCuandoNoSePuedeModificar()
        //{
        //    _mockRepositorioDetalleProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleProduccionPorId>())).Returns(new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION() });
        //    try
        //    {
        //        _servicio.ActualizarProduccion(new DETALLE_PRODUCCION { PRODUCCION = new PRODUCCION { FECHA = DateTime.Now.AddDays(-2) } });
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No se puede modificar el detalle de producción.", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestActualizarProduccionCuandoNoExisteIdProduccion1()
        //{
        //    _mockRepositorioDetalleProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleProduccionPorId>())).Returns(new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION() });
        //    _mockRepositorioDetalleProduccion.Setup(m => m.Actualizar(It.IsAny<DETALLE_PRODUCCION>()));
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION>());
        //    try
        //    {
        //        _servicio.ActualizarProduccion(new DETALLE_PRODUCCION { PRODUCCION = new PRODUCCION { FECHA = DateTime.Now } });
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe producción cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestActualizarProduccionCuandoNoExisteIdProduccion2()
        //{
        //    _mockRepositorioDetalleProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleProduccionPorId>())).Returns(new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION() });
        //    _mockRepositorioDetalleProduccion.Setup(m => m.Actualizar(It.IsAny<DETALLE_PRODUCCION>()));
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION() });
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION>());
        //    try
        //    {
        //        _servicio.ActualizarProduccion(new DETALLE_PRODUCCION { PRODUCCION = new PRODUCCION { FECHA = DateTime.Now } });
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("No existe producción cuyo id es 0", ex.Message);
        //        return;
        //    }
        //    Assert.Fail("Test Falló.");
        //}

        //[TestMethod]
        //public void TestActualizarProduccionOk()
        //{
        //    _mockRepositorioDetalleProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroDetalleProduccionPorId>())).Returns(new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION() });
        //    _mockRepositorioDetalleProduccion.Setup(m => m.Actualizar(It.IsAny<DETALLE_PRODUCCION>()));
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION() });
        //    _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorId>())).Returns(new List<PRODUCCION> { new PRODUCCION { DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION> { new DETALLE_PRODUCCION { RECETA = new RECETA { DETALLES_RECETA = new List<DETALLE_RECETA>()} } } } });
        //    _servicio.ActualizarProduccion(new DETALLE_PRODUCCION { PRODUCCION = new PRODUCCION { FECHA = DateTime.Now} });
        //    Assert.IsTrue(true);
        //}

        [TestMethod]
        public void TestObtenerRecetas()
        {
            _mockRepositorioReceta.Setup(m => m.ObtenerObjetos(It.IsAny<FiltroRecetasActiva>())).Returns(new List<RECETA> { new RECETA() });
            var resultado = _servicio.ObtenerRecetas();
            Assert.AreEqual(1, resultado.Count());
        }
    }
}
