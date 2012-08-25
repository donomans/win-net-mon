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
        public static Object Import(String fileName, Type importType)
        {
            StreamReader sr = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(importType);
                sr = new StreamReader(fileName);
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
