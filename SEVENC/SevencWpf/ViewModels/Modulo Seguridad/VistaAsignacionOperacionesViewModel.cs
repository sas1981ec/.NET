using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaAsignacionOperacionesViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly Rol _rol;
        private ObservableCollection<Operacion> _operaciones;
        private bool? _dialogResult;
        private List<object> _idsViejos;
        #endregion

        #region Constructor
        public VistaAsignacionOperacionesViewModel(ServicioWcfClient servicio, Rol rol)
        {
            _servicio = servicio;
            _rol = rol;
            CargarOperaciones();
        }
        #endregion

        #region Propiedades
        public ICommand ComandoGrabar
        {
            get
            {
                return new RelayCommand(Grabar);
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
                return string.Format("Asignar Operaciones a - {0}", _rol.Nombre);
            }
        }

        public ObservableCollection<Operacion> Operaciones
        {
            get
            {
                return _operaciones;
            }
            set
            {
                if (_operaciones == value) return;
                _operaciones = value;
                OnPropertyChanged("Operaciones");
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
        private void CargarOperaciones()
        {
            Operaciones = new ObservableCollection<Operacion>(_servicio.ObtenerOperaciones());
            ChequearOperacionesAsociadasARol();
            _idsViejos = new List<object>();
            foreach (var item in Operaciones.Where(o => o.EstaChequeada).Select(o => o.IdOperacion))
                _idsViejos.Add(item);
        }

        private void ChequearOperacionesAsociadasARol()
        {
            var operacionesDeRol = _servicio.ObtenerOperacionesPorIdRol(_rol.IdRol);
            foreach (var operacion in Operaciones)
            {
                if (operacionesDeRol.Any(o => o.IdOperacion == operacion.IdOperacion))
                    operacion.EstaChequeada = true;
            }
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private void Grabar()
        {
            if (!Operaciones.Any(o => o.EstaChequeada))
                return;
            _servicio.AsignarOperacionesARol(Operaciones.Where(o => o.EstaChequeada).Select(o => o.IdOperacion).ToList(), _rol.IdRol);
            GestionAuditoria.IdOperacion = 17;
            if (GestionAuditoria.PuedoAuditar())
            {
                var ids = new List<object>();
                foreach (var item in Operaciones.Where(o => o.EstaChequeada).Select(o => o.IdOperacion))
                    ids.Add(item);
                Auditar(GestionAuditoria.AuditarAsignacion(ids, _idsViejos, "RolOperacion", _rol.IdRol.ToString()));
            }
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
