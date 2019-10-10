using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Dominio.Filtros;
using PROASOFT.CapaDominio.Dominio.InterfacesRepositorios;
using PROASOFT.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PROASOFT.CapaDominio.ServiciosDominio
{
    public class ServicioItem : IItem
    {
        private readonly IRepositorioItem _repositorio;
        private readonly IRepositorioMedida _repositorioMedida;

        public ServicioItem(IRepositorioItem repositorio, IRepositorioMedida repositorioMedida)
        {
            _repositorio = repositorio;
            _repositorioMedida = repositorioMedida;
        }

        public IEnumerable<ITEM> ObtenerItems()
        {
            return _repositorio.ObtenerItemsConMedida(new FiltroItemGeneral()).OrderBy(i => i.NOMBRE);
        }

        public IEnumerable<MEDIDA> ObtenerMedidas()
        {
            return _repositorioMedida.ObtenerObjetos(new FiltroMedida()).OrderBy(m => m.ETIQUETA);
        }

        public void CrearItem(ITEM item)
        {
            item.STOCK_BODEGA_PRINCIPAL = new STOCK_BODEGA_PRINCIPAL();
            item.STOCK_PRODUCCION = new STOCK_PRODUCCION();
            _repositorio.Agregar(item);
        }

        public void ActualizarItem(ITEM item)
        {
            var itemAModificar = _repositorio.ObtenerObjetos(new FiltroItemPorId(item.ID_ITEM)).FirstOrDefault();
            if (itemAModificar == null)
                throw new ApplicationException($"No existe item cuyo id es {item.ID_ITEM}");
            itemAModificar.ESTA_ACTIVO = item.ESTA_ACTIVO;
            _repositorio.Actualizar(itemAModificar);
        }

        public void LiberarRecursos()
        {
            _repositorio.LiberarRecursos();
            _repositorioMedida.LiberarRecursos();
        }
    }

    public class FiltroItemGeneral : IFiltros<ITEM>
    {
        public Expression<Func<ITEM, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM>(i => i.ID_ITEM > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroMedida : IFiltros<MEDIDA>
    {
        public Expression<Func<MEDIDA, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<MEDIDA>(m => m.ID_MEDIDA > 0);
            return filtro.SastifechoPor();
        }
    }

    public class FiltroItemPorId : IFiltros<ITEM>
    {
        private readonly int _id;

        public FiltroItemPorId(int id)
        {
            _id = id;
        }

        public Expression<Func<ITEM, bool>> SastifechoPor()
        {
            var filtro = new FiltroDirecto<ITEM>(i => i.ID_ITEM == _id);
            return filtro.SastifechoPor();
        }
    }
}
