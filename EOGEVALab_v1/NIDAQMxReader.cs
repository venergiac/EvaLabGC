using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using NationalInstruments.DAQmx;
using System.Windows.Forms;

namespace EvaLab.EOG
{
    public class NIDAQMxReader
    {

        protected Task myTask = null;                //Main Task variable which gets called in the Main Function
        protected AnalogMultiChannelReader reader = null;

        private string currentChannel1 = "";

        public string CurrentChannel1
        {
            get { return currentChannel1; }
            set { currentChannel1 = value; if (myTask != null) Init(); }
        }
        private double minValueVoltage1 = -5;

        public double MinValueVoltage1
        {
            get { return minValueVoltage1; }
            set { minValueVoltage1 = value; if (myTask != null) Init(); }
        }
        private double maxValueVoltage1 = 5;

        public double MaxValueVoltage1
        {
            get { return maxValueVoltage1; }
            set { maxValueVoltage1 = value; if (myTask != null) Init(); }
        }

        private string currentChannel2 = "";

        public string CurrentChannel2
        {
            get { return currentChannel2; }
            set
            {
                currentChannel2 = value;
                if (myTask != null) Init();
            }
        }

        private double minValueVoltage2 = -5;

        public double MinValueVoltage2
        {
            get { return minValueVoltage2; }
            set { minValueVoltage2 = value; if (myTask != null) Init(); }
        }
        private double maxValueVoltage2 = 5;

        public double MaxValueVoltage2
        {
            get { return maxValueVoltage2; }
            set { maxValueVoltage2 = value; if (myTask != null) Init(); }
        }

        public string[] GetPhysicalChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
        }

        /// <summary>
        /// Init the myTask && the reader
        /// </summary>
        protected virtual void Init()
        {

            if (myTask != null)
            {
                myTask.Dispose();
                Debug.WriteLine("Task Disposed");
            }
            try
            {
            myTask = new Task();
            if ((currentChannel1 != null) && (currentChannel1.Length > 0))
            {

                //Create a virtual channel
                myTask.AIChannels.CreateVoltageChannel(currentChannel1, "",
                    (AITerminalConfiguration)(-1), minValueVoltage1,
                    maxValueVoltage1, AIVoltageUnits.Volts);
                Debug.WriteLine("Added new channel " + currentChannel1);
            }

            if ((currentChannel2 != null) && (currentChannel2.Length > 0) && !(currentChannel2.Equals(currentChannel1)))
            {

                //Create a virtual channel
                myTask.AIChannels.CreateVoltageChannel(currentChannel2, "",
                    (AITerminalConfiguration)(-1), minValueVoltage2,
                    maxValueVoltage2, AIVoltageUnits.Volts);
                Debug.WriteLine("Added new channel " + currentChannel2);
            }

            //Verify the Task
            myTask.Control(TaskAction.Verify);
            Debug.WriteLine("Task Initialized");

            reader = new AnalogMultiChannelReader(myTask.Stream);
            }
            catch (DaqException exception)
            {
                MessageBox.Show("Init : "+exception.Message);
                myTask.Dispose();
            }
        }

        public virtual void Dispose()
        {
            Debug.WriteLine("Task disposed");
            myTask.Dispose();
            myTask = null;
        }

        public double[] GetData()
        {
            if (reader == null)
            {
                Init();
            }
            return reader.ReadSingleSample();
        }

    }
}
