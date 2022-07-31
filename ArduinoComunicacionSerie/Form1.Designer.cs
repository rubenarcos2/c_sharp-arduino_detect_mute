namespace ArduinoComunicacionSerie
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.timerChangeStatus = new System.Windows.Forms.Timer(this.components);
            this.chkMicrophone = new System.Windows.Forms.CheckBox();
            this.chkMicroMute = new System.Windows.Forms.CheckBox();
            this.chkCamera = new System.Windows.Forms.CheckBox();
            this.chkToogleWebcam = new System.Windows.Forms.CheckBox();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.chkToogleMute = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // timerChangeStatus
            // 
            this.timerChangeStatus.Enabled = true;
            this.timerChangeStatus.Tick += new System.EventHandler(this.timerChangeStatus_Tick);
            // 
            // chkMicrophone
            // 
            this.chkMicrophone.AutoSize = true;
            this.chkMicrophone.Enabled = false;
            this.chkMicrophone.Location = new System.Drawing.Point(160, 9);
            this.chkMicrophone.Name = "chkMicrophone";
            this.chkMicrophone.Size = new System.Drawing.Size(115, 17);
            this.chkMicrophone.TabIndex = 2;
            this.chkMicrophone.Text = "Micrófono (En uso)";
            this.chkMicrophone.UseVisualStyleBackColor = true;
            // 
            // chkMicroMute
            // 
            this.chkMicroMute.AutoSize = true;
            this.chkMicroMute.Enabled = false;
            this.chkMicroMute.Location = new System.Drawing.Point(278, 8);
            this.chkMicroMute.Name = "chkMicroMute";
            this.chkMicroMute.Size = new System.Drawing.Size(106, 17);
            this.chkMicroMute.TabIndex = 3;
            this.chkMicroMute.Text = "Micrófono (Mute)";
            this.chkMicroMute.UseVisualStyleBackColor = true;
            // 
            // chkCamera
            // 
            this.chkCamera.AutoSize = true;
            this.chkCamera.Enabled = false;
            this.chkCamera.Location = new System.Drawing.Point(390, 8);
            this.chkCamera.Name = "chkCamera";
            this.chkCamera.Size = new System.Drawing.Size(111, 17);
            this.chkCamera.TabIndex = 4;
            this.chkCamera.Text = "Webcam (En uso)";
            this.chkCamera.UseVisualStyleBackColor = true;
            // 
            // chkToogleWebcam
            // 
            this.chkToogleWebcam.AutoSize = true;
            this.chkToogleWebcam.Location = new System.Drawing.Point(7, 32);
            this.chkToogleWebcam.Name = "chkToogleWebcam";
            this.chkToogleWebcam.Size = new System.Drawing.Size(161, 17);
            this.chkToogleWebcam.TabIndex = 6;
            this.chkToogleWebcam.Text = "Activar/Desactivar Webcam";
            this.chkToogleWebcam.UseVisualStyleBackColor = true;
            this.chkToogleWebcam.CheckedChanged += new System.EventHandler(this.chkToogleWebcam_CheckedChanged);
            // 
            // cmbDevices
            // 
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(174, 32);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(319, 21);
            this.cmbDevices.TabIndex = 7;
            // 
            // chkToogleMute
            // 
            this.chkToogleMute.AutoSize = true;
            this.chkToogleMute.Location = new System.Drawing.Point(7, 8);
            this.chkToogleMute.Name = "chkToogleMute";
            this.chkToogleMute.Size = new System.Drawing.Size(165, 17);
            this.chkToogleMute.TabIndex = 5;
            this.chkToogleMute.Text = "Mutear/Desmutear Micrófono";
            this.chkToogleMute.UseVisualStyleBackColor = true;
            this.chkToogleMute.CheckedChanged += new System.EventHandler(this.chkToogleMute_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 61);
            this.Controls.Add(this.cmbDevices);
            this.Controls.Add(this.chkToogleWebcam);
            this.Controls.Add(this.chkToogleMute);
            this.Controls.Add(this.chkCamera);
            this.Controls.Add(this.chkMicroMute);
            this.Controls.Add(this.chkMicrophone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "ArduinoDetectMicroCamMute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.onLoadFrm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerChangeStatus;
        private System.Windows.Forms.CheckBox chkMicrophone;
        private System.Windows.Forms.CheckBox chkMicroMute;
        private System.Windows.Forms.CheckBox chkCamera;
        private System.Windows.Forms.CheckBox chkToogleWebcam;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.CheckBox chkToogleMute;
    }
}

