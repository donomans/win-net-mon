using System;

namespace RemoteMon
{
    partial class NoConfigurationFound
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
            this.configurationFileErrorDescriptionLbl = new System.Windows.Forms.Label();
            this.loadFromServiceBtn = new System.Windows.Forms.Button();
            this.loadFromPathBtn = new System.Windows.Forms.Button();
            this.loadConfigurationFromPathOfd = new System.Windows.Forms.OpenFileDialog();
            this.ipLbl = new System.Windows.Forms.Label();
            this.portMaskedTxt = new System.Windows.Forms.MaskedTextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.ipTxt = new System.Windows.Forms.TextBox();
            this.portLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.startFreshBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // configurationFileErrorDescriptionLbl
            // 
            this.configurationFileErrorDescriptionLbl.Location = new System.Drawing.Point(6, 9);
            this.configurationFileErrorDescriptionLbl.Name = "configurationFileErrorDescriptionLbl";
            this.configurationFileErrorDescriptionLbl.Size = new System.Drawing.Size(292, 59);
            this.configurationFileErrorDescriptionLbl.TabIndex = 0;
            this.configurationFileErrorDescriptionLbl.Text = "No Configuration File was found at the default location. \r\n\r\nYou can load the con" +
                "figuration from the service (if you have the IP/Hostname and Port) or provide th" +
                "e path to the file.";
            // 
            // loadFromServiceBtn
            // 
            this.loadFromServiceBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.loadFromServiceBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadFromServiceBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.loadFromServiceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFromServiceBtn.Location = new System.Drawing.Point(12, 76);
            this.loadFromServiceBtn.Name = "loadFromServiceBtn";
            this.loadFromServiceBtn.Size = new System.Drawing.Size(111, 23);
            this.loadFromServiceBtn.TabIndex = 1;
            this.loadFromServiceBtn.Text = "Load From Service";
            this.loadFromServiceBtn.UseVisualStyleBackColor = true;
            this.loadFromServiceBtn.Click += new System.EventHandler(this.LoadFromServiceBtnClick);
            // 
            // loadFromPathBtn
            // 
            this.loadFromPathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadFromPathBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.loadFromPathBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadFromPathBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.loadFromPathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFromPathBtn.Location = new System.Drawing.Point(253, 76);
            this.loadFromPathBtn.Name = "loadFromPathBtn";
            this.loadFromPathBtn.Size = new System.Drawing.Size(111, 23);
            this.loadFromPathBtn.TabIndex = 1;
            this.loadFromPathBtn.Text = "Load From Path";
            this.loadFromPathBtn.UseVisualStyleBackColor = true;
            this.loadFromPathBtn.Click += new System.EventHandler(this.LoadFromPathBtnClick);
            // 
            // loadConfigurationFromPathOfd
            // 
            this.loadConfigurationFromPathOfd.CheckFileExists = false;
            this.loadConfigurationFromPathOfd.DefaultExt = "xml";
            this.loadConfigurationFromPathOfd.FileName = "configuration.xml";
            this.loadConfigurationFromPathOfd.Filter = "\"Configuration Files|*.xml\"";
            // 
            // ipLbl
            // 
            this.ipLbl.AutoSize = true;
            this.ipLbl.Location = new System.Drawing.Point(36, 122);
            this.ipLbl.Name = "ipLbl";
            this.ipLbl.Size = new System.Drawing.Size(70, 13);
            this.ipLbl.TabIndex = 2;
            this.ipLbl.Text = "IP/Hostname";
            this.ipLbl.Visible = false;
            // 
            // portMaskedTxt
            // 
            this.portMaskedTxt.Location = new System.Drawing.Point(273, 119);
            this.portMaskedTxt.Mask = "09999";
            this.portMaskedTxt.Name = "portMaskedTxt";
            this.portMaskedTxt.PromptChar = ' ';
            this.portMaskedTxt.Size = new System.Drawing.Size(44, 20);
            this.portMaskedTxt.TabIndex = 3;
            this.portMaskedTxt.Text = "5502";
            this.portMaskedTxt.ValidatingType = typeof(int);
            this.portMaskedTxt.Visible = false;
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(87, 157);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Visible = false;
            this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // ipTxt
            // 
            this.ipTxt.Location = new System.Drawing.Point(108, 119);
            this.ipTxt.Name = "ipTxt";
            this.ipTxt.Size = new System.Drawing.Size(100, 20);
            this.ipTxt.TabIndex = 5;
            this.ipTxt.Visible = false;
            // 
            // portLbl
            // 
            this.portLbl.AutoSize = true;
            this.portLbl.Location = new System.Drawing.Point(241, 122);
            this.portLbl.Name = "portLbl";
            this.portLbl.Size = new System.Drawing.Size(26, 13);
            this.portLbl.TabIndex = 2;
            this.portLbl.Text = "Port";
            this.portLbl.Visible = false;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(213, 157);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Visible = false;
            // 
            // startFreshBtn
            // 
            this.startFreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startFreshBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.startFreshBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.startFreshBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.startFreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startFreshBtn.Location = new System.Drawing.Point(133, 76);
            this.startFreshBtn.Name = "startFreshBtn";
            this.startFreshBtn.Size = new System.Drawing.Size(111, 23);
            this.startFreshBtn.TabIndex = 1;
            this.startFreshBtn.Text = "Start Fresh";
            this.startFreshBtn.UseVisualStyleBackColor = true;
            this.startFreshBtn.Click += new System.EventHandler(this.StartFreshBtnClick);
            // 
            // NoConfigurationFound
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(376, 110);
            this.Controls.Add(this.ipTxt);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.portMaskedTxt);
            this.Controls.Add(this.portLbl);
            this.Controls.Add(this.ipLbl);
            this.Controls.Add(this.startFreshBtn);
            this.Controls.Add(this.loadFromPathBtn);
            this.Controls.Add(this.loadFromServiceBtn);
            this.Controls.Add(this.configurationFileErrorDescriptionLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NoConfigurationFound";
            this.Text = "Configuration File Error:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label configurationFileErrorDescriptionLbl;
        private System.Windows.Forms.Button loadFromServiceBtn;
        private System.Windows.Forms.Button loadFromPathBtn;
        private System.Windows.Forms.OpenFileDialog loadConfigurationFromPathOfd;
        private System.Windows.Forms.Label ipLbl;
        private System.Windows.Forms.MaskedTextBox portMaskedTxt;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TextBox ipTxt;
        private System.Windows.Forms.Label portLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button startFreshBtn;
    }
}