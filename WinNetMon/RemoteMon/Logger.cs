using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace RemoteMon
{
    public sealed class Logger
    {
        private static readonly Logger instance = new Logger();
        private String _fileName = "";
        private Boolean _keepLogging = true;
        private readonly List<String> _toLog = new List<String>();//List<String> _toLog = new List<String>();

        public static Logger Instance
        {
            get { return instance; }
        }

        public void Log(LogType type, String text)
        {
            String methodName = "";
            if (type == LogType.Debug)
            {
                try
                {
                    methodName = new StackFrame(1).GetMethod().Name;
                }
                catch (Exception) { }
            }
            _toLog.Add(DateTime.Now + "\t" + type + "\t" + (methodName != "" ? (methodName + ": ") : "") + text);
            PushLog();
        }
        public void LogException(Type type, Exception exception)
        {
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
            _toLog.Add(DateTime.Now + "\t" + LogType.Exception + "\t" +
                       type.ToString() + " " + methodName +
                       " threw exception: " + exception.Message.Replace("\r\n", "") +
                       innerExceptions);
            PushLog();
        }
        static Logger()
        {
        }
        private Logger()
        {
            _keepLogging = true;
        }

        private void PushLog()
        {
            if (_fileName == "")
                return;
            if (_keepLogging)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(_fileName, true))
                    {
                        foreach (String s in _toLog)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    _toLog.Clear();
                }
                catch (Exception ex)
                {
                    //LogImmediate("log_danger.log", ex.Message);
                    throw ex;
                }
            }
        }
        private void LogImmediate(String fileName, String message)
        {
            _keepLogging = false;

            //SmsParser.Instance.SendMessage(
        }
        public void ResetLogging()
        {
            _keepLogging = true;
            PushLog();
        }
        public void SetFileName(String fileName)
        {
            _fileName = fileName;
        }
    }
    public enum LogType
    {
        Info,
        Debug,
        Exception,
        Monitor,
        Sms
    }
}
