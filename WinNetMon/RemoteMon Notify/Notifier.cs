using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon_Notify
{
    class Notifier
    {
        private readonly ContextMenuStrip _notifyMenuStrip = new ContextMenuStrip();
        private readonly ToolStripMenuItem _notifyStripStop = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _notifyStripStart = new ToolStripMenuItem();
        private readonly ToolStripTextBox _notifyStripInfo = new ToolStripTextBox();
        private readonly ToolStripSeparator _notifyStripSeperatorTop = new ToolStripSeparator();
        private readonly ToolStripSeparator _notifyStripSeperatorBottom = new ToolStripSeparator();
        private readonly ToolStripMenuItem _notifyStripQuit = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _notifyStripSettings = new ToolStripMenuItem();
        private readonly ResourceManager _resources;

        private readonly NotifyIcon _notifyIcon;
        
        private static Thread getCommands;
        private static Thread sendCommands;
        private static Thread alertPopup;

        private static readonly Object locker = new Object();
        private static Queue<IResult> queue = new Queue<IResult>();

        private static ITalker talker = null;

        private volatile static Boolean kill = false;


        private static NotifySettings notifySettings = null;

        public Notifier()
        {
            LoadSettings();

            getCommands = new Thread(GetCommands);
            getCommands.Start();

            sendCommands = new Thread(SendCommands);
            sendCommands.Start();

            alertPopup = new Thread(AlertPopup);
            alertPopup.Start();

            _resources = new ResourceManager("RemoteMon_Notify.Properties.Resources",
                                                       Assembly.GetExecutingAssembly());
            

            _notifyMenuStrip.SuspendLayout();

            _notifyIcon = new NotifyIcon
            {
                Visible = true,
                ContextMenuStrip = _notifyMenuStrip,
                Text = "RemoteMon Notify"
            };

            _notifyMenuStrip.Items.AddRange(new ToolStripItem[]
                                                     {
                                                         _notifyStripInfo,
                                                         _notifyStripSeperatorTop,
                                                         _notifyStripStop,
                                                         _notifyStripStart,
                                                         _notifyStripSeperatorBottom,
                                                         _notifyStripSettings,
                                                         _notifyStripQuit
                                                     });

            _notifyMenuStrip.Name = "notifyMenuStrip";
            _notifyMenuStrip.Size = new Size(161, 123);

            _notifyStripInfo.Name = "notifyStripInfo";
            _notifyStripInfo.Size = new Size(100, 23);
            _notifyStripInfo.ReadOnly = true;
            _notifyStripInfo.Text = "Status: OK";

            _notifyStripSeperatorTop.Name = "notifyStripSeperatorTop";
            _notifyStripSeperatorTop.Size = new Size(6, 6);

            _notifyStripStart.Name = "notifyStripStart";
            _notifyStripStart.Size = new Size(160, 22);
            _notifyStripStart.Text = "Start Service";
            _notifyStripStart.Click += NotifyStripStartClick;

            _notifyStripStop.Name = "notifyStripStop";
            _notifyStripStop.Size = new Size(160, 22);
            _notifyStripStop.Text = "Stop Service";
            _notifyStripStop.Click += NotifyStripStopClick;

            _notifyStripSeperatorBottom.Name = "notifyStripSeperatorBottom";
            _notifyStripSeperatorBottom.Size = new Size(6, 6);

            _notifyStripSettings.Name = "_notifyStripSettings";
            _notifyStripSettings.Size = new Size(160, 22);
            _notifyStripSettings.Text = "Settings";
            _notifyStripSettings.Click += NotifyStripSettingsClick;

            _notifyStripQuit.Name = "notifyStripQuit";
            _notifyStripQuit.Size = new Size(160, 22);
            _notifyStripQuit.Text = "Exit";
            _notifyStripQuit.Click += NotifyStripExitClick;

            _notifyMenuStrip.ResumeLayout(true);

        }


        private void GetCommands()
        {
            while(!kill)
            {
                if (talker != null && talker.CanUse)
                {
                    Command cmd = talker.GetCommand();//Namespace.Service);
                    if (cmd != null)
                    {
                        switch (cmd.CommandType)
                        {
                            case Commands.GetAlertResultsResponse:
                                foreach (IResult result in ((Results) cmd.Data).ToEnumerable())
                                {
                                    lock(locker)
                                    {
                                        queue.Enqueue(result);
                                    }
                                }
                                break;
                            default:
                                Logger.Instance.Log(this.GetType(), LogType.Command,
                                                    "Unexpected command received: " + cmd.CommandType + ", From Ip/Host: " + cmd.FromIp + ", From Namespace: " +
                                                    cmd.FromNamespace.ToString());
                                break;
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void AlertPopup()
        {
            while(!kill)
            {
                if (queue.Count > 0)
                {
                    lock (locker)
                    {
                        IResult result = queue.Dequeue();
                        _notifyIcon.BalloonTipText = result.ToString();
                        _notifyIcon.ShowBalloonTip(5000);
                    }
                }
                Thread.Sleep(250);
            }
        }

        private void SendCommands()
        {
            while (!kill)
            {
                if (talker != null && talker.CanUse)
                {
                    talker.SendCommand(new Command {CommandType = Commands.GetAlertResults, ToNamespace = Namespace.Service } );
                }

                Thread.Sleep(5000);
            }
        }

        private void LoadSettings()
        {
            try
            {
                notifySettings = NotifySettings.LoadSettings("settings.xml");
                if (notifySettings != null)
                {
                    if (Listener.IsLocal(notifySettings.ServiceAddress))
                        talker = new MessageQueueTalker(Namespace.Notifier);
                    else
                        talker = new TcpTalker(notifySettings.ServiceAddress, thisNamespace: Namespace.Notifier);
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }
        }
        private void ShowSettings()
        {
            try
            {
                Settings settings = new Settings();

                if (notifySettings != null)
                {
                    settings.settingsRemoteMonServiceAddressTextBox.Text = notifySettings.ServiceAddress;
                    settings.settingsRemoteMonServicePortTextBox.Text = notifySettings.ServicePort.ToString();
                }
                DialogResult dr = settings.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (notifySettings == null)
                        notifySettings = new NotifySettings();
                    notifySettings.ServiceAddress = settings.settingsRemoteMonServiceAddressTextBox.Text;
                    Int32 port;
                    if (Int32.TryParse(settings.settingsRemoteMonServicePortTextBox.Text, out port))
                        notifySettings.ServicePort = port;

                    if (Listener.IsLocal(notifySettings.ServiceAddress))
                        talker = new MessageQueueTalker(Namespace.Notifier);
                    else
                        talker = new TcpTalker(notifySettings.ServiceAddress, thisNamespace: Namespace.Notifier);

                    notifySettings.ExportToXml("settings.xml");
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }
        }

        #region Events
        private void NotifyStripSettingsClick(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void NotifyStripExitClick(object sender, EventArgs e)
        {
            kill = true;

            sendCommands.Join(200);
            getCommands.Join(200);
            alertPopup.Join(200);

            Application.Exit();
        }

        private void NotifyStripStopClick(object sender, EventArgs e)
        {
            ServiceController service = null;
            try
            {
                if (notifySettings != null && notifySettings.ServiceAddress != "")
                {
                    service = new ServiceController("RemoteMon Service", notifySettings.ServiceAddress);
                    if (service.Status != ServiceControllerStatus.Stopped)
                        service.Stop();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }
            finally
            {
                if(service != null)
                    service.Dispose();
            }
        }

        private void NotifyStripStartClick(object sender, EventArgs e)
        {
            ServiceController service = null;
            try
            {
                if (notifySettings != null && notifySettings.ServiceAddress != "")
                {
                    service = new ServiceController("RemoteMon Service", notifySettings.ServiceAddress);
                    if (service.Status != ServiceControllerStatus.Running)
                        service.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }
            finally
            {
                if (service != null)
                    service.Dispose();
            }
        }
        #endregion
    }
}
