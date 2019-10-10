using SEVENC.Aplicacion.Aplicacion.Interfaces.Modulo_Seguridad;
using SEVENC.Dominio.Entidades;
using SEVENC.Dominio.Dominio.Interfaces_Repositorios.Modulo_Seguridad.Auditoria;
using System;

namespace SEVENC.Dominio.ServiciosDominio.Modulo_Seguridad
{
    public class ServicioAuditoria : IAuditoria
    {
        private readonly IRepositorioAuditoria _repositorioAuditoria;

        public ServicioAuditoria(IRepositorioAuditoria repositorioAuditoria)
        {
            _repositorioAuditoria = repositorioAuditoria;
        }

        public void CrearAuditoria(Auditoria auditoria)
        {
            auditoria.Fecha = DateTime.Now;
            _repositorioAuditoria.Agregar(auditoria);
        }

        public void LiberarRecursos()
        {
            _repositorioAuditoria.LiberarRecursos();
        }
    }
}
