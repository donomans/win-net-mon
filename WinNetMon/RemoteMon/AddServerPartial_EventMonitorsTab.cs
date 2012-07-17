using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RemoteMon_Lib;
using EventLogEntryType = RemoteMon_Lib.EventLogEntryType;

namespace RemoteMon
{
    public partial class AddServer
    {
        //private static Boolean TestMonitorRunEvent;

        private static EventLog[] eventLogTest;
        private static List<ListViewItem> lviList;
        private static Int32 eventLogIndex;
        private static EventLogEntryType entryType = EventLogEntryType.None;
        private static String appSource = String.Empty;
        private Thread _eventMonitorListViewPopulation;
        private Thread _eventMonitorPopulation;

        //private static Boolean _eventAlreadyPopulated = false;

        public EventLogEntryType EntryType
        {
            get
            {
                {
                    GetEntryType();
                    return entryType;
                }
            }
        }

        public EventMonitor GetEventMonitor()
        {
            EventMonitor eventMonitor = new EventMonitor
                                            {
                                                ClearOldLogs = eventMonitorClearLogCb.Checked,
                                                EventLogKind = eventMonitorEventsLogTypeDdl.Text,
                                                EventNameMatch =
                                                    eventMonitorSourceFilterTextBox.Text,
                                                FriendlyName = FriendlyName,
                                                Server = IpOrHostName,
                                                EventType = EntryType,
                                                UpdateFrequency =
                                                    Convert.ToInt32(
                                                        eventMonitorTestDataUpdateFreqTextBox.Text),
                                                StartTime = DateTime.Now
                                            };
            return eventMonitor;
        }

        public void EventRepopulate(EventMonitor eventMonitor)
        {
            eventLogTest = EventLog.GetEventLogs(eventMonitor.Server);
            eventMonitorEventsLogTypeDdlWaitLabel.Visible = false;
            eventMonitorEventsLogTypeDdl.Visible = true;
            eventMonitorEventsLogTypeDdlErrorLabel.Text = String.Empty;

            eventMonitorEventsLogTypeDdl.Items.Clear();
            foreach (EventLog el in eventLogTest)
            {
                eventMonitorEventsLogTypeDdl.Items.Add(el.Log);
            }
            alreadyPopulated = true;

            eventMonitorClearLogCb.Checked = eventMonitor.ClearOldLogs;
            eventMonitorSourceFilterTextBox.Text = eventMonitor.EventNameMatch;
            entryType = eventMonitor.EventType;
            eventMonitorEntryTypeFilterCbError.Checked = (eventMonitor.EventType & EventLogEntryType.Error) == EventLogEntryType.Error;
            eventMonitorEntryTypeFilterCbWarning.Checked = (eventMonitor.EventType & EventLogEntryType.Warning) == EventLogEntryType.Warning;
            eventMonitorEntryTypeFilterCbInformation.Checked = (eventMonitor.EventType & EventLogEntryType.Information) == EventLogEntryType.Information;
            eventMonitorEntryTypeFilterCbFailureAudit.Checked = (eventMonitor.EventType & EventLogEntryType.FailureAudit) == EventLogEntryType.FailureAudit;
            eventMonitorEntryTypeFilterCbSuccessAudit.Checked = (eventMonitor.EventType & EventLogEntryType.SuccessAudit) == EventLogEntryType.SuccessAudit;
            eventMonitorEventsLogTypeDdl.Text = eventMonitor.EventLogKind;
            eventMonitorTestDataUpdateFreqTextBox.Text = eventMonitor.UpdateFrequency.ToString();
        }

        private void GetEvents()
        {
            while (!ipBoolean)
            {
                if (IsDisposed)
                    //NOTE:avoids situation where ip wasn't input and the addserver window was canceled - would loop forever otherwise
                    return;
                
                Thread.Sleep(100);
            }
            if (!alreadyPopulated)
            {
                try
                {
                    entryType = EventLogEntryType.None;
                    eventLogTest = EventLog.GetEventLogs(iporhostname);
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                    ToLog = "Error: " + ex.Message;
                    if (this.IsHandleCreated)
                        Invoke(new ErrorEventLogDelegate(EventLogError));
                }
                if (eventLogTest != null && this.IsHandleCreated)
                    Invoke(new PopulateEventLogDelegate(PopulateEventLogTypes));
            }
        }

        private void PopulateEventLogTypes()
        {
            eventMonitorEventsLogTypeDdlWaitLabel.Visible = false;
            eventMonitorEventsLogTypeDdl.Visible = true;
            eventMonitorEventsLogTypeDdlErrorLabel.Text = String.Empty;

            eventMonitorEventsLogTypeDdl.Items.Clear();
            foreach (EventLog el in eventLogTest)
            {
                eventMonitorEventsLogTypeDdl.Items.Add(el.Log);
            }
        }

        private void EventLogError()
        {
            Cursor = Cursors.Default;
            eventMonitorEventsLogTypeDdlErrorLabel.Text = "Event Logs could not be populated.";
        }

        private void EventMonitorEventsLogTypeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            eventMonitorEventsLogsFilteredListView.Items.Clear();

            eventLogIndex = eventMonitorEventsLogTypeDdl.SelectedIndex;

            if (_eventMonitorListViewPopulation != null)
                _eventMonitorListViewPopulation.Abort();
            _eventMonitorListViewPopulation = new Thread(PopulateListForListView)
                                                  {Name = "EventMonitor-ListView IndexChange"};
            _eventMonitorListViewPopulation.Start();

            eventMonitorEventsLogTypeDdlErrorLabel.ForeColor = Color.Black;
            eventMonitorEventsLogTypeDdlErrorLabel.Text = "Please Wait...";
            Cursor = Cursors.WaitCursor;
        }

        private void EventMonitorEntryTypeFilterBtnClick(object sender, EventArgs e)
        {
            if (_eventMonitorListViewPopulation != null)
                _eventMonitorListViewPopulation.Abort();

            //String fullEntryType = String.Empty;
            GetEntryType();

            eventMonitorEventsLogsFilteredListView.Items.Clear();

            _eventMonitorListViewPopulation = null;
            _eventMonitorListViewPopulation = new Thread(PopulateListForListView)
                                                  {Name = "EventMonitor-ListView EntryFilter"};
            _eventMonitorListViewPopulation.Start();
        }

        private void GetEntryType()
        {
            //EventLogEntryType entryType;
            List<String> fullEntryType = new List<String>(6);
            foreach (Control c in eventMonitorEntryTypeFilterPanel.Controls)
            {
                if (((CheckBox)c).Checked)
                    fullEntryType.Add(c.Text);// +", ";
            }
            String fullEntry = String.Join(", ", fullEntryType.ToArray());//.Trim().Trim(new[] {','}));

            if (fullEntry == String.Empty)
                entryType = 0;
            else
                entryType = (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), fullEntry);

            //return entryType;
        }

        private void EventMonitorSourceFilterBtnClick(object sender, EventArgs e)
        {
            if (_eventMonitorListViewPopulation != null)
                _eventMonitorListViewPopulation.Abort();

            appSource = eventMonitorSourceFilterTextBox.Text;

            eventMonitorEventsLogsFilteredListView.Items.Clear();

            _eventMonitorListViewPopulation = null;
            _eventMonitorListViewPopulation = new Thread(PopulateListForListView)
                                                  {Name = "EventMonitor-ListView SourceFilter"};
            _eventMonitorListViewPopulation.Start();
        }

        private void PopulateListView()
        {
            if (lviList != null)
                eventMonitorEventsLogsFilteredListView.Items.AddRange(lviList.ToArray());
            eventMonitorEventsLogTypeDdlErrorLabel.Text = String.Empty;
            Cursor = Cursors.Default;
        }

        private void PopulateListForListView()
        {
            lviList = new List<ListViewItem>();
            try
            {
                int x = 0;
                for(int y = eventLogTest[eventLogIndex].Entries.Count -1; y > -1; y--)
                //foreach (EventLogEntry ele in eventLogTest[eventLogIndex].Entries)
                {
                    using (EventLogEntry ele = eventLogTest[eventLogIndex].Entries[y])
                    {
                        Boolean appSourceBoolean = false;
                        if (appSource.Contains(","))
                        {
                            String[] splits = appSource.Split(new[] {','});

                            foreach (String s in splits)
                            {
                                if (ele.Source.ToLower().Contains(s.Trim().ToLower()))
                                {
                                    appSourceBoolean = true;
                                    break;
                                }
                            }
                        }
                        else if (appSource != String.Empty)
                        {
                            if (ele.Source.ToLower().Contains(appSource.Trim().ToLower()))
                            {
                                appSourceBoolean = true;
                            }
                        }

                        EventLogEntryType currEntryType = (EventLogEntryType) ele.EntryType;
                        if ((entryType == EventLogEntryType.None || ((entryType & currEntryType) == currEntryType)) &&
                            (appSource == String.Empty || appSourceBoolean))
                        {
                            lviList.Add(
                                new ListViewItem(new[]
                                                     {
                                                         ele.TimeGenerated.ToString(),
                                                         ele.EntryType.ToString(),
                                                         ele.Category,
                                                         ele.Source,
                                                         ele.Message.Replace("\r", " ").Replace("\n", String.Empty)
                                                     }));
                            x++;
                            if (x%50 == 0)
                            {
                                Invoke(new PopulateEventLogDelegate(PopulateListView));
                                lviList.Clear();
                                if (x > 400)
                                    break;
                            }
                        }
                    }
                }
                if (lviList.Count > 0)
                {
                    Invoke(new PopulateEventLogDelegate(PopulateListView));
                }
                else 
                    Invoke(new ErrorEventLogDelegate(EventLogError));
            }
            catch (ThreadAbortException)
            {
                //NOTE:to eat the [System.Threading.ThreadAbortException] 
                //NOTE:"{Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.}" execption caused by aborting this thread upon setting a new filter
            }
            catch (Exception ex)
            {
                ToLog = "Error: " + ex.Message;
                Invoke(new ErrorEventLogDelegate(EventLogError));
            }
        }

        #region Nested type: ErrorEventLogDelegate

        private delegate void ErrorEventLogDelegate();

        #endregion

        #region Nested type: PopulateEventLogDelegate

        private delegate void PopulateEventLogDelegate();

        #endregion
    }
}