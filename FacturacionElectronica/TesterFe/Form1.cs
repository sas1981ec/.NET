using System;
using System.Windows.Forms;
using System.Xml;
using TesterFe.ServicioFe;

namespace TesterFe
{
    public partial class FrmPrincipal : Form
    {
        private readonly ServicioClient _servicio;
        private string _xmlFirmado;
        private string _claveAcceso;
        public FrmPrincipal()
        {
            InitializeComponent();
            _servicio = new ServicioClient();
        }

        private void BtnFirmarClick(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = @"xml files (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                var doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);
                var respuesta = _servicio.Firmar(doc.InnerXml, "mirna_gisella_cedeno_delgado.p12", "Ecuador2015");
                TxtMensajes.Text = respuesta.Mensaje;
                _xmlFirmado = respuesta.Dato;
                var elemList = doc.GetElementsByTagName("claveAcceso");
                _claveAcceso = elemList[0].InnerText;
            }
            catch (Exception ex)
            {
                TxtMensajes.Text = ex.Message;
            }
        }

        private void BtnEnviarClick(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(_xmlFirmado))
                return;
            try
            {
                var respuesta = _servicio.EnviarSri(_xmlFirmado);
                TxtMensajes.Text = string.Format("Mensaje : {0}; Dato : {1}", respuesta.Mensaje, respuesta.Dato);
            }
            catch (Exception ex)
            {
                TxtMensajes.Text = ex.Message;
            }
        }

        private void BtnAutorizarClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_xmlFirmado) || string.IsNullOrWhiteSpace(_claveAcceso))
                return;
            try
            {
                var respuesta = _servicio.AutorizarSri(_claveAcceso);
                TxtMensajes.Text = string.Format("Mensaje : {0}; Dato : {1}", respuesta.Mensaje, respuesta.Dato);
            }
            catch (Exception ex)
            {
                TxtMensajes.Text = ex.Message;
            }
        }
    }
}
