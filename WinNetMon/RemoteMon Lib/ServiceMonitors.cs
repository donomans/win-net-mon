using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    /// <summary>
    /// Class of services that should be watched.
    /// </summary>
    [SerializableAttribute]
    public class ServiceMonitors : IMonitors<ServiceMonitor>
    {
        private Dictionary<String, ServiceMonitor> _services = new Dictionary<String, ServiceMonitor>();

        public Dictionary<String, ServiceMonitor> Monitor
        {
            get { return _services; }
            set { _services = value; }
        }

        public Boolean Remove(String hash)
        {
            return _services.Remove(hash);
        }

        public void Add(Object serviceMonitor)
        {
            ServiceMonitor service = (ServiceMonitor) serviceMonitor;
            _services.Add(service.Hash, service);
        }
        public Boolean Contains(String hash)
        {
            return _services.ContainsKey(hash);
        }



        [XmlIgnore]
        public Int32 Count { get { return _services.Count; } }

        public ServiceMonitor this[String hash]
        {
            get
            {
                return _services.ContainsKey(hash) ? _services[hash] : null;
            }
        }

        #region IEnumerable Members
        public IEnumerator<ServiceMonitor> GetEnumerator()
        {
            return _services.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }

    [SerializableAttribute]
    public class ServiceMonitor : IMonitor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private String _friendlyName = "";
        private Alerts _alertInfo = new Alerts();
        private String _server = "";
        private Int32 _updateFrequency = 30000;
        private List<Service> _services = new List<Service>();
        private Boolean _automaticRestart = false;
        private Boolean _common = false;
        private String _hash = "";

        public List<Service> Services
        {
            get { return _services; }
            set { _services = value; }
        }
        public Boolean Common
        {
            get { return _common; }
            set { _common = value; }
        }

        public String Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public FullMonitorType Type
        {
            get { return FullMonitorType.Service; }
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
        public Int32 ThresholdBreachCount
        {
            get { return 1; }
        }
        public Boolean AutomaticRestart
        {
            get { return _automaticRestart; }
            set { _automaticRestart = value; }
        }

        [XmlIgnore]
        public String Hash
        {
            get
            {
                if (_hash != "")
                    return _hash;
                _hash = Guid.NewGuid().ToString();// XmlExport.ByteArrayToString(XmlExport.Serializer(typeof(ServiceMonitor), this));
                return _hash;
            }
            set { _hash = value; }
        }

        public override String ToString()
        {
            List<String> slist = new List<String>(_services.Count);
            foreach(Service s in _services)
            {
                slist.Add(s.ToString());
            }
            if(slist.Count > 3)
                return String.Join("; ", slist.ToArray(), 0, 3) + "...";
            
            return String.Join("; ", slist.ToArray(), 0, slist.Count < 3 ? slist.Count : 3);
        }

        public IResult Check()
        {
            ServiceResult sr = new ServiceResult(this);// {MonitorHash = Hash};
            ServiceController sc = null;
            try
            {
                _stopwatch.Start();
                foreach (Service service in _services)
                {
                    sc = new ServiceController(service.ServiceName, _server);
                    Boolean ok = (service.GoodStatus == ServiceStatus.Running
                                      ? (sc.Status !=
                                         ServiceControllerStatus.Running
                                             ? false
                                             : true)
                                      : (sc.Status !=
                                         ServiceControllerStatus.Stopped
                                             ? false
                                             : true));
                    try
                    {
                        if (_automaticRestart)
                        {
                            if (!ok)
                            {
                                if (sc.Status == ServiceControllerStatus.Running)
                                    sc.Stop();
                                else
                                    sc.Start();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.Instance.LogException(this.GetType(), ex);
                    }

                    ServiceResultStatuses srs = (ServiceResultStatuses) sr.Value;
                    srs.Add(new ServiceResultStatus()
                                {
                                    Service = service,
                                    Ok = ok
                                });
                    sc.Dispose();
                }
                _stopwatch.Stop();
                sr.RunLength = _stopwatch.ElapsedMilliseconds;
                return sr;
            }
            catch (Exception ex)
            {
                sr.Ok = false;
                sr.Exception = true;
                sr.Value = new SerializableException(ex);
                _stopwatch.Stop();
                sr.RunLength = _stopwatch.ElapsedMilliseconds;
                return sr;
            }
            finally
            {
                if(sc != null)
                    sc.Dispose();
                _stopwatch.Reset();
            }
        }

        public new unsafe Int32 GetHashCode()
        {
            String s = _friendlyName + _server + _alertInfo + _common +
                       _automaticRestart + _updateFrequency;
            foreach (Service service in _services)
                s += service.ToString();

            fixed (Char* str = s.ToCharArray())
            {
                Char* chPtr = str;
                Int32 x = 0x34058547;
                Int32 y = x;
                Int32* intPtr = (Int32*)chPtr;
                for (Int32 i = s.Length; i > 0; i -= 4)
                {
                    x = (((x << 5) + x) + (x >> 0x1B)) ^ intPtr[0];
                    if (i <= 2)
                    {
                        break;
                    }
                    y = (((y << 5) + y) + (y >> 0x1B)) ^ intPtr[1];
                    intPtr += 2;
                }
                return (x + (y * 0x214BD12C));
            }
        }
    }

    [SerializableAttribute]
    public class ServiceResults: IResults<ServiceResult>
    {
        private Dictionary<String, ServiceResult> _results = new Dictionary<String, ServiceResult>();

        #region Implementation of IEnumerable

        public IEnumerator<ServiceResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IResults<ServiceResult>

        public Dictionary<String, ServiceResult> Results
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
            if (result.GetType() == typeof(ServiceResult))
                if (!_results.ContainsKey(((ServiceResult)result).MonitorHash))
                    _results.Add(((ServiceResult) result).MonitorHash, (ServiceResult) result);
                else
                    _results[((ServiceResult)result).MonitorHash] = (ServiceResult)result;
        }

        public void AddRange(IEnumerable<Object> results)
        {
            foreach(Object result in results)
                if (result.GetType() == typeof(ServiceResult))
                    if (!_results.ContainsKey(((ServiceResult)result).MonitorHash)) 
                        _results.Add(((ServiceResult)result).MonitorHash, (ServiceResult)result);
                    else
                        _results[((ServiceResult)result).MonitorHash] = (ServiceResult)result;
        }

        #endregion
    }

    [XmlInclude(typeof(ServiceMonitor))]
    [XmlInclude(typeof(ServiceResultStatuses))]
    [SerializableAttribute]
    public class ServiceResult : IResult
    {
        private ServiceMonitor _monitor;
        private Object _value = new ServiceResultStatuses();
        private readonly DateTime _runTime = DateTime.Now;
        private Int64 _lastRunLength = 0;
        private Boolean _exception = false;

        public ServiceResult() { }

        public ServiceResult(ServiceMonitor monitor)
        {
            _monitor = monitor;
        }

        public Object Monitor
        {
            get { return _monitor; }
            set
            {
                if(value.GetType() == typeof(ServiceMonitor))
                    _monitor = (ServiceMonitor)value;
                else
                    throw new Exception("value must be of type ServiceMonitor");
            }
        }

        public Boolean Ok
        {
            get 
            {
                return _value.GetType() == typeof(ServiceResultStatuses) && ((ServiceResultStatuses)_value).Ok;
            }
            set { }
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

        public FullMonitorType Type
        {
            get { return FullMonitorType.Service; }
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
            return this.Type + " Result Name: " + _monitor.FriendlyName + ", Ok: " + Ok + ", Value: " + _value;
        }

        public Boolean SendAlert()
        {
            return CAlert.SendAlert(this);
        }
    }

    [XmlInclude(typeof(ServiceResultStatus))]
    [SerializableAttribute]
    public class ServiceResultStatuses: IEnumerable<ServiceResultStatus>
    {
        private List<ServiceResultStatus> _serviceResultStatuses = new List<ServiceResultStatus>();

        public ServiceResultStatuses() { }

        public List<ServiceResultStatus> ServiceResults
        {
            get { return _serviceResultStatuses; }
            set { _serviceResultStatuses = value; }
        }

        public void Add(ServiceResultStatus serviceResultStatus)
        {
            _serviceResultStatuses.Add(serviceResultStatus);
        }

        public Boolean Ok
        {
            get
            {
                foreach (ServiceResultStatus srs in _serviceResultStatuses)
                {
                    if (!srs.Ok)
                        return false;
                }
                return true;
            }
        }
        public void AddRange(IEnumerable<ServiceResultStatus> serviceResultStatuses)
        {
            _serviceResultStatuses.AddRange(serviceResultStatuses);
        }
        public IEnumerator<ServiceResultStatus> GetEnumerator()
        {
            return _serviceResultStatuses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public String ToStringFull()
        {
            List<String> slist = new List<String>(_serviceResultStatuses.Count);
            foreach (ServiceResultStatus s in _serviceResultStatuses)
            {
                slist.Add(s.ToString());
            }
            //if (slist.Count > 3)
            //    return String.Join("; ", slist.ToArray(), 0, 3) + "...";
            return String.Join("; ", slist.ToArray(), 0, 3);
        }
        public override String ToString()
        {
            return "Status: " + (Ok ? "OK" : "Panic");
        }
    }
    
    [SerializableAttribute]
    public class ServiceResultStatus
    {
        private Service _service;
        private Boolean _ok;

        public ServiceResultStatus() { }

        public Service Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public Boolean Ok
        {
            get { return _ok; }
            set { _ok = value; }
        }

        public override String ToString()
        {
            return "Service Name: " + _service.ServiceName + ", Current Status: " + (_ok ? _service.GoodStatus.ToString() : (_service.GoodStatus == ServiceStatus.Running ? ServiceStatus.Stopped.ToString() : ServiceStatus.Running.ToString()));
        }
    }

    [SerializableAttribute]
    public class Service
    {
        public String ServiceName = "";
        
        public Service() { }

        public Service(String serviceName)
        {
            ServiceName = serviceName;
        }

        /// <summary>
        /// Alert if the status is not GoodStatus
        /// </summary>
        public ServiceStatus GoodStatus = ServiceStatus.Running;

        public override String ToString()
        {
            return "Service Name: " + ServiceName + ", Good Status: " + GoodStatus;
        }
    }
    public enum ServiceStatus
    {
        Running,
        Stopped
    }
}