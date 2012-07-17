using System.ServiceProcess;

namespace RemoteMon_Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
                                              { 
                                                  new NetworkMonitor() 
                                              };
            ServiceBase.Run(servicesToRun);
            //NetworkMonitor nm = new NetworkMonitor();
        }
    }
}
