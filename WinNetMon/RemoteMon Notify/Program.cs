using System;
using System.Windows.Forms;

namespace RemoteMon_Notify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Notifier noti = new Notifier();

            Application.Run();
        }
    }
}
