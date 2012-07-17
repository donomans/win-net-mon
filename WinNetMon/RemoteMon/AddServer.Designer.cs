using System;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    partial class AddServer : Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServer));
            this.addServerTabControl = new System.Windows.Forms.TabControl();
            this.perfCountersTab = new System.Windows.Forms.TabPage();
            this.perfCounterPerfCounterSetupGroupBox = new System.Windows.Forms.GroupBox();
            this.perfCounterTestDataUpdateFreqLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataUpdateFreqTextBox = new System.Windows.Forms.TextBox();
            this.perfCounterTestDataThresholdBreachTextBox = new System.Windows.Forms.TextBox();
            this.perfCounterTestDataThresholdLessThanCheckBox = new System.Windows.Forms.CheckBox();
            this.perfCounterTestDataThresholdBreachLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataHelpText = new System.Windows.Forms.TextBox();
            this.perfCounterTestDataHelpLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataStatus = new System.Windows.Forms.Label();
            this.perfCounterTestDataThresholdPanicTextBox = new System.Windows.Forms.TextBox();
            this.perfCounterTestDataThresholdWarningTextBox = new System.Windows.Forms.TextBox();
            this.perfCounterTestDataStatusLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataThresholdPanicLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataThresholdWarningLabel = new System.Windows.Forms.Label();
            this.perfCounterTestDataType = new System.Windows.Forms.Label();
            this.perfCounterTestDataTypeLabel = new System.Windows.Forms.Label();
            this.perfCounterTestData = new System.Windows.Forms.Label();
            this.perfCounterTestDataLabel = new System.Windows.Forms.Label();
            this.perfCounterTestBtn = new System.Windows.Forms.Button();
            this.perfCounterInstanceNameWaitLabel = new System.Windows.Forms.Label();
            this.perfCounterInstanceNameDdl = new System.Windows.Forms.ComboBox();
            this.perfCounterInstanceNameLabel = new System.Windows.Forms.Label();
            this.perfCounterCounterNameWaitLabel = new System.Windows.Forms.Label();
            this.perfCounterCounterNameDdl = new System.Windows.Forms.ComboBox();
            this.perfCounterCounterNameLabel = new System.Windows.Forms.Label();
            this.perfCounterCategoryWaitLabel = new System.Windows.Forms.Label();
            this.perfCounterCategoryLabel = new System.Windows.Forms.Label();
            this.perfCounterPCTypeDdl = new System.Windows.Forms.ComboBox();
            this.eventsTab = new System.Windows.Forms.TabPage();
            this.eventMonitorGroupBox = new System.Windows.Forms.GroupBox();
            this.eventMonitorTestDataUpdateFreqLabel = new System.Windows.Forms.Label();
            this.eventMonitorTestDataUpdateFreqTextBox = new System.Windows.Forms.TextBox();
            this.eventMonitorClearLogCb = new System.Windows.Forms.CheckBox();
            this.eventMonitorSourceFilterTextBox = new System.Windows.Forms.TextBox();
            this.eventMonitorSourceFilterSourceNameLabel = new System.Windows.Forms.Label();
            this.eventMonitorSourceFilterBtn = new System.Windows.Forms.Button();
            this.eventMonitorEntryTypeFilterBtn = new System.Windows.Forms.Button();
            this.eventMonitorEntryTypeFilterPanel = new System.Windows.Forms.Panel();
            this.eventMonitorEntryTypeFilterCbWarning = new System.Windows.Forms.CheckBox();
            this.eventMonitorEntryTypeFilterCbSuccessAudit = new System.Windows.Forms.CheckBox();
            this.eventMonitorEntryTypeFilterCbInformation = new System.Windows.Forms.CheckBox();
            this.eventMonitorEntryTypeFilterCbFailureAudit = new System.Windows.Forms.CheckBox();
            this.eventMonitorEntryTypeFilterCbError = new System.Windows.Forms.CheckBox();
            this.eventMonitorEventsLogTypeDdlErrorLabel = new System.Windows.Forms.Label();
            this.eventMonitorEventsLogTypeDdlWaitLabel = new System.Windows.Forms.Label();
            this.eventMonitorEventsLogsFilteredListView = new System.Windows.Forms.ListView();
            this.eventMonitorEventsLogsFilterColumnEventTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventMonitorEventsLogsFilterColumnEntryType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventMonitorEventsLogsFilterColumnCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventMonitorEventsLogsFilterColumnSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventMonitorEventsLogsFilterColumnMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventMonitorEventsLogTypeDdl = new System.Windows.Forms.ComboBox();
            this.eventMonitorEventsLogTypeDdlLabel = new System.Windows.Forms.Label();
            this.servicesTab = new System.Windows.Forms.TabPage();
            this.servicesServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.servicesAutomaticRestartServiceCheckBox = new System.Windows.Forms.CheckBox();
            this.servicesPickedServicesTestDataUpdateFreqLabel = new System.Windows.Forms.Label();
            this.servicesPickedServicesTestDataUpdateFreqTextBox = new System.Windows.Forms.TextBox();
            this.servicesPickedServicesListView = new System.Windows.Forms.ListView();
            this.servicesPickedServiceListViewServiceNameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.servicesPickedServiceListViewServiceStatusCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.servicesServiceLabel = new System.Windows.Forms.Label();
            this.servicesPickedServicesClearBtn = new System.Windows.Forms.Button();
            this.servicesServiceListView = new System.Windows.Forms.ListView();
            this.servicesServiceListViewServiceNameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.servicesServiceListViewStatusCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.servicesPickedServicesLabel = new System.Windows.Forms.Label();
            this.addServerCancelBtn = new System.Windows.Forms.Button();
            this.addServerOkBtn = new System.Windows.Forms.Button();
            this.addServerValidIpGroupBox = new System.Windows.Forms.GroupBox();
            this.addServerValidIpMonitorName = new System.Windows.Forms.TextBox();
            this.addServerValidIpMonitorNameLabel = new System.Windows.Forms.Label();
            this.addServerValidIpIpLabel = new System.Windows.Forms.Label();
            this.addServerValidIpLabel = new System.Windows.Forms.Label();
            this.addServerValidIpPingBtn = new System.Windows.Forms.Button();
            this.addServerValidIpIpTextBox = new System.Windows.Forms.TextBox();
            this.testMonitorUpdate = new System.Windows.Forms.Timer(this.components);
            this.addServerAlertConfiguration = new System.Windows.Forms.GroupBox();
            this.commonFourLabel = new System.Windows.Forms.Label();
            this.addServerAlertEmailTextBox = new System.Windows.Forms.TextBox();
            this.addServerAlertSmsTextBox = new System.Windows.Forms.TextBox();
            this.addServerAlertEmailOption = new System.Windows.Forms.CheckBox();
            this.addServerAlertSmsOption = new System.Windows.Forms.CheckBox();
            this.basicMonitorTestDataUpdateFreqTextBox = new System.Windows.Forms.TextBox();
            this.commonMonitorTestDataUpdateFreqTextBox = new System.Windows.Forms.TextBox();
            this.commonBaseTab = new System.Windows.Forms.TabPage();
            this.commonGeneralSetupGroupBox = new System.Windows.Forms.GroupBox();
            this.commonThreeLabel = new System.Windows.Forms.Label();
            this.commonMonitorTestDataUpdateFreqLabel = new System.Windows.Forms.Label();
            this.commonMonitorTestDataUpdateResultStatus = new System.Windows.Forms.Label();
            this.commonMonitorTestDataUpdateResult = new System.Windows.Forms.Label();
            this.commonMonitorCheckBtn = new System.Windows.Forms.Button();
            this.commonMonitorTestDataUpdateResultLabel = new System.Windows.Forms.Label();
            this.commonChooserGroupBox = new System.Windows.Forms.GroupBox();
            this.commonTwoLabel = new System.Windows.Forms.Label();
            this.commonHddGroupBox = new System.Windows.Forms.GroupBox();
            this.commonHddDriveLetterDdl = new System.Windows.Forms.ComboBox();
            this.commonHddSelected = new System.Windows.Forms.CheckBox();
            this.commonHddGtLtLabel = new System.Windows.Forms.Label();
            this.commonHddCriticalTextBox = new System.Windows.Forms.TextBox();
            this.commonHddWarningTextBox = new System.Windows.Forms.TextBox();
            this.commonHddDriveLetterLabel = new System.Windows.Forms.Label();
            this.commonHddCriticalPctLabel = new System.Windows.Forms.Label();
            this.commonHddWarningPctLabel = new System.Windows.Forms.Label();
            this.commonHddCriticalLabel = new System.Windows.Forms.Label();
            this.commonHddWarningLabel = new System.Windows.Forms.Label();
            this.commonMemoryGroupBox = new System.Windows.Forms.GroupBox();
            this.commonMemoryUsageTypeDdl = new System.Windows.Forms.ComboBox();
            this.commonMemorySelected = new System.Windows.Forms.CheckBox();
            this.commonMemoryGtLtLabel = new System.Windows.Forms.Label();
            this.commonMemoryWarningLabel = new System.Windows.Forms.Label();
            this.commonMemoryCriticalTextBox = new System.Windows.Forms.TextBox();
            this.commonMemoryCriticalLabel = new System.Windows.Forms.Label();
            this.commonMemoryUsageTypeLabel = new System.Windows.Forms.Label();
            this.commonMemoryWarningTextBox = new System.Windows.Forms.TextBox();
            this.commonMemoryWarningPctLabel = new System.Windows.Forms.Label();
            this.commonMemoryCriticalPctLabel = new System.Windows.Forms.Label();
            this.commonServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.commonServiceServicesChoiceDdl = new System.Windows.Forms.ComboBox();
            this.commonServiceSelected = new System.Windows.Forms.CheckBox();
            this.commonServiceServicesChoiceLabel = new System.Windows.Forms.Label();
            this.commonCpuGroupBox = new System.Windows.Forms.GroupBox();
            this.commonCpuUsageTypeDdl = new System.Windows.Forms.ComboBox();
            this.commonCpuSelected = new System.Windows.Forms.CheckBox();
            this.commonCpuGtLtLabel = new System.Windows.Forms.Label();
            this.commonCpuWarningLabel = new System.Windows.Forms.Label();
            this.commonCpuCriticalTextBox = new System.Windows.Forms.TextBox();
            this.commonCpuCriticalPctLabel = new System.Windows.Forms.Label();
            this.commonCpuCriticalLabel = new System.Windows.Forms.Label();
            this.commonCpuUsageTypeLabel = new System.Windows.Forms.Label();
            this.commonCpuWarningPctLabel = new System.Windows.Forms.Label();
            this.commonCpuWarningTextBox = new System.Windows.Forms.TextBox();
            this.commonSwapFileGroupBox = new System.Windows.Forms.GroupBox();
            this.commonSwapFileUsageTypeDdl = new System.Windows.Forms.ComboBox();
            this.commonSwapFileSelected = new System.Windows.Forms.CheckBox();
            this.commonSwapFileGtLtLabel = new System.Windows.Forms.Label();
            this.commonSwapFileWarningLabel = new System.Windows.Forms.Label();
            this.commonSwapFileCriticalTextBox = new System.Windows.Forms.TextBox();
            this.commonSwapFileUsageTypeLabel = new System.Windows.Forms.Label();
            this.commonSwapFileWarningPctLabel = new System.Windows.Forms.Label();
            this.commonSwapFileWarningTextBox = new System.Windows.Forms.TextBox();
            this.commonSwapFileCriticalLabel = new System.Windows.Forms.Label();
            this.commonSwapFileCriticalPctLabel = new System.Windows.Forms.Label();
            this.commonProcessGroupBox = new System.Windows.Forms.GroupBox();
            this.commonProcessStateUsageTypeDdl = new System.Windows.Forms.ComboBox();
            this.commonProcessStateProcessDd = new System.Windows.Forms.ComboBox();
            this.commonProcessStateGtLtLabel = new System.Windows.Forms.Label();
            this.commonProcessStateWarningLabel = new System.Windows.Forms.Label();
            this.commonProcessStateCriticalTextBox = new System.Windows.Forms.TextBox();
            this.commonProcessSelected = new System.Windows.Forms.CheckBox();
            this.commonProcessStateProcessLabel = new System.Windows.Forms.Label();
            this.commonProcessStateWarningPctLabel = new System.Windows.Forms.Label();
            this.commonProcessStateUsageTypeLabel = new System.Windows.Forms.Label();
            this.commonProcessStateWarningTextBox = new System.Windows.Forms.TextBox();
            this.commonProcessStateCriticalPctLabel = new System.Windows.Forms.Label();
            this.commonProcessStateCriticalLabel = new System.Windows.Forms.Label();
            this.addServerValidIpGroupBox2 = new System.Windows.Forms.GroupBox();
            this.commonOneLabel = new System.Windows.Forms.Label();
            this.addServerValidIpMonitorName2 = new System.Windows.Forms.TextBox();
            this.addServerValidIpMonitorNameLabel2 = new System.Windows.Forms.Label();
            this.addServerValidIpIpLabel2 = new System.Windows.Forms.Label();
            this.addServerValidIpLabel2 = new System.Windows.Forms.Label();
            this.addServerValidIpPingBtn2 = new System.Windows.Forms.Button();
            this.addServerValidIpIpTextBox2 = new System.Windows.Forms.TextBox();
            this.basicBaseTab = new System.Windows.Forms.TabPage();
            this.addServerValidIpMonitorName3 = new System.Windows.Forms.TextBox();
            this.addServerValidIpMonitorNameLabel3 = new System.Windows.Forms.Label();
            this.basicMonitorTestDataUpdateFreqLabel = new System.Windows.Forms.Label();
            this.basicMonitorFtpGroupBox = new System.Windows.Forms.GroupBox();
            this.basicMonitorFtpUseSslCheckBox = new System.Windows.Forms.CheckBox();
            this.basicMonitorFtpMonitorSelect = new System.Windows.Forms.CheckBox();
            this.basicMonitorFtpPathPortTextBox = new System.Windows.Forms.MaskedTextBox();
            this.basicMonitorFtpPassTextBox = new System.Windows.Forms.TextBox();
            this.basicMonitorFtpUserTextBox = new System.Windows.Forms.TextBox();
            this.basicMonitorFtpPathTextBox = new System.Windows.Forms.TextBox();
            this.basicMonitorFtpPassLabel = new System.Windows.Forms.Label();
            this.basicMonitorFtpPathPortLabel = new System.Windows.Forms.Label();
            this.basicMonitorFtpUserLabel = new System.Windows.Forms.Label();
            this.basicMonitorFtpPathLabel = new System.Windows.Forms.Label();
            this.basicMonitorFtpPathCheckResultText = new System.Windows.Forms.Label();
            this.basicMonitorFtpPathCheckBtn = new System.Windows.Forms.Button();
            this.basicMonitorFtpPathCheckResultLabel = new System.Windows.Forms.Label();
            this.basicMonitorPingGroupBox = new System.Windows.Forms.GroupBox();
            this.basicMonitorPingMonitorSelect = new System.Windows.Forms.CheckBox();
            this.basicMonitorIpResult = new System.Windows.Forms.Label();
            this.basicMonitorIpResultLabel = new System.Windows.Forms.Label();
            this.basicMonitorIpCheckBtn = new System.Windows.Forms.Button();
            this.basicMonitorIpTextBox = new System.Windows.Forms.TextBox();
            this.basicMonitorIpLabel = new System.Windows.Forms.Label();
            this.basicMonitorHttpGroupBox = new System.Windows.Forms.GroupBox();
            this.basicMonitorHttpMonitorSelect = new System.Windows.Forms.CheckBox();
            this.basicMonitorHttpPathPortTextBox = new System.Windows.Forms.MaskedTextBox();
            this.basicMonitorHttpPathPortLabel = new System.Windows.Forms.Label();
            this.basicMonitorHttpPathCheckResultText = new System.Windows.Forms.Label();
            this.basicMonitorHttpPathCheckResultLabel = new System.Windows.Forms.Label();
            this.basicMonitorHttpPathCheckBtn = new System.Windows.Forms.Button();
            this.basicMonitorHttpPathTextBox = new System.Windows.Forms.TextBox();
            this.basicMonitorHttpPathLabel = new System.Windows.Forms.Label();
            this.advancedBaseTab = new System.Windows.Forms.TabPage();
            this.addServerBaseTabControl = new System.Windows.Forms.TabControl();
            this.addServerToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addServerTabControl.SuspendLayout();
            this.perfCountersTab.SuspendLayout();
            this.perfCounterPerfCounterSetupGroupBox.SuspendLayout();
            this.eventsTab.SuspendLayout();
            this.eventMonitorGroupBox.SuspendLayout();
            this.eventMonitorEntryTypeFilterPanel.SuspendLayout();
            this.servicesTab.SuspendLayout();
            this.servicesServiceGroupBox.SuspendLayout();
            this.addServerValidIpGroupBox.SuspendLayout();
            this.addServerAlertConfiguration.SuspendLayout();
            this.commonBaseTab.SuspendLayout();
            this.commonGeneralSetupGroupBox.SuspendLayout();
            this.commonChooserGroupBox.SuspendLayout();
            this.commonHddGroupBox.SuspendLayout();
            this.commonMemoryGroupBox.SuspendLayout();
            this.commonServiceGroupBox.SuspendLayout();
            this.commonCpuGroupBox.SuspendLayout();
            this.commonSwapFileGroupBox.SuspendLayout();
            this.commonProcessGroupBox.SuspendLayout();
            this.addServerValidIpGroupBox2.SuspendLayout();
            this.basicBaseTab.SuspendLayout();
            this.basicMonitorFtpGroupBox.SuspendLayout();
            this.basicMonitorPingGroupBox.SuspendLayout();
            this.basicMonitorHttpGroupBox.SuspendLayout();
            this.advancedBaseTab.SuspendLayout();
            this.addServerBaseTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // addServerTabControl
            // 
            this.addServerTabControl.Controls.Add(this.perfCountersTab);
            this.addServerTabControl.Controls.Add(this.eventsTab);
            this.addServerTabControl.Controls.Add(this.servicesTab);
            this.addServerTabControl.HotTrack = true;
            this.addServerTabControl.Location = new System.Drawing.Point(3, 57);
            this.addServerTabControl.Name = "addServerTabControl";
            this.addServerTabControl.SelectedIndex = 0;
            this.addServerTabControl.ShowToolTips = true;
            this.addServerTabControl.Size = new System.Drawing.Size(586, 453);
            this.addServerTabControl.TabIndex = 0;
            this.addServerTabControl.SelectedIndexChanged += this.AddServerTabControlSelectedIndexChanged;
            // 
            // perfCountersTab
            // 
            this.perfCountersTab.Controls.Add(this.perfCounterPerfCounterSetupGroupBox);
            this.perfCountersTab.Location = new System.Drawing.Point(4, 22);
            this.perfCountersTab.Name = "perfCountersTab";
            this.perfCountersTab.Padding = new System.Windows.Forms.Padding(3);
            this.perfCountersTab.Size = new System.Drawing.Size(578, 427);
            this.perfCountersTab.TabIndex = 0;
            this.perfCountersTab.Text = "Perf Counters";
            this.perfCountersTab.UseVisualStyleBackColor = true;
            // 
            // perfCounterPerfCounterSetupGroupBox
            // 
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataUpdateFreqLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataUpdateFreqTextBox);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdBreachTextBox);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdLessThanCheckBox);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdBreachLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataHelpText);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataHelpLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataStatus);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdPanicTextBox);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdWarningTextBox);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataStatusLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdPanicLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataThresholdWarningLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataType);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataTypeLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestData);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestDataLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterTestBtn);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterInstanceNameWaitLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterInstanceNameDdl);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterInstanceNameLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterCounterNameWaitLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterCounterNameDdl);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterCounterNameLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterCategoryWaitLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterCategoryLabel);
            this.perfCounterPerfCounterSetupGroupBox.Controls.Add(this.perfCounterPCTypeDdl);
            this.perfCounterPerfCounterSetupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.perfCounterPerfCounterSetupGroupBox.Location = new System.Drawing.Point(3, 3);
            this.perfCounterPerfCounterSetupGroupBox.Name = "perfCounterPerfCounterSetupGroupBox";
            this.perfCounterPerfCounterSetupGroupBox.Size = new System.Drawing.Size(572, 421);
            this.perfCounterPerfCounterSetupGroupBox.TabIndex = 3;
            this.perfCounterPerfCounterSetupGroupBox.TabStop = false;
            this.perfCounterPerfCounterSetupGroupBox.Text = "Performance Counter Configuration";
            // 
            // perfCounterTestDataUpdateFreqLabel
            // 
            this.perfCounterTestDataUpdateFreqLabel.Location = new System.Drawing.Point(6, 305);
            this.perfCounterTestDataUpdateFreqLabel.Name = "perfCounterTestDataUpdateFreqLabel";
            this.perfCounterTestDataUpdateFreqLabel.Size = new System.Drawing.Size(90, 34);
            this.perfCounterTestDataUpdateFreqLabel.TabIndex = 22;
            this.perfCounterTestDataUpdateFreqLabel.Text = "Update Frequency (ms):";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataUpdateFreqLabel, "How long between checks, in milliseconds.");
            // 
            // perfCounterTestDataUpdateFreqTextBox
            // 
            this.perfCounterTestDataUpdateFreqTextBox.Location = new System.Drawing.Point(102, 308);
            this.perfCounterTestDataUpdateFreqTextBox.Name = "perfCounterTestDataUpdateFreqTextBox";
            this.perfCounterTestDataUpdateFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.perfCounterTestDataUpdateFreqTextBox.TabIndex = 21;
            this.perfCounterTestDataUpdateFreqTextBox.Text = "10000";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataUpdateFreqTextBox, "How long between checks, in milliseconds. (default: 10000, 10 seconds)");
            // 
            // perfCounterTestDataThresholdBreachTextBox
            // 
            this.perfCounterTestDataThresholdBreachTextBox.Location = new System.Drawing.Point(102, 266);
            this.perfCounterTestDataThresholdBreachTextBox.MaxLength = 2;
            this.perfCounterTestDataThresholdBreachTextBox.Name = "perfCounterTestDataThresholdBreachTextBox";
            this.perfCounterTestDataThresholdBreachTextBox.Size = new System.Drawing.Size(100, 20);
            this.perfCounterTestDataThresholdBreachTextBox.TabIndex = 5;
            this.perfCounterTestDataThresholdBreachTextBox.Text = "1";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdBreachTextBox, "Enter a number greater than 1 (X).  if the alert should only");
            // 
            // perfCounterTestDataThresholdLessThanCheckBox
            // 
            this.perfCounterTestDataThresholdLessThanCheckBox.AutoSize = true;
            this.perfCounterTestDataThresholdLessThanCheckBox.Location = new System.Drawing.Point(27, 209);
            this.perfCounterTestDataThresholdLessThanCheckBox.Name = "perfCounterTestDataThresholdLessThanCheckBox";
            this.perfCounterTestDataThresholdLessThanCheckBox.Size = new System.Drawing.Size(216, 17);
            this.perfCounterTestDataThresholdLessThanCheckBox.TabIndex = 1;
            this.perfCounterTestDataThresholdLessThanCheckBox.Text = "Threshold Warning/Panic on Less Than";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdLessThanCheckBox, "Threshold values throws warning/panic when actual value is less than if checked (" +
                    "default: unchecked)");
            this.perfCounterTestDataThresholdLessThanCheckBox.UseVisualStyleBackColor = true;
            this.perfCounterTestDataThresholdLessThanCheckBox.CheckedChanged += this.AddServerAlertSmsOptionCheckedChanged;
            // 
            // perfCounterTestDataThresholdBreachLabel
            // 
            this.perfCounterTestDataThresholdBreachLabel.Location = new System.Drawing.Point(6, 266);
            this.perfCounterTestDataThresholdBreachLabel.Name = "perfCounterTestDataThresholdBreachLabel";
            this.perfCounterTestDataThresholdBreachLabel.Size = new System.Drawing.Size(82, 30);
            this.perfCounterTestDataThresholdBreachLabel.TabIndex = 20;
            this.perfCounterTestDataThresholdBreachLabel.Text = "Threshold Breach Count:";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdBreachLabel, "Enter a number greater than 1 (X).  if the alert should only");
            // 
            // perfCounterTestDataHelpText
            // 
            this.perfCounterTestDataHelpText.CausesValidation = false;
            this.perfCounterTestDataHelpText.Location = new System.Drawing.Point(310, 129);
            this.perfCounterTestDataHelpText.MaxLength = 2000;
            this.perfCounterTestDataHelpText.Multiline = true;
            this.perfCounterTestDataHelpText.Name = "perfCounterTestDataHelpText";
            this.perfCounterTestDataHelpText.ReadOnly = true;
            this.perfCounterTestDataHelpText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.perfCounterTestDataHelpText.Size = new System.Drawing.Size(248, 190);
            this.perfCounterTestDataHelpText.TabIndex = 19;
            this.perfCounterTestDataHelpText.TabStop = false;
            // 
            // perfCounterTestDataHelpLabel
            // 
            this.perfCounterTestDataHelpLabel.AutoSize = true;
            this.perfCounterTestDataHelpLabel.CausesValidation = false;
            this.perfCounterTestDataHelpLabel.Location = new System.Drawing.Point(307, 113);
            this.perfCounterTestDataHelpLabel.Name = "perfCounterTestDataHelpLabel";
            this.perfCounterTestDataHelpLabel.Size = new System.Drawing.Size(171, 13);
            this.perfCounterTestDataHelpLabel.TabIndex = 18;
            this.perfCounterTestDataHelpLabel.Text = "Help Info on Performance Counter:";
            // 
            // perfCounterTestDataStatus
            // 
            this.perfCounterTestDataStatus.AutoSize = true;
            this.perfCounterTestDataStatus.CausesValidation = false;
            this.perfCounterTestDataStatus.Location = new System.Drawing.Point(99, 238);
            this.perfCounterTestDataStatus.Name = "perfCounterTestDataStatus";
            this.perfCounterTestDataStatus.Size = new System.Drawing.Size(16, 13);
            this.perfCounterTestDataStatus.TabIndex = 17;
            this.perfCounterTestDataStatus.Text = "...";
            // 
            // perfCounterTestDataThresholdPanicTextBox
            // 
            this.perfCounterTestDataThresholdPanicTextBox.Location = new System.Drawing.Point(143, 173);
            this.perfCounterTestDataThresholdPanicTextBox.MaxLength = 32;
            this.perfCounterTestDataThresholdPanicTextBox.Name = "perfCounterTestDataThresholdPanicTextBox";
            this.perfCounterTestDataThresholdPanicTextBox.Size = new System.Drawing.Size(100, 20);
            this.perfCounterTestDataThresholdPanicTextBox.TabIndex = 4;
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdPanicTextBox, "Enter a numeric value to act as a threshold for an alert.");
            // 
            // perfCounterTestDataThresholdWarningTextBox
            // 
            this.perfCounterTestDataThresholdWarningTextBox.Location = new System.Drawing.Point(143, 143);
            this.perfCounterTestDataThresholdWarningTextBox.MaxLength = 32;
            this.perfCounterTestDataThresholdWarningTextBox.Name = "perfCounterTestDataThresholdWarningTextBox";
            this.perfCounterTestDataThresholdWarningTextBox.Size = new System.Drawing.Size(100, 20);
            this.perfCounterTestDataThresholdWarningTextBox.TabIndex = 4;
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdWarningTextBox, "Enter a numeric value to act as a threshold for a warning (optional).");
            // 
            // perfCounterTestDataStatusLabel
            // 
            this.perfCounterTestDataStatusLabel.AutoSize = true;
            this.perfCounterTestDataStatusLabel.Location = new System.Drawing.Point(6, 238);
            this.perfCounterTestDataStatusLabel.Name = "perfCounterTestDataStatusLabel";
            this.perfCounterTestDataStatusLabel.Size = new System.Drawing.Size(40, 13);
            this.perfCounterTestDataStatusLabel.TabIndex = 15;
            this.perfCounterTestDataStatusLabel.Text = "Status:";
            // 
            // perfCounterTestDataThresholdPanicLabel
            // 
            this.perfCounterTestDataThresholdPanicLabel.AutoSize = true;
            this.perfCounterTestDataThresholdPanicLabel.Location = new System.Drawing.Point(6, 176);
            this.perfCounterTestDataThresholdPanicLabel.Name = "perfCounterTestDataThresholdPanicLabel";
            this.perfCounterTestDataThresholdPanicLabel.Size = new System.Drawing.Size(87, 13);
            this.perfCounterTestDataThresholdPanicLabel.TabIndex = 14;
            this.perfCounterTestDataThresholdPanicLabel.Text = "Threshold Panic:";
            // 
            // perfCounterTestDataThresholdWarningLabel
            // 
            this.perfCounterTestDataThresholdWarningLabel.AutoSize = true;
            this.perfCounterTestDataThresholdWarningLabel.Location = new System.Drawing.Point(6, 146);
            this.perfCounterTestDataThresholdWarningLabel.Name = "perfCounterTestDataThresholdWarningLabel";
            this.perfCounterTestDataThresholdWarningLabel.Size = new System.Drawing.Size(100, 13);
            this.perfCounterTestDataThresholdWarningLabel.TabIndex = 14;
            this.perfCounterTestDataThresholdWarningLabel.Text = "Threshold Warning:";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataThresholdWarningLabel, "Enter a numeric value to act as a threshold for an alert.");
            // 
            // perfCounterTestDataType
            // 
            this.perfCounterTestDataType.AutoSize = true;
            this.perfCounterTestDataType.CausesValidation = false;
            this.perfCounterTestDataType.Location = new System.Drawing.Point(376, 49);
            this.perfCounterTestDataType.Name = "perfCounterTestDataType";
            this.perfCounterTestDataType.Size = new System.Drawing.Size(16, 13);
            this.perfCounterTestDataType.TabIndex = 13;
            this.perfCounterTestDataType.Text = "...";
            this.addServerToolTip.SetToolTip(this.perfCounterTestDataType, "Type of data polled.");
            // 
            // perfCounterTestDataTypeLabel
            // 
            this.perfCounterTestDataTypeLabel.AutoSize = true;
            this.perfCounterTestDataTypeLabel.CausesValidation = false;
            this.perfCounterTestDataTypeLabel.Location = new System.Drawing.Point(307, 48);
            this.perfCounterTestDataTypeLabel.Name = "perfCounterTestDataTypeLabel";
            this.perfCounterTestDataTypeLabel.Size = new System.Drawing.Size(57, 13);
            this.perfCounterTestDataTypeLabel.TabIndex = 12;
            this.perfCounterTestDataTypeLabel.Text = "DataType:";
            // 
            // perfCounterTestData
            // 
            this.perfCounterTestData.AutoSize = true;
            this.perfCounterTestData.CausesValidation = false;
            this.perfCounterTestData.Location = new System.Drawing.Point(376, 24);
            this.perfCounterTestData.Name = "perfCounterTestData";
            this.perfCounterTestData.Size = new System.Drawing.Size(16, 13);
            this.perfCounterTestData.TabIndex = 11;
            this.perfCounterTestData.Text = "...";
            this.addServerToolTip.SetToolTip(this.perfCounterTestData, "Most current polled value from the chosen performance counter.");
            // 
            // perfCounterTestDataLabel
            // 
            this.perfCounterTestDataLabel.AutoSize = true;
            this.perfCounterTestDataLabel.CausesValidation = false;
            this.perfCounterTestDataLabel.Location = new System.Drawing.Point(307, 23);
            this.perfCounterTestDataLabel.Name = "perfCounterTestDataLabel";
            this.perfCounterTestDataLabel.Size = new System.Drawing.Size(33, 13);
            this.perfCounterTestDataLabel.TabIndex = 10;
            this.perfCounterTestDataLabel.Text = "Data:";
            // 
            // perfCounterTestBtn
            // 
            this.perfCounterTestBtn.CausesValidation = false;
            this.perfCounterTestBtn.Location = new System.Drawing.Point(102, 103);
            this.perfCounterTestBtn.Name = "perfCounterTestBtn";
            this.perfCounterTestBtn.Size = new System.Drawing.Size(100, 23);
            this.perfCounterTestBtn.TabIndex = 3;
            this.perfCounterTestBtn.Text = "Test Perf Counter";
            this.addServerToolTip.SetToolTip(this.perfCounterTestBtn, "Test the performance counter with the current selections.");
            this.perfCounterTestBtn.UseVisualStyleBackColor = true;
            this.perfCounterTestBtn.Click += this.PerfCounterTestBtnClick;
            // 
            // perfCounterInstanceNameWaitLabel
            // 
            this.perfCounterInstanceNameWaitLabel.AutoSize = true;
            this.perfCounterInstanceNameWaitLabel.Location = new System.Drawing.Point(86, 79);
            this.perfCounterInstanceNameWaitLabel.Name = "perfCounterInstanceNameWaitLabel";
            this.perfCounterInstanceNameWaitLabel.Size = new System.Drawing.Size(73, 13);
            this.perfCounterInstanceNameWaitLabel.TabIndex = 8;
            this.perfCounterInstanceNameWaitLabel.Text = "Please Wait...";
            // 
            // perfCounterInstanceNameDdl
            // 
            this.perfCounterInstanceNameDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.perfCounterInstanceNameDdl.FormattingEnabled = true;
            this.perfCounterInstanceNameDdl.Location = new System.Drawing.Point(71, 76);
            this.perfCounterInstanceNameDdl.Name = "perfCounterInstanceNameDdl";
            this.perfCounterInstanceNameDdl.Size = new System.Drawing.Size(223, 21);
            this.perfCounterInstanceNameDdl.Sorted = true;
            this.perfCounterInstanceNameDdl.TabIndex = 2;
            this.addServerToolTip.SetToolTip(this.perfCounterInstanceNameDdl, "Select an instance.  \"None\" is selected if the Instance is not applicable.");
            // 
            // perfCounterInstanceNameLabel
            // 
            this.perfCounterInstanceNameLabel.AutoSize = true;
            this.perfCounterInstanceNameLabel.Location = new System.Drawing.Point(6, 79);
            this.perfCounterInstanceNameLabel.Name = "perfCounterInstanceNameLabel";
            this.perfCounterInstanceNameLabel.Size = new System.Drawing.Size(51, 13);
            this.perfCounterInstanceNameLabel.TabIndex = 6;
            this.perfCounterInstanceNameLabel.Text = "Instance:";
            this.addServerToolTip.SetToolTip(this.perfCounterInstanceNameLabel, "Select an instance.  \"None\" is selected if the Instance is not applicable.");
            // 
            // perfCounterCounterNameWaitLabel
            // 
            this.perfCounterCounterNameWaitLabel.AutoSize = true;
            this.perfCounterCounterNameWaitLabel.Location = new System.Drawing.Point(86, 51);
            this.perfCounterCounterNameWaitLabel.Name = "perfCounterCounterNameWaitLabel";
            this.perfCounterCounterNameWaitLabel.Size = new System.Drawing.Size(73, 13);
            this.perfCounterCounterNameWaitLabel.TabIndex = 2;
            this.perfCounterCounterNameWaitLabel.Text = "Please Wait...";
            // 
            // perfCounterCounterNameDdl
            // 
            this.perfCounterCounterNameDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.perfCounterCounterNameDdl.FormattingEnabled = true;
            this.perfCounterCounterNameDdl.Location = new System.Drawing.Point(71, 48);
            this.perfCounterCounterNameDdl.Name = "perfCounterCounterNameDdl";
            this.perfCounterCounterNameDdl.Size = new System.Drawing.Size(223, 21);
            this.perfCounterCounterNameDdl.Sorted = true;
            this.perfCounterCounterNameDdl.TabIndex = 1;
            this.addServerToolTip.SetToolTip(this.perfCounterCounterNameDdl, "Select a Counter type.");
            // 
            // perfCounterCounterNameLabel
            // 
            this.perfCounterCounterNameLabel.AutoSize = true;
            this.perfCounterCounterNameLabel.Location = new System.Drawing.Point(6, 51);
            this.perfCounterCounterNameLabel.Name = "perfCounterCounterNameLabel";
            this.perfCounterCounterNameLabel.Size = new System.Drawing.Size(47, 13);
            this.perfCounterCounterNameLabel.TabIndex = 3;
            this.perfCounterCounterNameLabel.Text = "Counter:";
            this.addServerToolTip.SetToolTip(this.perfCounterCounterNameLabel, "Select a Counter type.");
            // 
            // perfCounterCategoryWaitLabel
            // 
            this.perfCounterCategoryWaitLabel.AutoSize = true;
            this.perfCounterCategoryWaitLabel.Location = new System.Drawing.Point(86, 23);
            this.perfCounterCategoryWaitLabel.Name = "perfCounterCategoryWaitLabel";
            this.perfCounterCategoryWaitLabel.Size = new System.Drawing.Size(73, 13);
            this.perfCounterCategoryWaitLabel.TabIndex = 1;
            this.perfCounterCategoryWaitLabel.Text = "Please Wait...";
            // 
            // perfCounterCategoryLabel
            // 
            this.perfCounterCategoryLabel.AutoSize = true;
            this.perfCounterCategoryLabel.Location = new System.Drawing.Point(6, 23);
            this.perfCounterCategoryLabel.Name = "perfCounterCategoryLabel";
            this.perfCounterCategoryLabel.Size = new System.Drawing.Size(52, 13);
            this.perfCounterCategoryLabel.TabIndex = 1;
            this.perfCounterCategoryLabel.Text = "Category:";
            this.addServerToolTip.SetToolTip(this.perfCounterCategoryLabel, "Select a performance counter Category.");
            // 
            // perfCounterPCTypeDdl
            // 
            this.perfCounterPCTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.perfCounterPCTypeDdl.FormattingEnabled = true;
            this.perfCounterPCTypeDdl.Location = new System.Drawing.Point(71, 20);
            this.perfCounterPCTypeDdl.Name = "perfCounterPCTypeDdl";
            this.perfCounterPCTypeDdl.Size = new System.Drawing.Size(223, 21);
            this.perfCounterPCTypeDdl.Sorted = true;
            this.perfCounterPCTypeDdl.TabIndex = 0;
            this.addServerToolTip.SetToolTip(this.perfCounterPCTypeDdl, "Select a performance counter Category.");
            this.perfCounterPCTypeDdl.SelectedIndexChanged += this.PerfCounterPcTypeDdlSelectedIndexChanged;
            // 
            // eventsTab
            // 
            this.eventsTab.Controls.Add(this.eventMonitorGroupBox);
            this.eventsTab.Location = new System.Drawing.Point(4, 22);
            this.eventsTab.Name = "eventsTab";
            this.eventsTab.Padding = new System.Windows.Forms.Padding(3);
            this.eventsTab.Size = new System.Drawing.Size(578, 427);
            this.eventsTab.TabIndex = 1;
            this.eventsTab.Text = "Events";
            this.eventsTab.UseVisualStyleBackColor = true;
            // 
            // eventMonitorGroupBox
            // 
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorTestDataUpdateFreqLabel);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorTestDataUpdateFreqTextBox);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorClearLogCb);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorSourceFilterTextBox);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorSourceFilterSourceNameLabel);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorSourceFilterBtn);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEntryTypeFilterBtn);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEntryTypeFilterPanel);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEventsLogTypeDdlErrorLabel);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEventsLogTypeDdlWaitLabel);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEventsLogsFilteredListView);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEventsLogTypeDdl);
            this.eventMonitorGroupBox.Controls.Add(this.eventMonitorEventsLogTypeDdlLabel);
            this.eventMonitorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventMonitorGroupBox.Location = new System.Drawing.Point(3, 3);
            this.eventMonitorGroupBox.Name = "eventMonitorGroupBox";
            this.eventMonitorGroupBox.Size = new System.Drawing.Size(572, 421);
            this.eventMonitorGroupBox.TabIndex = 0;
            this.eventMonitorGroupBox.TabStop = false;
            this.eventMonitorGroupBox.Text = "Event Monitor Configuration";
            // 
            // eventMonitorTestDataUpdateFreqLabel
            // 
            this.eventMonitorTestDataUpdateFreqLabel.Location = new System.Drawing.Point(390, 92);
            this.eventMonitorTestDataUpdateFreqLabel.Name = "eventMonitorTestDataUpdateFreqLabel";
            this.eventMonitorTestDataUpdateFreqLabel.Size = new System.Drawing.Size(90, 34);
            this.eventMonitorTestDataUpdateFreqLabel.TabIndex = 24;
            this.eventMonitorTestDataUpdateFreqLabel.Text = "Update Frequency (ms):";
            // 
            // eventMonitorTestDataUpdateFreqTextBox
            // 
            this.eventMonitorTestDataUpdateFreqTextBox.Location = new System.Drawing.Point(477, 101);
            this.eventMonitorTestDataUpdateFreqTextBox.Name = "eventMonitorTestDataUpdateFreqTextBox";
            this.eventMonitorTestDataUpdateFreqTextBox.Size = new System.Drawing.Size(92, 20);
            this.eventMonitorTestDataUpdateFreqTextBox.TabIndex = 23;
            this.eventMonitorTestDataUpdateFreqTextBox.Text = "90000";
            this.addServerToolTip.SetToolTip(this.eventMonitorTestDataUpdateFreqTextBox, "How long between checks, in milliseconds. (default: 90000, 1.5 Minutes)");
            // 
            // eventMonitorClearLogCb
            // 
            this.eventMonitorClearLogCb.Location = new System.Drawing.Point(287, 9);
            this.eventMonitorClearLogCb.Name = "eventMonitorClearLogCb";
            this.eventMonitorClearLogCb.Size = new System.Drawing.Size(97, 46);
            this.eventMonitorClearLogCb.TabIndex = 13;
            this.eventMonitorClearLogCb.Text = "Clear Old Logs Automatically";
            this.eventMonitorClearLogCb.UseVisualStyleBackColor = true;
            // 
            // eventMonitorSourceFilterTextBox
            // 
            this.eventMonitorSourceFilterTextBox.Location = new System.Drawing.Point(97, 101);
            this.eventMonitorSourceFilterTextBox.Name = "eventMonitorSourceFilterTextBox";
            this.eventMonitorSourceFilterTextBox.Size = new System.Drawing.Size(177, 20);
            this.eventMonitorSourceFilterTextBox.TabIndex = 1;
            // 
            // eventMonitorSourceFilterSourceNameLabel
            // 
            this.eventMonitorSourceFilterSourceNameLabel.AutoSize = true;
            this.eventMonitorSourceFilterSourceNameLabel.Location = new System.Drawing.Point(16, 104);
            this.eventMonitorSourceFilterSourceNameLabel.Name = "eventMonitorSourceFilterSourceNameLabel";
            this.eventMonitorSourceFilterSourceNameLabel.Size = new System.Drawing.Size(75, 13);
            this.eventMonitorSourceFilterSourceNameLabel.TabIndex = 2;
            this.eventMonitorSourceFilterSourceNameLabel.Text = "Source Name:";
            // 
            // eventMonitorSourceFilterBtn
            // 
            this.eventMonitorSourceFilterBtn.Location = new System.Drawing.Point(287, 99);
            this.eventMonitorSourceFilterBtn.Name = "eventMonitorSourceFilterBtn";
            this.eventMonitorSourceFilterBtn.Size = new System.Drawing.Size(75, 23);
            this.eventMonitorSourceFilterBtn.TabIndex = 12;
            this.eventMonitorSourceFilterBtn.Text = "Filter Source";
            this.eventMonitorSourceFilterBtn.UseVisualStyleBackColor = true;
            this.eventMonitorSourceFilterBtn.Click += this.EventMonitorSourceFilterBtnClick;
            // 
            // eventMonitorEntryTypeFilterBtn
            // 
            this.eventMonitorEntryTypeFilterBtn.Location = new System.Drawing.Point(287, 61);
            this.eventMonitorEntryTypeFilterBtn.Name = "eventMonitorEntryTypeFilterBtn";
            this.eventMonitorEntryTypeFilterBtn.Size = new System.Drawing.Size(97, 23);
            this.eventMonitorEntryTypeFilterBtn.TabIndex = 10;
            this.eventMonitorEntryTypeFilterBtn.Text = "Filter EntryTypes";
            this.eventMonitorEntryTypeFilterBtn.UseVisualStyleBackColor = true;
            this.eventMonitorEntryTypeFilterBtn.Click += this.EventMonitorEntryTypeFilterBtnClick;
            // 
            // eventMonitorEntryTypeFilterPanel
            // 
            this.eventMonitorEntryTypeFilterPanel.Controls.Add(this.eventMonitorEntryTypeFilterCbWarning);
            this.eventMonitorEntryTypeFilterPanel.Controls.Add(this.eventMonitorEntryTypeFilterCbSuccessAudit);
            this.eventMonitorEntryTypeFilterPanel.Controls.Add(this.eventMonitorEntryTypeFilterCbInformation);
            this.eventMonitorEntryTypeFilterPanel.Controls.Add(this.eventMonitorEntryTypeFilterCbFailureAudit);
            this.eventMonitorEntryTypeFilterPanel.Controls.Add(this.eventMonitorEntryTypeFilterCbError);
            this.eventMonitorEntryTypeFilterPanel.Location = new System.Drawing.Point(18, 49);
            this.eventMonitorEntryTypeFilterPanel.Name = "eventMonitorEntryTypeFilterPanel";
            this.eventMonitorEntryTypeFilterPanel.Size = new System.Drawing.Size(257, 44);
            this.eventMonitorEntryTypeFilterPanel.TabIndex = 3;
            // 
            // eventMonitorEntryTypeFilterCbWarning
            // 
            this.eventMonitorEntryTypeFilterCbWarning.AutoSize = true;
            this.eventMonitorEntryTypeFilterCbWarning.Location = new System.Drawing.Point(188, 3);
            this.eventMonitorEntryTypeFilterCbWarning.Name = "eventMonitorEntryTypeFilterCbWarning";
            this.eventMonitorEntryTypeFilterCbWarning.Size = new System.Drawing.Size(66, 17);
            this.eventMonitorEntryTypeFilterCbWarning.TabIndex = 4;
            this.eventMonitorEntryTypeFilterCbWarning.Text = "Warning";
            this.eventMonitorEntryTypeFilterCbWarning.UseVisualStyleBackColor = true;
            // 
            // eventMonitorEntryTypeFilterCbSuccessAudit
            // 
            this.eventMonitorEntryTypeFilterCbSuccessAudit.AutoSize = true;
            this.eventMonitorEntryTypeFilterCbSuccessAudit.Location = new System.Drawing.Point(93, 22);
            this.eventMonitorEntryTypeFilterCbSuccessAudit.Name = "eventMonitorEntryTypeFilterCbSuccessAudit";
            this.eventMonitorEntryTypeFilterCbSuccessAudit.Size = new System.Drawing.Size(91, 17);
            this.eventMonitorEntryTypeFilterCbSuccessAudit.TabIndex = 3;
            this.eventMonitorEntryTypeFilterCbSuccessAudit.Text = "SuccessAudit";
            this.eventMonitorEntryTypeFilterCbSuccessAudit.UseVisualStyleBackColor = true;
            // 
            // eventMonitorEntryTypeFilterCbInformation
            // 
            this.eventMonitorEntryTypeFilterCbInformation.AutoSize = true;
            this.eventMonitorEntryTypeFilterCbInformation.Location = new System.Drawing.Point(93, 3);
            this.eventMonitorEntryTypeFilterCbInformation.Name = "eventMonitorEntryTypeFilterCbInformation";
            this.eventMonitorEntryTypeFilterCbInformation.Size = new System.Drawing.Size(78, 17);
            this.eventMonitorEntryTypeFilterCbInformation.TabIndex = 2;
            this.eventMonitorEntryTypeFilterCbInformation.Text = "Information";
            this.eventMonitorEntryTypeFilterCbInformation.UseVisualStyleBackColor = true;
            // 
            // eventMonitorEntryTypeFilterCbFailureAudit
            // 
            this.eventMonitorEntryTypeFilterCbFailureAudit.AutoSize = true;
            this.eventMonitorEntryTypeFilterCbFailureAudit.Location = new System.Drawing.Point(7, 22);
            this.eventMonitorEntryTypeFilterCbFailureAudit.Name = "eventMonitorEntryTypeFilterCbFailureAudit";
            this.eventMonitorEntryTypeFilterCbFailureAudit.Size = new System.Drawing.Size(81, 17);
            this.eventMonitorEntryTypeFilterCbFailureAudit.TabIndex = 1;
            this.eventMonitorEntryTypeFilterCbFailureAudit.Text = "FailureAudit";
            this.eventMonitorEntryTypeFilterCbFailureAudit.UseVisualStyleBackColor = true;
            // 
            // eventMonitorEntryTypeFilterCbError
            // 
            this.eventMonitorEntryTypeFilterCbError.AutoSize = true;
            this.eventMonitorEntryTypeFilterCbError.Location = new System.Drawing.Point(7, 3);
            this.eventMonitorEntryTypeFilterCbError.Name = "eventMonitorEntryTypeFilterCbError";
            this.eventMonitorEntryTypeFilterCbError.Size = new System.Drawing.Size(48, 17);
            this.eventMonitorEntryTypeFilterCbError.TabIndex = 0;
            this.eventMonitorEntryTypeFilterCbError.Text = "Error";
            this.eventMonitorEntryTypeFilterCbError.UseVisualStyleBackColor = true;
            // 
            // eventMonitorEventsLogTypeDdlErrorLabel
            // 
            this.eventMonitorEventsLogTypeDdlErrorLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eventMonitorEventsLogTypeDdlErrorLabel.Location = new System.Drawing.Point(390, 16);
            this.eventMonitorEventsLogTypeDdlErrorLabel.Name = "eventMonitorEventsLogTypeDdlErrorLabel";
            this.eventMonitorEventsLogTypeDdlErrorLabel.Size = new System.Drawing.Size(124, 68);
            this.eventMonitorEventsLogTypeDdlErrorLabel.TabIndex = 5;
            // 
            // eventMonitorEventsLogTypeDdlWaitLabel
            // 
            this.eventMonitorEventsLogTypeDdlWaitLabel.AutoSize = true;
            this.eventMonitorEventsLogTypeDdlWaitLabel.Location = new System.Drawing.Point(89, 23);
            this.eventMonitorEventsLogTypeDdlWaitLabel.Name = "eventMonitorEventsLogTypeDdlWaitLabel";
            this.eventMonitorEventsLogTypeDdlWaitLabel.Size = new System.Drawing.Size(73, 13);
            this.eventMonitorEventsLogTypeDdlWaitLabel.TabIndex = 4;
            this.eventMonitorEventsLogTypeDdlWaitLabel.Text = "Please Wait...";
            // 
            // eventMonitorEventsLogsFilteredListView
            // 
            this.eventMonitorEventsLogsFilteredListView.CausesValidation = false;
            this.eventMonitorEventsLogsFilteredListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eventMonitorEventsLogsFilterColumnEventTime,
            this.eventMonitorEventsLogsFilterColumnEntryType,
            this.eventMonitorEventsLogsFilterColumnCategory,
            this.eventMonitorEventsLogsFilterColumnSource,
            this.eventMonitorEventsLogsFilterColumnMessage});
            this.eventMonitorEventsLogsFilteredListView.FullRowSelect = true;
            this.eventMonitorEventsLogsFilteredListView.GridLines = true;
            this.eventMonitorEventsLogsFilteredListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.eventMonitorEventsLogsFilteredListView.Location = new System.Drawing.Point(3, 129);
            this.eventMonitorEventsLogsFilteredListView.MultiSelect = false;
            this.eventMonitorEventsLogsFilteredListView.Name = "eventMonitorEventsLogsFilteredListView";
            this.eventMonitorEventsLogsFilteredListView.ShowGroups = false;
            this.eventMonitorEventsLogsFilteredListView.ShowItemToolTips = true;
            this.eventMonitorEventsLogsFilteredListView.Size = new System.Drawing.Size(566, 286);
            this.eventMonitorEventsLogsFilteredListView.TabIndex = 4;
            this.eventMonitorEventsLogsFilteredListView.UseCompatibleStateImageBehavior = false;
            this.eventMonitorEventsLogsFilteredListView.View = System.Windows.Forms.View.Details;
            // 
            // eventMonitorEventsLogsFilterColumnEventTime
            // 
            this.eventMonitorEventsLogsFilterColumnEventTime.Text = "Time";
            this.eventMonitorEventsLogsFilterColumnEventTime.Width = 125;
            // 
            // eventMonitorEventsLogsFilterColumnEntryType
            // 
            this.eventMonitorEventsLogsFilterColumnEntryType.Text = "Entry Type";
            this.eventMonitorEventsLogsFilterColumnEntryType.Width = 75;
            // 
            // eventMonitorEventsLogsFilterColumnCategory
            // 
            this.eventMonitorEventsLogsFilterColumnCategory.Text = "Category";
            this.eventMonitorEventsLogsFilterColumnCategory.Width = 70;
            // 
            // eventMonitorEventsLogsFilterColumnSource
            // 
            this.eventMonitorEventsLogsFilterColumnSource.Text = "Source";
            this.eventMonitorEventsLogsFilterColumnSource.Width = 90;
            // 
            // eventMonitorEventsLogsFilterColumnMessage
            // 
            this.eventMonitorEventsLogsFilterColumnMessage.Text = "Message";
            this.eventMonitorEventsLogsFilterColumnMessage.Width = 250;
            // 
            // eventMonitorEventsLogTypeDdl
            // 
            this.eventMonitorEventsLogTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventMonitorEventsLogTypeDdl.FormattingEnabled = true;
            this.eventMonitorEventsLogTypeDdl.Items.AddRange(new object[] {
            "Please Wait..."});
            this.eventMonitorEventsLogTypeDdl.Location = new System.Drawing.Point(71, 20);
            this.eventMonitorEventsLogTypeDdl.Name = "eventMonitorEventsLogTypeDdl";
            this.eventMonitorEventsLogTypeDdl.Size = new System.Drawing.Size(183, 21);
            this.eventMonitorEventsLogTypeDdl.TabIndex = 2;
            this.eventMonitorEventsLogTypeDdl.Visible = false;
            this.eventMonitorEventsLogTypeDdl.SelectedIndexChanged += this.EventMonitorEventsLogTypeDdlSelectedIndexChanged;
            // 
            // eventMonitorEventsLogTypeDdlLabel
            // 
            this.eventMonitorEventsLogTypeDdlLabel.AutoSize = true;
            this.eventMonitorEventsLogTypeDdlLabel.CausesValidation = false;
            this.eventMonitorEventsLogTypeDdlLabel.Location = new System.Drawing.Point(6, 23);
            this.eventMonitorEventsLogTypeDdlLabel.Name = "eventMonitorEventsLogTypeDdlLabel";
            this.eventMonitorEventsLogTypeDdlLabel.Size = new System.Drawing.Size(60, 13);
            this.eventMonitorEventsLogTypeDdlLabel.TabIndex = 1;
            this.eventMonitorEventsLogTypeDdlLabel.Text = "Log Types:";
            // 
            // servicesTab
            // 
            this.servicesTab.Controls.Add(this.servicesServiceGroupBox);
            this.servicesTab.Location = new System.Drawing.Point(4, 22);
            this.servicesTab.Name = "servicesTab";
            this.servicesTab.Padding = new System.Windows.Forms.Padding(3);
            this.servicesTab.Size = new System.Drawing.Size(578, 427);
            this.servicesTab.TabIndex = 2;
            this.servicesTab.Text = "Services";
            this.servicesTab.UseVisualStyleBackColor = true;
            // 
            // servicesServiceGroupBox
            // 
            this.servicesServiceGroupBox.Controls.Add(this.servicesAutomaticRestartServiceCheckBox);
            this.servicesServiceGroupBox.Controls.Add(this.servicesPickedServicesTestDataUpdateFreqLabel);
            this.servicesServiceGroupBox.Controls.Add(this.servicesPickedServicesTestDataUpdateFreqTextBox);
            this.servicesServiceGroupBox.Controls.Add(this.servicesPickedServicesListView);
            this.servicesServiceGroupBox.Controls.Add(this.servicesServiceLabel);
            this.servicesServiceGroupBox.Controls.Add(this.servicesPickedServicesClearBtn);
            this.servicesServiceGroupBox.Controls.Add(this.servicesServiceListView);
            this.servicesServiceGroupBox.Controls.Add(this.servicesPickedServicesLabel);
            this.servicesServiceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesServiceGroupBox.Location = new System.Drawing.Point(3, 3);
            this.servicesServiceGroupBox.Name = "servicesServiceGroupBox";
            this.servicesServiceGroupBox.Size = new System.Drawing.Size(572, 421);
            this.servicesServiceGroupBox.TabIndex = 5;
            this.servicesServiceGroupBox.TabStop = false;
            this.servicesServiceGroupBox.Text = "Service Monitor Configuration";
            // 
            // servicesAutomaticRestartServiceCheckBox
            // 
            this.servicesAutomaticRestartServiceCheckBox.AutoSize = true;
            this.servicesAutomaticRestartServiceCheckBox.Location = new System.Drawing.Point(384, 310);
            this.servicesAutomaticRestartServiceCheckBox.Name = "servicesAutomaticRestartServiceCheckBox";
            this.servicesAutomaticRestartServiceCheckBox.Size = new System.Drawing.Size(175, 17);
            this.servicesAutomaticRestartServiceCheckBox.TabIndex = 27;
            this.servicesAutomaticRestartServiceCheckBox.Text = "Automatically Restart Service(s)";
            this.servicesAutomaticRestartServiceCheckBox.UseVisualStyleBackColor = true;
            // 
            // servicesPickedServicesTestDataUpdateFreqLabel
            // 
            this.servicesPickedServicesTestDataUpdateFreqLabel.Location = new System.Drawing.Point(381, 345);
            this.servicesPickedServicesTestDataUpdateFreqLabel.Name = "servicesPickedServicesTestDataUpdateFreqLabel";
            this.servicesPickedServicesTestDataUpdateFreqLabel.Size = new System.Drawing.Size(90, 34);
            this.servicesPickedServicesTestDataUpdateFreqLabel.TabIndex = 26;
            this.servicesPickedServicesTestDataUpdateFreqLabel.Text = "Update Frequency (ms):";
            // 
            // servicesPickedServicesTestDataUpdateFreqTextBox
            // 
            this.servicesPickedServicesTestDataUpdateFreqTextBox.Location = new System.Drawing.Point(477, 354);
            this.servicesPickedServicesTestDataUpdateFreqTextBox.Name = "servicesPickedServicesTestDataUpdateFreqTextBox";
            this.servicesPickedServicesTestDataUpdateFreqTextBox.Size = new System.Drawing.Size(83, 20);
            this.servicesPickedServicesTestDataUpdateFreqTextBox.TabIndex = 25;
            this.servicesPickedServicesTestDataUpdateFreqTextBox.Text = "30000";
            this.addServerToolTip.SetToolTip(this.servicesPickedServicesTestDataUpdateFreqTextBox, "How long between checks, in milliseconds. (default: 30000, 30 seconds)");
            // 
            // servicesPickedServicesListView
            // 
            this.servicesPickedServicesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.servicesPickedServiceListViewServiceNameCol,
            this.servicesPickedServiceListViewServiceStatusCol});
            this.servicesPickedServicesListView.FullRowSelect = true;
            this.servicesPickedServicesListView.GridLines = true;
            this.servicesPickedServicesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.servicesPickedServicesListView.Location = new System.Drawing.Point(3, 268);
            this.servicesPickedServicesListView.Name = "servicesPickedServicesListView";
            this.servicesPickedServicesListView.ShowGroups = false;
            this.servicesPickedServicesListView.Size = new System.Drawing.Size(359, 147);
            this.servicesPickedServicesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.servicesPickedServicesListView.TabIndex = 2;
            this.addServerToolTip.SetToolTip(this.servicesPickedServicesListView, "Double click to change the Alert Status.  The stated status is the expected statu" +
                    "s of the Service.  If the service status does not match that, an alert will be s" +
                    "et.");
            this.servicesPickedServicesListView.UseCompatibleStateImageBehavior = false;
            this.servicesPickedServicesListView.View = System.Windows.Forms.View.Details;
            this.servicesPickedServicesListView.MouseDoubleClick += this.ServicesPickedServicesListViewMouseDoubleClick;
            // 
            // servicesPickedServiceListViewServiceNameCol
            // 
            this.servicesPickedServiceListViewServiceNameCol.Text = "Service Name";
            this.servicesPickedServiceListViewServiceNameCol.Width = 245;
            // 
            // servicesPickedServiceListViewServiceStatusCol
            // 
            this.servicesPickedServiceListViewServiceStatusCol.Text = "Alert if Status is NOT";
            this.servicesPickedServiceListViewServiceStatusCol.Width = 114;
            // 
            // servicesServiceLabel
            // 
            this.servicesServiceLabel.AutoSize = true;
            this.servicesServiceLabel.Location = new System.Drawing.Point(28, 16);
            this.servicesServiceLabel.Name = "servicesServiceLabel";
            this.servicesServiceLabel.Size = new System.Drawing.Size(366, 13);
            this.servicesServiceLabel.TabIndex = 1;
            this.servicesServiceLabel.Text = "Highlight one or more of the services to be alerted when the status changes.";
            // 
            // servicesPickedServicesClearBtn
            // 
            this.servicesPickedServicesClearBtn.Location = new System.Drawing.Point(368, 268);
            this.servicesPickedServicesClearBtn.Name = "servicesPickedServicesClearBtn";
            this.servicesPickedServicesClearBtn.Size = new System.Drawing.Size(75, 23);
            this.servicesPickedServicesClearBtn.TabIndex = 3;
            this.servicesPickedServicesClearBtn.Text = "Clear";
            this.servicesPickedServicesClearBtn.UseVisualStyleBackColor = true;
            this.servicesPickedServicesClearBtn.Click += this.AddServerPickedServicesClearBtnClick;
            // 
            // servicesServiceListView
            // 
            this.servicesServiceListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.servicesServiceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.servicesServiceListViewServiceNameCol,
            this.servicesServiceListViewStatusCol});
            this.servicesServiceListView.FullRowSelect = true;
            this.servicesServiceListView.GridLines = true;
            this.servicesServiceListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.servicesServiceListView.LabelEdit = true;
            this.servicesServiceListView.Location = new System.Drawing.Point(6, 32);
            this.servicesServiceListView.Name = "servicesServiceListView";
            this.servicesServiceListView.Size = new System.Drawing.Size(560, 217);
            this.servicesServiceListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.servicesServiceListView.TabIndex = 0;
            this.servicesServiceListView.UseCompatibleStateImageBehavior = false;
            this.servicesServiceListView.View = System.Windows.Forms.View.Details;
            this.servicesServiceListView.ItemSelectionChanged += this.AddServerServiceListViewItemSelectionChanged;
            // 
            // servicesServiceListViewServiceNameCol
            // 
            this.servicesServiceListViewServiceNameCol.Text = "Service Name";
            this.servicesServiceListViewServiceNameCol.Width = 341;
            // 
            // servicesServiceListViewStatusCol
            // 
            this.servicesServiceListViewStatusCol.Text = "Service Status";
            this.servicesServiceListViewStatusCol.Width = 153;
            // 
            // servicesPickedServicesLabel
            // 
            this.servicesPickedServicesLabel.AutoSize = true;
            this.servicesPickedServicesLabel.Location = new System.Drawing.Point(28, 252);
            this.servicesPickedServicesLabel.Name = "servicesPickedServicesLabel";
            this.servicesPickedServicesLabel.Size = new System.Drawing.Size(146, 13);
            this.servicesPickedServicesLabel.TabIndex = 4;
            this.servicesPickedServicesLabel.Text = "Selected Services to Monitor:";
            // 
            // addServerCancelBtn
            // 
            this.addServerCancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addServerCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.addServerCancelBtn.Location = new System.Drawing.Point(524, 570);
            this.addServerCancelBtn.Name = "addServerCancelBtn";
            this.addServerCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.addServerCancelBtn.TabIndex = 2;
            this.addServerCancelBtn.Text = "Cancel";
            this.addServerCancelBtn.UseVisualStyleBackColor = true;
            this.addServerCancelBtn.Click += new System.EventHandler(this.AddServerCancelBtnClick);
            // 
            // addServerOkBtn
            // 
            this.addServerOkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addServerOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addServerOkBtn.Location = new System.Drawing.Point(443, 570);
            this.addServerOkBtn.Name = "addServerOkBtn";
            this.addServerOkBtn.Size = new System.Drawing.Size(75, 23);
            this.addServerOkBtn.TabIndex = 1;
            this.addServerOkBtn.Text = "OK";
            this.addServerOkBtn.UseVisualStyleBackColor = true;
            this.addServerOkBtn.Click += this.AddServerOkBtnClick;
            // 
            // addServerValidIpGroupBox
            // 
            this.addServerValidIpGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpMonitorName);
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpMonitorNameLabel);
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpIpLabel);
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpLabel);
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpPingBtn);
            this.addServerValidIpGroupBox.Controls.Add(this.addServerValidIpIpTextBox);
            this.addServerValidIpGroupBox.Location = new System.Drawing.Point(5, 11);
            this.addServerValidIpGroupBox.Name = "addServerValidIpGroupBox";
            this.addServerValidIpGroupBox.Size = new System.Drawing.Size(581, 47);
            this.addServerValidIpGroupBox.TabIndex = 0;
            this.addServerValidIpGroupBox.TabStop = false;
            this.addServerValidIpGroupBox.Text = "Ip/Hostname and Monitor Name";
            // 
            // addServerValidIpMonitorName
            // 
            this.addServerValidIpMonitorName.Location = new System.Drawing.Point(262, 19);
            this.addServerValidIpMonitorName.MaxLength = 50;
            this.addServerValidIpMonitorName.Name = "addServerValidIpMonitorName";
            this.addServerValidIpMonitorName.Size = new System.Drawing.Size(100, 20);
            this.addServerValidIpMonitorName.TabIndex = 1;
            this.addServerValidIpMonitorName.Leave += this.AddServerMonitorNameLeaveFocus;
            // 
            // addServerValidIpMonitorNameLabel
            // 
            this.addServerValidIpMonitorNameLabel.AutoSize = true;
            this.addServerValidIpMonitorNameLabel.CausesValidation = false;
            this.addServerValidIpMonitorNameLabel.Location = new System.Drawing.Point(185, 22);
            this.addServerValidIpMonitorNameLabel.Name = "addServerValidIpMonitorNameLabel";
            this.addServerValidIpMonitorNameLabel.Size = new System.Drawing.Size(76, 13);
            this.addServerValidIpMonitorNameLabel.TabIndex = 4;
            this.addServerValidIpMonitorNameLabel.Text = "Monitor Name:";
            // 
            // addServerValidIpIpLabel
            // 
            this.addServerValidIpIpLabel.AutoSize = true;
            this.addServerValidIpIpLabel.CausesValidation = false;
            this.addServerValidIpIpLabel.Location = new System.Drawing.Point(6, 22);
            this.addServerValidIpIpLabel.Name = "addServerValidIpIpLabel";
            this.addServerValidIpIpLabel.Size = new System.Drawing.Size(72, 13);
            this.addServerValidIpIpLabel.TabIndex = 3;
            this.addServerValidIpIpLabel.Text = "Ip/Hostname:";
            // 
            // addServerValidIpLabel
            // 
            this.addServerValidIpLabel.AutoEllipsis = true;
            this.addServerValidIpLabel.CausesValidation = false;
            this.addServerValidIpLabel.Location = new System.Drawing.Point(453, 22);
            this.addServerValidIpLabel.Name = "addServerValidIpLabel";
            this.addServerValidIpLabel.Size = new System.Drawing.Size(69, 13);
            this.addServerValidIpLabel.TabIndex = 2;
            this.addServerValidIpLabel.Text = "...";
            // 
            // addServerValidIpPingBtn
            // 
            this.addServerValidIpPingBtn.CausesValidation = false;
            this.addServerValidIpPingBtn.Location = new System.Drawing.Point(371, 17);
            this.addServerValidIpPingBtn.Name = "addServerValidIpPingBtn";
            this.addServerValidIpPingBtn.Size = new System.Drawing.Size(75, 23);
            this.addServerValidIpPingBtn.TabIndex = 2;
            this.addServerValidIpPingBtn.Text = "Ping";
            this.addServerValidIpPingBtn.UseVisualStyleBackColor = true;
            this.addServerValidIpPingBtn.Click += this.AddServerValidIpPingBtnClick;
            // 
            // addServerValidIpIpTextBox
            // 
            this.addServerValidIpIpTextBox.Location = new System.Drawing.Point(78, 19);
            this.addServerValidIpIpTextBox.MaxLength = 15;
            this.addServerValidIpIpTextBox.Name = "addServerValidIpIpTextBox";
            this.addServerValidIpIpTextBox.Size = new System.Drawing.Size(100, 20);
            this.addServerValidIpIpTextBox.TabIndex = 0;
            this.addServerValidIpIpTextBox.Leave += new System.EventHandler(this.AddServerIpLeaveFocus);
            // 
            // testMonitorUpdate
            // 
            this.testMonitorUpdate.Enabled = true;
            this.testMonitorUpdate.Interval = 1000;
            this.testMonitorUpdate.Tick += this.TestMonitorUpdateTick;
            // 
            // addServerAlertConfiguration
            // 
            this.addServerAlertConfiguration.Controls.Add(this.commonFourLabel);
            this.addServerAlertConfiguration.Controls.Add(this.addServerAlertEmailTextBox);
            this.addServerAlertConfiguration.Controls.Add(this.addServerAlertSmsTextBox);
            this.addServerAlertConfiguration.Controls.Add(this.addServerAlertEmailOption);
            this.addServerAlertConfiguration.Controls.Add(this.addServerAlertSmsOption);
            this.addServerAlertConfiguration.Location = new System.Drawing.Point(3, 547);
            this.addServerAlertConfiguration.Name = "addServerAlertConfiguration";
            this.addServerAlertConfiguration.Size = new System.Drawing.Size(427, 46);
            this.addServerAlertConfiguration.TabIndex = 0;
            this.addServerAlertConfiguration.TabStop = false;
            this.addServerAlertConfiguration.Text = "Alert Configuration";
            // 
            // commonFourLabel
            // 
            this.commonFourLabel.AutoSize = true;
            this.commonFourLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commonFourLabel.ForeColor = System.Drawing.Color.Red;
            this.commonFourLabel.Location = new System.Drawing.Point(142, -3);
            this.commonFourLabel.Name = "commonFourLabel";
            this.commonFourLabel.Size = new System.Drawing.Size(17, 17);
            this.commonFourLabel.TabIndex = 5;
            this.commonFourLabel.Text = "4";
            this.addServerToolTip.SetToolTip(this.commonFourLabel, "Step 4: confirm the alert configuration - confirm existing Email address or enter" +
                    " a new one.");
            this.commonFourLabel.Visible = false;
            // 
            // addServerAlertEmailTextBox
            // 
            this.addServerAlertEmailTextBox.Location = new System.Drawing.Point(62, 19);
            this.addServerAlertEmailTextBox.MaxLength = 100;
            this.addServerAlertEmailTextBox.Name = "addServerAlertEmailTextBox";
            this.addServerAlertEmailTextBox.Size = new System.Drawing.Size(141, 20);
            this.addServerAlertEmailTextBox.TabIndex = 4;
            this.addServerAlertEmailTextBox.TextChanged += this.AddServerAlertEmailTextBoxTextChanged;
            // 
            // addServerAlertSmsTextBox
            // 
            this.addServerAlertSmsTextBox.Location = new System.Drawing.Point(281, 19);
            this.addServerAlertSmsTextBox.Name = "addServerAlertSmsTextBox";
            this.addServerAlertSmsTextBox.Size = new System.Drawing.Size(141, 20);
            this.addServerAlertSmsTextBox.TabIndex = 2;
            this.addServerAlertSmsTextBox.Visible = false;
            this.addServerAlertSmsTextBox.TextChanged += this.AddServerAlertSmsTextBoxTextChanged;
            // 
            // addServerAlertEmailOption
            // 
            this.addServerAlertEmailOption.AutoSize = true;
            this.addServerAlertEmailOption.Location = new System.Drawing.Point(4, 20);
            this.addServerAlertEmailOption.Name = "addServerAlertEmailOption";
            this.addServerAlertEmailOption.Size = new System.Drawing.Size(54, 17);
            this.addServerAlertEmailOption.TabIndex = 3;
            this.addServerAlertEmailOption.Text = "Email:";
            this.addServerAlertEmailOption.UseVisualStyleBackColor = true;
            this.addServerAlertEmailOption.CheckedChanged += this.AddServerAlertEmailOptionCheckedChanged;
            // 
            // addServerAlertSmsOption
            // 
            this.addServerAlertSmsOption.AutoSize = true;
            this.addServerAlertSmsOption.Location = new System.Drawing.Point(223, 21);
            this.addServerAlertSmsOption.Name = "addServerAlertSmsOption";
            this.addServerAlertSmsOption.Size = new System.Drawing.Size(52, 17);
            this.addServerAlertSmsOption.TabIndex = 1;
            this.addServerAlertSmsOption.Text = "SMS:";
            this.addServerAlertSmsOption.UseVisualStyleBackColor = true;
            this.addServerAlertSmsOption.Visible = false;
            this.addServerAlertSmsOption.CheckedChanged += this.AddServerAlertSmsOptionCheckedChanged;
            // 
            // basicMonitorTestDataUpdateFreqTextBox
            // 
            this.basicMonitorTestDataUpdateFreqTextBox.Location = new System.Drawing.Point(135, 470);
            this.basicMonitorTestDataUpdateFreqTextBox.Name = "basicMonitorTestDataUpdateFreqTextBox";
            this.basicMonitorTestDataUpdateFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.basicMonitorTestDataUpdateFreqTextBox.TabIndex = 27;
            this.basicMonitorTestDataUpdateFreqTextBox.Text = "300000";
            this.addServerToolTip.SetToolTip(this.basicMonitorTestDataUpdateFreqTextBox, "How long between checks, in milliseconds. (default: 300000, 5 minutes)");
            // 
            // commonMonitorTestDataUpdateFreqTextBox
            // 
            this.commonMonitorTestDataUpdateFreqTextBox.Location = new System.Drawing.Point(130, 25);
            this.commonMonitorTestDataUpdateFreqTextBox.Name = "commonMonitorTestDataUpdateFreqTextBox";
            this.commonMonitorTestDataUpdateFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.commonMonitorTestDataUpdateFreqTextBox.TabIndex = 29;
            this.commonMonitorTestDataUpdateFreqTextBox.Text = "30000";
            this.addServerToolTip.SetToolTip(this.commonMonitorTestDataUpdateFreqTextBox, "How long between checks, in milliseconds. (default: 30000, 30 seconds)");
            // 
            // commonBaseTab
            // 
            this.commonBaseTab.Controls.Add(this.commonGeneralSetupGroupBox);
            this.commonBaseTab.Controls.Add(this.commonChooserGroupBox);
            this.commonBaseTab.Controls.Add(this.addServerValidIpGroupBox2);
            this.commonBaseTab.Location = new System.Drawing.Point(4, 22);
            this.commonBaseTab.Name = "commonBaseTab";
            this.commonBaseTab.Size = new System.Drawing.Size(592, 513);
            this.commonBaseTab.TabIndex = 2;
            this.commonBaseTab.Text = "Common";
            this.commonBaseTab.UseVisualStyleBackColor = true;
            // 
            // commonGeneralSetupGroupBox
            // 
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonThreeLabel);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorTestDataUpdateFreqLabel);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorTestDataUpdateFreqTextBox);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorTestDataUpdateResultStatus);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorTestDataUpdateResult);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorCheckBtn);
            this.commonGeneralSetupGroupBox.Controls.Add(this.commonMonitorTestDataUpdateResultLabel);
            this.commonGeneralSetupGroupBox.Location = new System.Drawing.Point(5, 435);
            this.commonGeneralSetupGroupBox.Name = "commonGeneralSetupGroupBox";
            this.commonGeneralSetupGroupBox.Size = new System.Drawing.Size(581, 58);
            this.commonGeneralSetupGroupBox.TabIndex = 35;
            this.commonGeneralSetupGroupBox.TabStop = false;
            this.commonGeneralSetupGroupBox.Text = "Frequency and Checks";
            // 
            // commonThreeLabel
            // 
            this.commonThreeLabel.AutoSize = true;
            this.commonThreeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commonThreeLabel.ForeColor = System.Drawing.Color.Red;
            this.commonThreeLabel.Location = new System.Drawing.Point(276, -3);
            this.commonThreeLabel.Name = "commonThreeLabel";
            this.commonThreeLabel.Size = new System.Drawing.Size(17, 17);
            this.commonThreeLabel.TabIndex = 5;
            this.commonThreeLabel.Text = "3";
            this.addServerToolTip.SetToolTip(this.commonThreeLabel, "Step 3: Enter an Update Frequency and (optionally) check the current status of th" +
                    "e monitor, to make sure the results are expected.");
            // 
            // commonMonitorTestDataUpdateFreqLabel
            // 
            this.commonMonitorTestDataUpdateFreqLabel.AutoSize = true;
            this.commonMonitorTestDataUpdateFreqLabel.Location = new System.Drawing.Point(6, 28);
            this.commonMonitorTestDataUpdateFreqLabel.Name = "commonMonitorTestDataUpdateFreqLabel";
            this.commonMonitorTestDataUpdateFreqLabel.Size = new System.Drawing.Size(120, 13);
            this.commonMonitorTestDataUpdateFreqLabel.TabIndex = 30;
            this.commonMonitorTestDataUpdateFreqLabel.Text = "Update Frequency (ms):";
            // 
            // commonMonitorTestDataUpdateResultStatus
            // 
            this.commonMonitorTestDataUpdateResultStatus.AutoSize = true;
            this.commonMonitorTestDataUpdateResultStatus.Location = new System.Drawing.Point(485, 28);
            this.commonMonitorTestDataUpdateResultStatus.Name = "commonMonitorTestDataUpdateResultStatus";
            this.commonMonitorTestDataUpdateResultStatus.Size = new System.Drawing.Size(0, 13);
            this.commonMonitorTestDataUpdateResultStatus.TabIndex = 14;
            // 
            // commonMonitorTestDataUpdateResult
            // 
            this.commonMonitorTestDataUpdateResult.AutoSize = true;
            this.commonMonitorTestDataUpdateResult.Location = new System.Drawing.Point(399, 28);
            this.commonMonitorTestDataUpdateResult.Name = "commonMonitorTestDataUpdateResult";
            this.commonMonitorTestDataUpdateResult.Size = new System.Drawing.Size(16, 13);
            this.commonMonitorTestDataUpdateResult.TabIndex = 14;
            this.commonMonitorTestDataUpdateResult.Text = "...";
            // 
            // commonMonitorCheckBtn
            // 
            this.commonMonitorCheckBtn.Location = new System.Drawing.Point(272, 23);
            this.commonMonitorCheckBtn.Name = "commonMonitorCheckBtn";
            this.commonMonitorCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.commonMonitorCheckBtn.TabIndex = 12;
            this.commonMonitorCheckBtn.Text = "Check";
            this.addServerToolTip.SetToolTip(this.commonMonitorCheckBtn, "Click to check the current status with the provided information.");
            this.commonMonitorCheckBtn.UseVisualStyleBackColor = true;
            this.commonMonitorCheckBtn.Click += this.CommonMonitorCheckBtnClick;
            // 
            // commonMonitorTestDataUpdateResultLabel
            // 
            this.commonMonitorTestDataUpdateResultLabel.AutoSize = true;
            this.commonMonitorTestDataUpdateResultLabel.Location = new System.Drawing.Point(353, 28);
            this.commonMonitorTestDataUpdateResultLabel.Name = "commonMonitorTestDataUpdateResultLabel";
            this.commonMonitorTestDataUpdateResultLabel.Size = new System.Drawing.Size(40, 13);
            this.commonMonitorTestDataUpdateResultLabel.TabIndex = 13;
            this.commonMonitorTestDataUpdateResultLabel.Text = "Result:";
            // 
            // commonChooserGroupBox
            // 
            this.commonChooserGroupBox.Controls.Add(this.commonTwoLabel);
            this.commonChooserGroupBox.Controls.Add(this.commonHddGroupBox);
            this.commonChooserGroupBox.Controls.Add(this.commonMemoryGroupBox);
            this.commonChooserGroupBox.Controls.Add(this.commonServiceGroupBox);
            this.commonChooserGroupBox.Controls.Add(this.commonCpuGroupBox);
            this.commonChooserGroupBox.Controls.Add(this.commonSwapFileGroupBox);
            this.commonChooserGroupBox.Controls.Add(this.commonProcessGroupBox);
            this.commonChooserGroupBox.Location = new System.Drawing.Point(5, 56);
            this.commonChooserGroupBox.Name = "commonChooserGroupBox";
            this.commonChooserGroupBox.Size = new System.Drawing.Size(581, 373);
            this.commonChooserGroupBox.TabIndex = 34;
            this.commonChooserGroupBox.TabStop = false;
            this.commonChooserGroupBox.Text = "Common Monitors";
            // 
            // commonTwoLabel
            // 
            this.commonTwoLabel.AutoSize = true;
            this.commonTwoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commonTwoLabel.ForeColor = System.Drawing.Color.Red;
            this.commonTwoLabel.Location = new System.Drawing.Point(276, -3);
            this.commonTwoLabel.Name = "commonTwoLabel";
            this.commonTwoLabel.Size = new System.Drawing.Size(17, 17);
            this.commonTwoLabel.TabIndex = 5;
            this.commonTwoLabel.Text = "2";
            this.addServerToolTip.SetToolTip(this.commonTwoLabel, "Step 2: select a monitor and enter the required information");
            // 
            // commonHddGroupBox
            // 
            this.commonHddGroupBox.Controls.Add(this.commonHddDriveLetterDdl);
            this.commonHddGroupBox.Controls.Add(this.commonHddSelected);
            this.commonHddGroupBox.Controls.Add(this.commonHddGtLtLabel);
            this.commonHddGroupBox.Controls.Add(this.commonHddCriticalTextBox);
            this.commonHddGroupBox.Controls.Add(this.commonHddWarningTextBox);
            this.commonHddGroupBox.Controls.Add(this.commonHddDriveLetterLabel);
            this.commonHddGroupBox.Controls.Add(this.commonHddCriticalPctLabel);
            this.commonHddGroupBox.Controls.Add(this.commonHddWarningPctLabel);
            this.commonHddGroupBox.Controls.Add(this.commonHddCriticalLabel);
            this.commonHddGroupBox.Controls.Add(this.commonHddWarningLabel);
            this.commonHddGroupBox.Location = new System.Drawing.Point(6, 19);
            this.commonHddGroupBox.Name = "commonHddGroupBox";
            this.commonHddGroupBox.Size = new System.Drawing.Size(569, 45);
            this.commonHddGroupBox.TabIndex = 33;
            this.commonHddGroupBox.TabStop = false;
            this.commonHddGroupBox.Text = "Hard Drive Free Space";
            this.addServerToolTip.SetToolTip(this.commonHddGroupBox, "Hdd usage monitor - warnings and alerts at the set frequency and percentage.");
            this.commonHddGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonHddDriveLetterDdl
            // 
            this.commonHddDriveLetterDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonHddDriveLetterDdl.FormattingEnabled = true;
            this.commonHddDriveLetterDdl.Location = new System.Drawing.Point(112, 16);
            this.commonHddDriveLetterDdl.Name = "commonHddDriveLetterDdl";
            this.commonHddDriveLetterDdl.Size = new System.Drawing.Size(175, 21);
            this.commonHddDriveLetterDdl.Sorted = true;
            this.commonHddDriveLetterDdl.TabIndex = 30;
            this.addServerToolTip.SetToolTip(this.commonHddDriveLetterDdl, "Monitor the % Free Space of the selected drive");
            // 
            // commonHddSelected
            // 
            this.commonHddSelected.AutoSize = true;
            this.commonHddSelected.Location = new System.Drawing.Point(6, 19);
            this.commonHddSelected.Name = "commonHddSelected";
            this.commonHddSelected.Size = new System.Drawing.Size(15, 14);
            this.commonHddSelected.TabIndex = 29;
            this.commonHddSelected.Tag = RemoteMon_Lib.CommonMonitorType.HddUsage;
            this.commonHddSelected.UseVisualStyleBackColor = true;
            this.commonHddSelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonHddGtLtLabel
            // 
            this.commonHddGtLtLabel.AutoSize = true;
            this.commonHddGtLtLabel.Location = new System.Drawing.Point(316, 19);
            this.commonHddGtLtLabel.Name = "commonHddGtLtLabel";
            this.commonHddGtLtLabel.Size = new System.Drawing.Size(13, 13);
            this.commonHddGtLtLabel.TabIndex = 1;
            this.commonHddGtLtLabel.Text = "<";
            // 
            // commonHddCriticalTextBox
            // 
            this.commonHddCriticalTextBox.Location = new System.Drawing.Point(504, 16);
            this.commonHddCriticalTextBox.MaxLength = 150;
            this.commonHddCriticalTextBox.Name = "commonHddCriticalTextBox";
            this.commonHddCriticalTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonHddCriticalTextBox.TabIndex = 2;
            this.commonHddCriticalTextBox.Text = "10";
            this.addServerToolTip.SetToolTip(this.commonHddCriticalTextBox, "Send critical alert when Hdd usage hits this percentage (default: 90)");
            // 
            // commonHddWarningTextBox
            // 
            this.commonHddWarningTextBox.Location = new System.Drawing.Point(401, 16);
            this.commonHddWarningTextBox.MaxLength = 150;
            this.commonHddWarningTextBox.Name = "commonHddWarningTextBox";
            this.commonHddWarningTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonHddWarningTextBox.TabIndex = 2;
            this.commonHddWarningTextBox.Text = "30";
            this.addServerToolTip.SetToolTip(this.commonHddWarningTextBox, "Send warning when Hdd usage hits this percentage (default: 70)");
            // 
            // commonHddDriveLetterLabel
            // 
            this.commonHddDriveLetterLabel.AutoSize = true;
            this.commonHddDriveLetterLabel.Location = new System.Drawing.Point(71, 19);
            this.commonHddDriveLetterLabel.Name = "commonHddDriveLetterLabel";
            this.commonHddDriveLetterLabel.Size = new System.Drawing.Size(35, 13);
            this.commonHddDriveLetterLabel.TabIndex = 10;
            this.commonHddDriveLetterLabel.Text = "Drive:";
            // 
            // commonHddCriticalPctLabel
            // 
            this.commonHddCriticalPctLabel.AutoSize = true;
            this.commonHddCriticalPctLabel.Location = new System.Drawing.Point(527, 19);
            this.commonHddCriticalPctLabel.Name = "commonHddCriticalPctLabel";
            this.commonHddCriticalPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonHddCriticalPctLabel.TabIndex = 1;
            this.commonHddCriticalPctLabel.Text = "%";
            // 
            // commonHddWarningPctLabel
            // 
            this.commonHddWarningPctLabel.AutoSize = true;
            this.commonHddWarningPctLabel.Location = new System.Drawing.Point(424, 19);
            this.commonHddWarningPctLabel.Name = "commonHddWarningPctLabel";
            this.commonHddWarningPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonHddWarningPctLabel.TabIndex = 1;
            this.commonHddWarningPctLabel.Text = "%";
            // 
            // commonHddCriticalLabel
            // 
            this.commonHddCriticalLabel.AutoSize = true;
            this.commonHddCriticalLabel.Location = new System.Drawing.Point(460, 19);
            this.commonHddCriticalLabel.Name = "commonHddCriticalLabel";
            this.commonHddCriticalLabel.Size = new System.Drawing.Size(41, 13);
            this.commonHddCriticalLabel.TabIndex = 1;
            this.commonHddCriticalLabel.Text = "Critical:";
            // 
            // commonHddWarningLabel
            // 
            this.commonHddWarningLabel.AutoSize = true;
            this.commonHddWarningLabel.Location = new System.Drawing.Point(349, 19);
            this.commonHddWarningLabel.Name = "commonHddWarningLabel";
            this.commonHddWarningLabel.Size = new System.Drawing.Size(50, 13);
            this.commonHddWarningLabel.TabIndex = 1;
            this.commonHddWarningLabel.Text = "Warning:";
            // 
            // commonMemoryGroupBox
            // 
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryUsageTypeDdl);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemorySelected);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryGtLtLabel);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryWarningLabel);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryCriticalTextBox);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryCriticalLabel);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryUsageTypeLabel);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryWarningTextBox);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryWarningPctLabel);
            this.commonMemoryGroupBox.Controls.Add(this.commonMemoryCriticalPctLabel);
            this.commonMemoryGroupBox.Location = new System.Drawing.Point(6, 70);
            this.commonMemoryGroupBox.Name = "commonMemoryGroupBox";
            this.commonMemoryGroupBox.Size = new System.Drawing.Size(569, 45);
            this.commonMemoryGroupBox.TabIndex = 31;
            this.commonMemoryGroupBox.TabStop = false;
            this.commonMemoryGroupBox.Text = "Memory Usage";
            this.addServerToolTip.SetToolTip(this.commonMemoryGroupBox, "Memory usage monitor - warnings and alerts at the set frequency and percentage.");
            this.commonMemoryGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonMemoryUsageTypeDdl
            // 
            this.commonMemoryUsageTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonMemoryUsageTypeDdl.FormattingEnabled = true;
            this.commonMemoryUsageTypeDdl.Items.AddRange(new object[] {
            "% In Use",
            "Available"});
            this.commonMemoryUsageTypeDdl.Location = new System.Drawing.Point(112, 15);
            this.commonMemoryUsageTypeDdl.Name = "commonMemoryUsageTypeDdl";
            this.commonMemoryUsageTypeDdl.Size = new System.Drawing.Size(175, 21);
            this.commonMemoryUsageTypeDdl.Sorted = true;
            this.commonMemoryUsageTypeDdl.TabIndex = 30;
            this.commonMemoryUsageTypeDdl.SelectedIndexChanged += this.CommonMemoryUsageTypeDdlSelectedIndexChanged;
            // 
            // commonMemorySelected
            // 
            this.commonMemorySelected.AutoSize = true;
            this.commonMemorySelected.Location = new System.Drawing.Point(6, 18);
            this.commonMemorySelected.Name = "commonMemorySelected";
            this.commonMemorySelected.Size = new System.Drawing.Size(15, 14);
            this.commonMemorySelected.TabIndex = 29;
            this.commonMemorySelected.Tag = RemoteMon_Lib.CommonMonitorType.MemoryUsage;
            this.commonMemorySelected.UseVisualStyleBackColor = true;
            this.commonMemorySelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonMemoryGtLtLabel
            // 
            this.commonMemoryGtLtLabel.AutoSize = true;
            this.commonMemoryGtLtLabel.Location = new System.Drawing.Point(316, 19);
            this.commonMemoryGtLtLabel.Name = "commonMemoryGtLtLabel";
            this.commonMemoryGtLtLabel.Size = new System.Drawing.Size(13, 13);
            this.commonMemoryGtLtLabel.TabIndex = 1;
            this.commonMemoryGtLtLabel.Text = ">";
            // 
            // commonMemoryWarningLabel
            // 
            this.commonMemoryWarningLabel.AutoSize = true;
            this.commonMemoryWarningLabel.Location = new System.Drawing.Point(349, 18);
            this.commonMemoryWarningLabel.Name = "commonMemoryWarningLabel";
            this.commonMemoryWarningLabel.Size = new System.Drawing.Size(50, 13);
            this.commonMemoryWarningLabel.TabIndex = 1;
            this.commonMemoryWarningLabel.Text = "Warning:";
            // 
            // commonMemoryCriticalTextBox
            // 
            this.commonMemoryCriticalTextBox.Location = new System.Drawing.Point(504, 15);
            this.commonMemoryCriticalTextBox.MaxLength = 150;
            this.commonMemoryCriticalTextBox.Name = "commonMemoryCriticalTextBox";
            this.commonMemoryCriticalTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonMemoryCriticalTextBox.TabIndex = 2;
            this.commonMemoryCriticalTextBox.Tag = 25;
            this.commonMemoryCriticalTextBox.Text = "90";
            // 
            // commonMemoryCriticalLabel
            // 
            this.commonMemoryCriticalLabel.AutoSize = true;
            this.commonMemoryCriticalLabel.Location = new System.Drawing.Point(460, 18);
            this.commonMemoryCriticalLabel.Name = "commonMemoryCriticalLabel";
            this.commonMemoryCriticalLabel.Size = new System.Drawing.Size(41, 13);
            this.commonMemoryCriticalLabel.TabIndex = 1;
            this.commonMemoryCriticalLabel.Text = "Critical:";
            // 
            // commonMemoryUsageTypeLabel
            // 
            this.commonMemoryUsageTypeLabel.AutoSize = true;
            this.commonMemoryUsageTypeLabel.Location = new System.Drawing.Point(65, 18);
            this.commonMemoryUsageTypeLabel.Name = "commonMemoryUsageTypeLabel";
            this.commonMemoryUsageTypeLabel.Size = new System.Drawing.Size(41, 13);
            this.commonMemoryUsageTypeLabel.TabIndex = 10;
            this.commonMemoryUsageTypeLabel.Text = "Usage:";
            // 
            // commonMemoryWarningTextBox
            // 
            this.commonMemoryWarningTextBox.Location = new System.Drawing.Point(401, 15);
            this.commonMemoryWarningTextBox.MaxLength = 150;
            this.commonMemoryWarningTextBox.Name = "commonMemoryWarningTextBox";
            this.commonMemoryWarningTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonMemoryWarningTextBox.TabIndex = 2;
            this.commonMemoryWarningTextBox.Tag = 25;
            this.commonMemoryWarningTextBox.Text = "70";
            // 
            // commonMemoryWarningPctLabel
            // 
            this.commonMemoryWarningPctLabel.AutoSize = true;
            this.commonMemoryWarningPctLabel.Location = new System.Drawing.Point(424, 18);
            this.commonMemoryWarningPctLabel.Name = "commonMemoryWarningPctLabel";
            this.commonMemoryWarningPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonMemoryWarningPctLabel.TabIndex = 1;
            this.commonMemoryWarningPctLabel.Text = "%";
            // 
            // commonMemoryCriticalPctLabel
            // 
            this.commonMemoryCriticalPctLabel.AutoSize = true;
            this.commonMemoryCriticalPctLabel.Location = new System.Drawing.Point(527, 18);
            this.commonMemoryCriticalPctLabel.Name = "commonMemoryCriticalPctLabel";
            this.commonMemoryCriticalPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonMemoryCriticalPctLabel.TabIndex = 1;
            this.commonMemoryCriticalPctLabel.Text = "%";
            // 
            // commonServiceGroupBox
            // 
            this.commonServiceGroupBox.Controls.Add(this.commonServiceServicesChoiceDdl);
            this.commonServiceGroupBox.Controls.Add(this.commonServiceSelected);
            this.commonServiceGroupBox.Controls.Add(this.commonServiceServicesChoiceLabel);
            this.commonServiceGroupBox.Location = new System.Drawing.Point(6, 311);
            this.commonServiceGroupBox.Name = "commonServiceGroupBox";
            this.commonServiceGroupBox.Size = new System.Drawing.Size(569, 45);
            this.commonServiceGroupBox.TabIndex = 31;
            this.commonServiceGroupBox.TabStop = false;
            this.commonServiceGroupBox.Text = "Service State";
            this.commonServiceGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonServiceServicesChoiceDdl
            // 
            this.commonServiceServicesChoiceDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonServiceServicesChoiceDdl.DropDownWidth = 250;
            this.commonServiceServicesChoiceDdl.FormattingEnabled = true;
            this.commonServiceServicesChoiceDdl.Location = new System.Drawing.Point(112, 15);
            this.commonServiceServicesChoiceDdl.Name = "commonServiceServicesChoiceDdl";
            this.commonServiceServicesChoiceDdl.Size = new System.Drawing.Size(175, 21);
            this.commonServiceServicesChoiceDdl.Sorted = true;
            this.commonServiceServicesChoiceDdl.TabIndex = 30;
            // 
            // commonServiceSelected
            // 
            this.commonServiceSelected.AutoSize = true;
            this.commonServiceSelected.Location = new System.Drawing.Point(6, 18);
            this.commonServiceSelected.Name = "commonServiceSelected";
            this.commonServiceSelected.Size = new System.Drawing.Size(15, 14);
            this.commonServiceSelected.TabIndex = 29;
            this.commonServiceSelected.Tag = RemoteMon_Lib.CommonMonitorType.ServiceState;
            this.commonServiceSelected.UseVisualStyleBackColor = true;
            this.commonServiceSelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonServiceServicesChoiceLabel
            // 
            this.commonServiceServicesChoiceLabel.AutoSize = true;
            this.commonServiceServicesChoiceLabel.Location = new System.Drawing.Point(55, 18);
            this.commonServiceServicesChoiceLabel.Name = "commonServiceServicesChoiceLabel";
            this.commonServiceServicesChoiceLabel.Size = new System.Drawing.Size(51, 13);
            this.commonServiceServicesChoiceLabel.TabIndex = 10;
            this.commonServiceServicesChoiceLabel.Text = "Services:";
            // 
            // commonCpuGroupBox
            // 
            this.commonCpuGroupBox.Controls.Add(this.commonCpuUsageTypeDdl);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuSelected);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuGtLtLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuWarningLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuCriticalTextBox);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuCriticalPctLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuCriticalLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuUsageTypeLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuWarningPctLabel);
            this.commonCpuGroupBox.Controls.Add(this.commonCpuWarningTextBox);
            this.commonCpuGroupBox.Location = new System.Drawing.Point(6, 121);
            this.commonCpuGroupBox.Name = "commonCpuGroupBox";
            this.commonCpuGroupBox.Size = new System.Drawing.Size(569, 45);
            this.commonCpuGroupBox.TabIndex = 31;
            this.commonCpuGroupBox.TabStop = false;
            this.commonCpuGroupBox.Text = "CPU Usage";
            this.commonCpuGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonCpuUsageTypeDdl
            // 
            this.commonCpuUsageTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonCpuUsageTypeDdl.FormattingEnabled = true;
            this.commonCpuUsageTypeDdl.Items.AddRange(new object[] {
            "% Idle Time",
            "% User Time"});
            this.commonCpuUsageTypeDdl.Location = new System.Drawing.Point(112, 15);
            this.commonCpuUsageTypeDdl.Name = "commonCpuUsageTypeDdl";
            this.commonCpuUsageTypeDdl.Size = new System.Drawing.Size(175, 21);
            this.commonCpuUsageTypeDdl.Sorted = true;
            this.commonCpuUsageTypeDdl.TabIndex = 30;
            this.commonCpuUsageTypeDdl.SelectedIndexChanged += this.CommonCpuUsageTypeDdlSelectedIndexChanged;
            // 
            // commonCpuSelected
            // 
            this.commonCpuSelected.AutoSize = true;
            this.commonCpuSelected.Location = new System.Drawing.Point(6, 18);
            this.commonCpuSelected.Name = "commonCpuSelected";
            this.commonCpuSelected.Size = new System.Drawing.Size(15, 14);
            this.commonCpuSelected.TabIndex = 29;
            this.commonCpuSelected.Tag = RemoteMon_Lib.CommonMonitorType.CpuUsage;
            this.commonCpuSelected.UseVisualStyleBackColor = true;
            this.commonCpuSelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonCpuGtLtLabel
            // 
            this.commonCpuGtLtLabel.AutoSize = true;
            this.commonCpuGtLtLabel.Location = new System.Drawing.Point(316, 20);
            this.commonCpuGtLtLabel.Name = "commonCpuGtLtLabel";
            this.commonCpuGtLtLabel.Size = new System.Drawing.Size(13, 13);
            this.commonCpuGtLtLabel.TabIndex = 1;
            this.commonCpuGtLtLabel.Text = ">";
            // 
            // commonCpuWarningLabel
            // 
            this.commonCpuWarningLabel.AutoSize = true;
            this.commonCpuWarningLabel.Location = new System.Drawing.Point(349, 18);
            this.commonCpuWarningLabel.Name = "commonCpuWarningLabel";
            this.commonCpuWarningLabel.Size = new System.Drawing.Size(50, 13);
            this.commonCpuWarningLabel.TabIndex = 1;
            this.commonCpuWarningLabel.Text = "Warning:";
            // 
            // commonCpuCriticalTextBox
            // 
            this.commonCpuCriticalTextBox.Location = new System.Drawing.Point(504, 15);
            this.commonCpuCriticalTextBox.MaxLength = 150;
            this.commonCpuCriticalTextBox.Name = "commonCpuCriticalTextBox";
            this.commonCpuCriticalTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonCpuCriticalTextBox.TabIndex = 2;
            this.commonCpuCriticalTextBox.Tag = 25;
            this.commonCpuCriticalTextBox.Text = "90";
            // 
            // commonCpuCriticalPctLabel
            // 
            this.commonCpuCriticalPctLabel.AutoSize = true;
            this.commonCpuCriticalPctLabel.Location = new System.Drawing.Point(527, 18);
            this.commonCpuCriticalPctLabel.Name = "commonCpuCriticalPctLabel";
            this.commonCpuCriticalPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonCpuCriticalPctLabel.TabIndex = 1;
            this.commonCpuCriticalPctLabel.Text = "%";
            // 
            // commonCpuCriticalLabel
            // 
            this.commonCpuCriticalLabel.AutoSize = true;
            this.commonCpuCriticalLabel.Location = new System.Drawing.Point(460, 18);
            this.commonCpuCriticalLabel.Name = "commonCpuCriticalLabel";
            this.commonCpuCriticalLabel.Size = new System.Drawing.Size(41, 13);
            this.commonCpuCriticalLabel.TabIndex = 1;
            this.commonCpuCriticalLabel.Text = "Critical:";
            // 
            // commonCpuUsageTypeLabel
            // 
            this.commonCpuUsageTypeLabel.AutoSize = true;
            this.commonCpuUsageTypeLabel.Location = new System.Drawing.Point(65, 18);
            this.commonCpuUsageTypeLabel.Name = "commonCpuUsageTypeLabel";
            this.commonCpuUsageTypeLabel.Size = new System.Drawing.Size(41, 13);
            this.commonCpuUsageTypeLabel.TabIndex = 10;
            this.commonCpuUsageTypeLabel.Text = "Usage:";
            // 
            // commonCpuWarningPctLabel
            // 
            this.commonCpuWarningPctLabel.AutoSize = true;
            this.commonCpuWarningPctLabel.Location = new System.Drawing.Point(424, 18);
            this.commonCpuWarningPctLabel.Name = "commonCpuWarningPctLabel";
            this.commonCpuWarningPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonCpuWarningPctLabel.TabIndex = 1;
            this.commonCpuWarningPctLabel.Text = "%";
            // 
            // commonCpuWarningTextBox
            // 
            this.commonCpuWarningTextBox.Location = new System.Drawing.Point(401, 15);
            this.commonCpuWarningTextBox.MaxLength = 150;
            this.commonCpuWarningTextBox.Name = "commonCpuWarningTextBox";
            this.commonCpuWarningTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonCpuWarningTextBox.TabIndex = 2;
            this.commonCpuWarningTextBox.Tag = 25;
            this.commonCpuWarningTextBox.Text = "70";
            // 
            // commonSwapFileGroupBox
            // 
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileUsageTypeDdl);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileSelected);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileGtLtLabel);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileWarningLabel);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileCriticalTextBox);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileUsageTypeLabel);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileWarningPctLabel);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileWarningTextBox);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileCriticalLabel);
            this.commonSwapFileGroupBox.Controls.Add(this.commonSwapFileCriticalPctLabel);
            this.commonSwapFileGroupBox.Location = new System.Drawing.Point(6, 172);
            this.commonSwapFileGroupBox.Name = "commonSwapFileGroupBox";
            this.commonSwapFileGroupBox.Size = new System.Drawing.Size(569, 45);
            this.commonSwapFileGroupBox.TabIndex = 31;
            this.commonSwapFileGroupBox.TabStop = false;
            this.commonSwapFileGroupBox.Text = "Swap File Usage";
            this.commonSwapFileGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonSwapFileUsageTypeDdl
            // 
            this.commonSwapFileUsageTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonSwapFileUsageTypeDdl.FormattingEnabled = true;
            this.commonSwapFileUsageTypeDdl.Items.AddRange(new object[] {
            "% Usage",
            "% Usage Peak"});
            this.commonSwapFileUsageTypeDdl.Location = new System.Drawing.Point(112, 15);
            this.commonSwapFileUsageTypeDdl.Name = "commonSwapFileUsageTypeDdl";
            this.commonSwapFileUsageTypeDdl.Size = new System.Drawing.Size(175, 21);
            this.commonSwapFileUsageTypeDdl.Sorted = true;
            this.commonSwapFileUsageTypeDdl.TabIndex = 30;
            // 
            // commonSwapFileSelected
            // 
            this.commonSwapFileSelected.AutoSize = true;
            this.commonSwapFileSelected.Location = new System.Drawing.Point(6, 18);
            this.commonSwapFileSelected.Name = "commonSwapFileSelected";
            this.commonSwapFileSelected.Size = new System.Drawing.Size(15, 14);
            this.commonSwapFileSelected.TabIndex = 29;
            this.commonSwapFileSelected.Tag = RemoteMon_Lib.CommonMonitorType.SwapFileUsage;
            this.commonSwapFileSelected.UseVisualStyleBackColor = true;
            this.commonSwapFileSelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonSwapFileGtLtLabel
            // 
            this.commonSwapFileGtLtLabel.AutoSize = true;
            this.commonSwapFileGtLtLabel.Location = new System.Drawing.Point(316, 18);
            this.commonSwapFileGtLtLabel.Name = "commonSwapFileGtLtLabel";
            this.commonSwapFileGtLtLabel.Size = new System.Drawing.Size(13, 13);
            this.commonSwapFileGtLtLabel.TabIndex = 1;
            this.commonSwapFileGtLtLabel.Text = ">";
            // 
            // commonSwapFileWarningLabel
            // 
            this.commonSwapFileWarningLabel.AutoSize = true;
            this.commonSwapFileWarningLabel.Location = new System.Drawing.Point(349, 18);
            this.commonSwapFileWarningLabel.Name = "commonSwapFileWarningLabel";
            this.commonSwapFileWarningLabel.Size = new System.Drawing.Size(50, 13);
            this.commonSwapFileWarningLabel.TabIndex = 1;
            this.commonSwapFileWarningLabel.Text = "Warning:";
            // 
            // commonSwapFileCriticalTextBox
            // 
            this.commonSwapFileCriticalTextBox.Location = new System.Drawing.Point(504, 15);
            this.commonSwapFileCriticalTextBox.MaxLength = 150;
            this.commonSwapFileCriticalTextBox.Name = "commonSwapFileCriticalTextBox";
            this.commonSwapFileCriticalTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonSwapFileCriticalTextBox.TabIndex = 2;
            this.commonSwapFileCriticalTextBox.Text = "90";
            // 
            // commonSwapFileUsageTypeLabel
            // 
            this.commonSwapFileUsageTypeLabel.AutoSize = true;
            this.commonSwapFileUsageTypeLabel.Location = new System.Drawing.Point(65, 18);
            this.commonSwapFileUsageTypeLabel.Name = "commonSwapFileUsageTypeLabel";
            this.commonSwapFileUsageTypeLabel.Size = new System.Drawing.Size(41, 13);
            this.commonSwapFileUsageTypeLabel.TabIndex = 10;
            this.commonSwapFileUsageTypeLabel.Text = "Usage:";
            // 
            // commonSwapFileWarningPctLabel
            // 
            this.commonSwapFileWarningPctLabel.AutoSize = true;
            this.commonSwapFileWarningPctLabel.Location = new System.Drawing.Point(424, 18);
            this.commonSwapFileWarningPctLabel.Name = "commonSwapFileWarningPctLabel";
            this.commonSwapFileWarningPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonSwapFileWarningPctLabel.TabIndex = 1;
            this.commonSwapFileWarningPctLabel.Text = "%";
            // 
            // commonSwapFileWarningTextBox
            // 
            this.commonSwapFileWarningTextBox.Location = new System.Drawing.Point(401, 15);
            this.commonSwapFileWarningTextBox.MaxLength = 150;
            this.commonSwapFileWarningTextBox.Name = "commonSwapFileWarningTextBox";
            this.commonSwapFileWarningTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonSwapFileWarningTextBox.TabIndex = 2;
            this.commonSwapFileWarningTextBox.Text = "70";
            // 
            // commonSwapFileCriticalLabel
            // 
            this.commonSwapFileCriticalLabel.AutoSize = true;
            this.commonSwapFileCriticalLabel.Location = new System.Drawing.Point(460, 18);
            this.commonSwapFileCriticalLabel.Name = "commonSwapFileCriticalLabel";
            this.commonSwapFileCriticalLabel.Size = new System.Drawing.Size(41, 13);
            this.commonSwapFileCriticalLabel.TabIndex = 1;
            this.commonSwapFileCriticalLabel.Text = "Critical:";
            // 
            // commonSwapFileCriticalPctLabel
            // 
            this.commonSwapFileCriticalPctLabel.AutoSize = true;
            this.commonSwapFileCriticalPctLabel.Location = new System.Drawing.Point(527, 18);
            this.commonSwapFileCriticalPctLabel.Name = "commonSwapFileCriticalPctLabel";
            this.commonSwapFileCriticalPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonSwapFileCriticalPctLabel.TabIndex = 1;
            this.commonSwapFileCriticalPctLabel.Text = "%";
            // 
            // commonProcessGroupBox
            // 
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateUsageTypeDdl);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateProcessDd);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateGtLtLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateWarningLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateCriticalTextBox);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessSelected);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateProcessLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateWarningPctLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateUsageTypeLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateWarningTextBox);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateCriticalPctLabel);
            this.commonProcessGroupBox.Controls.Add(this.commonProcessStateCriticalLabel);
            this.commonProcessGroupBox.Location = new System.Drawing.Point(6, 223);
            this.commonProcessGroupBox.Name = "commonProcessGroupBox";
            this.commonProcessGroupBox.Size = new System.Drawing.Size(569, 82);
            this.commonProcessGroupBox.TabIndex = 31;
            this.commonProcessGroupBox.TabStop = false;
            this.commonProcessGroupBox.Text = "Process State";
            this.commonProcessGroupBox.Enter += this.CommonMonitorGroupBoxEnter;
            // 
            // commonProcessStateUsageTypeDdl
            // 
            this.commonProcessStateUsageTypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonProcessStateUsageTypeDdl.FormattingEnabled = true;
            this.commonProcessStateUsageTypeDdl.Items.AddRange(new object[] {
            "% User Time",
            "Elapsed Time"});
            this.commonProcessStateUsageTypeDdl.Location = new System.Drawing.Point(112, 20);
            this.commonProcessStateUsageTypeDdl.Name = "commonProcessStateUsageTypeDdl";
            this.commonProcessStateUsageTypeDdl.Size = new System.Drawing.Size(175, 21);
            this.commonProcessStateUsageTypeDdl.Sorted = true;
            this.commonProcessStateUsageTypeDdl.TabIndex = 30;
            this.commonProcessStateUsageTypeDdl.SelectedIndexChanged += this.CommonProcessStateUsageTypeDdlSelectedIndexChanged;
            // 
            // commonProcessStateProcessDd
            // 
            this.commonProcessStateProcessDd.DropDownWidth = 250;
            this.commonProcessStateProcessDd.FormattingEnabled = true;
            this.commonProcessStateProcessDd.Location = new System.Drawing.Point(362, 20);
            this.commonProcessStateProcessDd.Name = "commonProcessStateProcessDd";
            this.commonProcessStateProcessDd.Size = new System.Drawing.Size(175, 21);
            this.commonProcessStateProcessDd.Sorted = true;
            this.commonProcessStateProcessDd.TabIndex = 30;
            // 
            // commonProcessStateGtLtLabel
            // 
            this.commonProcessStateGtLtLabel.AutoSize = true;
            this.commonProcessStateGtLtLabel.Location = new System.Drawing.Point(65, 57);
            this.commonProcessStateGtLtLabel.Name = "commonProcessStateGtLtLabel";
            this.commonProcessStateGtLtLabel.Size = new System.Drawing.Size(13, 13);
            this.commonProcessStateGtLtLabel.TabIndex = 1;
            this.commonProcessStateGtLtLabel.Text = ">";
            // 
            // commonProcessStateWarningLabel
            // 
            this.commonProcessStateWarningLabel.AutoSize = true;
            this.commonProcessStateWarningLabel.Location = new System.Drawing.Point(94, 57);
            this.commonProcessStateWarningLabel.Name = "commonProcessStateWarningLabel";
            this.commonProcessStateWarningLabel.Size = new System.Drawing.Size(50, 13);
            this.commonProcessStateWarningLabel.TabIndex = 1;
            this.commonProcessStateWarningLabel.Text = "Warning:";
            // 
            // commonProcessStateCriticalTextBox
            // 
            this.commonProcessStateCriticalTextBox.Location = new System.Drawing.Point(265, 54);
            this.commonProcessStateCriticalTextBox.MaxLength = 150;
            this.commonProcessStateCriticalTextBox.Name = "commonProcessStateCriticalTextBox";
            this.commonProcessStateCriticalTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonProcessStateCriticalTextBox.TabIndex = 2;
            this.commonProcessStateCriticalTextBox.Tag = 25;
            this.commonProcessStateCriticalTextBox.Text = "90";
            // 
            // commonProcessSelected
            // 
            this.commonProcessSelected.AutoSize = true;
            this.commonProcessSelected.Location = new System.Drawing.Point(6, 23);
            this.commonProcessSelected.Name = "commonProcessSelected";
            this.commonProcessSelected.Size = new System.Drawing.Size(15, 14);
            this.commonProcessSelected.TabIndex = 29;
            this.commonProcessSelected.Tag = RemoteMon_Lib.CommonMonitorType.ProcessState;
            this.commonProcessSelected.UseVisualStyleBackColor = true;
            this.commonProcessSelected.Click += this.CommonMonitorMonitorSelectCheckedChanged;
            // 
            // commonProcessStateProcessLabel
            // 
            this.commonProcessStateProcessLabel.AutoSize = true;
            this.commonProcessStateProcessLabel.Location = new System.Drawing.Point(308, 23);
            this.commonProcessStateProcessLabel.Name = "commonProcessStateProcessLabel";
            this.commonProcessStateProcessLabel.Size = new System.Drawing.Size(48, 13);
            this.commonProcessStateProcessLabel.TabIndex = 10;
            this.commonProcessStateProcessLabel.Text = "Process:";
            // 
            // commonProcessStateWarningPctLabel
            // 
            this.commonProcessStateWarningPctLabel.AutoSize = true;
            this.commonProcessStateWarningPctLabel.Location = new System.Drawing.Point(169, 57);
            this.commonProcessStateWarningPctLabel.Name = "commonProcessStateWarningPctLabel";
            this.commonProcessStateWarningPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonProcessStateWarningPctLabel.TabIndex = 1;
            this.commonProcessStateWarningPctLabel.Text = "%";
            // 
            // commonProcessStateUsageTypeLabel
            // 
            this.commonProcessStateUsageTypeLabel.AutoSize = true;
            this.commonProcessStateUsageTypeLabel.Location = new System.Drawing.Point(65, 23);
            this.commonProcessStateUsageTypeLabel.Name = "commonProcessStateUsageTypeLabel";
            this.commonProcessStateUsageTypeLabel.Size = new System.Drawing.Size(41, 13);
            this.commonProcessStateUsageTypeLabel.TabIndex = 10;
            this.commonProcessStateUsageTypeLabel.Text = "Usage:";
            // 
            // commonProcessStateWarningTextBox
            // 
            this.commonProcessStateWarningTextBox.Location = new System.Drawing.Point(146, 54);
            this.commonProcessStateWarningTextBox.MaxLength = 150;
            this.commonProcessStateWarningTextBox.Name = "commonProcessStateWarningTextBox";
            this.commonProcessStateWarningTextBox.Size = new System.Drawing.Size(25, 20);
            this.commonProcessStateWarningTextBox.TabIndex = 2;
            this.commonProcessStateWarningTextBox.Tag = 25;
            this.commonProcessStateWarningTextBox.Text = "70";
            // 
            // commonProcessStateCriticalPctLabel
            // 
            this.commonProcessStateCriticalPctLabel.AutoSize = true;
            this.commonProcessStateCriticalPctLabel.Location = new System.Drawing.Point(288, 57);
            this.commonProcessStateCriticalPctLabel.Name = "commonProcessStateCriticalPctLabel";
            this.commonProcessStateCriticalPctLabel.Size = new System.Drawing.Size(15, 13);
            this.commonProcessStateCriticalPctLabel.TabIndex = 1;
            this.commonProcessStateCriticalPctLabel.Text = "%";
            // 
            // commonProcessStateCriticalLabel
            // 
            this.commonProcessStateCriticalLabel.AutoSize = true;
            this.commonProcessStateCriticalLabel.Location = new System.Drawing.Point(221, 57);
            this.commonProcessStateCriticalLabel.Name = "commonProcessStateCriticalLabel";
            this.commonProcessStateCriticalLabel.Size = new System.Drawing.Size(41, 13);
            this.commonProcessStateCriticalLabel.TabIndex = 1;
            this.commonProcessStateCriticalLabel.Text = "Critical:";
            // 
            // addServerValidIpGroupBox2
            // 
            this.addServerValidIpGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addServerValidIpGroupBox2.Controls.Add(this.commonOneLabel);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpMonitorName2);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpMonitorNameLabel2);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpIpLabel2);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpLabel2);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpPingBtn2);
            this.addServerValidIpGroupBox2.Controls.Add(this.addServerValidIpIpTextBox2);
            this.addServerValidIpGroupBox2.Location = new System.Drawing.Point(5, 11);
            this.addServerValidIpGroupBox2.Name = "addServerValidIpGroupBox2";
            this.addServerValidIpGroupBox2.Size = new System.Drawing.Size(581, 47);
            this.addServerValidIpGroupBox2.TabIndex = 1;
            this.addServerValidIpGroupBox2.TabStop = false;
            this.addServerValidIpGroupBox2.Text = "Ip/Hostname and Monitor Name";
            // 
            // commonOneLabel
            // 
            this.commonOneLabel.AutoSize = true;
            this.commonOneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commonOneLabel.ForeColor = System.Drawing.Color.Red;
            this.commonOneLabel.Location = new System.Drawing.Point(276, -3);
            this.commonOneLabel.Name = "commonOneLabel";
            this.commonOneLabel.Size = new System.Drawing.Size(17, 17);
            this.commonOneLabel.TabIndex = 5;
            this.commonOneLabel.Text = "1";
            this.addServerToolTip.SetToolTip(this.commonOneLabel, "Step 1: enter an IP or Hostname of the machine and (optionally) enter a friendly " +
                    "name for this monitor");
            // 
            // addServerValidIpMonitorName2
            // 
            this.addServerValidIpMonitorName2.Location = new System.Drawing.Point(262, 19);
            this.addServerValidIpMonitorName2.MaxLength = 50;
            this.addServerValidIpMonitorName2.Name = "addServerValidIpMonitorName2";
            this.addServerValidIpMonitorName2.Size = new System.Drawing.Size(100, 20);
            this.addServerValidIpMonitorName2.TabIndex = 1;
            this.addServerValidIpMonitorName2.Leave += this.AddServerMonitorNameLeaveFocus;
            // 
            // addServerValidIpMonitorNameLabel2
            // 
            this.addServerValidIpMonitorNameLabel2.AutoSize = true;
            this.addServerValidIpMonitorNameLabel2.CausesValidation = false;
            this.addServerValidIpMonitorNameLabel2.Location = new System.Drawing.Point(185, 22);
            this.addServerValidIpMonitorNameLabel2.Name = "addServerValidIpMonitorNameLabel2";
            this.addServerValidIpMonitorNameLabel2.Size = new System.Drawing.Size(76, 13);
            this.addServerValidIpMonitorNameLabel2.TabIndex = 4;
            this.addServerValidIpMonitorNameLabel2.Text = "Monitor Name:";
            // 
            // addServerValidIpIpLabel2
            // 
            this.addServerValidIpIpLabel2.AutoSize = true;
            this.addServerValidIpIpLabel2.CausesValidation = false;
            this.addServerValidIpIpLabel2.Location = new System.Drawing.Point(6, 22);
            this.addServerValidIpIpLabel2.Name = "addServerValidIpIpLabel2";
            this.addServerValidIpIpLabel2.Size = new System.Drawing.Size(72, 13);
            this.addServerValidIpIpLabel2.TabIndex = 3;
            this.addServerValidIpIpLabel2.Text = "Ip/Hostname:";
            // 
            // addServerValidIpLabel2
            // 
            this.addServerValidIpLabel2.AutoEllipsis = true;
            this.addServerValidIpLabel2.CausesValidation = false;
            this.addServerValidIpLabel2.Location = new System.Drawing.Point(487, 22);
            this.addServerValidIpLabel2.Name = "addServerValidIpLabel2";
            this.addServerValidIpLabel2.Size = new System.Drawing.Size(69, 13);
            this.addServerValidIpLabel2.TabIndex = 2;
            this.addServerValidIpLabel2.Text = "...";
            // 
            // addServerValidIpPingBtn2
            // 
            this.addServerValidIpPingBtn2.CausesValidation = false;
            this.addServerValidIpPingBtn2.Location = new System.Drawing.Point(406, 17);
            this.addServerValidIpPingBtn2.Name = "addServerValidIpPingBtn2";
            this.addServerValidIpPingBtn2.Size = new System.Drawing.Size(75, 23);
            this.addServerValidIpPingBtn2.TabIndex = 2;
            this.addServerValidIpPingBtn2.Text = "Confirm";
            this.addServerValidIpPingBtn2.UseVisualStyleBackColor = true;
            this.addServerValidIpPingBtn2.Click += this.AddServerValidIpPingBtnClick;
            // 
            // addServerValidIpIpTextBox2
            // 
            this.addServerValidIpIpTextBox2.Location = new System.Drawing.Point(78, 19);
            this.addServerValidIpIpTextBox2.MaxLength = 15;
            this.addServerValidIpIpTextBox2.Name = "addServerValidIpIpTextBox2";
            this.addServerValidIpIpTextBox2.Size = new System.Drawing.Size(100, 20);
            this.addServerValidIpIpTextBox2.TabIndex = 0;
            this.addServerValidIpIpTextBox2.Leave += this.AddServerIpLeaveFocus;
            // 
            // basicBaseTab
            // 
            this.basicBaseTab.Controls.Add(this.addServerValidIpMonitorName3);
            this.basicBaseTab.Controls.Add(this.addServerValidIpMonitorNameLabel3);
            this.basicBaseTab.Controls.Add(this.basicMonitorTestDataUpdateFreqLabel);
            this.basicBaseTab.Controls.Add(this.basicMonitorTestDataUpdateFreqTextBox);
            this.basicBaseTab.Controls.Add(this.basicMonitorFtpGroupBox);
            this.basicBaseTab.Controls.Add(this.basicMonitorPingGroupBox);
            this.basicBaseTab.Controls.Add(this.basicMonitorHttpGroupBox);
            this.basicBaseTab.Location = new System.Drawing.Point(4, 22);
            this.basicBaseTab.Name = "basicBaseTab";
            this.basicBaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.basicBaseTab.Size = new System.Drawing.Size(592, 513);
            this.basicBaseTab.TabIndex = 1;
            this.basicBaseTab.Text = "Basic";
            this.basicBaseTab.UseVisualStyleBackColor = true;
            // 
            // addServerValidIpMonitorName3
            // 
            this.addServerValidIpMonitorName3.Location = new System.Drawing.Point(347, 470);
            this.addServerValidIpMonitorName3.MaxLength = 50;
            this.addServerValidIpMonitorName3.Name = "addServerValidIpMonitorName3";
            this.addServerValidIpMonitorName3.Size = new System.Drawing.Size(100, 20);
            this.addServerValidIpMonitorName3.TabIndex = 29;
            this.addServerValidIpMonitorName3.Leave += this.AddServerMonitorNameLeaveFocus;
            // 
            // addServerValidIpMonitorNameLabel3
            // 
            this.addServerValidIpMonitorNameLabel3.AutoSize = true;
            this.addServerValidIpMonitorNameLabel3.CausesValidation = false;
            this.addServerValidIpMonitorNameLabel3.Location = new System.Drawing.Point(270, 473);
            this.addServerValidIpMonitorNameLabel3.Name = "addServerValidIpMonitorNameLabel3";
            this.addServerValidIpMonitorNameLabel3.Size = new System.Drawing.Size(76, 13);
            this.addServerValidIpMonitorNameLabel3.TabIndex = 30;
            this.addServerValidIpMonitorNameLabel3.Text = "Monitor Name:";
            // 
            // basicMonitorTestDataUpdateFreqLabel
            // 
            this.basicMonitorTestDataUpdateFreqLabel.AutoSize = true;
            this.basicMonitorTestDataUpdateFreqLabel.Location = new System.Drawing.Point(11, 473);
            this.basicMonitorTestDataUpdateFreqLabel.Name = "basicMonitorTestDataUpdateFreqLabel";
            this.basicMonitorTestDataUpdateFreqLabel.Size = new System.Drawing.Size(120, 13);
            this.basicMonitorTestDataUpdateFreqLabel.TabIndex = 28;
            this.basicMonitorTestDataUpdateFreqLabel.Text = "Update Frequency (ms):";
            // 
            // basicMonitorFtpGroupBox
            // 
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpUseSslCheckBox);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpMonitorSelect);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathPortTextBox);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPassTextBox);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpUserTextBox);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathTextBox);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPassLabel);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathPortLabel);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpUserLabel);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathLabel);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathCheckResultText);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathCheckBtn);
            this.basicMonitorFtpGroupBox.Controls.Add(this.basicMonitorFtpPathCheckResultLabel);
            this.basicMonitorFtpGroupBox.Location = new System.Drawing.Point(6, 121);
            this.basicMonitorFtpGroupBox.Name = "basicMonitorFtpGroupBox";
            this.basicMonitorFtpGroupBox.Size = new System.Drawing.Size(579, 76);
            this.basicMonitorFtpGroupBox.TabIndex = 4;
            this.basicMonitorFtpGroupBox.TabStop = false;
            this.basicMonitorFtpGroupBox.Text = "FTP Monitor";
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpGroupBox, "Ftp monitor - attempts to log into the FTP server and print current directory");
            this.basicMonitorFtpGroupBox.Enter += this.BasicMonitorGroupBoxEnter;
            // 
            // basicMonitorFtpUseSslCheckBox
            // 
            this.basicMonitorFtpUseSslCheckBox.AutoSize = true;
            this.basicMonitorFtpUseSslCheckBox.Location = new System.Drawing.Point(427, 48);
            this.basicMonitorFtpUseSslCheckBox.Name = "basicMonitorFtpUseSslCheckBox";
            this.basicMonitorFtpUseSslCheckBox.Size = new System.Drawing.Size(68, 17);
            this.basicMonitorFtpUseSslCheckBox.TabIndex = 30;
            this.basicMonitorFtpUseSslCheckBox.Text = "Use SSL";
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpUseSslCheckBox, resources.GetString("basicMonitorFtpUseSslCheckBox.ToolTip"));
            this.basicMonitorFtpUseSslCheckBox.UseVisualStyleBackColor = true;
            // 
            // basicMonitorFtpMonitorSelect
            // 
            this.basicMonitorFtpMonitorSelect.AutoSize = true;
            this.basicMonitorFtpMonitorSelect.Location = new System.Drawing.Point(6, 22);
            this.basicMonitorFtpMonitorSelect.Name = "basicMonitorFtpMonitorSelect";
            this.basicMonitorFtpMonitorSelect.Size = new System.Drawing.Size(15, 14);
            this.basicMonitorFtpMonitorSelect.TabIndex = 29;
            this.basicMonitorFtpMonitorSelect.Tag = RemoteMon_Lib.BasicMonitorType.Ftp;
            this.basicMonitorFtpMonitorSelect.UseVisualStyleBackColor = true;
            this.basicMonitorFtpMonitorSelect.Click += this.BasicMonitorMonitorSelectCheckedChanged;
            // 
            // basicMonitorFtpPathPortTextBox
            // 
            this.basicMonitorFtpPathPortTextBox.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.basicMonitorFtpPathPortTextBox.Location = new System.Drawing.Point(342, 19);
            this.basicMonitorFtpPathPortTextBox.Mask = "#####";
            this.basicMonitorFtpPathPortTextBox.Name = "basicMonitorFtpPathPortTextBox";
            this.basicMonitorFtpPathPortTextBox.Size = new System.Drawing.Size(38, 20);
            this.basicMonitorFtpPathPortTextBox.TabIndex = 7;
            this.basicMonitorFtpPathPortTextBox.Tag = "21";
            this.basicMonitorFtpPathPortTextBox.Text = "21";
            this.basicMonitorFtpPathPortTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpPathPortTextBox, "Port the FTP server runs on (default: 21)");
            this.basicMonitorFtpPathPortTextBox.ValidatingType = typeof(int);
            this.basicMonitorFtpPathPortTextBox.TypeValidationCompleted += this.BasicMonitorPortTextBoxTypeValidationCompleted;
            // 
            // basicMonitorFtpPassTextBox
            // 
            this.basicMonitorFtpPassTextBox.Location = new System.Drawing.Point(251, 46);
            this.basicMonitorFtpPassTextBox.MaxLength = 150;
            this.basicMonitorFtpPassTextBox.Name = "basicMonitorFtpPassTextBox";
            this.basicMonitorFtpPassTextBox.PasswordChar = '*';
            this.basicMonitorFtpPassTextBox.Size = new System.Drawing.Size(144, 20);
            this.basicMonitorFtpPassTextBox.TabIndex = 2;
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpPassTextBox, "Password to connect to FTP server (default: empty)");
            // 
            // basicMonitorFtpUserTextBox
            // 
            this.basicMonitorFtpUserTextBox.Location = new System.Drawing.Point(42, 46);
            this.basicMonitorFtpUserTextBox.MaxLength = 150;
            this.basicMonitorFtpUserTextBox.Name = "basicMonitorFtpUserTextBox";
            this.basicMonitorFtpUserTextBox.Size = new System.Drawing.Size(154, 20);
            this.basicMonitorFtpUserTextBox.TabIndex = 2;
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpUserTextBox, "Username to connect to FTP server (default: empty)");
            // 
            // basicMonitorFtpPathTextBox
            // 
            this.basicMonitorFtpPathTextBox.Location = new System.Drawing.Point(80, 19);
            this.basicMonitorFtpPathTextBox.MaxLength = 150;
            this.basicMonitorFtpPathTextBox.Name = "basicMonitorFtpPathTextBox";
            this.basicMonitorFtpPathTextBox.Size = new System.Drawing.Size(224, 20);
            this.basicMonitorFtpPathTextBox.TabIndex = 2;
            this.basicMonitorFtpPathTextBox.Text = "ftp://ftp.microsoft.com";
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpPathTextBox, "The address of the FTP server to check.");
            // 
            // basicMonitorFtpPassLabel
            // 
            this.basicMonitorFtpPassLabel.AutoSize = true;
            this.basicMonitorFtpPassLabel.Location = new System.Drawing.Point(212, 49);
            this.basicMonitorFtpPassLabel.Name = "basicMonitorFtpPassLabel";
            this.basicMonitorFtpPassLabel.Size = new System.Drawing.Size(33, 13);
            this.basicMonitorFtpPassLabel.TabIndex = 1;
            this.basicMonitorFtpPassLabel.Text = "Pass:";
            // 
            // basicMonitorFtpPathPortLabel
            // 
            this.basicMonitorFtpPathPortLabel.AutoSize = true;
            this.basicMonitorFtpPathPortLabel.Location = new System.Drawing.Point(311, 22);
            this.basicMonitorFtpPathPortLabel.Name = "basicMonitorFtpPathPortLabel";
            this.basicMonitorFtpPathPortLabel.Size = new System.Drawing.Size(29, 13);
            this.basicMonitorFtpPathPortLabel.TabIndex = 6;
            this.basicMonitorFtpPathPortLabel.Text = "Port:";
            // 
            // basicMonitorFtpUserLabel
            // 
            this.basicMonitorFtpUserLabel.AutoSize = true;
            this.basicMonitorFtpUserLabel.Location = new System.Drawing.Point(3, 49);
            this.basicMonitorFtpUserLabel.Name = "basicMonitorFtpUserLabel";
            this.basicMonitorFtpUserLabel.Size = new System.Drawing.Size(32, 13);
            this.basicMonitorFtpUserLabel.TabIndex = 1;
            this.basicMonitorFtpUserLabel.Text = "User:";
            // 
            // basicMonitorFtpPathLabel
            // 
            this.basicMonitorFtpPathLabel.AutoSize = true;
            this.basicMonitorFtpPathLabel.Location = new System.Drawing.Point(26, 22);
            this.basicMonitorFtpPathLabel.Name = "basicMonitorFtpPathLabel";
            this.basicMonitorFtpPathLabel.Size = new System.Drawing.Size(50, 13);
            this.basicMonitorFtpPathLabel.TabIndex = 1;
            this.basicMonitorFtpPathLabel.Text = "Ftp Path:";
            // 
            // basicMonitorFtpPathCheckResultText
            // 
            this.basicMonitorFtpPathCheckResultText.AutoSize = true;
            this.basicMonitorFtpPathCheckResultText.Location = new System.Drawing.Point(513, 22);
            this.basicMonitorFtpPathCheckResultText.Name = "basicMonitorFtpPathCheckResultText";
            this.basicMonitorFtpPathCheckResultText.Size = new System.Drawing.Size(16, 13);
            this.basicMonitorFtpPathCheckResultText.TabIndex = 5;
            this.basicMonitorFtpPathCheckResultText.Text = "...";
            // 
            // basicMonitorFtpPathCheckBtn
            // 
            this.basicMonitorFtpPathCheckBtn.Location = new System.Drawing.Point(386, 17);
            this.basicMonitorFtpPathCheckBtn.Name = "basicMonitorFtpPathCheckBtn";
            this.basicMonitorFtpPathCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.basicMonitorFtpPathCheckBtn.TabIndex = 3;
            this.basicMonitorFtpPathCheckBtn.Text = "Check";
            this.addServerToolTip.SetToolTip(this.basicMonitorFtpPathCheckBtn, "Click to check the current status with the provided information.");
            this.basicMonitorFtpPathCheckBtn.UseVisualStyleBackColor = true;
            this.basicMonitorFtpPathCheckBtn.Click += this.BasicMonitorFtpPathCheckBtnClick;
            // 
            // basicMonitorFtpPathCheckResultLabel
            // 
            this.basicMonitorFtpPathCheckResultLabel.AutoSize = true;
            this.basicMonitorFtpPathCheckResultLabel.Location = new System.Drawing.Point(467, 22);
            this.basicMonitorFtpPathCheckResultLabel.Name = "basicMonitorFtpPathCheckResultLabel";
            this.basicMonitorFtpPathCheckResultLabel.Size = new System.Drawing.Size(40, 13);
            this.basicMonitorFtpPathCheckResultLabel.TabIndex = 4;
            this.basicMonitorFtpPathCheckResultLabel.Text = "Result:";
            // 
            // basicMonitorPingGroupBox
            // 
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorPingMonitorSelect);
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorIpResult);
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorIpResultLabel);
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorIpCheckBtn);
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorIpTextBox);
            this.basicMonitorPingGroupBox.Controls.Add(this.basicMonitorIpLabel);
            this.basicMonitorPingGroupBox.Location = new System.Drawing.Point(6, 3);
            this.basicMonitorPingGroupBox.Name = "basicMonitorPingGroupBox";
            this.basicMonitorPingGroupBox.Size = new System.Drawing.Size(581, 54);
            this.basicMonitorPingGroupBox.TabIndex = 5;
            this.basicMonitorPingGroupBox.TabStop = false;
            this.basicMonitorPingGroupBox.Text = "Ping Monitor";
            this.addServerToolTip.SetToolTip(this.basicMonitorPingGroupBox, "Ping monitor - pings device at the set frequency.");
            this.basicMonitorPingGroupBox.Enter += this.BasicMonitorGroupBoxEnter;
            // 
            // basicMonitorPingMonitorSelect
            // 
            this.basicMonitorPingMonitorSelect.AutoSize = true;
            this.basicMonitorPingMonitorSelect.Location = new System.Drawing.Point(6, 24);
            this.basicMonitorPingMonitorSelect.Name = "basicMonitorPingMonitorSelect";
            this.basicMonitorPingMonitorSelect.Size = new System.Drawing.Size(15, 14);
            this.basicMonitorPingMonitorSelect.TabIndex = 29;
            this.basicMonitorPingMonitorSelect.Tag = RemoteMon_Lib.BasicMonitorType.Ping;
            this.basicMonitorPingMonitorSelect.UseVisualStyleBackColor = true;
            this.basicMonitorPingMonitorSelect.Click += this.BasicMonitorMonitorSelectCheckedChanged;
            // 
            // basicMonitorIpResult
            // 
            this.basicMonitorIpResult.AutoSize = true;
            this.basicMonitorIpResult.Location = new System.Drawing.Point(513, 24);
            this.basicMonitorIpResult.Name = "basicMonitorIpResult";
            this.basicMonitorIpResult.Size = new System.Drawing.Size(16, 13);
            this.basicMonitorIpResult.TabIndex = 14;
            this.basicMonitorIpResult.Text = "...";
            // 
            // basicMonitorIpResultLabel
            // 
            this.basicMonitorIpResultLabel.AutoSize = true;
            this.basicMonitorIpResultLabel.Location = new System.Drawing.Point(467, 24);
            this.basicMonitorIpResultLabel.Name = "basicMonitorIpResultLabel";
            this.basicMonitorIpResultLabel.Size = new System.Drawing.Size(40, 13);
            this.basicMonitorIpResultLabel.TabIndex = 13;
            this.basicMonitorIpResultLabel.Text = "Result:";
            // 
            // basicMonitorIpCheckBtn
            // 
            this.basicMonitorIpCheckBtn.Location = new System.Drawing.Point(386, 19);
            this.basicMonitorIpCheckBtn.Name = "basicMonitorIpCheckBtn";
            this.basicMonitorIpCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.basicMonitorIpCheckBtn.TabIndex = 12;
            this.basicMonitorIpCheckBtn.Text = "Check";
            this.addServerToolTip.SetToolTip(this.basicMonitorIpCheckBtn, "Click to check the current status with the provided information.");
            this.basicMonitorIpCheckBtn.UseVisualStyleBackColor = true;
            this.basicMonitorIpCheckBtn.Click += this.BasicMonitorIpCheckBtnClick;
            // 
            // basicMonitorIpTextBox
            // 
            this.basicMonitorIpTextBox.Location = new System.Drawing.Point(80, 21);
            this.basicMonitorIpTextBox.MaxLength = 15;
            this.basicMonitorIpTextBox.Name = "basicMonitorIpTextBox";
            this.basicMonitorIpTextBox.Size = new System.Drawing.Size(224, 20);
            this.basicMonitorIpTextBox.TabIndex = 11;
            this.addServerToolTip.SetToolTip(this.basicMonitorIpTextBox, "Ip of the server to check.");
            this.basicMonitorIpTextBox.Leave += this.AddServerIpLeaveFocus;
            // 
            // basicMonitorIpLabel
            // 
            this.basicMonitorIpLabel.AutoSize = true;
            this.basicMonitorIpLabel.Location = new System.Drawing.Point(39, 24);
            this.basicMonitorIpLabel.Name = "basicMonitorIpLabel";
            this.basicMonitorIpLabel.Size = new System.Drawing.Size(19, 13);
            this.basicMonitorIpLabel.TabIndex = 10;
            this.basicMonitorIpLabel.Text = "Ip:";
            // 
            // basicMonitorHttpGroupBox
            // 
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpMonitorSelect);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathPortTextBox);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathPortLabel);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathCheckResultText);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathCheckResultLabel);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathCheckBtn);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathTextBox);
            this.basicMonitorHttpGroupBox.Controls.Add(this.basicMonitorHttpPathLabel);
            this.basicMonitorHttpGroupBox.Location = new System.Drawing.Point(6, 63);
            this.basicMonitorHttpGroupBox.Name = "basicMonitorHttpGroupBox";
            this.basicMonitorHttpGroupBox.Size = new System.Drawing.Size(579, 52);
            this.basicMonitorHttpGroupBox.TabIndex = 3;
            this.basicMonitorHttpGroupBox.TabStop = false;
            this.basicMonitorHttpGroupBox.Text = "Http/Https Monitor";
            this.addServerToolTip.SetToolTip(this.basicMonitorHttpGroupBox, "Http monitor - Gets a webpage, ensuring the website is still running");
            this.basicMonitorHttpGroupBox.Enter += this.BasicMonitorGroupBoxEnter;
            // 
            // basicMonitorHttpMonitorSelect
            // 
            this.basicMonitorHttpMonitorSelect.AutoSize = true;
            this.basicMonitorHttpMonitorSelect.Location = new System.Drawing.Point(6, 23);
            this.basicMonitorHttpMonitorSelect.Name = "basicMonitorHttpMonitorSelect";
            this.basicMonitorHttpMonitorSelect.Size = new System.Drawing.Size(15, 14);
            this.basicMonitorHttpMonitorSelect.TabIndex = 29;
            this.basicMonitorHttpMonitorSelect.Tag = RemoteMon_Lib.BasicMonitorType.Http;
            this.basicMonitorHttpMonitorSelect.UseVisualStyleBackColor = true;
            this.basicMonitorHttpMonitorSelect.Click += this.BasicMonitorMonitorSelectCheckedChanged;
            // 
            // basicMonitorHttpPathPortTextBox
            // 
            this.basicMonitorHttpPathPortTextBox.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.basicMonitorHttpPathPortTextBox.Location = new System.Drawing.Point(342, 20);
            this.basicMonitorHttpPathPortTextBox.Mask = "#####";
            this.basicMonitorHttpPathPortTextBox.Name = "basicMonitorHttpPathPortTextBox";
            this.basicMonitorHttpPathPortTextBox.Size = new System.Drawing.Size(38, 20);
            this.basicMonitorHttpPathPortTextBox.TabIndex = 7;
            this.basicMonitorHttpPathPortTextBox.Tag = "80";
            this.basicMonitorHttpPathPortTextBox.Text = "80";
            this.basicMonitorHttpPathPortTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.addServerToolTip.SetToolTip(this.basicMonitorHttpPathPortTextBox, "Port the website runs on (default: 80)");
            this.basicMonitorHttpPathPortTextBox.ValidatingType = typeof(int);
            this.basicMonitorHttpPathPortTextBox.TypeValidationCompleted += this.BasicMonitorPortTextBoxTypeValidationCompleted;
            // 
            // basicMonitorHttpPathPortLabel
            // 
            this.basicMonitorHttpPathPortLabel.AutoSize = true;
            this.basicMonitorHttpPathPortLabel.Location = new System.Drawing.Point(311, 23);
            this.basicMonitorHttpPathPortLabel.Name = "basicMonitorHttpPathPortLabel";
            this.basicMonitorHttpPathPortLabel.Size = new System.Drawing.Size(29, 13);
            this.basicMonitorHttpPathPortLabel.TabIndex = 6;
            this.basicMonitorHttpPathPortLabel.Text = "Port:";
            // 
            // basicMonitorHttpPathCheckResultText
            // 
            this.basicMonitorHttpPathCheckResultText.AutoSize = true;
            this.basicMonitorHttpPathCheckResultText.Location = new System.Drawing.Point(513, 23);
            this.basicMonitorHttpPathCheckResultText.Name = "basicMonitorHttpPathCheckResultText";
            this.basicMonitorHttpPathCheckResultText.Size = new System.Drawing.Size(16, 13);
            this.basicMonitorHttpPathCheckResultText.TabIndex = 5;
            this.basicMonitorHttpPathCheckResultText.Text = "...";
            // 
            // basicMonitorHttpPathCheckResultLabel
            // 
            this.basicMonitorHttpPathCheckResultLabel.AutoSize = true;
            this.basicMonitorHttpPathCheckResultLabel.Location = new System.Drawing.Point(467, 23);
            this.basicMonitorHttpPathCheckResultLabel.Name = "basicMonitorHttpPathCheckResultLabel";
            this.basicMonitorHttpPathCheckResultLabel.Size = new System.Drawing.Size(40, 13);
            this.basicMonitorHttpPathCheckResultLabel.TabIndex = 4;
            this.basicMonitorHttpPathCheckResultLabel.Text = "Result:";
            // 
            // basicMonitorHttpPathCheckBtn
            // 
            this.basicMonitorHttpPathCheckBtn.Location = new System.Drawing.Point(386, 18);
            this.basicMonitorHttpPathCheckBtn.Name = "basicMonitorHttpPathCheckBtn";
            this.basicMonitorHttpPathCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.basicMonitorHttpPathCheckBtn.TabIndex = 3;
            this.basicMonitorHttpPathCheckBtn.Text = "Check";
            this.addServerToolTip.SetToolTip(this.basicMonitorHttpPathCheckBtn, "Click to check the current status with the provided information.");
            this.basicMonitorHttpPathCheckBtn.UseVisualStyleBackColor = true;
            this.basicMonitorHttpPathCheckBtn.Click += this.BasicMonitorHttpPathCheckBtnClick;
            // 
            // basicMonitorHttpPathTextBox
            // 
            this.basicMonitorHttpPathTextBox.Location = new System.Drawing.Point(80, 20);
            this.basicMonitorHttpPathTextBox.MaxLength = 150;
            this.basicMonitorHttpPathTextBox.Name = "basicMonitorHttpPathTextBox";
            this.basicMonitorHttpPathTextBox.Size = new System.Drawing.Size(224, 20);
            this.basicMonitorHttpPathTextBox.TabIndex = 2;
            this.basicMonitorHttpPathTextBox.Text = "http://www.google.com";
            this.addServerToolTip.SetToolTip(this.basicMonitorHttpPathTextBox, "The Url of the website to check.");
            // 
            // basicMonitorHttpPathLabel
            // 
            this.basicMonitorHttpPathLabel.AutoSize = true;
            this.basicMonitorHttpPathLabel.Location = new System.Drawing.Point(26, 23);
            this.basicMonitorHttpPathLabel.Name = "basicMonitorHttpPathLabel";
            this.basicMonitorHttpPathLabel.Size = new System.Drawing.Size(48, 13);
            this.basicMonitorHttpPathLabel.TabIndex = 1;
            this.basicMonitorHttpPathLabel.Text = "Url Path:";
            // 
            // advancedBaseTab
            // 
            this.advancedBaseTab.Controls.Add(this.addServerTabControl);
            this.advancedBaseTab.Controls.Add(this.addServerValidIpGroupBox);
            this.advancedBaseTab.Location = new System.Drawing.Point(4, 22);
            this.advancedBaseTab.Name = "advancedBaseTab";
            this.advancedBaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.advancedBaseTab.Size = new System.Drawing.Size(592, 513);
            this.advancedBaseTab.TabIndex = 0;
            this.advancedBaseTab.Text = "Advanced";
            this.advancedBaseTab.UseVisualStyleBackColor = true;
            // 
            // addServerBaseTabControl
            // 
            this.addServerBaseTabControl.Controls.Add(this.advancedBaseTab);
            this.addServerBaseTabControl.Controls.Add(this.basicBaseTab);
            this.addServerBaseTabControl.Controls.Add(this.commonBaseTab);
            this.addServerBaseTabControl.Location = new System.Drawing.Point(3, 2);
            this.addServerBaseTabControl.Name = "addServerBaseTabControl";
            this.addServerBaseTabControl.SelectedIndex = 0;
            this.addServerBaseTabControl.Size = new System.Drawing.Size(600, 539);
            this.addServerBaseTabControl.TabIndex = 3;
            this.addServerBaseTabControl.SelectedIndexChanged += this.AddServerBaseTabControlSelectedIndexChanged;
            // 
            // AddServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.addServerCancelBtn;
            this.ClientSize = new System.Drawing.Size(606, 599);
            this.Controls.Add(this.addServerBaseTabControl);
            this.Controls.Add(this.addServerAlertConfiguration);
            this.Controls.Add(this.addServerOkBtn);
            this.Controls.Add(this.addServerCancelBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddServer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a Device Monitor";
            this.FormClosed += this.AddServerFormClosed;
            this.addServerTabControl.ResumeLayout(false);
            this.perfCountersTab.ResumeLayout(false);
            this.perfCounterPerfCounterSetupGroupBox.ResumeLayout(false);
            this.perfCounterPerfCounterSetupGroupBox.PerformLayout();
            this.eventsTab.ResumeLayout(false);
            this.eventMonitorGroupBox.ResumeLayout(false);
            this.eventMonitorGroupBox.PerformLayout();
            this.eventMonitorEntryTypeFilterPanel.ResumeLayout(false);
            this.eventMonitorEntryTypeFilterPanel.PerformLayout();
            this.servicesTab.ResumeLayout(false);
            this.servicesServiceGroupBox.ResumeLayout(false);
            this.servicesServiceGroupBox.PerformLayout();
            this.addServerValidIpGroupBox.ResumeLayout(false);
            this.addServerValidIpGroupBox.PerformLayout();
            this.addServerAlertConfiguration.ResumeLayout(false);
            this.addServerAlertConfiguration.PerformLayout();
            this.commonBaseTab.ResumeLayout(false);
            this.commonGeneralSetupGroupBox.ResumeLayout(false);
            this.commonGeneralSetupGroupBox.PerformLayout();
            this.commonChooserGroupBox.ResumeLayout(false);
            this.commonChooserGroupBox.PerformLayout();
            this.commonHddGroupBox.ResumeLayout(false);
            this.commonHddGroupBox.PerformLayout();
            this.commonMemoryGroupBox.ResumeLayout(false);
            this.commonMemoryGroupBox.PerformLayout();
            this.commonServiceGroupBox.ResumeLayout(false);
            this.commonServiceGroupBox.PerformLayout();
            this.commonCpuGroupBox.ResumeLayout(false);
            this.commonCpuGroupBox.PerformLayout();
            this.commonSwapFileGroupBox.ResumeLayout(false);
            this.commonSwapFileGroupBox.PerformLayout();
            this.commonProcessGroupBox.ResumeLayout(false);
            this.commonProcessGroupBox.PerformLayout();
            this.addServerValidIpGroupBox2.ResumeLayout(false);
            this.addServerValidIpGroupBox2.PerformLayout();
            this.basicBaseTab.ResumeLayout(false);
            this.basicBaseTab.PerformLayout();
            this.basicMonitorFtpGroupBox.ResumeLayout(false);
            this.basicMonitorFtpGroupBox.PerformLayout();
            this.basicMonitorPingGroupBox.ResumeLayout(false);
            this.basicMonitorPingGroupBox.PerformLayout();
            this.basicMonitorHttpGroupBox.ResumeLayout(false);
            this.basicMonitorHttpGroupBox.PerformLayout();
            this.advancedBaseTab.ResumeLayout(false);
            this.addServerBaseTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl addServerTabControl;
        private System.Windows.Forms.TabPage perfCountersTab;
        private System.Windows.Forms.TabPage eventsTab;
        private System.Windows.Forms.TabPage servicesTab;
        private System.Windows.Forms.Button addServerCancelBtn;
        private System.Windows.Forms.Button addServerOkBtn;
        private System.Windows.Forms.GroupBox perfCounterPerfCounterSetupGroupBox;
        public System.Windows.Forms.ComboBox perfCounterPCTypeDdl;
        private System.Windows.Forms.Label perfCounterCategoryLabel;
        private System.Windows.Forms.GroupBox addServerValidIpGroupBox;
        public System.Windows.Forms.TextBox addServerValidIpMonitorName;
        private System.Windows.Forms.Label addServerValidIpMonitorNameLabel;
        private System.Windows.Forms.Label addServerValidIpIpLabel;
        private System.Windows.Forms.Label addServerValidIpLabel;
        private System.Windows.Forms.Button addServerValidIpPingBtn;
        public System.Windows.Forms.TextBox addServerValidIpIpTextBox;
        private System.Windows.Forms.Label perfCounterCategoryWaitLabel;
        private System.Windows.Forms.Label perfCounterCounterNameWaitLabel;
        public System.Windows.Forms.ComboBox perfCounterCounterNameDdl;
        private System.Windows.Forms.Label perfCounterCounterNameLabel;
        private System.Windows.Forms.Label perfCounterInstanceNameWaitLabel;
        public System.Windows.Forms.ComboBox perfCounterInstanceNameDdl;
        private System.Windows.Forms.Label perfCounterInstanceNameLabel;
        private System.Windows.Forms.Label perfCounterTestData;
        private System.Windows.Forms.Label perfCounterTestDataLabel;
        private System.Windows.Forms.Button perfCounterTestBtn;
        private System.Windows.Forms.Timer testMonitorUpdate;
        private System.Windows.Forms.Label perfCounterTestDataType;
        private System.Windows.Forms.Label perfCounterTestDataTypeLabel;
        private System.Windows.Forms.Label perfCounterTestDataStatus;
        public System.Windows.Forms.TextBox perfCounterTestDataThresholdWarningTextBox;
        private System.Windows.Forms.Label perfCounterTestDataStatusLabel;
        private System.Windows.Forms.Label perfCounterTestDataThresholdWarningLabel;
        private System.Windows.Forms.TextBox perfCounterTestDataHelpText;
        private System.Windows.Forms.Label perfCounterTestDataHelpLabel;
        private System.Windows.Forms.Label perfCounterTestDataThresholdBreachLabel;
        public System.Windows.Forms.TextBox perfCounterTestDataThresholdBreachTextBox;
        private System.Windows.Forms.GroupBox addServerAlertConfiguration;
        public System.Windows.Forms.TextBox addServerAlertEmailTextBox;
        public System.Windows.Forms.TextBox addServerAlertSmsTextBox;
        public System.Windows.Forms.CheckBox addServerAlertEmailOption;
        public System.Windows.Forms.CheckBox addServerAlertSmsOption;
        private System.Windows.Forms.GroupBox eventMonitorGroupBox;
        private System.Windows.Forms.Label perfCounterTestDataUpdateFreqLabel;
        public System.Windows.Forms.TextBox perfCounterTestDataUpdateFreqTextBox;
        private System.Windows.Forms.Label eventMonitorEventsLogTypeDdlLabel;
        public System.Windows.Forms.ComboBox eventMonitorEventsLogTypeDdl;
        private System.Windows.Forms.ListView eventMonitorEventsLogsFilteredListView;
        private System.Windows.Forms.ColumnHeader eventMonitorEventsLogsFilterColumnCategory;
        private System.Windows.Forms.ColumnHeader eventMonitorEventsLogsFilterColumnEntryType;
        private System.Windows.Forms.ColumnHeader eventMonitorEventsLogsFilterColumnSource;
        private System.Windows.Forms.ColumnHeader eventMonitorEventsLogsFilterColumnMessage;
        private System.Windows.Forms.Label eventMonitorEventsLogTypeDdlWaitLabel;
        private System.Windows.Forms.Label eventMonitorEventsLogTypeDdlErrorLabel;
        private System.Windows.Forms.ColumnHeader eventMonitorEventsLogsFilterColumnEventTime;
        private System.Windows.Forms.Panel eventMonitorEntryTypeFilterPanel;
        public System.Windows.Forms.CheckBox eventMonitorEntryTypeFilterCbWarning;
        public System.Windows.Forms.CheckBox eventMonitorEntryTypeFilterCbSuccessAudit;
        public System.Windows.Forms.CheckBox eventMonitorEntryTypeFilterCbInformation;
        public System.Windows.Forms.CheckBox eventMonitorEntryTypeFilterCbFailureAudit;
        public System.Windows.Forms.CheckBox eventMonitorEntryTypeFilterCbError;
        private System.Windows.Forms.Button eventMonitorEntryTypeFilterBtn;
        private System.Windows.Forms.Button eventMonitorSourceFilterBtn;
        private System.Windows.Forms.Label eventMonitorSourceFilterSourceNameLabel;
        public System.Windows.Forms.TextBox eventMonitorSourceFilterTextBox;
        public System.Windows.Forms.CheckBox eventMonitorClearLogCb;
        private System.Windows.Forms.ListView servicesServiceListView;
        private System.Windows.Forms.ColumnHeader servicesServiceListViewServiceNameCol;
        private System.Windows.Forms.ColumnHeader servicesServiceListViewStatusCol;
        private System.Windows.Forms.Label servicesServiceLabel;
        public System.Windows.Forms.ListView servicesPickedServicesListView;
        private System.Windows.Forms.Button servicesPickedServicesClearBtn;
        private System.Windows.Forms.ColumnHeader servicesPickedServiceListViewServiceNameCol;
        private System.Windows.Forms.Label servicesPickedServicesLabel;
        private System.Windows.Forms.GroupBox servicesServiceGroupBox;
        private System.Windows.Forms.TabPage commonBaseTab;
        private System.Windows.Forms.TabPage basicBaseTab;
        public System.Windows.Forms.GroupBox basicMonitorFtpGroupBox;
        public System.Windows.Forms.GroupBox basicMonitorPingGroupBox;
        private System.Windows.Forms.Label basicMonitorIpResult;
        private System.Windows.Forms.Label basicMonitorIpResultLabel;
        private System.Windows.Forms.Button basicMonitorIpCheckBtn;
        public System.Windows.Forms.TextBox basicMonitorIpTextBox;
        private System.Windows.Forms.Label basicMonitorIpLabel;
        public System.Windows.Forms.GroupBox basicMonitorHttpGroupBox;
        public System.Windows.Forms.MaskedTextBox basicMonitorHttpPathPortTextBox;
        private System.Windows.Forms.Label basicMonitorHttpPathPortLabel;
        private System.Windows.Forms.Label basicMonitorHttpPathCheckResultText;
        private System.Windows.Forms.Label basicMonitorHttpPathCheckResultLabel;
        private System.Windows.Forms.Button basicMonitorHttpPathCheckBtn;
        public System.Windows.Forms.TextBox basicMonitorHttpPathTextBox;
        private System.Windows.Forms.Label basicMonitorHttpPathLabel;
        private System.Windows.Forms.TabPage advancedBaseTab;
        private System.Windows.Forms.TabControl addServerBaseTabControl;
        private System.Windows.Forms.GroupBox addServerValidIpGroupBox2;
        public System.Windows.Forms.TextBox addServerValidIpMonitorName2;
        private System.Windows.Forms.Label addServerValidIpMonitorNameLabel2;
        private System.Windows.Forms.Label addServerValidIpIpLabel2;
        private System.Windows.Forms.Label addServerValidIpLabel2;
        private System.Windows.Forms.Button addServerValidIpPingBtn2;
        public System.Windows.Forms.TextBox addServerValidIpIpTextBox2;
        private Label eventMonitorTestDataUpdateFreqLabel;
        public TextBox eventMonitorTestDataUpdateFreqTextBox;
        private Label servicesPickedServicesTestDataUpdateFreqLabel;
        public TextBox servicesPickedServicesTestDataUpdateFreqTextBox;
        private Label commonMonitorTestDataUpdateFreqLabel;
        public TextBox commonMonitorTestDataUpdateFreqTextBox;
        private Label basicMonitorTestDataUpdateFreqLabel;
        public TextBox basicMonitorTestDataUpdateFreqTextBox;
        public CheckBox basicMonitorFtpMonitorSelect;
        public CheckBox basicMonitorPingMonitorSelect;
        public CheckBox basicMonitorHttpMonitorSelect;
        public MaskedTextBox basicMonitorFtpPathPortTextBox;
        public TextBox basicMonitorFtpPassTextBox;
        public TextBox basicMonitorFtpUserTextBox;
        public TextBox basicMonitorFtpPathTextBox;
        private Label basicMonitorFtpPassLabel;
        private Label basicMonitorFtpPathPortLabel;
        private Label basicMonitorFtpUserLabel;
        private Label basicMonitorFtpPathLabel;
        private Label basicMonitorFtpPathCheckResultText;
        private Button basicMonitorFtpPathCheckBtn;
        private Label basicMonitorFtpPathCheckResultLabel;
        public CheckBox basicMonitorFtpUseSslCheckBox;
        private ToolTip addServerToolTip;
        private ColumnHeader servicesPickedServiceListViewServiceStatusCol;
        public TextBox addServerValidIpMonitorName3;
        private Label addServerValidIpMonitorNameLabel3;
        public CheckBox servicesAutomaticRestartServiceCheckBox;
        public GroupBox commonHddGroupBox;
        public ComboBox commonHddDriveLetterDdl;
        public CheckBox commonHddSelected;
        private Label commonMonitorTestDataUpdateResult;
        private Label commonMonitorTestDataUpdateResultLabel;
        public TextBox commonHddCriticalTextBox;
        public TextBox commonHddWarningTextBox;
        private Button commonMonitorCheckBtn;
        private Label commonHddDriveLetterLabel;
        private Label commonHddCriticalPctLabel;
        private Label commonHddWarningPctLabel;
        private Label commonHddCriticalLabel;
        private Label commonHddWarningLabel;
        public GroupBox commonMemoryGroupBox;
        public CheckBox commonMemorySelected;
        public TextBox perfCounterTestDataThresholdPanicTextBox;
        private Label perfCounterTestDataThresholdPanicLabel;
        public CheckBox perfCounterTestDataThresholdLessThanCheckBox;
        public GroupBox commonSwapFileGroupBox;
        public CheckBox commonSwapFileSelected;
        public GroupBox commonCpuGroupBox;
        public CheckBox commonCpuSelected;
        public GroupBox commonProcessGroupBox;
        public CheckBox commonProcessSelected;
        public GroupBox commonServiceGroupBox;
        public CheckBox commonServiceSelected;
        public GroupBox commonChooserGroupBox;
        private Label commonFourLabel;
        private GroupBox commonGeneralSetupGroupBox;
        private Label commonThreeLabel;
        private Label commonTwoLabel;
        private Label commonOneLabel;
        public ComboBox commonMemoryUsageTypeDdl;
        private Label commonMemoryWarningLabel;
        public TextBox commonMemoryCriticalTextBox;
        private Label commonMemoryCriticalLabel;
        private Label commonMemoryUsageTypeLabel;
        public TextBox commonMemoryWarningTextBox;
        private Label commonMemoryWarningPctLabel;
        private Label commonMemoryCriticalPctLabel;
        public ComboBox commonServiceServicesChoiceDdl;
        private Label commonServiceServicesChoiceLabel;
        public ComboBox commonCpuUsageTypeDdl;
        private Label commonCpuWarningLabel;
        public TextBox commonCpuCriticalTextBox;
        private Label commonCpuCriticalPctLabel;
        private Label commonCpuCriticalLabel;
        private Label commonCpuUsageTypeLabel;
        private Label commonCpuWarningPctLabel;
        public TextBox commonCpuWarningTextBox;
        private Label commonSwapFileWarningLabel;
        public TextBox commonSwapFileCriticalTextBox;
        public TextBox commonSwapFileWarningTextBox;
        private Label commonSwapFileCriticalPctLabel;
        private Label commonSwapFileWarningPctLabel;
        private Label commonSwapFileCriticalLabel;
        public ComboBox commonProcessStateProcessDd;
        private Label commonProcessStateProcessLabel;
        public ComboBox commonSwapFileUsageTypeDdl;
        private Label commonSwapFileUsageTypeLabel;
        private Label commonMonitorTestDataUpdateResultStatus;
        public ComboBox commonProcessStateUsageTypeDdl;
        private Label commonProcessStateUsageTypeLabel;
        private Label commonProcessStateWarningLabel;
        public TextBox commonProcessStateCriticalTextBox;
        private Label commonProcessStateWarningPctLabel;
        public TextBox commonProcessStateWarningTextBox;
        private Label commonProcessStateCriticalPctLabel;
        private Label commonProcessStateCriticalLabel;
        private Label commonHddGtLtLabel;
        private Label commonMemoryGtLtLabel;
        private Label commonCpuGtLtLabel;
        private Label commonSwapFileGtLtLabel;
        private Label commonProcessStateGtLtLabel;
    }
}