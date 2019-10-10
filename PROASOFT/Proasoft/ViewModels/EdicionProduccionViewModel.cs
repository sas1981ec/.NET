using iTextSharp.text;
using iTextSharp.text.pdf;
using Proasoft.InfraestructuraVM;
using Proasoft.Views;
using PROASOFT.CapaAplicacion.Aplicacion.Contratos;
using PROASOFT.CapaAplicacion.Aplicacion.Interfaces;
using PROASOFT.CapaDominio.Entidades;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proasoft.ViewModels
{
    internal class EdicionOrdenProduccionViewModel : ViewModelBase
    {
        #region Campos
        private PRODUCCION _produccion;
        private ObservableCollection<DETALLE_PRODUCCION> _detallesProduccion;
        private DETALLE_PRODUCCION _detalleProduccionSeleccionado;
        private bool? _dialogResult;
        private Tuple<bool, Dictionary<ITEM, Tuple<double, bool>>> _datoBodega;
        #endregion

        #region Constructor
        public EdicionOrdenProduccionViewModel()
        {
            _produccion = new PRODUCCION();
            DetallesProduccion = new ObservableCollection<DETALLE_PRODUCCION>();
        }
        #endregion

        #region Propiedades
        public ICommand ComandoAgregarProducto
        {
            get
            {
                return new RelayCommand(AgregarProducto, PuedoAgregarProducto);
            }
        }

        public ICommand ComandoQuitarProducto
        {
            get
            {
                return new RelayCommand(QuitarProducto, PuedoQuitarProducto);
            }
        }

        public ICommand ComandoGrabar
        {
            get
            {
                return new RelayCommand(Grabar, PuedoGrabar);
            }
        }

        public ICommand ComandoCancelar
        {
            get
            {
                return new RelayCommand(Cancelar);
            }
        }

        public string Titulo
        {
            get
            {
                return "Nueva Producción";
            }
        }

        public PRODUCCION Produccion
        {
            get
            {
                return _produccion;
            }
            set
            {
                if (_produccion == value) return;
                _produccion = value;
                OnPropertyChanged("Produccion");
            }
        }


        public ObservableCollection<DETALLE_PRODUCCION> DetallesProduccion
        {
            get
            {
                return _detallesProduccion;
            }
            set
            {
                if (_detallesProduccion == value) return;
                _detallesProduccion = value;
                OnPropertyChanged("DetallesProduccion");
            }
        }


        public DETALLE_PRODUCCION DetalleProduccionSeleccionado
        {
            get
            {
                return _detalleProduccionSeleccionado;
            }
            set
            {
                if (_detalleProduccionSeleccionado == value) return;
                _detalleProduccionSeleccionado = value;
                OnPropertyChanged("DetalleProduccionSeleccionado");
            }
        }

        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            set
            {
                if (_dialogResult == value) return;
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }
        #endregion

        #region Metodos
        private bool PuedoAgregarProducto()
        {
            return true;
        }

        private bool PuedoQuitarProducto()
        {
            return DetalleProduccionSeleccionado != null;
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return DetallesProduccion.Count() > 0;
        }

        private void AgregarProducto()
        {
            var ventanaDetalle = new EdicionDetalleProduccion
            {
                DataContext = new EdicionDetalleProduccionViewModel(true, new DETALLE_PRODUCCION())
            };
            var resultado = ventanaDetalle.ShowDialog();
            if (resultado.HasValue && resultado.Value && !DetalleRepetido(((EdicionDetalleProduccionViewModel)ventanaDetalle.DataContext).DetalleProduccion.ID_RECETA, ((EdicionDetalleProduccionViewModel)ventanaDetalle.DataContext).DetalleProduccion.ID_EMPLEADO))
                DetallesProduccion.Add(((EdicionDetalleProduccionViewModel)ventanaDetalle.DataContext).DetalleProduccion);
        }

        private bool DetalleRepetido(int idReceta, int idEmpleado)
        {
            if (DetallesProduccion.Any(dp => dp.ID_RECETA == idReceta) && DetallesProduccion.Any(dp => dp.ID_EMPLEADO == idEmpleado))
            {
                MessageBox.Show("Detalle repetido.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return true;
            }
            return false;
        }

        private void QuitarProducto()
        {
            DetallesProduccion.Remove(DetalleProduccionSeleccionado);
        }

        protected virtual void Grabar()
        {
            var resultado = MessageBox.Show($"¿Está seguro de grabar la orden de producción?", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                if (ExisteEnBodega())
                {
                    var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
                    var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
                    _produccion.DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION>();
                    _produccion.ID_USUARIO = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario;
                    foreach (var item in DetallesProduccion)
                        _produccion.DETALLES_PRODUCCION.Add(new DETALLE_PRODUCCION { CANTIDAD = item.CANTIDAD, ID_EMPLEADO = item.ID_EMPLEADO, ID_RECETA = item.ID_RECETA });
                    var id = administradorProduccion.CrearProduccion(_produccion, _datoBodega.Item2);
                    administradorProduccion.LiberarRecursos();
                    MessageBox.Show($"Proceso Ok.{Environment.NewLine}Vea el detalle de la orden en el archivo pdf.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    CrearPdf(true, id);
                }
                else
                {
                    MessageBox.Show($"No existe al menos un insumo para la orden de producción.{Environment.NewLine}Vea el detalle en el archivo pdf.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                    CrearPdf(false, 0);
                }    
            }
        }

        private void CrearPdf(bool esExitoso, int id)
        {
            var documento = new Document(PageSize.A4);
            var ruta = "";
            var fecha = "";
            if (esExitoso)
            {
                ruta = $@"{AppDomain.CurrentDomain.BaseDirectory}/Archivos/{id}.pdf";
                documento.AddTitle($"Documento Orden Producción {id}");
            }
            else
            {
                fecha = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
                ruta = $@"{AppDomain.CurrentDomain.BaseDirectory}/Archivos/OrdenFallida_{fecha}.pdf";
                documento.AddTitle("Documento Orden Fallida");
            }
            var escritor = PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));
            documento.AddCreator("PROASOFT");
            documento.Open();
            if (esExitoso)
                documento.Add(new Paragraph($"Documento Orden Producción {id}"));
            else
                documento.Add(new Paragraph("ORDEN DE PRODUCCION FALLIDA"));
            var font = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            documento.Add(Chunk.NEWLINE);
            var tablaProductos = new PdfPTable(2)
            {
                WidthPercentage = 100
            };
            var nombre = new PdfPCell(new Phrase("Nombre", font))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.75f
            };
            var cantidad = new PdfPCell(new Phrase("Cantidad", font))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.75f
            };
            tablaProductos.AddCell(nombre);
            tablaProductos.AddCell(cantidad);
            var detallesProduccion = new List<DETALLE_PRODUCCION>();
            foreach (var detalle in DetallesProduccion)
            {
                if (detallesProduccion.Any(dp => dp.ID_RECETA == detalle.ID_RECETA))
                {
                    var cantidadAProducir = detallesProduccion.FirstOrDefault(dp => dp.ID_RECETA == detalle.ID_RECETA).CANTIDAD;
                    detallesProduccion.Remove(detallesProduccion.FirstOrDefault(dp => dp.ID_RECETA == detalle.ID_RECETA));
                    detallesProduccion.Add(new DETALLE_PRODUCCION { ID_RECETA = detalle.ID_RECETA, CANTIDAD = Convert.ToInt16(cantidadAProducir + detalle.CANTIDAD), RECETA = detalle.RECETA });
                }
                else
                    detallesProduccion.Add(new DETALLE_PRODUCCION { ID_RECETA = detalle.ID_RECETA, CANTIDAD = detalle.CANTIDAD, RECETA = detalle.RECETA });
            }
            foreach (var detalle in detallesProduccion)
            {
                nombre = new PdfPCell(new Phrase(detalle.RECETA.NOMBRE, font))
                {
                    BorderWidth = 0,
                };
                cantidad = new PdfPCell(new Phrase(detalle.CANTIDAD.ToString(), font))
                {
                    BorderWidth = 0,
                };
                tablaProductos.AddCell(nombre);
                tablaProductos.AddCell(cantidad);
            }
            documento.Add(tablaProductos);
            documento.Add(Chunk.NEWLINE);
            if(esExitoso)
                documento.Add(new Paragraph("INSUMOS PARA TRASPASAR DE BODEGA PRINCIPAL A BODEGA PRODUCCION"));
            else
                documento.Add(new Paragraph("INSUMOS FALTANTES"));

            var tablaInsumos = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            nombre = new PdfPCell(new Phrase("Nombre", font))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.75f
            };
            cantidad = new PdfPCell(new Phrase("Cantidad", font))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.75f
            };
            var medida = new PdfPCell(new Phrase("Medida", font))
            {
                BorderWidth = 0,
                BorderWidthBottom = 0.75f
            };
            tablaInsumos.AddCell(nombre);
            tablaInsumos.AddCell(cantidad);
            tablaInsumos.AddCell(medida);
            foreach (var item in _datoBodega.Item2.Where(i => i.Value.Item2 == esExitoso))
            {
                nombre = new PdfPCell(new Phrase(item.Key.NOMBRE, font))
                {
                    BorderWidth = 0,
                };
                cantidad = new PdfPCell(new Phrase(item.Value.Item1.ToString(), font))
                {
                    BorderWidth = 0,
                };
                medida = new PdfPCell(new Phrase(item.Key.MEDIDA.ETIQUETA, font))
                {
                    BorderWidth = 0,
                };
                tablaInsumos.AddCell(nombre);
                tablaInsumos.AddCell(cantidad);
                tablaInsumos.AddCell(medida);
            }
            documento.Add(tablaInsumos);
            documento.Close();
            escritor.Close();
            Process.Start(ruta);
        }

        private bool ExisteEnBodega()
        {
            var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
            var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
            _datoBodega = administradorProduccion.ExisteEnBodega(ObtenerDataRecetasCantidades());
            return _datoBodega.Item1;
        }

        private Dictionary<int, short> ObtenerDataRecetasCantidades()
        {
            var resultado = new Dictionary<int, short>();
            foreach (var detalle in DetallesProduccion)
            {
                if (resultado.Any(r => r.Key == detalle.ID_RECETA))
                {
                    var cantidad = resultado.FirstOrDefault(r => r.Key == detalle.ID_RECETA).Value;
                    resultado.Remove(detalle.ID_RECETA);
                    resultado.Add(detalle.ID_RECETA, Convert.ToInt16(detalle.CANTIDAD + cantidad));
                }
                else
                    resultado.Add(detalle.ID_RECETA, detalle.CANTIDAD);
            }
            return resultado;
        }
        #endregion
    }

    internal class EdicionProduccionViewModel : EdicionOrdenProduccionViewModel
    {
        #region Campos
        #endregion

        #region Constructor
        public EdicionProduccionViewModel() : base()
        {
        }
        #endregion

        #region Propiedades
        #endregion

        #region Metodos
        protected override void Grabar()
        {
            var resultado = MessageBox.Show($"¿Está seguro de grabar la producción?", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var ctx = new XmlApplicationContext("~/Springs/SpringProduccion.xml");
                var administradorProduccion = (IProduccion)ctx["AdministradorProduccion"];
                Produccion.DETALLES_PRODUCCION = new List<DETALLE_PRODUCCION>();
                Produccion.ID_USUARIO = ((LoginData)App.Current.Resources["LoginData"]).IdUsuario;
                foreach (var item in DetallesProduccion)
                    Produccion.DETALLES_PRODUCCION.Add(new DETALLE_PRODUCCION { CANTIDAD = item.CANTIDAD, ID_EMPLEADO = item.ID_EMPLEADO, ID_RECETA = item.ID_RECETA });
                administradorProduccion.CrearProduccionReal(Produccion);
                administradorProduccion.LiberarRecursos();
                MessageBox.Show($"Proceso Ok.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
        }
        #endregion
    }
}
