using System.Drawing;
using System.Windows.Forms;

namespace RemoteMon
{
    partial class NetworkMonitor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Network Computers");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serverBrowser = new System.Windows.Forms.TreeView();
            this.serverBrowserContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshServersToolStripServerBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.automatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualprovideIpRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSelServerToolStripServerBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perfMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asServiceMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asEventMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asBasicMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asPerfMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarLabelServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarLabelMonitoringStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pushConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshServersToolStripMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkMonitorToolsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationContainer = new System.Windows.Forms.SplitContainer();
            this.serverMonitorList = new System.Windows.Forms.DataGridView();
            this.serverMonitorListMachineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverMonitorListFriendlyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverMonitorListMonitorTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverMonitorListStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverMonitorListStatusValueColumn = new RemoteMon.DataGridViewGraphColumn();
            this.serverMonitorListInfoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertUpdating = new System.Windows.Forms.NotifyIcon(this.components);
            this.logPane = new System.Windows.Forms.TextBox();
            this.testMonitorUpdate = new System.Windows.Forms.Timer(this.components);
            this.networkMonitorBaseContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewGraphColumn1 = new RemoteMon.DataGridViewGraphColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveConfigurationFd = new System.Windows.Forms.SaveFileDialog();
            this.loadConfigurationFd = new System.Windows.Forms.OpenFileDialog();
            this.serverBrowserContextMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.configurationContainer.Panel1.SuspendLayout();
            this.configurationContainer.Panel2.SuspendLayout();
            this.configurationContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverMonitorList)).BeginInit();
            this.networkMonitorBaseContainer.Panel1.SuspendLayout();
            this.networkMonitorBaseContainer.Panel2.SuspendLayout();
            this.networkMonitorBaseContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverBrowser
            // 
            this.serverBrowser.ContextMenuStrip = this.serverBrowserContextMenu;
            this.serverBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverBrowser.Location = new System.Drawing.Point(0, 0);
            this.serverBrowser.Name = "serverBrowser";
            treeNode1.Name = "ServersNode";
            treeNode1.Text = "Network Computers";
            treeNode1.ToolTipText = "List of Computers on Network";
            this.serverBrowser.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.serverBrowser.ShowNodeToolTips = true;
            this.serverBrowser.Size = new System.Drawing.Size(199, 624);
            this.serverBrowser.TabIndex = 0;
            this.serverBrowser.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.ServerBrowserNodeMouseHover);
            this.serverBrowser.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ServerBrowserNodeMouseClick);
            this.serverBrowser.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ServerBrowserNodeMouseDoubleClick);
            this.serverBrowser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerBrowserKeyUp);
            // 
            // serverBrowserContextMenu
            // 
            this.serverBrowserContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshServersToolStripServerBrowser,
            this.addSelServerToolStripServerBrowser,
            this.addNewServerToolStripMenuItem});
            this.serverBrowserContextMenu.Name = "serverBrowserContextMenu";
            this.serverBrowserContextMenu.Size = new System.Drawing.Size(179, 70);
            this.serverBrowserContextMenu.Text = "Options";
            // 
            // refreshServersToolStripServerBrowser
            // 
            this.refreshServersToolStripServerBrowser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automatedToolStripMenuItem,
            this.manualprovideIpRangeToolStripMenuItem});
            this.refreshServersToolStripServerBrowser.Name = "refreshServersToolStripServerBrowser";
            this.refreshServersToolStripServerBrowser.Size = new System.Drawing.Size(178, 22);
            this.refreshServersToolStripServerBrowser.Text = "Refresh Servers";
            // 
            // automatedToolStripMenuItem
            // 
            this.automatedToolStripMenuItem.Name = "automatedToolStripMenuItem";
            this.automatedToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.automatedToolStripMenuItem.Text = "Automated (not exhaustive)";
            this.automatedToolStripMenuItem.Click += new System.EventHandler(this.AutomatedToolStripMenuItemClick);
            // 
            // manualprovideIpRangeToolStripMenuItem
            // 
            this.manualprovideIpRangeToolStripMenuItem.Name = "manualprovideIpRangeToolStripMenuItem";
            this.manualprovideIpRangeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.manualprovideIpRangeToolStripMenuItem.Text = "Manual (provide IP range)";
            this.manualprovideIpRangeToolStripMenuItem.Click += new System.EventHandler(this.ManualprovideIpRangeToolStripMenuItemClick);
            // 
            // addSelServerToolStripServerBrowser
            // 
            this.addSelServerToolStripServerBrowser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceMonitorToolStripMenuItem,
            this.eventMonitorToolStripMenuItem,
            this.basicMonitorToolStripMenuItem,
            this.perfMonitorToolStripMenuItem});
            this.addSelServerToolStripServerBrowser.Name = "addSelServerToolStripServerBrowser";
            this.addSelServerToolStripServerBrowser.Size = new System.Drawing.Size(178, 22);
            this.addSelServerToolStripServerBrowser.Text = "Add Selected Server";
            this.addSelServerToolStripServerBrowser.Click += new System.EventHandler(this.AddSelServerToolStripAddServerClick);
            // 
            // serviceMonitorToolStripMenuItem
            // 
            this.serviceMonitorToolStripMenuItem.Name = "serviceMonitorToolStripMenuItem";
            this.serviceMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.serviceMonitorToolStripMenuItem.Text = "As Service Monitor";
            this.serviceMonitorToolStripMenuItem.Click += new System.EventHandler(this.ServiceMonitorToolStripMenuItemClick);
            // 
            // eventMonitorToolStripMenuItem
            // 
            this.eventMonitorToolStripMenuItem.Name = "eventMonitorToolStripMenuItem";
            this.eventMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.eventMonitorToolStripMenuItem.Text = "As Event Monitor";
            this.eventMonitorToolStripMenuItem.Click += new System.EventHandler(this.EventMonitorToolStripMenuItemClick);
            // 
            // basicMonitorToolStripMenuItem
            // 
            this.basicMonitorToolStripMenuItem.Name = "basicMonitorToolStripMenuItem";
            this.basicMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.basicMonitorToolStripMenuItem.Text = "As Basic Monitor";
            this.basicMonitorToolStripMenuItem.Click += new System.EventHandler(this.BasicMonitorToolStripMenuItemClick);
            // 
            // perfMonitorToolStripMenuItem
            // 
            this.perfMonitorToolStripMenuItem.Name = "perfMonitorToolStripMenuItem";
            this.perfMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.perfMonitorToolStripMenuItem.Text = "As Perf Monitor";
            this.perfMonitorToolStripMenuItem.Click += new System.EventHandler(this.PerfMonitorToolStripMenuItemClick);
            // 
            // addNewServerToolStripMenuItem
            // 
            this.addNewServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asServiceMonitorToolStripMenuItem,
            this.asEventMonitorToolStripMenuItem,
            this.asBasicMonitorToolStripMenuItem,
            this.asPerfMonitorToolStripMenuItem});
            this.addNewServerToolStripMenuItem.Name = "addNewServerToolStripMenuItem";
            this.addNewServerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.addNewServerToolStripMenuItem.Text = "Add New Server";
            this.addNewServerToolStripMenuItem.Click += new System.EventHandler(this.AddNewServerToolStripMenuItemClick);
            // 
            // asServiceMonitorToolStripMenuItem
            // 
            this.asServiceMonitorToolStripMenuItem.Name = "asServiceMonitorToolStripMenuItem";
            this.asServiceMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.asServiceMonitorToolStripMenuItem.Text = "As Service Monitor";
            this.asServiceMonitorToolStripMenuItem.Click += new System.EventHandler(this.AsServiceMonitorToolStripMenuItemClick);
            // 
            // asEventMonitorToolStripMenuItem
            // 
            this.asEventMonitorToolStripMenuItem.Name = "asEventMonitorToolStripMenuItem";
            this.asEventMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.asEventMonitorToolStripMenuItem.Text = "As Event Monitor";
            this.asEventMonitorToolStripMenuItem.Click += new System.EventHandler(this.AsEventMonitorToolStripMenuItemClick);
            // 
            // asBasicMonitorToolStripMenuItem
            // 
            this.asBasicMonitorToolStripMenuItem.Name = "asBasicMonitorToolStripMenuItem";
            this.asBasicMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.asBasicMonitorToolStripMenuItem.Text = "As Basic Monitor";
            this.asBasicMonitorToolStripMenuItem.Click += new System.EventHandler(this.AsBasicMonitorToolStripMenuItemClick);
            // 
            // asPerfMonitorToolStripMenuItem
            // 
            this.asPerfMonitorToolStripMenuItem.Name = "asPerfMonitorToolStripMenuItem";
            this.asPerfMonitorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.asPerfMonitorToolStripMenuItem.Text = "As Perf Monitor";
            this.asPerfMonitorToolStripMenuItem.Click += new System.EventHandler(this.AsPerfMonitorToolStripMenuItemClick);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel,
            this.statusBarLabelServiceStatus,
            this.statusBarLabelMonitoringStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 681);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1099, 24);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "Status Strip";
            this.statusBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerBrowserKeyUp);
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // statusBarLabelServiceStatus
            // 
            this.statusBarLabelServiceStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statusBarLabelServiceStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBarLabelServiceStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarLabelServiceStatus.Name = "statusBarLabelServiceStatus";
            this.statusBarLabelServiceStatus.Size = new System.Drawing.Size(925, 19);
            this.statusBarLabelServiceStatus.Spring = true;
            this.statusBarLabelServiceStatus.Text = "Service Status: Unknown";
            this.statusBarLabelServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBarLabelMonitoringStatus
            // 
            this.statusBarLabelMonitoringStatus.Name = "statusBarLabelMonitoringStatus";
            this.statusBarLabelMonitoringStatus.Size = new System.Drawing.Size(159, 19);
            this.statusBarLabelMonitoringStatus.Text = "Monitoring Status: Unknown";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1099, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "Menu";
            this.mainMenu.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerBrowserKeyUp);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigurationToolStripMenuItem,
            this.saveConfigurationToolStripMenuItem,
            this.pushConfigurationToolStripMenuItem,
            this.getConfigurationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadConfigurationToolStripMenuItem.Text = "Load Configuration";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler(this.LoadConfigurationToolStripMenuItemClick);
            // 
            // saveConfigurationToolStripMenuItem
            // 
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveConfigurationToolStripMenuItem.Text = "Save Configuration";
            this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler(this.SaveConfigurationToolStripMenuItemClick);
            // 
            // pushConfigurationToolStripMenuItem
            // 
            this.pushConfigurationToolStripMenuItem.Name = "pushConfigurationToolStripMenuItem";
            this.pushConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pushConfigurationToolStripMenuItem.Text = "Push Configuration";
            this.pushConfigurationToolStripMenuItem.Click += new System.EventHandler(this.PushConfigurationToolStripMenuItemClick);
            // 
            // getConfigurationToolStripMenuItem
            // 
            this.getConfigurationToolStripMenuItem.Name = "getConfigurationToolStripMenuItem";
            this.getConfigurationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.getConfigurationToolStripMenuItem.Text = "Get Configuration";
            this.getConfigurationToolStripMenuItem.Click += new System.EventHandler(this.GetConfigurationToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshServersToolStripMainMenu,
            this.cancelRefreshToolStripMenuItem,
            this.clearLogWindowToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // refreshServersToolStripMainMenu
            // 
            this.refreshServersToolStripMainMenu.Name = "refreshServersToolStripMainMenu";
            this.refreshServersToolStripMainMenu.Size = new System.Drawing.Size(234, 22);
            this.refreshServersToolStripMainMenu.Text = "Automated Server Refresh (F5)";
            this.refreshServersToolStripMainMenu.Click += new System.EventHandler(this.RefreshServersToolStripMainMenuClick);
            // 
            // cancelRefreshToolStripMenuItem
            // 
            this.cancelRefreshToolStripMenuItem.Name = "cancelRefreshToolStripMenuItem";
            this.cancelRefreshToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.cancelRefreshToolStripMenuItem.Text = "Cancel Automated Refresh";
            this.cancelRefreshToolStripMenuItem.Click += new System.EventHandler(this.CancelRefreshToolStripMenuItemClick);
            // 
            // clearLogWindowToolStripMenuItem
            // 
            this.clearLogWindowToolStripMenuItem.Name = "clearLogWindowToolStripMenuItem";
            this.clearLogWindowToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.clearLogWindowToolStripMenuItem.Text = "Clear Log Window";
            this.clearLogWindowToolStripMenuItem.Click += new System.EventHandler(this.ClearLogWindowToolStripMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolStripMenuItem,
            this.networkMonitorToolsSeparator,
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // addServerToolStripMenuItem
            // 
            this.addServerToolStripMenuItem.Name = "addServerToolStripMenuItem";
            this.addServerToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addServerToolStripMenuItem.Text = "Add Server";
            // 
            // networkMonitorToolsSeparator
            // 
            this.networkMonitorToolsSeparator.Name = "networkMonitorToolsSeparator";
            this.networkMonitorToolsSeparator.Size = new System.Drawing.Size(128, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.versionToolStripMenuItem.Text = "Version";
            // 
            // configurationContainer
            // 
            this.configurationContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.configurationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationContainer.Location = new System.Drawing.Point(0, 0);
            this.configurationContainer.Name = "configurationContainer";
            // 
            // configurationContainer.Panel1
            // 
            this.configurationContainer.Panel1.Controls.Add(this.serverBrowser);
            // 
            // configurationContainer.Panel2
            // 
            this.configurationContainer.Panel2.Controls.Add(this.serverMonitorList);
            this.configurationContainer.Size = new System.Drawing.Size(1099, 628);
            this.configurationContainer.SplitterDistance = 203;
            this.configurationContainer.TabIndex = 4;
            // 
            // serverMonitorList
            // 
            this.serverMonitorList.AllowUserToAddRows = false;
            this.serverMonitorList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.serverMonitorList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.serverMonitorList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.serverMonitorList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverMonitorList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.serverMonitorList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.serverMonitorList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.serverMonitorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.serverMonitorList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverMonitorListMachineColumn,
            this.serverMonitorListFriendlyName,
            this.serverMonitorListMonitorTypeColumn,
            this.serverMonitorListStatusColumn,
            this.serverMonitorListStatusValueColumn,
            this.serverMonitorListInfoColumn});
            this.serverMonitorList.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.serverMonitorList.DefaultCellStyle = dataGridViewCellStyle3;
            this.serverMonitorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverMonitorList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.serverMonitorList.Location = new System.Drawing.Point(0, 0);
            this.serverMonitorList.MultiSelect = false;
            this.serverMonitorList.Name = "serverMonitorList";
            this.serverMonitorList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.serverMonitorList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.serverMonitorList.RowTemplate.DividerHeight = 1;
            this.serverMonitorList.RowTemplate.Height = 25;
            this.serverMonitorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.serverMonitorList.Size = new System.Drawing.Size(888, 624);
            this.serverMonitorList.TabIndex = 0;
            this.serverMonitorList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ServerMonitorListCellDoubleClick);
            this.serverMonitorList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.ServerMonitorListDataError);
            this.serverMonitorList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ServerMonitorListUserDeletingRow);
            this.serverMonitorList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerBrowserKeyUp);
            // 
            // serverMonitorListMachineColumn
            // 
            this.serverMonitorListMachineColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverMonitorListMachineColumn.DividerWidth = 1;
            this.serverMonitorListMachineColumn.HeaderText = "Machine";
            this.serverMonitorListMachineColumn.Name = "serverMonitorListMachineColumn";
            this.serverMonitorListMachineColumn.ReadOnly = true;
            this.serverMonitorListMachineColumn.ToolTipText = "IP, Name, or Url";
            this.serverMonitorListMachineColumn.Width = 74;
            // 
            // serverMonitorListFriendlyName
            // 
            this.serverMonitorListFriendlyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverMonitorListFriendlyName.DividerWidth = 1;
            this.serverMonitorListFriendlyName.HeaderText = "Friendly Name";
            this.serverMonitorListFriendlyName.Name = "serverMonitorListFriendlyName";
            this.serverMonitorListFriendlyName.ReadOnly = true;
            this.serverMonitorListFriendlyName.ToolTipText = "Friendly Name for the monitor";
            // 
            // serverMonitorListMonitorTypeColumn
            // 
            this.serverMonitorListMonitorTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverMonitorListMonitorTypeColumn.DividerWidth = 1;
            this.serverMonitorListMonitorTypeColumn.HeaderText = "Monitor Type";
            this.serverMonitorListMonitorTypeColumn.Name = "serverMonitorListMonitorTypeColumn";
            this.serverMonitorListMonitorTypeColumn.ReadOnly = true;
            this.serverMonitorListMonitorTypeColumn.Width = 95;
            // 
            // serverMonitorListStatusColumn
            // 
            this.serverMonitorListStatusColumn.DividerWidth = 1;
            this.serverMonitorListStatusColumn.HeaderText = "Status";
            this.serverMonitorListStatusColumn.Name = "serverMonitorListStatusColumn";
            this.serverMonitorListStatusColumn.ReadOnly = true;
            this.serverMonitorListStatusColumn.ToolTipText = "Current status of this monitor.";
            this.serverMonitorListStatusColumn.Width = 63;
            // 
            // serverMonitorListStatusValueColumn
            // 
            this.serverMonitorListStatusValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverMonitorListStatusValueColumn.DividerWidth = 1;
            this.serverMonitorListStatusValueColumn.HeaderText = "Current Value";
            this.serverMonitorListStatusValueColumn.MinimumWidth = 85;
            this.serverMonitorListStatusValueColumn.Name = "serverMonitorListStatusValueColumn";
            this.serverMonitorListStatusValueColumn.ReadOnly = true;
            this.serverMonitorListStatusValueColumn.ToolTipText = "Current Value of the monitor";
            this.serverMonitorListStatusValueColumn.Width = 85;
            // 
            // serverMonitorListInfoColumn
            // 
            this.serverMonitorListInfoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverMonitorListInfoColumn.DividerWidth = 1;
            this.serverMonitorListInfoColumn.HeaderText = "Info";
            this.serverMonitorListInfoColumn.Name = "serverMonitorListInfoColumn";
            this.serverMonitorListInfoColumn.ReadOnly = true;
            this.serverMonitorListInfoColumn.ToolTipText = "Current information on the status of monitor";
            this.serverMonitorListInfoColumn.Width = 51;
            // 
            // alertUpdating
            // 
            this.alertUpdating.Text = "notifyIcon1";
            this.alertUpdating.Visible = true;
            // 
            // logPane
            // 
            this.logPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPane.Location = new System.Drawing.Point(0, 0);
            this.logPane.MaxLength = 16000000;
            this.logPane.Multiline = true;
            this.logPane.Name = "logPane";
            this.logPane.ReadOnly = true;
            this.logPane.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logPane.Size = new System.Drawing.Size(1099, 25);
            this.logPane.TabIndex = 5;
            this.logPane.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerBrowserKeyUp);
            this.logPane.MouseEnter += new System.EventHandler(this.LogPaneMouseEnter);
            this.logPane.MouseLeave += new System.EventHandler(this.LogPaneMouseLeave);
            // 
            // testMonitorUpdate
            // 
            this.testMonitorUpdate.Enabled = true;
            this.testMonitorUpdate.Interval = 1000;
            // 
            // networkMonitorBaseContainer
            // 
            this.networkMonitorBaseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkMonitorBaseContainer.Location = new System.Drawing.Point(0, 24);
            this.networkMonitorBaseContainer.Name = "networkMonitorBaseContainer";
            this.networkMonitorBaseContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // networkMonitorBaseContainer.Panel1
            // 
            this.networkMonitorBaseContainer.Panel1.Controls.Add(this.configurationContainer);
            // 
            // networkMonitorBaseContainer.Panel2
            // 
            this.networkMonitorBaseContainer.Panel2.Controls.Add(this.logPane);
            this.networkMonitorBaseContainer.Size = new System.Drawing.Size(1099, 657);
            this.networkMonitorBaseContainer.SplitterDistance = 628;
            this.networkMonitorBaseContainer.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DividerWidth = 1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Machine";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "IP, Name, or Url";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DividerWidth = 1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Friendly Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "Friendly Name for the monitor";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DividerWidth = 1;
            this.dataGridViewTextBoxColumn3.HeaderText = "Monitor Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DividerWidth = 1;
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Current status of this monitor.";
            this.dataGridViewTextBoxColumn4.Width = 63;
            // 
            // dataGridViewGraphColumn1
            // 
            this.dataGridViewGraphColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewGraphColumn1.DividerWidth = 1;
            this.dataGridViewGraphColumn1.HeaderText = "Current Value";
            this.dataGridViewGraphColumn1.MinimumWidth = 85;
            this.dataGridViewGraphColumn1.Name = "dataGridViewGraphColumn1";
            this.dataGridViewGraphColumn1.ReadOnly = true;
            this.dataGridViewGraphColumn1.ToolTipText = "Current Value of the monitor";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DividerWidth = 1;
            this.dataGridViewTextBoxColumn5.HeaderText = "Info";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ToolTipText = "Current information on the status of monitor";
            // 
            // saveConfigurationFd
            // 
            this.saveConfigurationFd.DefaultExt = "xml";
            this.saveConfigurationFd.FileName = "configuration.xml";
            // 
            // loadConfigurationFd
            // 
            this.loadConfigurationFd.DefaultExt = "xml";
            this.loadConfigurationFd.FileName = "configuration.xml";
            this.loadConfigurationFd.Filter = "\"Configuration Files|*.xml\"";
            // 
            // NetworkMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 705);
            this.Controls.Add(this.networkMonitorBaseContainer);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "NetworkMonitor";
            this.Text = "Network Monitor Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkMonitorFormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NetworkMonitorKeyUp);
            this.serverBrowserContextMenu.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.configurationContainer.Panel1.ResumeLayout(false);
            this.configurationContainer.Panel2.ResumeLayout(false);
            this.configurationContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverMonitorList)).EndInit();
            this.networkMonitorBaseContainer.Panel1.ResumeLayout(false);
            this.networkMonitorBaseContainer.Panel2.ResumeLayout(false);
            this.networkMonitorBaseContainer.Panel2.PerformLayout();
            this.networkMonitorBaseContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView serverBrowser;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.SplitContainer configurationContainer;
        private System.Windows.Forms.ContextMenuStrip serverBrowserContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addSelServerToolStripServerBrowser;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.NotifyIcon alertUpdating;
        private System.Windows.Forms.TextBox logPane;
        private System.Windows.Forms.ToolStripMenuItem refreshServersToolStripServerBrowser;
        private System.Windows.Forms.ToolStripMenuItem refreshServersToolStripMainMenu;
        private System.Windows.Forms.ToolStripMenuItem clearLogWindowToolStripMenuItem;
        private System.Windows.Forms.DataGridView serverMonitorList;
        private System.Windows.Forms.ToolStripMenuItem serviceMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perfMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asPerfMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asServiceMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asEventMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asBasicMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualprovideIpRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelRefreshToolStripMenuItem;
        private System.Windows.Forms.Timer testMonitorUpdate;
        private System.Windows.Forms.ToolStripSeparator networkMonitorToolsSeparator;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabelServiceStatus;
        private SplitContainer networkMonitorBaseContainer;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewGraphColumn dataGridViewGraphColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn serverMonitorListMachineColumn;
        private DataGridViewTextBoxColumn serverMonitorListFriendlyName;
        private DataGridViewTextBoxColumn serverMonitorListMonitorTypeColumn;
        private DataGridViewTextBoxColumn serverMonitorListStatusColumn;
        private DataGridViewGraphColumn serverMonitorListStatusValueColumn;
        private DataGridViewTextBoxColumn serverMonitorListInfoColumn;
        private SaveFileDialog saveConfigurationFd;
        private ToolStripMenuItem pushConfigurationToolStripMenuItem;
        private ToolStripStatusLabel statusBarLabelMonitoringStatus;
        private OpenFileDialog loadConfigurationFd;
        private ToolStripMenuItem getConfigurationToolStripMenuItem;

    }
}

