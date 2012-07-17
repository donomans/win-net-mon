using System;
using System.Diagnostics;
using System.Drawing;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class AddServer
    {
        #region Private Fields
        private Thread _commonMonitorPopulation;
        private Boolean _testMonitorCommon;
        private CommonMonitorType _commonType = CommonMonitorType.None;
        private const Single NoProcess = -1f;
        private Boolean _processRetry = true;
        //private Boolean _commonAlreadyPopulated = false;

        private PerformanceCounter _commonPfcCounter;
        //private PerformanceCounter _commonHddUsage;
        //private PerformanceCounter _commonCpuUsage;
        //private PerformanceCounter _commonMemoryUsage;
        private ManagementObjectCollection _commonMemoryUsage;
        private String _commonMemoryUsageLastSelected;
        //private PerformanceCounter _commonSwapFileUsage;
        //private PerformanceCounter _commonProcessState;
        private ServiceController _commonServiceState;
        #endregion

        #region Public Properties
        public CommonMonitorType CommonType { get { return _commonType; } }
        #endregion

        public void CommonRepopulate(IMonitor common)
        {
            PerformanceCounterCategory hddUsageCategories = new PerformanceCounterCategory("LogicalDisk", IpOrHostName);
            CommonPopulateHddUsage(hddUsageCategories.GetInstanceNames());
           
            ServiceController[] services = ServiceController.GetServices(IpOrHostName);
            String[] servicesList = new String[services.Length];
            Int32 count = 0;
            foreach (ServiceController sc in services)
            {
                servicesList[count++] = sc.DisplayName;
            }
            CommonPopulateServiceState(servicesList);
            
            PerformanceCounterCategory processStateCategories = new PerformanceCounterCategory("Process", IpOrHostName);
            CommonPopulateProcessState(processStateCategories.GetInstanceNames());
            
            switch(common.Type)
            {
                case FullMonitorType.PerformanceCounter:
                    try
                    {
                        PfcMonitor pfc = (PfcMonitor) common;
                        if (_commonPfcCounter == null)
                        {
                            _commonPfcCounter = new PerformanceCounter
                                                    {
                                                        CategoryName = pfc.Category,
                                                        CounterName = pfc.Counter,
                                                        InstanceName = pfc.Instance,
                                                        MachineName = pfc.Server
                                                    };
                        }
                        else
                        {
                            _commonPfcCounter.Dispose();
                            _commonPfcCounter = new PerformanceCounter
                                                    {
                                                        CategoryName = pfc.Category,
                                                        CounterName = pfc.Counter,
                                                        InstanceName = pfc.Instance,
                                                        MachineName = pfc.Server
                                                    };
                        }
                        switch (pfc.Category)
                        {
                            case "Processor":
                                _commonType = CommonMonitorType.CpuUsage;
                                commonCpuUsageTypeDdl.Text = pfc.Counter;
                                commonCpuCriticalTextBox.Text = pfc.ThresholdPanic;
                                commonCpuWarningTextBox.Text = pfc.ThresholdWarning;
                                commonCpuGroupBox.BackColor = Color.LightGray;
                                commonCpuSelected.Checked = true;
                                break;
                            case "LogicalDisk":
                                _commonType = CommonMonitorType.HddUsage;
                                commonHddDriveLetterDdl.Text = pfc.Instance;
                                commonHddCriticalTextBox.Text = pfc.ThresholdPanic;
                                commonHddWarningTextBox.Text = pfc.ThresholdWarning;
                                commonHddGroupBox.BackColor = Color.LightGray;
                                commonHddSelected.Checked = true;
                                break;
                            case "Process":
                                _commonType = CommonMonitorType.ProcessState;
                                commonProcessStateUsageTypeDdl.Text = pfc.Counter;
                                commonProcessStateProcessDd.Text = pfc.Instance;
                                commonProcessStateCriticalTextBox.Text = pfc.ThresholdPanic;
                                commonProcessStateWarningTextBox.Text = pfc.ThresholdWarning;
                                commonProcessGroupBox.BackColor = Color.LightGray;
                                commonProcessSelected.Checked = true;
                                break;
                            case "Paging File":
                                _commonType = CommonMonitorType.SwapFileUsage;
                                commonSwapFileUsageTypeDdl.Text = pfc.Counter;
                                commonSwapFileCriticalTextBox.Text = pfc.ThresholdPanic;
                                commonSwapFileWarningTextBox.Text = pfc.ThresholdWarning;
                                commonSwapFileGroupBox.BackColor = Color.LightGray;
                                commonSwapFileSelected.Checked = true;
                                break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.Instance.LogException(this.GetType(), ex);
                    }
                    break;
                case FullMonitorType.Service:
                    ServiceMonitor service = (ServiceMonitor) common;
                    _commonServiceState = new ServiceController(service.Services[0].ServiceName, service.Server);
                    _commonType = CommonMonitorType.ServiceState;
                    commonServiceServicesChoiceDdl.Text = service.Services[0].ServiceName; //NOTE: there is only one service in Common tab
                    commonServiceGroupBox.BackColor = Color.LightGray;
                    commonServiceSelected.Checked = true;
                    break;
                case FullMonitorType.Wmi:
                    WmiMonitor wmi = (WmiMonitor) common;
                    _commonType = CommonMonitorType.MemoryUsage;
                    //NOTE: % In Use == WmiType.MemoryUsage
                    //NOTE: Available == WmiType.MemoryFree
                    switch(wmi.WmiType)
                    {
                        case WmiType.MemoryFree:
                            commonMemoryUsageTypeDdl.Text = "Available";
                            break;
                        case WmiType.MemoryUsage:
                            commonMemoryUsageTypeDdl.Text = "% In Use";
                            break;
                    }
                    commonMemoryWarningTextBox.Text = wmi.ThresholdWarning;
                    commonMemoryCriticalTextBox.Text = wmi.ThresholdPanic;
                    commonMemoryGroupBox.BackColor = Color.LightGray;
                    commonMemorySelected.Checked = true;
                    break;
            }
            commonMonitorTestDataUpdateFreqTextBox.Text = common.UpdateFrequency.ToString();
            addServerValidIpIpTextBox2.Text = common.Server;
            addServerValidIpMonitorName2.Text = common.FriendlyName;
            alreadyPopulated = true;
        }

        public IMonitor GetCommonMonitor()
        {
            switch(_commonType)
            {
                case CommonMonitorType.CpuUsage:
                    PfcMonitor cpuUsage = new PfcMonitor
                                              {
                                                  FriendlyName = addServerValidIpMonitorName2.Text,
                                                  Category = _commonPfcCounter.CategoryName,
                                                  Counter = _commonPfcCounter.CounterName,
                                                  Instance = _commonPfcCounter.InstanceName,
                                                  ThresholdWarning = commonCpuWarningTextBox.Text,
                                                  ThresholdPanic = commonCpuCriticalTextBox.Text,
                                                  ThresholdBreachCount = 1,
                                                  ThresholdLessThan = commonCpuGtLtLabel.Text == "<" ? true : false,
                                                  Server = addServerValidIpIpTextBox2.Text,
                                                  UpdateFrequency =
                                                      Convert.ToInt32(commonMonitorTestDataUpdateFreqTextBox.Text),
                                                  Common = true
                                              };
                    return cpuUsage;
                case CommonMonitorType.HddUsage:
                    PfcMonitor hddUsage = new PfcMonitor
                                              {
                                                  FriendlyName = addServerValidIpMonitorName2.Text,
                                                  Category = _commonPfcCounter.CategoryName,
                                                  Counter = _commonPfcCounter.CounterName,
                                                  Instance = _commonPfcCounter.InstanceName,
                                                  ThresholdWarning = commonHddWarningTextBox.Text,
                                                  ThresholdPanic = commonHddCriticalTextBox.Text,
                                                  ThresholdBreachCount = 1,
                                                  ThresholdLessThan = commonHddGtLtLabel.Text == "<" ? true : false,
                                                  Server = addServerValidIpIpTextBox2.Text,
                                                  UpdateFrequency =
                                                      Convert.ToInt32(commonMonitorTestDataUpdateFreqTextBox.Text),
                                                  Common = true
                                              };
                    return hddUsage;
                case CommonMonitorType.MemoryUsage:
                    WmiMonitor memoryUsage = new WmiMonitor
                                                 {
                                                     FriendlyName = addServerValidIpMonitorName2.Text,
                                                     ThresholdWarning = commonMemoryWarningTextBox.Text,
                                                     ThresholdPanic = commonMemoryCriticalTextBox.Text,
                                                     ThresholdBreachCount = 1,
                                                     ThresholdLessThan =
                                                         commonMemoryGtLtLabel.Text == "<" ? true : false,
                                                     Server = addServerValidIpIpTextBox2.Text,
                                                     UpdateFrequency =
                                                         Convert.ToInt32(commonMonitorTestDataUpdateFreqTextBox.Text),
                                                     WmiType =
                                                         commonMemoryUsageTypeDdl.Text == "% In Use"
                                                             ? WmiType.MemoryUsage
                                                             : WmiType.MemoryFree,
                                                     Common = true
                                                 };
                    return memoryUsage;
                case CommonMonitorType.ProcessState:
                    PfcMonitor processState = new PfcMonitor
                                                  {
                                                      FriendlyName = addServerValidIpMonitorName2.Text,
                                                      Category = _commonPfcCounter.CategoryName,
                                                      Counter = _commonPfcCounter.CounterName,
                                                      Instance = _commonPfcCounter.InstanceName,
                                                      ThresholdWarning = commonProcessStateWarningTextBox.Text,
                                                      ThresholdPanic = commonProcessStateCriticalTextBox.Text,
                                                      ThresholdBreachCount = 1,
                                                      ThresholdLessThan =
                                                          commonProcessStateGtLtLabel.Text == "<" ? true : false,
                                                      Server = addServerValidIpIpTextBox2.Text,
                                                      UpdateFrequency =
                                                          Convert.ToInt32(commonMonitorTestDataUpdateFreqTextBox.Text),
                                                      Common = true
                                                  };
                    return processState;
                case CommonMonitorType.ServiceState:
                    ServiceMonitor serviceState = new ServiceMonitor
                                                      {
                                                          FriendlyName = addServerValidIpMonitorName2.Text,
                                                          Server = addServerValidIpIpTextBox2.Text,
                                                          UpdateFrequency =
                                                              Convert.ToInt32(
                                                                  commonMonitorTestDataUpdateFreqTextBox.Text),
                                                          AutomaticRestart = false,
                                                          Common = true
                                                      };
                    serviceState.Services.Add(new Service(_commonServiceState.ServiceName));
                    return serviceState;
                case CommonMonitorType.SwapFileUsage:
                    PfcMonitor swapFileUsage = new PfcMonitor
                                                   {
                                                       FriendlyName = addServerValidIpMonitorName2.Text,
                                                       Category = _commonPfcCounter.CategoryName,
                                                       Counter = _commonPfcCounter.CounterName,
                                                       Instance = _commonPfcCounter.InstanceName,
                                                       ThresholdWarning = commonSwapFileWarningTextBox.Text,
                                                       ThresholdPanic = commonSwapFileCriticalTextBox.Text,
                                                       ThresholdBreachCount = 1,
                                                       ThresholdLessThan =
                                                           commonSwapFileGtLtLabel.Text == "<" ? true : false,
                                                       Server = addServerValidIpIpTextBox2.Text,
                                                       UpdateFrequency =
                                                           Convert.ToInt32(commonMonitorTestDataUpdateFreqTextBox.Text),
                                                       Common = true
                                                   };
                    return swapFileUsage;
            }
            return null;
        }

        private void GetCommon()
        {
            while (!ipBoolean)
            {
                if (IsDisposed)
                    //NOTE:avoids situation where ip wasn't input and the addserver window was canceled - would loop forever otherwise
                    return;

                Thread.Sleep(100);
            }
            try
            {
                if(!alreadyPopulated)
                    PopulateCommon();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                ToLog = "Error: " + ex.Message;
            }
        }


        private void PopulateCommon()
        {
            #region Memory Usage
                //Invoke(new PopulateCommonMonitorDelegate(CommonPopulateMemoryUsage));
            #endregion
            #region CPU Usage
                //Invoke(new PopulateCommonMonitorDelegate(CommonPopulateCpuUsage));
            #endregion
            #region Hdd Usage
                PerformanceCounterCategory hddUsageCategories = new PerformanceCounterCategory("LogicalDisk", IpOrHostName);
                Invoke(new PopulateCommonMonitorWithValuesDelegate(CommonPopulateHddUsage), new object[] {hddUsageCategories.GetInstanceNames()});
            #endregion
            #region Swap File Usage
                //Invoke(new PopulateCommonMonitorDelegate(CommonPopulateSwapFileUsage));
            #endregion
            #region Service State
                ServiceController[] services = ServiceController.GetServices(IpOrHostName);
                String[] servicesList = new String[services.Length];
                Int32 count = 0;
                foreach(ServiceController sc in services)
                {
                    servicesList[count++] = sc.DisplayName;
                }
                Invoke(new PopulateCommonMonitorWithValuesDelegate(CommonPopulateServiceState), new object[] {servicesList});
            #endregion
            #region Process State
                PerformanceCounterCategory processStateCategories = new PerformanceCounterCategory("Process", IpOrHostName);
                //List<String> processStateCounterNames = new List<String>();
                //foreach (DictionaryEntry idc in processStateCategories.ReadCategory())
                //{
                //    processStateCounterNames.Add(((InstanceDataCollection)idc.Value).CounterName);
                //}
                Invoke(new PopulateCommonMonitorWithValuesDelegate(CommonPopulateProcessState), new object[] {processStateCategories.GetInstanceNames()});//new object[] {processStateCounterNames.ToArray()});
            #endregion
        }

        #region Events
        private void CommonCpuUsageTypeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            if (commonCpuUsageTypeDdl.Text == "% User Time")
            {
                commonCpuWarningTextBox.Text = "70";
                commonCpuCriticalTextBox.Text = "90";
                commonCpuGtLtLabel.Text = ">";
            }
            else
            {
                commonCpuWarningTextBox.Text = "30";
                commonCpuCriticalTextBox.Text = "10";
                commonCpuGtLtLabel.Text = "<";
            }
        }
        private void CommonMemoryUsageTypeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            if (commonMemoryUsageTypeDdl.Text == "Available")
            {
                commonMemoryWarningPctLabel.Visible = false;
                commonMemoryWarningTextBox.Size = new Size(55, 25);
               
                commonMemoryCriticalPctLabel.Visible = false;
                commonMemoryCriticalTextBox.Size = new Size(55, 25);
                
                commonMemoryGtLtLabel.Text = "<";
                if(_commonMemoryUsage != null &&  _commonMemoryUsage.Count > 0)
                {
                    foreach(ManagementObject item in _commonMemoryUsage)
                    {
                        Single total = Convert.ToSingle(item["TotalVisibleMemorySize"]);
                        commonMemoryWarningTextBox.Text = (total*.3f).ToString();
                        commonMemoryCriticalTextBox.Text = (total*.1f).ToString();
                    }
                }
            }
            else
            {
                commonMemoryWarningPctLabel.Visible = true;
                commonMemoryWarningTextBox.Size = new Size((Int32)commonMemoryWarningTextBox.Tag, 25);
                commonMemoryWarningTextBox.Text = "70";
                commonMemoryCriticalPctLabel.Visible = true;
                commonMemoryCriticalTextBox.Size = new Size((Int32)commonMemoryCriticalTextBox.Tag, 25);
                commonMemoryCriticalTextBox.Text = "90";
                commonMemoryGtLtLabel.Text = ">";
            }
        }
        private void CommonProcessStateUsageTypeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            if(commonProcessStateUsageTypeDdl.Text == "Elapsed Time")
            {
                commonProcessStateWarningPctLabel.Visible = false;
                commonProcessStateWarningTextBox.Size = new Size(55, 25);
                commonProcessStateCriticalPctLabel.Visible = false;
                commonProcessStateCriticalTextBox.Size = new Size(55, 25);
                commonProcessStateGtLtLabel.Text = "<";
            }
            else
            {
                commonProcessStateWarningPctLabel.Visible = true;
                commonProcessStateWarningTextBox.Size = new Size((Int32)commonProcessStateWarningTextBox.Tag, 25);
                commonProcessStateCriticalPctLabel.Visible = true;
                commonProcessStateCriticalTextBox.Size = new Size((Int32)commonProcessStateCriticalTextBox.Tag, 25);
                commonProcessStateGtLtLabel.Text = ">";
            }
        }

        private void CommonMonitorCheckBtnClick(object sender, EventArgs e)
        {
            CommonUpdateSelectedMonitor();
        }

        
        #region Single selection
        private void CommonMonitorGroupBoxEnter(object sender, EventArgs e)
        {
            GroupBox groupBox = (GroupBox)sender;
            groupBox.BackColor = Color.LightGray;
            //NOTE: uncheck boxes
            foreach (Control control in this.commonChooserGroupBox.Controls)
            {
                if (control.GetType() == typeof(GroupBox))
                {
                    if (control.Name != groupBox.Name)
                    {
                        control.BackColor = Color.Transparent;
                        foreach (Control childcontrol in control.Controls)
                        {
                            if (childcontrol.Name.EndsWith("Selected") &&
                                childcontrol.GetType() == typeof(CheckBox))
                            {
                                ((CheckBox)childcontrol).CheckState = CheckState.Unchecked;
                                break;
                            }
                        }
                    }
                }
            }
            foreach (Control control in groupBox.Controls)
            {
                if (control.GetType() == typeof(CheckBox))
                {
                    if (control.Name.EndsWith("Selected"))
                    {
                        ((CheckBox)control).Checked = true;
                        _commonType = (CommonMonitorType)control.Tag;//(CommonMonitorType)Enum.Parse(typeof(CommonMonitorType), control.Tag.ToString());
                        break;
                    }
                }
            }
        }
        private void CommonMonitorMonitorSelectCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Parent.BackColor == Color.LightGray)
            {
                checkBox.CheckState = CheckState.Checked;
            }
        }
        #endregion
        #endregion
        #region Button checks
        
        private void CommonUpdateSelectedMonitor()
        {
            ManagementObjectSearcher searcher = null;
            try
            {
                Single currentValue = 0f;
                Single warning = 0f;
                Single critical = 0f;
                _testMonitorCommon = true;

                switch (_commonType)
                {
                    #region CpuUsage
                    case CommonMonitorType.CpuUsage:
                        if (_commonPfcCounter == null ||
                        (_commonPfcCounter.CounterName != commonCpuUsageTypeDdl.Text ||
                        _commonPfcCounter.CategoryName != "Processor" ||
                        (_commonPfcCounter.MachineName != IpOrHostName &&
                        IpOrHostName.ToLower() != Environment.MachineName.ToLower())))
                        {
                            if (_commonPfcCounter != null)
                                _commonPfcCounter.Dispose();
                            _commonPfcCounter = new PerformanceCounter
                                                {
                                                    CategoryName = "Processor", 
                                                    CounterName = commonCpuUsageTypeDdl.Text, 
                                                    InstanceName = "_Total", 
                                                    MachineName = IpOrHostName
                                                };
                            //("Processor", commonCpuUsageTypeDdl.Text, "_Total", IpOrHostName);
                        }
                        currentValue = _commonPfcCounter.NextValue();
                        warning = Convert.ToSingle(commonCpuWarningTextBox.Text);
                        critical = Convert.ToSingle(commonCpuCriticalTextBox.Text);
                        if (commonCpuUsageTypeDdl.Text == "% User Time")
                        {
                            if (currentValue > warning)
                            {
                                if (currentValue > critical)
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                }
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                            }
                        }
                        else
                        {
                            if (currentValue < warning)
                            {
                                if (currentValue < critical)
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                }
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                            }
                        }
                        break;
                    #endregion
                    #region HddUsage
                    case CommonMonitorType.HddUsage:
                        if (_commonPfcCounter == null ||
                            (_commonPfcCounter.CounterName != commonHddDriveLetterDdl.Text ||
                            _commonPfcCounter.CategoryName != "LogicalDisk" ||
                            (_commonPfcCounter.MachineName != IpOrHostName &&
                            IpOrHostName.ToLower() != Environment.MachineName.ToLower())))
                        {
                            if (_commonPfcCounter != null)
                                _commonPfcCounter.Dispose();
                            _commonPfcCounter = new PerformanceCounter
                                                    {
                                                        CategoryName = "LogicalDisk",
                                                        CounterName = "% Free Space",
                                                        InstanceName = commonHddDriveLetterDdl.Text,
                                                        MachineName = IpOrHostName
                                                    };
                            //("LogicalDisk", "% Free Space", commonHddDriveLetterDdl.Text, IpOrHostName);
                        }
                        //currentValue = _commonHddUsage.NextValue();
                        currentValue = _commonPfcCounter.NextValue();
                        warning = Convert.ToSingle(commonHddWarningTextBox.Text);
                        critical = Convert.ToSingle(commonHddCriticalTextBox.Text);
                        if (currentValue < warning)
                        {
                            if (currentValue < critical)
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                            }
                        }
                        else
                        {
                            commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                            commonMonitorTestDataUpdateResultStatus.Text = "OK";
                            commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                        }
                        break;
                    #endregion
                    #region MemoryUsage
                    case CommonMonitorType.MemoryUsage:
                        if (commonMemoryUsageTypeDdl.Text == "% In Use")
                        {
                            if (_commonMemoryUsageLastSelected != "% In Use")
                            {
                                ManagementScope scopeInUse = new ManagementScope(@"\\" + IpOrHostName);//, new ConnectionOptions());
                                scopeInUse.Connect();
                                ObjectQuery objectQueryInUse =
                                    new ObjectQuery(
                                        "SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
                                searcher = new ManagementObjectSearcher(scopeInUse, objectQueryInUse);
                                _commonMemoryUsage = searcher.Get();
                            }
                            foreach (ManagementObject item in _commonMemoryUsage)
                            {

                                _commonMemoryUsageLastSelected = "% In Use";
                                Single total = Convert.ToSingle(item["TotalVisibleMemorySize"]);
                                Single free = Convert.ToSingle(item["FreePhysicalMemory"]);
                                currentValue = ((total - free) / total) * 100f;
                            }
                            warning = Convert.ToSingle(commonMemoryWarningTextBox.Text);
                            critical = Convert.ToSingle(commonMemoryCriticalTextBox.Text);

                            if (currentValue > warning)
                            {
                                if (currentValue > critical)
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                }
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                            }
                        }
                        else
                        {
                            if (_commonMemoryUsageLastSelected != "Available")
                            {
                                ManagementScope scopeAvailable = new ManagementScope(@"\\" + IpOrHostName);//, new ConnectionOptions());
                                scopeAvailable.Connect();
                                ObjectQuery objectQueryAvailable =
                                    new ObjectQuery(
                                        "SELECT FreePhysicalMemory FROM Win32_OperatingSystem");
                                searcher = new ManagementObjectSearcher(scopeAvailable, objectQueryAvailable);
                                _commonMemoryUsage = searcher.Get();
                            }
                            foreach (ManagementObject item in _commonMemoryUsage)
                            {
                                _commonMemoryUsageLastSelected = "Available";
                                //Single total = Convert.ToSingle(item["TotalVisibleMemorySize"]);
                                currentValue = Convert.ToSingle(item["FreePhysicalMemory"]);
                                //currentValue = free;
                            }

                            warning = Convert.ToSingle(commonMemoryWarningTextBox.Text);
                            critical = Convert.ToSingle(commonMemoryCriticalTextBox.Text);

                            if (currentValue < warning)
                            {
                                if (currentValue < critical)
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                }
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                            }
                        }
                        break;
                    #endregion
                    #region ProcessState
                    case CommonMonitorType.ProcessState:
                        try
                        {
                            if (_commonPfcCounter == null ||
                            (_commonPfcCounter.CounterName != commonProcessStateUsageTypeDdl.Text ||
                            _commonPfcCounter.InstanceName != commonProcessStateProcessDd.Text ||
                            _commonPfcCounter.CategoryName != "Process" ||
                            (_commonPfcCounter.MachineName != IpOrHostName &&
                            IpOrHostName.ToLower() != Environment.MachineName.ToLower())))
                            {
                                if (_commonPfcCounter != null)
                                    _commonPfcCounter.Dispose();
                                _commonPfcCounter = new PerformanceCounter
                                                        {
                                                            CategoryName = "Process",
                                                            CounterName = commonProcessStateUsageTypeDdl.Text,
                                                            InstanceName = commonProcessStateProcessDd.Text,
                                                            MachineName = IpOrHostName
                                                        };
                                    //("Process", commonProcessStateUsageTypeDdl.Text, commonProcessStateProcessDd.Text, IpOrHostName);
                                _processRetry = true;
                            }
                            //currentValue = _commonProcessState.NextValue();
                            if (_processRetry)
                                currentValue = _commonPfcCounter.NextValue();
                            else
                                currentValue = NoProcess;
                            warning = Convert.ToSingle(commonProcessStateWarningTextBox.Text);
                            critical = Convert.ToSingle(commonProcessStateCriticalTextBox.Text);
                        }
                        catch (InvalidOperationException) 
                        { 
                            currentValue = NoProcess;
                            _processRetry = false;
                        }

                        if (currentValue == NoProcess)
                        {
                            commonMonitorTestDataUpdateResult.Text = "...";
                            commonMonitorTestDataUpdateResultStatus.Text = "Process not running.";
                            commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (commonProcessStateUsageTypeDdl.Text == "Elapsed Time")
                            {
                                if (currentValue < warning)
                                {
                                    if (currentValue < critical)
                                    {
                                        commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                        commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                        commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                        commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                        commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                    }
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                if (currentValue > warning)
                                {
                                    if (currentValue > critical)
                                    {
                                        commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                        commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                        commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                        commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                        commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                                    }
                                }
                                else
                                {
                                    commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                    commonMonitorTestDataUpdateResultStatus.Text = "OK";
                                    commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region ServiceState
                    case CommonMonitorType.ServiceState:
                        //Boolean threwexception = false;
                        
                        //try { String garbage = _commonServiceState.ServiceName; }
                        //catch (Exception) { threwexception = true; }

                        if (_commonServiceState == null || //threwexception ||
                            (_commonServiceState.ServiceName != commonServiceServicesChoiceDdl.Text ||
                            (_commonServiceState.MachineName != IpOrHostName &&
                            IpOrHostName.ToLower() != Environment.MachineName.ToLower())))
                        {
                            _commonServiceState = new ServiceController(commonServiceServicesChoiceDdl.Text,
                                                                        IpOrHostName);
                        }
                        commonMonitorTestDataUpdateResult.Text = _commonServiceState.Status.ToString();
                        if(_commonServiceState.Status == ServiceControllerStatus.Running)
                        {
                            commonMonitorTestDataUpdateResultStatus.Text = "OK";
                            commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                            commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                        }
                        break;
                    #endregion
                    #region SwapFileUsage
                    case CommonMonitorType.SwapFileUsage:
                        if (_commonPfcCounter == null ||
                            (_commonPfcCounter.CounterName != commonSwapFileUsageTypeDdl.Text ||
                            _commonPfcCounter.CategoryName != "Paging File" ||
                            (_commonPfcCounter.MachineName != IpOrHostName &&
                            IpOrHostName.ToLower() != Environment.MachineName.ToLower())))
                        {
                            if (_commonPfcCounter != null)
                                _commonPfcCounter.Dispose();
                            _commonPfcCounter = new PerformanceCounter
                                                    {
                                                        CategoryName = "Paging File",
                                                        CounterName = commonSwapFileUsageTypeDdl.Text,
                                                        InstanceName = "_Total",
                                                        MachineName = IpOrHostName
                                                    };
                            //("Paging File", commonSwapFileUsageTypeDdl.Text, "_Total", IpOrHostName);
                        }
                        //currentValue = _commonSwapFileUsage.NextValue();
                        currentValue = _commonPfcCounter.NextValue();
                        warning = Convert.ToSingle(commonSwapFileWarningTextBox.Text);
                        critical = Convert.ToSingle(commonSwapFileCriticalTextBox.Text);

                        if (currentValue > warning)
                        {
                            if (currentValue > critical)
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "Critical";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                                commonMonitorTestDataUpdateResultStatus.Text = "Warning";
                                commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Orange;
                            }
                        }
                        else
                        {
                            commonMonitorTestDataUpdateResult.Text = currentValue.ToString("f");
                            commonMonitorTestDataUpdateResultStatus.Text = "OK";
                            commonMonitorTestDataUpdateResultStatus.ForeColor = Color.Green;
                        }
                        break;
                    #endregion
                    case CommonMonitorType.None:
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                _testMonitorCommon = false;
                Logger.Instance.LogException(this.GetType(), ex);
            }
            finally
            {
                if(searcher != null)
                    searcher.Dispose();
            }
        }
        #endregion

        #region Delegates
        //private delegate void PopulateCommonMonitorDelegate();
        private delegate void PopulateCommonMonitorWithValuesDelegate(String[] population);
        #endregion
        #region Invokes
        private void CommonPopulateHddUsage(String[] population)
        {
            if (!alreadyPopulated && _commonType != CommonMonitorType.HddUsage)
            {
                commonHddDriveLetterDdl.Items.Clear();
                foreach (String s in population)
                    if (s != "_Total")
                        commonHddDriveLetterDdl.Items.Add(s);
            }
        }
        //private void CommonPopulateCpuUsage()
        //{
        //    commonCpuUsageTypeDdl.Items.Clear();
        //    commonCpuUsageTypeDdl.Items.AddRange(new[] {"% Idle Time", "% User Time"});
        //}
        //private void CommonPopulateMemoryUsage()
        //{
        //    commonMemoryUsageTypeDdl.Items.Clear();
        //    commonMemoryUsageTypeDdl.Items.AddRange(new[] {"Available MBytes", "% Committed Bytes In Use"});
        //}
        //private void CommonPopulateSwapFileUsage()
        //{
        //    commonSwapFileUsageTypeDdl.Items.Clear();
        //    commonSwapFileUsageTypeDdl.Items.AddRange(new[] {"% Usage", "% Usage Peak"});
        //}
        private void CommonPopulateServiceState(String[] population)
        {
            if (!alreadyPopulated && _commonType != CommonMonitorType.ServiceState)
            {
                commonServiceServicesChoiceDdl.Items.Clear();
                commonServiceServicesChoiceDdl.Items.AddRange(population);
            }
        }
        private void CommonPopulateProcessState(String[] population)
        {
            if (!alreadyPopulated && _commonType != CommonMonitorType.ProcessState)
            {
                commonProcessStateProcessDd.Items.Clear();
                commonProcessStateProcessDd.Items.AddRange(population);
            }
        }
        #endregion
    }
}