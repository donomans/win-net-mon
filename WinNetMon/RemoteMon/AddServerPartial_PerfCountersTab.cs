using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class AddServer
    {
        private static Boolean testMonitorRunPerf;

        //private static Boolean _pfcAlreadyPopulated = false;

        private static PerformanceCounterCategory[] perfCounterCategories;
        private static List<String> perfCounterNames;
        private static List<String> perfCounterInstances;

        private static PerformanceCounter perfCounterTest;
        private Thread _perfCounterPopulation;

        public PfcMonitor GetPfcMonitor()
        {
            PfcMonitor pfcMonitor = new PfcMonitor
                                        {
                                            Category =
                                                perfCounterPCTypeDdl.SelectedItem.ToString(),
                                            Counter =
                                                perfCounterCounterNameDdl.SelectedItem.
                                                ToString(),
                                            Instance =
                                                perfCounterInstanceNameDdl.SelectedItem.
                                                ToString(),
                                            ThresholdWarning =
                                                perfCounterTestDataThresholdWarningTextBox.Text,
                                            ThresholdPanic =
                                                perfCounterTestDataThresholdPanicTextBox.Text,
                                            ThresholdBreachCount =
                                                Convert.ToInt32(
                                                    perfCounterTestDataThresholdBreachTextBox.
                                                        Text),
                                            ThresholdLessThan =
                                                perfCounterTestDataThresholdLessThanCheckBox.
                                                Checked,
                                            UpdateFrequency =
                                                Convert.ToInt32(
                                                    perfCounterTestDataUpdateFreqTextBox.Text),
                                            Server = IpOrHostName,
                                            FriendlyName = FriendlyName
                                        };
            return pfcMonitor;
        }

        private void GetPerfCounterTypes()
        {
            while (!ipBoolean)
            {
                if (IsDisposed)
                    return;
                    //NOTE: avoids situation where ip wasn't input and the addserver window was canceled - would loop forever otherwise

                Thread.Sleep(100);
            }
            if (!alreadyPopulated)
            {
                try
                {
                    //String tmpHostName = _hostName == String.Empty ? _iporhostname : _hostName;
                    if (String.IsNullOrEmpty(IpOrHostName))
                        //NOTE: display categories for machine this program is run on in lieu of a valid result based on an ip/hostname entered
                        perfCounterCategories = PerformanceCounterCategory.GetCategories();
                    else
                        perfCounterCategories = PerformanceCounterCategory.GetCategories(IpOrHostName);
                }
                catch (Exception ex)
                {
                    ToLog = "Error: " + ex.Message;
                }
                if (perfCounterCategories != null)
                {
                    while (!IsHandleCreated)
                        Thread.Sleep(50);
                    Invoke(new PopulatePerfCounterDelegate(PopulatePerfCounterCategoriesDdl));
                    //else
                    //{
                    //    //NOTE: need to ensure these are active, incase someone redoes the 
                    //    perfCounterPCTypeDdl.Visible = true;
                    //    perfCounterCategoryWaitLabel.Visible = false;
                    //}
                }
            }
        }

        public void PerfCounterRepopulate(PfcMonitor pfc)
        {
            alreadyPopulated = true;

            perfCounterCategories = pfc.Server == "" ? PerformanceCounterCategory.GetCategories() : PerformanceCounterCategory.GetCategories(pfc.Server);

            for (int x = 0; x < perfCounterCategories.Length; x++)
            {
                perfCounterPCTypeDdl.Items.Add(perfCounterCategories[x].CategoryName);
            }

            PerformanceCounterCategory pcc = new PerformanceCounterCategory(pfc.Category, pfc.Server);
            InstanceDataCollectionCollection idcc = pcc.ReadCategory();
            perfCounterNames = new List<String>(idcc.Count);
            foreach (DictionaryEntry idc in idcc)
            {
                perfCounterNames.Add(((InstanceDataCollection)idc.Value).CounterName);
            }

            perfCounterInstances = new List<String>(pcc.GetInstanceNames());
            
            if (perfCounterNames.Count > 0)
                perfCounterCounterNameDdl.Items.AddRange(perfCounterNames.ToArray());
            else
            {
                perfCounterCounterNameDdl.Items.Add("None");
                perfCounterCounterNameDdl.SelectedItem = "None";
            }
            if (perfCounterInstances.Count > 0)
                perfCounterInstanceNameDdl.Items.AddRange(perfCounterInstances.ToArray());
            else
            {
                perfCounterInstanceNameDdl.Items.Add("None");
                perfCounterInstanceNameDdl.SelectedItem = "None";
            }

            try
            {
                perfCounterPCTypeDdl.SelectedIndexChanged -= PerfCounterPcTypeDdlSelectedIndexChanged;
                perfCounterPCTypeDdl.SelectedItem = pfc.Category;
                perfCounterPCTypeDdl.SelectedIndexChanged += PerfCounterPcTypeDdlSelectedIndexChanged;
                perfCounterCounterNameDdl.SelectedItem = pfc.Counter;
                perfCounterInstanceNameDdl.SelectedItem = pfc.Instance;

                perfCounterTestDataThresholdBreachTextBox.Text = pfc.ThresholdBreachCount.ToString();
                perfCounterTestDataThresholdLessThanCheckBox.Checked = pfc.ThresholdLessThan;
                perfCounterTestDataThresholdPanicTextBox.Text = pfc.ThresholdPanic;
                perfCounterTestDataThresholdWarningTextBox.Text = pfc.ThresholdWarning;
                perfCounterTestDataUpdateFreqTextBox.Text = pfc.UpdateFrequency.ToString();
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
            }

            perfCounterCategoryWaitLabel.Visible = false;
            perfCounterPCTypeDdl.Visible = true;
            perfCounterCounterNameWaitLabel.Visible = false;
            perfCounterCounterNameDdl.Visible = true;
            perfCounterInstanceNameWaitLabel.Visible = false;
            perfCounterInstanceNameDdl.Visible = true;
        }

        private void GetCounterNamesAndInstances(object selectedItemText)
        {
            PerformanceCounterCategory pcc = new PerformanceCounterCategory((String)selectedItemText, IpOrHostName);

            try
            {
                InstanceDataCollectionCollection idcc = pcc.ReadCategory();
                perfCounterNames = new List<String>(idcc.Count);

                foreach (DictionaryEntry idc in idcc)
                {
                    perfCounterNames.Add(((InstanceDataCollection) idc.Value).CounterName);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                ToLog = "Error: " + ex.Message;
            }

            try
            {
                perfCounterInstances = new List<String>(pcc.GetInstanceNames());
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                ToLog = "Error: " + ex.Message;
            }

            Invoke(new PopulateCounterNamesDelegate(PopulatePerfCounterNamesAndInstancesDdl));
        }

        //private void GetInstanceNames(object selectedItemText)
        //{
        //}

        private void PopulatePerfCounterCategoriesDdl()
        {
            perfCounterPCTypeDdl.Items.Clear();

            perfCounterCategoryWaitLabel.Visible = false;
            perfCounterPCTypeDdl.Visible = true;

            perfCounterCounterNameDdl.Visible = false;
            perfCounterInstanceNameDdl.Visible = false;
            perfCounterCounterNameWaitLabel.Visible = true;
            perfCounterInstanceNameWaitLabel.Visible = true;

            for (int x = 0; x < perfCounterCategories.Length; x++)
            {
                perfCounterPCTypeDdl.Items.Add(perfCounterCategories[x].CategoryName);
            }
        }

        private void PopulatePerfCounterNamesAndInstancesDdl()
        {
            perfCounterCounterNameDdl.Items.Clear();
            perfCounterInstanceNameDdl.Items.Clear();

            perfCounterCounterNameWaitLabel.Visible = false;
            perfCounterCounterNameDdl.Visible = true;
            perfCounterInstanceNameWaitLabel.Visible = false;
            perfCounterInstanceNameDdl.Visible = true;

            if (perfCounterNames.Count > 0)
                perfCounterCounterNameDdl.Items.AddRange(perfCounterNames.ToArray());
            else
            {
                perfCounterCounterNameDdl.Items.Add("None");
                perfCounterCounterNameDdl.SelectedItem = "None";
            }
            if (perfCounterInstances.Count > 0)
                perfCounterInstanceNameDdl.Items.AddRange(perfCounterInstances.ToArray());
            else
            {
                perfCounterInstanceNameDdl.Items.Add("None");
                perfCounterInstanceNameDdl.SelectedItem = "None";
            }
        }

        private void PerfCounterPcTypeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            Thread counterNamePopulation = new Thread(GetCounterNamesAndInstances);
            counterNamePopulation.Start(perfCounterPCTypeDdl.SelectedItem.ToString());
        }

        private void PerfCounterTestBtnClick(object sender, EventArgs e)
        {
            if (perfCounterPCTypeDdl.SelectedItem != null && perfCounterCounterNameDdl.SelectedItem != null &&
                perfCounterCounterNameDdl.SelectedItem.ToString() != "None" &&
                perfCounterInstanceNameDdl.SelectedItem != null)
            {
                try
                {
                    testMonitorRunPerf = true;
                    perfCounterTest = new PerformanceCounter
                                          {
                                              CategoryName = perfCounterPCTypeDdl.SelectedItem.ToString(),
                                              CounterName = perfCounterCounterNameDdl.SelectedItem.ToString(),
                                              InstanceName = ((String) perfCounterInstanceNameDdl.SelectedItem != "None"
                                                                  ? perfCounterInstanceNameDdl.SelectedItem.ToString()
                                                                  : String.Empty),
                                              MachineName = IpOrHostName
                                          };
                    //(perfCounterPCTypeDdl.SelectedItem.ToString(),
                    //                                     perfCounterCounterNameDdl.SelectedItem.ToString(),
                    //                                     ((String)perfCounterInstanceNameDdl.SelectedItem != "None"
                    //                                          ? perfCounterInstanceNameDdl.SelectedItem.ToString()
                    //                                          : String.Empty),
                    //                                     IpOrHostName);//_hostName == String.Empty ? _iporhostname : _hostName);
                }
                catch (Exception ex)
                {
                    testMonitorRunPerf = false;
                    ToLog = "Error: " + ex.Message;
                    perfCounterTestDataHelpText.Text = "Error accessing performance counter.";
                }
            }
            else
            {
                testMonitorRunPerf = false;
                //perfCounterTestDataHelpText.ForeColor = Color.Red;
                perfCounterTestDataHelpText.Text =
                    "Error: Performance Counter could not be created -- the necessary values were not found.";
            }
        }

        #region Nested type: PopulateCounterNamesDelegate

        private delegate void PopulateCounterNamesDelegate();

        #endregion
        #region Nested type: PopulatePerfCounterDelegate

        private delegate void PopulatePerfCounterDelegate();

        #endregion
        
    }
}