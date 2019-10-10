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
    internal class VistaStockViewModel : ViewModelBase
    {
        #region Campos
        private ObservableCollection<Stock> _stocks;
        #endregion

        #region Constructor
        public VistaStockViewModel()
        {
            CargarStock();
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

        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set
            {
                if (_stocks == value) return;
                _stocks = value;
                OnPropertyChanged("Stocks");
            }
        }
        #endregion

        #region Metodos
        private void CargarStock()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringStock.xml");
            var administradorStock = (IStock)ctx["AdministradorStock"];
            Stocks = new ObservableCollection<Stock>(administradorStock.ObtenerStockItems().OrderBy(s => s.NombreItem));
            administradorStock.LiberarRecursos();
        }

        private bool PuedoExportar()
        {
            return Stocks != null && Stocks.Count() > 0;
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
                    foreach (var stock in Stocks)
                    {
                        EscribirRegistros(ws, indice, stock);
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
            ws.Range["A1"].Offset[0, 0].Value = "Insumo";
            ws.Range["A1"].Offset[0, 1].Value = "Medida";
            ws.Range["A1"].Offset[0, 2].Value = "Cantidad Bodeda Principal";
            ws.Range["A1"].Offset[0, 3].Value = "Cantidad Bodega Produccion";
            ws.Range["A1"].Offset[0, 4].Value = "Total";
            ws.Range["A1", "E1"].Font.Bold = true;
        }

        private void EscribirRegistros(Microsoft.Office.Interop.Excel.Worksheet ws, int indice, Stock stock)
        {
            ws.Range["A" + indice.ToString()].Offset[0, 0].Value = stock.NombreItem;
            ws.Range["A" + indice.ToString()].Offset[0, 1].Value = stock.EtiquetaMedida;
            ws.Range["A" + indice.ToString()].Offset[0, 2].Value = stock.CantidadBodedaPrincipal;
            ws.Range["A" + indice.ToString()].Offset[0, 3].Value = stock.CantidadBodegaProduccion;
            ws.Range["A" + indice.ToString()].Offset[0, 4].Value = stock.Total;
        }
        #endregion
    }
}
