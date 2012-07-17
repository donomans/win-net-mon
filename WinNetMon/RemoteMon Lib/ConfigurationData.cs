using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    [SerializableAttribute]
    [XmlRootAttribute(ElementName = "Configuration", IsNullable = false)]
    public class ConfigurationData// : IExportable<ConfigurationData>//: IEnumerable<CMonitor>
    {
        public ConfigurationData() { }

        private DateTime _timeStamp = new DateTime();
        private Boolean _verboseLogging = true;
        private MonitorSettings _settings = new MonitorSettings();
        private BasicMonitors _basicMonitors = new BasicMonitors();
        private PfcMonitors _pfcMonitors = new PfcMonitors();
        private ServiceMonitors _serviceMonitors = new ServiceMonitors();
        private EventMonitors _eventMonitors = new EventMonitors();
        private WmiMonitors _wmiMonitors = new WmiMonitors();

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


        [XmlElement("Settings")]
        public MonitorSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }
        
        [XmlElement("Basic")]
        public BasicMonitors BasicMonitors
        {
            get { return _basicMonitors; }
            set { _basicMonitors = value; }
        }
        [XmlElement("PerformanceCounters")]
        public PfcMonitors PfcMonitors
        {
            get { return _pfcMonitors; }
            set { _pfcMonitors = value; }
        }
        [XmlElement("Services")]
        public ServiceMonitors ServiceMonitors
        {
            get { return _serviceMonitors; }
            set { _serviceMonitors = value; }
        }
        [XmlElement("Events")]
        public EventMonitors EventMonitors
        {
            get { return _eventMonitors; }
            set { _eventMonitors = value; }
        }
        [XmlElement("Wmi")]
        public WmiMonitors WmiMonitors
        {
            get { return _wmiMonitors; }
            set { _wmiMonitors = value; }
        }

        [XmlIgnore]
        public Int32 Count 
        {
            get { return _basicMonitors.Count + _eventMonitors.Count + _pfcMonitors.Count + _serviceMonitors.Count + _wmiMonitors.Count; }
        }

        public IMonitor this[String hash]
        {
            get
            {
                IMonitor monitor = _pfcMonitors[hash];
                if (monitor != null)
                    return monitor;
                monitor = _serviceMonitors[hash];
                if (monitor != null)
                    return monitor;
                monitor = _eventMonitors[hash];
                if (monitor != null)
                    return monitor;
                monitor = _basicMonitors[hash];
                if (monitor != null)
                    return monitor;
                monitor = _wmiMonitors[hash];
                return monitor;
            }
        }
        public IMonitor this[FullMonitorType type, String hash]
        {
            get
            {
                switch(type)
                {
                    case FullMonitorType.Basic:
                        return _basicMonitors[hash];
                    case FullMonitorType.EventLog:
                        return _eventMonitors[hash];
                    case FullMonitorType.PerformanceCounter:
                        return _pfcMonitors[hash];
                    case FullMonitorType.Service:
                        return _serviceMonitors[hash];
                    case FullMonitorType.Wmi:
                        return _wmiMonitors[hash];
                }
                return null;
            }
        }
        public void RemoveMonitor(String hash)
        {
            if (_pfcMonitors.Remove(hash))
                return;
            else if (_serviceMonitors.Remove(hash))
                return;
            else if (_eventMonitors.Remove(hash))
                return;
            else if (_basicMonitors.Remove(hash))
                return;
            else if (_wmiMonitors.Remove(hash))
                return;
        }

        public Boolean ExportToXml(String fileName)
        {
            XmlExport xml = new XmlExport(fileName, this);
            return xml.ExportConfigurationData();
        }

        public static ConfigurationData LoadConfiguration(String fileName)
        {
            XmlImport xml = new XmlImport(fileName);
            Object o = xml.Import(typeof(ConfigurationData));
            return o == null ? null : (ConfigurationData)o;
        }

        public void Add(Object monitor)
        {
            switch (((IMonitor)monitor).Type)
            {
                case FullMonitorType.Basic:
                    _basicMonitors.Add(monitor);
                    break;
                case FullMonitorType.EventLog:
                    _eventMonitors.Add(monitor);
                    break;
                case FullMonitorType.PerformanceCounter:
                    _pfcMonitors.Add(monitor);
                    break;
                case FullMonitorType.Service:
                    _serviceMonitors.Add(monitor);
                    break;
                case FullMonitorType.Wmi:
                    _wmiMonitors.Add(monitor);
                    break;
            }
        }

        public IEnumerable<IMonitor> ToEnumerable()
        {
            IMonitor[] monitors = new IMonitor[this.Count];
            Int32 count = 0;
            foreach (BasicMonitor monitor in _basicMonitors)
                monitors[count++] = monitor;
            foreach (WmiMonitor monitor in _wmiMonitors)
                monitors[count++] = monitor;
            foreach (PfcMonitor monitor in _pfcMonitors)
                monitors[count++] = monitor;
            foreach (EventMonitor monitor in _eventMonitors)
                monitors[count++] = monitor;
            foreach (ServiceMonitor monitor in _serviceMonitors)
                monitors[count++] = monitor;

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

    //public interface IExportable<out T>
    //{
    //    Boolean ExportToXml(String fileName);
    //    T LoadConfiguration(String fileName);
    //}

    public interface IMonitors<T> : IEnumerable<T> where T : IMonitor
    {
        Dictionary<String, T> Monitor { get; set; }
        T this[String hash] { get; }
        Boolean Remove(String hash);
        Int32 Count { get; }
    }

    /// <summary>
    /// Base Monitor information
    /// </summary>  
    //[XmlInclude(typeof(WmiMonitor))]
    //[XmlInclude(typeof(BasicMonitor))]
    //[XmlInclude(typeof(PfcMonitor))]
    //[XmlInclude(typeof(ServiceMonitor))]
    //[XmlInclude(typeof(EventMonitor))]
    //[SerializableAttribute]
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
    //[SerializableAttribute]
    public interface IResult//<T> where T : IMonitor
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
    public class Results// : IEnumerable<CResult>
    {
        private ServiceResults _serviceResults = new ServiceResults();
        private WmiResults _wmiResults = new WmiResults();
        private EventResults _eventResults = new EventResults();
        private PfcResults _pfcResults = new PfcResults();
        private BasicResults _basicResults = new BasicResults();

        public Results() { }

        public Results(IEnumerable<IResult> results)
        {
            this.AddRange(results);
        }

        public Int32 Count
        {
            get{ return _serviceResults.Count + _wmiResults.Count + _eventResults.Count + _pfcResults.Count + _basicResults.Count; }
        }

        public ServiceResults ServiceResults
        {
            get { return _serviceResults; }
            set { _serviceResults = value; }
        }
        public WmiResults WmiResults
        {
            get { return _wmiResults; }
            set { _wmiResults = value; }
        }
        public EventResults EventResults
        {
            get { return _eventResults; }
            set { _eventResults = value; }
        }
        public PfcResults PfcResults
        {
            get { return _pfcResults; }
            set { _pfcResults = value; }
        }
        public BasicResults BasicResults
        {
            get { return _basicResults; }
            set { _basicResults = value; }
        }

        public void AddRange(IEnumerable<IResult> results)
        {
            foreach(IResult ir in results)
            {
                switch(ir.Type)
                {
                    case FullMonitorType.Basic:
                        _basicResults.Add(ir);
                        break;
                    case FullMonitorType.EventLog:
                        _eventResults.Add(ir);
                        break;
                    case FullMonitorType.PerformanceCounter:
                        _pfcResults.Add(ir);
                        break;
                    case FullMonitorType.Service:
                        _serviceResults.Add(ir);
                        break;
                    case FullMonitorType.Wmi:
                        _wmiResults.Add(ir);
                        break;
                }
            }
        }
        public void Add(IResult result)
        {
            switch (result.Type)
            {
                case FullMonitorType.Basic:
                    _basicResults.Add(result);
                    break;
                case FullMonitorType.EventLog:
                    _eventResults.Add(result);
                    break;
                case FullMonitorType.PerformanceCounter:
                    _pfcResults.Add(result);
                    break;
                case FullMonitorType.Service:
                    _serviceResults.Add(result);
                    break;
                case FullMonitorType.Wmi:
                    _wmiResults.Add(result);
                    break;
            }
        }

        public void Clear()
        {
            _serviceResults.Clear();
            _basicResults.Clear();
            _eventResults.Clear();
            _pfcResults.Clear();
            _wmiResults.Clear();
        }

        public IEnumerable<IResult> ToEnumerable()
        {
            IResult[] results = new IResult[this.Count];
            Int32 count = 0;
            foreach (BasicResult result in _basicResults)
                results[count++] = result;
            foreach (EventResult result in _eventResults)
                results[count++] = result;
            foreach (PfcResult result in _pfcResults)
                results[count++] = result;
            foreach (ServiceResult result in _serviceResults)
                results[count++] = result;
            foreach (WmiResult result in _wmiResults)
                results[count++] = result;

            return results;
        }

        #region Implementation of IEnumerable
        //public IEnumerator<IResult> GetEnumerator()
        //{
        //    //IEnumerator<Dictionary<String,CResult>> ienumresults = _results.Values.GetEnumerator(); //.GetEnumerator();
        //    List<IResult> results = new List<IResult>();//
        //    foreach (IResult result in _basicResults)
        //        results.Add(result);
        //    foreach (IResult result in _eventResults)
        //        results.Add(result);
        //    foreach (IResult result in _pfcResults)
        //        results.Add(result);
        //    foreach (IResult result in _serviceResults)
        //        results.Add(result);
        //    foreach (IResult result in _wmiResults)
        //        results.Add(result);
        //    //while(ienumresults.MoveNext())
        //    //{
        //    //    if(ienumresults.Current != null)
        //    //        results.AddRange(ienumresults.Current.Values);
        //    //}
        //    //ienumresults.Dispose();
            
        //    return results.GetEnumerator();
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
        #endregion
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
