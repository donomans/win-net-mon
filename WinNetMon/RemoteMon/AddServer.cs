using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class AddServer
    {
        //private Boolean _NameBoolean = false;
        private static Boolean ipBoolean;
        private static String iporhostname = String.Empty;
        //private String _hostName = String.Empty;
        private String _name = String.Empty;
        private String _toLog = String.Empty;
        private FullMonitorType _returnValue = FullMonitorType.None;
        private static Boolean alreadyPopulated = false;


        public AddServer(String monitorName, String remoteIpOrHostName, FullMonitorType tab,
                         MonitorBaseType baseTab, IEnumerable<Alert> defaultAlerts)
        {
            InitializeComponent();
            perfCounterPCTypeDdl.Visible = false;
            perfCounterCategoryWaitLabel.Visible = true;
            perfCounterCounterNameDdl.Visible = false;
            perfCounterCounterNameWaitLabel.Visible = true;
            perfCounterInstanceNameDdl.Visible = false;
            perfCounterInstanceNameWaitLabel.Visible = true;

            foreach(Alert c in defaultAlerts)
            {
                switch(c.Type)
                {
                    case AlertType.Email:
                        addServerAlertEmailTextBox.Text = c.Info;
                        break;
                    case AlertType.Phone:
                        addServerAlertSmsTextBox.Text = c.Info;
                        break;
                }
                    
            }
            //NOTE:MonitorName should be the hostname if for some reason HostName isn't populated
            //_hostName = (hostName == "" ? Environment.MachineName : (hostName != "" ? hostName : monitorName));
            IpOrHostName = remoteIpOrHostName == "" ? Environment.MachineName : remoteIpOrHostName;
            FriendlyName = monitorName == "" ? Environment.MachineName + " " + tab : monitorName;

            switch (tab)
            {
                #region cases

                default:
                case FullMonitorType.None:                
                case FullMonitorType.PerformanceCounter:
                    _perfCounterPopulation = new Thread(GetPerfCounterTypes) {Name = "PerformanceCounter Start"};
                    _perfCounterPopulation.Start();
                    addServerBaseTabControl.SelectedIndex = (int) baseTab;
                    addServerTabControl.SelectedIndex = (int) tab;
                    break;
                case FullMonitorType.Service:
                    _serviceMonitorPopulation = new Thread(GetServices) {Name = "ServiceMonitor Start"};
                    _serviceMonitorPopulation.Start();
                    addServerBaseTabControl.SelectedIndex = (int) baseTab;
                    addServerTabControl.SelectedIndex = (int) tab;
                    break;
                case FullMonitorType.EventLog:
                    _eventMonitorPopulation = new Thread(GetEvents) {Name = "EventMonitor Start"};
                    _eventMonitorPopulation.Start();
                    addServerBaseTabControl.SelectedIndex = (int) baseTab;
                    addServerTabControl.SelectedIndex = (int) tab;
                    break;
                case FullMonitorType.Basic:
                    //_basicMonitorPopulation = new Thread(GetBasics) {Name = "BasicMonitor Start"};
                    //_basicMonitorPopulation.Start();
                    addServerBaseTabControl.SelectedIndex = (int) baseTab;
                    //addServerTabControl.SelectedIndex = (int) tab;
                    break;
                case FullMonitorType.Common:
                    //commonBaseTab.Focus();
                    //commonBaseTab.Select();
                    _commonMonitorPopulation = new Thread(GetCommon) {Name = "CommonMonitor Start"};
                    _commonMonitorPopulation.Start();
                    addServerBaseTabControl.SelectedIndex = (int) baseTab;
                    break;
                #endregion
            }

            ReturnValue = FullMonitorType.None;
        }
        public AddServer(String monitorName, String remoteIpOrHostName, FullMonitorType tab,
                         MonitorBaseType baseTab, IEnumerable<Alert> defaultAlerts, IMonitor repopulateMonitor)
        {
            InitializeComponent();
            perfCounterPCTypeDdl.Visible = false;
            perfCounterCategoryWaitLabel.Visible = true;
            perfCounterCounterNameDdl.Visible = false;
            perfCounterCounterNameWaitLabel.Visible = true;
            perfCounterInstanceNameDdl.Visible = false;
            perfCounterInstanceNameWaitLabel.Visible = true;

            foreach (Alert c in defaultAlerts)
            {
                switch (c.Type)
                {
                    case AlertType.Email:
                        addServerAlertEmailTextBox.Text = c.Info;
                        break;
                    case AlertType.Phone:
                        addServerAlertSmsTextBox.Text = c.Info;
                        break;
                }

            }
            //NOTE:MonitorName should be the hostname if for some reason HostName isn't populated
            //_hostName = (hostName == "" ? Environment.MachineName : (hostName != "" ? hostName : monitorName));
            IpOrHostName = remoteIpOrHostName == "" ? Environment.MachineName : remoteIpOrHostName;
            FriendlyName = monitorName == "" ? Environment.MachineName + " " + tab : monitorName;

            switch (repopulateMonitor.Type)
            {
                #region cases
                case FullMonitorType.PerformanceCounter:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    addServerTabControl.SelectedIndex = (int)tab;
                    if (repopulateMonitor.Common)
                    {
                        CommonRepopulate(repopulateMonitor);
                        ReturnValue = FullMonitorType.Common;
                    }
                    else
                    {
                        ReturnValue = repopulateMonitor.Type;
                        PerfCounterRepopulate((PfcMonitor)repopulateMonitor);
                    }
                    break;
                case FullMonitorType.Service:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    addServerTabControl.SelectedIndex = (int)tab;
                    if (repopulateMonitor.Common)
                    {
                        CommonRepopulate(repopulateMonitor);
                        ReturnValue = FullMonitorType.Common;
                    }
                    else
                    {
                        ReturnValue = repopulateMonitor.Type;
                        ServiceRepopulate((ServiceMonitor)repopulateMonitor);
                    }
                    break;
                case FullMonitorType.EventLog:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    addServerTabControl.SelectedIndex = (int)tab;
                    if (repopulateMonitor.Common)
                    {
                        CommonRepopulate(repopulateMonitor);
                        ReturnValue = FullMonitorType.Common;
                    }
                    else
                    {
                        ReturnValue = repopulateMonitor.Type;
                        EventRepopulate((EventMonitor) repopulateMonitor);
                    }
                    break;
                case FullMonitorType.Basic:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    BasicRepopulate((BasicMonitor)repopulateMonitor);
                    ReturnValue = repopulateMonitor.Type;
                    break;
                case FullMonitorType.Common:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    if (repopulateMonitor.Common)
                    {
                        CommonRepopulate(repopulateMonitor);
                        ReturnValue = FullMonitorType.Common;
                    }
                    else
                        ReturnValue = repopulateMonitor.Type;
                    break;
                case FullMonitorType.Wmi:
                    addServerBaseTabControl.SelectedIndex = (int)baseTab;
                    if (repopulateMonitor.Common)
                    {
                        CommonRepopulate(repopulateMonitor);
                        ReturnValue = FullMonitorType.Common;
                    }
                    else
                        ReturnValue = repopulateMonitor.Type;
                    break;
                default:
                case FullMonitorType.None:
                    Logger.Instance.Log(this.GetType(), LogType.Debug, "AddServer: Unable to match Monitor Type");
                    this.Close();
                    break;
                    #endregion
            }

        }

        public String FriendlyName
        {
            get { return _name; }
            private set
            {
                _name = value;
                addServerValidIpMonitorName.Text = value;
                addServerValidIpMonitorName2.Text = value;
                addServerValidIpMonitorName3.Text = value;
            }
        }

        public String IpOrHostName
        {
            get { return iporhostname; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    ipBoolean = true;
                    iporhostname = value;
                    addServerValidIpIpTextBox.Text = value;
                    addServerValidIpIpTextBox2.Text = value;
                    basicMonitorIpTextBox.Text = value;
                }
                else
                {
                    ipBoolean = false;
                    iporhostname = String.Empty;
                }
            }
        }

        public FullMonitorType ReturnValue
        {
            get { return _returnValue; }
            private set { _returnValue = value; }
        }

        public Boolean GetAlerts(out List<Alert> alerts)
        {
            alerts = new List<Alert>(2);
            if (addServerAlertEmailOption.CheckState == CheckState.Checked)
                alerts.Add(new EmailAlert { Info = addServerAlertEmailTextBox.Text });
            if (addServerAlertSmsOption.CheckState == CheckState.Checked)
                alerts.Add(new SmsAlert { Info = addServerAlertSmsTextBox.Text });

            return addServerAlertEmailOption.Checked | addServerAlertSmsOption.Checked;
        }

        private object TestMonitorData
        {
            get
            {
                switch((MonitorBaseType) addServerBaseTabControl.SelectedIndex)
                {
                    case MonitorBaseType.Advanced:
                        switch ((FullMonitorType)addServerTabControl.SelectedIndex)
                        {
                            case FullMonitorType.PerformanceCounter:
                                if (perfCounterTest != null)
                                    return perfCounterTest;
                                return "None";
                            case FullMonitorType.EventLog:
                                if (eventLogTest != null)
                                    return eventLogTest;
                                return "None";
                            case FullMonitorType.Service:
                            default:
                                return "None";
                        }
                    case MonitorBaseType.Basic:
                    case MonitorBaseType.Common:
                    default:
                        return "None";
                }
            }
        }

        public String ToLog //NOTE:change to function that takes string and gives back List<String> or something?
        {
            get { return _toLog; }
            //NOTE: clean up extra newline characters that slip in
            private set { _toLog += DateTime.Now + ": " + value.Replace("\r\n", "") + Environment.NewLine; }
        }

        private void AddServerOkBtnClick(object sender, EventArgs e)
        {
            switch((MonitorBaseType) addServerBaseTabControl.SelectedIndex)
            {
                case MonitorBaseType.Advanced:
                    ReturnValue = (FullMonitorType)addServerTabControl.SelectedIndex;
                    break;
                case MonitorBaseType.Basic:
                    ReturnValue = FullMonitorType.Basic;
                    break;
                case MonitorBaseType.Common:
                    ReturnValue = FullMonitorType.Common;
                    break;
            }
            DialogResult = Validator();

            ((NetworkMonitor)this.Owner).RepopulateSelServerResult(this);
            this.Dispose();//.Close();
        }
        private void AddServerCancelBtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            ((NetworkMonitor)this.Owner).RepopulateSelServerResult(this);
            this.Dispose();//.Close();
        }

        #region Ping
        #region Ip/Hostname set
        private void AddServerMonitorNameLeaveFocus(object sender, EventArgs e)
        {
            FriendlyName = ((TextBox)sender).Text;
        }
        private void AddServerIpLeaveFocus(object sender, EventArgs e)
        {
            IpOrHostName = ((TextBox)sender).Text;
        }
        #endregion
        private void AddServerValidIpPingBtnClick(object sender, EventArgs e)
        {
            SetIpAfterPing(false);
        }

        private void SetIpAfterPing(Boolean killThreads)
        {
            Cursor = Cursors.WaitCursor;
            addServerValidIpLabel.ForeColor = Color.Black;
            addServerValidIpLabel.ResetText();
            addServerValidIpLabel.Text = "...";
            addServerValidIpLabel.Update();
            addServerValidIpLabel2.ForeColor = Color.Black;
            addServerValidIpLabel2.ResetText();
            addServerValidIpLabel2.Text = "...";
            addServerValidIpLabel2.Update();
            basicMonitorIpResult.ForeColor = Color.Black;
            basicMonitorIpResult.ResetText();
            basicMonitorIpResult.Text = "...";
            basicMonitorIpResult.Update();
            IPStatus pr = PingIp(iporhostname);
            if (pr == IPStatus.Success)
            {
                addServerValidIpLabel.ForeColor = Color.Green;
                addServerValidIpLabel2.ForeColor = Color.Green;
                basicMonitorIpResult.ForeColor = Color.Green;
                addServerValidIpLabel.Text = "OK";
                addServerValidIpLabel2.Text = "OK";
                basicMonitorIpResult.Text = "OK";
                //IpOrHostName = addServerValidIpIpTextBox.Text;
            }
            else
            {
                addServerValidIpLabel.ForeColor = Color.Red;
                addServerValidIpLabel2.ForeColor = Color.Red;
                basicMonitorIpResult.ForeColor = Color.Red;
                addServerValidIpLabel.Text = pr.ToString();
                addServerValidIpLabel2.Text = pr.ToString();
                basicMonitorIpResult.Text = pr.ToString();
                //IpOrHostName = addServerValidIpIpTextBox.Text;
            }
            
            //IPAddress addy;
            //if (!IPAddress.TryParse(addServerValidIpIpTextBox.Text, out addy))
            //{
            //    _hostName = addServerValidIpIpTextBox.Text;
            //}

            if (killThreads)
                AfterPingKillThreads();
            else
                AfterPingRemakeThreads();
        }

        private void AfterPingRemakeThreads()
        {
            perfCounterPCTypeDdl.Visible = false;
            perfCounterCategoryWaitLabel.Visible = true;
            alreadyPopulated = false; //NOTE: need to refresh the drop downs
            if (_perfCounterPopulation != null)
            {
                if (!_perfCounterPopulation.IsAlive)
                {
                    _perfCounterPopulation = null;
                    _perfCounterPopulation = new Thread(GetPerfCounterTypes) { Name = "PerformanceCounter AfterIP" };
                    _perfCounterPopulation.Start();
                }
                else
                {
                    _perfCounterPopulation.Join(50);
                    _perfCounterPopulation = null;
                    _perfCounterPopulation = new Thread(GetPerfCounterTypes) { Name = "PerformanceCounter AfterIP" };
                    _perfCounterPopulation.Start();
                }
            }
            else
            {
                _perfCounterPopulation = new Thread(GetPerfCounterTypes) { Name = "PerformanceCounter AfterIP" };
                _perfCounterPopulation.Start();
            }
            if (_eventMonitorPopulation != null)
            {
                if (!_eventMonitorPopulation.IsAlive)
                {
                    _eventMonitorPopulation = null;
                    _eventMonitorPopulation = new Thread(GetEvents) { Name = "EventMonitor AfterIP" };
                    _eventMonitorPopulation.Start();
                }
                else
                {
                    _eventMonitorPopulation.Join(50);
                    _eventMonitorPopulation = null;
                    _eventMonitorPopulation = new Thread(GetEvents) { Name = "EventMonitor AfterIP" };
                    _eventMonitorPopulation.Start();
                }
            }
            else
            {
                _eventMonitorPopulation = new Thread(GetEvents) { Name = "EventMonitor AfterIP" };
                _eventMonitorPopulation.Start();
            }

            if (_serviceMonitorPopulation != null)
            {
                if (!_serviceMonitorPopulation.IsAlive)
                {
                    _serviceMonitorPopulation = null;
                    _serviceMonitorPopulation = new Thread(GetServices) { Name = "ServiceMonitor AfterIP" };
                    _serviceMonitorPopulation.Start();
                }
                else
                {
                    _serviceMonitorPopulation.Join(50);
                    _serviceMonitorPopulation = null;
                    _serviceMonitorPopulation = new Thread(GetServices) { Name = "ServiceMonitor AfterIP" };
                    _serviceMonitorPopulation.Start();
                }
            }
            else
            {
                _serviceMonitorPopulation = new Thread(GetServices) { Name = "ServiceMonitor AfterIP" };
                _serviceMonitorPopulation.Start();
            }


            if (_commonMonitorPopulation != null)
            {
                if (!_commonMonitorPopulation.IsAlive)
                {
                    _commonMonitorPopulation = null;
                    _commonMonitorPopulation = new Thread(GetCommon) { Name = "CommonMonitor AfterIP" };
                    _commonMonitorPopulation.Start();
                }
                else
                {
                    _commonMonitorPopulation.Join(50);
                    _commonMonitorPopulation = null;
                    _commonMonitorPopulation = new Thread(GetCommon) { Name = "CommonMonitor AfterIP" };
                    _commonMonitorPopulation.Start();
                }
            }
            else
            {
                _commonMonitorPopulation = new Thread(GetCommon) { Name = "CommonMonitor AfterIP" };
                _commonMonitorPopulation.Start();
            }

            Cursor = Cursors.Default;
        }
        private void AfterPingKillThreads()
        {
            perfCounterPCTypeDdl.Visible = false;
            perfCounterCategoryWaitLabel.Visible = true;
            if (_perfCounterPopulation != null)
            {
                if (_perfCounterPopulation.IsAlive)
                {
                    _perfCounterPopulation.Join(50);
                }
            }

            if (_eventMonitorPopulation != null)
            {
                if (!_eventMonitorPopulation.IsAlive)
                {
                    _eventMonitorPopulation.Join(50);
                }
            }

            if (_serviceMonitorPopulation != null)
            {
                if (!_serviceMonitorPopulation.IsAlive)
                {
                    _serviceMonitorPopulation.Join(50);
                }
            }

            if (_commonMonitorPopulation != null)
            {
                if (!_commonMonitorPopulation.IsAlive)
                {
                    _commonMonitorPopulation.Join(50);
                }
            }

            Cursor = Cursors.Default;
        }

        private IPStatus PingIp(String ip)
        {
            Ping p = new Ping();
            if (!String.IsNullOrEmpty(ip))
            {
                try
                {
                    PingReply pr = p.Send(ip);
                    p.Dispose();
                    if (pr != null) 
                        return pr.Status;
                }
                catch (Exception ex)
                {
                    p.Dispose();
                    ToLog = "Error: " + ex.Message;
                }
            }
            else
                p.Dispose();
            return IPStatus.Unknown;
        }
        #endregion

        private void TestMonitorUpdateTick(object sender, EventArgs e)
        {
            if (testMonitorRunPerf || _testMonitorCommon)// || TestMonitorRunEvent || TestMonitorRunBasic || TestMonitorRunService) 
            {
                switch ((MonitorBaseType)addServerBaseTabControl.SelectedIndex)
                {
                    case MonitorBaseType.Advanced:
                        switch ((FullMonitorType) addServerTabControl.SelectedIndex)
                        {
                            case FullMonitorType.PerformanceCounter:
                                try
                                {
                                    if (TestMonitorData.ToString() != "None")
                                    {
                                        PerformanceCounterType pct = ((PerformanceCounter) TestMonitorData).CounterType;
                                        //perfCounterTestDataHelpText.ForeColor = Color.Black;
                                        perfCounterTestDataHelpText.Text =
                                            ((PerformanceCounter) TestMonitorData).CounterHelp;
                                        switch (pct)
                                        {
                                            case PerformanceCounterType.RawFraction:
                                            case PerformanceCounterType.SampleFraction:
                                                perfCounterTestData.Text =
                                                    ((PerformanceCounter) TestMonitorData).NextValue().ToString("f") +
                                                    "%";
                                                perfCounterTestDataType.Text = pct.ToString();
                                                break;
                                            case PerformanceCounterType.Timer100Ns:
                                            case PerformanceCounterType.Timer100NsInverse:
                                            case PerformanceCounterType.CounterTimer:
                                            case PerformanceCounterType.CounterTimerInverse:
                                                perfCounterTestData.Text =
                                                    ((PerformanceCounter) TestMonitorData).NextValue().ToString("f") +
                                                    "% Avg";
                                                perfCounterTestDataType.Text = pct.ToString();
                                                break;
                                            case PerformanceCounterType.SampleCounter:
                                            case PerformanceCounterType.RateOfCountsPerSecond64:
                                            case PerformanceCounterType.RateOfCountsPerSecond32:
                                                perfCounterTestData.Text =
                                                    ((PerformanceCounter) TestMonitorData).NextValue().ToString("f") +
                                                    "/sec";
                                                perfCounterTestDataType.Text = pct.ToString();
                                                break;
                                            case PerformanceCounterType.NumberOfItems32:
                                            case PerformanceCounterType.NumberOfItems64:
                                            case PerformanceCounterType.NumberOfItemsHEX32:
                                            case PerformanceCounterType.NumberOfItemsHEX64:
                                                perfCounterTestData.Text =
                                                    ((PerformanceCounter) TestMonitorData).NextValue().ToString("f");
                                                perfCounterTestDataType.Text = pct.ToString();
                                                break;
                                            case PerformanceCounterType.AverageBase:
                                            case PerformanceCounterType.AverageCount64:
                                            case PerformanceCounterType.AverageTimer32:
                                            case PerformanceCounterType.CounterDelta32:
                                            case PerformanceCounterType.CounterDelta64:
                                            case PerformanceCounterType.CounterMultiBase:
                                            case PerformanceCounterType.CounterMultiTimer:
                                            case PerformanceCounterType.CounterMultiTimer100Ns:
                                            case PerformanceCounterType.CounterMultiTimer100NsInverse:
                                            case PerformanceCounterType.CounterMultiTimerInverse:
                                            case PerformanceCounterType.CountPerTimeInterval32:
                                            case PerformanceCounterType.CountPerTimeInterval64:
                                            case PerformanceCounterType.ElapsedTime:
                                            case PerformanceCounterType.RawBase:
                                            case PerformanceCounterType.SampleBase:
                                            default:
                                                perfCounterTestData.Text =
                                                    ((PerformanceCounter) TestMonitorData).NextValue().ToString();
                                                perfCounterTestDataType.Text = pct.ToString();
                                                break;
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(perfCounterTestDataThresholdWarningTextBox.Text))
                                    {
                                        Double threshold = 0;
                                        Double currentcounter = 1; //NOTE:avoid divide by zero down below
                                        String countersplit = perfCounterTestData.Text;

                                        try
                                        {
                                            threshold = Convert.ToDouble(perfCounterTestDataThresholdWarningTextBox.Text);
                                            if (countersplit.Contains("%"))
                                                countersplit = countersplit.Split(new[] {'%'})[0];
                                            else if (countersplit.Contains("/"))
                                                countersplit = countersplit.Split(new[] {'/'})[0];

                                            currentcounter = Convert.ToDouble(countersplit);
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.Instance.LogException(this.GetType(), ex);
                                        }
                                        if (currentcounter > threshold)
                                        {
                                            perfCounterTestDataStatus.ForeColor = Color.Red;
                                            perfCounterTestDataStatus.Text = "Threshold exceeded by " +
                                                                             (((currentcounter/threshold) - 1)*100).
                                                                                 ToString("f") +
                                                                             "%";
                                        }
                                        else
                                        {
                                            perfCounterTestDataStatus.ForeColor = Color.Green;
                                            perfCounterTestDataStatus.Text = "OK";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Instance.LogException(this.GetType(), ex);
                                    perfCounterTestData.Text = "Unable to populate";
                                }
                                break;
                            case FullMonitorType.Service:
                            case FullMonitorType.EventLog:
                                break;
                        }
                        break;
                    case MonitorBaseType.Basic:
                        break;
                    case MonitorBaseType.Common:
                        CommonUpdateSelectedMonitor();
                        break;
                        
                }
            }
        }


        private void AddServerFormClosed(object sender, FormClosedEventArgs e)
        {
            //NOTE:dispose of tab related threads that were otherwise left
            if (_perfCounterPopulation != null)
            {
                _perfCounterPopulation.Abort();
                _perfCounterPopulation = null;
            }
            if (_serviceMonitorPopulation != null)
            {
                _serviceMonitorPopulation.Abort();
                _serviceMonitorPopulation = null;
            }
            if (_eventMonitorPopulation != null)
            {
                _eventMonitorPopulation.Abort();
                _eventMonitorPopulation = null;
            }
            if (_eventMonitorListViewPopulation != null)
            {
                _eventMonitorListViewPopulation.Abort();
                _eventMonitorListViewPopulation = null;
            }
            if (_commonMonitorPopulation != null)
            {
                _commonMonitorPopulation.Abort();
                _commonMonitorPopulation = null;
            }

            alreadyPopulated = false;

            #region Common
            if (_commonPfcCounter != null)
                _commonPfcCounter.Dispose();
            if (_commonMemoryUsage != null)
            _commonMemoryUsage.Dispose();
            if (_commonServiceState != null)
                _commonServiceState.Dispose();
            #endregion
            #region Event
            foreach(EventLog el in eventLogTest)
                el.Dispose();
            #endregion
            #region Pfc
            if(perfCounterTest != null)
                perfCounterTest.Dispose();
            perfCounterCategories = null;
            perfCounterNames = null;
            perfCounterInstances = null;
            #endregion
            #region Service
            foreach(System.ServiceProcess.ServiceController sc in servicesTest)
                sc.Dispose();
            #endregion

            #region Remove Handlers Event
            addServerTabControl.SelectedIndexChanged -= AddServerTabControlSelectedIndexChanged;
            perfCounterTestDataThresholdLessThanCheckBox.CheckedChanged -= AddServerAlertSmsOptionCheckedChanged;
            perfCounterTestBtn.Click -= PerfCounterTestBtnClick;
            perfCounterPCTypeDdl.SelectedIndexChanged -= PerfCounterPcTypeDdlSelectedIndexChanged;
            eventMonitorSourceFilterBtn.Click -= EventMonitorSourceFilterBtnClick;
            eventMonitorEntryTypeFilterBtn.Click -= EventMonitorEntryTypeFilterBtnClick;
            eventMonitorEventsLogTypeDdl.SelectedIndexChanged -= EventMonitorEventsLogTypeDdlSelectedIndexChanged;
            servicesPickedServicesListView.MouseDoubleClick -= ServicesPickedServicesListViewMouseDoubleClick;
            servicesPickedServicesClearBtn.Click -= AddServerPickedServicesClearBtnClick;
            servicesServiceListView.ItemSelectionChanged -= AddServerServiceListViewItemSelectionChanged;
            addServerCancelBtn.Click -= AddServerCancelBtnClick;
            addServerOkBtn.Click -= AddServerOkBtnClick;
            addServerValidIpMonitorName.Leave -= AddServerMonitorNameLeaveFocus;
            addServerValidIpPingBtn.Click -= AddServerValidIpPingBtnClick;
            testMonitorUpdate.Tick -= TestMonitorUpdateTick;
            addServerAlertEmailTextBox.TextChanged -= AddServerAlertEmailTextBoxTextChanged;
            addServerAlertSmsTextBox.TextChanged -= AddServerAlertSmsTextBoxTextChanged;
            addServerAlertEmailOption.CheckedChanged -= AddServerAlertEmailOptionCheckedChanged;
            addServerAlertSmsOption.CheckedChanged -= AddServerAlertSmsOptionCheckedChanged;
            commonMonitorCheckBtn.Click -= CommonMonitorCheckBtnClick;
            commonHddGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonHddSelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            commonMemoryGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonMemoryUsageTypeDdl.SelectedIndexChanged -= CommonMemoryUsageTypeDdlSelectedIndexChanged;
            commonMemorySelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            commonServiceGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonServiceSelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            commonCpuGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonCpuUsageTypeDdl.SelectedIndexChanged -= CommonCpuUsageTypeDdlSelectedIndexChanged;
            commonCpuSelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            commonSwapFileGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonSwapFileSelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            commonProcessGroupBox.Enter -= CommonMonitorGroupBoxEnter;
            commonProcessStateUsageTypeDdl.SelectedIndexChanged -= CommonProcessStateUsageTypeDdlSelectedIndexChanged;
            commonProcessSelected.Click -= CommonMonitorMonitorSelectCheckedChanged;
            addServerValidIpMonitorName2.Leave -= AddServerMonitorNameLeaveFocus;
            addServerValidIpPingBtn2.Click -= AddServerValidIpPingBtnClick;
            addServerValidIpIpTextBox2.Leave -= AddServerIpLeaveFocus;
            addServerValidIpMonitorName3.Leave -= AddServerMonitorNameLeaveFocus;
            basicMonitorFtpGroupBox.Enter -= BasicMonitorGroupBoxEnter;
            basicMonitorFtpMonitorSelect.Click -= BasicMonitorMonitorSelectCheckedChanged;
            basicMonitorFtpPathPortTextBox.TypeValidationCompleted -= BasicMonitorPortTextBoxTypeValidationCompleted;
            basicMonitorFtpPathCheckBtn.Click -= BasicMonitorFtpPathCheckBtnClick;
            basicMonitorPingGroupBox.Enter -= BasicMonitorGroupBoxEnter;
            basicMonitorPingMonitorSelect.Click -= BasicMonitorMonitorSelectCheckedChanged;
            basicMonitorIpCheckBtn.Click -= BasicMonitorIpCheckBtnClick;
            basicMonitorIpTextBox.Leave -= AddServerIpLeaveFocus;
            basicMonitorHttpGroupBox.Enter -= BasicMonitorGroupBoxEnter;
            basicMonitorHttpMonitorSelect.Click -= BasicMonitorMonitorSelectCheckedChanged;
            basicMonitorHttpPathPortTextBox.TypeValidationCompleted -= BasicMonitorPortTextBoxTypeValidationCompleted;
            basicMonitorHttpPathCheckBtn.Click -= BasicMonitorHttpPathCheckBtnClick;
            addServerBaseTabControl.SelectedIndexChanged -= AddServerBaseTabControlSelectedIndexChanged;
            FormClosed -= AddServerFormClosed;
            #endregion
        }

        #region Tab Index changes 
        //NOTE: I hate this - the tab should be able to  manage this stuff, but I've not found a reliable way... this feels wrong and messy.

        private void AddServerTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((FullMonitorType) addServerTabControl.SelectedIndex)
            {
                    #region cases

                default:
                case FullMonitorType.None:
                case FullMonitorType.PerformanceCounter:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    ReturnValue = FullMonitorType.PerformanceCounter;
                    _perfCounterPopulation = new Thread(GetPerfCounterTypes)
                                                {Name = "PerformanceCounter TabSwitch"};
                    _perfCounterPopulation.Start();
                    break;
                case FullMonitorType.Service:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    ReturnValue = FullMonitorType.Service;
                    _serviceMonitorPopulation = new Thread(GetServices)
                                                   {Name = "ServiceMonitor TabSwitch"};
                    _serviceMonitorPopulation.Start();
                    break;
                case FullMonitorType.EventLog:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    ReturnValue = FullMonitorType.EventLog;
                    _eventMonitorPopulation = new Thread(GetEvents) {Name = "EventMonitor TabSwitch"};
                    _eventMonitorPopulation.Start();
                    break;
                
                    #endregion
            }
        }
        private void AddServerBaseTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((MonitorBaseType) addServerBaseTabControl.SelectedIndex)
            {
                case MonitorBaseType.Advanced:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    ReturnValue = (FullMonitorType) addServerTabControl.SelectedIndex;
                    commonFourLabel.Visible = false;
                    _perfCounterPopulation = new Thread(GetPerfCounterTypes)
                                                {Name = "PerformanceCounter TabSwitch"};
                    _perfCounterPopulation.Start();
                    break;
                case MonitorBaseType.Basic:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    commonFourLabel.Visible = false;
                    
                    ReturnValue = FullMonitorType.Basic;
                    break;
                case MonitorBaseType.Common:
                    if (_perfCounterPopulation != null)
                    {
                        _perfCounterPopulation.Abort();
                        _perfCounterPopulation = null;
                    }
                    if (_serviceMonitorPopulation != null)
                    {
                        _serviceMonitorPopulation.Abort();
                        _serviceMonitorPopulation = null;
                    }
                    if (_eventMonitorPopulation != null)
                    {
                        _eventMonitorPopulation.Abort();
                        _eventMonitorPopulation = null;
                    }
                    ReturnValue = FullMonitorType.Common;
                    commonFourLabel.Visible = true;
                    _commonMonitorPopulation = new Thread(GetCommon)
                                                {Name = "CommonMonitor TabSwitch"};
                    _commonMonitorPopulation.Start();
                    break;
            }
        }
        #endregion
        #region validation

        private DialogResult Validator()
        {
            switch((MonitorBaseType) addServerBaseTabControl.SelectedIndex)
            {
                case MonitorBaseType.Advanced:
                    switch ((FullMonitorType) addServerTabControl.SelectedIndex)
                    {
                        case FullMonitorType.EventLog:
                            ValidateEventMonitors();
                            break;
                        case FullMonitorType.PerformanceCounter:
                            ValidatePerfCounters();
                            break;
                        case FullMonitorType.Service:
                            ValidateServiceMonitors();
                            break;
                    }
                    break;
                case MonitorBaseType.Basic:
                    ValidateBasicMonitors();
                    break;
                case MonitorBaseType.Common:
                    ValidateCommonMonitors();
                    break;
            }
            //NOTE: need flag it as None if there is validation error.
            return DialogResult.OK; 
        }

        private void ValidatePerfCounters()
        {
        }

        private void ValidateEventMonitors()
        {
        }

        private void ValidateBasicMonitors()
        {
        }

        private void ValidateServiceMonitors()
        {
        }

        private void ValidateCommonMonitors()
        {
        }

        #endregion

        #region Alerts
        private void AddServerAlertEmailOptionCheckedChanged(object sender, EventArgs e)
        {
            addServerAlertEmailTextBox.ReadOnly = !addServerAlertEmailOption.Checked;
            
            if(addServerAlertEmailTextBox.ReadOnly)
                addServerAlertEmailTextBox.BackColor = Color.Gray;
            else
                addServerAlertEmailTextBox.BackColor = Color.White;
        }
        private void AddServerAlertSmsOptionCheckedChanged(object sender, EventArgs e)
        {
            addServerAlertSmsTextBox.ReadOnly = !addServerAlertSmsOption.Checked;

            if (addServerAlertSmsTextBox.ReadOnly)
                addServerAlertSmsTextBox.BackColor = Color.Gray;
            else
                addServerAlertSmsTextBox.BackColor = Color.White;
        }

        private void AddServerAlertSmsTextBoxTextChanged(object sender, EventArgs e)
        {
            if (addServerAlertSmsTextBox.Text != "")
                addServerAlertSmsOption.Checked = true;
            else
                addServerAlertSmsOption.Checked = false;
        }
        private void AddServerAlertEmailTextBoxTextChanged(object sender, EventArgs e)
        {
            if (addServerAlertEmailTextBox.Text != "")
                addServerAlertEmailOption.Checked = true;
            else
                addServerAlertEmailOption.Checked = false;
        }
        #endregion

    }
}