using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteMon_Lib
{
    [SerializableAttribute]
    [XmlRootAttribute(ElementName = "Configuration", IsNullable = false)]
    public class ConfigurationData
    {
        public ConfigurationData() { }

        private DateTime _timeStamp = new DateTime();
        private Boolean _verboseLogging = true;
        private MonitorSettings _settings = new MonitorSettings();

        private Dictionary<FullMonitorType, IMonitors<IMonitor>> _monitors = new Dictionary<FullMonitorType, IMonitors<IMonitor>>();

        [XmlElement("TimeStamp")]
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        [XmlElement("VerboseLogging")]
        public Boolean VerboseLogging
        {
            get { return _verboseLogging; }
            set { _verboseLogging = value; }
        }

        [XmlIgnore]
        public IEnumerable<IMonitor> AllMonitors
        {
            get { return _monitors.Select<KeyValuePair<FullMonitorType, IMonitors<IMonitor>>, IMonitor>(a => a.Value); }
        }

        [XmlElement("Settings")]
        public MonitorSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }
        
        [XmlElement("Basic")]
        public BasicMonitors BasicMonitors
        {
            get { return (BasicMonitors)_monitors[FullMonitorType.Basic]; }
            set { _monitors[FullMonitorType.Basic] = (IMonitors<IMonitor>)value; }
        }
        [XmlElement("PerformanceCounters")]
        public PfcMonitors PfcMonitors
        {
            get { return (PfcMonitors)_monitors[FullMonitorType.PerformanceCounter]; }
            set { _monitors[FullMonitorType.PerformanceCounter] = (IMonitors<IMonitor>)value; }
        }
        [XmlElement("Services")]
        public ServiceMonitors ServiceMonitors
        {
            get { return (ServiceMonitors)_monitors[FullMonitorType.Service]; }
            set { _monitors[FullMonitorType.Service] = (IMonitors<IMonitor>)value; }
        }
        [XmlElement("Events")]
        public EventMonitors EventMonitors
        {
            get { return (EventMonitors)_monitors[FullMonitorType.EventLog]; }
            set { _monitors[FullMonitorType.EventLog] = (IMonitors<IMonitor>)value; }
        }
        [XmlElement("Wmi")]
        public WmiMonitors WmiMonitors
        {
            get { return (WmiMonitors)_monitors[FullMonitorType.Wmi]; }
            set { _monitors[FullMonitorType.Wmi] = (IMonitors<IMonitor>)value; }
        }

        [XmlIgnore]
        public Int32 Count 
        {
            get { return _monitors.Sum(a => a.Value.Count);}
        }

        public IMonitor this[String hash]
        {
            get
            {
                IMonitor monitor = null;
                _monitors.Map(a =>
                {
                    if (a.Value.Contains(hash))
                    {
                        monitor = a.Value[hash];
                        return;
                    }
                });
                return monitor;
            }
        }
        public IMonitor this[FullMonitorType type, String hash]
        {
            get { return _monitors[type][hash]; }
        }
        public void RemoveMonitor(String hash)
        {
            _monitors.Map(a =>
            {
                if (a.Value.Remove(hash))
                    return;
            });
        }

        public Boolean ExportToXml(String fileName)
        {            
            return new XmlExport(fileName, this).ExportConfigurationData();
        }

        public static ConfigurationData LoadConfiguration(String fileName)
        {
            XmlImport xml = new XmlImport(fileName);
            Object o = xml.Import(typeof(ConfigurationData));
            return o == null ? null : (ConfigurationData)o;
        }

        public void Add(Object monitor)
        {
            _monitors[((IMonitor)monitor).Type].Add(monitor);
        }

        public IEnumerable<IMonitor> ToEnumerable()
        {
            IMonitor[] monitors = new IMonitor[this.Count];
            Int32 count = 0;
            _monitors.Map(t =>
                {
                    t.Value.Map(m =>
                        {
                            monitors[count++] = m;
                        });
                });

            return monitors;
        }
        #region Implementation of IEnumerable

        //public IEnumerator<IMonitor> GetEnumerator()
        //{
        //    List<IMonitor> monitors = new List<IMonitor>();
        //    foreach (BasicMonitor monitor in _basicMonitors)
        //        monitors.Add(monitor);
        //    foreach (WmiMonitor monitor in _wmiMonitors)
        //        monitors.Add(monitor);
        //    foreach (PfcMonitor monitor in _pfcMonitors)
        //        monitors.Add(monitor);
        //    foreach (EventMonitor monitor in _eventMonitors)
        //        monitors.Add(monitor);
        //    foreach (ServiceMonitor monitor in _serviceMonitors)
        //        monitors.Add(monitor);

        //    return monitors.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        #endregion
    }

    
    public interface IMonitors<T> : IEnumerable<T> where T : IMonitor
    {
        Dictionary<String, T> Monitor { get; set; }
        T this[String hash] { get; }
        Boolean Remove(String hash);
        Int32 Count { get; }

        void Add(Object monitor);
        Boolean Contains(String hash);
    }

    /// <summary>
    /// Base Monitor information
    /// </summary>  
    public interface IMonitor
    {
        String Server { get; set; } //NOTE: store the ip/hostname or url
        FullMonitorType Type { get; }
        MonitorBaseType MonitorBaseType { get; }
        String FriendlyName { get; set; }
        Alerts AlertInfo { get; set; }
        /// <summary>
        /// How often to check the monitor
        /// <remarks>In milliseconds</remarks>
        /// </summary>
        Int32 UpdateFrequency { get; set; }
        Int32 ThresholdBreachCount { get; }
        String Hash { get; }
        Boolean Common { get; set; }
        String ToString();
        IResult Check();
        Int32 GetHashCode();
    }

    public interface IResults<T> : IEnumerable<T> where T : IResult
    {
        Dictionary<String, T> Results { get; set; }
        Int32 Count { get; }
        void Clear();
        void Add(Object result);
        void AddRange(IEnumerable<Object> results);
    }

    [XmlInclude(typeof(WmiMonitor))]
    [XmlInclude(typeof(BasicMonitor))]
    [XmlInclude(typeof(PfcMonitor))]
    [XmlInclude(typeof(ServiceMonitor))]
    [XmlInclude(typeof(EventMonitor))]
    public interface IResult
    {
        Object Monitor { get; set; } //had to change to object - IMonitor won't serialize... this is so dumb.
        Boolean Ok { get; set; }
        Boolean Exception { get; set; }
        Object Value { get; set; }
        FullMonitorType Type { get; }
        String MonitorHash { get; }//set; }
        Int64 RunLength { get; set; }
        DateTime RunTime { get; }
        String ToString();
        Boolean SendAlert();
    }

    [XmlInclude(typeof(WmiResult))]
    [XmlInclude(typeof(BasicResult))]
    [XmlInclude(typeof(PfcResult))]
    [XmlInclude(typeof(ServiceResult))]
    [XmlInclude(typeof(EventResult))]
    [SerializableAttribute]
    public class Results
    {
        private Dictionary<FullMonitorType, IResults<IResult>> _results = new Dictionary<FullMonitorType, IResults<IResult>>();

        public Results() { }

        public Results(IEnumerable<IResult> results)
        {
            this.AddRange(results);
        }

        public Int32 Count
        {
            get { return _results.Sum(a => a.Value.Count); }
        }

        public ServiceResults ServiceResults
        {
            get { return (ServiceResults)_results[FullMonitorType.Service]; }
            set { _results[FullMonitorType.Service] = (IResults<IResult>)value; }            
        }
        public WmiResults WmiResults
        {
            get { return (WmiResults)_results[FullMonitorType.Wmi]; }
            set { _results[FullMonitorType.Wmi] = (IResults<IResult>)value; }
        }
        public EventResults EventResults
        {
            get { return (EventResults)_results[FullMonitorType.EventLog]; }
            set { _results[FullMonitorType.EventLog] = (IResults<IResult>)value; }
        }
        public PfcResults PfcResults
        {
            get { return (PfcResults)_results[FullMonitorType.PerformanceCounter]; }
            set { _results[FullMonitorType.PerformanceCounter] = (IResults<IResult>)value; }
        }
        public BasicResults BasicResults
        {
            get { return (BasicResults)_results[FullMonitorType.Basic]; }
            set { _results[FullMonitorType.Basic] = (IResults<IResult>)value; }
        }

        public void AddRange(IEnumerable<IResult> results)
        {
            results.Map(r => _results[r.Type].Add(r));            
        }
        public void Add(IResult result)
        {
            _results[result.Type].Add(result);           
        }

        public void Clear()
        {
            _results.Clear();            
        }

        public IEnumerable<IResult> ToEnumerable()
        {
            IResult[] results = new IResult[this.Count];
            Int32 count = 0;

            _results.Map(t =>
                {
                    t.Value.Map(r =>
                        {
                            results[count++] = r;
                        });
                });           
            return results;
        } 
    }




    public enum MonitorBaseType
    {
        Advanced,
        Basic,
        Common
    }
    public enum FullMonitorType
    {
        PerformanceCounter,
        EventLog,
        Service,
        Basic,
        Common,
        Wmi,
        None
    }
    public enum CommonMonitorType
    {
        HddUsage,
        MemoryUsage,
        SwapFileUsage,
        CpuUsage,
        ProcessState,
        ServiceState,
        None
    }
}
