using Proasoft.InfraestructuraVM;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using Spring.Context.Support;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class VistaReporteGerencialViewModel : ViewModelBase
    {
        #region Campos
        private ObservableCollection<Produccion> _produccion;
        private Produccion _productoSeleccionado;
        private ObservableCollection<DetalleProduccion> _detallesProduccion;
        private ObservableCollection<Verificador> _verificador;
        private string _fecha;
        #endregion

        #region Constructor
        public VistaReporteGerencialViewModel()
        {
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
           // CargarDatos();
        }
        #endregion

        #region Propiedades
        public ICommand ExportarExcel
        {
            get
            {
                return new RelayCommand(Exportar, PuedoExportar);
            }
        }

        public ICommand Buscar
        {
            get
            {
                return new RelayCommand(CargarDatos, PuedoBuscar);
            }
        }

        public ObservableCollection<Produccion> Produccion
        {
            get { return _produccion; }
            set
            {
                if (_produccion == value) return;
                _produccion = value;
                OnPropertyChanged("Produccion");
            }
        }

        public Produccion ProductoSeleccionado
        {
            get { return _productoSeleccionado; }
            set
            {
                if (_productoSeleccionado == value) return;
                _productoSeleccionado = value;
                OnPropertyChanged("ProductoSeleccionado");
                if(ProductoSeleccionado != null)
                    CargarDetallesProduccion();
            }
        }

        public ObservableCollection<DetalleProduccion> DetallesProduccion
        {
            get { return _detallesProduccion; }
            set
            {
                if (_detallesProduccion == value) return;
                _detallesProduccion = value;
                OnPropertyChanged("DetallesProduccion");
            }
        }

        public ObservableCollection<Verificador> Verificador
        {
            get { return _verificador; }
            set
            {
                if (_verificador == value) return;
                _verificador = value;
                OnPropertyChanged("Verificador");
            }
        }

        public string Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha == value) return;
                _fecha = value;
                OnPropertyChanged("Fecha");
            }
        }
        #endregion

        #region Metodos
        private void CargarDatos()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringReporteGerencial.xml");
            var administradorReporteGerencial = (IReporteGerencial)ctx["AdministradorReporteGerencial"];
            var datos = administradorReporteGerencial.ObtenerCuadreDiario(ObtenerFecha(Fecha));
            Produccion = new ObservableCollection<Produccion>(datos.Item1.OrderBy(i => i.NombreProducto));
            Verificador = new ObservableCollection<Verificador>(datos.Item2.OrderBy(i => i.NombreItem));
            administradorReporteGerencial.LiberarRecursos();
        }

        private bool PuedoExportar()
        {
            return Produccion != null && Produccion.Count() > 0;
        }

        private bool PuedoBuscar()
        {
            return true;
        }

        private void Exportar()
        {
            EstaOcupado = true;
            IniciarHiloExportarGeneral();
        }

        private async void IniciarHiloExportarGeneral()
        {
            try
            {
                await Task.Run(() =>
                {
                    var excel = new Microsoft.Office.Interop.Excel.Application();
                    var libro = excel.Workbooks.Add();
                    var ws = (Microsoft.Office.Interop.Excel.Worksheet)libro.ActiveSheet;
                    EscribirCabecera(ws);
                    var indice = 2;
                    foreach (var item in Produccion)
                    {
                        EscribirRegistros(ws, indice, item);
                        indice++;
                    }
                    EscribirCabeceraVerificador(ws, indice);
                    indice++;
                    foreach (var item in Verificador)
                    {
                        EscribirRegistrosVerificador(ws, indice, item);
                        indice++;
                    }
                    excel.Visible = true;
                    libro.Activate();
                    EstaOcupado = false;
                });
            }
            catch (Exception)
            {
                EstaOcupado = false;
                throw;
            }
        }

        private void EscribirCabecera(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            ws.Range["A1"].Offset[0, 0].Value = "Nombre Producto";
            ws.Range["A1"].Offset[0, 1].Value = "Cantidad";
            ws.Range["A1"].Offset[0, 2].Value = "Costo Producción Unitaria";
            ws.Range["A1"].Offset[0, 3].Value = "Costo Producción Total";
            ws.Range["A1", "D1"].Font.Bold = true;
        }

        private void EscribirRegistros(Microsoft.Office.Interop.Excel.Worksheet ws, int indice, Produccion produccion)
        {
            ws.Range["A" + indice.ToString()].Offset[0, 0].Value = produccion.NombreProducto;
            ws.Range["A" + indice.ToString()].Offset[0, 1].Value = produccion.Cantidad;
            ws.Range["A" + indice.ToString()].Offset[0, 2].Value = produccion.CostoProduccionUnitaria;
            ws.Range["A" + indice.ToString()].Offset[0, 3].Value = produccion.CostoProduccionTotal;
        }

        private void EscribirCabeceraVerificador(Microsoft.Office.Interop.Excel.Worksheet ws, int indice)
        {
            ws.Range["A" + indice.ToString()].Offset[0, 0].Value = "Insumo";
            ws.Range["A" + indice.ToString()].Offset[0, 1].Value = "Medida";
            ws.Range["A" + indice.ToString()].Offset[0, 2].Value = "Cantidad Ingresada";
            ws.Range["A" + indice.ToString()].Offset[0, 3].Value = "Cantidad Producida";
            ws.Range["A" + indice.ToString()].Offset[0, 4].Value = "Cantidad Restante";
            ws.Range["A1", "E1"].Font.Bold = true;
        }

        private void EscribirRegistrosVerificador(Microsoft.Office.Interop.Excel.Worksheet ws, int indice, Verificador verificador)
        {
            ws.Range["A" + indice.ToString()].Offset[0, 0].Value = verificador.NombreItem;
            ws.Range["A" + indice.ToString()].Offset[0, 1].Value = verificador.Medida;
            ws.Range["A" + indice.ToString()].Offset[0, 2].Value = verificador.CantidadIngresada;
            ws.Range["A" + indice.ToString()].Offset[0, 3].Value = verificador.CantidadProducida;
            ws.Range["A" + indice.ToString()].Offset[0, 4].Value = verificador.CantidadRestante;
        }

        private void CargarDetallesProduccion()
        {
            DetallesProduccion = new ObservableCollection<DetalleProduccion>(ProductoSeleccionado.DetallesProduccion);
        }
        #endregion
    }
}
