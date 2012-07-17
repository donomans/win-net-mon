using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using RemoteMon_Lib;

namespace RemoteMon_Service
{
    internal class CommandChecker: IDisposable
    {
        private Thread _check;
        private static volatile Boolean kill = false;
        private readonly Listener _listener;
        private readonly Int32 _port;
        private readonly String _configPath;


        public CommandChecker(Int32 port)
        {
            if (Environment.CurrentDirectory.Contains("system32"))
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon");
                _configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon\configuration.xml";
            }
            else
                _configPath = "configuration.xml";

            _listener = new Listener(port);
            _port = _listener.Port;
        }
        public CommandChecker()
        {
            if (Environment.CurrentDirectory.Contains("system32"))
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon");
                _configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon\configuration.xml";
            }
            else
                _configPath = "configuration.xml";

            _listener = new Listener();
            _port = _listener.Port;
        }

        public Int32 Port { get { return _port; } }

        public void SendCommandToAll(Command cmd)
        {
            _listener.SendCommandToAll(cmd);
        }

        public void Start()
        {
            if (_check == null)
                _check = new Thread(Check);
            else if (_check.IsAlive)
            {
                _check.Join(50);
                _check = new Thread(Check);
            }

            _check.Start();
        }
        public void Stop()
        {
            Logger.Instance.Log(this.GetType(), LogType.Info, "Command checker stopping.");
            kill = true;
            _check.Join(100);
            _listener.Dispose();
        }

        private void Check()
        {
            while (!kill)            
            {
                  List<Command> commands = new List<Command>(_listener.GetCommands());
                foreach (Command cmd in commands)
                {
                    try
                    {
                        switch (cmd.CommandType)
                        {
                            case Commands.GetResults:
                                Int64 lastcount = Convert.ToInt64(cmd.Data);
                                if (lastcount < ResultChecker.GetLastResultCounter(cmd.FromIp, cmd.FromNamespace))
                                {
                                    Results results = ResultChecker.GetLatestResults(cmd.FromIp, cmd.FromNamespace);
                                    if (results != null && results.Count > 0)
                                    {
                                        Command cmdResults = new Command
                                                                    {
                                                                        CommandType = Commands.GetResultsResponse,
                                                                        Data = results,
                                                                        ToNamespace = cmd.FromNamespace,
                                                                        ToIp = cmd.FromIp
                                                                    };
                                        _listener.SendCommand(cmdResults);
                                        ResultChecker.ClearResults(cmd.FromIp, cmd.FromNamespace);
                                    }
                                }
                                break;
                            case Commands.StartScheduler:
                                MonitorScheduler.Scheduler.SetMonitors(NetworkMonitor.Configuration);
                                MonitorScheduler.Scheduler.Start();
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.SchedulerStatus,
                                                                Data = MonitorScheduler.Scheduler.Running,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.StopScheduler:
                                MonitorScheduler.Scheduler.Kill();
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.SchedulerStatus,
                                                                Data = MonitorScheduler.Scheduler.Running,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.SchedulerStatus:
                                //need enum to show status better
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.SchedulerStatus,
                                                                Data = MonitorScheduler.Scheduler.Running,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.ServiceStatus:
                                //need enum to show status better
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.ServiceStatus,
                                                                Data = true,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.UpdateConfiguration:
                                if (cmd.Data != null)
                                {
                                    NetworkMonitor.SetConfiguration((ConfigurationData) cmd.Data);
                                    //NetworkMonitor.Configuration = (ConfigurationData)cmd.Data;
                                    //save it once i get it.
                                    NetworkMonitor.Configuration.ExportToXml(_configPath);
                                }
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.UpdateConfigurationResponse,
                                                                Data = true,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.GetConfiguration:
                                _listener.SendCommand(new Command
                                                            {
                                                                CommandType = Commands.GetConfigurationResponse,
                                                                Data = NetworkMonitor.Configuration,
                                                                ToNamespace = cmd.FromNamespace,
                                                                ToIp = cmd.FromIp
                                                            });
                                break;
                            case Commands.GetConfigurationResponse:
                                if (cmd.Data != null)
                                {
                                    //NetworkMonitor.Configuration = (ConfigurationData)cmd.Data;
                                    NetworkMonitor.SetConfiguration((ConfigurationData) cmd.Data);
                                    //save it once i get it.
                                    NetworkMonitor.Configuration.ExportToXml(_configPath);
                                }
                                break;
                            case Commands.ResultsSync:
                                if (NetworkMonitor.ConfigurationLoaded)
                                {
                                    SyncDatas localdatas = new SyncDatas {Counter = ResultChecker.GetLastResultCounter(cmd.FromIp, cmd.FromNamespace)};
                                    //List<SyncData> localdatas = new List<SyncData>();
                                    foreach (IMonitor monitor in NetworkMonitor.Configuration.ToEnumerable())
                                    {
                                        localdatas.Add(new SyncData
                                                            {FriendlyName = monitor.FriendlyName, GuidHash = monitor.Hash, IntHash = monitor.GetHashCode()});
                                    }
                                    _listener.SendCommand(new Command
                                                                {
                                                                    CommandType = Commands.ResultsSyncResponse,
                                                                    Data = localdatas,
                                                                    ToNamespace = cmd.FromNamespace,
                                                                    ToIp = cmd.FromIp
                                                                });
                                }
                                break;
                            case Commands.GetAlertResults:
                                Results alertResults = ResultChecker.GetLatestAlertResults(cmd.FromIp, cmd.FromNamespace);
                                Command cmdAlertResults = new Command
                                                                {
                                                                    CommandType = Commands.GetAlertResultsResponse,
                                                                    Data = alertResults,
                                                                    ToNamespace = cmd.FromNamespace,
                                                                    ToIp = cmd.FromIp
                                                                };
                                _listener.SendCommand(cmdAlertResults);
                                ResultChecker.ClearResults(cmd.FromIp, cmd.FromNamespace);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log(this.GetType(), LogType.Debug, "Command Type: " + cmd.CommandType);
                        Logger.Instance.LogException(this.GetType(), ex);
                    }
                }
                Thread.Sleep(250);
            }
        }

        public void Dispose()
        {
            _listener.Dispose();
        }
    }
}