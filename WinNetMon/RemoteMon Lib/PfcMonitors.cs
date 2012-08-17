using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    [SerializableAttribute]
    public class PfcMonitors : IMonitors<PfcMonitor>
    {
        private Dictionary<String, PfcMonitor> _pfcTypes = new Dictionary<String, PfcMonitor>();

        /// <summary>
        /// Return the PfcMonitor
        /// </summary>
        /// <param name="hash">Hash value of the PfcMonitor class</param>
        /// <returns></returns>
        public PfcMonitor this[String hash]
        {
            get
            {
                return _pfcTypes.ContainsKey(hash) ? _pfcTypes[hash] : null;
            }
        }

        public Boolean Remove(String hash)
        {
            return _pfcTypes.Remove(hash);
        }
        public void Add(Object pfcMonitor)
        {
            PfcMonitor pfc = (PfcMonitor) pfcMonitor;
            _pfcTypes.Add(pfc.Hash, pfc);
        }
        public Boolean Contains(String hash)
        {
            return _pfcTypes.ContainsKey(hash);
        }



        [XmlIgnore]
        public Int32 Count { get { return _pfcTypes.Count; } }

        public Dictionary<String, PfcMonitor> Monitor
        {
            get { return _pfcTypes; }
            set { _pfcTypes = value; }
        }


        #region IEnumerable Members
        public IEnumerator<PfcMonitor> GetEnumerator()
        {
            return _pfcTypes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

    }

    [SerializableAttribute]
    public class PfcMonitor : IMonitor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private String _friendlyName = "";
        private Alerts _alertInfo = new Alerts();
        private String _server = "";
        private Int32 _updateFrequency = 10000;
        private Single _thresholdPanicFloat = 0f;
        private String _thresholdPanic = "";
        private Single _thresholdWarningFloat = 0f; 
        private String _thresholdWarning = "";
        private String _category = "";
        private String _counter = "";
        private String _instance = "";
        private Int32 _thresholdBreachCount = 1;
        private Boolean _thresholdLessThan = false;
        private Boolean _common = false;
        private String _hash = "";
        private PerformanceCounter _performanceCounter;

        public override String ToString()
        {
            return "Category Name: " + _category + ", Counter Name: " + _counter + ", Counter Instance: " + _instance;
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
        public String Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public String Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }

        public String Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        public Int32 ThresholdBreachCount
        {
            get { return _thresholdBreachCount; }
            set { _thresholdBreachCount = value; }
        }

        public String Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public FullMonitorType Type
        {
            get { return FullMonitorType.PerformanceCounter; }
        }
        public MonitorBaseType MonitorBaseType
        {
            get { return MonitorBaseType.Advanced; }
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
                _hash = Guid.NewGuid().ToString();// XmlExport.ByteArrayToString(XmlExport.Serializer(typeof(PfcMonitor), this));
                return _hash;
            }
            set { _hash = value; }
        }

        public IResult Check()
        {
            PfcResult pr = new PfcResult(this);// {MonitorHash = Hash};
            //PerformanceCounter performanceCounter = null;
            try
            {
                _stopwatch.Start();
                if (_performanceCounter == null)
                {
                    _performanceCounter = new PerformanceCounter
                                              {
                                                  CategoryName = _category,
                                                  CounterName = _counter,
                                                  InstanceName = _instance != "None" ? _instance : "",
                                                  MachineName = _server
                                              };
                    //if (_instance != "None")
                    //    _performanceCounter = new PerformanceCounter(_category, _counter, _instance, _server);
                    //else
                    //    _performanceCounter = new PerformanceCounter(_category, _counter, "", _server);
                }
                Single nextValue = _performanceCounter.NextValue();
                //if(nextValue == 0f)
                //    nextValue = _performanceCounter.NextValue();
                if (_thresholdLessThan)
                {
                    if (nextValue < _thresholdWarningFloat)
                    {
                        if (nextValue < _thresholdPanicFloat)
                        {
                            pr.Ok = false;
                            pr.Critical = true;
                            pr.Value = nextValue;
                        }
                        else
                        {
                            pr.Ok = false;
                            pr.Critical = false;
                            pr.Value = nextValue;
                        }
                    }
                    else
                    {
                        pr.Ok = true;
                        pr.Critical = false;
                        pr.Value = nextValue;
                    }
                }
                else
                {
                    if (nextValue > _thresholdWarningFloat)
                    {
                        if (nextValue > _thresholdPanicFloat)
                        {
                            pr.Ok = false;
                            pr.Critical = true;
                            pr.Value = nextValue;
                        }
                        else
                        {
                            pr.Ok = false;
                            pr.Critical = false;
                            pr.Value = nextValue;
                        }
                    }
                    else
                    {
                        pr.Ok = true;
                        pr.Critical = false;
                        pr.Value = nextValue;
                    }
                }
                _stopwatch.Stop();
                pr.RunLength = _stopwatch.ElapsedMilliseconds;
                return pr;
            }
            catch (Exception ex)
            {
                pr.Ok = false;
                pr.Exception = true;
                pr.Value = new SerializableException(ex);
                _stopwatch.Stop();
                pr.RunLength = _stopwatch.ElapsedMilliseconds;
                return pr;
            }
            finally
            {
                //if (performanceCounter != null)
                //    performanceCounter.Dispose();
                _stopwatch.Reset();
            }
        }

        ~PfcMonitor()
        {
            if(_performanceCounter != null)
                _performanceCounter.Dispose();
        }

        public override Int32 GetHashCode()
        {
            String s = _friendlyName + _server + _alertInfo.GetHashCode() + _common +
                       _updateFrequency + _category + _counter + _instance +
                       _thresholdBreachCount + _thresholdLessThan + _thresholdPanic +
                       _thresholdWarning;
            return s.GetHashCode();
        }
    }

    [SerializableAttribute]
    public class PfcResults : IResults<PfcResult>
    {
        private Dictionary<String, PfcResult> _results = new Dictionary<String, PfcResult>();


        #region Implementation of IEnumerable

        public IEnumerator<PfcResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IResults<WmiResult>

        public Dictionary<String, PfcResult> Results
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
            if (result.GetType() == typeof(PfcResult))
                if (!_results.ContainsKey(((PfcResult)result).MonitorHash))
                    _results.Add(((PfcResult)result).MonitorHash, (PfcResult)result);
                else
                    _results[((PfcResult) result).MonitorHash] = (PfcResult) result;
        }

        public void AddRange(IEnumerable<Object> results)
        {
            foreach (Object result in results)
            {
                if (result.GetType() == typeof(WmiResult))
                    if (!_results.ContainsKey(((PfcResult)result).MonitorHash))
                        _results.Add(((PfcResult)result).MonitorHash, (PfcResult)result);
                    else
                        _results[((PfcResult) result).MonitorHash] = (PfcResult) result;
            }
        }
        #endregion
    }


    [XmlInclude(typeof(PfcMonitor))]
    [SerializableAttribute]
    public class PfcResult : IResult
    {
        private PfcMonitor _monitor;
        private readonly DateTime _runTime = DateTime.Now;
        private Boolean _ok = false;
        private Object _value = 0;
        private Boolean _critical = false;
        private Int64 _lastRunLength = 0;
        private Boolean _exception = false;

        public PfcResult() { }

        public PfcResult(PfcMonitor monitor)
        {
            _monitor = monitor;
        }

        public Object Monitor
        {
            get { return _monitor; }
            set
            {
                if (value.GetType() == typeof(PfcMonitor))
                    _monitor = (PfcMonitor)value;
                else
                    throw new Exception("value must be of type PfcMonitor");
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
            get { return FullMonitorType.PerformanceCounter; }
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
            return Alert.SendAlert(this);
        }
    }

}