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
        public static Boolean Export(String fileName, Object data)
        {
            Stream str = null;
            try
            {
                if (data != null && fileName != "")
                {                   
                    XmlSerializer serializer = new XmlSerializer(data.GetType());

                    str = File.Create(fileName + "tmp");

                    serializer.Serialize(str, data);
                    str.Dispose();
                    FileInfo fi = new FileInfo(fileName + "tmp");
                    fi.CopyTo(fileName, true);
                    ///NOTE: need to make sure file isn't ever deleted if this fails, for some reason.
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
        public static Boolean Export(String fileName, ConfigurationData data)
        {
            data.TimeStamp = DateTime.Now;
            return Export(fileName, data);
        }

  
        public static Byte[] Serializer(Object obj)
        {
            MD5 md5 = null;
            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                fs = File.Create(Environment.CurrentDirectory + @"\tmp.ngs");
                
                xs.Serialize(fs, obj);

                Byte[] bytes = new Byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                List<Byte> byteList = new List<Byte>(bytes);

                Byte[] datetimebytes = StringToByteArray(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"));
                byteList.AddRange(datetimebytes);

                md5 = MD5.Create();
                Byte[] hashbytes = md5.ComputeHash(byteList.ToArray());
   
                return hashbytes;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(XmlExport), ex);
                return null;
            }
            finally
            {
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
