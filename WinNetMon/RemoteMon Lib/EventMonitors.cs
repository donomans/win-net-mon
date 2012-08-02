using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    /// <summary>
    /// Class of Events to monitor
    /// </summary>
    [SerializableAttribute]
    public class EventMonitors : IMonitors<EventMonitor>
    {
        private Dictionary<String, EventMonitor> _events = new Dictionary<String, EventMonitor>();
        
       
        public EventMonitor this[String hash]
        {
            get
            {
                return _events.ContainsKey(hash) ? _events[hash] : null;
            }
        }

        public Boolean Remove(String hash)
        {
            return _events.Remove(hash);
        }

        public void Add(Object eventMonitor)
        {
            EventMonitor em = (EventMonitor) eventMonitor;
            _events.Add(em.Hash, em);
        }

        public Boolean Contains(String hash)
        {
            return _events.ContainsKey(hash);
        }


        [XmlIgnore]
        public Int32 Count { get { return _events.Count; } }

        public Dictionary<String, EventMonitor> Monitor
        {
            get { return _events; }
            set { _events = value; }
        }

        #region IEnumerable Members
        public IEnumerator<EventMonitor> GetEnumerator()
        {
            return _events.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
    [SerializableAttribute]
    public class EventMonitor : IMonitor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private String _eventLogKind = "";
        private String _eventNameMatch = "";
        private EventLogEntryType _eventType = EventLogEntryType.Warning;
        private Boolean _clearOldLogs = false;
        private String _friendlyName = "";
        private Alerts _alertInfo = new Alerts();
        private String _server = "";
        private Int32 _updateFrequency = 90000;
        private Boolean _common = false;
        private DateTime _startTime = DateTime.Now;
        private String _hash = "";

        public override String ToString()
        {
            return "Event Log Type: " + _eventLogKind + ", Event Log Entry Types: " + _eventType + ", Event Log Match: " + _eventNameMatch;
        }

        public Boolean Common
        {
            get { return _common; }
            set { _common = value; }
        }
        public String EventLogKind
        {
            get { return _eventLogKind; }
            set { _eventLogKind = value; }
        }
        public String EventNameMatch
        {
            get { return _eventNameMatch; }
            set { _eventNameMatch = value; }
        }
        public EventLogEntryType EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }
        public Boolean ClearOldLogs
        {
            get { return _clearOldLogs; }
            set { _clearOldLogs = value; }
        }
        public String Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public FullMonitorType Type
        {
            get { return FullMonitorType.EventLog; }
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
            set { }
        }
        [XmlIgnore]
        public String Hash
        {
            get
            {
                if(_hash != "")
                    return _hash;
                _hash = Guid.NewGuid().ToString();// XmlExport.ByteArrayToString(XmlExport.Serializer(typeof (EventMonitor), this));
                return _hash;
            }
            set { _hash = value; }
        }

        public IResult Check()
        {
            EventResult er = new EventResult(this);// {MonitorHash = Hash};
            try
            {
                _stopwatch.Start();
                EventLog[] logs = EventLog.GetEventLogs(_server);
                EventLog log = null;
                //List<EventLog> sourcematches = new List<EventLog>();
                foreach (EventLog eventLog in logs)
                {
                    using(eventLog)
                    {
                        if (MatchingEvent(eventLog))
                        {
                            log = eventLog;
                            //sourcematches.Add(eventLog);
                            break;
                        }
                    }
                }
                if (log != null)
                {
                    for (int y = log.Entries.Count - 1; y > -1; y--)
                    {
                        using (EventLogEntry entry = log.Entries[y])
                        {
                            if (entry.TimeGenerated < _startTime)
                                break;

                            if (MatchingEventLogEntry(entry))
                            {
                                EventResultMatches erm = (EventResultMatches) er.Value;
                                erm.Add(new EventResultMatch(entry.Source, entry.Message, entry.TimeGenerated));
                            }
                        }
                    }
                }
                _stopwatch.Stop();
                er.RunLength = _stopwatch.ElapsedMilliseconds;
                return er;
            }
            catch (Exception ex)
            {
                er.Ok = false;
                er.Exception = true;
                er.Value = new SerializableException(ex);
                _stopwatch.Stop();
                er.RunLength = _stopwatch.ElapsedMilliseconds;
                return er;
            }
            finally
            {
                this._startTime = DateTime.Now;
                _stopwatch.Reset();
            }
        }

        private Boolean MatchingEvent(EventLog eventLog)
        {
            return eventLog.Source == _eventLogKind;
        }

        private Boolean MatchingEventLogEntry(EventLogEntry eventLogEntry)
        {
            if(((EventLogEntryType)eventLogEntry.EntryType & _eventType) == _eventType) //NOTE: Entry type
            {
                if (_eventNameMatch != "") //NOTE: Source name
                {
                    String[] sourcematches = _eventNameMatch.Split(',');
                    foreach (String source in sourcematches)
                        if (eventLogEntry.Source.StartsWith(source))
                        {
                            return true;
                        }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public override Int32 GetHashCode()
        {
            String s = _friendlyName + _server + _alertInfo.GetHashCode() + _common +
                       _updateFrequency + _clearOldLogs + _eventLogKind + 
                       _eventType;
            return s.GetHashCode();
        }
    }

    [SerializableAttribute]
    public class EventResults : IResults<EventResult>
    {
        private Dictionary<String, EventResult> _results = new Dictionary<String, EventResult>();


        #region Implementation of IEnumerable

        public IEnumerator<EventResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IResults<WmiResult>

        public Dictionary<String, EventResult> Results
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
            if (result.GetType() == typeof(EventResult))
                if (!_results.ContainsKey(((EventResult)result).MonitorHash)) 
                    _results.Add(((EventResult)result).MonitorHash, (EventResult)result);
                else
                    _results[((EventResult)result).MonitorHash] = (EventResult)result;
        }

        public void AddRange(IEnumerable<Object> results)
        {
            foreach (Object result in results)
                if (result.GetType() == typeof(EventResult))
                    if (!_results.ContainsKey(((EventResult)result).MonitorHash)) 
                        _results.Add(((EventResult)result).MonitorHash, (EventResult)result);
                    else
                        _results[((EventResult)result).MonitorHash] = (EventResult)result;
        }
        #endregion
    }

    [XmlInclude(typeof(EventMonitor))]
    [XmlInclude(typeof(EventResultMatches))]
    [SerializableAttribute]
    public class EventResult : IResult
    {
        private EventMonitor _monitor;
        private Object _value = new EventResultMatches();
        private readonly DateTime _runTime = DateTime.Now;
        private Int64 _lastRunLength = 0;
        private Boolean _exception = false;
        
        public EventResult() { }

        public EventResult(EventMonitor monitor)
        {
            _monitor = monitor;
        }

        public Object Monitor
        {
            get { return _monitor; }
            set
            {
                if (value.GetType() == typeof(EventMonitor))
                    _monitor = (EventMonitor)value;
                else
                    throw new Exception("value must be of type EventMonitor");
            }
        }

        public Boolean Ok
        {
            get { return _value.GetType() == typeof(EventResultMatches) && ((EventResultMatches)Value).Count > 0 ? false : true; }
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
            get { return FullMonitorType.EventLog; }
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

    [XmlInclude(typeof(EventResultMatch))]
    [SerializableAttribute]
    public class EventResultMatches: IEnumerable<EventResultMatch>
    {
        private List<EventResultMatch> _eventResultMatches = new List<EventResultMatch>();

        public EventResultMatches() { }

        public List<EventResultMatch> EventResults
        {
            get { return _eventResultMatches; }
            set { _eventResultMatches = value; }
        }

        public Int32 Count
        {
            get { return _eventResultMatches.Count; }
        }

        public void Add(EventResultMatch eventResultMatch)
        {
            _eventResultMatches.Add(eventResultMatch);
        }

        public IEnumerator<EventResultMatch> GetEnumerator()
        {
            return _eventResultMatches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override String ToString()
        {
            return Count > 0 ? "New events!" : "No new events.";
        }
    }
    
    [SerializableAttribute]
    public class EventResultMatch
    {
        private String _eventText = "";
        private String _source = "";
        private DateTime _timeGenerated;

        public EventResultMatch() { }

        public EventResultMatch(String source, String text, DateTime timeGenerated)
        {
            _source = source;
            _eventText = text;
            _timeGenerated = timeGenerated;
        }
        
        public DateTime TimeGenerated
        {
            get { return _timeGenerated; }
            set { _timeGenerated = value; }
        }

        public String Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public String EventText
        {
            get { return _eventText; }
            set { _eventText = value; }
        }
    }

    /// <summary>
    /// Specifies the event type of an event log entry.
    /// 
    /// </summary>
    /// <filterpriority>2</filterpriority>
    [Flags]
    public enum EventLogEntryType
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Information = 4,
        SuccessAudit = 8,
        FailureAudit = 16
    }
}