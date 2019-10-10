using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views.Modulo_Seguridad;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaSucursalViewModel : ViewModelEspecializado
    {
        #region Campos
        private Sucursal _sucursalSeleccionada;
        private ObservableCollection<Sucursal> _sucursales;
        private Visibility _esVisibleCrearSucursal;
        private Visibility _esVisibleActualizarSucursal;
        #endregion

        #region Constructor
        public VistaSucursalViewModel()
        {
            Autorizar();
            CargarSucursales();
        }
        #endregion

        #region Propiedades
        public ICommand NuevaSucursal
        {
            get
            {
                return new RelayCommand(CrearSucursal, PuedoCrearSucursal);
            }
        }

        public ICommand EditarSucursal
        {
            get
            {
                return new RelayCommand(ActualizarSucursal, PuedoActualizarSucursal);
            }
        }

        public ICommand EliminarSucursal
        {
            get
            {
                return new RelayCommand(BorrarSucursal, PuedoBorrarSucursal);
            }
        }

        public ObservableCollection<Sucursal> Sucursales
        {
            get { return _sucursales; }
            set
            {
                if (_sucursales == value) return;
                _sucursales = value;
                OnPropertyChanged("Sucursales");
            }
        }

        public Sucursal SucursalSeleccionada
        {
            get { return _sucursalSeleccionada; }
            set
            {
                if (_sucursalSeleccionada == value) return;
                _sucursalSeleccionada = value;
                if (_sucursalSeleccionada == null) return;
                OnPropertyChanged("SucursalSeleccionada");
            }
        }

        public Visibility EsVisibleCrearSucursal
        {
            get { return _esVisibleCrearSucursal; }
            set
            {
                if (_esVisibleCrearSucursal == value) return;
                _esVisibleCrearSucursal = value;
                OnPropertyChanged("EsVisibleCrearSucursal");
            }
        }

        public Visibility EsVisibleActualizarSucursal
        {
            get { return _esVisibleActualizarSucursal; }
            set
            {
                if (_esVisibleActualizarSucursal == value) return;
                _esVisibleActualizarSucursal = value;
                OnPropertyChanged("EsVisibleActualizarSucursal");
            }
        }
        #endregion

        #region Metodos
        private void CargarSucursales()
        {
            Sucursales = new ObservableCollection<Sucursal>(Servicio.ObtenerSucursales());
            SucursalSeleccionada = Sucursales.FirstOrDefault();
            GestionAuditoria.IdOperacion = 14;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarConsulta("Sucursal"));
        }

        private bool PuedoCrearSucursal()
        {
            return true;
        }

        private bool PuedoActualizarSucursal()
        {
            return SucursalSeleccionada != null;
        }

        private bool PuedoBorrarSucursal()
        {
            return SucursalSeleccionada != null;
        }

        private void CrearSucursal()
        {
            var sucursal = new Sucursal();
            var edicionSucursal = new VistaEdicionSucursal
            {
                DataContext = new VistaEdicionSucursalViewModel(Servicio, true, sucursal)
            };
            var resultado = edicionSucursal.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 12;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarCreacion(sucursal, "Sucursal", sucursal.IdSucursal.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void CierreEdicion(bool? resultado)
        {
            if (resultado == null || !resultado.Value) return;
            CargarSucursales();
        }

        private void ActualizarSucursal()
        {
            var sucursalVieja = SucursalSeleccionada.Clone();
            var edicionSucursal = new VistaEdicionSucursal { DataContext = new VistaEdicionSucursalViewModel(Servicio, false, SucursalSeleccionada) };
            var resultado = edicionSucursal.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                GestionAuditoria.IdOperacion = 13;
                if (GestionAuditoria.PuedoAuditar())
                    Auditar(GestionAuditoria.AuditarActualizacion(SucursalSeleccionada, sucursalVieja, "Sucursal", SucursalSeleccionada.IdSucursal.ToString()));
            }
            CierreEdicion(resultado);
        }

        private void BorrarSucursal()
        {
            var mbr = MessageBox.Show($"Esta seguro de eliminar la sucursal - {SucursalSeleccionada.Nombre}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var sucursalVieja = SucursalSeleccionada.Clone();
            SucursalSeleccionada.EstaEliminada = true;
            Servicio.ActualizarSucursal(SucursalSeleccionada);
            GestionAuditoria.IdOperacion = 13;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(SucursalSeleccionada, sucursalVieja, "Sucursal", SucursalSeleccionada.IdSucursal.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CierreEdicion(true);
        }

        internal override void Autorizar()
        {
            EsVisibleCrearSucursal = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(12) ? Visibility.Visible : Visibility.Collapsed;
            EsVisibleActualizarSucursal = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(13) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
