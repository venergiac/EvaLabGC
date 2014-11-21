using System;
using System.Collections.Generic;
using System.Text;
using NationalInstruments;
using NationalInstruments.DAQmx;
using EVALab.Util.Box;
using System.Diagnostics;

namespace EvaLab.EOG
{
    public class NIDAQMxReaderService : NIDAQMxReader
    {

        private int frequency = 100;
        private AsyncCallback myAsyncCallback;
        private AnalogWaveform<double>[] dataWave;
        private Task runningTask;

        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        private NIDAQMxClient client = null;

        public NIDAQMxClient Client
        {
            get { return client; }
            set { client = value; }
        }

        /// <summary>
        /// Init the myTask && the reader
        /// </summary>
        protected override void Init()
        {
            base.Init();

            // Configure the timing parameters
            myTask.Timing.ConfigureSampleClock("", Convert.ToDouble(frequency), SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples);

            // For .NET Framework 2.0 or later, use SynchronizeCallbacks to specify that the object 
            // marshals callbacks across threads appropriately.
            reader.SynchronizeCallbacks = true;
        }

        public void CheckTask()
        {
            if (myTask == null) Init();
        }

        public void Start()
        {
            if (myTask == null) Init();
            runningTask = myTask;
            myAsyncCallback = new AsyncCallback(myCallback);
            reader.BeginReadWaveform(1, myAsyncCallback, myTask);
            Debug.WriteLine("BeginReadWaveform");
        }

        private void myCallback(IAsyncResult ar)
        {
            try
            {
                 
                if (runningTask == ar.AsyncState)
                {                
                    //double[] data = reader.EndReadSingleSample(ar);
                    dataWave = reader.EndReadWaveform(ar);

                    double[] data = new double[2];
                    reader.BeginMemoryOptimizedReadWaveform(1, myAsyncCallback, myTask, dataWave);
                    
                    int currentLineIndex = 0;
                    foreach (AnalogWaveform<double> waveform in dataWave)
                    {
                        for (int sample = 0; sample < waveform.Samples.Count; ++sample)
                        {
                            if (sample == 1)
                                break;

                            data[currentLineIndex] = waveform.Samples[sample].Value;
                        }
                        currentLineIndex++;
                    }

                    //Call to store data
                    client.DoReceive(data);
                }
            }
            catch (DaqException ex)
            {
                ExceptionForm.Show(null,"Error on reading data",ex);
                if (runningTask!=null) runningTask.Dispose();
            }
        }

        public override void Dispose()
        {
            runningTask = null;
            if (myTask != null) myTask.Dispose();
            myTask = null;
            Debug.WriteLine("Task Disposed");
        }
    }

    public interface NIDAQMxClient {

        void DoReceive(double[] data);

    }
}
