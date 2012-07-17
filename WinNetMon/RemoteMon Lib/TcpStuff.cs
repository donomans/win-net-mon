using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Messaging;

namespace RemoteMon_Lib
{
    public sealed class Listener: IDisposable //service uses this
    {
        private readonly TcpListener _listener;
        private readonly Thread _acceptConnections;
        private readonly Dictionary<String, ITalker> _connections = new Dictionary<String, ITalker>(); //ip, connection
        private static readonly Object connections_locker = new Object();
        private readonly Int32 _port;
        internal const String QueueName = @".\Private$\RemoteMon";

        public TcpListener Listen
        {
            get { return _listener; }
        }

        public Listener(Int32 listenPort = 5502)
        {
            if (!MessageQueue.Exists(Listener.QueueName))
            {
                MessageQueue messageQueue = MessageQueue.Create(Listener.QueueName, false);
                messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                messageQueue.SetPermissions("SYSTEM", MessageQueueAccessRights.FullControl);
                WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (wi != null)
                    messageQueue.SetPermissions(wi.Name, MessageQueueAccessRights.FullControl);
                messageQueue.Dispose();
            }
            else
            {
                MessageQueue messageQueue = new MessageQueue(Listener.QueueName);
                messageQueue.Purge(); //get rid of all the old commands that linger since last time the service was running
                messageQueue.Dispose();
            }
           

            _port = listenPort;
            _listener = new TcpListener(IPAddress.Any, listenPort);
            _listener.Start();

            //MessageQueueTalker localtalker = new MessageQueueTalker(Dns.GetHostName());
            _connections.Add(Dns.GetHostName(), new MessageQueueTalker(Namespace.Service)); //this is only used in the service
            //foreach (IPAddress ipaddy in Dns.GetHostAddresses(Dns.GetHostName()))
            //{
            //    //add all local addresses 
            //    _connections.Add(ipaddy.ToString(), localtalker);
            //}

            _acceptConnections = new Thread(GetConnections);
            _acceptConnections.Start();
        }

        public Int32 Port { get { return _port; } }

        public void SendCommandToAll(Command cmd)
        {
            foreach(ITalker talker in _connections.Values)
            {
                if (talker.Connected)
                {
                    cmd.ToNamespace = Namespace.Client | Namespace.Notifier;
                    talker.SendCommand(cmd);
                }
            }
        }

        private void GetConnections()
        {
            while (true)
            {
                TcpClient client = null;
                try
                {
                    client = _listener.AcceptTcpClient();
                    client.ReceiveTimeout = 100000;
                    client.SendTimeout = 100000;
                }
                catch(Exception) { /*eat the quit exception*/ }
                
                if (client != null)
                {
                    String ip = ((IPEndPoint) client.Client.RemoteEndPoint).Address.ToString();

                    if (_connections.ContainsKey(ip))
                        lock (connections_locker)
                            _connections[ip].Dispose();

                    lock (connections_locker)
                        _connections[ip] = new TcpTalker(client);
                }
                Thread.Sleep(1000);
            }
        }

        public IEnumerable<Command> GetCommands()//Namespace nameSpace)
        {
            List<Command> commands = new List<Command>(_connections.Count); //most commands possible
            List<String> badIp = new List<String>(_connections.Count);
            lock (connections_locker)
            {
                foreach (ITalker conn in _connections.Values)
                {
                    if (!conn.Connected)
                    {
                        //house keeping
                        badIp.Add(conn.Ip);
                        continue;
                    }
                    Command tmp = conn.GetCommand();//nameSpace);
                    if (tmp != null)
                        if (tmp.Good)
                            commands.Add(tmp);
                }
            }

            //house keeping
            if (badIp.Count > 0)
            {
                lock (connections_locker)
                {
                    foreach (String s in badIp)
                    {
                        if (_connections.ContainsKey(s))
                        {
                            _connections[s].Dispose();
                            _connections.Remove(s);
                        }
                    }
                }
            }
            return commands;
        }

        public Boolean SendCommand(Command command)//, Namespace toNamespace)
        {
            if (!_connections.ContainsKey(command.ToIp))
                return false;// _connections[ip] = new TcpTalker(ip);
            return _connections[command.ToIp].SendCommand(command);//, toNamespace);
        }

        public static Boolean IsLocal(String ipHostName)
        {
            try
            {
                IPHostEntry localhostentry = Dns.GetHostEntry(Dns.GetHostName());
                IPHostEntry providedhostentry = Dns.GetHostEntry(ipHostName);
                if (localhostentry.HostName == providedhostentry.HostName)
                    return true;

                foreach (IPAddress providedip in providedhostentry.AddressList)
                {
                    foreach (IPAddress localip in localhostentry.AddressList)
                    {
                        if (localip.ToString() == providedip.ToString())
                            return true;
                    }
                }
                foreach (String providedalias in providedhostentry.Aliases)
                {
                    foreach (String localalias in providedhostentry.Aliases)
                    {
                        if (localalias == providedalias)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(Listener), ex);
                return false;
            }
            return false;
        }

        public void Dispose()
        {
            _listener.Stop();

            foreach (ITalker conn in _connections.Values)
                conn.Dispose();

            if (_acceptConnections != null && _acceptConnections.IsAlive)
                _acceptConnections.Join(100);
        }
    }

    public class TcpTalker: ITalker
    {
        private readonly TcpClient _client;
        private NetworkStream _stream;
        private readonly String _ip;
        private Boolean _canUse = false;
        private BinaryReader _binaryReader = null;
        private BinaryWriter _binaryWriter = null;

        private readonly Namespace _namespace;

        public TcpTalker(String connectHostName, Int32 connectPort = 5502, Namespace thisNamespace= Namespace.Client)
        {
            _namespace = thisNamespace;
            _client = new TcpClient { ExclusiveAddressUse = false, ReceiveTimeout = 100000, SendTimeout = 100000, LingerState = new LingerOption(true, 100) };
            _client.Connect(connectHostName, connectPort);
            _ip = ((IPEndPoint)_client.Client.RemoteEndPoint).Address.ToString();
            _stream = _client.GetStream();
            _canUse = true;
        }
        internal TcpTalker(TcpClient client, Namespace thisNamespace = Namespace.Service)
        {
            _namespace = thisNamespace;
            _ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            _client = client;
            _client.LingerState = new LingerOption(true, 100);
            _stream = _client.GetStream();
            _canUse = true;
        }

        public Boolean CanUse
        {
            get { return _canUse; }
        }
        public String Ip //the IP i'm connected to
        {
            get { return _ip; }
        }
        public Boolean Connected
        {
            get { return _client != null ? _client.Connected : false; }
        }

        public Boolean SendCommand(Command command)//, Namespace toNamespace)
        {
            try
            {
                if (_client.Connected)
                {
                    if (String.IsNullOrEmpty(command.ToIp))
                        command.ToIp = _ip;
                    //command.ToNamespace = toNamespace;
                    command.FromNamespace = _namespace;

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Command));
                    if (_stream == null)
                        _stream = _client.GetStream();

                    if (_binaryWriter == null)
                        _binaryWriter = new BinaryWriter(_stream);

                    using (StringWriter sw = new StringWriter(new StringBuilder()))
                    {
                        xmlSerializer.Serialize(sw, command);
                        _binaryWriter.Write(sw.ToString());
                    }
                    Logger.Instance.Log(this.GetType(), LogType.Command, command.CommandType + " Command sent to " + _ip);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                return false;
            }
        }
        public Command GetCommand()//Namespace fromNamespace)
        {
            try
            {
                if (_client.Connected)
                {
                    if (_stream == null)
                        _stream = _client.GetStream();

                    if (_stream.CanRead && _stream.DataAvailable)
                    {
                        if (_binaryReader == null)
                            _binaryReader = new BinaryReader(_stream);
                        using (StringReader sr = new StringReader(_binaryReader.ReadString()))
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof (Command));
                            Command ret = (Command) xmlSerializer.Deserialize(sr);
                            ret.FromIp = _ip; //this will be used to know where to send the response to later on.
                            //ret.FromNamespace = fromNamespace;
                            Logger.Instance.Log(this.GetType(), LogType.Command, ret.CommandType + " Command received from " + _ip);
                            return ret;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                return null;
            }
        }
        
        public void Dispose()
        {
            if (_client != null && _client.Connected)
                _client.Close();
            if (_stream != null)
                _stream.Dispose();
            if (_binaryWriter != null)
                _binaryWriter.Close();
            if (_binaryReader != null)
                _binaryReader.Close();
        }
    }

    public class MessageQueueTalker: ITalker
    {
        private readonly MessageQueue _messageQueue = null;
        private readonly String _ip;

        private readonly Namespace _namespace;

        public MessageQueueTalker(Namespace thisNamespace)
        {
            _namespace = thisNamespace;
            _ip = Dns.GetHostName();
            if (!MessageQueue.Exists(Listener.QueueName))
            {
                _messageQueue = MessageQueue.Create(Listener.QueueName, false);
                _messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                _messageQueue.SetPermissions("SYSTEM", MessageQueueAccessRights.FullControl);
                WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (wi != null)
                    _messageQueue.SetPermissions(wi.Name, MessageQueueAccessRights.FullControl);

            }
            else
                _messageQueue = new MessageQueue(Listener.QueueName, false, true, QueueAccessMode.SendAndReceive);

            _messageQueue.Formatter = new CommandMessageFormatter();
            //((XmlMessageFormatter)_messageQueue.Formatter).TargetTypes = new[] { typeof(String) };
        }

        public Boolean CanUse
        {
            get { return Connected; }
        }
        public String Ip //the IP i'm connected to
        {
            get { return _ip; }
        }
        public Boolean Connected
        {
            get { return MessageQueue.Exists(Listener.QueueName); }
        }

        public Boolean SendCommand(Command command)
        {
            try
            {
                if (String.IsNullOrEmpty(command.ToIp))
                    command.ToIp = _ip;
                command.FromNamespace = _namespace;

                using (MessageEnumerator messageEnum = _messageQueue.GetMessageEnumerator2())
                {
                    //check for repeat messages before sending
                    while (messageEnum.MoveNext())
                    {
                        using (Message message = messageEnum.Current)
                        {
                            if (message != null)
                            {
                                Namespace messageNamespace = (Namespace) Enum.Parse(typeof (Namespace), message.Label);

                                if ((messageNamespace & command.ToNamespace) == command.ToNamespace)
                                {
                                    //using (StringReader sr = new StringReader(message.Body.ToString()))
                                    //{
                                        //XmlSerializer deserializer = new XmlSerializer(typeof (Command));
                                    Command tmp = (Command) message.Body;// deserializer.Deserialize(sr);
                                        
                                    if (tmp.CommandType == command.CommandType)
                                    {
                                        //don't send another - there is already one in the queue.
                                        Logger.Instance.Log(this.GetType(), LogType.Debug, command.CommandType + " Command to " + _ip + " squelched.");
                                        return false;
                                    }
                                    //}
                                }
                            }
                        }
                    }
                }
                //using (StringWriter sw = new StringWriter(new StringBuilder()))
                //{
                //    XmlSerializer serializer = new XmlSerializer(typeof (Command));
                //    serializer.Serialize(sw, command);

                using (Message message = new Message(command))//sw.ToString()))
                {
                    _messageQueue.Send(message, command.ToNamespace.ToString());//nameSpace); //the namespace it came from
                }
                //}
                Logger.Instance.Log(this.GetType(), LogType.Command, command.CommandType + " Command sent to " + _ip);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                return false;
            }
        }
        public Command GetCommand()//Namespace fromNamespace)
        {
            try
            {
                using (MessageEnumerator messageEnum = _messageQueue.GetMessageEnumerator2())
                {
                    while (messageEnum.MoveNext())
                    {
                        //loop through and grab the first message that matches the "from" namespace and remove it from the queue.
                        using (Message message = messageEnum.Current)
                        {
                            if (message != null)
                            {
                                Namespace messageNamespace = (Namespace) Enum.Parse(typeof (Namespace), message.Label);

                                if ((messageNamespace & _namespace) == _namespace)
                                {
                                    //message = messageEnum.Current;
                                    if (message.Label == _namespace.ToString())
                                        using(Message m = messageEnum.RemoveCurrent()) { /*the last thing should remove it*/ }
                                    else
                                        message.Label = (messageNamespace ^ _namespace).ToString(); //remove this namespace out of the current

                                    //using (StringReader sr = new StringReader(message.Body.ToString()))//Stream body = message.BodyStream)
                                    //{
                                        //XmlSerializer deserializer = new XmlSerializer(typeof (Command));
                                    Command ret = (Command)message.Body;//(Command)deserializer.Deserialize(sr);

                                    ret.FromIp = _ip;

                                    Logger.Instance.Log(this.GetType(), LogType.Command, ret.CommandType + " Command received from " + _ip);
                                    return ret;
                                    //}

                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(this.GetType(), ex);
                return null;
            }
        }
       
        public void Dispose()
        {
            if (_messageQueue != null)
                _messageQueue.Dispose();
        }

        private class CommandMessageFormatter: IMessageFormatter
        {
            #region Implementation of ICloneable

            public Object Clone()
            {
                return new CommandMessageFormatter();
            }

            #endregion

            #region Implementation of IMessageFormatter

            public bool CanRead(Message message)
            {
                return true;
            }

            public Object Read(Message message)
            {
                Byte[] bytes = new Byte[message.BodyStream.Length];
                using (Stream bodyStream = message.BodyStream)
                {
                    Int32 count = 0;
                    while (count < bodyStream.Length)
                        bytes[count++] = Convert.ToByte(bodyStream.ReadByte());
                }
                String s = Encoding.UTF8.GetString(bytes);

                using (StringReader sr = new StringReader(s))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof (Command));
                    return deserializer.Deserialize(sr);
                }
            }

            public void Write(Message message, Object obj)
            {
                if (obj.GetType() != typeof(Command))
                    throw new Exception("Invalid type - must be type of Command.");

                using (StringWriter sw = new StringWriter(new StringBuilder()))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Command));
                    serializer.Serialize(sw, obj);
                    using(MemoryStream stream = new MemoryStream(Encoding.UTF32.GetBytes(sw.ToString())))
                    {
                        message.BodyStream = stream;
                    }
                }
            }

            #endregion
        }
    }

    public interface ITalker: IDisposable
    {
        Boolean CanUse { get; }
        String Ip { get; }
        Boolean Connected { get; }
        Boolean SendCommand(Command command);//, Namespace toNamespace);
        Command GetCommand(); //Namespace fromNamespace);
    }

    [Flags]
    public enum Namespace
    {
        None = 0,
        Service = 1,
        Client = 2,
        Notifier = 4
    }

    [SerializableAttribute]
    public enum Commands
    {
        StartScheduler,
        StopScheduler,
        SchedulerStatus,
        ServiceStatus,
        GetResults,
        GetResultsResponse,
        GetAlertResults,
        GetAlertResultsResponse,
        ResultsSync,
        ResultsSyncResponse,
        UpdateConfiguration,
        UpdateConfigurationResponse,
        GetConfiguration,
        GetConfigurationResponse,
    }

    [SerializableAttribute]
    public class SyncDatas: IEnumerable<SyncData>
    {
        private List<SyncData> _data = new List<SyncData>();
        private Int64 _counter = 0;

        public SyncDatas() { }

        public List<SyncData> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Int64 Counter { get { return _counter; } set { _counter = value; } }

        public Int32 Count { get { return _data.Count; } }

        public void Clear()
        {
            _data.Clear();
        }
        public void Add(SyncData data)
        {
            _data.Add(data);
        }
        public void AddRange(IEnumerable<SyncData> datas)
        {
            _data.AddRange(datas);
        }

        public SyncData Find(Predicate<SyncData> predicate)
        {
            foreach (SyncData sd in _data)
                if (predicate(sd))
                    return sd;
            return null;
        }

        #region Implementation of IEnumerable

        public IEnumerator<SyncData> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    [SerializableAttribute]
    public class SyncData
    {
        public String GuidHash;
        public Int32 IntHash;
        public String FriendlyName;

        public SyncData() { }
    }

    [XmlInclude(typeof(ConfigurationData))]
    [XmlInclude(typeof(Results))]
    [XmlInclude(typeof(SyncDatas))]
    [XmlInclude(typeof(SerializableException))]
    [SerializableAttribute]
    public class Command
    {
        private Commands _commandType;
        private Object _data;
        private Boolean _good;
        private String _toIp;
        private String _fromIp;
        private Namespace _toNamespace;
        private Namespace _fromNamespace;

        public Commands CommandType
        {
            get { return _commandType; }
            set
            {
                _commandType = value;
                _good = true;
            }
        }

        public Object Data
        {
            get { return _data; }
            set
            {
                _data = value;
                _good = true;
            }
        }
        public Boolean Good { get { return _good; } }

        public String ToIp
        {
            get { return _toIp; }
            set { _toIp = value; }
        }
        public String FromIp
        {
            get { return _fromIp; }
            set { _fromIp = value; }
        }

        public Namespace ToNamespace
        {
            get { return _toNamespace; }
            set { _toNamespace = value; }
        }
        public Namespace FromNamespace
        {
            get { return _fromNamespace; }
            set { _fromNamespace = value; }
        }
    }

    [Serializable]
    public class SerializableException
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public SerializableException InnerException { get; set; }

        public SerializableException()
        {
            this.TimeStamp = DateTime.Now;
        }

        public SerializableException(Exception ex)
        {
            this.TimeStamp = DateTime.Now;
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;

            if (ex.InnerException != null)
                this.InnerException = new SerializableException(ex.InnerException);
        }

        public override string ToString()
        {
            return this.TimeStamp + ": " + this.Message + this.StackTrace;
        }
    }
}
