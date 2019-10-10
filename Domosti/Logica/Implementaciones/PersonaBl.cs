using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using System.Web.Security;
using Domosti.CapaDatos.Interfaces;
using Domosti.CapaDatos.Modelos;
using Domosti.CapaNegocios.Bases;
using Domosti.CapaNegocios.Interfaces;

namespace Domosti.CapaNegocios.Implementaciones
{
    public class PersonaBl : ClaseBase, IPersonaBl
    {
        private readonly IPersonaDal _dal;
        private readonly IDispositivoDal _dispositivoDal;

        public PersonaBl(IPersonaDal dal, IDispositivoDal dispositivoDal)
        {
            _dal = dal;
            _dispositivoDal = dispositivoDal;
        }

        public Stream Login(string email, string clave, string idDevice, string imei, string iccid)
        {
            var usuarioApp = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            if (usuarioApp == null)
                return ObtenerSalida("101", "N", "No posee cuenta DomOsti.");
            if (usuarioApp.Contrasena != HashPassword(clave))
                return ObtenerSalida("102", "N", "Contraseña incorrecta.");
            var dispositivo = usuarioApp.Dispositivos.FirstOrDefault(d => d.IdDevice == idDevice && d.Imei == imei && d.IccId == iccid);
            return dispositivo == null ? ObtenerSalida("103", "N", "Dispositivo no asociado.") : ObtenerSalida("100", dispositivo.IdDispositivo.ToString(CultureInfo.InvariantCulture), "Login Correcto.");
        }
        public Stream RestaurarContrasena(string email)
        {
            var usuarioApp = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            if (usuarioApp == null)
                return ObtenerSalida("1701", "N", "No existe correo electrónico.");
            var contrasenaNueva = Membership.GeneratePassword(10, 0);
            usuarioApp.Contrasena = HashPassword(contrasenaNueva);
            _dal.ActualizarUsuarioApp(usuarioApp);
            SendEmail(email, "Cambio de Contraseña DomOstium App.",
                                string.Format(
                                "Estimado {0} {1}<p> Se ha restablecido su contraseña. <p> Usuario : {2} <p> Contraseña : {3} </p>",
                                usuarioApp.Nombres, usuarioApp.Apellidos, email, contrasenaNueva));
            return ObtenerSalida("1700", "", "");
        }
        public Stream AsociarDispositivo(string email, string nombreDispositivo, string idDevice, string imei, string iccid, string token)
        {
            var usuario = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            if (usuario == null)
                return ObtenerSalida("202","F", "Existen problemas con los datos ingresados, la aplicacion se reiniciara.");
            var dispositivo = new Dispositivo
            {
                FechaRegistro = DateTime.Now.ToUniversalTime().AddHours(-5),
                IccId = iccid,
                IdDevice = idDevice,
                Imei = imei,
                Nombre = nombreDispositivo,
                Token = token
            };
              usuario.Dispositivos.Add(dispositivo);
              _dal.ActualizarUsuarioApp(usuario);
            return ObtenerSalida("200",dispositivo.IdDispositivo.ToString(CultureInfo.InvariantCulture), "Dispositivo asociado exitosamente.");
        }
        public Stream ObtenerUsuarioApp(string idDispositivo)
        {
            var dispositivo = Convert.ToInt32(idDispositivo);
            var idUsuario = _dispositivoDal.ObtenerDispositivos().FirstOrDefault(d => d.IdDispositivo == dispositivo);
            if(idUsuario == null)
                return ObtenerSalida("2110", "", "No existe dispositivo.");
            var usuarioApp = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.IdPersona == idUsuario.IdUsuarioApp);
            if(usuarioApp == null)
                return ObtenerSalida("2120", "", "No existe usuario.");
            var usuarioAppJson = new UsuarioAppJson
            {
                idUsuario = usuarioApp.IdPersona.ToString(CultureInfo.InvariantCulture),
                apellidos = usuarioApp.Apellidos,
                fechaNacimiento = string.Format("{0}-{1}-{2}", usuarioApp.FechaNacimiento.Day.ToString("00"), usuarioApp.FechaNacimiento.Month.ToString("00"), usuarioApp.FechaNacimiento.Year),
                identificacion = usuarioApp.Identificacion,
                nombres = usuarioApp.Nombres,
                tipoIdentificacion = usuarioApp.TipoIdentificacion
            };
            return ObtenerSalidaUsuarioApp(new List<UsuarioAppJson>{usuarioAppJson}, "");
        }
        public Stream CrearUsuarioApp(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string contrasena, string fechaNacimiento)
        {
            using (var transaccion = new TransactionScope())
            {
                if (_dal.ObtenerUsuariosApp().Any(u => u.Email.ToUpper() == email.ToUpper()))
                    return ObtenerSalida("701", "", "Este correo ya esta asociado a una cuenta DomOstium.");
                var fecha = Convert.ToDateTime(fechaNacimiento);
                var usuarioApp = new UsuarioApp
                {
                    Apellidos = apellidos,
                    Contrasena = HashPassword(contrasena),
                    Email = email,
                    Identificacion = identificacion,
                    Nombres = nombres,
                    TipoIdentificacion = tipoIdentificacion,
                    FechaNacimiento = fecha
                };
                _dal.CrearUsuarioApp(usuarioApp);
                var residentes = _dal.ObtenerResidentes().Where(r => !r.EstaEliminado && r.Email.ToUpper() == email.ToUpper());
                foreach (var residente in residentes)
                {
                    residente.IdUsuarioApp = usuarioApp.IdPersona;
                    _dal.ActualizarResidente(residente);
                }
                SendEmail(email, "Bienvenido a DomOstium App",
                        string.Format(
                        "Estimado {0} {1}<p>Usted se ha registrado a DomOstium App<p>Usuario :{2}<p>Contraseña:{3}<p>Gracias y Bienvenido a DomOstium App.</p>",
                        nombres, apellidos,email,contrasena));
                transaccion.Complete();
                return ObtenerSalida("700", "", "");
            }
        }
        public Stream ActualizarUsuarioApp(string idResidente, string nombres, string apellidos, string fechaNacimiento)
        {
            var id = Convert.ToInt32(idResidente);
            var residente = _dal.ObtenerResidentes().FirstOrDefault(r => r.IdPersona == id);
            if (residente == null)
                return ObtenerSalida("1910", "", "No existe el residente.");
            if (!residente.IdUsuarioApp.HasValue)
                return ObtenerSalida("1920", "", "No existe el usuario.");
            var usuarioApp = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.IdPersona == residente.IdUsuarioApp);
            if(usuarioApp == null)
                return ObtenerSalida("1920", "", "No existe el usuario.");
            var fecha = Convert.ToDateTime(fechaNacimiento);
            usuarioApp.Nombres = nombres;
            usuarioApp.Apellidos = apellidos;
            usuarioApp.FechaNacimiento = fecha;
            _dal.ActualizarUsuarioApp(usuarioApp);
            return ObtenerSalida("1900", "", "");
        }

        public Stream CambiarContrasena(string idResidente, string contrasenaVieja, string contrasenaNueva)
        {
            var id = Convert.ToInt32(idResidente);
            var residente = _dal.ObtenerResidentes().FirstOrDefault(r => r.IdPersona == id);
            if (residente == null)
                return ObtenerSalida("2010", "", "No existe el residente.");
            if (!residente.IdUsuarioApp.HasValue)
                return ObtenerSalida("2020", "", "No existe el usuario.");
            var usuarioApp = _dal.ObtenerUsuariosApp().FirstOrDefault(u => u.IdPersona == residente.IdUsuarioApp);
            if (usuarioApp == null)
                return ObtenerSalida("2020", "", "No existe el usuario.");
            if(usuarioApp.Contrasena != HashPassword(contrasenaVieja))
                return ObtenerSalida("2030", "", "Contrasena antigua incorrecta.");
            usuarioApp.Contrasena = HashPassword(contrasenaNueva);
            _dal.ActualizarUsuarioApp(usuarioApp);
            return ObtenerSalida("2000", "", "");
        }
        private void SendEmail(string para, string asunto, string mensaje)
        {
            using (var message = new MailMessage(ConfigurationManager.AppSettings["De"], para) { Subject = asunto, SubjectEncoding = Encoding.UTF8, Body = mensaje, Priority = MailPriority.High, IsBodyHtml = true })
            {
                message.Body += Environment.NewLine;
                message.BodyEncoding = Encoding.UTF8;

                using (var client = new SmtpClient(ConfigurationManager.AppSettings["Host"], Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"])))
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["De"], ConfigurationManager.AppSettings["ContrasenaCorreo"]);
                    client.Timeout = 10000;
                    client.Send(message);
                }
            }
        }
        public Stream ObtenerVisitantesPorResidente(string idResidente)
        {
            var residente = Convert.ToInt32(idResidente);
            var visitantes = _dal.ObtenerVisitantes().Where(v => v.IdResidente == residente);
            var resultado = visitantes.Select(visitante => new VisitanteJson
            {
                id = visitante.IdPersona.ToString(CultureInfo.InvariantCulture), 
                identificacion = visitante.Identificacion, 
                tipoIdentificacion = visitante.TipoIdentificacion, 
                nombres = visitante.Nombres, 
                apellidos = visitante.Apellidos,
                telefono = visitante.Telefono,
                email = visitante.Email,
                estaEliminado = visitante.EstaEliminado ? "1" : "0"
            }).OrderBy(r => r.apellidos).ThenBy(r => r.nombres).ToList();
            return resultado.Count == 0 ? ObtenerSalida("801",null, "No existen visitantes registrados.") : ObtenerSalidaVisitante(resultado, "");
        }
        public Stream CrearVisitante(string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono, string idResidente)
        {
            var residente = Convert.ToInt32(idResidente);
            var visitante = new Visitante
            {
                Apellidos = apellidos,
                Identificacion = identificacion,
                Nombres = nombres,
                TipoIdentificacion = tipoIdentificacion,
                Email = email,
                Telefono = telefono,
                IdResidente = residente
            };
            _dal.CrearVisitante(visitante);
            return ObtenerSalida("900", "", "");
        }
        public Stream ActualizarVisitante(string idVisitante, string tipoIdentificacion, string identificacion, string nombres, string apellidos, string email, string telefono)
        {
            var visitante = Convert.ToInt32(idVisitante);
            var visitanteAModificar = _dal.ObtenerVisitantes().FirstOrDefault(v => v.IdPersona == visitante);
            if(visitanteAModificar == null)
                return ObtenerSalida("1001", "", "No existe el visitante para a modificar.");
            visitanteAModificar.TipoIdentificacion = tipoIdentificacion;
            visitanteAModificar.Identificacion = identificacion;
            visitanteAModificar.Nombres = nombres;
            visitanteAModificar.Apellidos = apellidos;
            visitanteAModificar.Email = email;
            visitanteAModificar.Telefono = telefono;
            _dal.ActualizarVisitante(visitanteAModificar);
            return ObtenerSalida("1000", "", "");
        }
        public Stream EliminarVisitante(string idVisitante)
        {
            var visitante = Convert.ToInt32(idVisitante);
            var visitanteAEliminar = _dal.ObtenerVisitantes().FirstOrDefault(v => v.IdPersona == visitante);
            if (visitanteAEliminar == null)
                return ObtenerSalida("1101", "", "No existe el visitante para a eliminar.");
            visitanteAEliminar.EstaEliminado = true;
            _dal.ActualizarVisitante(visitanteAEliminar);
            return ObtenerSalida("1100", "", "");
        }
    }
}
