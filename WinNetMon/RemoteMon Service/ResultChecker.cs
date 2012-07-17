using System;
using System.Collections.Generic;
using System.Threading;
using RemoteMon_Lib;

namespace RemoteMon_Service
{
    internal class ResultChecker
    {
        private Thread _check;
        private readonly Dictionary<string, CountTime> _counts = new Dictionary<string, CountTime>();
        private static readonly Object result_locker = new Object();
        private static volatile Boolean kill = false;

        private static readonly Dictionary<Namespace, Dictionary<String, ResultsCounter>> latest_results = new Dictionary<Namespace, Dictionary<String, ResultsCounter>>();

        public static Results GetLatestResults(String ipHost, Namespace nameSpace)
        {
            if (!latest_results.ContainsKey(nameSpace))
                latest_results[nameSpace] = new Dictionary<String, ResultsCounter>();
            if (!latest_results[nameSpace].ContainsKey(ipHost))
                latest_results[nameSpace].Add(ipHost, new ResultsCounter());
            return latest_results[nameSpace][ipHost].Results;
        }
        public static Results GetLatestAlertResults(String ipHost, Namespace nameSpace)
        {
            if (!latest_results.ContainsKey(nameSpace))
                latest_results[nameSpace] = new Dictionary<String, ResultsCounter>();
            if (!latest_results[nameSpace].ContainsKey(ipHost))
                latest_results[nameSpace].Add(ipHost, new ResultsCounter());
            Results results = new Results();
            foreach (IResult result in latest_results[nameSpace][ipHost].Results.ToEnumerable())
            {
                if(!result.Ok)
                    results.Add(result);
            }
            return results;
        }
        public static Int64 GetLastResultCounter(String ipHost, Namespace nameSpace)
        {
            if (!latest_results.ContainsKey(nameSpace))
                latest_results[nameSpace] = new Dictionary<String, ResultsCounter>();
            if (!latest_results[nameSpace].ContainsKey(ipHost))
                latest_results[nameSpace].Add(ipHost, new ResultsCounter());
            return latest_results[nameSpace][ipHost].Counter;
        }
        public static void ClearResults(String ipHost, Namespace nameSpace)
        {
            lock(result_locker)
            {
                if (!latest_results.ContainsKey(nameSpace))
                    latest_results[nameSpace] = new Dictionary<String, ResultsCounter>();
                if (!latest_results[nameSpace].ContainsKey(ipHost))
                    latest_results[nameSpace].Add(ipHost, new ResultsCounter());

                latest_results[nameSpace][ipHost].Results = null; // results.Clear();
            }
        }
        private void ClearResultsNoLock(String ipHost, Namespace nameSpace)
        {
            if (!latest_results.ContainsKey(nameSpace))
                latest_results[nameSpace] = new Dictionary<String, ResultsCounter>();
            if (!latest_results[nameSpace].ContainsKey(ipHost))
                latest_results[nameSpace].Add(ipHost, new ResultsCounter());

            latest_results[nameSpace][ipHost].Results = null;
        }

        private class CountTime
        {
            public DateTime Last;
            public Int32 Count = 0;
            public DateTime LastSent;
        }
        private class ResultsCounter
        {
            public Results Results;
            public Int64 Counter;

            public ResultsCounter()
            {
                Results = new Results();
                Counter = 0;
            }
        }

        public void Start()
        {
            //start/restart the _check and _alert to check for results and determine if alerts should be sent
            if (_check == null)
            {
                _check = new Thread(Check);
            }
            else if (_check.IsAlive)
                _check.Join(50);
            _check.Start();
        }
        public void Stop()
        {
            Logger.Instance.Log(this.GetType(), LogType.Info, "Result checker stopping.");

            //kill threads 
            kill = true;
            _check.Join(100);
            //_alert.Join(100);
        }

        private void Check()
        {
            while (!kill)
            {
                //loop and check status and add to alert queue
                Results r = null;
                try
                {                    
                    lock (result_locker)
                    {
                        r = MonitorScheduler.GetResults();
                        //results.AddRange(r.ToEnumerable());
                        foreach (Namespace nKey in latest_results.Keys)
                        {
                            foreach (String sKey in latest_results[nKey].Keys)
                            {
                                ResultsCounter rc = latest_results[nKey][sKey];
                                rc.Counter++;
                                if (rc.Results == null)
                                    rc.Results = new Results(r.ToEnumerable());
                                else
                                    rc.Results.AddRange(r.ToEnumerable());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                }
                try
                {
                    if (r == null)
                        continue;

                    foreach (IResult result in r.ToEnumerable())
                    {
                        if (!result.Ok)
                        {
                            CountTime tmp;
                            if (!_counts.ContainsKey(result.MonitorHash))
                                tmp = _counts[result.MonitorHash] = new CountTime();
                            else
                                tmp = _counts[result.MonitorHash];

                            Double totalMilliseconds = (result.RunTime - tmp.Last).TotalMilliseconds;
                            if (totalMilliseconds < ((((IMonitor)result.Monitor).UpdateFrequency) * 2))
                            {
                                tmp.Count++;
                            }
                            else if (((IMonitor)result.Monitor).ThresholdBreachCount == 1) //if the breach count is 1, increase it - should fire an alert if so
                                tmp.Count++;
                            else
                                tmp.Count = 0;

                            if (tmp.Count >= ((IMonitor)result.Monitor).ThresholdBreachCount) //its it has passed the breach count then its valid
                            {
                                if ((DateTime.Now - tmp.LastSent).TotalMinutes >= 5) //only alert every 5 minutes at most? - don't want to spam.
                                {
                                    Boolean send = result.SendAlert();
                                    send = NetworkMonitor.Configuration.Settings.DefaultAlerts.SendAlerts(result) & send;

                                    if (!send)
                                        Logger.Instance.Log(this.GetType(), LogType.Info, "Failed to send all alerts for monitor result: " + result.ToString());
                                    else
                                        tmp.LastSent = DateTime.Now; //only if it sent successfully
                                }
                                //}
                                tmp.Count = 0;
                            }

                            tmp.Last = result.RunTime;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(this.GetType(), ex);
                }
                Thread.Sleep(500);
            }
        }
    }
}