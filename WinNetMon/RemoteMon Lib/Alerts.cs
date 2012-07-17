using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Xml.Serialization;

namespace RemoteMon_Lib
{
    [Serializable]
    public class Alerts : IEnumerable<CAlert>
    {
        private readonly List<CAlert> _alerts = new List<CAlert>(2);

        public void RemoveType(AlertType alertType)
        {
            _alerts.RemoveAll(x => x.Type == alertType);
        }

        public Int32 Count
        {
            get { return _alerts.Count; }
        }

        public void AddRange(IEnumerable<CAlert> alerts)
        {
            _alerts.AddRange(alerts);
        }
        public void Add(CAlert alert)
        {
            _alerts.Add(alert);
        }
        public CAlert this[int index]
        {
            get { return _alerts[index]; }
            set { _alerts[index] = value; }
        }

        public Boolean SendAlerts(IResult result)
        {
            return CAlert.SendAlert(result, this);
        }

        public IEnumerator<CAlert> GetEnumerator()
        {
            return _alerts.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [XmlInclude(typeof(EmailAlert))]
    [XmlInclude(typeof(SmsAlert))]
    public abstract class CAlert
    { //NOTE: couldn't do interface - they don't serialize.
        public abstract AlertType Type { get; }
        public abstract String Info { get; set; }
        //public abstract Boolean Send();
        //public new abstract String ToString();
        public override string ToString()
        {
            return "Alert Type: " + this.Type + ", Info: " + Info;
        }


        public static Boolean SendAlert(IResult result)
        {
            Alerts alerts = ((IMonitor) result.Monitor).AlertInfo;
            return GetSend(result, alerts);
        }
        internal static Boolean SendAlert(IResult result, Alerts alerts)
        {
            return GetSend(result, alerts);
        }
        private static Boolean GetSend(IResult result, Alerts alerts)
        {
            Boolean send = true;
            //if (alerts.Count > 0)
            //{
                foreach (CAlert ca in alerts)
                {
                    try
                    {
                        switch (ca.Type)
                        {
                            case AlertType.Email:
                                EmailAlert ea = (EmailAlert)ca;
                                using (MailMessage message = new MailMessage(ea.EmailAddressFrom, ea.Info,
                                                                      "MONITOR ALERT: Type: " + result.Type + ", Named: " +
                                                                      ((IMonitor)result.Monitor).FriendlyName,
                                                                      "The Monitor: " + ((IMonitor)result.Monitor).FriendlyName +
                                                                      "\r\n\r\nDescription: " + ((IMonitor)result.Monitor).ToString() +
                                                                      "\r\n\r\nValue: " + result.Value +
                                                                      "\r\n\r\nResult: " + result.ToString() +
                                                                      "\r\n\r\nRan at: " + result.RunTime))
                                {
                                    message.IsBodyHtml = false;
                                    SmtpClient client = new SmtpClient(ea.EmailServerHostName)
                                                            {
                                                                EnableSsl = ea.UseSsl,
                                                                UseDefaultCredentials = false,
                                                                DeliveryMethod = SmtpDeliveryMethod.Network
                                                            };
                                        
                                    if (ea.EmailUserName == "" && ea.EmailUserPass == "")
                                        client.UseDefaultCredentials = true;
                                    else
                                        client.Credentials = new System.Net.NetworkCredential(ea.EmailUserName, ea.EmailUserPass);

                                    if (ea.EmailServerPort > 0)
                                        client.Port = ea.EmailServerPort;

                                    try
                                    {
                                        client.Send(message);
                                        Logger.Instance.Log(typeof (CAlert), LogType.Info,
                                                            "Alert Email successfully sent for: " + ((IMonitor) result.Monitor).FriendlyName);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.Instance.Log(typeof (CAlert), LogType.Info,
                                                            "Alert Email for: " + ((IMonitor) result.Monitor).FriendlyName + " failed to send.");
                                        Logger.Instance.LogException(typeof (CAlert), ex);
                                        send = false;
                                    }
                                }
                                break;
                            case AlertType.Phone:
                                break;
                            case AlertType.LocalPopup:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log(typeof(CAlert), LogType.Info,
                                            "Alert failed to send -- " + ca.ToString());
                        Logger.Instance.LogException(typeof(CAlert), ex);
                        send = false;
                    }
                }
            //}
            //else
            //    send = false;
            return send;
        }

       
    }


    [SerializableAttribute]
    public class EmailAlert : CAlert
    {
        private String _emailAddressTo = "";
        private String _emailServerHostName = "";
        private Int32 _emailServerPort = 465;
        private String _emailAddressFrom = "";
        private String _emailUserName = "";
        private String _emailUserPass = "";
        private Boolean _useSsl = true;

        public override String Info
        {
            get { return _emailAddressTo; }
            set { _emailAddressTo = value; }
        }
        public String EmailServerHostName
        {
            get { return _emailServerHostName; }
            set { _emailServerHostName = value; }
        }
        public override AlertType Type
        {
            get { return AlertType.Email; }
        }

        public Int32 EmailServerPort
        {
            get { return _emailServerPort; }
            set { _emailServerPort = value; }
        }

        //public String EmailAddressTo
        //{
        //    get { return _emailAddressTo; }
        //    set { _emailAddressTo = value; }
        //}

        public String EmailAddressFrom
        {
            get { return _emailAddressFrom; }
            set { _emailAddressFrom = value; }
        }
        public String EmailUserName
        {
            get { return _emailUserName; }
            set { _emailUserName = value; }
        }

        public String EmailUserPass
        {
            get { return _emailUserPass; }
            set { _emailUserPass = value; }
        }

        public Boolean UseSsl
        {
            get { return _useSsl; }
            set { _useSsl = value; }
        }

        //public override Boolean Send()
        //{
        //    return false;
        //    //send email
        //}
        //public override string ToString()
        //{
        //    return "Alert Type: " + this.Type + ", Info: " + _info;
        //}
    }
    [SerializableAttribute]
    public class SmsAlert : CAlert
    {
        private String _smsNumber = "";
        private String _smsServer = "";
        private String _userName = "";
        private String _password = "";

        public override String Info
        {
            get { return _smsNumber; }
            set { _smsNumber = value; }
        }
        public String SmsServer
        {
            get { return _smsServer; }
            set { _smsServer = value; }
        }
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public override AlertType Type
        {
            get { return AlertType.Phone; }
        }

        //public override Boolean Send()
        //{
        //    return false;
        //    //send sms
        //}
    //    public override string ToString()
    //    {
    //        return "Alert Type: " + this.Type + ", Info: " + _info;
    //    }
    }

    public enum AlertType
    {
        Email,
        Phone,
        LocalPopup
    }
}