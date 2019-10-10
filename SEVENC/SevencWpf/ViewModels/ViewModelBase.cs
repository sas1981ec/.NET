using SEVENC.Dominio.General;
using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System;
using System.ComponentModel;

namespace SevencWpf.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private bool _disposed;
        protected readonly ServicioWcfClient Servicio;
        protected readonly GestionAuditoria GestionAuditoria;
        public event PropertyChangedEventHandler PropertyChanged;

        internal ViewModelBase()
        {
            Servicio = new ServicioWcfClient();
            Servicio.ClientCredentials.UserName.UserName = Encriptar.HashPassword("EraDigital");
            Servicio.ClientCredentials.UserName.Password = Encriptar.HashPassword("M@ch1n3L3@rn1ng");
            GestionAuditoria = new GestionAuditoria();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Auditar(Auditoria auditoria)
        {
            Servicio.CrearAuditoria(auditoria);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                if (Servicio != null)
                    Servicio.Close();
            _disposed = true;
        }
    }

    internal abstract class ViewModelEspecializado : ViewModelBase
    {
        internal abstract void Autorizar();
    }
}
