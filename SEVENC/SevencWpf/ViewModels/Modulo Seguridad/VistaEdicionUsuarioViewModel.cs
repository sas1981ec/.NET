using Microsoft.Win32;
using SevencWpf.InfraestructuraVM;
using SevencWpf.ServicioWcf;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SevencWpf.ViewModels.Modulo_Seguridad
{
    internal class VistaEdicionUsuarioViewModel : ViewModelBase
    {
        #region Campos
        private readonly ServicioWcfClient _servicio;
        private readonly bool _esNuevo;
        private Usuario _usuario;
        private bool? _dialogResult;
        #endregion

        #region Constructor
        public VistaEdicionUsuarioViewModel(ServicioWcfClient servicio, bool esNuevo, Usuario usuario)
        {
            _servicio = servicio;
            _esNuevo = esNuevo;
            _usuario = usuario;
        }
        #endregion

        #region Propiedades
        public ICommand ComandoGrabar
        {
            get
            {
                return new RelayCommand(Grabar, PuedoGrabar);
            }
        }

        public ICommand ComandoCancelar
        {
            get
            {
                return new RelayCommand(Cancelar);
            }
        }

        public ICommand ComandoFoto
        {
            get
            {
                return new RelayCommand(SubirFoto);
            }
        }

        public ICommand ComandoEliminarFoto
        {
            get
            {
                return new RelayCommand(BorrarFoto);
            }
        }

        public string Titulo
        {
            get
            {
                return _esNuevo ? "Nuevo Usuario" : string.Format("Editar Usuario - {0}", _usuario.NombreCompleto);
            }
        }

        public Usuario Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                if (_usuario == value) return;
                _usuario = value;
                OnPropertyChanged("Usuario");
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

        public bool EsNuevo
        {
            get
            {
                return _esNuevo;
            }
        }

        public BitmapImage FotoBitmap
        {
            get
            {
                if (Usuario.Foto == null || Usuario.Foto.Count() == 1)
                {
                    var imagen = new BitmapImage(new Uri(@"/Recursos/Imagenes/usuario.png", UriKind.Relative));
                    return imagen;
                }
                using (var stream = new MemoryStream(Usuario.Foto))
                {
                    var imagen = new BitmapImage();
                    imagen.BeginInit();
                    imagen.CacheOption = BitmapCacheOption.OnLoad;
                    imagen.StreamSource = stream;
                    imagen.EndInit();
                    return imagen;
                }
            }
        }
        #endregion

        #region Metodos
        private void SubirFoto()
        {
            var openFileDialog = new OpenFileDialog
            { Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* " };
            if (openFileDialog.ShowDialog() != true) return;
            var stream = openFileDialog.OpenFile();
            {
                using (stream)
                {
                    if(!TamanoOk(openFileDialog.FileName))
                        return;
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    Usuario.Foto = bytes;
                    OnPropertyChanged("Usuario");
                    OnPropertyChanged("FotoBitmap");
                }
            }
        }

        private bool TamanoOk(string fileName)
        {
            var longitud = new FileInfo(fileName).Length;
            if (longitud > Convert.ToInt32(ConfigurationManager.AppSettings["MaximoTamanoImagen"]))
            {
                MessageBox.Show($"El Archivo {fileName}\nPesa más de 1 MB\nNo se puede subir.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        private void BorrarFoto()
        {
            Usuario.Foto = null;
            OnPropertyChanged("Usuario");
            OnPropertyChanged("FotoBitmap");
        }

        private void Cancelar()
        {
            DialogResult = false;
        }

        private bool PuedoGrabar()
        {
            return !string.IsNullOrWhiteSpace(Usuario.Nombres) && !string.IsNullOrWhiteSpace(Usuario.Apellidos) &&
                !string.IsNullOrWhiteSpace(Usuario.Email) && !string.IsNullOrWhiteSpace(Usuario.UserName);
        }

        private void Grabar()
        {
            if (_usuario.Foto == null)
                EstablecerFotoParaNulls();
            if (_esNuevo)
            {
                if (VerificarUserName() && VerificarEmail())
                    _servicio.CrearUsuario(_usuario);
                else
                    return;
            }
            else
                _servicio.ActualizarUsuario(_usuario);
            MessageBox.Show("Proceso Ok", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }

        private void EstablecerFotoParaNulls()
        {
            var bytes = new byte[1];
            _usuario.Foto = bytes;
        }

        private bool VerificarUserName()
        {
            var resultado = _servicio.ExisteUserName(_usuario.UserName);
            if (resultado)
            {
                MessageBox.Show("UserName ya existe.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Usuario.UserName = "";
            }
            return !resultado;
        }

        private bool VerificarEmail()
        {
            var validation = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,5}$");
            if (!validation.IsMatch(_usuario.Email))
            {
                MessageBox.Show("Dirección de correo no valida.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Usuario.Email = "";
                return false;
            }
            var resultado = _servicio.ExisteEmail(_usuario.Email);
            if (resultado)
            {
                MessageBox.Show("Email ya existe.", Application.Current.Resources["NombreAplicacion"].ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Usuario.Email = "";
            }
            return !resultado;
        }
        #endregion
    }
}
