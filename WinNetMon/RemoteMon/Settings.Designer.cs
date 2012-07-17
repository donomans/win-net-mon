namespace RemoteMon
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
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.settingsGeneralTab = new System.Windows.Forms.TabPage();
            this.settingsLoggingGroupBox = new System.Windows.Forms.GroupBox();
            this.settingsVerboseLoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsLoggingClientFileDialogButton = new System.Windows.Forms.Button();
            this.settingsLoggingServiceFileDialogButton = new System.Windows.Forms.Button();
            this.settingsLoggingServicePathTextBox = new System.Windows.Forms.TextBox();
            this.settingsLoggingClientPathTextBox = new System.Windows.Forms.TextBox();
            this.settingsLoggingClientPathLabel = new System.Windows.Forms.Label();
            this.settingsLoggingServicePathLabel = new System.Windows.Forms.Label();
            this.settingsSmsNotificationGroupBox = new System.Windows.Forms.GroupBox();
            this.settingsSmsTest = new System.Windows.Forms.Button();
            this.settingsAlertSmsLabel = new System.Windows.Forms.Label();
            this.settingsiSmsAddressLabel = new System.Windows.Forms.Label();
            this.settingsiSmsPassTextBox = new System.Windows.Forms.TextBox();
            this.settingsiSmsUserLabel = new System.Windows.Forms.Label();
            this.settingsiSmsUserTextBox = new System.Windows.Forms.TextBox();
            this.settingsiSmsPass = new System.Windows.Forms.Label();
            this.settingsiSmsAddressTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertSmsTextBox = new System.Windows.Forms.TextBox();
            this.settingsServerSettings = new System.Windows.Forms.GroupBox();
            this.settingsRemoteMonServicePortLabel = new System.Windows.Forms.Label();
            this.settingsRemoteServerLabel = new System.Windows.Forms.Label();
            this.settingsRemoteMonServicePortTextBox = new System.Windows.Forms.TextBox();
            this.settingsRemoteServerTextBox = new System.Windows.Forms.TextBox();
            this.settingsRemoteMonServiceAddressTextBox = new System.Windows.Forms.TextBox();
            this.settingsRemoteMonServiceAddressLabel = new System.Windows.Forms.Label();
            this.settingEmailNotificationsGroupBox = new System.Windows.Forms.GroupBox();
            this.settingsMailServerSslCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsMailServerPortMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.settingsEmailTest = new System.Windows.Forms.Button();
            this.settingsMailServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertEmailPasswordTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertEmailAccountTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertEmailPasswordLabel = new System.Windows.Forms.Label();
            this.settingsAlertEmailFromTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertEmailToTextBox = new System.Windows.Forms.TextBox();
            this.settingsAlertEmailAccountLabel = new System.Windows.Forms.Label();
            this.settingsAlertEmailFromLabel = new System.Windows.Forms.Label();
            this.settingsAlertEmailToLabel = new System.Windows.Forms.Label();
            this.settingsMailServerPortLabel = new System.Windows.Forms.Label();
            this.settingsMailServerAddressLabel = new System.Windows.Forms.Label();
            this.settingsAdvancedTab = new System.Windows.Forms.TabPage();
            this.settingsOkButton = new System.Windows.Forms.Button();
            this.settingsCancelButton = new System.Windows.Forms.Button();
            this.settingsLoggingFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.settingsTabControl.SuspendLayout();
            this.settingsGeneralTab.SuspendLayout();
            this.settingsLoggingGroupBox.SuspendLayout();
            this.settingsSmsNotificationGroupBox.SuspendLayout();
            this.settingsServerSettings.SuspendLayout();
            this.settingEmailNotificationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Controls.Add(this.settingsGeneralTab);
            this.settingsTabControl.Controls.Add(this.settingsAdvancedTab);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsTabControl.HotTrack = true;
            this.settingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(557, 579);
            this.settingsTabControl.TabIndex = 0;
            // 
            // settingsGeneralTab
            // 
            this.settingsGeneralTab.Controls.Add(this.settingsLoggingGroupBox);
            this.settingsGeneralTab.Controls.Add(this.settingsSmsNotificationGroupBox);
            this.settingsGeneralTab.Controls.Add(this.settingsServerSettings);
            this.settingsGeneralTab.Controls.Add(this.settingEmailNotificationsGroupBox);
            this.settingsGeneralTab.Location = new System.Drawing.Point(4, 22);
            this.settingsGeneralTab.Name = "settingsGeneralTab";
            this.settingsGeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsGeneralTab.Size = new System.Drawing.Size(549, 553);
            this.settingsGeneralTab.TabIndex = 0;
            this.settingsGeneralTab.Text = "General";
            this.settingsGeneralTab.UseVisualStyleBackColor = true;
            // 
            // settingsLoggingGroupBox
            // 
            this.settingsLoggingGroupBox.Controls.Add(this.settingsVerboseLoggingCheckBox);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingClientFileDialogButton);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingServiceFileDialogButton);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingServicePathTextBox);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingClientPathTextBox);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingClientPathLabel);
            this.settingsLoggingGroupBox.Controls.Add(this.settingsLoggingServicePathLabel);
            this.settingsLoggingGroupBox.Location = new System.Drawing.Point(6, 251);
            this.settingsLoggingGroupBox.Name = "settingsLoggingGroupBox";
            this.settingsLoggingGroupBox.Size = new System.Drawing.Size(532, 117);
            this.settingsLoggingGroupBox.TabIndex = 3;
            this.settingsLoggingGroupBox.TabStop = false;
            this.settingsLoggingGroupBox.Text = "Logging";
            // 
            // settingsVerboseLoggingCheckBox
            // 
            this.settingsVerboseLoggingCheckBox.AutoSize = true;
            this.settingsVerboseLoggingCheckBox.Checked = true;
            this.settingsVerboseLoggingCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settingsVerboseLoggingCheckBox.Location = new System.Drawing.Point(11, 84);
            this.settingsVerboseLoggingCheckBox.Name = "settingsVerboseLoggingCheckBox";
            this.settingsVerboseLoggingCheckBox.Size = new System.Drawing.Size(106, 17);
            this.settingsVerboseLoggingCheckBox.TabIndex = 10;
            this.settingsVerboseLoggingCheckBox.Text = "Verbose Logging";
            this.settingsVerboseLoggingCheckBox.UseVisualStyleBackColor = true;
            // 
            // settingsLoggingClientFileDialogButton
            // 
            this.settingsLoggingClientFileDialogButton.Location = new System.Drawing.Point(326, 17);
            this.settingsLoggingClientFileDialogButton.Name = "settingsLoggingClientFileDialogButton";
            this.settingsLoggingClientFileDialogButton.Size = new System.Drawing.Size(111, 23);
            this.settingsLoggingClientFileDialogButton.TabIndex = 9;
            this.settingsLoggingClientFileDialogButton.Text = "Pick Client Log";
            this.settingsLoggingClientFileDialogButton.UseVisualStyleBackColor = true;
            this.settingsLoggingClientFileDialogButton.Visible = false;
            // 
            // settingsLoggingServiceFileDialogButton
            // 
            this.settingsLoggingServiceFileDialogButton.Location = new System.Drawing.Point(326, 49);
            this.settingsLoggingServiceFileDialogButton.Name = "settingsLoggingServiceFileDialogButton";
            this.settingsLoggingServiceFileDialogButton.Size = new System.Drawing.Size(111, 23);
            this.settingsLoggingServiceFileDialogButton.TabIndex = 9;
            this.settingsLoggingServiceFileDialogButton.Text = "Pick Service Log";
            this.settingsLoggingServiceFileDialogButton.UseVisualStyleBackColor = true;
            this.settingsLoggingServiceFileDialogButton.Visible = false;
            // 
            // settingsLoggingServicePathTextBox
            // 
            this.settingsLoggingServicePathTextBox.Location = new System.Drawing.Point(131, 51);
            this.settingsLoggingServicePathTextBox.MaxLength = 100;
            this.settingsLoggingServicePathTextBox.Name = "settingsLoggingServicePathTextBox";
            this.settingsLoggingServicePathTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsLoggingServicePathTextBox.TabIndex = 8;
            // 
            // settingsLoggingClientPathTextBox
            // 
            this.settingsLoggingClientPathTextBox.Location = new System.Drawing.Point(131, 19);
            this.settingsLoggingClientPathTextBox.MaxLength = 100;
            this.settingsLoggingClientPathTextBox.Name = "settingsLoggingClientPathTextBox";
            this.settingsLoggingClientPathTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsLoggingClientPathTextBox.TabIndex = 8;
            // 
            // settingsLoggingClientPathLabel
            // 
            this.settingsLoggingClientPathLabel.AutoSize = true;
            this.settingsLoggingClientPathLabel.Location = new System.Drawing.Point(8, 22);
            this.settingsLoggingClientPathLabel.Name = "settingsLoggingClientPathLabel";
            this.settingsLoggingClientPathLabel.Size = new System.Drawing.Size(107, 13);
            this.settingsLoggingClientPathLabel.TabIndex = 0;
            this.settingsLoggingClientPathLabel.Text = "Log File Path (Client):";
            // 
            // settingsLoggingServicePathLabel
            // 
            this.settingsLoggingServicePathLabel.AutoSize = true;
            this.settingsLoggingServicePathLabel.Location = new System.Drawing.Point(8, 54);
            this.settingsLoggingServicePathLabel.Name = "settingsLoggingServicePathLabel";
            this.settingsLoggingServicePathLabel.Size = new System.Drawing.Size(117, 13);
            this.settingsLoggingServicePathLabel.TabIndex = 0;
            this.settingsLoggingServicePathLabel.Text = "Log File Path (Service):";
            // 
            // settingsSmsNotificationGroupBox
            // 
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsSmsTest);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsAlertSmsLabel);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsAddressLabel);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsPassTextBox);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsUserLabel);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsUserTextBox);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsPass);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsiSmsAddressTextBox);
            this.settingsSmsNotificationGroupBox.Controls.Add(this.settingsAlertSmsTextBox);
            this.settingsSmsNotificationGroupBox.Location = new System.Drawing.Point(3, 403);
            this.settingsSmsNotificationGroupBox.Name = "settingsSmsNotificationGroupBox";
            this.settingsSmsNotificationGroupBox.Size = new System.Drawing.Size(532, 144);
            this.settingsSmsNotificationGroupBox.TabIndex = 2;
            this.settingsSmsNotificationGroupBox.TabStop = false;
            this.settingsSmsNotificationGroupBox.Text = "Default Sms Notifications";
            this.settingsSmsNotificationGroupBox.Visible = false;
            // 
            // settingsSmsTest
            // 
            this.settingsSmsTest.Location = new System.Drawing.Point(228, 100);
            this.settingsSmsTest.Name = "settingsSmsTest";
            this.settingsSmsTest.Size = new System.Drawing.Size(75, 23);
            this.settingsSmsTest.TabIndex = 9;
            this.settingsSmsTest.Text = "Test Sms";
            this.settingsSmsTest.UseVisualStyleBackColor = true;
            this.settingsSmsTest.Visible = false;
            this.settingsSmsTest.Click += new System.EventHandler(this.SettingsSmsTestClick);
            // 
            // settingsAlertSmsLabel
            // 
            this.settingsAlertSmsLabel.AutoSize = true;
            this.settingsAlertSmsLabel.Location = new System.Drawing.Point(8, 26);
            this.settingsAlertSmsLabel.Name = "settingsAlertSmsLabel";
            this.settingsAlertSmsLabel.Size = new System.Drawing.Size(41, 13);
            this.settingsAlertSmsLabel.TabIndex = 0;
            this.settingsAlertSmsLabel.Text = "Phone:";
            // 
            // settingsiSmsAddressLabel
            // 
            this.settingsiSmsAddressLabel.AutoSize = true;
            this.settingsiSmsAddressLabel.Location = new System.Drawing.Point(286, 26);
            this.settingsiSmsAddressLabel.Name = "settingsiSmsAddressLabel";
            this.settingsiSmsAddressLabel.Size = new System.Drawing.Size(89, 13);
            this.settingsiSmsAddressLabel.TabIndex = 0;
            this.settingsiSmsAddressLabel.Text = "MTS iSMS Addr.:";
            this.settingsiSmsAddressLabel.Visible = false;
            // 
            // settingsiSmsPassTextBox
            // 
            this.settingsiSmsPassTextBox.Location = new System.Drawing.Point(381, 59);
            this.settingsiSmsPassTextBox.MaxLength = 100;
            this.settingsiSmsPassTextBox.Name = "settingsiSmsPassTextBox";
            this.settingsiSmsPassTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsiSmsPassTextBox.TabIndex = 8;
            this.settingsiSmsPassTextBox.Visible = false;
            // 
            // settingsiSmsUserLabel
            // 
            this.settingsiSmsUserLabel.AutoSize = true;
            this.settingsiSmsUserLabel.Location = new System.Drawing.Point(8, 62);
            this.settingsiSmsUserLabel.Name = "settingsiSmsUserLabel";
            this.settingsiSmsUserLabel.Size = new System.Drawing.Size(86, 13);
            this.settingsiSmsUserLabel.TabIndex = 0;
            this.settingsiSmsUserLabel.Text = "MTS iSMS User:";
            this.settingsiSmsUserLabel.Visible = false;
            // 
            // settingsiSmsUserTextBox
            // 
            this.settingsiSmsUserTextBox.Location = new System.Drawing.Point(103, 59);
            this.settingsiSmsUserTextBox.MaxLength = 100;
            this.settingsiSmsUserTextBox.Name = "settingsiSmsUserTextBox";
            this.settingsiSmsUserTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsiSmsUserTextBox.TabIndex = 8;
            this.settingsiSmsUserTextBox.Visible = false;
            // 
            // settingsiSmsPass
            // 
            this.settingsiSmsPass.AutoSize = true;
            this.settingsiSmsPass.Location = new System.Drawing.Point(286, 62);
            this.settingsiSmsPass.Name = "settingsiSmsPass";
            this.settingsiSmsPass.Size = new System.Drawing.Size(87, 13);
            this.settingsiSmsPass.TabIndex = 0;
            this.settingsiSmsPass.Text = "MTS iSMS Pass:";
            this.settingsiSmsPass.Visible = false;
            // 
            // settingsiSmsAddressTextBox
            // 
            this.settingsiSmsAddressTextBox.Location = new System.Drawing.Point(381, 23);
            this.settingsiSmsAddressTextBox.MaxLength = 100;
            this.settingsiSmsAddressTextBox.Name = "settingsiSmsAddressTextBox";
            this.settingsiSmsAddressTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsiSmsAddressTextBox.TabIndex = 8;
            this.settingsiSmsAddressTextBox.Visible = false;
            // 
            // settingsAlertSmsTextBox
            // 
            this.settingsAlertSmsTextBox.Location = new System.Drawing.Point(103, 23);
            this.settingsAlertSmsTextBox.Name = "settingsAlertSmsTextBox";
            this.settingsAlertSmsTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsAlertSmsTextBox.TabIndex = 6;
            this.settingsAlertSmsTextBox.Visible = false;
            // 
            // settingsServerSettings
            // 
            this.settingsServerSettings.Controls.Add(this.settingsRemoteMonServicePortLabel);
            this.settingsServerSettings.Controls.Add(this.settingsRemoteServerLabel);
            this.settingsServerSettings.Controls.Add(this.settingsRemoteMonServicePortTextBox);
            this.settingsServerSettings.Controls.Add(this.settingsRemoteServerTextBox);
            this.settingsServerSettings.Controls.Add(this.settingsRemoteMonServiceAddressTextBox);
            this.settingsServerSettings.Controls.Add(this.settingsRemoteMonServiceAddressLabel);
            this.settingsServerSettings.Location = new System.Drawing.Point(9, 6);
            this.settingsServerSettings.Name = "settingsServerSettings";
            this.settingsServerSettings.Size = new System.Drawing.Size(532, 94);
            this.settingsServerSettings.TabIndex = 1;
            this.settingsServerSettings.TabStop = false;
            this.settingsServerSettings.Text = "Server Settings";
            // 
            // settingsRemoteMonServicePortLabel
            // 
            this.settingsRemoteMonServicePortLabel.AutoSize = true;
            this.settingsRemoteMonServicePortLabel.Location = new System.Drawing.Point(346, 25);
            this.settingsRemoteMonServicePortLabel.Name = "settingsRemoteMonServicePortLabel";
            this.settingsRemoteMonServicePortLabel.Size = new System.Drawing.Size(29, 13);
            this.settingsRemoteMonServicePortLabel.TabIndex = 0;
            this.settingsRemoteMonServicePortLabel.Text = "Port:";
            this.settingsRemoteMonServicePortLabel.Visible = false;
            // 
            // settingsRemoteServerLabel
            // 
            this.settingsRemoteServerLabel.AutoSize = true;
            this.settingsRemoteServerLabel.Location = new System.Drawing.Point(5, 58);
            this.settingsRemoteServerLabel.Name = "settingsRemoteServerLabel";
            this.settingsRemoteServerLabel.Size = new System.Drawing.Size(158, 13);
            this.settingsRemoteServerLabel.TabIndex = 0;
            this.settingsRemoteServerLabel.Text = "Remote Server Address:";
            // 
            // settingsRemoteMonServicePortTextBox
            // 
            this.settingsRemoteMonServicePortTextBox.Location = new System.Drawing.Point(381, 22);
            this.settingsRemoteMonServicePortTextBox.Name = "settingsRemoteMonServicePortTextBox";
            this.settingsRemoteMonServicePortTextBox.Size = new System.Drawing.Size(53, 20);
            this.settingsRemoteMonServicePortTextBox.TabIndex = 6;
            this.settingsRemoteMonServicePortTextBox.Visible = false;
            // 
            // settingsRemoteServerTextBox
            // 
            this.settingsRemoteServerTextBox.Location = new System.Drawing.Point(172, 55);
            this.settingsRemoteServerTextBox.Name = "settingsRemoteServerTextBox";
            this.settingsRemoteServerTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsRemoteServerTextBox.TabIndex = 6;
            // 
            // settingsRemoteMonServiceAddressTextBox
            // 
            this.settingsRemoteMonServiceAddressTextBox.Location = new System.Drawing.Point(172, 22);
            this.settingsRemoteMonServiceAddressTextBox.Name = "settingsRemoteMonServiceAddressTextBox";
            this.settingsRemoteMonServiceAddressTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsRemoteMonServiceAddressTextBox.TabIndex = 6;
            // 
            // settingsRemoteMonServiceAddressLabel
            // 
            this.settingsRemoteMonServiceAddressLabel.AutoSize = true;
            this.settingsRemoteMonServiceAddressLabel.Location = new System.Drawing.Point(5, 25);
            this.settingsRemoteMonServiceAddressLabel.Name = "settingsRemoteMonServiceAddressLabel";
            this.settingsRemoteMonServiceAddressLabel.Size = new System.Drawing.Size(148, 13);
            this.settingsRemoteMonServiceAddressLabel.TabIndex = 0;
            this.settingsRemoteMonServiceAddressLabel.Text = "RemoteMon Service Address:";
            // 
            // settingEmailNotificationsGroupBox
            // 
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsMailServerSslCheckBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsMailServerPortMaskedTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsEmailTest);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsMailServerAddressTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailPasswordTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailAccountTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailPasswordLabel);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailFromTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailToTextBox);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailAccountLabel);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailFromLabel);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsAlertEmailToLabel);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsMailServerPortLabel);
            this.settingEmailNotificationsGroupBox.Controls.Add(this.settingsMailServerAddressLabel);
            this.settingEmailNotificationsGroupBox.Location = new System.Drawing.Point(9, 106);
            this.settingEmailNotificationsGroupBox.Name = "settingEmailNotificationsGroupBox";
            this.settingEmailNotificationsGroupBox.Size = new System.Drawing.Size(532, 139);
            this.settingEmailNotificationsGroupBox.TabIndex = 0;
            this.settingEmailNotificationsGroupBox.TabStop = false;
            this.settingEmailNotificationsGroupBox.Text = "Default Email Notifications";
            // 
            // settingsMailServerSslCheckBox
            // 
            this.settingsMailServerSslCheckBox.AutoSize = true;
            this.settingsMailServerSslCheckBox.Checked = true;
            this.settingsMailServerSslCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settingsMailServerSslCheckBox.Location = new System.Drawing.Point(289, 78);
            this.settingsMailServerSslCheckBox.Name = "settingsMailServerSslCheckBox";
            this.settingsMailServerSslCheckBox.Size = new System.Drawing.Size(68, 17);
            this.settingsMailServerSslCheckBox.TabIndex = 11;
            this.settingsMailServerSslCheckBox.Text = "Use SSL";
            this.settingsMailServerSslCheckBox.UseVisualStyleBackColor = true;
            // 
            // settingsMailServerPortMaskedTextBox
            // 
            this.settingsMailServerPortMaskedTextBox.Location = new System.Drawing.Point(381, 48);
            this.settingsMailServerPortMaskedTextBox.Mask = "09999";
            this.settingsMailServerPortMaskedTextBox.Name = "settingsMailServerPortMaskedTextBox";
            this.settingsMailServerPortMaskedTextBox.PromptChar = ' ';
            this.settingsMailServerPortMaskedTextBox.RejectInputOnFirstFailure = true;
            this.settingsMailServerPortMaskedTextBox.Size = new System.Drawing.Size(41, 20);
            this.settingsMailServerPortMaskedTextBox.TabIndex = 10;
            this.settingsMailServerPortMaskedTextBox.ValidatingType = typeof(int);
            // 
            // settingsEmailTest
            // 
            this.settingsEmailTest.Location = new System.Drawing.Point(401, 97);
            this.settingsEmailTest.Name = "settingsEmailTest";
            this.settingsEmailTest.Size = new System.Drawing.Size(75, 23);
            this.settingsEmailTest.TabIndex = 9;
            this.settingsEmailTest.Text = "Test Email";
            this.settingsEmailTest.UseVisualStyleBackColor = true;
            this.settingsEmailTest.Click += new System.EventHandler(this.SettingsEmailTestClick);
            // 
            // settingsMailServerAddressTextBox
            // 
            this.settingsMailServerAddressTextBox.Location = new System.Drawing.Point(381, 20);
            this.settingsMailServerAddressTextBox.MaxLength = 100;
            this.settingsMailServerAddressTextBox.Name = "settingsMailServerAddressTextBox";
            this.settingsMailServerAddressTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsMailServerAddressTextBox.TabIndex = 8;
            // 
            // settingsAlertEmailPasswordTextBox
            // 
            this.settingsAlertEmailPasswordTextBox.Location = new System.Drawing.Point(105, 104);
            this.settingsAlertEmailPasswordTextBox.MaxLength = 100;
            this.settingsAlertEmailPasswordTextBox.Name = "settingsAlertEmailPasswordTextBox";
            this.settingsAlertEmailPasswordTextBox.PasswordChar = '*';
            this.settingsAlertEmailPasswordTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsAlertEmailPasswordTextBox.TabIndex = 8;
            // 
            // settingsAlertEmailAccountTextBox
            // 
            this.settingsAlertEmailAccountTextBox.Location = new System.Drawing.Point(105, 75);
            this.settingsAlertEmailAccountTextBox.MaxLength = 100;
            this.settingsAlertEmailAccountTextBox.Name = "settingsAlertEmailAccountTextBox";
            this.settingsAlertEmailAccountTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsAlertEmailAccountTextBox.TabIndex = 8;
            // 
            // settingsAlertEmailPasswordLabel
            // 
            this.settingsAlertEmailPasswordLabel.AutoSize = true;
            this.settingsAlertEmailPasswordLabel.Location = new System.Drawing.Point(8, 107);
            this.settingsAlertEmailPasswordLabel.Name = "settingsAlertEmailPasswordLabel";
            this.settingsAlertEmailPasswordLabel.Size = new System.Drawing.Size(58, 13);
            this.settingsAlertEmailPasswordLabel.TabIndex = 0;
            this.settingsAlertEmailPasswordLabel.Text = "User Pass:";
            // 
            // settingsAlertEmailFromTextBox
            // 
            this.settingsAlertEmailFromTextBox.Location = new System.Drawing.Point(105, 48);
            this.settingsAlertEmailFromTextBox.MaxLength = 100;
            this.settingsAlertEmailFromTextBox.Name = "settingsAlertEmailFromTextBox";
            this.settingsAlertEmailFromTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsAlertEmailFromTextBox.TabIndex = 8;
            // 
            // settingsAlertEmailToTextBox
            // 
            this.settingsAlertEmailToTextBox.Location = new System.Drawing.Point(105, 20);
            this.settingsAlertEmailToTextBox.MaxLength = 100;
            this.settingsAlertEmailToTextBox.Name = "settingsAlertEmailToTextBox";
            this.settingsAlertEmailToTextBox.Size = new System.Drawing.Size(141, 20);
            this.settingsAlertEmailToTextBox.TabIndex = 8;
            // 
            // settingsAlertEmailAccountLabel
            // 
            this.settingsAlertEmailAccountLabel.AutoSize = true;
            this.settingsAlertEmailAccountLabel.Location = new System.Drawing.Point(8, 78);
            this.settingsAlertEmailAccountLabel.Name = "settingsAlertEmailAccountLabel";
            this.settingsAlertEmailAccountLabel.Size = new System.Drawing.Size(63, 13);
            this.settingsAlertEmailAccountLabel.TabIndex = 0;
            this.settingsAlertEmailAccountLabel.Text = "User Name:";
            // 
            // settingsAlertEmailFromLabel
            // 
            this.settingsAlertEmailFromLabel.AutoSize = true;
            this.settingsAlertEmailFromLabel.Location = new System.Drawing.Point(8, 51);
            this.settingsAlertEmailFromLabel.Name = "settingsAlertEmailFromLabel";
            this.settingsAlertEmailFromLabel.Size = new System.Drawing.Size(89, 13);
            this.settingsAlertEmailFromLabel.TabIndex = 0;
            this.settingsAlertEmailFromLabel.Text = "Email Addr. From:";
            // 
            // settingsAlertEmailToLabel
            // 
            this.settingsAlertEmailToLabel.AutoSize = true;
            this.settingsAlertEmailToLabel.Location = new System.Drawing.Point(8, 23);
            this.settingsAlertEmailToLabel.Name = "settingsAlertEmailToLabel";
            this.settingsAlertEmailToLabel.Size = new System.Drawing.Size(92, 13);
            this.settingsAlertEmailToLabel.TabIndex = 0;
            this.settingsAlertEmailToLabel.Text = "Email Address To:";
            // 
            // settingsMailServerPortLabel
            // 
            this.settingsMailServerPortLabel.AutoSize = true;
            this.settingsMailServerPortLabel.Location = new System.Drawing.Point(284, 51);
            this.settingsMailServerPortLabel.Name = "settingsMailServerPortLabel";
            this.settingsMailServerPortLabel.Size = new System.Drawing.Size(85, 13);
            this.settingsMailServerPortLabel.TabIndex = 0;
            this.settingsMailServerPortLabel.Text = "Mail Server Port:";
            // 
            // settingsMailServerAddressLabel
            // 
            this.settingsMailServerAddressLabel.AutoSize = true;
            this.settingsMailServerAddressLabel.Location = new System.Drawing.Point(284, 23);
            this.settingsMailServerAddressLabel.Name = "settingsMailServerAddressLabel";
            this.settingsMailServerAddressLabel.Size = new System.Drawing.Size(91, 13);
            this.settingsMailServerAddressLabel.TabIndex = 0;
            this.settingsMailServerAddressLabel.Text = "Mail Server Addr.:";
            // 
            // settingsAdvancedTab
            // 
            this.settingsAdvancedTab.Location = new System.Drawing.Point(4, 22);
            this.settingsAdvancedTab.Name = "settingsAdvancedTab";
            this.settingsAdvancedTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsAdvancedTab.Size = new System.Drawing.Size(549, 553);
            this.settingsAdvancedTab.TabIndex = 1;
            this.settingsAdvancedTab.Text = "Advanced";
            this.settingsAdvancedTab.UseVisualStyleBackColor = true;
            // 
            // settingsOkButton
            // 
            this.settingsOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.settingsOkButton.Location = new System.Drawing.Point(349, 581);
            this.settingsOkButton.Name = "settingsOkButton";
            this.settingsOkButton.Size = new System.Drawing.Size(75, 23);
            this.settingsOkButton.TabIndex = 1;
            this.settingsOkButton.Text = "OK";
            this.settingsOkButton.UseVisualStyleBackColor = true;
            this.settingsOkButton.Click += new System.EventHandler(this.SettingsOkButtonClick);
            // 
            // settingsCancelButton
            // 
            this.settingsCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.settingsCancelButton.Location = new System.Drawing.Point(430, 581);
            this.settingsCancelButton.Name = "settingsCancelButton";
            this.settingsCancelButton.Size = new System.Drawing.Size(75, 23);
            this.settingsCancelButton.TabIndex = 2;
            this.settingsCancelButton.Text = "Cancel";
            this.settingsCancelButton.UseVisualStyleBackColor = true;
            // 
            // settingsLoggingFileDialog
            // 
            this.settingsLoggingFileDialog.CheckFileExists = false;
            this.settingsLoggingFileDialog.FileName = "log.log";
            // 
            // Settings
            // 
            this.AcceptButton = this.settingsOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.settingsCancelButton;
            this.ClientSize = new System.Drawing.Size(557, 610);
            this.Controls.Add(this.settingsCancelButton);
            this.Controls.Add(this.settingsOkButton);
            this.Controls.Add(this.settingsTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.settingsTabControl.ResumeLayout(false);
            this.settingsGeneralTab.ResumeLayout(false);
            this.settingsLoggingGroupBox.ResumeLayout(false);
            this.settingsLoggingGroupBox.PerformLayout();
            this.settingsSmsNotificationGroupBox.ResumeLayout(false);
            this.settingsSmsNotificationGroupBox.PerformLayout();
            this.settingsServerSettings.ResumeLayout(false);
            this.settingsServerSettings.PerformLayout();
            this.settingEmailNotificationsGroupBox.ResumeLayout(false);
            this.settingEmailNotificationsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage settingsGeneralTab;
        private System.Windows.Forms.TabPage settingsAdvancedTab;
        private System.Windows.Forms.Button settingsOkButton;
        private System.Windows.Forms.Button settingsCancelButton;
        private System.Windows.Forms.GroupBox settingEmailNotificationsGroupBox;
        public System.Windows.Forms.TextBox settingsAlertEmailToTextBox;
        public System.Windows.Forms.TextBox settingsAlertSmsTextBox;
        private System.Windows.Forms.GroupBox settingsServerSettings;
        private System.Windows.Forms.Label settingsRemoteServerLabel;
        public System.Windows.Forms.TextBox settingsRemoteServerTextBox;
        public System.Windows.Forms.TextBox settingsRemoteMonServiceAddressTextBox;
        private System.Windows.Forms.Label settingsRemoteMonServiceAddressLabel;
        public System.Windows.Forms.TextBox settingsiSmsPassTextBox;
        public System.Windows.Forms.TextBox settingsiSmsUserTextBox;
        public System.Windows.Forms.TextBox settingsiSmsAddressTextBox;
        public System.Windows.Forms.TextBox settingsMailServerAddressTextBox;
        private System.Windows.Forms.Label settingsiSmsPass;
        private System.Windows.Forms.Label settingsiSmsUserLabel;
        private System.Windows.Forms.Label settingsiSmsAddressLabel;
        private System.Windows.Forms.Label settingsMailServerAddressLabel;
        private System.Windows.Forms.Label settingsAlertSmsLabel;
        private System.Windows.Forms.Label settingsAlertEmailToLabel;
        private System.Windows.Forms.Button settingsSmsTest;
        private System.Windows.Forms.Button settingsEmailTest;
        private System.Windows.Forms.GroupBox settingsSmsNotificationGroupBox;
        private System.Windows.Forms.GroupBox settingsLoggingGroupBox;
        private System.Windows.Forms.OpenFileDialog settingsLoggingFileDialog;
        private System.Windows.Forms.Button settingsLoggingServiceFileDialogButton;
        public System.Windows.Forms.TextBox settingsLoggingClientPathTextBox;
        private System.Windows.Forms.Label settingsLoggingServicePathLabel;
        private System.Windows.Forms.Button settingsLoggingClientFileDialogButton;
        public System.Windows.Forms.TextBox settingsLoggingServicePathTextBox;
        private System.Windows.Forms.Label settingsLoggingClientPathLabel;
        private System.Windows.Forms.Label settingsRemoteMonServicePortLabel;
        public System.Windows.Forms.TextBox settingsRemoteMonServicePortTextBox;
        public System.Windows.Forms.CheckBox settingsVerboseLoggingCheckBox;
        public System.Windows.Forms.MaskedTextBox settingsMailServerPortMaskedTextBox;
        public System.Windows.Forms.TextBox settingsAlertEmailPasswordTextBox;
        public System.Windows.Forms.TextBox settingsAlertEmailAccountTextBox;
        private System.Windows.Forms.Label settingsAlertEmailPasswordLabel;
        private System.Windows.Forms.Label settingsAlertEmailAccountLabel;
        private System.Windows.Forms.Label settingsMailServerPortLabel;
        public System.Windows.Forms.TextBox settingsAlertEmailFromTextBox;
        private System.Windows.Forms.Label settingsAlertEmailFromLabel;
        public System.Windows.Forms.CheckBox settingsMailServerSslCheckBox;
    }
}