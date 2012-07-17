using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace RemoteMon_Lib
{
    public sealed class Logger
    {
        private static volatile Logger instance = new Logger();
        private String _fileName;
        private Boolean _keepLogging = true;
        private DateTime _lastTimePushed = DateTime.MinValue;
        private static readonly Object log_lock = new Object();
        private Boolean _verbose = true;

        //private Dictionary<String, Log> _toLogWorking = new Dictionary<String, Log>();
        private Log _logWorking = new Log();
        //private Dictionary<String, Log> _toLog = new Dictionary<String, Log>();
        private Log _log = new Log();
        //need to take into account multiple sources, based on namespace  (can get from Type? where to log if from RemoteMon_Lib namespace?)
        //-- need to figure out how to know namespace, and then store that - use _toLog as a Dictionary<String, List<String>> _toLog
        //possibly add a timer to this class to log in background after 30 seconds or _toLog's list is > X # of Strings??
        
        //could the main server updates be added to Logger class when "Monitor" LogType is used?
        
        public static Logger Instance
        {
            get { return instance; }
        }

        public void Log(Type type, LogType logType, String text)
        {
//#if !DEBUG
            //if(logType == LogType.Debug)
            //    return;
//#endif
            if (!_verbose)
                return;

            if (type == null)
                type = this.GetType();

            String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            String methodName = "";
            if (logType == LogType.Debug)
            {
                try
                {
                    methodName = new StackFrame(1).GetMethod().Name;
                }
                catch (Exception) { }
            }
            //String nameSpace = "RemoteMon_Lib";
            //if(type.Namespace != null)
            //    nameSpace = type.Namespace;
            lock (log_lock)
            {
                //if (!_toLogWorking.ContainsKey(nameSpace))
                //    _toLogWorking.Add(nameSpace, new Log());

                //_toLogWorking[nameSpace]
                _logWorking.Add(dateTime + "\t" + logType + "\t" + type.ToString() + " " +
                                (methodName != "" ? (methodName + ": ") : "") + text + "\t");

            }
            PushLog();
        }
        public void LogException(Type type, Exception exception)
        {
            if (!_verbose)
                return;

            if (type == null)
                type = this.GetType();

            String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            String methodName = "";
            try
            {
                methodName = new StackFrame(1).GetMethod().Name;
            }
            catch(Exception) { }
            
            String innerExceptions = "";
            Exception innerException = exception.InnerException;
            while(innerException != null)
            {
                innerExceptions += " --> Inner exception: " + innerException.Message;
                innerException = innerException.InnerException;
            }
            //String nameSpace = "RemoteMon_Lib";
            //if (type.Namespace != null)
            //    nameSpace = type.Namespace;
            lock (log_lock)
            {
                //if (!_toLogWorking.ContainsKey(nameSpace))
                //    _toLogWorking.Add(nameSpace, new Log());
                //_toLogWorking[nameSpace]
                _logWorking.Add(dateTime + "\t" + LogType.Exception + "\t" +
                                        type.ToString() + " " + methodName +
                                        " threw exception: " + exception.Message.Replace("\r\n", "") +
                                        innerExceptions.Replace("\r\n", "") + "\t");
            }
            PushLog();
        }
        private void LogMonitorException(IResult result)//String nameSpace, IResult result)
        {
            if (!_verbose)
                return;

            String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            SerializableException exception = (SerializableException) result.Value;

            String innerExceptions = "";
            SerializableException innerException = exception.InnerException;
            while (innerException != null)
            {
                innerExceptions += " --> Inner exception: " + innerException.Message;
                innerException = innerException.InnerException;
            }
            
            lock (log_lock)
            {
                //if (!_toLogWorking.ContainsKey(nameSpace))
                //    _toLogWorking.Add(nameSpace, new Log());
                //_toLogWorking[nameSpace]
                _logWorking.Add(dateTime + "\t" + LogType.MonitorException + "\t" +
                                        ((IMonitor)result.Monitor).FriendlyName +
                                        " threw exception: " + exception.Message.Replace("\r\n", "") +
                                        innerExceptions.Replace("\r\n", "") + "\t");
            }
            PushLog();
        }
        public void LogMonitor(Type type, IResult result)
        {
            if (type == null)
                type = this.GetType();

            String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            //String nameSpace = "RemoteMon_Lib";
            //if (type.Namespace != null)
            //    nameSpace = type.Namespace;
            lock (log_lock)
            {
                //if (!_toLogWorking.ContainsKey(nameSpace))
                //{
                //    _toLogWorking.Add(nameSpace, new Log());
                //}
                
                if(result.Exception)
                {
                    //_toLogWorking[nameSpace]
                    _logWorking.Add(dateTime + "\t" + LogType.MonitorException + "\t" + "Name: " +
                                                 ((IMonitor)result.Monitor).FriendlyName + ", " +
                                                 ((IMonitor)result.Monitor).Type + " Ok: " + result.Ok + " Exception: " + result.Exception +
                                                 ", Last run length: " + result.RunLength + "\t");
                    LogMonitorException(result);//nameSpace, result);
                }
                else
                {
                    //_toLogWorking[nameSpace]
                    _logWorking.Add(dateTime + "\t" + LogType.Monitor + "\t" + "Name: " +
                                                 ((IMonitor)result.Monitor).FriendlyName + ", " + result.ToString() +
                                                 ", Last run length: " + result.RunLength + "\t" + result.Value);
                    PushLog();
                }
            }
        }

        static Logger()
        {
        }

        private Logger()
        {
            _keepLogging = true;
            if (!Environment.CurrentDirectory.Contains("system32"))
                _fileName = Environment.CurrentDirectory + @"\log.log";
            else 
                _fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RemoteMon\log.log";
        }

        private void PushLog(Boolean lazy = true)
        {
            if (lazy && (DateTime.Now - _lastTimePushed).TotalSeconds < 10) //only push every 10 seconds max
                return;

            if (_keepLogging)
            {
                //try
                //{
                lock (log_lock)
                {
                    //_toLog = null;
                    //_toLog = _toLogWorking;
                    //_toLogWorking = new Dictionary<String, Log>();
                    _log = null;
                    _log = _logWorking;
                    _logWorking = new Log();

                    //foreach (Log toLog in _toLog.Values)
                    //{
                    if (_log.FileName == "")
                    {
                        _log.FileName = _fileName; //default filename
                    }
                    FileInfo fi = new FileInfo(_log.FileName);
                    if(!fi.Exists)
                    {
                        if (!fi.Directory.Exists)
                            fi.Directory.Create();
                        fi.Create().Close();
                    }
                    else
                    {
                        if (fi.Length > 52428800) //50MB
                        {
                            try
                            {
                                const Int32 lohsize = 10000;
                                    
                                using (FileStream originalfs = fi.OpenRead())//new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                                {
                                    String datetime = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH' 'mm");

                                    using (FileStream newfs = new FileStream(fi.Directory.FullName +
                                                                                    @"\" + fi.CreationTime.ToString("yyyy'-'MM'-'dd' 'HH' 'mm") +
                                                                                    " to " + datetime + ".zip", FileMode.OpenOrCreate))
                                    {
                                        Int64 count = originalfs.Length;

                                        using (GZipStream gzip = new GZipStream(newfs, CompressionMode.Compress))
                                        {
                                            Int32 leftover = (Int32)count % lohsize;
                                            Int32 buckets = (Int32)count / lohsize;

                                            if (leftover > 0)
                                                buckets++;

                                            for (Int32 x = 0; x < buckets; x++)
                                            {
                                                Int32 size;
                                                if (count >= lohsize)
                                                    size = lohsize;
                                                else
                                                    size = (Int32) count;
                                                Byte[] buffer = new Byte[size];
                                                //Bucket buffer = new Bucket(originalfs.Length);
                                                originalfs.Read(buffer, 0, size);

                                                gzip.Write(buffer, 0, size);

                                                count -= size;
                                            }
                                        }
                                        if (count != 0)
                                        {
                                            Logger.Instance.Log(this.GetType(), LogType.Wtf, "Mismatch of buffer sizes - zipped log file likely corrupt: " +
                                                                                             fi.Directory.FullName + @"\" +
                                                                                             fi.CreationTime.ToString("yyyy'-'MM'-'dd' 'HH' 'mm") + " to " +
                                                                                             datetime + ".zip");
                                        }
                                    }
                                }
                                fi.Delete();
                            }
                            catch(Exception ex)
                            {
                                Logger.Instance.LogException(this.GetType(), ex);
                            }
                        }
                           
                    }

                    using (StreamWriter sw = new StreamWriter(fi.OpenWrite()))//_log.FileName, true))
                    {
                        foreach (String s in _log)
                        {
                            sw.WriteLine(s);
                        }
                        _lastTimePushed = DateTime.Now;
                    }
                    _log.Clear();
                }
                //}
                //catch (Exception ex)
                //{
                //    //LogImmediate("log_danger.log", ex.Message);
                //    throw;
                //}
            }
        }
        private void LogImmediate(String fileName, String message)
        {
            _keepLogging = false;

            //SmsParser.Instance.SendMessage(
        }
        public void LogImmediate()
        {
            PushLog(false);
        }
        public void ResetLogging()
        {
            _keepLogging = true;
            PushLog();
        }
        public void SetFileName(String fileName)//Type type, String fileName)
        {
            //if (type == null)
            //    type = this.GetType();

            //String nameSpace = "RemoteMon_Lib";
            //if (type.Namespace != null)
            //    nameSpace = type.Namespace;
            //if (_toLog.ContainsKey(nameSpace))
            //    _toLog[nameSpace].FileName = fileName;
            //else
            //    _toLog.Add(nameSpace, new Log(fileName));
            _fileName = fileName;
            _log = new Log(fileName);
        }
        public Boolean Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }
    }
    public class Log: IEnumerable<String>
    {
        public String FileName = "";
        private readonly List<String> _log = new List<String>();

        public Log(String fileName, String toLog)
        {
            FileName = fileName;
            _log.Add(toLog);
        }
        public Log(String fileName)
        {
            FileName = fileName;
        }
        public Log()
        {
            
        }

        public void Clear()
        {
            _log.Clear();
        }

        public void Add(String s)
        {
            _log.Add(s);
        }

        #region Implementation of IEnumerable
        public IEnumerator<String> GetEnumerator()
        {
            return _log.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
    public enum LogType
    {
        Info,
        Debug,
        Exception,
        Command,
        Monitor,
        MonitorException,
        Wtf,
        Sms
    }

    internal class Bucket
    {
        private const Int32 LohSize = 10000;

        private Bytes _bytes;

        public Bucket(Int64 size)
        {
            Int32 leftover = (Int32)size%LohSize;
            Int32 buckets = (Int32)size/LohSize;
 
            if (leftover > 0)
                buckets++;

            _bytes = new Bytes(buckets, size);
        }

        public Byte this[Int64 index]
        {
            get { return _bytes[index]; }
            set { _bytes[index] = value; }
        }


        private class Bytes
        {
            private Byte[][] _bytes;
            private readonly Int32 _buckets;
            private readonly Int64 _size;

            public Bytes(Int32 buckets, Int64 totalSize)
            {
                _size = totalSize;
                _buckets = buckets;
                _bytes = new Byte[_buckets][];
            }

            public void Add(Int32 bucket, Byte[] bytes)
            {
                _bytes[bucket] = bytes;
            }

            public Byte this[Int64 index]
            {
                get
                {
                    Int32 bucket = (Int32)index / Bucket.LohSize;
                    Int32 bucketindex = (Int32)index % Bucket.LohSize;
                    if (bucket >= _bytes.Length)
                        throw new IndexOutOfRangeException("Out of range for bucket: Length is " + _bytes.Length + " and index passed was " + bucket);
                    if (_bytes[bucket] == null)
                        _bytes[bucket] = new Byte[Bucket.LohSize];
                    if (bucketindex >= _bytes[bucket].Length)
                        throw new IndexOutOfRangeException("Out of range for index: Length is " + _bytes[bucket].Length + " and index passed was " + bucketindex);

                    return _bytes[bucket][bucketindex];
                }
                set
                {
                    Int32 bucket = (Int32)index / Bucket.LohSize;
                    Int32 bucketindex = (Int32)index % Bucket.LohSize;
                    if (bucket >= _bytes.Length)
                        throw new IndexOutOfRangeException("Out of range for bucket: Length is " + _bytes.Length + " and index passed was " + bucket);
                    if (_bytes[bucket] == null)
                        _bytes[bucket] = new Byte[Bucket.LohSize];
                    if (bucketindex >= _bytes[bucket].Length)
                        throw new IndexOutOfRangeException("Out of range for index: Length is " + _bytes[bucket].Length + " and index passed was " + bucketindex);

                    _bytes[bucket][bucketindex] = value;
                }
            }
        }
    }
}
