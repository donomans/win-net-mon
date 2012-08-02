using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    [SerializableAttribute]
    public class WmiMonitors : IMonitors<WmiMonitor>
    {
        private Dictionary<String, WmiMonitor> _wmiTypes = new Dictionary<String, WmiMonitor>();

        /// <summary>
        /// Return the WmiMonitor
        /// </summary>
        /// <param name="hash">Hash value of the WmiMonitor class</param>
        /// <returns></returns>
        public WmiMonitor this[String hash]
        {
            get
            {
                return _wmiTypes.ContainsKey(hash) ? _wmiTypes[hash] : null;
            }
        }

        public Boolean Remove(String hash)
        {
            return _wmiTypes.Remove(hash);
        }

        public void Add(Object pfcMonitor)
        {
            WmiMonitor wmi = (WmiMonitor) pfcMonitor;
            _wmiTypes.Add(wmi.Hash, wmi);
        }
        public Boolean Contains(String hash)
        {
            return _wmiTypes.ContainsKey(hash);
        }


        [XmlIgnore]
        public Int32 Count { get { return _wmiTypes.Count; } }

        public Dictionary<String, WmiMonitor> Monitor
        {
            get { return _wmiTypes; }
            set { _wmiTypes = value; }
        }


        #region IEnumerable Members
        public IEnumerator<WmiMonitor> GetEnumerator()
        {
            return _wmiTypes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

    }

    [SerializableAttribute]
    public class WmiMonitor : IMonitor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private String _friendlyName = "";
        private Alerts _alertInfo = new Alerts();
        private String _server = "";
        private Int32 _updateFrequency = 10000;
        private WmiType _wmiType;
        private Int32 _thresholdBreachCount = 1;
        private Boolean _thresholdLessThan = false;
        private Single _thresholdPanicFloat = 0f; 
        private String _thresholdPanic = "";
        private Single _thresholdWarningFloat = 0f;
        private String _thresholdWarning = "";
        private Boolean _common = false;
        private String _hash = "";

        public override String ToString()
        {
            return "Wmi monitor type: " + _wmiType;
        }
       
        public Boolean Common
        {
            get { return _common; }
            set { _common = value; }
        }

        /// <summary>
        /// Warning/Panic for threshold less than or greater than.
        /// (Ex: ThresholdLessThan = false... 
        ///     threshold value is 15, warning/panic will not happen until value is greater than 15)
        /// </summary>
        public Boolean ThresholdLessThan
        {
            get { return _thresholdLessThan; }
            set { _thresholdLessThan = value; }
        }
        public WmiType WmiType
        {
            get { return _wmiType; }
            set { _wmiType = value; }
        }
        public Int32 ThresholdBreachCount
        {
            get { return _thresholdBreachCount; }
            set { _thresholdBreachCount = value; }
        }
        public String ThresholdPanic
        {
            get { return _thresholdPanic; }
            set
            {
                Single val;
                if (Single.TryParse(value, out val))
                    _thresholdPanicFloat = val;
                _thresholdPanic = value;
            }
        }
        [XmlIgnore]
        public Single ThresholdPanicSingle
        {
            get { return _thresholdPanicFloat; }
        }
        public String ThresholdWarning
        {
            get { return _thresholdWarning; }
            set
            {
                Single val;
                if (Single.TryParse(value, out val))
                    _thresholdWarningFloat = val;
                _thresholdWarning = value;
            }
        }
        [XmlIgnore]
        public Single ThresholdWarningSingle
        {
            get { return _thresholdWarningFloat; }
        }

        public String Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public FullMonitorType Type
        {
            get { return FullMonitorType.Wmi; }
        }
        public MonitorBaseType MonitorBaseType
        {
            get { return MonitorBaseType.Common; }
        }
        public String FriendlyName
        {
            get { return _friendlyName; }
            set { _friendlyName = value; }
        }
        public Alerts AlertInfo
        {
            get { return _alertInfo; }
            set { _alertInfo = value; }
        }
        public Int32 UpdateFrequency
        {
            get { return _updateFrequency; }
            set { _updateFrequency = value; }
        }
        [XmlIgnore]
        public String Hash
        {
            get
            {
                if (_hash != "")
                    return _hash;
                _hash = Guid.NewGuid().ToString();// XmlExport.ByteArrayToString(XmlExport.Serializer(typeof(WmiMonitor), this));
                return _hash;
            }
            set { _hash = value; }
        }

        public IResult Check()
        {
            WmiResult wr = new WmiResult(this);// {MonitorHash = Hash};
            try
            {
                _stopwatch.Start();
                switch (_wmiType)
                {
                    case WmiType.MemoryFree:
                        ManagementScope scopeAvailable = new ManagementScope(@"\\" + _server);//, new ConnectionOptions());
                        scopeAvailable.Connect();
                        ObjectQuery objectQueryAvailable =
                            new ObjectQuery(
                                "SELECT FreePhysicalMemory FROM Win32_OperatingSystem");
                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scopeAvailable, objectQueryAvailable))
                        {
                            using (ManagementObjectCollection moc = searcher.Get())
                            {
                                using(ManagementObjectCollection.ManagementObjectEnumerator moe = moc.GetEnumerator())
                                {
                                    while (moe.MoveNext())
                                    {
                                        using (ManagementBaseObject mbo = moe.Current)
                                            wr.Value = Convert.ToSingle(mbo["FreePhysicalMemory"]);
                                    }
                                }
                            }
                        }
                        if ((Single) wr.Value < _thresholdWarningFloat)
                        {
                            if ((Single) wr.Value < _thresholdPanicFloat)
                            {
                                wr.Ok = false;
                                wr.Critical = true;
                            }
                            else
                            {
                                wr.Ok = false;
                                wr.Critical = false;
                            }
                        }
                        else
                        {
                            wr.Ok = true;
                        }
                        break;
                    case WmiType.MemoryUsage:
                        ManagementScope scopeInUse = new ManagementScope(@"\\" + _server);//, new ConnectionOptions());
                        scopeInUse.Connect();
                        ObjectQuery objectQueryInUse =
                            new ObjectQuery(
                                "SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scopeInUse, objectQueryInUse))
                        {
                            using (ManagementObjectCollection moc = searcher.Get())
                            {
                                using (ManagementObjectCollection.ManagementObjectEnumerator moe = moc.GetEnumerator())
                                {
                                    while (moe.MoveNext())
                                    {
                                        using (ManagementBaseObject mbo = moe.Current)
                                        {
                                            Single total = Convert.ToSingle(mbo["TotalVisibleMemorySize"]);
                                            Single free = Convert.ToSingle(mbo["FreePhysicalMemory"]);
                                            wr.Value = ((total - free)/total)*100f;
                                        }
                                    }
                                }
                            }
                        }
                        if ((Single)wr.Value > _thresholdWarningFloat)
                        {
                            if ((Single) wr.Value > _thresholdPanicFloat)
                            {
                                wr.Ok = false;
                                wr.Critical = true;
                            }
                            else
                            {
                                wr.Ok = false;
                                wr.Critical = false;
                            }
                        }
                        else
                        {
                            wr.Ok = true;
                        }
                        break;
                }
                _stopwatch.Stop();
                wr.RunLength = _stopwatch.ElapsedMilliseconds;
                return wr;
            }
            catch (Exception ex)
            {
                wr.Ok = false;
                wr.Exception = true;
                wr.Value = new SerializableException(ex);
                _stopwatch.Stop();
                wr.RunLength = _stopwatch.ElapsedMilliseconds;
                return wr;
            }
            finally
            {
                _stopwatch.Reset();
            }
        }

        public override Int32 GetHashCode()
        {
            String s = _friendlyName + _server + _alertInfo.GetHashCode() + 
                       _common + _thresholdBreachCount + _thresholdLessThan + 
                       _thresholdPanic + _thresholdWarning +
                       _updateFrequency + _wmiType;

            return s.GetHashCode();           
        }
    }

    [SerializableAttribute]
    public class WmiResults : IResults<WmiResult>
    {
        private Dictionary<String, WmiResult> _results = new Dictionary<String, WmiResult>();


        #region Implementation of IEnumerable

        public IEnumerator<WmiResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IResults<WmiResult>

        public Dictionary<String, WmiResult> Results
        {
            get { return _results; }
            set { _results = value; }
        }

        public int Count
        {
            get { return _results.Count; }
        }

        public void Clear()
        {
            _results.Clear();
        }

        public void Add(Object result)
        {
            if (result.GetType() == typeof(WmiResult))
                if (!_results.ContainsKey(((WmiResult)result).MonitorHash))
                    _results.Add(((WmiResult)result).MonitorHash, (WmiResult)result);
                else
                    _results[((WmiResult) result).MonitorHash] = (WmiResult) result;
        }

        public void AddRange(IEnumerable<Object> results)
        {
            foreach (Object result in results)
                if (result.GetType() == typeof(WmiResult))
                    if (!_results.ContainsKey(((WmiResult)result).MonitorHash))
                        _results.Add(((WmiResult)result).MonitorHash, (WmiResult)result);
                    else
                        _results[((WmiResult) result).MonitorHash] = (WmiResult) result;
        }
        #endregion
    }

    [XmlInclude(typeof(WmiMonitor))]
    [SerializableAttribute]
    public class WmiResult : IResult
    {
        private WmiMonitor _monitor;
        private readonly DateTime _runTime = DateTime.Now;
        private Boolean _ok = false;
        private Object _value = 0;
        private Boolean _critical = false;
        private Int64 _lastRunLength = 0;
        private Boolean _exception = false;

        public WmiResult() { }

        public WmiResult(WmiMonitor monitor)
        {
            _monitor = monitor;
        }

        public Object Monitor
        {
            get { return _monitor; }
            set
            {
                if (value.GetType() == typeof(WmiMonitor))
                    _monitor = (WmiMonitor)value;
                else
                    throw new Exception("value must be of type WmiMonitor");
            }
        }

        public Boolean Ok
        {
            get { return _ok; }
            set { _ok = value; }
        }
        public Boolean Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public Boolean Critical
        {
            get { return _critical; }
            set { _critical = value; }
        }

        public FullMonitorType Type
        {
            get { return FullMonitorType.Wmi; }
        }

        public String MonitorHash
        {
            get { return _monitor.Hash; }
            set { _monitor.Hash = value; }
        }

        public Int64 RunLength
        {
            get { return _lastRunLength; }
            set { _lastRunLength = value; }
        }

        public DateTime RunTime
        {
            get { return _runTime; }
        }

        public override String ToString()
        {
            return this.Type + " Result Name: " + _monitor.FriendlyName + ", Ok: " + _ok + ", Panic: " + _critical + ", Value: " + _value;
        }

        public Boolean SendAlert()
        {
            return CAlert.SendAlert(this);
        }
    }

    public enum WmiType
    {
        MemoryUsage, // % free memory ... ((total - free) / total) * 100f
        MemoryFree, // free
        None
    }
}

