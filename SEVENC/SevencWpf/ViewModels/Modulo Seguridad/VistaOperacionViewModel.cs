using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaOperacionViewModel : ViewModelEspecializado
    {
        #region Campos
        private Operacion _operacionSeleccionada;
        private ObservableCollection<Operacion> _operaciones;
        private Visibility _esVisibleActualizarOperacion;
        #endregion

        #region Constructor
        public VistaOperacionViewModel()
        {
            Autorizar();
            CargarOperaciones();
        }
        #endregion

        #region Propiedades
        public ICommand CambiarEstadoOperacion
        {
            get
            {
                return new RelayCommand(CambiarEstado, PuedoCambiarEstadoOperacion);
            }
        }

        public ICommand CambiarEstadoAuditable
        {
            get
            {
                return new RelayCommand(EstadoAuditable, PuedoCambiarEstadoAuditable);
            }
        }

        public ObservableCollection<Operacion> Operaciones
        {
            get { return _operaciones; }
            set
            {
                if (_operaciones == value) return;
                _operaciones = value;
                OnPropertyChanged("Operaciones");
            }
        }

        public Operacion OperacionSeleccionada
        {
            get { return _operacionSeleccionada; }
            set
            {
                if (_operacionSeleccionada == value) return;
                _operacionSeleccionada = value;
                if (_operacionSeleccionada == null) return;
                OnPropertyChanged("OperacionSeleccionada");
            }
        }

        public Visibility EsVisibleActualizarOperacion
        {
            get { return _esVisibleActualizarOperacion; }
            set
            {
                if (_esVisibleActualizarOperacion == value) return;
                _esVisibleActualizarOperacion = value;
                OnPropertyChanged("EsVisibleActualizarOperacion");
            }
        }
        #endregion

        #region Metodos
        private void CargarOperaciones()
        {
            Operaciones = new ObservableCollection<Operacion>(Servicio.ObtenerOperaciones());
            GestionAuditoria.IdOperacion = 20;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarConsulta("Operacion"));
        }

        private bool PuedoCambiarEstadoOperacion()
        {
            return OperacionSeleccionada != null;
        }

        private bool PuedoCambiarEstadoAuditable()
        {
            return OperacionSeleccionada != null;
        }

        private void CambiarEstado()
        {
            var mbr = MessageBox.Show($"Esta de cambiar estado de la operación - {OperacionSeleccionada.Nombre}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var operacionVieja = OperacionSeleccionada.Clone();
            OperacionSeleccionada.EstaActiva = !OperacionSeleccionada.EstaActiva;
            Servicio.ActualizarOperacion(OperacionSeleccionada);
            GestionAuditoria.IdOperacion = 19;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(OperacionSeleccionada, operacionVieja, "Operacion", OperacionSeleccionada.IdOperacion.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CargarOperaciones();
        }

        private void EstadoAuditable()
        {
            var mbr = MessageBox.Show($"Esta de cambiar estado de auditable a la operación - {OperacionSeleccionada.Nombre}", "Confirmación", MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK != mbr) return;
            var operacionVieja = OperacionSeleccionada.Clone();
            OperacionSeleccionada.EsAuditable = !OperacionSeleccionada.EsAuditable;
            Servicio.ActualizarOperacion(OperacionSeleccionada);
            GestionAuditoria.IdOperacion = 19;
            if (GestionAuditoria.PuedoAuditar())
                Auditar(GestionAuditoria.AuditarActualizacion(OperacionSeleccionada, operacionVieja, "Operacion", OperacionSeleccionada.IdOperacion.ToString()));
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            CargarOperaciones();
        }

        internal override void Autorizar()
        {
            EsVisibleActualizarOperacion = ((LoginData)App.Current.Resources["LoginData"]).Operaciones.ContainsKey(19) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
