namespace RemoteMon_Notify
{
    partial class Settings
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
            this.settingsOkBtn = new System.Windows.Forms.Button();
            this.settingsCancelBtn = new System.Windows.Forms.Button();
            this.settingsRemoteMonServicePortLabel = new System.Windows.Forms.Label();
            this.settingsRemoteMonServicePortTextBox = new System.Windows.Forms.TextBox();
            this.settingsRemoteMonServiceAddressTextBox = new System.Windows.Forms.TextBox();
            this.settingsRemoteMonServiceAddressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // settingsOkBtn
            // 
            this.settingsOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.settingsOkBtn.Location = new System.Drawing.Point(12, 112);
            this.settingsOkBtn.Name = "settingsOkBtn";
            this.settingsOkBtn.Size = new System.Drawing.Size(75, 23);
            this.settingsOkBtn.TabIndex = 0;
            this.settingsOkBtn.Text = "OK";
            this.settingsOkBtn.UseVisualStyleBackColor = true;
            // 
            // settingsCancelBtn
            // 
            this.settingsCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.settingsCancelBtn.Location = new System.Drawing.Point(100, 112);
            this.settingsCancelBtn.Name = "settingsCancelBtn";
            this.settingsCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.settingsCancelBtn.TabIndex = 0;
            this.settingsCancelBtn.Text = "Cancel";
            this.settingsCancelBtn.UseVisualStyleBackColor = true;
            // 
            // settingsRemoteMonServicePortLabel
            // 
            this.settingsRemoteMonServicePortLabel.AutoSize = true;
            this.settingsRemoteMonServicePortLabel.Location = new System.Drawing.Point(23, 61);
            this.settingsRemoteMonServicePortLabel.Name = "settingsRemoteMonServicePortLabel";
            this.settingsRemoteMonServicePortLabel.Size = new System.Drawing.Size(29, 13);
            this.settingsRemoteMonServicePortLabel.TabIndex = 8;
            this.settingsRemoteMonServicePortLabel.Text = "Port:";
            this.settingsRemoteMonServicePortLabel.Visible = false;
            // 
            // settingsRemoteMonServicePortTextBox
            // 
            this.settingsRemoteMonServicePortTextBox.Location = new System.Drawing.Point(26, 77);
            this.settingsRemoteMonServicePortTextBox.Name = "settingsRemoteMonServicePortTextBox";
            this.settingsRemoteMonServicePortTextBox.Size = new System.Drawing.Size(53, 20);
            this.settingsRemoteMonServicePortTextBox.TabIndex = 9;
            this.settingsRemoteMonServicePortTextBox.Visible = false;
            // 
            // settingsRemoteMonServiceAddressTextBox
            // 
            this.settingsRemoteMonServiceAddressTextBox.Location = new System.Drawing.Point(26, 38);
            this.settingsRemoteMonServiceAddressTextBox.Name = "settingsRemoteMonServiceAddressTextBox";
            this.settingsRemoteMonServiceAddressTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsRemoteMonServiceAddressTextBox.TabIndex = 10;
            // 
            // settingsRemoteMonServiceAddressLabel
            // 
            this.settingsRemoteMonServiceAddressLabel.AutoSize = true;
            this.settingsRemoteMonServiceAddressLabel.Location = new System.Drawing.Point(23, 22);
            this.settingsRemoteMonServiceAddressLabel.Name = "settingsRemoteMonServiceAddressLabel";
            this.settingsRemoteMonServiceAddressLabel.Size = new System.Drawing.Size(148, 13);
            this.settingsRemoteMonServiceAddressLabel.TabIndex = 7;
            this.settingsRemoteMonServiceAddressLabel.Text = "RemoteMon Service Address:";
            // 
            // Settings
            // 
            this.AcceptButton = this.settingsOkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.settingsCancelBtn;
            this.ClientSize = new System.Drawing.Size(188, 144);
            this.Controls.Add(this.settingsRemoteMonServicePortLabel);
            this.Controls.Add(this.settingsRemoteMonServicePortTextBox);
            this.Controls.Add(this.settingsRemoteMonServiceAddressTextBox);
            this.Controls.Add(this.settingsRemoteMonServiceAddressLabel);
            this.Controls.Add(this.settingsCancelBtn);
            this.Controls.Add(this.settingsOkBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button settingsOkBtn;
        private System.Windows.Forms.Button settingsCancelBtn;
        private System.Windows.Forms.Label settingsRemoteMonServicePortLabel;
        public System.Windows.Forms.TextBox settingsRemoteMonServicePortTextBox;
        public System.Windows.Forms.TextBox settingsRemoteMonServiceAddressTextBox;
        private System.Windows.Forms.Label settingsRemoteMonServiceAddressLabel;
    }
}