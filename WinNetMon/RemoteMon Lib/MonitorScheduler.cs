using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

///THIS NEEDS TO BE REDONE WITH PARALLEL OR ASYNC

namespace RemoteMon_Lib
{
    public class MonitorScheduler //maybe this needs to be turned into a singleton with .Start() as the starter
    {
        private const Int32 GoalTime = 100; //100ms
        private const Int32 HalfGoalTime = GoalTime / 2; //50ms
        private const Int32 MaxGoalTime = 200 * GoalTime; //20 seconds
        private const Int32 Buffer = 85;
        private const Int32 MaxThreads = 40;
        private static Boolean kill = false;
        //private static Boolean verbose = false;

        private static readonly MonitorScheduler scheduler = new MonitorScheduler();

        private Thread _timer;
        private readonly ThreadScheduler _schedule;//, ThreadScheduler.Priority.High);
        private Boolean _start = false;

        public static MonitorScheduler Scheduler { get { return scheduler; } }

        public void Kill()
        {
            Logger.Instance.Log(this.GetType(), LogType.Info, "Stopping Monitors.");
            kill = true;
            _timer.Join(25);
            _schedule.Kill();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Monitors Stopped.");
        }

        private MonitorScheduler()//ConfigurationData configurationData)
        {
            _schedule = new ThreadScheduler(MaxThreads); //determine this number based on number of cores machine has -- core count * 10 maybe?
            _timer = new Thread(TimerCallback);
            _timer.Start();
        }

        public Boolean Running { get { return _start & !kill; } }

        private void TimerCallback()
        {
            Logger.Instance.Log(this.GetType(), LogType.Info, "Work queuer started.");
            while (!kill)
            {
                if (_start)
                    Requeue();

                Thread.Sleep(GoalTime);
            }
        }

        private void Requeue() //NOTE: this is responsible for requeueing the appropriate threads that need to
        {
            if (!Locks.Queueing)
                _schedule.Enqueue();
        }

        public void Start()
        {
            Logger.Instance.Log(this.GetType(), LogType.Info, "Scheduler starting.");
            kill = false;
            _start = true;
            if (_timer == null || !_timer.IsAlive)
            {
                _timer = new Thread(TimerCallback);
                _timer.Start();
            }

            _schedule.Start();
            Logger.Instance.Log(this.GetType(), LogType.Info, "Scheduler started.");
        }
        public void SetMonitors(ConfigurationData configurationData, Boolean start = false)
        {
            MonitorQueue.Queue.SetMonitors(configurationData);
            
            if(start)
                Start();
        }
        //public void SetMonitors(ConfigurationData configurationData)
        //{
        //    MonitorQueue.Queue.SetMonitors(configurationData);
        //}
        public static Results GetResults()
        {
            return MonitorQueue.Queue.LastResults;
        }

        private static class Locks
        {
            public static readonly Object ResultLocker = new Object();
            public static readonly Object MonitorLocker = new Object();
            public static readonly Object QueueLocker = new Object();
            public static volatile Boolean Queueing = false;
        }
        public sealed class MonitorQueue
        {
            private static volatile MonitorQueue queue = new MonitorQueue();

            private Results _lastResults = new Results();
            private Results _workingResults = new Results();

            private List<IWorkUnit> _allmonitors;// = new List<IWorkUnit>();

            private readonly Dictionary<String, IWorkUnit> _queue = new Dictionary<String, IWorkUnit>();

            public static MonitorQueue Queue { get { return queue; } }

            private MonitorQueue() { }

            public List<IWorkUnit> AllMonitors
            {
                get
                {
                    lock (MonitorScheduler.Locks.MonitorLocker)
                    {
                        if (_allmonitors == null)
                            return new List<IWorkUnit>();

                        return _allmonitors;
                    }
                }
            }
            public Int32 QueueCount { get { return _queue.Count; } }
            public Boolean QueueContainsKey(String key)
            {
                return _queue.ContainsKey(key);
            }
            public Dictionary<String, IWorkUnit> CurrentQueue
            {
                get
                {
                    lock (MonitorScheduler.Locks.QueueLocker)
                    {
                        return _queue;
                    }
                }
            }

            public void SetMonitors(ConfigurationData configurationData)
            {
                Logger.Instance.Log(this.GetType(), LogType.Info, "Loading Scheduler configuration.");
                lock (MonitorScheduler.Locks.MonitorLocker)
                {
                    if (_allmonitors == null)
                        _allmonitors = new List<IWorkUnit>(configurationData.Count);
                    else
                        _allmonitors.Clear();

                    _allmonitors.AddRange(configurationData.AllMonitors.Select(m => new MonitorInformation(m)));

                }
                Logger.Instance.Log(this.GetType(), LogType.Info, "Scheduler configuration loaded.");
            }

            public Results LastResults
            {
                get
                {
                    lock (MonitorScheduler.Locks.ResultLocker)
                    {
                        _lastResults = null;
                        _lastResults = _workingResults;
                        _workingResults = new Results();
                        return _lastResults;
                    }
                }
            }

            public void AddResults(IEnumerable<IResult> results)
            {
                lock (MonitorScheduler.Locks.ResultLocker)
                {
                    _workingResults.AddRange(results);
                }
            }

            public void AddResult(IResult result)
            {
                lock (MonitorScheduler.Locks.ResultLocker)
                {
                    _workingResults.Add(result);
                }
            }
        }
        private class MonitorInformations : IWorkUnits// IEnumerable<MonitorInformation>//<T> :  where T : IWorkUnit
        {
            private readonly List<IWorkUnit> _workUnits;
            private readonly String _hash;

            public MonitorInformations(IEnumerable<IWorkUnit> workUnits)
            {
                _workUnits = new List<IWorkUnit>(workUnits);
                _hash = Guid.NewGuid().ToString();
            }

            public void ChangeWorkUnits(IEnumerable<IWorkUnit> newWorkUnits)
            {
                _workUnits.Clear();
                _workUnits.AddRange(newWorkUnits);
            }

            public Int32 Count { get { return _workUnits.Count; } }
            public String Hash { get { return _hash; } }

            //public MonitorInformation[] Work { get { return _workUnits.ToArray(); } }

            public IEnumerator<IWorkUnit> GetEnumerator()
            {
                return _workUnits.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        private class MonitorInformation : IWorkUnit
        {
            private readonly IMonitor _monitor;
            private DateTime _lastRunTime;
            private Int64 _lastRunLength = 0;
            private readonly Stopwatch _stopwatch = new Stopwatch();
            private volatile Boolean _isRunning = false;
            private volatile Boolean _isScheduled = false;

            public MonitorInformation(IMonitor monitor)
            {
                _monitor = monitor;
            }

            public Int32 UpdateFrequency { get { return _monitor.UpdateFrequency; } }
            public DateTime LastRunTime { get { return _lastRunTime; } }
            public Int64 LastRunLength { get { return _lastRunLength; } }
            public bool IsRunning { get { return _isRunning; } set { _isRunning = value; } }
            public bool IsScheduled { get { return _isScheduled; } set { _isScheduled = value; } }

            public String Hash { get { return _monitor.Hash; } }

            public IResult Check()
            {
                IResult ir = _monitor.Check();
                _lastRunLength = ir.RunLength;
                _lastRunTime = ir.RunTime;
                return ir;
            }

            public override String ToString()
            {
                return _monitor.FriendlyName;
            }
        }
        private class ThreadScheduler
        {
            private Thread _worker;
            private NewThreads _threads;


            public ThreadScheduler(Int32 maxThreads)//, Priority schedulePriority)
            {
                _threads = new NewThreads(maxThreads);
                _worker = new Thread(Worker);
                //_worker.Start();
            }

            public void Start()
            {
                Logger.Instance.Log(this.GetType(), LogType.Info, "Thread scheduler starting.");
                if (_worker == null || !_worker.IsAlive)
                {
                    _worker = new Thread(Worker);
                    _worker.Start();
                }
                Logger.Instance.Log(this.GetType(), LogType.Info, "Thread scheduler started.");
            }

            public void Enqueue()
            {
                MonitorScheduler.Locks.Queueing = true;
                if (!_worker.IsAlive)
                {
                    _worker = new Thread(Worker);
                    _worker.Start();
                }
                //need to schedule check()'s based on how frequently things need to be monitored
                //     loop through and find out which ones should be run, based on the _lastRun and the current time
                //lock (MonitorScheduler.Locks.MonitorLocker)
                //{
                List<IWorkUnit> allmonitors = new List<IWorkUnit>(MonitorQueue.Queue.AllMonitors);
                foreach (IWorkUnit im in allmonitors)
                {
                    //nothing should be scheduled that's current running.
                    TimeSpan imTime = DateTime.Now - im.LastRunTime;

                    if (!im.IsRunning && !im.IsScheduled)
                    {
                        //queue up the ones that should be
                        if (imTime.TotalMilliseconds > im.UpdateFrequency)
                        {
                            //don't keep adding duplicates if Enqueue runs again before it is completed, which is a problem, but not terribly so
                            if (!MonitorQueue.Queue.QueueContainsKey(im.Hash))
                            {
                                im.IsScheduled = true;
                                MonitorQueue.Queue.CurrentQueue.Add(im.Hash, im);
                                if (imTime.TotalMilliseconds > (im.UpdateFrequency * 1.2) && imTime.TotalMilliseconds < 60000000000000) //before realistic time number
                                    Logger.Instance.Log(this.GetType(), LogType.Debug,
                                                        "Monitor: " + im.ToString() +
                                                        " was late in being scheduled by: " +
                                                        (imTime.TotalMilliseconds - im.UpdateFrequency).ToString() +
                                                        "ms.");
                            }
                        }
                    }
                }
                //}
                MonitorScheduler.Locks.Queueing = false;
            }

            public void Kill()
            {
                if(_worker != null && _worker.IsAlive)
                    _worker.Join(200);
                _threads.Kill();
            }

            private void Worker()
            {
                while (!kill)
                {
                    try
                    {
                        if (MonitorQueue.Queue.QueueCount > 0)
                        {
                            List<IWorkUnit> monitorInformations = new List<IWorkUnit>(MonitorQueue.Queue.CurrentQueue.Values);
                            //foreach (IWorkUnit iwu in MonitorQueue.Queue.CurrentQueue.Values)
                            //{
                            //    iwu.IsScheduled = true;
                            //    monitorInformations.Add(iwu);
                            //}
                            //monitorInformations.AddRange(MonitorQueue.Queue.CurrentQueue.Values);
                            _threads.DoWorkLoad(monitorInformations);

                            foreach (IWorkUnit iwu in MonitorQueue.Queue.CurrentQueue.Values)
                                iwu.IsScheduled = false;

                            MonitorQueue.Queue.CurrentQueue.Clear();
                        }

                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogException(this.GetType(), ex);
                        //log exception
                        if (MonitorQueue.Queue.QueueCount > 0)
                        {
                            //lock (MonitorScheduler.Locks.QueueLocker)
                            //{
                            foreach (IWorkUnit iwu in MonitorQueue.Queue.CurrentQueue.Values)
                                iwu.IsScheduled = false;
                            MonitorQueue.Queue.CurrentQueue.Clear();
                            //}
                        }
                        break;
                    }
                    Thread.Sleep(MonitorScheduler.Buffer); //loop every 85 ms
                }
            }

            private class NewThreads
            {
                private readonly List<NewThread> _newThreads;// = new List<NewThread>();
                private Int32 _maxThreads;
                private Int32 _currentThreads = 0; //NOTE: amount of threads used last time DoWorkLoad was called

                private NewThreads()
                {
                    //this class needs to know the state of the threads, how long they've been running, and 
                    //whether or not new work items should kill an old thread or be allowed to run through and have new threads add
                    //  -what to do about work units that come in when maxthreads is reached?

                    //need some benchmarking -
                    //  -first time spin off max threads and time each thread, if the time it takes is less than 100ms, 
                    //   keep stacking them together until it reaches close to that
                }
                public NewThreads(Int32 maxThreads)
                {
                    _maxThreads = maxThreads;
                    _newThreads = new List<NewThread>(_maxThreads);
                    for (Int32 x = 0; x < _maxThreads; x++)
                    {
                        _newThreads.Add(null);
                    }
                }
                public void Kill()
                {
                    foreach (NewThread nt in _newThreads)
                    {
                        if (nt != null)
                            nt.Kill();
                    }
                }
                public void DoWorkLoad(List<IWorkUnit> workUnits)
                {
                    //split up work into units and decide which NewThread to put it on, based on how many threads are alive and
                    //how long it took previously?
                    if (_currentThreads == 0)
                    {
                        if (_maxThreads > workUnits.Count)
                            _currentThreads = DoWorkThreadCare(new[] { workUnits }, 1);
                        else
                            _currentThreads = DoWorkThreadCare(new[] { workUnits }, workUnits.Count / _maxThreads);
                    }
                    else
                    {
                        //rebalance the threads

                        //total new threads is going to be at least greatgoaltime.count + greathalfgoaltime.count
                        List<IWorkUnit> lessHalfGoalTime = new List<IWorkUnit>(workUnits.Count);
                        List<IWorkUnit> greatHalfGoalTime = new List<IWorkUnit>(workUnits.Count);
                        List<IWorkUnit> greatGoalTime = new List<IWorkUnit>(workUnits.Count);
                        //need to figure out how to split them based on last used time
                        //  need to loop through and find the way to group them to create the least amount of newthreads
                        // ex values:
                        // 23 ms
                        // 45 ms
                        // 11 ms
                        // 80 ms
                        // 37 ms
                        // 64 ms
                        // 225 ms
                        // 18 ms
                        // 10 ms
                        // 74 ms
                        // 184 ms
                        // 83 ms
                        // 8 ms
                        // 48 ms
                        // 55 ms
                        //group into three classes: < goaltime/2, and > goaltime/2, > goaltime
                        // 23 ms    80 ms   225 ms
                        // 45 ms    64 ms   184 ms
                        // 11 ms    74 ms
                        // 37 ms    83 ms
                        // 18 ms    55 ms
                        // 10 ms
                        // 8 ms
                        // 48 ms
                        foreach (IWorkUnit mi in workUnits)
                        {
                            if (mi.LastRunLength > MonitorScheduler.GoalTime)
                                greatGoalTime.Add(mi);
                            else if (mi.LastRunLength > MonitorScheduler.HalfGoalTime)
                                greatHalfGoalTime.Add(mi);
                            else
                                lessHalfGoalTime.Add(mi);
                        }
                        //find possible matches for each of the first two groups

                        //  start with the highest number because that will have the fewest choices
                        greatHalfGoalTime.Sort((mi1, mi2) => mi2.LastRunLength.CompareTo(mi1.LastRunLength));
                        //    83 ms can match with 11 ms and 8 ms
                        //    should choose 11 ms because it will leave an easier match for the future with an 8 ms
                        lessHalfGoalTime.Sort((mi1, mi2) => mi2.LastRunLength.CompareTo(mi1.LastRunLength));

                        Dictionary<String, List<IWorkUnit>> decidedMatches =
                            new Dictionary<String, List<IWorkUnit>>();
                        foreach (IWorkUnit mihg in greatHalfGoalTime)
                        {
                            //loop through lesshalfgoaltime and create a list of possible matches 
                            //decidedMatches.Add(mig.Hash, new List<MonitorInformation>());
                            decidedMatches[mihg.Hash] = PickMatches(mihg, lessHalfGoalTime);
                            decidedMatches[mihg.Hash].Add(mihg);
                            //otherwise this won't be part of it, only the lesshalfgoaltime matches
                        }
                        foreach (IWorkUnit mig in greatGoalTime)
                        {
                            decidedMatches[mig.Hash] = new List<IWorkUnit>(1) { mig };
                            //this took a long time last time, so put it in a separate thread
                        }
                        //check to make sure everything has been taken care of
                        if (lessHalfGoalTime.Count > 0)
                        {
                            //if there are still extra lesshalfgoaltime items then group them
                            Int64 timerCount = 0;
                            Int32 matchCount = 0;
                            for (Int32 x = 0; x < lessHalfGoalTime.Count; x++)
                            {
                                if (!decidedMatches.ContainsKey(matchCount.ToString()))
                                    decidedMatches.Add(matchCount.ToString(), new List<IWorkUnit>(lessHalfGoalTime.Count));
                                decidedMatches[matchCount.ToString()].Add(lessHalfGoalTime[x]);

                                timerCount += lessHalfGoalTime[x].LastRunLength;

                                if (timerCount > GoalTime || decidedMatches[matchCount.ToString()].Count > 9)
                                {
                                    matchCount++;
                                    timerCount = 0;
                                }
                            }
                        }
                        //matches need to be queued
                        _currentThreads = DoWorkThreadCare(decidedMatches.Values);
                    }
                }

                private static List<IWorkUnit> PickMatches(IWorkUnit mi, List<IWorkUnit> subList)//, out List<MonitorInformation> unaccountedMatches)
                {
                    //unaccountedMatches = new List<MonitorInformation>();
                    List<IWorkUnit> possibleMatches = new List<IWorkUnit>(subList.Count);
                    foreach (IWorkUnit mil in subList)
                    {
                        if ((mi.LastRunLength + mil.LastRunLength) < MonitorScheduler.GoalTime)
                        {
                            possibleMatches.Add(mil);
                        }
                    }
                    //sort possible matches
                    if (possibleMatches.Count > 0)
                    {
                        possibleMatches.Sort((mi1, mi2) => mi2.LastRunLength.CompareTo(mi1.LastRunLength));
                        List<IWorkUnit> definiteMatches = new List<IWorkUnit>(subList.Count);

                        while (possibleMatches.Count > 0)
                        {
                            Int64[] total = { 0 };
                            definiteMatches.ForEach(m => total[0] += m.LastRunLength);
                            total[0] += mi.LastRunLength;
                            if (total[0] < MonitorScheduler.GoalTime && (possibleMatches[0].LastRunLength + total[0] < MonitorScheduler.GoalTime))
                            {
                                definiteMatches.Add(possibleMatches[0]);
                                subList.Remove(possibleMatches[0]);
                                possibleMatches.Remove(possibleMatches[0]);
                            }
                            else
                                break;
                        }
                        return definiteMatches;
                    }

                    return possibleMatches;
                }

                private Int32 DoWorkThreadCare(IEnumerable<List<IWorkUnit>> workUnitses, Int32 countPerNewThread = -1)
                {
                    //Int32 totalCounter = 0;
                    Int32 counterTotal = 0;
                    Boolean useCount = false;
                    if (countPerNewThread == -1)
                        useCount = true;
                    //Random r = new Random();
                    Boolean startKilling = false;

                    foreach (List<IWorkUnit> workUnits in workUnitses)
                    {
                        //foreach (IWorkUnit iwu in workUnits)
                        //    iwu.IsScheduled = true;

                        //try
                        //{
                        //need to spread threads over range of threads to avoid always depending on the first few
                        Int32 counter = new Random().Next(0, _maxThreads - 1);
                        if (useCount)
                            countPerNewThread = workUnits.Count;
                        Int32 workUnitCounter = 0;
                        //for (; ; counter++) // i should never have more threads than work units
                        while (workUnitCounter < workUnits.Count)
                        {
                            try
                            {
                                if (_newThreads[counter] == null) //never used this thread
                                {
                                    List<IWorkUnit> range = workUnits.GetRange(workUnitCounter, countPerNewThread);
                                    //totalCounter += countPerNewThread;
                                    workUnitCounter += countPerNewThread;
                                    _newThreads[counter] = new NewThread(range.ToArray());
                                    _newThreads[counter].DoWork();
                                }
                                else if (_newThreads[counter].IsAlive) //thread is alive
                                {
                                    if (startKilling)
                                    {
                                        if (_newThreads[counter].Killable)
                                        //living thread is old and should be remade - been running for 500ms
                                        {
                                            List<IWorkUnit> range = workUnits.GetRange(workUnitCounter,
                                                                                                countPerNewThread);
                                            //totalCounter += countPerNewThread;
                                            workUnitCounter += countPerNewThread;
                                            _newThreads[counter].KillAndRemake(range.ToArray());
                                            _newThreads[counter].DoWork();
                                        }
                                    }
                                    else //living thread is not old, so skip to the next thread and use that
                                    {
                                        continue;
                                    }
                                }
                                else //thread is dead
                                {
                                    List<IWorkUnit> range = workUnits.GetRange(workUnitCounter, countPerNewThread);
                                    //totalCounter += countPerNewThread;
                                    workUnitCounter += countPerNewThread;
                                    _newThreads[counter].KillAndRemake(range.ToArray());
                                    _newThreads[counter].DoWork();
                                }
                                //x++; //why do i need this?
                                counterTotal++;
                                //totalCounter++;
                                if (counterTotal > (_maxThreads / 2))
                                {
                                    Logger.Instance.Log(this.GetType(), LogType.Debug, "Still looking for thread to schedule work...");
                                    Thread.Sleep(10);
                                    if (counterTotal > (_maxThreads * .67)) //if this has managed get through 2/3 of the threads then start killing old ones
                                        startKilling = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Instance.LogException(this.GetType(), ex);
                                foreach (IWorkUnit mi in workUnits)
                                {
                                    mi.IsScheduled = false;
                                }
                            }
                        }

                        if ((counterTotal * countPerNewThread) < workUnits.Count) //not all the workunits were scheduled and 
                        {
                            //log this i guess? figure out what to do about this... this shouldn't happen
                            Logger.Instance.Log(this.GetType(), LogType.Wtf, "Some work units were not scheduled, for some reason."); //NOTE: Wtf.
                        }
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                    }

                    return counterTotal;
                }

                public Int32 MaxCount { get { return _maxThreads; } set { _maxThreads = value; } }
                public Int32 CurrentCount { get { return _currentThreads; } }


                private sealed class NewThread
                {
                    private Thread _thread;
                    private Int64 _lastRunningTime;
                    //private readonly String _hash;
                    private readonly IWorkUnits _work;

                    //NOTE: every active NewThread instance gets a stopwatch
                    //private readonly static Dictionary<String, Stopwatch> timers = new Dictionary<String, Stopwatch>();

                    public NewThread(IEnumerable<IWorkUnit> work)
                    {
                        if (work == null)
                        {
                            throw new Exception("Work load cannot be null");
                        }
                        _thread = new Thread(this.Check) { IsBackground = true };
                        _thread.IsBackground = true;

                        _work = new MonitorInformations(work);
                    }

                    public void Kill()
                    {
                        if (_thread != null && _thread.IsAlive)
                            _thread.Join(15);
                    }

                    public void KillAndRemake(IEnumerable<IWorkUnit> work)
                    {
                        if (_thread != null && _thread.IsAlive)
                            _thread.Join(25);
                        _thread = new Thread(this.Check) { IsBackground = true };
                        _thread.IsBackground = true;

                        _work.ChangeWorkUnits(work);
                    }

                    public void DoWork()
                    {
                        //if (!timers.ContainsKey(Hash))
                        //{
                        //    timers.Add(Hash, new Stopwatch());
                        //}
                        if (_thread == null)
                            _thread = new Thread(this.Check) { IsBackground = true };

                        if (!_thread.IsAlive)
                            _thread.Start();//_work);
                        else
                        {
                            _thread.Join(25);
                            _thread = new Thread(this.Check) { IsBackground = true };
                            _thread.Start();//_work);
                        }
                    }

                    private void Check()//Object info) //thread worker
                    {
                        try
                        {
                            //MonitorInformations mi = (MonitorInformations)info;
                            //List<MonitorInformation> miList = new List<MonitorInformation>(mi);
                            //Stopwatch s = timers[_work.Hash];//mi.Hash];
                            //s.Reset();
                            //s.Start();
                            if (_work.Count > 1) 
                            {
                                List<IResult> results = new List<IResult>(_work.Count); //mi.Count);
                                //s.Start();
                                foreach (IWorkUnit monitorInformation in _work) //mi)
                                {
                                    monitorInformation.IsRunning = true;
                                    IResult result = monitorInformation.Check();
                                    _lastRunningTime = result.RunLength;
                                    Logger.Instance.LogMonitor(this.GetType(), result);
                                    results.Add(result);
                                    monitorInformation.IsRunning = false;
                                    //monitorInformation.IsScheduled = false;
                                }
                                //s.Stop();
                                //lock (MonitorScheduler.Locks.ResultLocker)
                                //{
                                MonitorQueue.Queue.AddResults(results);
                                //}
                            }
                            else
                            {
                                foreach (IWorkUnit monitorInformation in _work) //mi)
                                {
                                    //s.Start();
                                    monitorInformation.IsRunning = true;
                                    IResult result = monitorInformation.Check();
                                    _lastRunningTime = result.RunLength;
                                    Logger.Instance.LogMonitor(this.GetType(), result);
                                    monitorInformation.IsRunning = false;
                                    //monitorInformation.IsScheduled = false;
                                    //s.Stop();
                                    //lock (MonitorScheduler.Locks.ResultLocker)
                                    //{
                                    MonitorQueue.Queue.AddResult(result);
                                    //}
                                }
                            }
                            //s.Stop();
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogException(typeof(NewThread), ex);
                        }

                        //foreach (IWorkUnit iwu in _work)
                        //{
                        //    if (iwu.IsScheduled)
                        //    {
                        //        //throw new Exception("");
                        //    }
                        //}
                    }

                    public Boolean IsAlive { get { return _thread.IsAlive; } }
                    public Boolean Killable { get { return RunTime > MonitorScheduler.MaxGoalTime; } }
                    //public String Hash { get { return _work.Hash; } }
                    public Int64 RunTime { get { return _lastRunningTime; } }

                    //~NewThread()
                    //{
                    //    timers.Remove(_work.Hash);
                    //}
                }
            }

            public enum Priority //NOTE: unused at this point
            {
                Low,
                Medium,
                High
            }
        }
        public interface IWorkUnit
        {
            String Hash { get; }
            IResult Check();
            Int64 LastRunLength { get; }
            DateTime LastRunTime { get; }
            Int32 UpdateFrequency { get; }
            Boolean IsRunning { get; set; }
            Boolean IsScheduled { get; set; }
            //void UpdateLastRun();
        }
        public interface IWorkUnits : IEnumerable<IWorkUnit>
        {
            void ChangeWorkUnits(IEnumerable<IWorkUnit> work);
            Int32 Count { get; }
            //String Hash { get; }
        }
    }
}
