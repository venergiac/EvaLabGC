using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVALab.Analysis;
using EVALab.Analysis.Data;
using EVALabAnalysis.Dialog;
using EVALabAnalysis.Parameter;
using System.Diagnostics;
using EVALabAnalysis.Display;
using EVALab.Util.Box;
using EVALab.Analysis.Saccade;
using EVALabAnalysis.CaseBase;
using EVALab.Analysis.ROI;

namespace EVALabAnalysis
{
    public partial class Form1 : Form
    {

        private AnalysisManager manager = AnalysisManager.Instance;
        private TreeNode dataRootNode = null;
        private TreeNode analysisRootNode = null;
        private TreeNode roiRootNode = null;

        public Form1()
        {
            InitializeComponent();
        }

        #region Init
        private void Form1_Load(object sender, EventArgs e)
        {
            //prepare Tree
            this.treeView.Nodes.Clear();
            dataRootNode = this.treeView.Nodes.Add("Data");
            dataRootNode.ImageIndex = 287;
            dataRootNode.SelectedImageIndex = 287;
            roiRootNode = this.treeView.Nodes.Add("ROIs");
            roiRootNode.ImageIndex = 382;
            roiRootNode.SelectedImageIndex = 382;
            analysisRootNode = this.treeView.Nodes.Add("Cases");
            analysisRootNode.ImageIndex = 140;
            analysisRootNode.SelectedImageIndex = 141;
            //update Tree case
            UpdateTreeCase();
        }
        #endregion

        #region OpenFile

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogEOG.ShowDialog(this);
        }

        private void rawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogRawEOG.ShowDialog(this);
        }

        private void openFileDialogRawEOG_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (!e.Cancel)
                {
                    manager.CreateBinocularTrialFromRawDataFile(openFileDialogEOG.FileName);
                    UpdateTreeDatas();
                }
            }
            catch (Exception exc)
            {
                ExceptionForm.Show(this, "Error on openFileDialogEOG", exc);
            }
        }

        private void dataToolStripMenuItemOpenASL_Click(object sender, EventArgs e)
        {
            openFileDialogASL.ShowDialog(this);
        }

        private void rawDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialogRawASL.ShowDialog(this);
        }

        private void openFileDialogEOG_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (!e.Cancel)
                {
                    manager.CreateBinocularTrialFromDataFile(openFileDialogEOG.FileName);
                    UpdateTreeDatas();
                }
            }
            catch (Exception exc)
            {
                ExceptionForm.Show(this, "Error on openFileDialogEOG", exc);
            }
        }

        private void openFileDialogASL_FileOk_1(object sender, CancelEventArgs e)
        {
            try
            {
                if (!e.Cancel)
                {
                    manager.CreateASLTrialFromDataFile(openFileDialogASL.FileName);
                    UpdateTreeDatas();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show(this, exc.Message);
            }
        }

        private void openFileDialogRawASL_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (!e.Cancel)
                {
                    manager.CreateRawASLTrialFromDataFile(openFileDialogRawASL.FileName);
                    UpdateTreeDatas();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show(this, exc.Message);
            }
        }


        #endregion

        #region Tree manager

        private void AddAnalysis(ref TreeNode node, DataList data)
        {
            if (data.Saccades.List.Count>0)
            {
                TreeNode myNode = node.Nodes.Add(data.Saccades.Name);
                myNode.Tag = data.Saccades;
                myNode.ImageIndex = 94;
            }

            if (data.Fixations.List.Count > 0)
            {
                TreeNode myNode = node.Nodes.Add(data.Fixations.Name);
                myNode.Tag = data.Fixations;
                myNode.ImageIndex = 94;
            }
        }

        private void AddTrial(ref TreeNode node, Trial trial)
        {
            TreeNode childNode = new TreeNode(trial.Name);
            childNode.ImageIndex = 95;
            childNode.Tag = trial;
            foreach (DataList obj in trial.Data)
            {
                TreeNode myNode = childNode.Nodes.Add(obj.Name);
                myNode.Tag = obj;
                myNode.ContextMenuStrip = contextMenuStripTree;
                myNode.ImageIndex = 93;
                AddAnalysis(ref myNode, obj);
            }
            
            node.Nodes.Add(childNode);
        }

        private void AddROIs(ref TreeNode node, ROIList rois)
        {
            TreeNode childNode = new TreeNode(rois.Name);
            childNode.ImageIndex = 380;
            childNode.Tag = rois;
            node.Nodes.Add(childNode);
        }

        private void UpdateTreeDatas()
        {
            dataRootNode.Nodes.Clear();
            foreach (Trial trial in manager.Trials)
            {
                AddTrial(ref dataRootNode, trial);
            }

            dataRootNode.Expand();
            this.notifyIcon1.ShowBalloonTip(2000, "Data Changed", "Data Changed please refresh alla view", ToolTipIcon.Info);

        }

        private void UpdateTreeROIs()
        {
            roiRootNode.Nodes.Clear();
            foreach (ROIList rois in manager.ROIs)
            {
                AddROIs(ref roiRootNode, rois);
            }

            roiRootNode.Expand();
            this.notifyIcon1.ShowBalloonTip(2000, "ROI Changed", "ROI Changed please refresh alla view", ToolTipIcon.Info);

        }

        private void AddCase(ref TreeNode node, Case thiscase)
        {
            TreeNode childNode = new TreeNode(thiscase.Name);
            childNode.ImageIndex = 490;
            childNode.SelectedImageIndex = 485;
            childNode.Tag = thiscase;
            childNode.ContextMenuStrip = this.contextMenuStripCase;

            node.Nodes.Add(childNode);
        }

        private void UpdateTreeCase()
        {
            analysisRootNode.Nodes.Clear();
            foreach (Case thiscase in manager.Cases)
            {
                AddCase(ref analysisRootNode, thiscase);
            }
        }

        #endregion

        #region Get Selected
        /// <summary>
        /// Ritorna l'attual plot
        /// </summary>
        /// <returns></returns>
        private PanelPlotControl GetCurrentPanelPlotControl()
        {
            if ((this.tabView.SelectedTab == null) || !(this.tabView.SelectedTab.Controls[0] is PanelPlotControl))
            {
                AddNewPanelPlotControl();
            }
            return (PanelPlotControl)this.tabView.SelectedTab.Controls[0];
        }

        private MainSequence GetCurrentMainSequence()
        {
            if ((this.tabView.SelectedTab == null) || !(this.tabView.SelectedTab.Controls[0] is MainSequence))
            {
                AddNewMainSequence();
            }
            return (MainSequence)this.tabView.SelectedTab.Controls[0];
        }

        private DataList GetCurrentDataList()
        {
            if (this.treeView.SelectedNode.Tag is DataList) {
                DataList list = (DataList)this.treeView.SelectedNode.Tag;
                return list;
            } else {
                return null;
            }
        }

        private Case GetCurrentCase()
        {
            Case thiscase = (Case)this.treeView.SelectedNode.Tag;
            return thiscase;
        }

        #endregion


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void AddNewPanelPlotControl()
        {
            TabPage tabPage = new TabPage();
            tabView.TabPages.Add(tabPage);
            tabView.SuspendLayout();
            PanelPlotControl ctrl = new PanelPlotControl();
            tabPage.Controls.Add(ctrl);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabPage" + tabView.TabCount;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(508, 360);
            tabPage.TabIndex = tabView.TabCount;
            tabPage.Text = "Graph" + tabView.TabCount;
            tabPage.UseVisualStyleBackColor = true;

            ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrl.Location = new System.Drawing.Point(3, 3);
            ctrl.Name = "PanelPlotControl" + tabView.TabCount;
            ctrl.Size = new System.Drawing.Size(502, 354);
            ctrl.TabIndex = 0;

            tabView.SelectedTab = tabPage;
            tabView.ResumeLayout();
        }

        private void AddNewMainSequence()
        {
            TabPage tabPage = new TabPage();
            tabView.TabPages.Add(tabPage);
            tabView.SuspendLayout();
            MainSequence ctrl = new MainSequence();
            tabPage.Controls.Add(ctrl);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabPage" + tabView.TabCount;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(508, 360);
            tabPage.TabIndex = tabView.TabCount;
            tabPage.Text = "MainSequence" + tabView.TabCount;
            tabPage.UseVisualStyleBackColor = true;

            ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrl.Location = new System.Drawing.Point(3, 3);
            ctrl.Name = "MainSequence" + tabView.TabCount;
            ctrl.Size = new System.Drawing.Size(502, 354);
            ctrl.TabIndex = 0;

            tabView.SelectedTab = tabPage;
            tabView.ResumeLayout();
        }

        private void AddNewCBResult(List<Result> results)
        {
            TabPage tabPage = new TabPage();
            tabView.TabPages.Add(tabPage);
            tabView.SuspendLayout();
            DataGridView ctrl = new DataGridView();

            //add columns
            // Declare DataColumn and DataRow variables.
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "score";
            table.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "description";
            table.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "diagnosis";
            table.Columns.Add(column);

            foreach(Result result in results) {
                row = table.NewRow();
                row["score"] = result.Score;
                row["name"] = result.ThisCase.Name;
                row["description"] = result.ThisCase.Description;
                row["diagnosis"] = result.ThisCase.Diagnosis;

                table.Rows.Add(row);
            }

            ctrl.DataSource = table;
            ctrl.Invalidate();

            tabPage.Controls.Add(ctrl);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabPage" + tabView.TabCount;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(508, 360);
            tabPage.TabIndex = tabView.TabCount;
            tabPage.Text = "Results" + tabView.TabCount;
            tabPage.UseVisualStyleBackColor = true;

            ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrl.Location = new System.Drawing.Point(3, 3);
            ctrl.Name = "Results" + tabView.TabCount;
            ctrl.Size = new System.Drawing.Size(502, 354);
            ctrl.TabIndex = 0;

            tabView.SelectedTab = tabPage;
            tabView.ResumeLayout();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            AddNewPanelPlotControl();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabView.TabPages.Remove(this.tabView.SelectedTab);
        }

        /// <summary>
        /// About Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Dialog.AboutBox box = new Dialog.AboutBox();
            box.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            dlg.SelectedObject = Properties.Settings.Default;
            DialogResult res = dlg.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Reload();
            }

        }

        public void Log(string message)
        {
            this.textBoxLog.Text += message + "\n";
        }


        #region TreeAction

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataList list = GetCurrentDataList();
            this.GetCurrentPanelPlotControl().SetDataListVSTime(ref list);
        }

        private void showXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataList list = GetCurrentDataList();
            this.GetCurrentPanelPlotControl().SetDataListXY(ref list);
        }

        private void mainSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaccadeList list = GetCurrentDataList().Saccades;
            this.GetCurrentMainSequence().AddSaccades(GetCurrentDataList().Name, ref list);
        }

        private void filterApplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            FilterParams pars = FilterParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (FilterParams)dlg.SelectedObject;
                manager.FilterData(GetCurrentDataList(), pars);
                UpdateTreeDatas();

                Log("Current Data successfully filtered.");
            }
        }

        private void denoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            FilterParams pars = FilterParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (FilterParams)dlg.SelectedObject;
                manager.DenoiseData(GetCurrentDataList(), pars);
                UpdateTreeDatas();

                Log("Current Data successfully denoised.");
            }
        }

        private void buttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            FilterParams pars = FilterParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (FilterParams)dlg.SelectedObject;
                manager.FilterData(GetCurrentDataList(), pars);
                UpdateTreeDatas();

                Log("Current Data successfully filtered.");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.DeleteData(GetCurrentDataList());
            UpdateTreeDatas();
        }

        private void degreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            DataParams pars = DataParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (DataParams)dlg.SelectedObject;
                manager.ConvertToDegreeData(GetCurrentDataList(), pars);
                UpdateTreeDatas();
                Log("Current Data to degree successfully converted");
            }
        }

        private void saccToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            SaccadeParams pars = SaccadeParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                notifyIcon1.ShowBalloonTip(5000, "Saccade Analysis", "Saccade analysis in progress. Please refresh the view after the elaboration.", ToolTipIcon.Info);
                pars = (SaccadeParams)dlg.SelectedObject;
                manager.FindSaccade(GetCurrentDataList(), pars, EVALabAnalysis.AnalysisManager.AnalysisType.XY);
                UpdateTreeDatas();
                this.Cursor = Cursors.Default;
                Log("Saccades successfully found\n");
            }
        }

        private void byVelocityXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            SaccadeParams pars = SaccadeParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (SaccadeParams)dlg.SelectedObject;
                this.Cursor = Cursors.WaitCursor;
                notifyIcon1.ShowBalloonTip(5000, "Saccade Analysis", "Saccade analysis in progress. Please refresh the view after the elaboration.", ToolTipIcon.Info);
                manager.FindSaccade(GetCurrentDataList(), pars, EVALabAnalysis.AnalysisManager.AnalysisType.X);
                UpdateTreeDatas();
                this.Cursor = Cursors.Default;
                Log("Saccades successfully found\n");
            }
        }

        private void byVelocityYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            SaccadeParams pars = SaccadeParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (SaccadeParams)dlg.SelectedObject;
                this.Cursor = Cursors.WaitCursor;
                notifyIcon1.ShowBalloonTip(5000, "Saccade Analysis", "Saccade analysis in progress. Please refresh the view after the elaboration.", ToolTipIcon.Info);
                manager.FindSaccade(GetCurrentDataList(), pars, EVALabAnalysis.AnalysisManager.AnalysisType.Y);
                UpdateTreeDatas();
                this.Cursor = Cursors.Default;
                Log("Saccades successfully found\n");
            }
        }

        private void byDispersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyDlg dlg = new PropertyDlg();
            FixationParams pars = FixationParams.Instance();
            dlg.SelectedObject = pars;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pars = (FixationParams)dlg.SelectedObject;
                this.Cursor = Cursors.WaitCursor;
                long timeToElaborate = DateTime.Now.Ticks;
                DataList input = GetCurrentDataList();
                manager.FindFixation(GetCurrentDataList(), pars);
                Log(input.Fixations.List.Count + " fixations found in " + (DateTime.Now.Ticks - timeToElaborate) + " for " + input.List.Count + " sample");
                this.Cursor = Cursors.Default;
                UpdateTreeDatas();
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
            DataList list = GetCurrentDataList();
            if (list == null) return;
            this.GetCurrentPanelPlotControl().SetDataList(ref list);
        }
        #endregion



        private void toolStripAddMainSequence_Click(object sender, EventArgs e)
        {
            AddNewMainSequence();
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCase.Show(this, GetCurrentCase());
        }

        private void findCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCBResult(manager.BestCases(GetCurrentDataList()));
        }

        private void splitByReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.SplitByReference(this.GetCurrentDataList());
            UpdateTreeDatas();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Dispose();
        }

        private void RefreshDataOnPanel()
        {
            foreach (TabPage tab in tabView.TabPages)
            {
                try
                {
                    Control panel = tab.Controls[0];
                    panel.Refresh();
                }
                catch (Exception ee)
                {

                    Debug.WriteLine(ee.ToString());
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RefreshDataOnPanel();
        }
        #region ROI

        private void openROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogROI.ShowDialog(this);
        }
        private void openFileDialogROI_FileOk(object sender, CancelEventArgs e)
        {
            manager.CreateROIListFromDataFile(openFileDialogROI.FileName);
            UpdateTreeROIs();
        }

        private void alignDataToROIsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROIDialog roiDlg = new ROIDialog();
            roiDlg.SetList(manager.ROIs);

            if (roiDlg.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                manager.AlignData(this.GetCurrentDataList(), (ROIList)roiDlg.SelectedObject);
                this.Cursor = Cursors.Default;
                Log("Data aligned to ROI");
                UpdateTreeDatas();
            }
        }

        private void computeSequencingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROIDialog roiDlg = new ROIDialog();
            roiDlg.SetList(manager.ROIs);

            if (roiDlg.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                manager.AnalyzeROIs(this.GetCurrentDataList(), (ROIList)roiDlg.SelectedObject);
                this.Cursor = Cursors.Default;
                Log("Sequencing parameters");
                UpdateTreeDatas();
            }
        }


        #endregion


        #region save
        private void exportRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                saveFileDialogRawASL.ShowDialog(this);
            }
            catch (Exception ee)
            {
                EVALab.Util.Box.ExceptionForm.Show(this,"Unable to open analysis",ee);
            }
        }

        private void saveFileDialogRawASL_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                manager.CreateRawDataFileFromDataList(saveFileDialogRawASL.FileName, this.GetCurrentDataList());
            }
            catch (Exception ee)
            {
                EVALab.Util.Box.ExceptionForm.Show(this,"Unable to save analysis",ee);
            }
        }
        #endregion

        #region save project
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogProject.ShowDialog(this);
        }

        private void opneProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogProject.ShowDialog(this);
        }

        private void openFileDialogProject_FileOk(object sender, CancelEventArgs e)
        {

            this.manager.OpenAnalysis(openFileDialogProject.OpenFile());
            this.UpdateTreeDatas();
        }

        private void saveFileDialogProject_FileOk(object sender, CancelEventArgs e)
        {
            this.manager.SaveAnalysis(saveFileDialogProject.OpenFile());
        }
        #endregion





    }
}
