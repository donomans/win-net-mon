using System;
using System.Net;

namespace RemoteMon_Lib
{

    public sealed class SmsParser
    {
        private static readonly SmsParser instance = new SmsParser();

        private String _fullxml;
        private String _phone;
        private String _message;
        private String _fullmessage;
        /// <summary>
        /// read only Phone Number
        /// </summary>
        public String PhoneNumber { get { return _phone; } }
        /// <summary>
        /// read write message
        /// </summary>
        public String Message { get { return _message; } set { _message = value; } }
        /// <summary>
        /// read  FullMessage
        /// </summary>
        public String FullMessage { get { return _fullmessage; } }

        public static SmsParser Instance
        {
            get { return instance; }
        }
        private SmsParser()
        {
            _fullxml = "";
            _message = "";
            _fullmessage = "";
            _phone = "";
        }
        static SmsParser()
        {

        }
        public void LoadSms(String xml)
        {
            _fullxml = FormatSmsXml(xml);
            _message = "";
            _fullmessage = "";
            _phone = "";
            ParseNodes();
            LogReceive();
        }
        private static String FormatSmsXml(String xmlToFormat)
        {
            return xmlToFormat.Replace("XMLDATA=", "").Replace("%20", " ").Replace("%3C", "<").Replace("%3F", "?").Replace("%3D", "=").Replace("%22", "\"").Replace("%3E", ">").Replace("%0D%0A", Environment.NewLine).Replace("%2B", "+").Replace("%2F", "/").Replace("%3A", ":");
        }
        public void LoadXml(String xml)
        {
            _fullxml = FormatSmsXml(xml);
            ParseNodes();
        }
        private void LogReceive()
        {
            Logger.Instance.Log(this.GetType(), LogType.Sms, "Time Received: " + DateTime.Now + ", Phone Number: " + _phone + ", Message: " + _message);
        }
        private void LogSend(String phone, String message)
        {
            Logger.Instance.Log(this.GetType(), LogType.Sms, "Time Sent: " + DateTime.Now + ", Phone Number: " + phone + ", Message: " + message + ", Original message: " + _fullmessage);            
        }
        private void ParseNodes()
        {
            int sendernumberindex = _fullxml.IndexOf("<SenderNumber>", 0);
            if (sendernumberindex != -1)
            {
                _phone = _fullxml.Substring(sendernumberindex, _fullxml.IndexOf("</SenderNumber>", sendernumberindex) - sendernumberindex).Replace("<SenderNumber>", "");
                int messageindex = _fullxml.IndexOf("<message>", 0);
                if (messageindex != -1)
                {
                    _fullmessage = _fullxml.Substring(messageindex, _fullxml.IndexOf("</message>", messageindex) - messageindex).Replace("<message>", "");
                    _message = _fullmessage.Trim();
                }
            }
        }
        public void SendMessage(String phone, String message)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(@"http://192.168.51.248:81/sendmsg?user=admin&passwd=adminmts&cat=1&to=""" + phone + @"""&text=" + message);
                response = (HttpWebResponse)request.GetResponse();
                response.Close();
                LogSend(phone, message);
                //return newmessage;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(GetType(), ex);
            }
        }
    }

    /*
    public sealed class SmsListener
    {   
        private static readonly SmsListener instance = new SmsListener();
        private TcpListener tcp;
        private List<String> commands;
        //private int port;

        public static SmsListener Instance
        {
            get { return instance; }
        }
        
        public void Start(int Port)
        {
            if (Port > 0)
                tcp = new TcpListener(IPAddress.Any, Port);
            else
                return;
            tcp.Start();
            
        }
        public List<String> PollCommands()
        {
            if (commands.Count > 0)
            {
                List<String> temp = commands;
                commands.Clear();
                return temp;
            }
            else
                return null;
        }
        private void PrepareCommand(String Sms)
        {
            commands.Add(ParseSms(Sms));
        }
        private String ParseSms(String Sms)
        {
            return Sms;
        }

        static SmsListener()
        {
        }
        private SmsListener()
        {
            //sms = "";
            //port = 0;
        }        
    }*/
}
