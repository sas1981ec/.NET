namespace TesterFe
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _servicio.Close();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnFirmar = new System.Windows.Forms.Button();
            this.BtnEnviar = new System.Windows.Forms.Button();
            this.BtnAutorizar = new System.Windows.Forms.Button();
            this.TxtMensajes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnFirmar
            // 
            this.BtnFirmar.Location = new System.Drawing.Point(258, 38);
            this.BtnFirmar.Name = "BtnFirmar";
            this.BtnFirmar.Size = new System.Drawing.Size(75, 23);
            this.BtnFirmar.TabIndex = 0;
            this.BtnFirmar.Text = "Firmar";
            this.BtnFirmar.UseVisualStyleBackColor = true;
            this.BtnFirmar.Click += new System.EventHandler(this.BtnFirmarClick);
            // 
            // BtnEnviar
            // 
            this.BtnEnviar.Location = new System.Drawing.Point(258, 67);
            this.BtnEnviar.Name = "BtnEnviar";
            this.BtnEnviar.Size = new System.Drawing.Size(75, 23);
            this.BtnEnviar.TabIndex = 1;
            this.BtnEnviar.Text = "Enviar";
            this.BtnEnviar.UseVisualStyleBackColor = true;
            this.BtnEnviar.Click += new System.EventHandler(this.BtnEnviarClick);
            // 
            // BtnAutorizar
            // 
            this.BtnAutorizar.Location = new System.Drawing.Point(258, 96);
            this.BtnAutorizar.Name = "BtnAutorizar";
            this.BtnAutorizar.Size = new System.Drawing.Size(75, 23);
            this.BtnAutorizar.TabIndex = 2;
            this.BtnAutorizar.Text = "Autorizar";
            this.BtnAutorizar.UseVisualStyleBackColor = true;
            this.BtnAutorizar.Click += new System.EventHandler(this.BtnAutorizarClick);
            // 
            // TxtMensajes
            // 
            this.TxtMensajes.Location = new System.Drawing.Point(24, 25);
            this.TxtMensajes.Multiline = true;
            this.TxtMensajes.Name = "TxtMensajes";
            this.TxtMensajes.ReadOnly = true;
            this.TxtMensajes.Size = new System.Drawing.Size(212, 112);
            this.TxtMensajes.TabIndex = 3;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 154);
            this.Controls.Add(this.TxtMensajes);
            this.Controls.Add(this.BtnAutorizar);
            this.Controls.Add(this.BtnEnviar);
            this.Controls.Add(this.BtnFirmar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prueba";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnFirmar;
        private System.Windows.Forms.Button BtnEnviar;
        private System.Windows.Forms.Button BtnAutorizar;
        private System.Windows.Forms.TextBox TxtMensajes;
    }
}

