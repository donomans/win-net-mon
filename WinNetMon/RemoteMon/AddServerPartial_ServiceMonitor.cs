using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class AddServer
    {
        //private static Boolean TestMonitorRunService;

        private static ServiceController[] servicesTest;
        private Thread _serviceMonitorPopulation;

        public ServiceMonitor GetServiceMonitor()
        {
            ServiceMonitor serviceMonitor = new ServiceMonitor
                                          {
                                              FriendlyName = FriendlyName,
                                              Server = IpOrHostName,
                                              UpdateFrequency = //NOTE: make UpdateFrequency fields a masked textbox
                                                  Convert.ToInt32(
                                                      servicesPickedServicesTestDataUpdateFreqTextBox.Text),
                                              AutomaticRestart =
                                                  servicesAutomaticRestartServiceCheckBox.Checked
                                          };
            foreach(ListViewItem lvi in servicesPickedServicesListView.Items)
            {
                Service s = new Service(lvi.SubItems[0].Text);
                s.GoodStatus = (ServiceStatus)Enum.Parse(typeof (ServiceStatus), lvi.SubItems[1].Text);
                serviceMonitor.Services.Add(s);
            }
            return serviceMonitor;
        }

        public void ServiceRepopulate(ServiceMonitor serviceMonitor)
        {
            servicesTest = ServiceController.GetServices(serviceMonitor.Server);
            servicesServiceListView.Items.Clear();

            foreach (ServiceController sc in servicesTest)
            {
                servicesServiceListView.Items.Add(new ListViewItem(new[] { sc.DisplayName, sc.Status.ToString() }));
            }
            alreadyPopulated = true;

            servicesPickedServicesTestDataUpdateFreqTextBox.Text =
                            serviceMonitor.UpdateFrequency.ToString();
            servicesAutomaticRestartServiceCheckBox.Checked = serviceMonitor.AutomaticRestart;
            foreach (Service s in serviceMonitor.Services)
            {
                servicesPickedServicesListView.Items.Add(
                    new ListViewItem(new[] { s.ServiceName, s.GoodStatus.ToString() }));
            }
        }

        private void GetServices()
        {
            while (!ipBoolean)
            {
                if (IsDisposed)
                    //NOTE: avoids situation where ip wasn't input and the addserver window was canceled - would loop forever otherwise
                    return;
                
                Thread.Sleep(100);
            }
            if (!alreadyPopulated)
            {
                try
                {
                    servicesTest = ServiceController.GetServices(IpOrHostName);
                        //_hostName == String.Empty ? _iporhostname : _hostName);
                }
                catch (Exception ex)
                {
                    ToLog = "Error: " + ex.Message;
                    //Invoke(new ErrorServiceDelegate(ServiceError));
                }
                if (servicesTest != null)
                {
                    while (!IsHandleCreated)
                        Thread.Sleep(250);

                    Invoke(new PopulateServiceDelegate(PopulateServices));
                }
            }
        }

        private void PopulateServices()
        {
            servicesServiceListView.Items.Clear();

            foreach (ServiceController sc in servicesTest)
            {
                servicesServiceListView.Items.Add(new ListViewItem(new[] {sc.DisplayName, sc.Status.ToString()}));
            }
        }

        private void AddServerServiceListViewItemSelectionChanged(object sender,
                                                                   ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem(new []{e.Item.SubItems[0].Text, e.Item.SubItems[1].Text})
                                            {Name = e.Item.SubItems[0].Text};
            if (e.IsSelected && !servicesPickedServicesListView.Items.ContainsKey(listViewItem.Name))
            {
                servicesPickedServicesListView.Items.Add(listViewItem);
            }
            else if(!e.IsSelected)
                foreach (ListViewItem lvi in servicesPickedServicesListView.Items)
                {
                    if (lvi.Text == e.Item.SubItems[0].Text)
                    {
                        servicesPickedServicesListView.Items.Remove(lvi);
                        break;
                    }
                }
        }

        private void AddServerPickedServicesClearBtnClick(object sender, EventArgs e)
        {
            servicesPickedServicesListView.Items.Clear();
        }

        private void ServicesPickedServicesListViewMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo listViewHitTestInfo = servicesPickedServicesListView.HitTest(e.X, e.Y);
            listViewHitTestInfo.Item.SubItems[1].Text = listViewHitTestInfo.Item.SubItems[1].Text == "Stopped" ? "Running" : "Stopped";
        }

        #region Nested type: ErrorServiceDelegate

        private delegate void ErrorServiceDelegate();

        #endregion

        #region Nested type: PopulateServiceDelegate

        private delegate void PopulateServiceDelegate();

        #endregion
    }
}