using System;

namespace RemoteMon_Lib
{
    [Serializable]
    public class NotifySettings
    {
        private String _serviceAddress = "";
        private Int32 _servicePort = 5502;

        public Int32 ServicePort
        {
            get { return _servicePort; }
            set { _servicePort = value; }
        }
        public String ServiceAddress
        {
            get { return _serviceAddress; }
            set { _serviceAddress = value; }
        }

        public static NotifySettings LoadSettings(String fileName)
        {
            XmlImport xml = new XmlImport(fileName);
            Object o = xml.Import(typeof (NotifySettings));
            return o == null ? null : (NotifySettings)o;
        }

        public Boolean ExportToXml(String fileName)
        {
            XmlExport xml = new XmlExport(fileName, this);
            return xml.ExportNotifySettings();
        }
    }
}