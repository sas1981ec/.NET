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
    //[TestClass]
    //public class TestServicioReporteGerencial
    //{
    //    private ServicioReporteGerencial _servicio;
    //    private Mock<IRepositorioProduccion> _mockRepositorioProduccion;
    //    private Mock<IRepositorioItemProduccion> _mockRepositorioItemProduccion;
    //    private Mock<IRepositorioDetalleCompra> _mockRepositorioDetalleCompra;

    //    [TestInitialize]
    //    public void Inicializar()
    //    {
    //        _mockRepositorioProduccion = new Mock<IRepositorioProduccion>();
    //        _mockRepositorioItemProduccion = new Mock<IRepositorioItemProduccion>();
    //        _mockRepositorioDetalleCompra = new Mock<IRepositorioDetalleCompra>();
    //        _servicio = new ServicioReporteGerencial(_mockRepositorioProduccion.Object, _mockRepositorioItemProduccion.Object, _mockRepositorioDetalleCompra.Object);
    //    }

    //    [TestMethod]
    //    public void TestObtenerCuadreDiarioCuandoNoExisteAlgo()
    //    {
    //        _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorFecha>())).Returns(new List<PRODUCCION>());
    //        _mockRepositorioItemProduccion.Setup(m => m.ObtenerItemsProduccionConDetalles(It.IsAny<FiltroItemProduccionPorFecha>())).Returns(new List<ITEM_PRODUCCION>());
    //        try
    //        {
    //            var resultado = _servicio.ObtenerCuadreDiario(DateTime.Now);
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.AreEqual("No existen datos para la consulta.", ex.Message);
    //            return;
    //        }
    //        Assert.Fail("Test Falló.");
    //    }

    //    [TestMethod]
    //    public void TestObtenerCuadreDiarioOk()
    //    {
    //        _mockRepositorioProduccion.Setup(m => m.ObtenerProduccionesConDetallesYRecetas(It.IsAny<FiltroProduccionPorFecha>())).Returns(new List<PRODUCCION> { new PRODUCCION()});
    //        _mockRepositorioItemProduccion.Setup(m => m.ObtenerItemsProduccionConDetalles(It.IsAny<FiltroItemProduccionPorFecha>())).Returns(new List<ITEM_PRODUCCION> { new ITEM_PRODUCCION()});
    //        var resultado = _servicio.ObtenerCuadreDiario(DateTime.Now);
    //        Assert.AreEqual(0, resultado.Item1.Count());
    //        Assert.AreEqual(0, resultado.Item2.Count());
    //    }
    //}
}
