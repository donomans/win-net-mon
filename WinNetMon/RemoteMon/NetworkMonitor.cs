using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using RemoteMon_Lib;
using ThreadState = System.Threading.ThreadState;

//ಠ_ಠ 
///THIS NEEDS TO BE REDONE WITH ASYNC 

namespace RemoteMon
{
    public partial class NetworkMonitor : Form
    {
        private Thread _refreshThread;
        private readonly Thread _resultTimer;
        private readonly Thread _commandPoller;
        private readonly Thread _serviceControllerPoller;
        private readonly Thread _schedulerConfigurationPoller;
        private static ConfigurationData configurationData = null;
        private Int32 _gridViewIndex = -1;
        private ServiceController _networkMonitorServiceController = null;// = new ServiceController();
        //private AddServer addserver = null;
        private volatile static Boolean serviceActive = false;
        private volatile static Boolean ignoreServiceConfigRequest = false;
        private Boolean ServiceActive
        {
            get { return serviceActive; }
            set
            {
                if (value != serviceActive && value)
                {
                    Invoke(new UpdateLogDelegate(UpdateLog), "Service active.");
                    Logger.Instance.Log(this.GetType(), LogType.Info, "Service found active");
                    statusBarLabelMonitoringStatus.Text = "Monitoring Status: Remotely";
                }
                serviceActive = value;
            }
        }

        private volatile static Boolean configurationLoaded = false;
        private static volatile Boolean serviceLocal = false;
        private static ITalker talker = null;
        //private static Listener listener = null;

        private static DateTime lastMessage = DateTime.Now;

        private static Int64 counter = 0;

        private static volatile Boolean kill = false;

        private static volatile Boolean synced = false;
        private static Dictionary<String, String> syncDict = new Dictionary<String, String>();

        private readonly String _path = Environment.CurrentDirectory;
        private String _configPath;
        private String _logPath;

        public NetworkMonitor()
        {
            InitializeComponent();
            //NOTE: get configuration path and load configuration/settings if present
            _configPath = _path + @"\configuration.xml";
            _logPath = _path + @"\log.log";
            //1) need to try to get configuration
            //from file or from service
            //2) start the poller for service to check commands
            //3) need to start the scheduler if appropriate
            Logger.Instance.SetFileName(_logPath);

            _commandPoller = new Thread(GetCommands) { IsBackground = true };
            _commandPoller.Start();

            Log("Loading configuration data... ");
            Logger.Instance.Log(this.GetType(), LogType.Info, "Loading configuration data.");
            PopulateConfigurationData(true);


            Log("Checking service status... ");
            Logger.Instance.Log(this.GetType(), LogType.Info, "Checking service status.");
            _serviceControllerPoller = new Thread(ServiceControllerUpdate) { IsBackground = true };
            _serviceControllerPoller.Start();
            LogAppend("Done");

            PopulateGridView();

            _schedulerConfigurationPoller = new Thread(SchedulerConfigurationUpdate) { IsBackground = true };
            _schedulerConfigurationPoller.Start();

            _resultTimer = new Thread(RefreshResults) { IsBackground = true }; ;//, null, 500, 500);
            _resultTimer.Start();
        }

        #region Loops
        #region Results
        private void RefreshResults()//Object obj)
        {
            Thread.Sleep(30000); //sleep 30 seconds before trying to get any results
            while (!kill)
            {
                if (ServiceActive)
                {
                    if (talker != null && talker.Connected)
                    {
                        if (!synced)
                        {
                            talker.SendCommand(new Command { CommandType = Commands.ResultsSync, ToNamespace = Namespace.Service });
                            Thread.Sleep(10000);
                        }
                        else
                            talker.SendCommand(new Command { CommandType = Commands.GetResults, Data = counter, ToNamespace = Namespace.Service });
                    }
                }
                else
                {
                    synced = false;
                    List<IResult> results = new List<IResult>(MonitorScheduler.GetResults().ToEnumerable());
                    try
                    {
                        if (results.Count > 0 && this.IsHandleCreated) //nullreferenceexception? wat.
                            //this.Invoke(new UpdateGridViewRowsDelegate(UpdateDataGridViewWithResults), results);
                            UpdateDataGridViewWithResults(results);
                    }
                    catch (ObjectDisposedException)
                    {
                        //ignore this exception - can happen rarely when app is closed and the handle is destroyed between the check and the invoke call
                    }
                }
                Thread.Sleep(5000);
            }
        }
        private void UpdateDataGridViewWithResults(IEnumerable<IResult> results)
        {
            foreach (IResult result in results)
            {
                #region exceptions
                //if (result.Exception)
                //{
                //    SerializableException ex = (SerializableException)result.Value;
                //    Logger.Instance.LogMonitor
                //    //Logger.Instance.Log(this.GetType(), LogType.Monitor, "Monitor: " + configurationData[result.MonitorHash].FriendlyName + " threw an exception when attempting to check the status.");
                //    //switch (result.Type)
                //    //{
                //    //    case FullMonitorType.Basic:
                //    //        Logger.Instance.LogException(typeof(BasicMonitor), ex);
                //    //        break;
                //    //    case FullMonitorType.EventLog:
                //    //        Logger.Instance.LogException(typeof(EventMonitor), ex);
                //    //        break;
                //    //    case FullMonitorType.PerformanceCounter:
                //    //        Logger.Instance.LogException(typeof(PfcMonitor), ex);
                //    //        break;
                //    //    case FullMonitorType.Service:
                //    //        Logger.Instance.LogException(typeof(ServiceMonitor), ex);
                //    //        break;
                //    //    case FullMonitorType.Wmi:
                //    //        Logger.Instance.LogException(typeof(WmiMonitor), ex);
                //    //        break;
                //    //}
                //    //continue;
                //}
                #endregion

                DataGridViewRow row = null;
                String hash = "";
                if (synced)
                {
                    try
                    {
                        row = GetRow(syncDict[result.MonitorHash]);
                        hash = syncDict[result.MonitorHash];
                    }
                    catch(KeyNotFoundException)
                    {
                        synced = false;
                    }
                }
                else
                {
                    row = GetRow(result.MonitorHash);
                    hash = result.MonitorHash;
                }
                if (row != null && hash != "")
                {
                    switch (result.Type)
                    {
                        #region Basic
                        case FullMonitorType.Basic:
                            BasicResult br = (BasicResult)result;
                            BasicMonitor bm = (BasicMonitor)configurationData[FullMonitorType.Basic, hash];
                            if (bm != null)
                            {
                                if (serverMonitorList.InvokeRequired)
                                {
                                    serverMonitorList.Invoke(new MethodInvoker(() => row.SetValues(new Object[]
                                                                                                       {
                                                                                                           bm.Server,
                                                                                                           bm.FriendlyName,
                                                                                                           "Basic",
                                                                                                           br.Ok ? "OK" : "Panic",
                                                                                                           br.Exception ? "Unknown" : br.Value.ToString(),
                                                                                                           br.Exception
                                                                                                               ? "Check logs for details."
                                                                                                               : bm.ToString()
                                                                                                       })));
                                }
                                else
                                {
                                    row.SetValues(new Object[]
                                                      {
                                                          bm.Server,
                                                          bm.FriendlyName,
                                                          "Basic",
                                                          br.Ok ? "OK" : "Panic",
                                                          br.Exception ? "Unknown" : br.Value.ToString(),
                                                          br.Exception ? "Check logs for details." : bm.ToString()
                                                      });
                                }
                            }
                            break;
                        #endregion
                        #region EventLog
                        case FullMonitorType.EventLog:
                            EventResult er = (EventResult)result;
                            EventMonitor em = (EventMonitor)configurationData[FullMonitorType.EventLog, hash];
                            if (em != null)
                            {
                                if (serverMonitorList.InvokeRequired)
                                {
                                    serverMonitorList.Invoke(new MethodInvoker(() => row.SetValues(new Object[]
                                                                                                       {
                                                                                                           em.Server,
                                                                                                           em.FriendlyName,
                                                                                                           "Event Log",
                                                                                                           er.Ok ? "OK" : "Panic",
                                                                                                           er.Exception
                                                                                                               ? "Unknown"
                                                                                                               : ((EventResultMatches) er.Value).ToString(),
                                                                                                           er.Exception
                                                                                                               ? "Check logs for details."
                                                                                                               : em.ToString()
                                                                                                       })));

                                }
                                else
                                {
                                    row.SetValues(new Object[]
                                                      {
                                                          em.Server,
                                                          em.FriendlyName,
                                                          "Event Log",
                                                          er.Ok ? "OK" : "Panic",
                                                          er.Exception
                                                              ? "Unknown"
                                                              : ((EventResultMatches) er.Value).ToString(),
                                                          er.Exception ? "Check logs for details." : em.ToString()
                                                      });
                                }
                            }
                            break;
                        #endregion
                        #region PerformanceCounter
                        case FullMonitorType.PerformanceCounter:
                            PfcResult pr = (PfcResult)result;
                            PfcMonitor pm = (PfcMonitor)configurationData[FullMonitorType.PerformanceCounter, hash];
                            if (pm != null)
                            {
                                DataGridViewGraphCell cell = (DataGridViewGraphCell)row.Cells["serverMonitorListStatusValueColumn"];
                                cell.SetPanic(pm.ThresholdPanicSingle);
                                cell.SetWarning(pm.ThresholdWarningSingle);
                                cell.LessThan(pm.ThresholdLessThan);
                                cell.SetValue(pr.Value);
                                if (serverMonitorList.InvokeRequired)
                                {
                                    serverMonitorList.Invoke(new MethodInvoker(() => row.SetValues(new Object[]
                                                                                                       {
                                                                                                           pm.Server,
                                                                                                           pm.FriendlyName,
                                                                                                           "Performance Counter",
                                                                                                           pr.Ok ? "OK" : (pr.Critical ? "Panic" : "Warning"),
                                                                                                           pr.Exception ? "Unknown" : pr.Value,
                                                                                                           pr.Exception
                                                                                                               ? "Check logs for details."
                                                                                                               : pm.ToString()
                                                                                                       })));
                                }
                                else
                                {
                                    row.SetValues(new Object[]
                                                      {
                                                          pm.Server,
                                                          pm.FriendlyName,
                                                          "Performance Counter",
                                                          pr.Ok ? "OK" : (pr.Critical ? "Panic" : "Warning"),
                                                          pr.Exception ? "Unknown" : pr.Value,
                                                          pr.Exception ? "Check logs for details." : pm.ToString()
                                                      });
                                }
                            }
                            break;
                        #endregion
                        #region Services
                        case FullMonitorType.Service:
                            ServiceResult sr = (ServiceResult)result;
                            ServiceMonitor sm = (ServiceMonitor)configurationData[FullMonitorType.Service, hash];
                            if (sm != null)
                            {
                                if (serverMonitorList.InvokeRequired)
                                {
                                    serverMonitorList.Invoke(new MethodInvoker(() => row.SetValues(new Object[]
                                                                                                       {
                                                                                                           sm.Server,
                                                                                                           sm.FriendlyName,
                                                                                                           "Service",
                                                                                                           sr.Ok ? "OK" : "Panic",
                                                                                                           sr.Exception
                                                                                                               ? "Unknown"
                                                                                                               : ((ServiceResultStatuses) sr.Value).ToString(),
                                                                                                           sr.Exception
                                                                                                               ? "Check logs for details."
                                                                                                               : sm.ToString()
                                                                                                       })));
                                }
                                else
                                {
                                    row.SetValues(new Object[]
                                                      {
                                                          sm.Server,
                                                          sm.FriendlyName,
                                                          "Service",
                                                          sr.Ok ? "OK" : "Panic",
                                                          sr.Exception ? "Unknown" : ((ServiceResultStatuses) sr.Value).ToString(),
                                                          sr.Exception ? "Check logs for details." : sm.ToString()
                                                      });
                                }
                            }
                            break;
                        #endregion
                        #region Wmi
                        case FullMonitorType.Wmi:
                            WmiResult wr = (WmiResult)result;
                            WmiMonitor wm = (WmiMonitor)configurationData[FullMonitorType.Wmi, hash];
                            if (wm != null)
                            {
                                DataGridViewGraphCell cell = (DataGridViewGraphCell)row.Cells["serverMonitorListStatusValueColumn"];
                                cell.SetPanic(wm.ThresholdPanicSingle);
                                cell.SetWarning(wm.ThresholdWarningSingle);
                                cell.LessThan(wm.ThresholdLessThan);
                                cell.SetValue(wr.Value);
                                if (serverMonitorList.InvokeRequired)
                                {
                                    serverMonitorList.Invoke(new MethodInvoker(() => row.SetValues(new Object[]
                                                                                                       {
                                                                                                           wm.Server,
                                                                                                           wm.FriendlyName,
                                                                                                           "Wmi",
                                                                                                           wr.Ok ? "OK" : "Panic",
                                                                                                           wr.Exception ? "Unknown" : wr.Value,
                                                                                                           wr.Exception
                                                                                                               ? "Check logs for details."
                                                                                                               : wm.ToString()
                                                                                                       })));
                                }
                                else
                                {
                                    row.SetValues(new Object[]
                                                      {
                                                          wm.Server,
                                                          wm.FriendlyName,
                                                          "Wmi",
                                                          wr.Ok ? "OK" : "Panic",
                                                          wr.Exception ? "Unknown" : wr.Value,
                                                          wr.Exception ? "Check logs for details." : wm.ToString()
                                                      });
                                }
                            }
                            break;
                        #endregion
                    }
                    //serverMonitorList.Refresh();
                }
            }
        }
        private DataGridViewRow GetRow(String hash)
        {
            foreach (DataGridViewRow dataGridViewRow in serverMonitorList.Rows)
            {
                if (dataGridViewRow.Tag.ToString() == hash)
                {
                    return dataGridViewRow;
                }
            }
            return null;
        }
        #endregion

        #region Service Status
        private void ServiceControllerUpdate()
        {
            while (!kill)
            {
                ServiceControllerPoller();
                //GC.Collect();
                //Int64 total = GC.GetTotalMemory(false);
                //Int32 maxgen = GC.MaxGeneration;
                Thread.Sleep(60000); //check every minute
            }
        }
        private void ServiceControllerPoller()
        {
            if (configurationData == null)
                ServiceActive = false;
            else if (configurationData.Settings.RemoteServiceAddress != "")
            {
                try
                {
                    if (talker == null || !talker.Connected)
                    {
                        if(Listener.IsLocal(configurationData.Settings.RemoteServiceAddress))
                            talker = new MessageQueueTalker(Namespace.Client);
                        else
                            talker = new TcpTalker(configurationData.Settings.RemoteServiceAddress);
                    }

                    talker.SendCommand(new Command { CommandType = Commands.ServiceStatus, Data = null, ToNamespace = Namespace.Service });

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                    Invoke(new UpdateLogDelegate(UpdateLog), "Unable to connect to Service machine: network monitor service status unknown");
                    statusBarLabelServiceStatus.Text = "Service Status: Unknown";
                    ServiceActive = false;
                    synced = false;
                    statusBarLabelMonitoringStatus.Text = "Monitoring Status: Locally";
                }
            }
        }
        #endregion

        #region Commands
        private void GetCommands()
        {
            while (!kill)
            {
                if (talker != null)
                {
                    Command cmd = talker.GetCommand();
                    if (cmd != null)
                    {
                        lastMessage = DateTime.Now;
                        ServiceActive = true;
                        switch (cmd.CommandType)
                        {
                            case Commands.GetResultsResponse:
                                Results results = (Results)cmd.Data;
                                if (results != null)
                                {
                                    counter++;
                                    if (results.Count > 0)
                                        UpdateDataGridViewWithResults(results.ToEnumerable());
                                }
                                break;
                            case Commands.GetConfigurationResponse:
                                configurationData = (ConfigurationData)cmd.Data;
                                configurationLoaded = true;

                                if (serverMonitorList.InvokeRequired)
                                {
                                    Invoke(new MethodInvoker(() =>  serverMonitorList.Rows.Clear()));
                                }
                                else
                                    serverMonitorList.Rows.Clear();

                                PopulateGridView();
                                configurationData.ExportToXml("configuration.xml");
                                //NOTE: may need to convert this to some sort of overlay window that isn't a modal dialog box so it doesn't block this thread
                                MessageBox.Show("Configuration Retrived from the RemoteMon Service.", "Configuration Retrieved", MessageBoxButtons.OK);
                                break;
                            case Commands.UpdateConfigurationResponse:
                                if ((Boolean)cmd.Data)
                                    MessageBox.Show("Configuration Updated successfully.",
                                                    "Service Configuration Updated", MessageBoxButtons.OK);
                                break;
                            case Commands.ServiceStatus:
                                //Boolean oldValue = serviceActive;
                                ServiceActive = (Boolean)cmd.Data;
                                statusBarLabelServiceStatus.Text = "Service Status: " +
                                                                    (ServiceActive ? "Running" : "Stopped");

                                if (ServiceActive && MonitorScheduler.Scheduler.Running)
                                {
                                    Logger.Instance.Log(this.GetType(), LogType.Info, "Stopping Monitoring");
                                    MonitorScheduler.Scheduler.Kill();
                                }
                                break;
                            case Commands.GetConfiguration:
                                //if the service requests a configuration, then pop a dialog box asking whether or not to give it.
                                if (!ignoreServiceConfigRequest)
                                {
                                    DialogResult dr =
                                        MessageBox.Show(
                                            "The RemoteMon Service has requested your configuration, because it does not have any.  \n\r\n\rDo you want to upload your configuration to the service?",
                                            "Service needs a configuration", MessageBoxButtons.YesNo);
                                    if (dr == DialogResult.Yes)
                                    {
                                        talker.SendCommand(new Command { CommandType = Commands.GetConfigurationResponse, Data = configurationData, ToNamespace = cmd.FromNamespace, ToIp = cmd.FromIp });
                                        synced = false;
                                        ignoreServiceConfigRequest = false;
                                    }
                                    else
                                        ignoreServiceConfigRequest = true;
                                }
                                break;
                            case Commands.ResultsSyncResponse:
                                if (cmd.Data != null && configurationLoaded)
                                {
                                    SyncDatas servicedatas = (SyncDatas)cmd.Data;
                                    SyncDatas localdatas = new SyncDatas();
                                    counter = servicedatas.Counter;
                                    foreach (IMonitor monitor in configurationData.ToEnumerable())
                                    {
                                        localdatas.Add(new SyncData { FriendlyName = monitor.FriendlyName, GuidHash = monitor.Hash, IntHash = monitor.GetHashCode() });
                                    }
                                    syncDict.Clear();
                                    foreach (SyncData ld in localdatas)
                                    {
                                        //loop through each and create a dictionary of the guid from service to guid from local configuration
                                        SyncData local = ld;
                                        SyncData match = GetMatch(servicedatas, local);
                                        if (match != null)
                                            syncDict.Add(match.GuidHash, ld.GuidHash); //shouldn't be duplicates
                                    }
                                    //mark synced as true
                                    synced = true;
                                }
                                break;
                        }
                    }
                }
                Thread.Sleep(250);
            }
        }

        private SyncData GetMatch(SyncDatas servicedatas, SyncData local)
        {
            return servicedatas.Find(service =>
                                         {
                                             if (service.FriendlyName == local.FriendlyName
                                                 && service.IntHash == local.IntHash)
                                                 return true;
                                             else
                                                 return false;
                                         });
        }

        #endregion

        #region Scheduler
        private void SchedulerConfigurationUpdate()
        {
            Thread.Sleep(30000); //wait 30 seconds before trying to start the monitoring
            while (!kill)
            {
                if (configurationLoaded && !ServiceActive && !MonitorScheduler.Scheduler.Running)
                {
                    statusBarLabelMonitoringStatus.Text = "Monitoring Status: Locally";
                    Invoke(new UpdateLogDelegate(UpdateLog), "Starting monitors... ");
                    Logger.Instance.Log(this.GetType(), LogType.Info, "Starting monitors.");
                    MonitorScheduler.Scheduler.SetMonitors(configurationData, true);
                    Invoke(new UpdateLogDelegate(UpdateLog), "Done");
                }
                
                if (ServiceActive && MonitorScheduler.Scheduler.Running)
                    MonitorScheduler.Scheduler.Kill();

                if((lastMessage - DateTime.Now).TotalMinutes >= 1.5f && ServiceActive)
                {
                    ServiceActive = false;
                    //if service was active, but its not anywmore, log it
                    Invoke(new UpdateLogDelegate(UpdateLog), "Service inactive.");
                    Logger.Instance.Log(this.GetType(), LogType.Info, "Service found inactive or unresponsive");

                    Logger.Instance.Log(this.GetType(), LogType.Info, "Starting Monitoring");
                    MonitorScheduler.Scheduler.Start();
                }

                Thread.Sleep(1000);
            }
        }
        #endregion
        #endregion

        #region Configuration
        private void PopulateConfigurationData(Boolean retry)
        {
            if (!File.Exists(_configPath))
            {
                NoConfigurationFound noConfigurationFound = new NoConfigurationFound();
                DialogResult dialogResult = noConfigurationFound.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    if (noConfigurationFound.File)
                    {
                        _configPath = noConfigurationFound.FileName;
                    }
                    else
                    {
                        if (Listener.IsLocal(noConfigurationFound.IpOrHostName))
                            talker = new MessageQueueTalker(Namespace.Client);
                        else
                            talker = new TcpTalker(noConfigurationFound.IpOrHostName, connectPort: noConfigurationFound.Port);

                        talker.SendCommand(new Command { CommandType = Commands.GetConfiguration, Data = null, ToNamespace = Namespace.Service });
                        return;
                    }
                }
            }

            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                if (!File.Exists(_configPath))
                    //NOTE: Create a blank configuration.xml file
                    CreateConfigurationXml();

                configurationData = ConfigurationData.LoadConfiguration(_configPath);
                configurationLoaded = true;

                if (configurationData.Settings.ClientLogPath != "")
                {
                    _logPath = configurationData.Settings.ClientLogPath;
                    Logger.Instance.SetFileName(_logPath);
                }
            }
            catch (Exception ex)
            {
                if (retry && (ex.InnerException != null && ex.InnerException.Message == "Root element is missing."))
                {
                    File.Delete(_configPath); //NOTE: should I be doing this?
                    PopulateConfigurationData(false);
                }
                else
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                    MessageBox.Show("Error loading configuration file - check log for details.", "Error");
                }
            }

        }
        private void CreateConfigurationXml()
        {
            StreamWriter sw = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                sw = new StreamWriter(_configPath);
                XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                doc.AppendChild(declaration);
                XmlElement root = doc.CreateElement("Configuration");
                doc.AppendChild(root);
                doc.Save(sw);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                MessageBox.Show("Error encountered while loading configuration file - check log for details.", "Error");
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }
        private void PopulateGridView()
        {
            if (configurationData == null)
                return;

            foreach (ServiceMonitor serviceMonitor in configurationData.ServiceMonitors)
            {
                PopulateGridViewFromServerInfo(FullMonitorType.Service, serviceMonitor, false);
            }
            foreach (EventMonitor eventMonitor in configurationData.EventMonitors)
            {
                PopulateGridViewFromServerInfo(FullMonitorType.EventLog, eventMonitor, false);
            }
            foreach (PfcMonitor pfcMonitor in configurationData.PfcMonitors)
            {
                PopulateGridViewFromServerInfo(FullMonitorType.PerformanceCounter, pfcMonitor, false);
            }
            foreach (BasicMonitor basicMonitor in configurationData.BasicMonitors)
            {
                PopulateGridViewFromServerInfo(FullMonitorType.Basic, basicMonitor, false);
            }
            foreach (WmiMonitor wmiMonitor in configurationData.WmiMonitors)
            {
                PopulateGridViewFromServerInfo(FullMonitorType.Wmi, wmiMonitor, false);
            }
        }
        #endregion

        #region Form Log
        private void Log(String message)
        {
            logPane.Text += Environment.NewLine + message;
        }
        private void LogAppend(String message)
        {
            logPane.Text += message;
        }
        public static Boolean VerboseLogging { get { return configurationData.VerboseLogging; } }

        #endregion

        #region Add Server
        private void AddNewServerShow(FullMonitorType tab, MonitorBaseType baseTab)
        {
            AddServer addserver = new AddServer(String.Empty, String.Empty, tab, baseTab, configurationData.Settings.DefaultAlerts);
            AddOwnedForm(addserver);
            addserver.Show();
        }
        private void AddSelServerShow(FullMonitorType tab, MonitorBaseType baseTab)
        {
            AddServer addserver = new AddServer(serverBrowser.SelectedNode.Text, serverBrowser.SelectedNode.Text, tab,
                                                baseTab, configurationData.Settings.DefaultAlerts);
            AddOwnedForm(addserver);
            addserver.Show();
        }
        private void RepopulateSelServer(AddServer addserver)
        {
            AddOwnedForm(addserver);
            addserver.Show();
        }
        public void RepopulateSelServerResult(AddServer addserver)
        {
            if (addserver.DialogResult == DialogResult.OK)
            {
                IMonitor monitor = PopulateConfigurationDataFromServerInfo(addserver);
                PopulateGridViewFromServerInfo(addserver.ReturnValue, monitor, addserver.Tag != null ? true : false);
                _gridViewIndex = -1;
            }

            if (addserver.ToLog != "")
                Log(addserver.ToLog.TrimEnd(new[] { '\r', '\n' }));
        }
        private IMonitor PopulateConfigurationDataFromServerInfo(AddServer addServerForm)
        {
            List<Alert> alerts = new List<Alert>(2);
            //Boolean alertsExist = addServerForm.GetAlerts(out alerts);
            //alerts = 
            if (addServerForm.addServerAlertEmailOption.CheckState == CheckState.Checked)
            {
                EmailAlert ealert = new EmailAlert { Info = addServerForm.addServerAlertEmailTextBox.Text };
                if (!configurationData.Settings.IsDefaultAlert(ealert))
                    alerts.Add(ealert);
            }
            if (addServerForm.addServerAlertSmsOption.CheckState == CheckState.Checked)
            {
                SmsAlert salert = new SmsAlert { Info = addServerForm.addServerAlertSmsTextBox.Text };
                if (!configurationData.Settings.IsDefaultAlert(salert))
                    alerts.Add(salert);
            }

            Boolean alertsExist = alerts.Count > 0 && (addServerForm.addServerAlertEmailOption.Checked | addServerForm.addServerAlertSmsOption.Checked);
            try
            {
                switch (addServerForm.ReturnValue)
                {
                    case FullMonitorType.Basic:
                        #region Basic
                        BasicMonitor basicMonitor = addServerForm.GetBasicMonitor();
                        if (alertsExist)
                        {
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    basicMonitor.AlertInfo.Add(alert);
                                //}
                            }
                        }
                        //else
                        //{
                        //    basicMonitor.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                        //}
                        if (addServerForm.Tag == null)
                            configurationData.BasicMonitors.Add(basicMonitor);
                        else
                        {
                            configurationData.BasicMonitors.Remove(addServerForm.Tag.ToString());
                            configurationData.BasicMonitors.Add(basicMonitor);
                        }
                        configurationData.ExportToXml(_configPath);
                        MonitorScheduler.Scheduler.SetMonitors(configurationData);
                        return basicMonitor;
                        #endregion
                    case FullMonitorType.Common:
                        #region Common

                        /* 
                         * - Hard Drive Usage (dropdown list of C:\, D:\, etc)
                         * - Memory Usage
                         * - Swap/page file usage
                         * - CPU Usage
                         * - Process State (e.g. explorer.exe)
                         * - Service State (e.g. Intelligent Background Transfer Service)
                         */
                        List<Alert> validAlerts = new List<Alert>(2);
                        if (alertsExist)
                        {
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    validAlerts.Add(alert);
                                //}
                            }
                        }

                        switch (addServerForm.CommonType)
                        {
                            case CommonMonitorType.HddUsage: //Pfc
                                PfcMonitor hddUsage = (PfcMonitor) addServerForm.GetCommonMonitor();
                                hddUsage.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.PfcMonitors.Add(hddUsage);
                                else
                                {
                                    configurationData.PfcMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.PfcMonitors.Add(hddUsage);
                                }
                                //if (!alertsExist)
                                //{
                                //    hddUsage.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return hddUsage;
                            case CommonMonitorType.CpuUsage: //Pfc
                                PfcMonitor cpuUsage = (PfcMonitor) addServerForm.GetCommonMonitor();
                                cpuUsage.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.PfcMonitors.Add(cpuUsage);
                                else
                                {
                                    configurationData.PfcMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.PfcMonitors.Add(cpuUsage);
                                }
                                //if (!alertsExist)
                                //{
                                //    cpuUsage.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return cpuUsage;
                            case CommonMonitorType.MemoryUsage: //Wmi
                                WmiMonitor memoryUsage = (WmiMonitor) addServerForm.GetCommonMonitor();
                                memoryUsage.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.WmiMonitors.Add(memoryUsage);
                                else
                                {
                                    configurationData.WmiMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.WmiMonitors.Add(memoryUsage);
                                }
                                //if (!alertsExist)
                                //{
                                //    memoryUsage.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return memoryUsage;
                            case CommonMonitorType.ProcessState: //Pfc
                                PfcMonitor processState = (PfcMonitor) addServerForm.GetCommonMonitor();
                                processState.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.PfcMonitors.Add(processState);
                                else
                                {
                                    configurationData.PfcMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.PfcMonitors.Add(processState);
                                }
                                //if (!alertsExist)
                                //{
                                //    processState.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return processState;
                            case CommonMonitorType.ServiceState: //Service
                                ServiceMonitor serviceState = (ServiceMonitor) addServerForm.GetCommonMonitor();
                                serviceState.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.ServiceMonitors.Add(serviceState);
                                else
                                {
                                    configurationData.ServiceMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.ServiceMonitors.Add(serviceState);
                                }
                                //if (!alertsExist)
                                //{
                                //    serviceState.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return serviceState;
                            case CommonMonitorType.SwapFileUsage: //Pfc
                                PfcMonitor swapFileUsage = (PfcMonitor) addServerForm.GetCommonMonitor();
                                swapFileUsage.AlertInfo.AddRange(validAlerts);
                                if (addServerForm.Tag == null)
                                    configurationData.PfcMonitors.Add(swapFileUsage);
                                else
                                {
                                    configurationData.PfcMonitors.Remove(addServerForm.Tag.ToString());
                                    configurationData.PfcMonitors.Add(swapFileUsage);
                                }
                                //if (!alertsExist)
                                //{
                                //    swapFileUsage.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                                //}
                                configurationData.ExportToXml(_configPath);
                                MonitorScheduler.Scheduler.SetMonitors(configurationData);
                                return swapFileUsage;
                            case CommonMonitorType.None:
                            default:
                                Logger.Instance.Log(this.GetType(), LogType.Info,
                                                    "CommonType had an invalid value: unable to populate a Monitor correctly");
                                break;
                        }
                        return null;

                        #endregion
                    case FullMonitorType.EventLog:
                        #region Events
                        EventMonitor eventMonitor = addServerForm.GetEventMonitor();
                        if (alertsExist)
                        {
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    eventMonitor.AlertInfo.Add(alert);
                                //}
                            }
                        }
                        //else
                        //{
                        //    eventMonitor.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                        //}
                        if (addServerForm.Tag == null)
                            configurationData.EventMonitors.Add(eventMonitor);
                        else
                        {
                            configurationData.EventMonitors.Remove(addServerForm.Tag.ToString());
                            configurationData.EventMonitors.Add(eventMonitor);
                        }
                        configurationData.ExportToXml(_configPath);
                        MonitorScheduler.Scheduler.SetMonitors(configurationData);
                        return eventMonitor;
                        #endregion
                    case FullMonitorType.PerformanceCounter:
                        #region PerfCounters

                        PfcMonitor pfcMonitor = addServerForm.GetPfcMonitor();
                        if (alertsExist)
                        {    
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    pfcMonitor.AlertInfo.Add(alert);
                                //}
                            }
                        }
                        //else
                        //{
                        //    pfcMonitor.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                        //}
                        if (addServerForm.Tag == null)
                            configurationData.PfcMonitors.Add(pfcMonitor);
                        else
                        {
                            configurationData.PfcMonitors.Remove(addServerForm.Tag.ToString());
                            configurationData.PfcMonitors.Add(pfcMonitor);
                        }
                        configurationData.ExportToXml(_configPath);
                        MonitorScheduler.Scheduler.SetMonitors(configurationData);
                        return pfcMonitor;

                        #endregion
                    case FullMonitorType.Service:
                        #region Services

                        ServiceMonitor serviceMonitor = addServerForm.GetServiceMonitor();
                        if (alertsExist)
                        {
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    serviceMonitor.AlertInfo.Add(alert);
                                //}
                            }
                        }
                        //else
                        //{
                        //    serviceMonitor.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                        //}
                        if (addServerForm.Tag == null)
                            configurationData.ServiceMonitors.Add(serviceMonitor);
                        else
                        {
                            configurationData.ServiceMonitors.Remove(addServerForm.Tag.ToString());
                            configurationData.ServiceMonitors.Add(serviceMonitor);
                        }
                        configurationData.ExportToXml(_configPath);
                        MonitorScheduler.Scheduler.SetMonitors(configurationData);
                        return serviceMonitor;

                        #endregion
                    case FullMonitorType.Wmi:
                        #region Wmi (For future use)

                        WmiMonitor wmiMonitor = (WmiMonitor)addServerForm.GetCommonMonitor();
                        //NOTE: need a GetWmiMonitor() later on, potentially
                        if (alertsExist)
                        {    
                            foreach (Alert alert in alerts)
                            {
                                //if (!configurationData.Settings.IsDefaultAlert(alert))
                                //{
                                    wmiMonitor.AlertInfo.Add(alert);
                                //}
                            }
                        }
                        //else
                        //{
                        //    wmiMonitor.AlertInfo.AddRange(configurationData.Settings.DefaultAlerts);
                        //}
                        if (addServerForm.Tag == null)
                            configurationData.WmiMonitors.Add(wmiMonitor);
                        else
                        {
                            configurationData.WmiMonitors.Remove(addServerForm.Tag.ToString());
                            configurationData.WmiMonitors.Add(wmiMonitor);
                        }
                        configurationData.ExportToXml(_configPath);
                        MonitorScheduler.Scheduler.SetMonitors(configurationData);
                        return wmiMonitor;

                        #endregion
                    case FullMonitorType.None:
                    default:
                        return null;
                }

            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                MessageBox.Show(this, "Unable to save the Monitor, please check the log for details.",
                                "Error Saving Monitor", MessageBoxButtons.OK);
                return null;
            }
        }
        private void PopulateGridViewFromServerInfo(FullMonitorType returnValue, IMonitor monitor, Boolean repopulate)
        {
            if (monitor == null)
                return;
            Int32 row = 0;

            if (!repopulate)
            {
                if (serverMonitorList.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => row = serverMonitorList.Rows.Add()));
                }
                else
                    row = serverMonitorList.Rows.Add();
                _gridViewIndex = row;
            }
            else
                row = _gridViewIndex;

            switch (returnValue)
            {
                case FullMonitorType.Basic:
                    #region Basic
                    BasicMonitor basicMonitor = (BasicMonitor)monitor;
                    if (serverMonitorList.InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                                                     {
                                                         serverMonitorList.Rows[row].Tag = basicMonitor.Hash;
                                                         serverMonitorList.Rows[row].SetValues(new Object[]
                                                                                                   {
                                                                                                       basicMonitor.Server,
                                                                                                       basicMonitor.FriendlyName,
                                                                                                       "Basic",
                                                                                                       "OK",
                                                                                                       "0",
                                                                                                       basicMonitor.ToString()
                                                                                                   });
                                                     }));
                    }
                    else
                    {
                        serverMonitorList.Rows[row].Tag = basicMonitor.Hash;
                        serverMonitorList.Rows[row].SetValues(new Object[]
                                                              {
                                                                  basicMonitor.Server,
                                                                  basicMonitor.FriendlyName,
                                                                  "Basic",
                                                                  "OK",
                                                                  "0",
                                                                  basicMonitor.ToString()
                                                              });
                    }
                    break;
                    #endregion
                case FullMonitorType.Common:
                    #region Common
                    if (monitor.Type != FullMonitorType.Common) //NOTE: Don't want infinite loop somehow
                        PopulateGridViewFromServerInfo(monitor.Type, monitor, true);
                    else
                        Logger.Instance.Log(this.GetType(), LogType.Info, "Monitor Type was unindentifiable for monitor: " + monitor.ToString());
                    break;
                    #endregion
                case FullMonitorType.EventLog:
                    #region Events
                    EventMonitor eventMonitor = (EventMonitor)monitor;
                    if (serverMonitorList.InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                                                     {
                                                         serverMonitorList.Rows[row].Tag = eventMonitor.Hash;
                                                         serverMonitorList.Rows[row].SetValues(new Object[]
                                                                                                   {
                                                                                                       eventMonitor.Server,
                                                                                                       eventMonitor.FriendlyName,
                                                                                                       "Event Log",
                                                                                                       "OK",
                                                                                                       "0",
                                                                                                       eventMonitor.ToString()
                                                                                                   });
                                                     }));
                    }
                    else
                    {
                        serverMonitorList.Rows[row].Tag = eventMonitor.Hash;
                        serverMonitorList.Rows[row].SetValues(new Object[]
                                                                  {
                                                                      eventMonitor.Server,
                                                                      eventMonitor.FriendlyName,
                                                                      "Event Log",
                                                                      "OK",
                                                                      "0",
                                                                      eventMonitor.ToString()
                                                                  });
                    }
                    break;
                    #endregion
                case FullMonitorType.PerformanceCounter:
                    #region PerfCounters
                    PfcMonitor pfcMonitor = (PfcMonitor)monitor;
                    if (serverMonitorList.InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                                                     {
                                                         serverMonitorList.Rows[row].Tag = pfcMonitor.Hash;
                                                         serverMonitorList.Rows[row].SetValues(new Object[]
                                                                                                   {
                                                                                                       pfcMonitor.Server,
                                                                                                       pfcMonitor.FriendlyName,
                                                                                                       "Performance Counter",
                                                                                                       "OK",
                                                                                                       "0",
                                                                                                       pfcMonitor.ToString()
                                                                                                   });
                                                     }));
                    }
                    else
                    {
                        serverMonitorList.Rows[row].Tag = pfcMonitor.Hash;
                        serverMonitorList.Rows[row].SetValues(new Object[]
                                                                  {
                                                                      pfcMonitor.Server,
                                                                      pfcMonitor.FriendlyName,
                                                                      "Performance Counter",
                                                                      "OK",
                                                                      "0",
                                                                      pfcMonitor.ToString()
                                                                  });
                    }
                    break;
                    #endregion
                case FullMonitorType.Service:
                    #region Services
                    ServiceMonitor services = (ServiceMonitor)monitor;
                    if (serverMonitorList.InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                                                     {
                                                         serverMonitorList.Rows[row].Tag = services.Hash;
                                                         serverMonitorList.Rows[row].SetValues(new Object[]
                                                                                                   {
                                                                                                       services.Server,
                                                                                                       services.FriendlyName,
                                                                                                       "Service",
                                                                                                       "OK",
                                                                                                       "0",
                                                                                                       services.ToString()
                                                                                                   });
                                                     }));
                    }
                    else
                    {
                        serverMonitorList.Rows[row].Tag = services.Hash;
                        serverMonitorList.Rows[row].SetValues(new Object[]
                                                                  {
                                                                      services.Server,
                                                                      services.FriendlyName,
                                                                      "Service",
                                                                      "OK",
                                                                      "0",
                                                                      services.ToString()
                                                                  });
                    }
                    break;
                    #endregion
                case FullMonitorType.Wmi:
                    #region Wmi
                    WmiMonitor wmiMonitor = (WmiMonitor)monitor;
                    if (serverMonitorList.InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                                                     {
                                                         serverMonitorList.Rows[row].Tag = wmiMonitor.Hash;
                                                         serverMonitorList.Rows[row].SetValues(new Object[]
                                                                                                   {
                                                                                                       wmiMonitor.Server,
                                                                                                       wmiMonitor.FriendlyName,
                                                                                                       "Wmi",
                                                                                                       "OK",
                                                                                                       "0",
                                                                                                       wmiMonitor.ToString()
                                                                                                   });
                                                     }));
                    }
                    else
                    {
                        serverMonitorList.Rows[row].Tag = wmiMonitor.Hash;
                        serverMonitorList.Rows[row].SetValues(new Object[]
                                                                  {
                                                                      wmiMonitor.Server,
                                                                      wmiMonitor.FriendlyName,
                                                                      "Wmi",
                                                                      "OK",
                                                                      "0",
                                                                      wmiMonitor.ToString()
                                                                  });
                    }
                    break;
                    #endregion
                case FullMonitorType.None:
                default:
                    break;
            }
        }
        #endregion

        #region Invokes
        private void UpdateStatusBar(String message)
        {
            statusBarLabel.Text = message;
        }

        private void UpdateLog(String message)
        {
            Log(message);
        }

        private void ToggleNode(int node)
        {
            if (node < serverBrowser.Nodes.Count)
                serverBrowser.Nodes[node].Toggle();
            //else
            //    serverBrowser.Nodes[0].Toggle();
        }

        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
            serverMonitorList.Cursor = Cursors.Default;
        }
        #endregion

        #region Refresh Servers
        private void CancelRefreshToolStripMenuItemClick(object sender, EventArgs e)
        {
            //if (_refreshThread.IsAlive)
            //    _refreshThread.Abort();

            SetDefaultCursor();
            Log("Canceled Server List Refresh.");
        }

        private void RefreshServersToolStripMainMenuClick(object sender, EventArgs e)
        {
            RefreshServers();
        }
        private void AutomatedToolStripMenuItemClick(object sender, EventArgs e)
        {
            RefreshServers();
        }

        private void NetworkMonitorKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshServers();
            }
        }
        private void ServerBrowserKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshServers();
            }
        }

        private void RefreshServers()
        {
            if (_refreshThread == null)
                _refreshThread = new Thread(RefreshBg);
            else if (_refreshThread.IsAlive ||
                _refreshThread.ThreadState == ThreadState.Running ||
                _refreshThread.ThreadState == ThreadState.Stopped ||
                _refreshThread.ThreadState == ThreadState.Suspended ||
                _refreshThread.ThreadState == ThreadState.WaitSleepJoin)
            {
                _refreshThread.Abort();
                _refreshThread = new Thread(RefreshBg);
            }

            serverBrowser.Nodes.Clear();
            serverBrowser.Nodes.Add("Network Computers");
            Cursor = Cursors.WaitCursor;
            serverBrowser.Nodes[0].Expand();
            _refreshThread.Start();
        }
        private void RefreshBg()
        {
            try
            {
                Invoke(new UpdateLogDelegate(UpdateLog), new[] { "Updating Network Computer's list." });

                //NetworkBrowser nb = new NetworkBrowser();

                ArrayList al = NetworkBrowser.GetNetworkComputers();
                if (al != null)
                {
                    foreach (String s in al)
                    {
                        String ip = String.Empty;
                        IPHostEntry hostEntry;
                        try
                        {
                            hostEntry = Dns.GetHostEntry(s);
                            //IPAddress[] Addies = Dns.GetHostEntry(s).AddressList;
                            foreach (IPAddress addy in hostEntry.AddressList) //Addies)
                            {
                                //if (addy.AddressFamily != AddressFamily.InterNetworkV6 && !addy.IsIPv6LinkLocal &&
                                //    !addy.IsIPv6Multicast && !addy.IsIPv6SiteLocal)
                                ip = addy.ToString();
                            }
                            if (ip == String.Empty)
                                ip = hostEntry.AddressList[0].ToString();

                            Invoke(new UpdateServerBrowserDelegate(UpdateServerBrowser), new object[] { ip, hostEntry.HostName });
                        }
                        catch { }
                    }
                }
                //Invoke(new ToggleNodeDelegate(ToggleNode), new[] {0});
                Invoke(new UpdateLogDelegate(UpdateLog), new[] { "Network Computer list update complete." });
            }
            catch (ThreadAbortException) { }
            finally { Invoke(new DefaultCursorDelegate(SetDefaultCursor)); }
        }

        private void UpdateServerBrowser(String ip, String hostName)
        {
            serverBrowser.Nodes[0].Nodes.Add(hostName == "" ? ip : hostName);
            serverBrowser.Nodes[0].Nodes[serverBrowser.Nodes[0].Nodes.Count - 1].ToolTipText = ip;
            serverBrowser.Nodes[0].Nodes[serverBrowser.Nodes[0].Nodes.Count - 1].Tag = hostName;
        }

        private void ManualprovideIpRangeToolStripMenuItemClick(object sender, EventArgs e)
        {
            ManualIpSelection mis = new ManualIpSelection();
            try
            {
                IPAddress[] addies = Dns.GetHostAddresses(Environment.MachineName);
                foreach (IPAddress addy in addies)
                {
                    if (!addy.IsIPv6LinkLocal && !addy.IsIPv6Multicast && !addy.IsIPv6SiteLocal)
                    {
                        String[] splits = addy.ToString().Split(new[] { '.' });
                        if (splits.Length > 3)
                            splits[3] = "1";
                        else
                            continue;
                        mis.ManualIpSelection_StartRange_Txt.Text = String.Join(".", splits);
                        splits[3] = "255";
                        mis.ManualIpSelection_EndRange_Txt.Text = String.Join(".", splits);
                        break;
                    }
                }
                DialogResult dr = mis.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    Byte[] startAddress = IpFromDotString(mis.ManualIpSelection_StartRange_Txt.Text.Split(new[] { '.' }));
                    if (startAddress != null)
                    {
                        Byte[] endAddress = IpFromDotString(mis.ManualIpSelection_EndRange_Txt.Text.Split(new[] { '.' }));
                        if (endAddress != null)
                            ManualRefreshServers(startAddress, endAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }
            finally
            {
                mis.Dispose();
            }
        }
        private static Byte[] IpFromDotString(String[] ip)
        {
            if (ip.Length == 4)
            {
                byte[] ipbytes = new Byte[4];
                int x = 0;
                foreach (String s in ip)
                {
                    Byte b;
                    if (Byte.TryParse(s, out b))
                    {
                        ipbytes[x] = b;
                    }
                    else
                        return null;
                    x++;
                }
                return ipbytes;
            }
            return null;
        }
        private void ManualRefreshServers(Byte[] startRange, Byte[] endRange)
        {
            if (startRange.Length == 4 && endRange.Length == 4)
            {
                //NOTE: more control than Ping.SendAsync - this forces 10 at a time at most
                ThreadPool.SetMaxThreads(10, 10);
                for (int byte0 = startRange[0]; byte0 <= endRange[0]; byte0++)
                {
                    for (int byte1 = startRange[1]; byte1 <= endRange[1]; byte1++)
                    {
                        for (int byte2 = startRange[2]; byte2 <= endRange[2]; byte2++)
                        {
                            for (int byte3 = startRange[3]; byte3 <= endRange[3]; byte3++)
                            {
                                String ip = byte0 + "." +
                                            byte1 + "." +
                                            byte2 + "." +
                                            byte3;
                                ThreadPool.QueueUserWorkItem(ManualPingServer, ip);
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show(this, "Invalid IP addresses provided", "Invalid IP", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }
        private void ManualPingServer(object ipAddress)
        {
            String ip = ipAddress.ToString();
            Invoke(new UpdateStatusBarDelegate(UpdateStatusBar), "Pinging " + ip);
            //Invoke(new UpdateLogDelegate(UpdateLog), "Pinging " + ip);
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(ip);
                if (pr != null && pr.Status == IPStatus.Success)
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                    if (hostEntry.HostName != "")
                        Invoke(new UpdateServerBrowserDelegate(UpdateServerBrowser), new[] { ip, hostEntry.HostName });
                    else
                        Invoke(new UpdateServerBrowserDelegate(UpdateServerBrowser), new[] { ip, ip });
                }
                //Invoke(new UpdateStatusBarDelegate(UpdateStatusBar), "Finished pinging: " + ip);
            }
            catch (Exception)
            {
                Invoke(new UpdateLogDelegate(UpdateLog), "Unable to access IP: " + ip);
                //Invoke(new UpdateStatusBarDelegate(UpdateStatusBar), "Unsuccessful ping of: " + ip);
                Logger.Instance.Log(this.GetType(), LogType.Info, "Unable to access IP: " + ip);
            }
            finally
            {
                p.Dispose();
            }
        }
        #endregion

        #region Delegates
        #region Nested type: UpdateLogDelegate
        private delegate void UpdateLogDelegate(String message);
        #endregion
        #region Nested type: UpdateServerBrowserDelegate
        private delegate void UpdateServerBrowserDelegate(String ip, String hostName);
        #endregion
        #region Nested type: UpdateServersBrowserDelegate
        private delegate void UpdateServersBrowserDelegate(String[] ips, String hostNames);
        #endregion
        #region Nested type: ToggleNodeDelegate
        private delegate void ToggleNodeDelegate(int node);
        #endregion
        #region Nested type: DefaultCursorDelegate
        private delegate void DefaultCursorDelegate();
        #endregion
        #region Nested type: UpdateStatusBarDelegate
        private delegate void UpdateStatusBarDelegate(String message);
        #endregion
        #region Nested type: UpdateGridViewRowsDelegate
        private delegate void UpdateGridViewRowsDelegate(List<IResult> results);
        #endregion
        #endregion

        #region Events

        private void ServerMonitorListDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #region LogPane
        private void LogPaneMouseEnter(object sender, EventArgs e)
        {

            networkMonitorBaseContainer.SplitterDistance = networkMonitorBaseContainer.Height - (networkMonitorBaseContainer.Height / 3);
        }
        private void LogPaneMouseLeave(object sender, EventArgs e)
        {
            networkMonitorBaseContainer.SplitterDistance = networkMonitorBaseContainer.Height - 25;
        }
        private void ClearLogWindowToolStripMenuItemClick(object sender, EventArgs e)
        {
            logPane.Clear();
        }
        #endregion

        #region Configuration Save/Load/Push
        private void SaveConfigurationToolStripMenuItemClick(object sender, EventArgs e)
        {
            DialogResult dr = saveConfigurationFd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String fileName = saveConfigurationFd.FileName;
                XmlExport.Export(fileName, configurationData);

                //try
                //{
                //    xmlExport.
                //}
                //catch (Exception ex)
                //{
                //    Logger.Instance.LogException(this.GetType(), ex);
                //}
            }
        }
        private void LoadConfigurationToolStripMenuItemClick(object sender, EventArgs e)
        {
            DialogResult dr = loadConfigurationFd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    configurationData = ConfigurationData.LoadConfiguration(loadConfigurationFd.FileName);
                    configurationLoaded = true;
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                    configurationLoaded = false;
                    MessageBox.Show("Please confirm the file location and format, and try again.", "Load Configuration Failed", MessageBoxButtons.OK);
                }

                if (!ServiceActive)
                    MonitorScheduler.Scheduler.SetMonitors(configurationData);
            }
        }
        private void PushConfigurationToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ServiceActive)
            {
                if (talker != null)
                {
                    talker.SendCommand(new Command { CommandType = Commands.UpdateConfiguration, Data = configurationData, ToNamespace = Namespace.Service });
                }
            }
            else
            {
                MessageBox.Show(
                    "Service not found, please make sure the service is running and the Ip/Hostname and Port are set in the Settings",
                    "Cannot find service", MessageBoxButtons.OK);
            }
        }
        private void GetConfigurationToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ServiceActive)
            {
                if (talker != null)
                {
                    talker.SendCommand(new Command { CommandType = Commands.GetConfiguration, ToNamespace = Namespace.Service });
                }
            }
            else
            {
                MessageBox.Show(
                    "Service not found, please make sure the service is running and the Ip/Hostname and Port are set in the Settings",
                    "Cannot find service", MessageBoxButtons.OK);
            }
        }
        #endregion

        private void SettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            try
            {
                if (configurationData.Settings.DefaultAlerts.Count > 0)
                {
                    foreach (Alert alert in configurationData.Settings.DefaultAlerts)
                    {
                        switch (alert.Type)
                        {
                            case AlertType.Email:
                                EmailAlert emailAlert = ((EmailAlert)alert);
                                settings.settingsAlertEmailToTextBox.Text = emailAlert.Info;
                                settings.settingsMailServerAddressTextBox.Text = emailAlert.EmailServerHostName;
                                settings.settingsAlertEmailFromTextBox.Text = emailAlert.EmailAddressFrom;
                                settings.settingsAlertEmailAccountTextBox.Text = emailAlert.EmailUserName;
                                settings.settingsAlertEmailPasswordTextBox.Text = emailAlert.EmailUserPass;
                                settings.settingsMailServerPortMaskedTextBox.Text = emailAlert.EmailServerPort != -1 ? emailAlert.EmailServerPort.ToString() : "";
                                settings.settingsMailServerSslCheckBox.Checked = emailAlert.UseSsl;
                                break;
                            case AlertType.Phone:
                                SmsAlert smsAlert = ((SmsAlert)alert);
                                settings.settingsAlertSmsTextBox.Text = smsAlert.Info;
                                settings.settingsiSmsAddressTextBox.Text = smsAlert.SmsServer;
                                settings.settingsiSmsUserTextBox.Text = smsAlert.UserName;
                                settings.settingsiSmsPassTextBox.Text = smsAlert.Password;
                                break;
                            default:
                                break; //NOTE: impossible
                        }
                    }
                }

                settings.settingsVerboseLoggingCheckBox.Checked = configurationData.VerboseLogging;

                if (configurationData.Settings.ClientLogPath != "")
                    settings.settingsLoggingClientPathTextBox.Text = configurationData.Settings.ClientLogPath;
                if (configurationData.Settings.ServiceLogPath != "")
                    settings.settingsLoggingServicePathTextBox.Text = configurationData.Settings.ServiceLogPath;

                if (configurationData.Settings.RemoteServerAddress != "")
                    settings.settingsRemoteServerTextBox.Text =
                        configurationData.Settings.RemoteServerAddress;
                if (configurationData.Settings.RemoteServiceAddress != "")
                    settings.settingsRemoteMonServiceAddressTextBox.Text =
                        configurationData.Settings.RemoteServiceAddress;
                if (configurationData.Settings.RemoteServicePort != "")
                    settings.settingsRemoteMonServicePortTextBox.Text = configurationData.Settings.RemoteServicePort;


                DialogResult dialogResult = settings.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    //NOTE: need to save the settings somewhere and load them on startup
                    if (settings.settingsAlertEmailToTextBox.Text != "")
                    {
                        configurationData.Settings.DefaultAlerts.RemoveType(AlertType.Email);

                        configurationData.Settings.DefaultAlerts.Add(new EmailAlert
                        {
                            Info = settings.settingsAlertEmailToTextBox.Text,
                            EmailServerHostName =
                                settings.settingsMailServerAddressTextBox.Text,
                            EmailAddressFrom = settings.settingsAlertEmailFromTextBox.Text,
                            EmailServerPort =
                                (settings.settingsMailServerPortMaskedTextBox.Text != ""
                                     ? Convert.ToInt32(
                                         settings.settingsMailServerPortMaskedTextBox.ValidateText())
                                                                                      : -1),
                            EmailUserName = settings.settingsAlertEmailAccountTextBox.Text,
                            EmailUserPass = settings.settingsAlertEmailPasswordTextBox.Text,
                            UseSsl = settings.settingsMailServerSslCheckBox.Checked
                        });
                    }
                    if (settings.settingsAlertSmsTextBox.Text != "")
                    {
                        configurationData.Settings.DefaultAlerts.RemoveType(AlertType.Email);
                        configurationData.Settings.DefaultAlerts.Add(new SmsAlert
                        {
                            Info = settings.settingsAlertSmsTextBox.Text,
                            Password = settings.settingsiSmsPassTextBox.Text,
                            SmsServer = settings.settingsiSmsAddressTextBox.Text,
                            UserName = settings.settingsiSmsUserTextBox.Text
                        });
                    }

                    configurationData.VerboseLogging = settings.settingsVerboseLoggingCheckBox.Checked;
                    Logger.Instance.Verbose = settings.settingsVerboseLoggingCheckBox.Checked;

                    configurationData.Settings.RemoteServerAddress =
                        settings.settingsRemoteServerTextBox.Text;
                    configurationData.Settings.RemoteServiceAddress =
                        settings.settingsRemoteMonServiceAddressTextBox.Text;
                    configurationData.Settings.RemoteServicePort = settings.settingsRemoteMonServicePortTextBox.Text;

                    //NOTE: whenever log path changes, the Logger needs to reflect that
                    configurationData.Settings.ClientLogPath = settings.settingsLoggingClientPathTextBox.Text == ""
                                                                   ? _logPath
                                                                   : settings.settingsLoggingClientPathTextBox.Text;
                    _logPath = settings.settingsLoggingClientPathTextBox.Text;
                    Logger.Instance.SetFileName(settings.settingsLoggingClientPathTextBox.Text == ""
                                                    ? _logPath
                                                    : settings.settingsLoggingClientPathTextBox.Text);

                    configurationData.Settings.ServiceLogPath = settings.settingsLoggingServicePathTextBox.Text;
                    //NOTE: no way to tell where - default to some folder in the service, if this is blank.



                    if (!configurationData.ExportToXml(_configPath))
                    {
                        Logger.Instance.Log(this.GetType(), LogType.Info,
                                            @"Failed to save settings: Configuration Data or Filename was empty - " +
                                            @"make sure you have permissions to the [Current User]\AppData\Roaming\RemoteMon directory.  " +
                                            @"If this problem persists, delete the configuration.xml file in that directory and try again.");
                        MessageBox.Show("Failed to save settings: Configuration Data or Filename was empty", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                MessageBox.Show(this, "Unable to save or load settings, please check log for details.", "Error",
                                MessageBoxButtons.OK);
            }
            finally
            {
                settings.Dispose();
            }
        }

        private void ServerMonitorListCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            String hash = serverMonitorList.Rows[e.RowIndex].Tag.ToString();
            _gridViewIndex = e.RowIndex;
            IMonitor monitor = configurationData[hash];
            switch (monitor.Type)
            {
                case FullMonitorType.Basic:
                    #region Basic
                    BasicMonitor basic = (BasicMonitor)monitor;
                    AddServer basicAddServer;
                    if (basic.Common)
                    {

                        if (basic.AlertInfo.Count > 0)
                            basicAddServer = new AddServer(basic.FriendlyName, String.Empty, FullMonitorType.Basic,
                                                         MonitorBaseType.Basic, basic.AlertInfo, basic);
                        else
                            basicAddServer = new AddServer(basic.FriendlyName, String.Empty, FullMonitorType.Basic,
                                                         MonitorBaseType.Basic,
                                                         configurationData.Settings.DefaultAlerts, basic);
                    }
                    else
                    {
                        if (basic.AlertInfo.Count > 0)
                            basicAddServer = new AddServer(basic.FriendlyName, String.Empty, FullMonitorType.Basic,
                                                           MonitorBaseType.Basic, basic.AlertInfo);
                        else
                            basicAddServer = new AddServer(basic.FriendlyName, String.Empty, FullMonitorType.Basic,
                                                           MonitorBaseType.Basic,
                                                           configurationData.Settings.DefaultAlerts, basic);
                    }
                    basicAddServer.Tag = hash;
                    RepopulateSelServer(basicAddServer);
                    break;
                    #endregion
                case FullMonitorType.EventLog:
                    #region EventLog
                    EventMonitor events = (EventMonitor)monitor;
                    AddServer eventAddServer;
                    if (events.Common)
                    {
                        if (events.AlertInfo.Count > 0)
                            eventAddServer = new AddServer(events.FriendlyName, events.Server, FullMonitorType.EventLog,
                                                         MonitorBaseType.Advanced, events.AlertInfo, events);
                        else
                            eventAddServer = new AddServer(events.FriendlyName, events.Server, FullMonitorType.EventLog,
                                                         MonitorBaseType.Advanced,
                                                         configurationData.Settings.DefaultAlerts, events);
                    }
                    else
                    {
                        if (events.AlertInfo.Count > 0)
                            eventAddServer = new AddServer(events.FriendlyName, events.Server, FullMonitorType.EventLog,
                                                           MonitorBaseType.Advanced, events.AlertInfo, events);
                        else
                            eventAddServer = new AddServer(events.FriendlyName, events.Server, FullMonitorType.EventLog,
                                                           MonitorBaseType.Advanced,
                                                           configurationData.Settings.DefaultAlerts, events);
                    }
                    eventAddServer.Tag = hash;
                    RepopulateSelServer(eventAddServer);
                    break;
                    #endregion
                case FullMonitorType.PerformanceCounter:
                    #region PerformanceCounter
                    PfcMonitor pfc = (PfcMonitor)monitor;
                    AddServer pfcAddServer;
                    if (pfc.Common)
                    {
                        if (pfc.AlertInfo.Count > 0)
                            pfcAddServer = new AddServer(pfc.FriendlyName, pfc.Server, FullMonitorType.PerformanceCounter,
                                                         MonitorBaseType.Common, pfc.AlertInfo, pfc);
                        else
                            pfcAddServer = new AddServer(pfc.FriendlyName, pfc.Server, FullMonitorType.PerformanceCounter,
                                                         MonitorBaseType.Common,
                                                         configurationData.Settings.DefaultAlerts, pfc);
                    }
                    else
                    {
                        if (pfc.AlertInfo.Count > 0)
                            pfcAddServer = new AddServer(pfc.FriendlyName, pfc.Server,
                                                         FullMonitorType.PerformanceCounter,
                                                         MonitorBaseType.Advanced, pfc.AlertInfo, pfc);
                        else
                            pfcAddServer = new AddServer(pfc.FriendlyName, pfc.Server,
                                                         FullMonitorType.PerformanceCounter,
                                                         MonitorBaseType.Advanced,
                                                         configurationData.Settings.DefaultAlerts, pfc);
                    }
                    pfcAddServer.Tag = hash;
                    RepopulateSelServer(pfcAddServer);
                    break;
                    #endregion
                case FullMonitorType.Service:
                    #region Service
                    ServiceMonitor service = (ServiceMonitor)monitor;
                    AddServer serviceAddServer;
                    if (service.Common)
                    {
                        if (service.AlertInfo.Count > 0)
                            serviceAddServer = new AddServer(service.FriendlyName, service.Server,
                                                             FullMonitorType.Service,
                                                             MonitorBaseType.Common, service.AlertInfo, service);
                        else
                            serviceAddServer = new AddServer(service.FriendlyName, service.Server,
                                                             FullMonitorType.Service,
                                                             MonitorBaseType.Common,
                                                             configurationData.Settings.DefaultAlerts, service);
                    }
                    else
                    {
                        if (service.AlertInfo.Count > 0)
                            serviceAddServer = new AddServer(service.FriendlyName, service.Server,
                                                             FullMonitorType.Service,
                                                             MonitorBaseType.Advanced, service.AlertInfo, service);
                        else
                            serviceAddServer = new AddServer(service.FriendlyName, service.Server,
                                                             FullMonitorType.Service,
                                                             MonitorBaseType.Advanced,
                                                             configurationData.Settings.DefaultAlerts, service);
                    }
                    serviceAddServer.Tag = hash;
                    RepopulateSelServer(serviceAddServer);
                    break;
                    #endregion
                case FullMonitorType.Wmi:
                    #region Wmi
                    WmiMonitor wmi = (WmiMonitor)monitor;
                    AddServer wmiAddServer;
                    if (wmi.Common)
                    {
                        if (wmi.AlertInfo.Count > 0)
                            wmiAddServer = new AddServer(wmi.FriendlyName, wmi.Server, FullMonitorType.Wmi,
                                                         MonitorBaseType.Common, wmi.AlertInfo, wmi);
                        else
                            wmiAddServer = new AddServer(wmi.FriendlyName, wmi.Server, FullMonitorType.Wmi,
                                                         MonitorBaseType.Common,
                                                         configurationData.Settings.DefaultAlerts, wmi);
                    }
                    else
                    {
                        if (wmi.AlertInfo.Count > 0)
                            wmiAddServer = new AddServer(wmi.FriendlyName, wmi.Server, FullMonitorType.Wmi,
                                                         MonitorBaseType.Common, wmi.AlertInfo, wmi);
                        else
                            wmiAddServer = new AddServer(wmi.FriendlyName, wmi.Server, FullMonitorType.Wmi,
                                                         MonitorBaseType.Common,
                                                         configurationData.Settings.DefaultAlerts, wmi);
                    }
                    wmiAddServer.Tag = hash;
                    RepopulateSelServer(wmiAddServer);
                    break;
                    #endregion
            }
        }

        private void ServerMonitorListUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                configurationData.RemoveMonitor(e.Row.Tag.ToString());
                configurationData.ExportToXml(_configPath);
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                e.Cancel = true;
                MessageBox.Show("Could not delete the row from the configuration.  Please see the logs for more information.", 
                                "Error deleting row", MessageBoxButtons.OK);
            }
        }

        #region Server Browser
        private void ServerBrowserNodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node.Text != e.Node.TreeView.Nodes[0].Text)
                statusBarLabel.Text = "Name: " + e.Node.Text + ", IP: " + e.Node.ToolTipText;
        }
        private void ServerBrowserNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                serverBrowser.SelectedNode = e.Node;
        }
        private void ServerBrowserNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text != "Network Computers")
            {
                serverBrowser.SelectedNode = e.Node;
                AddSelServerShow(FullMonitorType.Common, MonitorBaseType.Common);
            }
        }
        #endregion
        #region Add Server ToolStrip
        private void AddSelServerToolStripAddServerClick(object sender, EventArgs e)
        {
            AddSelServerShow(FullMonitorType.None, MonitorBaseType.Common);
        }
        private void ServiceMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddSelServerShow(FullMonitorType.Service, MonitorBaseType.Advanced);
        }
        private void EventMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddSelServerShow(FullMonitorType.EventLog, MonitorBaseType.Advanced);
        }
        private void BasicMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddSelServerShow(FullMonitorType.Basic, MonitorBaseType.Basic);
        }
        private void PerfMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddSelServerShow(FullMonitorType.PerformanceCounter, MonitorBaseType.Advanced);
        }
        private void AddNewServerToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewServerShow(FullMonitorType.Common, MonitorBaseType.Common);
        }
        private void AsServiceMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewServerShow(FullMonitorType.Service, MonitorBaseType.Advanced);
        }
        private void AsEventMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewServerShow(FullMonitorType.EventLog, MonitorBaseType.Advanced);
        }
        private void AsBasicMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewServerShow(FullMonitorType.Basic, MonitorBaseType.Basic);
        }
        private void AsPerfMonitorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewServerShow(FullMonitorType.PerformanceCounter, MonitorBaseType.Advanced);
        }
        #endregion

        #region Form
        private void NetworkMonitorFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_networkMonitorServiceController != null)
                _networkMonitorServiceController.Dispose();
            kill = true;
            
            MonitorScheduler.Scheduler.Kill();

            if (_refreshThread != null && _refreshThread.IsAlive)
                _refreshThread.Join(200);
            if (_resultTimer != null && _resultTimer.IsAlive)
                _resultTimer.Join(200);
            if (_commandPoller != null && _commandPoller.IsAlive)
                _commandPoller.Join(200);
            if (_serviceControllerPoller != null && _serviceControllerPoller.IsAlive)
                _serviceControllerPoller.Join(200);
            if (_schedulerConfigurationPoller != null && _schedulerConfigurationPoller.IsAlive)
                _schedulerConfigurationPoller.Join(200);

            DataGridViewGraphCell.Dispose();

            Logger.Instance.LogImmediate();
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (NativeMethods.DwmIsCompositionEnabled())
                {
                    NativeMethods.MARGINS m = new NativeMethods.MARGINS(0, 0, 0, 0);
                    NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, m);
                }
            }
            catch (Exception)
            {
                //NOTE: if it fails, it fails - its probably Windows XP
            }

            base.OnLoad(e);
        }
        #endregion
       
        #endregion
    }
}