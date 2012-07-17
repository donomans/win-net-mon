using System;
using System.Collections.Generic;

namespace RemoteMon_Lib
{
    [Serializable]
    public class MonitorSettings
    {
        private String _remoteServerAddress = "";
        private String _remoteServiceAddress = "";
        private String _clientLogPath = "";
        private String _serviceLogPath = "";
        private String _remoteServicePort = "";

        private Alerts _defaultAlerts = new Alerts();

        public Boolean IsDefaultAlert(CAlert alert)
        {
            foreach (CAlert c in _defaultAlerts)
            {
                if (c.Type == alert.Type && c.Info.ToLower() == alert.Info.ToLower())
                    return true;
            }
            return false;
        }

        public void AddRangeNoDupes(IEnumerable<CAlert> alerts)
        {
            foreach (CAlert ca in alerts)
            {
                Boolean toAdd = true;
                foreach (CAlert c in _defaultAlerts)
                    if (c.Info == ca.Info)
                        toAdd = false;

                if(toAdd)
                    _defaultAlerts.Add(ca);
            }
        }

        public Alerts DefaultAlerts
        {
            get { return _defaultAlerts; }
            set { _defaultAlerts = value; }
        }
        public String RemoteServiceAddress
        {
            get { return _remoteServiceAddress; }
            set { _remoteServiceAddress = value; }
        }
        public String RemoteServicePort
        {
            get { return _remoteServicePort; }
            set { _remoteServicePort = value; }
        }
        public String RemoteServerAddress
        {
            get { return _remoteServerAddress; }
            set { _remoteServerAddress = value; }
        }
        public String ClientLogPath
        {
            get { return _clientLogPath; }
            set { _clientLogPath = value; }
        }
        public String ServiceLogPath
        {
            get { return _serviceLogPath; }
            set { _serviceLogPath = value; }
        }
    }
}