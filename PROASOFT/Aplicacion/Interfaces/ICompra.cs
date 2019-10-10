using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;

namespace PROASOFT.CapaAplicacion.Aplicacion.Interfaces
{
    public interface ICompra : IBaseProveedor, IBaseItem, IBase
    {
        IEnumerable<COMPRA> ObtenerCompras(DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<DETALLE_COMPRA> ObtenerDetallesCompra(int idCompra);
        void RegistrarNuevaCompra(COMPRA compra);
        void ConfirmarCompra(int idCompra, short idUsuario);
        void ModificarDetalleCompra(DETALLE_COMPRA detalleCompra, IEnumerable<short> idRoles);
    }
}
