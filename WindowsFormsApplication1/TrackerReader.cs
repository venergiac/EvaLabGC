using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EVALabGC
{
    public abstract class TrackerReader
    {
        protected ReaderStatus status = ReaderStatus.Stopped;
        public ReaderStatus Status
        {
            get
            {
                return status;
            }
        }

        public StimulusController StimulusController
        {
            get { return stimulusController; }
            set { stimulusController = value; }
        }


        protected Context context = null;
        public Context Context
        {
            get { return context; }
            set { context = value; }
        }

        public enum ReaderStatus
        {
            Started,
            Stopped,
            Error,
            Unknown
        }

        //create an Serial Port object
        protected StimulusController stimulusController = null;

        public abstract void Start();

        public abstract void Stop();

        public delegate void ASLDataEventHandler(object sender, ASLDataEventArgs e);
        public event ASLDataEventHandler ASLDataEvent;
        public delegate void ASLReaderEventHandler(object sender, ASLReaderEventArgs e);
        public event ASLReaderEventHandler ASLReaderEvent;

        protected void DoAslReaderEvent(long startTime, long currentTime, ReaderStatus status)
        {
            if (this.ASLReaderEvent != null) this.ASLReaderEvent(this, new ASLReaderEventArgs(startTime, currentTime, status));
        }

        protected void DoAslDataEvent(ASLDataEventArgs args)
        {
            if (ASLDataEvent != null) ASLDataEvent(this, args);
        }


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
    }

    /***
    * Classe evento
    * */
    public class ASLDataEventArgs : EventArgs
    {
        private Data data;

        public Data Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public ASLDataEventArgs(Data data)
        {
            this.data = data;
        }
    }

    public class ASLReaderEventArgs : EventArgs
    {
        private EVALabGC.TrackerReader.ReaderStatus status = EVALabGC.TrackerReader.ReaderStatus.Unknown;

        public EVALabGC.TrackerReader.ReaderStatus Status
        {
            get { return status; }
        }
        private long startTime = 0;

        public long StartTime
        {
            get { return startTime; }
        }
        private long currentTime = 0;

        public long CurrentTime
        {
            get { return currentTime; }
        }
        public ASLReaderEventArgs(long startTime, long currentTime, EVALabGC.TrackerReader.ReaderStatus status)
        {
            this.status = status;
            this.startTime = startTime;
            this.currentTime = currentTime;
        }
    }
}
