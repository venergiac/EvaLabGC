using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EVALabGC.Object;
using System.IO;
using EVALab.Util;
using EVALab.Util.Input;
using System.Diagnostics;
using EVALabGC.ASL;
using EVALab.Util.Box;
using EVALab.Util.Meta;
using EVALabGC.Store;
using EVALab.Util.Output;
using EVALab.Util.License;

namespace EVALabGC
{
    public partial class ControlForm : System.Windows.Forms.Form
    {
        private Image image = null;

        private Task currentTask = null;
        private Form1 form = null;

        private License license;

        //private delegate void StopCallback();
        delegate void VoidCallback();
        delegate void SetValueCallback(double value);
        delegate void SetTextCallback(string value);

        protected int counter = 0;

        protected Context context = null;
        protected Joystick joystick = null;


        protected double sampleRefresh = 50;

        delegate void StopCallback();

        public ControlForm()
        {
            InitializeComponent();
            // Obtain the license
            license = LicenseManager.Validate(typeof(FakeApp), this);

            Init();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        protected void Init()
        {
            IniFile file = new IniFile(Application.StartupPath + "\\Config.ini");
            context = new Context(this, file);
            context.Reader.ASLReaderEvent += new EVALabGC.ASL.Reader.ASLReaderEventHandler(Reader_ASLReaderEvent);
            context.Reader.ASLDataEvent += new EVALabGC.ASL.Reader.ASLDataEventHandler(Reader_ASLDataEvent);
            sampleRefresh = context.SampleRefresh;
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {

            this.toolStripStatusLabelVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

#if DEBUG
            this.toolStripStatusLabelVersion.Text = this.toolStripStatusLabelVersion.Text + " DEBUG (BAD IDEA)";
#endif

            //Update screen
            toolStripComboBoxScreen.SelectedIndex = 0;

            if (Screen.AllScreens.Length > 1)
            {
                toolStripComboBoxScreen.Items.Add("Screen 2");
            }

            //adpater
            int idxAdpater = 0;
            foreach (string adapter in Graphic.GetAdapterList())
            {
                ToolStripItem item = adpatersToolStripMenuItem.DropDownItems.Add(adapter);
                item.Click += new EventHandler(item_Click);
                item.Tag = idxAdpater++;
            }

            //add Reference to Calibration and 
            this.treeMenu.Nodes[1].Nodes[0].Tag = TaskManager.Instance().MakeCalibrationTask();
            this.treeMenu.Nodes[1].Nodes[1].Tag = TaskManager.Instance().MakeValidationTask();
            this.treeMenu.Nodes[1].Nodes[2].Tag = TaskManager.Instance().MakeTestTask();
        }


        #region Event
        double currentX = 0;
        double currentY = 0;
        double currentTime = 0;
        long currentItemId = -1;
        long previousItemId = -1;
        double previousTime = -1;
        protected void Reader_ASLDataEvent(object sender, ASLDataEventArgs e)
        {
            currentX = e.Data.X;
            currentY = e.Data.Y;
            currentTime = e.Data.Time;
            currentItemId = e.Data.StimulusId;

            //push data into buffer
            if (Store.StorageManager.Instance().Ready)
            {
                if (joystick != null)
                {
                    joystick.UpdateStatus();
                    Store.StorageManager.Instance().Add(e.Data.X, e.Data.Y, e.Data.Pupil, e.Data.Time, e.Data.StimulusId, joystick.GetJoystickXValue(), joystick.GetJoystickYValue(), joystick.GetJoystickButtonsValue());
                }
                else
                {
                    Store.StorageManager.Instance().Add(e.Data.X, e.Data.Y, e.Data.Pupil, e.Data.Time, e.Data.StimulusId, 0, 0, 0);
                }
            }

            //push data into preview
            counter++;
            if ((counter % sampleRefresh == 0) && (previewToolStripMenuItem.Checked))
            {
                counter = 0;
                UpdatePreviewPosition();
                if (currentItemId != previousItemId)
                {
                    UpdatePreviewStimulus();
                    previousItemId = currentItemId;
                }
            }

        }

        protected void Reader_ASLReaderEvent(object sender, ASLReaderEventArgs e)
        {

            if (e.Status == Reader.ReaderStatus.Stopped)
            {
                SafeStop();
                SafeUpdateCommand();
                AcceptCurrentExperiment();
                SafeRefreshPanelView();
                SafeIconize(false, true);
                SafeLog("STOP Request by task or by user");
                SafeUpdateExperimentList();
                StopJoystick();
                UpdatePreviewPosition();
                UpdatePreviewStimulus();
                previousItemId = -1;
                previousTime = 0;
            }
            else if (e.Status == Reader.ReaderStatus.Started)
            {
                SafeUpdateCommand();
                SafeIconize(true, true);
                SafeLog("Reader STARTED");
            }
        }
        #endregion

        #region Tree Management
        private void openExperimentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogExp.ShowDialog(this);
        }

        private void openFileDialogExp_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Stream file = openFileDialogExp.OpenFile();
                TaskManager.Instance().AddAndParseFileTask(openFileDialogExp.FileName, file);
                file.Close();
                RefreshTree();
            }
            catch (Exception exc)
            {
                ExceptionForm.Show(this, "Unable to open file", exc);
            }
        }

        /// <summary>
        /// Update tree
        /// </summary>
        private void RefreshTree()
        {
            this.treeMenu.Nodes[0].Nodes.Clear();
            foreach (Task task in TaskManager.Instance().Tasks)
            {
                TreeNode node = this.treeMenu.Nodes[0].Nodes.Add(task.Id, task.Name);
                node.Tag = task;
            }
            SetCurrentTask(null);
        }

        private void treeMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name.Equals("Joystick"))
            {
            }
        }

        private void UpdateJoystickNode()
        {
            TreeNode handNode = this.treeMenu.Nodes[2];
            handNode.Nodes.Clear();
            if (joystick != null)
            {
                TreeNode jnode = handNode.Nodes.Add(joystick.Name);
                jnode.ImageIndex = 238;
                handNode.Expand();
            }
        }

        private void treeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeMenu.SelectedNode;
            Debug.WriteLine("SET " + node.Name);
            if ((node != null) && (node.Tag is Task))
            {
                SetCurrentTask((Task)node.Tag);
                Debug.WriteLine("SET CURRENT TASK" + node.Name);
            }
            else
            {
                SetCurrentTask(null);
            }
        }

        /// <summary>
        /// Set the current task
        /// </summary>
        /// <param name="task"></param>
        private void SetCurrentTask(Task task)
        {
            this.propertyGridTask.SelectedObject = task;
            this.propertyGridTask.Refresh();

            currentTask = task;
            UpdateCommand();
        }

        private void SafeUpdateCommand()
        {
            this.Invoke(new VoidCallback(UpdateCommand));
        }

        private void UpdateCommand()
        {
            this.Focus();
            if ((currentTask != null) && (currentTask.Runnable))
            {
                toolStripButtonStart.Enabled = !(context.Reader.Status == Reader.ReaderStatus.Started);
                toolStripButtonStop.Enabled = (context.Reader.Status == Reader.ReaderStatus.Started);
            }
            else
            {
                toolStripButtonStart.Enabled = false;
                toolStripButtonStop.Enabled = false;
            }
            this.treeMenu.Enabled = !(context.Reader.Status == Reader.ReaderStatus.Started);
            this.Refresh();
        }

        #endregion

        #region Data

        public void AcceptCurrentExperiment()
        {
            if (currentTask != null)
            {
                Store.StorageManager.Instance().AcceptCurrentBuffer(currentTask.Form.Experiment);
            }
        }

        public void SafeUpdateExperimentList()
        {
            this.Invoke(new VoidCallback(UpdateExperimentList));
        }

        public void UpdateExperimentList()
        {
            listViewResult.Clear();
            foreach (Experiment experiment in Store.StorageManager.Instance().Experiments)
            {
                ListViewItem item = listViewResult.Items.Add(experiment.Id, 356);
                item.Tag = experiment;
            }
            listViewResult.Refresh();
        }

        private void listViewResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewResult.SelectedItems.Count > 0)
            {
                propertyGrid1.SelectedObject = listViewResult.SelectedItems[0].Tag;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewResult.SelectedItems.Count > 0)
            {
                Store.StorageManager.Instance().RemoveExperiment((Experiment)listViewResult.SelectedItems[0].Tag);
                UpdateExperimentList();
                propertyGrid1.SelectedObject = null;
            }
        }


        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel != MessageBox.Show(this, "All experiments will be deleted", "Delete all", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Store.StorageManager.Instance().Clear();
                UpdateExperimentList();
            }

        }

        #endregion

        #region Start & Command

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if ((form != null) && !(form.IsDisposed))
            {
                StopJoystick();
                form.Dispose();
            }
        }

        /// <summary>
        /// Only show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.currentTask != null)
            {
                ShowFormByItem();
            }
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            if (this.currentTask != null)
            {
                //alert
                if (previewToolStripMenuItem.Checked)
                {
                    this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Preview is enabled. Please for Best performance Uncheck it and make Gaze Contingent Iconized.", ToolTipIcon.Warning);
                }
                //start
                ShowFormByItem();
                if (currentTask.StoreData)
                {
                    Store.StorageManager.Instance().Ready = true;
                    Store.StorageManager.Instance().Init(this.context);
                }
                else
                {
                    Store.StorageManager.Instance().Ready = false;
                }
                Start();
                UpdateCommand();
            }
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop();
            UpdateCommand();
        }

        private void ShowFormByItem()
        {
            string item = toolStripComboBoxScreen.SelectedItem.ToString();
            if (item.Equals("Screen 2"))
            {
                ShowForm(true, 1);
            }
            else if (item.Equals("Screen 1"))
            {
                ShowForm(true, 0);
            }
            else
            {
                ShowForm(false, 1);
            }
        }

        private void ShowForm(bool fullScreen, int idxScreen)
        {
            if (form != null)
            {
                form.Close();
            }

            if (currentTask == null) return;
            form = currentTask.Form;

            form.Init(context);
            if (fullScreen)
            {
                form.FullScreen(idxScreen);
            }
            else
            {
                form.FormScreen();
            }

            this.SetPreviewImage(form.GetPreviewImage());
            RefreshPanelView();

        }

        #endregion

        #region Log
        public void Log(string message)
        {
            this.listBoxTask.Items.Add(message);
            this.listBoxTask.SelectedIndex = this.listBoxTask.Items.Count - 1;
        }

        public void SafeLog(string message)
        {
            //plot position
            if (listBoxTask.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Log);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                Log(message);
            }
        }

        private void SafeProgressBarCompletition(int value)
        {
            //plot position
            if (progressBarCompletition.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetProgressBarCompletition);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                SetProgressBarCompletition(value);
            }
        }

        private void SetProgressBarCompletition(double value)
        {
            if ((value < 0) || (value > 100)) return;
            this.progressBarCompletition.Value = (int)value;
        }

        public void Log(string message, int completition)
        {
            this.listBoxTask.Items.Add(message + " " + completition + "%");
            SetProgressBarCompletition(completition);
        }
        #endregion

        #region Preview

        private void UpdatePreviewStimulus()
        {
            //request current stimulus
            SafeLog(currentTask.Form.GetStimulusName(currentItemId));
            //plot position
            SafeRefreshPanelView();
        }

        public void UpdatePreviewPosition()
        {
            SafeProgressBarCompletition(currentTask.Form.GetCompletition());
            SafeProgressBarAcquisition((int)(100.0 * (1000.0 / ((currentTime - previousTime) / sampleRefresh)) / 250.0));
            previousTime = currentTime;
            SafeRefreshPanelView();
        }
        #region ProgressBarAcquisition
        private void SafeProgressBarAcquisition(int value)
        {
            //plot position
            if (toolStripProgressBarAcquisition.Owner.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetProgressBarAcquisition);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                SetProgressBarAcquisition(value);
            }
        }

        private void SetProgressBarAcquisition(double value)
        {
            if ((value < toolStripProgressBarAcquisition.Minimum) || (value > toolStripProgressBarAcquisition.Maximum)) return;
            toolStripProgressBarAcquisition.Value = (int)value;
        }
        #endregion

        #region PanelView
        private void SafeRefreshPanelView()
        {
            //plot position
            if (panelView.InvokeRequired)
            {
                VoidCallback d = new VoidCallback(RefreshPanelView);
                this.Invoke(d);
            }
            else
            {
                RefreshPanelView();
            }
        }

        private void RefreshPanelView()
        {
            panelView.Refresh();
        }
        #endregion

        private void SetPreviewImage(Image image)
        {
            this.image = image;
            SafeRefreshPanelView();
        }

        private SolidBrush defaultBrush = new SolidBrush(Color.Gray);
        private SolidBrush activeBrush = new SolidBrush(Color.Red);
        private Pen defaultPen = new Pen(Color.Gray);

        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            Graphics myGraphics = e.Graphics;
            if ((image == null) && (currentTask != null))
            {

                image = currentTask.Form.GetPreviewImage();
                //Debug.WriteLine("Update Image Preview imge is " + image);
            }
            if (image != null)
            {
                Debug.WriteLine("Image Preview OK " + image);
                myGraphics.DrawImage(image, new RectangleF(0, 0,
                    this.panelView.Width, this.panelView.Height));
            }
            myGraphics.DrawRectangle(new Pen(Color.Red), (int)(panelView.Width * currentX / (double)Context.WIDTH - 3), (int)(panelView.Height * currentY / (double)Context.HEIGHT - 3), 6, 6);

            int jX = 0;
            int jY = 0;
            if (joystick != null)
            {
                joystick.UpdateStatus();
                if (joystick.GetJoystickXValue() > 100)
                {
                    jX = 5;
                }
                else if (joystick.GetJoystickXValue() < 100)
                {
                    jX = -5;
                }
                if (joystick.GetJoystickYValue() > 100)
                {
                    jY = 5;
                }
                else if (joystick.GetJoystickYValue() < 100)
                {
                    jY = -5;
                }
                myGraphics.FillEllipse(((joystick.GetJoystickButtonsValue() > 0) ? activeBrush : defaultBrush), panelView.Size.Width - 20 - jX, panelView.Size.Height - 20 - jY, 10, 10);

                //print joystick
                myGraphics.DrawEllipse(defaultPen, panelView.Size.Width - 25, panelView.Size.Height - 25, 20, 20);
            }
            else
            {
                myGraphics.FillEllipse(defaultBrush, panelView.Size.Width - 20 - jX, panelView.Size.Height - 20 - jY, 10, 10);
            }
        }

        #endregion



        ToolStripMenuItem lastItemSelected = null;
        void item_Click(object sender, EventArgs e)
        {
            if (lastItemSelected != null)
            {
                lastItemSelected.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            lastItemSelected = ((ToolStripMenuItem)sender);
            this.context.IdxAdapter = ((int)((ToolStripMenuItem)sender).Tag);
            this.toolStripStatusLabelRefreshRate.Text = "refresh rate set to " + Graphic.GetRefreshRate(this.context.IdxAdapter) + "Hz";
        }


        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentTask == null) return;
                SubjectForm subjFrm = new SubjectForm(currentTask.Id);
                ExperimentSettings expSettings = subjFrm.ExperimentSettings;
                if (subjFrm.ShowDialog() != DialogResult.Cancel)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string path = subjFrm.SelectedPath;
                    StorageManager.Instance().Store(path, expSettings, context);
                    UpdateExperimentList();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                ExceptionForm.Show(this, "Check directory", err);
            }
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void aboutEvaLabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog(this);
        }

        #region Icon
        private void SafeIconize(bool iconize, bool useUserRequest)
        {
            if ((useUserRequest) && !(iconizedAfterStartToolStripMenuItem.Checked)) return;
            if (this.InvokeRequired)
            {
                if (iconize) this.Invoke(new VoidCallback(HideAsIcon));
                else this.Invoke(new VoidCallback(ShowFromIcon));
            }
            else
            {
                if (iconize) HideAsIcon();
                else ShowFromIcon();
            }
        }

        private void ShowFromIcon()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void HideAsIcon()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void iconizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideAsIcon();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFromIcon();
        }

        private void ControlForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Gaze Contingent has been iconized (GOOD IDEA). Double click to restore it.", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        private void panelView_MouseClick(object sender, MouseEventArgs e)
        {
            panelView.Refresh();
        }

        private void ControlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Dispose();
        }

        private void toolStripButtonReloadTasks_Click(object sender, EventArgs e)
        {
            TaskManager.Instance().ReloadTasks();
            RefreshTree();
        }


        #region Joystick

        protected void StartJoystick(System.Windows.Forms.Form frm)
        {
            if (this.joystick != null) StopJoystick();
            if (!(inputDeviceToolStripMenuItem.Checked)) return;

            Log("Starting joystick");
            //look for joystick
            Joystick joystick = new Joystick(frm.Handle, context.XidPort, context.XidBaud);
            string[] joys = joystick.FindJoysticks();
            string joyName = null;
            for (int i = 0; (joys != null) && (i < joys.Length); i++)
            {
                if (joys[i].ToLower().IndexOf("xbox") >= 0)
                {
                    Log("Found joystick " + joys[i] + " maybe is virtual");
                }
                else if (joys[i].ToLower().IndexOf("no game") >= 0)
                {
                }
                else
                {
                    Log("Found joystick " + joys[i]);
                    joyName = joys[i];
                }
            }
            if (joyName != null)
            {
                this.joystick = joystick;
                this.joystick.AcquireJoystick(joyName);
                Log("Acquired joystick " + joyName);
            }
            else
            {
                Log("Joystick not found");
                joystick.ReleaseJoystick();
                joystick = null;
            }
            UpdateJoystickNode();            
        }

        protected void StopJoystick()
        {
            
            if (joystick != null)
            {
                Log("Stopping joystick");
                joystick.ReleaseJoystick();
                joystick = null;
            }
            
            UpdateJoystickNode();
        }


        public Joystick Joystick
        {
            get { return joystick; }
            set { joystick = value; }
        }

        private void inputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(inputDeviceToolStripMenuItem.Checked))
            {
                StopJoystick();
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Input devices will be ignored.", ToolTipIcon.Warning);
            }
            else
            {
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Input devices will be acquired.", ToolTipIcon.Info);
            }
        }

        #endregion
        #region buffer data save

        public void Reset()
        {
            Store.StorageManager.Instance().Reset();
        }

        #endregion

        #region Start & Stop
        protected virtual void Start()
        {
            try
            {
                if (this.form != null)
                {
                    StartJoystick(this.form);
                }
                else
                {
                    StartJoystick(this);
                } 
                context.Reader.Start();
            }
            catch (Exception exc)
            {
                StopJoystick();
                EVALab.Util.Box.ExceptionForm.Show(this, "Error on opening reader", exc);
                return;
            }
            Reset();
        }

        protected virtual void SafeStop()
        {
            this.Invoke(new StopCallback(Stop));
        }

        protected virtual void Stop()
        {
            StopJoystick();
            if (context.Reader.Status == Reader.ReaderStatus.Started)
            {
                context.Reader.Stop();
            }
        }
        #endregion

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultToolStripMenuItem.Checked = false;
            defaultX2ToolStripMenuItem.Checked = false;
            defaultX3ToolStripMenuItem.Checked = false;

            if (sender == defaultToolStripMenuItem)
            {
                defaultToolStripMenuItem.Checked = true;
                defaultX2ToolStripMenuItem.Checked = false;
                defaultX3ToolStripMenuItem.Checked = false;
                this.sampleRefresh = context.SampleRefresh;
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Refresh rate was set to suggested value.", ToolTipIcon.Info);
            }
            else if (sender == defaultX2ToolStripMenuItem)
            {
                defaultX2ToolStripMenuItem.Checked = true;
                defaultToolStripMenuItem.Checked = false;
                defaultX3ToolStripMenuItem.Checked = false;
                this.sampleRefresh = context.SampleRefresh / 2;
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Refresh rate was set to high value.", ToolTipIcon.Warning);
            }
            else if (sender == defaultX3ToolStripMenuItem)
            {
                defaultX2ToolStripMenuItem.Checked = true;
                defaultX2ToolStripMenuItem.Checked = false;
                defaultToolStripMenuItem.Checked = false;
                this.sampleRefresh = context.SampleRefresh / 3;
                this.notifyIcon1.ShowBalloonTip(3000, "Gaze Contingent", "Refresh rate was set to highest value.", ToolTipIcon.Warning);
            }
        }
    }
}
