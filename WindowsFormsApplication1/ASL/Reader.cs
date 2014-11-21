using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Win32;

namespace EVALabGC.ASL
{

    public class Reader : TrackerReader
    {
        //private FakeSerialPort sp = new FakeSerialPort();
        private SerialPort sp = new SerialPort();

        private char[] RICHIESTA = { '0' };

        public HiPerfTimer timer = new HiPerfTimer();

        private string portName = "COM3";
        private int baudRate = 9600;
        private double frequency = 240.0;


        bool reading = false;
        private long startTime = 0;
        private Thread workerThread = null;

        //SerialErrorReceivedEventHandler
        SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = null;


        public Reader(string portName, int baudRate, int frequency)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.frequency = frequency;
        }



        public void OpenPort()
        {
            //ports
            sp.PortName = portName;
            sp.BaudRate = baudRate;
            sp.ReadBufferSize = 8;
            sp.DtrEnable = true;
            sp.RtsEnable = true;
            sp.ReadTimeout = 20;
            sp.WriteTimeout = 1000;

            //open port
            sp.Open();

            //register data andler
            //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            if (serialErrorReceivedEventHandler == null)
            {
                serialErrorReceivedEventHandler = new SerialErrorReceivedEventHandler(sp_ErrorReceived);
                sp.ErrorReceived += serialErrorReceivedEventHandler;
            }
        }

        public override void Start()
        {
            if (!sp.IsOpen)
            {
                Debug.WriteLine("Opening Com Port");
                OpenPort();
            }
            workerThread = new Thread(new ThreadStart(DoRequest));
            workerThread.Priority = ThreadPriority.Highest;
            Debug.WriteLine("Reader: Starting Thread");
            dataIdx = 0;
            workerThread.Start();
            reading = true;
            status = ReaderStatus.Started;
            this.stimulusController.OnStart();
            Debug.WriteLine("Reader: Push event start ");
            DoAslReaderEvent(startTime, GetTimeMS(), ReaderStatus.Started);
            Debug.WriteLine("Reader: First request");
            //timer.Start();
            //sp.Write(RICHIESTA, 0, 1);
        }

        private void DoRequest()
        {
            double totTime = 0;
            double msToWait = 1000.0 / frequency;
            timer.Start();
            double duration = 0;
            while (reading)
            {
                try
                {
                    lock (timer)
                    {
                        timer.Stop();
                    }
                    
                    duration = timer.GetDuration();
                    if ((duration >= msToWait) || (duration >= 100))
                    {
                        Debug.WriteLineIf(duration > 10, "timer.Duration = " + duration);
                        sp.Write(RICHIESTA, 0, 1);
                        totTime += duration;
                        ReadData(false, totTime);
                    }
                }
                catch (Exception e)
                {
                    if (EVALab.Util.Box.ExceptionForm.Show(null, "Error on requesting data", e)) return;
                }
            }
        }

        void sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (reading)
            {
                sp.Write(RICHIESTA, 0, 1);
            }
            Debug.WriteLine("ERROR: " + e.ToString());
        }

        byte[] data = new byte[8];
        int dataIdx = 0;

        /*void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReadData(true);
        }*/

        public void ReadData(bool requestAfterReading, double time)
        {
            //Debug.WriteLine("Reader: ReadData");
            try
            {
                lock (timer)
                {
                    timer.Start();
                    int nByte = sp.BytesToRead;
                    for (int i = 0; i < nByte; i++)
                    {
                        Debug.Assert(dataIdx < 8);
                        data[dataIdx++] = (byte)sp.ReadByte();
                        if (data[0] != 0)
                        {
                            sp.DiscardInBuffer();
                            if (requestAfterReading) sp.Write(RICHIESTA, 0, 1);
                            return;
                        }
                        if (dataIdx >= 8)
                        {
                            dataIdx = 0;

                            if (data[dataIdx] != 0) return;
                            if (requestAfterReading) sp.Write(RICHIESTA, 0, 1);

                            DoAslDataEvent(DoASLEventArgs(data, time));
                        }
                    }
                }
                //DoRequest();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public override void Stop()
        {
            if (status == ReaderStatus.Stopped) return;
            lock (this)
            {
                reading = false;
                //workerThread.Abort();
                status = ReaderStatus.Stopped;
                this.stimulusController.OnStop();
                DoAslReaderEvent(startTime, GetTimeMS(), ReaderStatus.Stopped);
                //sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);
                if (serialErrorReceivedEventHandler == null)
                {
                    sp.ErrorReceived -= serialErrorReceivedEventHandler;
                }
                Debug.WriteLine("Thread stopped");

                Thread.Sleep(300);
                
                sp.Close();
                Debug.WriteLine("COM closed");
            }
        }

        private ASLDataEventArgs[] pool = new ASLDataEventArgs[100];
        private int idxPool = 0;
        private ASLDataEventArgs DoASLEventArgs(byte[] data, double time)
        {
            lock (pool)
            {
                if (idxPool == 100) idxPool = 0;
                if (pool[idxPool] == null)
                {
                    Data dataC = new Data(0, data, time);
                    if (stimulusController != null)
                    {
                        dataC.StimulusId = stimulusController.GetCurrentStimulusId(dataC);
                    }
                    pool[idxPool] = new ASLDataEventArgs(dataC);
                }
                else
                {
                    pool[idxPool].Data.ParseData(0, data, time);
                    if (stimulusController != null)
                    {
                        pool[idxPool].Data.StimulusId = stimulusController.GetCurrentStimulusId(pool[idxPool].Data);
                    }
                }

                return pool[idxPool++];
            }
        }
    }
}
