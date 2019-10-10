using System;
using Domosti.CapaDatos.Modelos;

namespace Domosti.CapaDatos.Bases
{
    public class Liberador : IDisposable
    {
        protected ModeloDomostiContainer Contexto { get; set; }
        private bool _disposed;
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
                Contexto.Dispose();
            _disposed = true;
        }
    }
}
