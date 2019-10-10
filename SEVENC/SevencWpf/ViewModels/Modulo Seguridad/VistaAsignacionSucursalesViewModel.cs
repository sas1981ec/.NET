using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaAsignacionSucursalesViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly Empresa _empresa;
        private ObservableCollection<Sucursal> _sucursales;
        private bool? _dialogResult;
        private List<object> _idsViejos;
        #endregion

        #region Constructor
        public VistaAsignacionSucursalesViewModel(ServicioWcfClient servicio, Empresa empresa)
        {
            _servicio = servicio;
            _empresa = empresa;
            CargarSucursales();
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
                return string.Format("Asignar Sucursales a - {0}", _empresa.NombreComercial);
            }
        }

        public ObservableCollection<Sucursal> Sucursales
        {
            get
            {
                return _sucursales;
            }
            set
            {
                if (_sucursales == value) return;
                _sucursales = value;
                OnPropertyChanged("Sucursales");
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
        private void CargarSucursales()
        {
            Sucursales = new ObservableCollection<Sucursal>(_servicio.ObtenerSucursales());
            ChequearSucursalesAsociadasAEmpresa();
            _idsViejos = new List<object>();
            foreach (var item in Sucursales.Where(s => s.EstaChequeada).Select(s => s.IdSucursal))
                _idsViejos.Add(item);
        }

        private void ChequearSucursalesAsociadasAEmpresa()
        {
            var sucursalesDeEmpresa = _servicio.ObtenerSucursalesPorIdEmpresa(_empresa.IdEmpresa);
            foreach (var sucursal in Sucursales)
            {
                if(sucursalesDeEmpresa.Any(s => s.IdSucursal == sucursal.IdSucursal))
                    sucursal.EstaChequeada = true;
            }
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private void Grabar()
        {
            if (!Sucursales.Any(s => s.EstaChequeada))
                return;
            _servicio.AsignarSucursalesAEmpresa(Sucursales.Where(s => s.EstaChequeada).Select(s => s.IdSucursal).ToList(), _empresa.IdEmpresa);
            GestionAuditoria.IdOperacion = 4;
            if (GestionAuditoria.PuedoAuditar())
            {
                var ids = new List<object>();
                foreach (var item in Sucursales.Where(s => s.EstaChequeada).Select(s => s.IdSucursal))
                    ids.Add(item);
                Auditar(GestionAuditoria.AuditarAsignacion(ids, _idsViejos, "EmpresaSucursal", _empresa.IdEmpresa.ToString()));
            }
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        #endregion
    }
}
