using System;
using System.ComponentModel;

namespace Proasoft.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _estaOcupado;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected DateTime ObtenerFecha(string fechaTexto)
        {
            var fecha = fechaTexto.Split('/');
            return new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
        }

        public bool EstaOcupado
        {
            get
            {
                return _estaOcupado;
            }
            set
            {
                if (_estaOcupado == value)
                    return;
                _estaOcupado = value;
                OnPropertyChanged("EstaOcupado");
            }
        }
    }
}
