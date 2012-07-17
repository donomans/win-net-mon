using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    /// <summary>
    /// Takes configuration data and exports it to XML file
    /// </summary>
    public class XmlExport
    {
        private String _fileName = "";
        private Object _data;

        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        //private ConfigurationData ConfigurationData
        //{
        //    get { return _data; }
        //    set { _data = value; }
        //}

        private XmlExport() {}

        public XmlExport(string fileName, ConfigurationData data)
        {
            _fileName = fileName;
            _data = data;
        }
        public XmlExport(string fileName, NotifySettings data)
        {
            _fileName = fileName;
            _data = data;
        }

        /// <summary>
        /// Export the set configuration data
        /// </summary>
        /// <remarks>ConfigurationData must be set before</remarks>
        /// <returns></returns>
        public Boolean ExportConfigurationData()
        {
            Stream str = null;
            try
            {
                if (_data != null && _fileName != "")
                {
                    ConfigurationData data = (ConfigurationData) _data;
                    data.TimeStamp = DateTime.Now;
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationData));

                    str = File.Create(_fileName + "tmp");

                    serializer.Serialize(str, data);
                    str.Dispose();
                    FileInfo fi = new FileInfo(_fileName + "tmp");
                    fi.CopyTo(_fileName, true);
                    //NOTE: need to make sure file isn't ever deleted if this fails, for some reason.
                    fi.Delete();

                    return true;
                }
                else
                    return false;
            }
            finally
            {
                if (str != null)
                {
                    str.Dispose();
                }
            }
        }
        public Boolean ExportNotifySettings()
        {
            Stream str = null;
            try
            {
                if (_data != null && _fileName != "")
                {
                    NotifySettings data = (NotifySettings)_data;

                    XmlSerializer serializer = new XmlSerializer(typeof(NotifySettings));

                    str = File.Create(_fileName + "tmp");

                    serializer.Serialize(str, data);
                    str.Dispose();
                    FileInfo fi = new FileInfo(_fileName + "tmp");
                    fi.CopyTo(_fileName, true);
                    //NOTE: need to make sure file isn't ever deleted if this fails, for some reason.
                    fi.Delete();

                    return true;
                }
                else
                    return false;
            }
            finally
            {
                if (str != null)
                {
                    str.Dispose();
                }
            }
        }

        public static Byte[] Serializer(Type type, Object obj)
        {
            //MemoryStream str = null;
            MD5 md5 = null;
            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(type);
                fs = File.Create(Environment.CurrentDirectory + @"\tmp.ngs");//Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon\tmp.ngs");
                //str = new MemoryStream(); //NOTE: why doesn't memory stream work properly?
                
                xs.Serialize(fs, obj);

                Byte[] bytes = new Byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                List<Byte> byteList = new List<Byte>(bytes);

                Byte[] datetimebytes = StringToByteArray(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"));
                byteList.AddRange(datetimebytes);

                md5 = MD5.Create();
                Byte[] hashbytes = md5.ComputeHash(byteList.ToArray());//str);
   
                return hashbytes;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(XmlExport), ex);
                return null;
            }
            finally
            {
                //if (str != null)
                //    str.Dispose();
                if (fs != null)
                    fs.Dispose();
                if(md5 != null)
                    md5.Clear();
            }
        }
        public static String ByteArrayToString(Byte[] inBytes)
        {
            if (inBytes == null)
                return "";
            try
            {
                int i;
                StringBuilder sOutput = new StringBuilder(inBytes.Length);
                for (i = 0; i < inBytes.Length; i++)
                {
                    sOutput.Append(inBytes[i].ToString("X2"));
                }
                return sOutput.ToString();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(XmlExport), ex);
                return "";
            }
        }
        public static Byte[] StringToByteArray(String inString)
        {
            List<Byte> bytes = new List<Byte>(inString.Length);
            foreach(Char c in inString)
            {
                bytes.Add(Convert.ToByte(c));
            }
            return bytes.ToArray();
        }
    }
}
