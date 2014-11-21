using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EVALab.Util;

using Microsoft.Win32;
using EVALab.Util.Input;
using System.Diagnostics;
using EVALabGC.ASL;


namespace EVALabGC
{
    public partial class ControlForm : Controller
    {

        private Form1 form = new CalibrateForm();
        private DateTime previousTime = DateTime.Now;
        private int idxToSend = 0;
        private double maxToSend = 100.0;
        delegate void SetValueCallback(int value);
        delegate void SetTextCallback(string value);
        delegate void StopCallback();
        delegate void EnableStartStopSaveButtonCallback(bool value);

        private Random rnd = new Random();


        private Image image = null;

        public ControlForm()
        {
            InitializeComponent();
            Init();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (form != null)
            { 
                form.Close();
                form = null;
            }
            this.toolStripClose.Enabled = form != null;
            this.toolStripButton5.Enabled = form == null;
            this.toolStripDropDownButton1.Enabled = form == null;

        }

        private void ShowForm(bool fullScreen, int idxScreen)
        {
            if ((form != null) && !(form.IsDisposed))
            {
                form.Dispose();
            }

            if (toolStripGC.Checked)
            {
                form = new GCForm();
            }
            else if (toolStripCalibration.Checked)
            {
                form = new CalibrateForm();
            }
            else if (toolStripROI.Checked)
            {
                form = new ROI.Form();
            }
            else if (toolStripStarTask.Checked)
            {
                form = new Theeuwes.TForm();
            }
            

            form.Show();
            
            if (fullScreen)
            {
                form.FullScreen(idxScreen);
            }
            form.Init(context);
            this.SetPreviewImage(form.GetPreviewImage());
            
            this.toolStripButton5.Enabled = form == null;
        }

        /// <summary>
        


        private void reader_ASLEvent(object sender, ASL.ASLDataEventArgs e)
        {
            //push data into preview
            counter++;
            if (counter % 50 == 0)
            {
                counter=0;
                Graphics myGraphics = this.panel1.CreateGraphics();
                if (image == null)
                {
                    myGraphics.Clear(this.panel1.BackColor);
                }
                else
                {
                    myGraphics.DrawImage(image, new RectangleF(0, 0,
                     this.panel1.Width, this.panel1.Height));
                }
                myGraphics.DrawLine(new Pen(Color.Red), e.Data.X, e.Data.Y - 5, e.Data.X, e.Data.Y + 5);
                myGraphics.DrawLine(new Pen(Color.Red), e.Data.X - 5, e.Data.Y, e.Data.X + 5, e.Data.Y);
                myGraphics.Dispose();
            }
            //push data into buffer
            if (Store.StorageManager.Instance().Ready)
            {
                Store.StorageManager.Instance().Add(e.Data.X, e.Data.Y, e.Data.Pupil, e.Data.Time, e.Data.StimulusId, GetJoystickXValue(), GetJoystickYValue(), GetJoystickButtonsValue());
            }

            //save data into text
            if (idxToSend++ <= maxToSend) return;
            idxToSend = 0;
            if (this.textBoxX.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetValueCallback d = new SetValueCallback(SetTextX);
                this.Invoke
                    (d, new object[] {e.Data.X });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                 SetTextX(e.Data.X);
            }
            if (this.textBoxY.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetValueCallback d = new SetValueCallback(SetTextY);
                this.Invoke
                    (d, new object[] {e.Data.Y });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                SetTextY(e.Data.Y);
            }
            if (this.textBoxT.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetValueCallback d = new SetValueCallback(SetTextT);
                this.Invoke
                    (d, new object[] { (int)(maxToSend * 1000.0 / (double)(DateTime.Now - previousTime).TotalMilliseconds) });
            }
            else
            {
                // It's on the same thread, no need for Invoke
               SetTextT((int)(maxToSend * 1000.0 / (double)(DateTime.Now - previousTime).TotalMilliseconds));
            }

            previousTime = DateTime.Now;
        }

        public void SetTextT(int value)
        {
            this.textBoxT.Text = "" + value;
        }

        public void SetTextX(int value)
        {
            this.textBoxX.Text = "" + value;
        }
        public void SetTextY(int value)
        {
            this.textBoxY.Text = "" + value;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Start();
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Stop();
            EnableStartStopSaveButton(true);
        }

        public void EnableStartStopSaveButton(bool value)
        {
            toolStripButton1.Enabled = value;
            toolStripButton2.Enabled = !value;
            toolStripSaveData.Enabled = value;
        }

        public void AddLog(string message)
        {
            this.listBox1.Items.Add(message);
        }


        public override void Log(string message)
        {
            if (this.listBox1.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback d = new SetTextCallback(AddLog);
                this.Invoke
                    (d, new object[] { message });
            }
            else
            {
                AddLog(message);

            }
            
        }

       

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            idxToSend = (int)maxToSend;
            context.Reader.DoRequest();
            this.Log("Request ASL Forced");
        } 

        private void SetPreviewImage(Image image)
        {
            if (image == null) return;
            this.image = image;
            this.panel1.Refresh();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                //////context.ConfigFile = fileDialog.FileName;
                this.Log("Opening file " + fileDialog.FileName);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                form.BackColor = colorDialog.Color;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileName = "GC_" + DateTime.Now.Date.ToShortDateString().Replace('/','-') + "_soggetto.txt";
            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.Cursor = Cursors.WaitCursor;
                StreamWriter sw = new StreamWriter(fileDialog.FileName);
                sw.Write(Store.StorageManager.Instance().Buffer);
                sw.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics myGraphics = this.panel1.CreateGraphics();
            if (image != null)
            {
                myGraphics.DrawImage(image, new RectangleF(0, 0,
                    this.panel1.Width, this.panel1.Height));
            }
        }

        /// <summary>
        /// Calibration Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripGC.Checked = !this.toolStripCalibration.Checked;
            toolStripROI.Checked = !this.toolStripCalibration.Checked;
            toolStripStarTask.Checked = !this.toolStripCalibration.Checked;
            
            toolStripSaveData.Enabled = toolStripGC.Checked; 
            this.Log("(!): Experiment changed please reopen configuration.");
        }

        /// <summary>
        /// GC Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripCalibration.Checked = !this.toolStripGC.Checked;
            toolStripROI.Checked = !this.toolStripGC.Checked;
            toolStripStarTask.Checked = !this.toolStripGC.Checked;
            if (this.toolStripGC.Checked)
            {
                Store.StorageManager.Instance().Init(this.context);
            }
            toolStripSaveData.Enabled = toolStripGC.Checked;
            this.Log("(!): Experiment changed please reopen configuration.");
        }

        /// <summary>
        /// ROI Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripROI_Click(object sender, EventArgs e)
        {
            toolStripCalibration.Checked = !this.toolStripROI.Checked;
            toolStripGC.Checked = !this.toolStripROI.Checked;
            toolStripStarTask.Checked = !this.toolStripROI.Checked;
            if (this.toolStripROI.Checked)
            {
                Store.StorageManager.Instance().Init(this.context);
            }
            toolStripSaveData.Enabled = toolStripROI.Checked;
            this.Log("(!): Experiment changed please reopen configuration.");
        }

        /// <summary>
        /// Theeuwes Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void starTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripCalibration.Checked = !this.toolStripStarTask.Checked;
            toolStripGC.Checked = !this.toolStripStarTask.Checked;
            toolStripROI.Checked = !this.toolStripStarTask.Checked;
            if (this.toolStripStarTask.Checked)
            {
                Store.StorageManager.Instance().Init(this.context);
            }
            toolStripSaveData.Enabled = toolStripROI.Checked;
            this.Log("(!): Experiment changed please reopen configuration.");
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
            {
                toolStripMenuItem3.Enabled = true;
                toolStripMenuItem4.Enabled = true;
            }
            else
            {
                toolStripMenuItem3.Enabled = true;
                toolStripMenuItem4.Enabled = false;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //screen 1
            this.ShowForm(true, 0);
            this.toolStripClose.Enabled = form != null;
            this.toolStripDropDownButton1.Enabled = form == null;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //screen 2
            this.ShowForm(true, 1);
            this.toolStripClose.Enabled = form != null;
            this.toolStripDropDownButton1.Enabled = form == null;

        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            this.ShowForm(false, 1);
            this.toolStripClose.Enabled = form != null;
            this.toolStripDropDownButton1.Enabled = form == null;
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show(this);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Test.ControlForm testForm = new Test.ControlForm();
            testForm.ShowDialog(this);
        }

    }
}
