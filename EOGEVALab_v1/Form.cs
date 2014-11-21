#define EMBEDDED_SERVICE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using EvaLab.EOG.Model;
using EVALab.Util.Box;
using EVALab.Util.Meta;
using EVALab.Util.License;

namespace EvaLab.EOG
{
    /// <summary>
    /// Main Form
    /// </summary>
    public partial class Form1 : Form, IContext
    {
        private Config configuration = new Config();
        private ParallelPort parallelPort = new ParallelPort();
        private List<ExperimentItem> items = new List<ExperimentItem>();

#if EMBEDDED_SERVICE
        private ExperimentRunnerClient runner = new ExperimentRunnerClient();
        private NIDAQMxReaderService daqReader = new NIDAQMxReaderService();
#else
        private ExperimentRunner runner = new ExperimentRunner();
        private NIDAQMxReader daqReader = new NIDAQMxReader();
#endif

        private FormGraph graph = new FormGraph();

        private delegate void SetValueItemCallback(int value);
        private delegate void SetValueDataCallback(double time, double[] values, double reference);

        private DateTime startTime = DateTime.Now;
        private StringBuilder buffer = new StringBuilder("");
        private bool started = false;
        private int sample = 0;
        private decimal refreshSample = 10;
        Calibration calibrationManager = new Calibration();
        License license;
        public Form1()
        {
            InitializeComponent();

            // Obtain the license
            license = LicenseManager.Validate(typeof(FakeApp), this);

            Init();
        }

        /// <summary>
        /// Read data from configuration file and register itself to ExperimentRunner events
        /// </summary>
        private void Init()
        {
            parallelPort.BaseAddress = configuration.portAddress;

            numericUpDownCh1MaxV.Value = configuration.maxVoltageCh1;
            numericUpDownCh1MinV.Value = configuration.minVoltageCh1;
            numericUpDownCh2MaxV.Value = configuration.maxVoltageCh2;
            numericUpDownCh2MinV.Value = configuration.minVoltageCh2;
            numericUpDownFrequency.Value = configuration.frequency;

            //InitializeComponent DAQ
            comboCh1Channels.Items.AddRange(daqReader.GetPhysicalChannels());
            if (comboCh1Channels.Items.Count > 0)
            {
                int idx = comboCh1Channels.Items.IndexOf(configuration.channel1);
                if (idx > -1) comboCh1Channels.SelectedIndex = idx;
            }

            comboCh2Channels.Items.AddRange(daqReader.GetPhysicalChannels());
            if (comboCh2Channels.Items.Count > 0)
            {
                int idx = comboCh2Channels.Items.IndexOf(configuration.channel2);
                if (idx > -1) comboCh2Channels.SelectedIndex = idx;
            }

            //register ti event
            ExperimentRunner.EventRunner += new ExperimentRunner.ExperimentRunnerHandler(ExperimentRunner_EventRunner);

        }

        public ParallelPort GetParallelPort()
        {
            return this.parallelPort;
        }
        public Calibration GetCalibration()
        {
            return this.calibrationManager;
        }
        public ExperimentRunner GetExperimentRunner()
        {
            return this.runner;
        }

        public NIDAQMxReader GetExperimentReader()
        {
            return this.daqReader;
        }
        public List<ExperimentItem> GetExperiment()
        {
            return this.items;
        }

        #region External Graph
        /// <summary>
        /// Callback by Runner
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void ExperimentRunner_EventRunner(object o, ExperimentRunner.ExperimentRunnerEventArgs e)
        {
            sample++;
            //push data into buffer
            if ((started) && (e.DataValues != null))
            {
                buffer.Append( e.DataValues[0] + "  " + ((e.DataValues.Length > 1) ? e.DataValues[1] : 0) + "  " + e.Time + "  " + e.CurrentExperimentItem + "  " + e.ExperimentValue + "  " + calibrationManager.ScaleCh1(e.DataValues[0]) + "  " + ((e.DataValues.Length > 1) ? calibrationManager.ScaleCh2(e.DataValues[1]) : 0) + "\n");
                //graph.AddData(e.DataValues[0], ((e.DataValues.Length > 1) ? e.DataValues[1] : 0.0), e.Time, ConvertReferenceToVoltage(e.ExperimentValue));
            }

            if (this.listBox1.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetValueItemCallback d = new SetValueItemCallback(SetCurrentItem);
                this.Invoke
                    (d, new object[] { e.CurrentExperimentItem });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                SetCurrentItem(e.CurrentExperimentItem);
            }

            if ((e.DataValues != null) && (sample % refreshSample == 0))
            {
                sample = 0;
                if ((this.textBoxCh1.InvokeRequired) || (this.textBoxCh2.InvokeRequired))
                {
                    // It's on a different thread, so use Invoke.
                    SetValueDataCallback d = new SetValueDataCallback(SetDataValues);
                    this.Invoke
                        (d, new object[] { e.Time, e.DataValues, e.ExperimentValue });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    SetDataValues(e.Time, e.DataValues, e.ExperimentValue);
                }
            }
        }

        /// <summary>
        /// Utility function scale reference to voltage
        /// </summary>
        /// <param name="referenceValue"></param>
        /// <returns></returns>
        private double ConvertReferenceToVoltage(int referenceValue)
        {
            double refVoltage = (double)referenceValue / (double)ParallelPort.MAX_VALUE;
            double dataVoltageUp = daqReader.MaxValueVoltage1;
            double dataVoltageBottom = Math.Abs(daqReader.MinValueVoltage1);

            if (checkCh2.Checked)
            {
                dataVoltageUp = Math.Max(dataVoltageUp, daqReader.MaxValueVoltage2);
                dataVoltageBottom = Math.Min(dataVoltageBottom, daqReader.MinValueVoltage2);
            }
            dataVoltageBottom = Math.Abs(dataVoltageBottom);

            if (refVoltage > 0)
            {

                return refVoltage * dataVoltageUp;
            }
            else
            {
                return refVoltage * dataVoltageBottom;
            }
        }
        #endregion

        private double previousData0 = 0;
        private double previousData1 = 0;

        /// <summary>
        /// data Set
        /// </summary>
        /// <param name="time"></param>
        /// <param name="datas"></param>
        private void SetDataValues(double time, double[] datas, double reference)
        {
            try
            {
                if (datas.Length > 1)
                {
                    textBoxCh1.Text = "" + datas[0];
                    textBoxCh2.Text = "" + datas[1];
                    progressBar1.Value = (int)Math.Round(10.0 * Math.Abs(datas[0] - previousData0) / Math.Max(Math.Abs(datas[0]), Math.Abs(previousData0)));
                    progressBar2.Value = (int)Math.Round(10.0 * Math.Abs(datas[1] - previousData1) / Math.Max(Math.Abs(datas[1]), Math.Abs(previousData1)));
                    this.zedChartControlForm1.AddValue(time, calibrationManager.ScaleCh1(datas[0]), calibrationManager.ScaleCh2(datas[1]));
                    if (this.calibrationManager.Ready())
                    {
                        this.zedChartControlForm1.AddReference(time, reference);
                    }
                    previousData0 = datas[0];
                    previousData1 = datas[1];
                }
                else if (datas.Length > 0)
                {
                    textBoxCh1.Text = "" + datas[0];
                    progressBar1.Value = (int)Math.Round(10.0 * Math.Abs(datas[0] - previousData0) / Math.Max(Math.Abs(datas[0]), Math.Abs(previousData1)));
                    if (this.calibrationManager.Ready())
                    {
                        this.zedChartControlForm1.AddReference(time, reference);
                    }
                    this.zedChartControlForm1.AddValue(time, calibrationManager.ScaleCh1(datas[0]));
                    previousData0 = datas[0];
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private void SetCurrentItem(int idx)
        {
            if (idx == ExperimentRunner.ExperimentRunnerEventArgs.EXPERIMENT_START)
            {
                RefreshToolStrip();
                started = true;
                sample = 0;
                startTime = DateTime.Now;
                graph.Reset();
                buffer = new StringBuilder("");
            }
            else if (idx == ExperimentRunner.ExperimentRunnerEventArgs.EXPERIMENT_STOP)
            {
                RefreshToolStrip();
                started = false;
                graph.CreateGraph();
            }
            else if (idx >= 0)
            {
                this.listBox1.SelectedIndex = idx;
                this.logOnStatus("item " + idx, (int)Math.Round( 100 * ((double)idx+1) / (double)(this.listBox1.Items.Count)));
            }

        }

        #region SingleControllerEvent

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.parallelPort.LedBar = checkBoxLedBar.Checked;
            this.numericUpDown1.Value = trackBar1.Value;
            this.runner.CurrentExperimentValue = trackBar1.Value;
            parallelPort.Show(trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.parallelPort.LedBar = checkBoxLedBar.Checked;
            this.runner.CurrentExperimentValue = trackBar1.Value;
            parallelPort.Show(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zedChartControlForm1.Reset();
            parallelPort.Reset();
            calibrationManager.Reset();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.trackBar1.Value = (int)numericUpDown1.Value;
            }
            catch (Exception exc)
            {
                EVALab.Util.Box.ExceptionForm.Show(this, "Value is not in range", exc);
            }
        }

        private void comboCh1Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentChannel = (string)comboCh1Channels.SelectedItem;
            if (currentChannel.Equals(daqReader.CurrentChannel2))
            {
                MessageBox.Show("You cannot set the same ch1 end ch2 physical channel");
                comboCh1Channels.SelectedIndex = 0;
            }
            else
            {
                daqReader.CurrentChannel1 = (string)comboCh1Channels.SelectedItem;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parallelPort.Set(trackBar1.Value);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            ExperimentReader reader = new ExperimentReader();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Experiment (*.xexp)|*.xexp|All files (*.*)|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                items = reader.ParseXML(openFile.FileName);
                RefreshList();
                toolStripButton3.Enabled = (items != null) && (items.Count > 0);
            }
        }

        private void RefreshList()
        {
            this.listBox1.Items.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                ExperimentItem item = items[i];//.ElementAt(i);
                this.listBox1.Items.Add(item.Name + "\t" + item.Duration + "(ms)");
            }
        }

        private void RefreshToolStrip()
        {
            toolStripButtonCalibration.Enabled = (runner.Status == EvaLab.EOG.ExperimentRunner.ExperimentRunnerEventArgs.EXPERIMENT_STOP);
            toolStripButton3.Enabled = (items != null) && (items.Count > 0) && (runner.Status == EvaLab.EOG.ExperimentRunner.ExperimentRunnerEventArgs.EXPERIMENT_STOP);
            toolStripButton4.Enabled = (runner.Status == EvaLab.EOG.ExperimentRunner.ExperimentRunnerEventArgs.EXPERIMENT_START);
            buttonCalibrateLeft.Enabled = !toolStripButtonCalibration.Enabled;
            buttonCalibrateRight.Enabled = !toolStripButtonCalibration.Enabled;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            runner.Stop();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }



        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            daqReader.MaxValueVoltage1 = (double)numericUpDownCh1MaxV.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            daqReader.MinValueVoltage1 = (double)numericUpDownCh1MinV.Value;
        }

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                zedChartControlForm1.Reset();
                runner.Init(this);
                Cursor.Current = Cursors.WaitCursor;
                runner.Start();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {

                ExceptionForm.Show(this, "Unable to start Engine", ex);
            }
        }

        /// <summary>
        /// Calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCalibration_Click(object sender, EventArgs e)
        {
            try{
                zedChartControlForm1.Reset();
                runner.InitForTest(this);
                Cursor.Current = Cursors.WaitCursor;
                runner.Start();
                Cursor.Current = Cursors.Default;
             }
            catch (Exception ex)
            {
                
                ExceptionForm.Show(this, "Unable to start Engine", ex);
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            runner.Init(this);
            runner.DoData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCh2.Checked)
            {
                daqReader.CurrentChannel2 = (string)comboCh2Channels.SelectedItem;
            }
            else
            {
                daqReader.CurrentChannel2 = null;
            }
            comboCh2Channels.Enabled = checkCh2.Checked;
            numericUpDownCh2MaxV.Enabled = checkCh2.Checked;
            numericUpDownCh2MinV.Enabled = checkCh2.Checked;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            daqReader.MaxValueVoltage2 = (double)numericUpDownCh2MaxV.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            daqReader.MinValueVoltage2 = (double)numericUpDownCh2MinV.Value;
        }

        private void comboCh2Channels_SelectedIndexChanged(object sender, EventArgs e)
        {

            string currentChannel = (string)comboCh2Channels.SelectedItem;
            if (currentChannel.Equals(daqReader.CurrentChannel1))
            {
                EVALab.Util.Box.ExceptionForm.Show(this, "You cannot set the same ch1 end ch2 physical channel", null);
                comboCh2Channels.SelectedIndex = 0;
            }
            else
            {
                daqReader.CurrentChannel2 = (string)comboCh2Channels.SelectedItem;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            runner.Frequency = (int)numericUpDownFrequency.Value;
        }

        private void toolStripButtonSaveConf_Click(object sender, EventArgs e)
        {
            configuration.portAddress = parallelPort.BaseAddress;

            configuration.maxVoltageCh1 = (int)numericUpDownCh1MaxV.Value;
            configuration.minVoltageCh1 = (int)numericUpDownCh1MinV.Value;
            configuration.maxVoltageCh2 = (int)numericUpDownCh2MaxV.Value;
            configuration.minVoltageCh2 = (int)numericUpDownCh2MinV.Value;
            configuration.frequency = (int)numericUpDownFrequency.Value;

            //InitializeComponent DAQ
            configuration.channel1 = "" + comboCh1Channels.SelectedItem;

            configuration.channel2 = "" + comboCh2Channels.SelectedItem;

            configuration.Save();
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                SubjectForm subjFrm = new SubjectForm("EOG" + (this.checkBoxLedBar.Checked ? "STD" : "CUSTOM"));
                if (subjFrm.ShowDialog(this) != DialogResult.Cancel)
                {
                    ExperimentSettings expSettings = subjFrm.ExperimentSettings;

                    string filenameRaw = "DATA.txt";
                    string filename = "EXPERIMENT.xml";

                    this.Cursor = Cursors.WaitCursor;
                    string path = subjFrm.SelectedPath;

                    path += "\\" + expSettings.Id;

                    logOnStatus("Making directory " + path, 0);
                    Directory.CreateDirectory(@"" + path);
                    logOnStatus("Directory " + path + " made", 20);
                    
                    logOnStatus("Saving raw file data " + filenameRaw, 40);
                    StreamWriter sw = new StreamWriter(@"" + path + "\\" + filenameRaw);
                    sw.Write(buffer);
                    sw.Close();
                    logOnStatus("Raw file data " + filenameRaw + " saved", 60);                        

                    string buffer2 = "<experiment>";
                    buffer2 += expSettings.ToXML();
                    buffer2 += calibrationManager.ToXML();
                    buffer2 += "<data><![CDATA[";
                    buffer2 += buffer;
                    buffer2 += "]]></data>";
                    buffer2 += "</experiment>";

                    logOnStatus("Saving raw file data " + filename, 80);
                    sw = new StreamWriter(@"" + path + "\\" + filename);
                    sw.Write(buffer2);
                    sw.Close();

                    logOnStatus("Data saved on" + path, 100);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                ExceptionForm.Show(this, "Check directory", err);
            }
        }


        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            graph.ShowDialog();
        }
        #endregion

        #region Calibration
        private void buttonCalibrateLeft_Click(object sender, EventArgs e)
        {
            textBoxMinVoltageCh1.Text = textBoxCh1.Text;
            textBoxMinVoltageCh2.Text = textBoxCh2.Text;

            calibrationManager.SetMin((double)(numericUpDown1.Value), SafeParse(textBoxMinVoltageCh1.Text, -10), SafeParse(textBoxMinVoltageCh2.Text, -10));
        }

        private void buttonCalibrateRight_Click(object sender, EventArgs e)
        {
            textBoxMaxVoltageCh1.Text = textBoxCh1.Text;
            textBoxMaxVoltageCh2.Text = textBoxCh2.Text;

            calibrationManager.SetMax((double)(numericUpDown1.Value), SafeParse(textBoxMaxVoltageCh1.Text, 10), SafeParse(textBoxMaxVoltageCh2.Text, 10));
        }

        private double SafeParse(string value, double defaultValue) 
        {
            try
            {
                return Double.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private void listBoxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = listBoxProfile.SelectedItem.ToString();
            int valueIntDirect = 0;
            int valueIntLedBar = 0;
            if (value.Equals("Right"))
            {
                valueIntDirect = 1;
                valueIntLedBar = 200;
            }
            else if (value.Equals("Left"))
            {
                valueIntDirect = 64;
                valueIntLedBar = -200;
            }
            else if (value.Equals("Middle Left"))
            {
                valueIntDirect = 16;
                valueIntLedBar = -100;
            }
            else if (value.Equals("Middle Right"))
            {
                valueIntDirect = 4;
                valueIntLedBar = 100;
            }
            else if (value.Equals("Center"))
            {
                valueIntDirect = -255;
                valueIntLedBar = 1;
            }
            if (checkBoxLedBar.Checked)
            {
                this.numericUpDown1.Value = valueIntLedBar;
                parallelPort.Show(valueIntLedBar);
            }
            else
            {
                this.numericUpDown1.Value = valueIntDirect;
                parallelPort.Show(valueIntDirect);
            }
        }
        #endregion

        #region Status Log

        public void logOnStatus(string message, int value)
        {
            this.toolStripStatusLabel.Text = message;
            this.toolStripProgressBar.Value = value;
        }

        #endregion

        private void toolStripButtonTest_Click(object sender, EventArgs e)
        {
            Test.TestParallelForm.Show(this, this);
        }

        private void checkBoxLedBar_CheckedChanged(object sender, EventArgs e)
        {
            this.parallelPort.LedBar = checkBoxLedBar.Checked;
        }

        private void numericRefresh_ValueChanged(object sender, EventArgs e)
        {
            refreshSample = numericRefresh.Value;
        }
    }
}
