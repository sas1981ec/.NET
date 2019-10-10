using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using SevencWpf.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaSeleccionEmpresaViewModel : ViewModelBase
    {
        #region Campos
        private readonly Window _vista;
        private readonly ObservableCollection<Empresa> _empresas;
        private Empresa _empresaSeleccionada;
        #endregion

        #region Constructor
        public VistaSeleccionEmpresaViewModel(Window vista, ObservableCollection<Empresa> empresas)
        {
            _vista = vista;
            _empresas = empresas;
            EmpresaSeleccionada = Empresas.FirstOrDefault();
        }
        #endregion

        #region Propiedades
        public ICommand ComandoAceptar
        {
            get
            {
                return new RelayCommand(Aceptar);
            }
        }

        public ObservableCollection<Empresa> Empresas
        {
            get { return _empresas; }
        }

        public Empresa EmpresaSeleccionada
        {
            get { return _empresaSeleccionada; }
            set
            {
                if (_empresaSeleccionada == value) return;
                _empresaSeleccionada = value;
                if (_empresaSeleccionada == null) return;
                OnPropertyChanged("EmpresaSeleccionada");
            }
        }
        #endregion

        #region Metodos
        private void Aceptar()
        {
            ((LoginData)App.Current.Resources["LoginData"]).EmpresaSeleccionada = EmpresaSeleccionada;
            var vistaPrincipal = new VistaPrincipal();
            vistaPrincipal.Show();
            _vista.Close();
        }
        #endregion
    }
}
