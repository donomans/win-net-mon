using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    /// <summary>
    /// Monitor for ping, http request, file/directory check, 
    /// </summary>
    [SerializableAttribute]
    public class BasicMonitors : IMonitors<BasicMonitor>
    {
        private Dictionary<String, BasicMonitor> _basic = new Dictionary<String, BasicMonitor>();

        public Dictionary<String, BasicMonitor> Monitor
        {
            get { return _basic; }
            set { _basic = value; }
        }
        
        public Boolean Remove(String hash)
        {
            return _basic.Remove(hash);
        }

        public void Add(Object basicMonitor)
        {
            BasicMonitor basic = (BasicMonitor)basicMonitor;
            _basic.Add(basic.Hash, basic);
        }

        [XmlIgnore]
        public Int32 Count { get { return _basic.Count; } }

        public BasicMonitor this[String hash]
        {
            get
            {
                return _basic.ContainsKey(hash) ? _basic[hash] : null;
            }
        }

        /// <summary>
        /// Get the BasicMonitor(s) of a certain BasicMonitorType
        /// </summary>
        /// <param name="type"></param>
        /// <returns>List of BasicMonitor with that monitor type</returns>
        public List<BasicMonitor> this[BasicMonitorType type]
        {
            get
            {
                List<BasicMonitor> basic = new List<BasicMonitor>(_basic.Count);
                foreach(BasicMonitor bm in _basic.Values)
                    if(bm.BasicMonitorType == type)
                        basic.Add(bm);
                return basic;
            }
        }
    
        #region IEnumerable Members
        public IEnumerator<BasicMonitor> GetEnumerator()
        {
            return _basic.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        
    }

    [SerializableAttribute]
    public class BasicMonitor : IMonitor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private String _friendlyName = "";
        private Alerts _alertInfo = new Alerts();
        //private String _server = "";
        private Int32 _updateFrequency = 300000;
        private BasicMonitorType _basicMonitorType = BasicMonitorType.None;
        private Credentials _credential = new Credentials();
        private BasicMonitorData _data = new BasicMonitorData();
        //private String _description = "";
        private Boolean _common = false;
        private String _hash = "";

        public override String ToString()
        {
            return "Type: " + _basicMonitorType + ", Info: " + _data.UrlUncIp + (_data.Port != 0 ? " at port: " + _data.Port : "");
        }

        public Boolean Common
        {
            get { return _common; }
            set { _common = value; }
        }
        public BasicMonitorType BasicMonitorType
        {
            get { return _basicMonitorType; }
            set { _basicMonitorType = value; }
        }
        public Credentials Credential
        {
            get { return _credential; }
            set { _credential = value; }
        }
        public BasicMonitorData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public String Server
        {
            get { return _data.UrlUncIp; }
            set { _data.UrlUncIp = value; }
        }
        public FullMonitorType Type
        {
            get { return FullMonitorType.Basic; }
        }
        public MonitorBaseType MonitorBaseType
        {
            get { return MonitorBaseType.Basic; }
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

        [XmlIgnore]
        public String Hash
        {
            get
            {
                if (_hash != "")
                    return _hash;
                _hash = Guid.NewGuid().ToString();// XmlExport.ByteArrayToString(XmlExport.Serializer(typeof(BasicMonitor), this));
                return _hash;
            }
            set { _hash = value; }
        }

        public IResult Check()
        {
            BasicResult br = new BasicResult(this);
            Ping ping = null;
            WebResponse response = null;
            try
            {
                _stopwatch.Start();
                switch (_basicMonitorType)
                {
                    case BasicMonitorType.Ftp:
                        FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(_data.UrlUncIp);
                        ftp.EnableSsl = _credential.UseSecure;
                        ftp.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                        if (_credential.Username != "")
                            if (_credential.Domain != "")
                                ftp.Credentials = new NetworkCredential(_credential.Username, _credential.Password,
                                                                        _credential.Domain);
                            else
                                ftp.Credentials = new NetworkCredential(_credential.Username, _credential.Password);
                        using (response = ftp.GetResponse())
                        {
                            if (response != null)
                            {    
                                using (Stream str = response.GetResponseStream())
                                {
                                    if (((FtpWebResponse) response).StatusCode == FtpStatusCode.PathnameCreated)
                                    {
                                        br.Ok = true;
                                        br.Value = "OK"; //ftpresponse.StatusDescription;
                                    }
                                    else
                                    {
                                        br.Ok = false;
                                        br.Value = "OK"; //ftpresponse.StatusDescription;
                                    }
                                }
                            }
                        }
                        break;
                    case BasicMonitorType.Http:
                        HttpWebRequest http = (HttpWebRequest)WebRequest.Create(_data.UrlUncIp);
                        http.AllowAutoRedirect = true;
                        http.UserAgent =
                            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MDDC)";

                        using (response = http.GetResponse())
                        {
                            using (Stream str = response.GetResponseStream())
                            {
                                if (((HttpWebResponse) response).StatusCode == HttpStatusCode.OK)
                                {
                                    br.Ok = true;
                                    br.Value = ((HttpWebResponse) response).StatusDescription;
                                }
                                else
                                {
                                    br.Ok = false;
                                    br.Value = ((HttpWebResponse) response).StatusDescription;
                                }
                            }
                        }
                        break;
                    case BasicMonitorType.Ping:
                        using (ping = new Ping())
                        {
                            PingReply pr = ping.Send(_data.UrlUncIp);
                            if (pr != null)
                            {
                                br.Ok = pr.Status == IPStatus.Success ? true : false;
                                br.Value = pr.Status.ToString() + ": " + pr.RoundtripTime.ToString() + "ms";
                            }
                            else
                            {
                                br.Ok = false;
                                br.Value = "Failure: null response.";
                            }
                        }
                        break;
                }
                _stopwatch.Stop();
                br.RunLength = _stopwatch.ElapsedMilliseconds;
                return br;
            }
            catch (Exception ex)
            {
                br.Ok = false;
                br.Exception = true;
                br.Value = new SerializableException(ex);
                _stopwatch.Stop();
                br.RunLength = _stopwatch.ElapsedMilliseconds;
                return br;
            }
            finally
            {
                _stopwatch.Reset();
                if(response != null)
                    response.Close();
            }
        }

        public new unsafe Int32 GetHashCode()
        {
            String s = _friendlyName + _alertInfo + _common +
                       _updateFrequency + _basicMonitorType + 
                       _credential + _data;

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
    public class BasicResults : IResults<BasicResult>
    {
        private Dictionary<String, BasicResult> _results = new Dictionary<String, BasicResult>();


        #region Implementation of IEnumerable

        public IEnumerator<BasicResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IResults<WmiResult>

        public Dictionary<String, BasicResult> Results
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
            if (result.GetType() == typeof(BasicResult))
                if (!_results.ContainsKey(((BasicResult)result).MonitorHash))
                    _results.Add(((BasicResult)result).MonitorHash, (BasicResult)result);
                else
                    _results[((BasicResult) result).MonitorHash] = (BasicResult) result;
        }

        public void AddRange(IEnumerable<Object> results)
        {
            foreach (Object result in results)
                if (result.GetType() == typeof(BasicResult))
                    if (!_results.ContainsKey(((BasicResult)result).MonitorHash))
                        _results.Add(((BasicResult)result).MonitorHash, (BasicResult)result);
                    else
                        _results[((BasicResult) result).MonitorHash] = (BasicResult) result;
        }
        #endregion
    }


    [XmlInclude(typeof(BasicMonitor))]
    [SerializableAttribute]
    public class BasicResult: IResult
    {
        private BasicMonitor _monitor;
        private Object _value = "";
        private Boolean _ok = false;
        private Int64 _lastRunLength = 0;
        private Boolean _exception = false;
        private readonly DateTime _runTime = DateTime.Now;

        public BasicResult() { }

        public BasicResult(BasicMonitor monitor)
        {
            _monitor = monitor;
        }

        public Object Monitor
        {
            get { return _monitor; }
            set
            {
                if (value.GetType() == typeof(BasicMonitor))
                    _monitor = (BasicMonitor)value;
                else
                    throw new Exception("value must be of type BasicMonitor");
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

        public FullMonitorType Type
        {
            get { return FullMonitorType.Basic; }
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
            return this.Type + " Result Name: " + _monitor.FriendlyName + ", Ok: " + _ok + ", Value: " + _value;
        }

        public Boolean SendAlert()
        {
            return CAlert.SendAlert(this);
        }
    }

    public enum BasicMonitorType
    {
        Ping,
        Http,
        Ftp,
        //FileWatch,
        //DirectoryWatch,
        //Telnet,
        None
    }
    [SerializableAttribute]
    public class BasicMonitorData
    {
        private String _urlUncIp = "";
        private Int32 _port = 0;
        
        public String UrlUncIp
        {
            get { return _urlUncIp; }
            set { _urlUncIp = value; }
        }
        public Int32 Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }
    [SerializableAttribute]
    public class Credentials
    {
        private String _username = "";
        private String _password = "";
        private String _domain = "";
        private Boolean _useSecure = false;

        public String Username
        {
            get { return _username; } 
            set { _username = value; /*IsValid = true;*/ }
        }
        public String Password
        {
            get { return _password; } 
            set { _password = value; /*IsValid = true;*/ }
        }
        public String Domain
        {
            get { return _domain; } 
            set { _domain = value; /*IsValid = true;*/ }
        }
        public Boolean UseSecure
        {
            get { return _useSecure; }
            set { _useSecure = value; }
        }
    }
}