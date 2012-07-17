using System;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using RemoteMon_Lib;

namespace RemoteMon_Service
{
    public class NetworkMonitor : ServiceBase
    {       
        internal static readonly ResultChecker ResultChecker = new ResultChecker();
        internal static readonly CommandChecker CommandChecker = new CommandChecker();
        private static readonly Thread configuration_getter = new Thread(ConfigurationSetup) { IsBackground = true};
        private static volatile Boolean configLoaded = false;
        private static ConfigurationData configurationData;

        internal static Boolean ConfigurationLoaded { get { return configLoaded; } set { configLoaded = value; } }

        public NetworkMonitor()
        {
            this.ServiceName = "RemoteMon Service";
            this.AutoLog = true;
            this.CanPauseAndContinue = false;
            ////InitializeComponent();
            //Start();
        }

        protected override void OnStart(string[] args) //void Start()//
        {
            //throw new Exception(Environment.CurrentDirectory);
            Logger.Instance.Log(this.GetType(), LogType.Info, "Starting Service.");
            //throw new Exception("got here.");
            //NOTE: three main components
            //Loop for dealing with results and alerting
            //Loop for listening for commands
            //MonitorScheduler

            //1) try to set configuration
            //configurationData = ConfigurationData.LoadConfiguration("configuration.xml");
            //if (configurationData != null)
            //    configLoaded = true;

            //2) get configuration
            //   if configuration is non existent, poll and wait for configuration to be found (either through tcp or a file to appear)
            Logger.Instance.Log(this.GetType(), LogType.Info, "Starting Command checker.");
            CommandChecker.Start();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Command checker started.");

            Logger.Instance.Log(this.GetType(), LogType.Info, "Starting Result checker.");
            ResultChecker.Start();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Result checker started.");

            configuration_getter.Start();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Service started.");
            //Console.ReadLine();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            ResultChecker.Stop();
            CommandChecker.Stop();
            MonitorScheduler.Scheduler.Kill();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Service stopping.");
            
            Logger.Instance.LogImmediate();

            base.OnStop();
        }

        internal static void SetConfiguration(ConfigurationData configuration)
        {
            configurationData = configuration;
            ConfigurationLoaded = true;
            MonitorScheduler.Scheduler.SetMonitors(configurationData);
        }

        private static void ConfigurationSetup()
        {
            while (!configLoaded)
            {
                try
                {
                    if (Environment.CurrentDirectory.Contains("system32"))
                    {
                        if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon"))
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon");
                        configurationData = ConfigurationData.LoadConfiguration(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon\configuration.xml");
                    }
                    else
                        configurationData = ConfigurationData.LoadConfiguration(@"configuration.xml");
                }
                catch (Exception)
                {
                    //Logger.Instance.LogException(this.GetType(), ex);
                }
                try
                {
                    if (configurationData == null)
                    {
                        CommandChecker.SendCommandToAll(new Command { CommandType = Commands.GetConfiguration });
                        Thread.Sleep(60000);
                    }
                }
                catch (Exception)
                {
                    //Logger.Instance.LogException(this.GetType(), ex);
                }
                if (configurationData != null)
                {
                    configLoaded = true;
                    Logger.Instance.Log(typeof(NetworkMonitor), LogType.Info, "Configuration Loaded.");
                }

                if (!configLoaded)
                    Logger.Instance.Log(typeof(NetworkMonitor), LogType.Info, "Unable to find configuration.");

                Thread.Sleep(10000);
            }

            //should fill this in if its not in here, for whatever reason, then any clients requesting it will have it filled out for sure
            configurationData.Settings.RemoteServiceAddress = System.Net.Dns.GetHostName();
            configurationData.Settings.RemoteServicePort = CommandChecker.Port.ToString();

            MonitorScheduler.Scheduler.SetMonitors(configurationData, true);
        }

        internal static ConfigurationData Configuration
        {
            get { return configurationData; }
            set { configurationData = value; }
        }
    }
}
