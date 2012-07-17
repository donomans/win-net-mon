using System;
using System.IO;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    /// <summary>
    /// Takes XML file and imports it to configuration data
    /// </summary>
    class XmlImport
    {
        private string _fileName;
        
        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public XmlImport(string fileName)
        {
            _fileName = fileName;
        }

        public Object Import(Type importType)
        {
            StreamReader sr = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(importType);
                sr = new StreamReader(_fileName);
                return xml.Deserialize(sr); 
            }
            finally
            {
                if(sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
    }
}
