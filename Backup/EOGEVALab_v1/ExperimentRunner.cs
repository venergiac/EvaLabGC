using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.Runtime.InteropServices;
using System.Diagnostics;
using Win32;
using System.Windows.Forms;
using EvaLab.EOG.Trigger;	// DllImport

namespace EvaLab.EOG
{
    /// <summary>
    /// The Main Thread able to push stimulus and get data
    /// </summary>
    public class ExperimentRunner
    {
        private ParallelPort parallelPort = null;
        protected NIDAQMxReader reader = null;
        protected List<ExperimentItem> items = null;
        protected Random rnd = new Random();

        protected Thread workerThread = null;

        public delegate void ExperimentRunnerHandler(object o, ExperimentRunnerEventArgs e);
        public static event ExperimentRunnerHandler EventRunner;

        protected bool requestData = true;

        private int frequency = 100;

        private bool running = false;

        public HiPerfTimer timer = new HiPerfTimer();

        protected IContext ctx = null;

        public int Status
        {
            get { 
                if (running) return ExperimentRunnerEventArgs.EXPERIMENT_START;
                return ExperimentRunnerEventArgs.EXPERIMENT_STOP; 
            }
        }

        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        protected int currentExperimentItem = ExperimentRunnerEventArgs.EXPERIMENT_INVALID;
        private int currentExperimentValue = ExperimentRunnerEventArgs.EXPERIMENT_VALUE_NULL;

        public int CurrentExperimentValue
        {
            get { return currentExperimentValue; }
            set { currentExperimentValue = value; }
        }

        /// <summary>
        /// Initialize the Runner
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="parallelPort"></param>
        /// <param name="items"></param>
        public void InitForTest(IContext ctx)
        {
            this.CurrentExperimentValue = 0;
            this.currentExperimentItem = ExperimentRunnerEventArgs.EXPERIMENT_TEST;
            Init(ctx);
        }

        /// <summary>
        /// Initialize the Runner
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="parallelPort"></param>
        /// <param name="items"></param>
        public void Init(IContext ctx)
        {
            this.ctx = ctx;
            this.items = ctx.GetExperiment();
            this.parallelPort = ctx.GetParallelPort();
            workerThread = new Thread(new ThreadStart(Run));
            workerThread.Priority = ThreadPriority.Highest;
            this.reader = ctx.GetExperimentReader(); ;
        }

        protected double HotTestStart()
        {
            //hots start NIDAQ
            double lastTime = 0;
            double totTime = 0;
            double msToWait = 1000.0 / frequency;
            for (int i = 0; i < 100; i++)
            {
                while (running)
                {
                    lock (timer)
                    {
                        timer.Stop();
                    }

                    double duration = timer.GetDuration();
                    if ((duration >= msToWait) || (duration >= 100))
                    {
                        ReadData();
                        long startTimeTmp = GetTimeMS();
                        if (i > 90)
                        {
                            totTime += startTimeTmp - lastTime;
                        }
                        lastTime = startTimeTmp;
                        break;
                    }
                }
            }

            return totTime / 10.0;
        }

        /// <summary>
        /// Start the current thread
        /// </summary>
        public virtual void Start()
        {
            running = true;
            lastTime = 0;
            lastDuration = 0;
            startTime = GetTimeMS();
            i = -1;

            double msToWait = 1000.0 / frequency;
            double msReal = HotTestStart();
            if (!(msReal <= msToWait + msToWait*0.2))
            {
                DialogResult result= MessageBox.Show("Warning acquisition is lossing data.", "Millisecond to wait " + msToWait + " real ms " + msReal, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }
            Debug.WriteLine(msReal + " vs " + msToWait);

            //start
            startTime = GetTimeMS();
            workerThread.Start();
            if (EventRunner != null)
            {
                EventRunner(this, new ExperimentRunnerEventArgs(ExperimentRunnerEventArgs.EXPERIMENT_START,startTime));
            }
        }

        /// <summary>
        /// Stop the current thread
        /// </summary>
        public virtual void Stop()
        {
            Debug.WriteLine("Runner Stap call");
            try
            {
                running = false;
                workerThread.Abort();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                if (EventRunner != null)
                {
                    EventRunner(this, new ExperimentRunnerEventArgs(ExperimentRunnerEventArgs.EXPERIMENT_STOP, startTime));
                }
            }
        }

        /// <summary>
        /// Methdo to run in order to get data
        /// </summary>
        long lastTime = 0;
        long lastDuration = 0;
        int i = -1;
        protected long startTime = 0;
        protected double time = 0;
        protected ITrigger currentTrigger = null;

        private void Run()
        {
            try
            {
                double msToWait = 1000.0 / frequency;
                time = 0;
                double duration = 0;        
                while (((items == null) && (running)) || ((i + 1 < items.Count) && (running)))
                {
                    lock (timer)
                    {
                        timer.Stop();
                    }

                    duration = timer.GetDuration();
                    if ((duration >= msToWait) || (duration >= 100))
                    {
                        time = GetTimeMS() - startTime;
                        if ((items != null) && (DateTime.Now.Ticks - lastTime > lastDuration * 10000))
                        {
                            i++;
                            currentExperimentItem = i;
                            ExperimentItem item = items[i];
                            lastDuration = item.Duration;
                            lastTime = DateTime.Now.Ticks;

                            //choose stimlus
                            int[] values = item.Values;
                            int idx = rnd.Next(0, values.Length);
                            Debug.WriteLine("time " + time);

                            //set trigger
                            if (currentTrigger != null)
                            {
                                currentTrigger.DoAfter(ctx);
                            }
                            currentTrigger = item.Trigger;
                            if (currentTrigger != null)
                            {
                                currentTrigger.DoBefore(ctx);
                            }
                            //push stimlus
                            DoCurrentExperimentValue(values[idx]);

                            //old way
                            //parallelPort.Show(currentExperimentValue = values[idx]);

                            //if (requestData) DoEventRunner(currentExperimentItem, currentExperimentValue, time);
                        }
                        else
                        {
                            if (requestData) DoEventRunner(i, currentExperimentValue, time);
                        }
                        
                    }
                }
            }
            finally
            {
                Stop();
            }
        }

        public virtual void DoCurrentExperimentValue(int v) {
            parallelPort.Show(currentExperimentValue = v);
            if (requestData) DoEventRunner(currentExperimentItem, currentExperimentValue, time);
        }

        protected virtual void DoEventRunner(int i, int currentExperimentValue, double[] data, double time)
        {
            if (EventRunner != null)
            {
                EventRunner(this, new ExperimentRunnerEventArgs(i, currentExperimentValue, data, time));
            }
        }

        protected virtual void DoEventRunner(int i, int currentExperimentValue, double time)
        {
            if (EventRunner != null)
            {
                EventRunner(this, new ExperimentRunnerEventArgs(i, currentExperimentValue, ReadData(), time));
            }
        }

        /// <summary>
        /// ReadData from redear (NidAQ)
        /// </summary>
        /// <returns></returns>
        protected virtual double[] ReadData()
        {
            double[] data = null;
            try
            {
                lock (timer)
                {
                    timer.Start();
                    lock (reader)
                    {
                        data = reader.GetData();
                    }
                }
            }
            catch (Exception e)
            {
                if (!EVALab.Util.Box.ExceptionForm.Show(null, "Unable to Open AnalogMultiChannelReader", e))
                {
                    this.Stop();
                }
            }
            return data;
        }

        /// <summary>
        /// extern read data sync
        /// </summary>
        /// <returns></returns>
        public virtual double[] DoData()
        {
            double[] data = ReadData();
            if (EventRunner != null)
            {
                EventRunner(this, new ExperimentRunnerEventArgs(data, startTime));
            }
            return data;
        }

        /// <summary>
        /// Unfortunatly the DateTime.Now.Ticks doesn't work at resolution < 16ms
        /// </summary>
        /// <param name="lpSystemTime"></param>
        #region GetTimeMSByKernel
        [DllImport("Kernel32.dll")]
        protected extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        protected struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        protected long GetTimeMS()
        {
            // Call the native GetSystemTime method
            // with the defined structure.
            SYSTEMTIME stime = new SYSTEMTIME();
            GetSystemTime(ref stime);

            return (long)stime.wMilliseconds + (long)stime.wSecond * 1000 + (long)stime.wMinute * 60000;
        }
        #endregion

        /// <summary>
        /// EventArgs to push event
        /// </summary>
        public class ExperimentRunnerEventArgs : EventArgs
        {
            public static int EXPERIMENT_TEST = -50;
            public static int EXPERIMENT_DATA = -40;
            public static int EXPERIMENT_INVALID = -30;
            public static int EXPERIMENT_START = -20;
            public static int EXPERIMENT_STOP = -10;

            public static int EXPERIMENT_VALUE_NULL = 255;

            private double time = 0;

            public double Time
            {
                get { return time; }
                set { time = value; }
            }

            private int experimentValue = EXPERIMENT_VALUE_NULL;

            public int ExperimentValue
            {
                get { return experimentValue; }
                set { experimentValue = value; }
            }

            /// <summary>
            /// IDX of current item
            /// </summary>
            private int currentExperimentItem;

            public int CurrentExperimentItem
            {
                get { return currentExperimentItem; }
                set { currentExperimentItem = value; }
            }

            /// <summary>
            /// datas
            /// </summary>
            protected readonly double[] dataValues;

            public double[] DataValues
            {
                get { return dataValues; }
            }

            public ExperimentRunnerEventArgs(int currentExperimentItem, double time)
            {
                this.currentExperimentItem = currentExperimentItem;
                this.experimentValue = EXPERIMENT_VALUE_NULL;
                this.dataValues = null;
                this.time = time;
            }

            public ExperimentRunnerEventArgs(int currentExperimentItem, int experimentValue, double time)
            {
                this.currentExperimentItem = currentExperimentItem;
                this.experimentValue = experimentValue;
                this.dataValues = null;
                this.time = time;
            }

            public ExperimentRunnerEventArgs(int currentExperimentItem, int experimentValue, double[] dataValues, double time)
            {
                this.currentExperimentItem = currentExperimentItem;
                this.experimentValue = experimentValue;
                this.dataValues = dataValues;
                this.time = time;
            }

            public ExperimentRunnerEventArgs(double[] dataValues, double time)
            {
                this.currentExperimentItem = EXPERIMENT_DATA;
                this.dataValues = dataValues;
                this.time = time;
            }

        }

    }
}
