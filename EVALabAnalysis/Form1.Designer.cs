namespace EVALabAnalysis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.ComponentModel.License license = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (license != null)
                {
                    license.Dispose();
                    license = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBinocularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openASLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItemOpenASL = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openCSVDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opneProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMainSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripAddMainSequence = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.propertyNode = new System.Windows.Forms.PropertyGrid();
            this.tabView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelPlotControl1 = new EVALabAnalysis.Display.PanelPlotControl();
            this.openFileDialogASL = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showVsTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterApplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.denoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computeDistanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saccadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saccToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byVelocityXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byVelocityYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDispersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignDataToROIsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computeSequencingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importExperimentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitByReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFixationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSaccadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripCase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.openFileDialogRawEOG = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogEOG = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogRawASL = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogRawASL = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogROI = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogProject = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogProject = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStripTrial = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogRawCSV = new System.Windows.Forms.OpenFileDialog();
            this.openROIToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripROI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripDATA = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openCSVDATAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openASLRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabView.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStripTree.SuspendLayout();
            this.contextMenuStripCase.SuspendLayout();
            this.contextMenuStripTrial.SuspendLayout();
            this.contextMenuStripROI.SuspendLayout();
            this.contextMenuStripDATA.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel1.Text = "Version";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel2.Text = "                ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.viewToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.newProjectToolStripMenuItem,
            this.opneProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "File";
            this.toolStripMenuItemFile.ToolTipText = "Open saved project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBinocularToolStripMenuItem,
            this.openASLToolStripMenuItem,
            this.openCSVDataToolStripMenuItem,
            this.openROIToolStripMenuItem});
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "Open Data";
            // 
            // openBinocularToolStripMenuItem
            // 
            this.openBinocularToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.rawDataToolStripMenuItem});
            this.openBinocularToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openBinocularToolStripMenuItem.Image")));
            this.openBinocularToolStripMenuItem.Name = "openBinocularToolStripMenuItem";
            this.openBinocularToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openBinocularToolStripMenuItem.Text = "Open Binocular EOG";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.dataToolStripMenuItem.Text = "Data ...";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // rawDataToolStripMenuItem
            // 
            this.rawDataToolStripMenuItem.Name = "rawDataToolStripMenuItem";
            this.rawDataToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.rawDataToolStripMenuItem.Text = "Raw Data ...";
            this.rawDataToolStripMenuItem.Click += new System.EventHandler(this.rawDataToolStripMenuItem_Click);
            // 
            // openASLToolStripMenuItem
            // 
            this.openASLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItemOpenASL,
            this.rawDataToolStripMenuItem1});
            this.openASLToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openASLToolStripMenuItem.Image")));
            this.openASLToolStripMenuItem.Name = "openASLToolStripMenuItem";
            this.openASLToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openASLToolStripMenuItem.Text = "Open ASL";
            // 
            // dataToolStripMenuItemOpenASL
            // 
            this.dataToolStripMenuItemOpenASL.Name = "dataToolStripMenuItemOpenASL";
            this.dataToolStripMenuItemOpenASL.Size = new System.Drawing.Size(135, 22);
            this.dataToolStripMenuItemOpenASL.Text = "Data ...";
            this.dataToolStripMenuItemOpenASL.Click += new System.EventHandler(this.dataToolStripMenuItemOpenASL_Click);
            // 
            // rawDataToolStripMenuItem1
            // 
            this.rawDataToolStripMenuItem1.Name = "rawDataToolStripMenuItem1";
            this.rawDataToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.rawDataToolStripMenuItem1.Text = "Raw Data ...";
            this.rawDataToolStripMenuItem1.Click += new System.EventHandler(this.rawDataToolStripMenuItem1_Click);
            // 
            // openCSVDataToolStripMenuItem
            // 
            this.openCSVDataToolStripMenuItem.Name = "openCSVDataToolStripMenuItem";
            this.openCSVDataToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openCSVDataToolStripMenuItem.Text = "Open CSV Data ...";
            this.openCSVDataToolStripMenuItem.Click += new System.EventHandler(this.openCSVDataToolStripMenuItem_Click);
            // 
            // openROIToolStripMenuItem
            // 
            this.openROIToolStripMenuItem.Name = "openROIToolStripMenuItem";
            this.openROIToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openROIToolStripMenuItem.Text = "Open ROI ...";
            this.openROIToolStripMenuItem.Click += new System.EventHandler(this.openROIToolStripMenuItem_Click);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newProjectToolStripMenuItem.Image")));
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // opneProjectToolStripMenuItem
            // 
            this.opneProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("opneProjectToolStripMenuItem.Image")));
            this.opneProjectToolStripMenuItem.Name = "opneProjectToolStripMenuItem";
            this.opneProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.opneProjectToolStripMenuItem.Text = "Open Project ...";
            this.opneProjectToolStripMenuItem.Click += new System.EventHandler(this.opneProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveProjectToolStripMenuItem.Image")));
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project ...";
            this.saveProjectToolStripMenuItem.ToolTipText = "Save current project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphToolStripMenuItem,
            this.newMainSequenceToolStripMenuItem,
            this.removeGraphToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // newGraphToolStripMenuItem
            // 
            this.newGraphToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newGraphToolStripMenuItem.Image")));
            this.newGraphToolStripMenuItem.Name = "newGraphToolStripMenuItem";
            this.newGraphToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.newGraphToolStripMenuItem.Text = "New Graph";
            this.newGraphToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // newMainSequenceToolStripMenuItem
            // 
            this.newMainSequenceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newMainSequenceToolStripMenuItem.Image")));
            this.newMainSequenceToolStripMenuItem.Name = "newMainSequenceToolStripMenuItem";
            this.newMainSequenceToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.newMainSequenceToolStripMenuItem.Text = "New Main Sequence";
            this.newMainSequenceToolStripMenuItem.Click += new System.EventHandler(this.toolStripAddMainSequence_Click);
            // 
            // removeGraphToolStripMenuItem
            // 
            this.removeGraphToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeGraphToolStripMenuItem.Image")));
            this.removeGraphToolStripMenuItem.Name = "removeGraphToolStripMenuItem";
            this.removeGraphToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.removeGraphToolStripMenuItem.Text = "Remove Graph";
            this.removeGraphToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem.Image")));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsToolStripMenuItem.Text = "Settings ...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.aboutToolStripMenuItem.Text = "About ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripSeparator2,
            this.toolStripButtonOpen,
            this.toolStripAddMainSequence,
            this.toolStripButton1,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(780, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStripOpen";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton5.Text = "Open Project";
            this.toolStripButton5.Click += new System.EventHandler(this.opneProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonOpen.Text = "Add Graph";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripAddMainSequence
            // 
            this.toolStripAddMainSequence.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAddMainSequence.Image")));
            this.toolStripAddMainSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAddMainSequence.Name = "toolStripAddMainSequence";
            this.toolStripAddMainSequence.Size = new System.Drawing.Size(133, 22);
            this.toolStripAddMainSequence.Text = "Add Main Sequence";
            this.toolStripAddMainSequence.Click += new System.EventHandler(this.toolStripAddMainSequence_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(105, 22);
            this.toolStripButton1.Text = "Remove Graph";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Refresh All view";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Settings ...";
            this.toolStripButton3.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Information";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabView);
            this.splitContainer1.Size = new System.Drawing.Size(780, 222);
            this.splitContainer1.SplitterDistance = 158;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyNode);
            this.splitContainer2.Size = new System.Drawing.Size(158, 222);
            this.splitContainer2.SplitterDistance = 125;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 52;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 267;
            this.treeView.Size = new System.Drawing.Size(158, 125);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "accept.png");
            this.imageList.Images.SetKeyName(1, "add.png");
            this.imageList.Images.SetKeyName(2, "anchor.png");
            this.imageList.Images.SetKeyName(3, "application.png");
            this.imageList.Images.SetKeyName(4, "application_add.png");
            this.imageList.Images.SetKeyName(5, "application_cascade.png");
            this.imageList.Images.SetKeyName(6, "application_delete.png");
            this.imageList.Images.SetKeyName(7, "application_double.png");
            this.imageList.Images.SetKeyName(8, "application_edit.png");
            this.imageList.Images.SetKeyName(9, "application_error.png");
            this.imageList.Images.SetKeyName(10, "application_form.png");
            this.imageList.Images.SetKeyName(11, "application_form_add.png");
            this.imageList.Images.SetKeyName(12, "application_form_delete.png");
            this.imageList.Images.SetKeyName(13, "application_form_edit.png");
            this.imageList.Images.SetKeyName(14, "application_form_magnify.png");
            this.imageList.Images.SetKeyName(15, "application_get.png");
            this.imageList.Images.SetKeyName(16, "application_go.png");
            this.imageList.Images.SetKeyName(17, "application_home.png");
            this.imageList.Images.SetKeyName(18, "application_key.png");
            this.imageList.Images.SetKeyName(19, "application_lightning.png");
            this.imageList.Images.SetKeyName(20, "application_link.png");
            this.imageList.Images.SetKeyName(21, "application_osx.png");
            this.imageList.Images.SetKeyName(22, "application_osx_terminal.png");
            this.imageList.Images.SetKeyName(23, "application_put.png");
            this.imageList.Images.SetKeyName(24, "application_side_boxes.png");
            this.imageList.Images.SetKeyName(25, "application_side_contract.png");
            this.imageList.Images.SetKeyName(26, "application_side_expand.png");
            this.imageList.Images.SetKeyName(27, "application_side_list.png");
            this.imageList.Images.SetKeyName(28, "application_side_tree.png");
            this.imageList.Images.SetKeyName(29, "application_split.png");
            this.imageList.Images.SetKeyName(30, "application_tile_horizontal.png");
            this.imageList.Images.SetKeyName(31, "application_tile_vertical.png");
            this.imageList.Images.SetKeyName(32, "application_view_columns.png");
            this.imageList.Images.SetKeyName(33, "application_view_detail.png");
            this.imageList.Images.SetKeyName(34, "application_view_gallery.png");
            this.imageList.Images.SetKeyName(35, "application_view_icons.png");
            this.imageList.Images.SetKeyName(36, "application_view_list.png");
            this.imageList.Images.SetKeyName(37, "application_view_tile.png");
            this.imageList.Images.SetKeyName(38, "application_xp.png");
            this.imageList.Images.SetKeyName(39, "application_xp_terminal.png");
            this.imageList.Images.SetKeyName(40, "arrow_branch.png");
            this.imageList.Images.SetKeyName(41, "arrow_divide.png");
            this.imageList.Images.SetKeyName(42, "arrow_down.png");
            this.imageList.Images.SetKeyName(43, "arrow_in.png");
            this.imageList.Images.SetKeyName(44, "arrow_inout.png");
            this.imageList.Images.SetKeyName(45, "arrow_join.png");
            this.imageList.Images.SetKeyName(46, "arrow_left.png");
            this.imageList.Images.SetKeyName(47, "arrow_merge.png");
            this.imageList.Images.SetKeyName(48, "arrow_out.png");
            this.imageList.Images.SetKeyName(49, "arrow_redo.png");
            this.imageList.Images.SetKeyName(50, "arrow_refresh.png");
            this.imageList.Images.SetKeyName(51, "arrow_refresh_small.png");
            this.imageList.Images.SetKeyName(52, "arrow_right.png");
            this.imageList.Images.SetKeyName(53, "arrow_rotate_anticlockwise.png");
            this.imageList.Images.SetKeyName(54, "arrow_rotate_clockwise.png");
            this.imageList.Images.SetKeyName(55, "arrow_switch.png");
            this.imageList.Images.SetKeyName(56, "arrow_turn_left.png");
            this.imageList.Images.SetKeyName(57, "arrow_turn_right.png");
            this.imageList.Images.SetKeyName(58, "arrow_undo.png");
            this.imageList.Images.SetKeyName(59, "arrow_up.png");
            this.imageList.Images.SetKeyName(60, "asterisk_orange.png");
            this.imageList.Images.SetKeyName(61, "asterisk_yellow.png");
            this.imageList.Images.SetKeyName(62, "attach.png");
            this.imageList.Images.SetKeyName(63, "award_star_add.png");
            this.imageList.Images.SetKeyName(64, "award_star_bronze_1.png");
            this.imageList.Images.SetKeyName(65, "award_star_bronze_2.png");
            this.imageList.Images.SetKeyName(66, "award_star_bronze_3.png");
            this.imageList.Images.SetKeyName(67, "award_star_delete.png");
            this.imageList.Images.SetKeyName(68, "award_star_gold_1.png");
            this.imageList.Images.SetKeyName(69, "award_star_gold_2.png");
            this.imageList.Images.SetKeyName(70, "award_star_gold_3.png");
            this.imageList.Images.SetKeyName(71, "award_star_silver_1.png");
            this.imageList.Images.SetKeyName(72, "award_star_silver_2.png");
            this.imageList.Images.SetKeyName(73, "award_star_silver_3.png");
            this.imageList.Images.SetKeyName(74, "basket.png");
            this.imageList.Images.SetKeyName(75, "basket_add.png");
            this.imageList.Images.SetKeyName(76, "basket_delete.png");
            this.imageList.Images.SetKeyName(77, "basket_edit.png");
            this.imageList.Images.SetKeyName(78, "basket_error.png");
            this.imageList.Images.SetKeyName(79, "basket_go.png");
            this.imageList.Images.SetKeyName(80, "basket_put.png");
            this.imageList.Images.SetKeyName(81, "basket_remove.png");
            this.imageList.Images.SetKeyName(82, "bell.png");
            this.imageList.Images.SetKeyName(83, "bell_add.png");
            this.imageList.Images.SetKeyName(84, "bell_delete.png");
            this.imageList.Images.SetKeyName(85, "bell_error.png");
            this.imageList.Images.SetKeyName(86, "bell_go.png");
            this.imageList.Images.SetKeyName(87, "bell_link.png");
            this.imageList.Images.SetKeyName(88, "bin.png");
            this.imageList.Images.SetKeyName(89, "bin_closed.png");
            this.imageList.Images.SetKeyName(90, "bin_empty.png");
            this.imageList.Images.SetKeyName(91, "bomb.png");
            this.imageList.Images.SetKeyName(92, "book.png");
            this.imageList.Images.SetKeyName(93, "book_add.png");
            this.imageList.Images.SetKeyName(94, "book_addresses.png");
            this.imageList.Images.SetKeyName(95, "book_delete.png");
            this.imageList.Images.SetKeyName(96, "book_edit.png");
            this.imageList.Images.SetKeyName(97, "book_error.png");
            this.imageList.Images.SetKeyName(98, "book_go.png");
            this.imageList.Images.SetKeyName(99, "book_key.png");
            this.imageList.Images.SetKeyName(100, "book_link.png");
            this.imageList.Images.SetKeyName(101, "book_next.png");
            this.imageList.Images.SetKeyName(102, "book_open.png");
            this.imageList.Images.SetKeyName(103, "book_previous.png");
            this.imageList.Images.SetKeyName(104, "box.png");
            this.imageList.Images.SetKeyName(105, "brick.png");
            this.imageList.Images.SetKeyName(106, "brick_add.png");
            this.imageList.Images.SetKeyName(107, "brick_delete.png");
            this.imageList.Images.SetKeyName(108, "brick_edit.png");
            this.imageList.Images.SetKeyName(109, "brick_error.png");
            this.imageList.Images.SetKeyName(110, "brick_go.png");
            this.imageList.Images.SetKeyName(111, "brick_link.png");
            this.imageList.Images.SetKeyName(112, "bricks.png");
            this.imageList.Images.SetKeyName(113, "briefcase.png");
            this.imageList.Images.SetKeyName(114, "bug.png");
            this.imageList.Images.SetKeyName(115, "bug_add.png");
            this.imageList.Images.SetKeyName(116, "bug_delete.png");
            this.imageList.Images.SetKeyName(117, "bug_edit.png");
            this.imageList.Images.SetKeyName(118, "bug_error.png");
            this.imageList.Images.SetKeyName(119, "bug_go.png");
            this.imageList.Images.SetKeyName(120, "bug_link.png");
            this.imageList.Images.SetKeyName(121, "building.png");
            this.imageList.Images.SetKeyName(122, "building_add.png");
            this.imageList.Images.SetKeyName(123, "building_delete.png");
            this.imageList.Images.SetKeyName(124, "building_edit.png");
            this.imageList.Images.SetKeyName(125, "building_error.png");
            this.imageList.Images.SetKeyName(126, "building_go.png");
            this.imageList.Images.SetKeyName(127, "building_key.png");
            this.imageList.Images.SetKeyName(128, "building_link.png");
            this.imageList.Images.SetKeyName(129, "bullet_add.png");
            this.imageList.Images.SetKeyName(130, "bullet_arrow_bottom.png");
            this.imageList.Images.SetKeyName(131, "bullet_arrow_down.png");
            this.imageList.Images.SetKeyName(132, "bullet_arrow_top.png");
            this.imageList.Images.SetKeyName(133, "bullet_arrow_up.png");
            this.imageList.Images.SetKeyName(134, "bullet_black.png");
            this.imageList.Images.SetKeyName(135, "bullet_blue.png");
            this.imageList.Images.SetKeyName(136, "bullet_delete.png");
            this.imageList.Images.SetKeyName(137, "bullet_disk.png");
            this.imageList.Images.SetKeyName(138, "bullet_error.png");
            this.imageList.Images.SetKeyName(139, "bullet_feed.png");
            this.imageList.Images.SetKeyName(140, "bullet_go.png");
            this.imageList.Images.SetKeyName(141, "bullet_green.png");
            this.imageList.Images.SetKeyName(142, "bullet_key.png");
            this.imageList.Images.SetKeyName(143, "bullet_orange.png");
            this.imageList.Images.SetKeyName(144, "bullet_picture.png");
            this.imageList.Images.SetKeyName(145, "bullet_pink.png");
            this.imageList.Images.SetKeyName(146, "bullet_purple.png");
            this.imageList.Images.SetKeyName(147, "bullet_red.png");
            this.imageList.Images.SetKeyName(148, "bullet_star.png");
            this.imageList.Images.SetKeyName(149, "bullet_toggle_minus.png");
            this.imageList.Images.SetKeyName(150, "bullet_toggle_plus.png");
            this.imageList.Images.SetKeyName(151, "bullet_white.png");
            this.imageList.Images.SetKeyName(152, "bullet_wrench.png");
            this.imageList.Images.SetKeyName(153, "bullet_yellow.png");
            this.imageList.Images.SetKeyName(154, "cake.png");
            this.imageList.Images.SetKeyName(155, "calculator.png");
            this.imageList.Images.SetKeyName(156, "calculator_add.png");
            this.imageList.Images.SetKeyName(157, "calculator_delete.png");
            this.imageList.Images.SetKeyName(158, "calculator_edit.png");
            this.imageList.Images.SetKeyName(159, "calculator_error.png");
            this.imageList.Images.SetKeyName(160, "calculator_link.png");
            this.imageList.Images.SetKeyName(161, "calendar.png");
            this.imageList.Images.SetKeyName(162, "calendar_add.png");
            this.imageList.Images.SetKeyName(163, "calendar_delete.png");
            this.imageList.Images.SetKeyName(164, "calendar_edit.png");
            this.imageList.Images.SetKeyName(165, "calendar_link.png");
            this.imageList.Images.SetKeyName(166, "calendar_view_day.png");
            this.imageList.Images.SetKeyName(167, "calendar_view_month.png");
            this.imageList.Images.SetKeyName(168, "calendar_view_week.png");
            this.imageList.Images.SetKeyName(169, "camera.png");
            this.imageList.Images.SetKeyName(170, "camera_add.png");
            this.imageList.Images.SetKeyName(171, "camera_delete.png");
            this.imageList.Images.SetKeyName(172, "camera_edit.png");
            this.imageList.Images.SetKeyName(173, "camera_error.png");
            this.imageList.Images.SetKeyName(174, "camera_go.png");
            this.imageList.Images.SetKeyName(175, "camera_link.png");
            this.imageList.Images.SetKeyName(176, "camera_small.png");
            this.imageList.Images.SetKeyName(177, "cancel.png");
            this.imageList.Images.SetKeyName(178, "car.png");
            this.imageList.Images.SetKeyName(179, "car_add.png");
            this.imageList.Images.SetKeyName(180, "car_delete.png");
            this.imageList.Images.SetKeyName(181, "cart.png");
            this.imageList.Images.SetKeyName(182, "cart_add.png");
            this.imageList.Images.SetKeyName(183, "cart_delete.png");
            this.imageList.Images.SetKeyName(184, "cart_edit.png");
            this.imageList.Images.SetKeyName(185, "cart_error.png");
            this.imageList.Images.SetKeyName(186, "cart_go.png");
            this.imageList.Images.SetKeyName(187, "cart_put.png");
            this.imageList.Images.SetKeyName(188, "cart_remove.png");
            this.imageList.Images.SetKeyName(189, "cd.png");
            this.imageList.Images.SetKeyName(190, "cd_add.png");
            this.imageList.Images.SetKeyName(191, "cd_burn.png");
            this.imageList.Images.SetKeyName(192, "cd_delete.png");
            this.imageList.Images.SetKeyName(193, "cd_edit.png");
            this.imageList.Images.SetKeyName(194, "cd_eject.png");
            this.imageList.Images.SetKeyName(195, "cd_go.png");
            this.imageList.Images.SetKeyName(196, "chart_bar.png");
            this.imageList.Images.SetKeyName(197, "chart_bar_add.png");
            this.imageList.Images.SetKeyName(198, "chart_bar_delete.png");
            this.imageList.Images.SetKeyName(199, "chart_bar_edit.png");
            this.imageList.Images.SetKeyName(200, "chart_bar_error.png");
            this.imageList.Images.SetKeyName(201, "chart_bar_link.png");
            this.imageList.Images.SetKeyName(202, "chart_curve.png");
            this.imageList.Images.SetKeyName(203, "chart_curve_add.png");
            this.imageList.Images.SetKeyName(204, "chart_curve_delete.png");
            this.imageList.Images.SetKeyName(205, "chart_curve_edit.png");
            this.imageList.Images.SetKeyName(206, "chart_curve_error.png");
            this.imageList.Images.SetKeyName(207, "chart_curve_go.png");
            this.imageList.Images.SetKeyName(208, "chart_curve_link.png");
            this.imageList.Images.SetKeyName(209, "chart_line.png");
            this.imageList.Images.SetKeyName(210, "chart_line_add.png");
            this.imageList.Images.SetKeyName(211, "chart_line_delete.png");
            this.imageList.Images.SetKeyName(212, "chart_line_edit.png");
            this.imageList.Images.SetKeyName(213, "chart_line_error.png");
            this.imageList.Images.SetKeyName(214, "chart_line_link.png");
            this.imageList.Images.SetKeyName(215, "chart_organisation.png");
            this.imageList.Images.SetKeyName(216, "chart_organisation_add.png");
            this.imageList.Images.SetKeyName(217, "chart_organisation_delete.png");
            this.imageList.Images.SetKeyName(218, "chart_pie.png");
            this.imageList.Images.SetKeyName(219, "chart_pie_add.png");
            this.imageList.Images.SetKeyName(220, "chart_pie_delete.png");
            this.imageList.Images.SetKeyName(221, "chart_pie_edit.png");
            this.imageList.Images.SetKeyName(222, "chart_pie_error.png");
            this.imageList.Images.SetKeyName(223, "chart_pie_link.png");
            this.imageList.Images.SetKeyName(224, "clock.png");
            this.imageList.Images.SetKeyName(225, "clock_add.png");
            this.imageList.Images.SetKeyName(226, "clock_delete.png");
            this.imageList.Images.SetKeyName(227, "clock_edit.png");
            this.imageList.Images.SetKeyName(228, "clock_error.png");
            this.imageList.Images.SetKeyName(229, "clock_go.png");
            this.imageList.Images.SetKeyName(230, "clock_link.png");
            this.imageList.Images.SetKeyName(231, "clock_pause.png");
            this.imageList.Images.SetKeyName(232, "clock_play.png");
            this.imageList.Images.SetKeyName(233, "clock_red.png");
            this.imageList.Images.SetKeyName(234, "clock_stop.png");
            this.imageList.Images.SetKeyName(235, "cog.png");
            this.imageList.Images.SetKeyName(236, "cog_add.png");
            this.imageList.Images.SetKeyName(237, "cog_delete.png");
            this.imageList.Images.SetKeyName(238, "cog_edit.png");
            this.imageList.Images.SetKeyName(239, "cog_error.png");
            this.imageList.Images.SetKeyName(240, "cog_go.png");
            this.imageList.Images.SetKeyName(241, "coins.png");
            this.imageList.Images.SetKeyName(242, "coins_add.png");
            this.imageList.Images.SetKeyName(243, "coins_delete.png");
            this.imageList.Images.SetKeyName(244, "color_swatch.png");
            this.imageList.Images.SetKeyName(245, "color_wheel.png");
            this.imageList.Images.SetKeyName(246, "comment.png");
            this.imageList.Images.SetKeyName(247, "comment_add.png");
            this.imageList.Images.SetKeyName(248, "comment_delete.png");
            this.imageList.Images.SetKeyName(249, "comment_edit.png");
            this.imageList.Images.SetKeyName(250, "comments.png");
            this.imageList.Images.SetKeyName(251, "comments_add.png");
            this.imageList.Images.SetKeyName(252, "comments_delete.png");
            this.imageList.Images.SetKeyName(253, "compress.png");
            this.imageList.Images.SetKeyName(254, "computer.png");
            this.imageList.Images.SetKeyName(255, "computer_add.png");
            this.imageList.Images.SetKeyName(256, "computer_delete.png");
            this.imageList.Images.SetKeyName(257, "computer_edit.png");
            this.imageList.Images.SetKeyName(258, "computer_error.png");
            this.imageList.Images.SetKeyName(259, "computer_go.png");
            this.imageList.Images.SetKeyName(260, "computer_key.png");
            this.imageList.Images.SetKeyName(261, "computer_link.png");
            this.imageList.Images.SetKeyName(262, "connect.png");
            this.imageList.Images.SetKeyName(263, "contrast.png");
            this.imageList.Images.SetKeyName(264, "contrast_decrease.png");
            this.imageList.Images.SetKeyName(265, "contrast_high.png");
            this.imageList.Images.SetKeyName(266, "contrast_increase.png");
            this.imageList.Images.SetKeyName(267, "contrast_low.png");
            this.imageList.Images.SetKeyName(268, "control_eject.png");
            this.imageList.Images.SetKeyName(269, "control_eject_blue.png");
            this.imageList.Images.SetKeyName(270, "control_end.png");
            this.imageList.Images.SetKeyName(271, "control_end_blue.png");
            this.imageList.Images.SetKeyName(272, "control_equalizer.png");
            this.imageList.Images.SetKeyName(273, "control_equalizer_blue.png");
            this.imageList.Images.SetKeyName(274, "control_fastforward.png");
            this.imageList.Images.SetKeyName(275, "control_fastforward_blue.png");
            this.imageList.Images.SetKeyName(276, "control_pause.png");
            this.imageList.Images.SetKeyName(277, "control_pause_blue.png");
            this.imageList.Images.SetKeyName(278, "control_play.png");
            this.imageList.Images.SetKeyName(279, "control_play_blue.png");
            this.imageList.Images.SetKeyName(280, "control_repeat.png");
            this.imageList.Images.SetKeyName(281, "control_repeat_blue.png");
            this.imageList.Images.SetKeyName(282, "control_rewind.png");
            this.imageList.Images.SetKeyName(283, "control_rewind_blue.png");
            this.imageList.Images.SetKeyName(284, "control_start.png");
            this.imageList.Images.SetKeyName(285, "control_start_blue.png");
            this.imageList.Images.SetKeyName(286, "control_stop.png");
            this.imageList.Images.SetKeyName(287, "control_stop_blue.png");
            this.imageList.Images.SetKeyName(288, "controller.png");
            this.imageList.Images.SetKeyName(289, "controller_add.png");
            this.imageList.Images.SetKeyName(290, "controller_delete.png");
            this.imageList.Images.SetKeyName(291, "controller_error.png");
            this.imageList.Images.SetKeyName(292, "creditcards.png");
            this.imageList.Images.SetKeyName(293, "cross.png");
            this.imageList.Images.SetKeyName(294, "css.png");
            this.imageList.Images.SetKeyName(295, "css_add.png");
            this.imageList.Images.SetKeyName(296, "css_delete.png");
            this.imageList.Images.SetKeyName(297, "css_go.png");
            this.imageList.Images.SetKeyName(298, "css_valid.png");
            this.imageList.Images.SetKeyName(299, "cup.png");
            this.imageList.Images.SetKeyName(300, "cup_add.png");
            this.imageList.Images.SetKeyName(301, "cup_delete.png");
            this.imageList.Images.SetKeyName(302, "cup_edit.png");
            this.imageList.Images.SetKeyName(303, "cup_error.png");
            this.imageList.Images.SetKeyName(304, "cup_go.png");
            this.imageList.Images.SetKeyName(305, "cup_key.png");
            this.imageList.Images.SetKeyName(306, "cup_link.png");
            this.imageList.Images.SetKeyName(307, "cursor.png");
            this.imageList.Images.SetKeyName(308, "cut.png");
            this.imageList.Images.SetKeyName(309, "cut_red.png");
            this.imageList.Images.SetKeyName(310, "database.png");
            this.imageList.Images.SetKeyName(311, "database_add.png");
            this.imageList.Images.SetKeyName(312, "database_connect.png");
            this.imageList.Images.SetKeyName(313, "database_delete.png");
            this.imageList.Images.SetKeyName(314, "database_edit.png");
            this.imageList.Images.SetKeyName(315, "database_error.png");
            this.imageList.Images.SetKeyName(316, "database_gear.png");
            this.imageList.Images.SetKeyName(317, "database_go.png");
            this.imageList.Images.SetKeyName(318, "database_key.png");
            this.imageList.Images.SetKeyName(319, "database_lightning.png");
            this.imageList.Images.SetKeyName(320, "database_link.png");
            this.imageList.Images.SetKeyName(321, "database_refresh.png");
            this.imageList.Images.SetKeyName(322, "database_save.png");
            this.imageList.Images.SetKeyName(323, "database_table.png");
            this.imageList.Images.SetKeyName(324, "date.png");
            this.imageList.Images.SetKeyName(325, "date_add.png");
            this.imageList.Images.SetKeyName(326, "date_delete.png");
            this.imageList.Images.SetKeyName(327, "date_edit.png");
            this.imageList.Images.SetKeyName(328, "date_error.png");
            this.imageList.Images.SetKeyName(329, "date_go.png");
            this.imageList.Images.SetKeyName(330, "date_link.png");
            this.imageList.Images.SetKeyName(331, "date_magnify.png");
            this.imageList.Images.SetKeyName(332, "date_next.png");
            this.imageList.Images.SetKeyName(333, "date_previous.png");
            this.imageList.Images.SetKeyName(334, "delete.png");
            this.imageList.Images.SetKeyName(335, "disconnect.png");
            this.imageList.Images.SetKeyName(336, "disk.png");
            this.imageList.Images.SetKeyName(337, "disk_multiple.png");
            this.imageList.Images.SetKeyName(338, "door.png");
            this.imageList.Images.SetKeyName(339, "door_in.png");
            this.imageList.Images.SetKeyName(340, "door_open.png");
            this.imageList.Images.SetKeyName(341, "door_out.png");
            this.imageList.Images.SetKeyName(342, "drink.png");
            this.imageList.Images.SetKeyName(343, "drink_empty.png");
            this.imageList.Images.SetKeyName(344, "drive.png");
            this.imageList.Images.SetKeyName(345, "drive_add.png");
            this.imageList.Images.SetKeyName(346, "drive_burn.png");
            this.imageList.Images.SetKeyName(347, "drive_cd.png");
            this.imageList.Images.SetKeyName(348, "drive_cd_empty.png");
            this.imageList.Images.SetKeyName(349, "drive_delete.png");
            this.imageList.Images.SetKeyName(350, "drive_disk.png");
            this.imageList.Images.SetKeyName(351, "drive_edit.png");
            this.imageList.Images.SetKeyName(352, "drive_error.png");
            this.imageList.Images.SetKeyName(353, "drive_go.png");
            this.imageList.Images.SetKeyName(354, "drive_key.png");
            this.imageList.Images.SetKeyName(355, "drive_link.png");
            this.imageList.Images.SetKeyName(356, "drive_magnify.png");
            this.imageList.Images.SetKeyName(357, "drive_network.png");
            this.imageList.Images.SetKeyName(358, "drive_rename.png");
            this.imageList.Images.SetKeyName(359, "drive_user.png");
            this.imageList.Images.SetKeyName(360, "drive_web.png");
            this.imageList.Images.SetKeyName(361, "dvd.png");
            this.imageList.Images.SetKeyName(362, "dvd_add.png");
            this.imageList.Images.SetKeyName(363, "dvd_delete.png");
            this.imageList.Images.SetKeyName(364, "dvd_edit.png");
            this.imageList.Images.SetKeyName(365, "dvd_error.png");
            this.imageList.Images.SetKeyName(366, "dvd_go.png");
            this.imageList.Images.SetKeyName(367, "dvd_key.png");
            this.imageList.Images.SetKeyName(368, "dvd_link.png");
            this.imageList.Images.SetKeyName(369, "email.png");
            this.imageList.Images.SetKeyName(370, "email_add.png");
            this.imageList.Images.SetKeyName(371, "email_attach.png");
            this.imageList.Images.SetKeyName(372, "email_delete.png");
            this.imageList.Images.SetKeyName(373, "email_edit.png");
            this.imageList.Images.SetKeyName(374, "email_error.png");
            this.imageList.Images.SetKeyName(375, "email_go.png");
            this.imageList.Images.SetKeyName(376, "email_link.png");
            this.imageList.Images.SetKeyName(377, "email_open.png");
            this.imageList.Images.SetKeyName(378, "email_open_image.png");
            this.imageList.Images.SetKeyName(379, "emoticon_evilgrin.png");
            this.imageList.Images.SetKeyName(380, "emoticon_grin.png");
            this.imageList.Images.SetKeyName(381, "emoticon_happy.png");
            this.imageList.Images.SetKeyName(382, "emoticon_smile.png");
            this.imageList.Images.SetKeyName(383, "emoticon_surprised.png");
            this.imageList.Images.SetKeyName(384, "emoticon_tongue.png");
            this.imageList.Images.SetKeyName(385, "emoticon_unhappy.png");
            this.imageList.Images.SetKeyName(386, "emoticon_waii.png");
            this.imageList.Images.SetKeyName(387, "emoticon_wink.png");
            this.imageList.Images.SetKeyName(388, "error.png");
            this.imageList.Images.SetKeyName(389, "error_add.png");
            this.imageList.Images.SetKeyName(390, "error_delete.png");
            this.imageList.Images.SetKeyName(391, "error_go.png");
            this.imageList.Images.SetKeyName(392, "exclamation.png");
            this.imageList.Images.SetKeyName(393, "eye.png");
            this.imageList.Images.SetKeyName(394, "feed.png");
            this.imageList.Images.SetKeyName(395, "feed_add.png");
            this.imageList.Images.SetKeyName(396, "feed_delete.png");
            this.imageList.Images.SetKeyName(397, "feed_disk.png");
            this.imageList.Images.SetKeyName(398, "feed_edit.png");
            this.imageList.Images.SetKeyName(399, "feed_error.png");
            this.imageList.Images.SetKeyName(400, "feed_go.png");
            this.imageList.Images.SetKeyName(401, "feed_key.png");
            this.imageList.Images.SetKeyName(402, "feed_link.png");
            this.imageList.Images.SetKeyName(403, "feed_magnify.png");
            this.imageList.Images.SetKeyName(404, "female.png");
            this.imageList.Images.SetKeyName(405, "film.png");
            this.imageList.Images.SetKeyName(406, "film_add.png");
            this.imageList.Images.SetKeyName(407, "film_delete.png");
            this.imageList.Images.SetKeyName(408, "film_edit.png");
            this.imageList.Images.SetKeyName(409, "film_error.png");
            this.imageList.Images.SetKeyName(410, "film_go.png");
            this.imageList.Images.SetKeyName(411, "film_key.png");
            this.imageList.Images.SetKeyName(412, "film_link.png");
            this.imageList.Images.SetKeyName(413, "film_save.png");
            this.imageList.Images.SetKeyName(414, "find.png");
            this.imageList.Images.SetKeyName(415, "flag_blue.png");
            this.imageList.Images.SetKeyName(416, "flag_green.png");
            this.imageList.Images.SetKeyName(417, "flag_orange.png");
            this.imageList.Images.SetKeyName(418, "flag_pink.png");
            this.imageList.Images.SetKeyName(419, "flag_purple.png");
            this.imageList.Images.SetKeyName(420, "flag_red.png");
            this.imageList.Images.SetKeyName(421, "flag_yellow.png");
            this.imageList.Images.SetKeyName(422, "folder.png");
            this.imageList.Images.SetKeyName(423, "folder_add.png");
            this.imageList.Images.SetKeyName(424, "folder_bell.png");
            this.imageList.Images.SetKeyName(425, "folder_brick.png");
            this.imageList.Images.SetKeyName(426, "folder_bug.png");
            this.imageList.Images.SetKeyName(427, "folder_camera.png");
            this.imageList.Images.SetKeyName(428, "folder_database.png");
            this.imageList.Images.SetKeyName(429, "folder_delete.png");
            this.imageList.Images.SetKeyName(430, "folder_edit.png");
            this.imageList.Images.SetKeyName(431, "folder_error.png");
            this.imageList.Images.SetKeyName(432, "folder_explore.png");
            this.imageList.Images.SetKeyName(433, "folder_feed.png");
            this.imageList.Images.SetKeyName(434, "folder_find.png");
            this.imageList.Images.SetKeyName(435, "folder_go.png");
            this.imageList.Images.SetKeyName(436, "folder_heart.png");
            this.imageList.Images.SetKeyName(437, "folder_image.png");
            this.imageList.Images.SetKeyName(438, "folder_key.png");
            this.imageList.Images.SetKeyName(439, "folder_lightbulb.png");
            this.imageList.Images.SetKeyName(440, "folder_link.png");
            this.imageList.Images.SetKeyName(441, "folder_magnify.png");
            this.imageList.Images.SetKeyName(442, "folder_page.png");
            this.imageList.Images.SetKeyName(443, "folder_page_white.png");
            this.imageList.Images.SetKeyName(444, "folder_palette.png");
            this.imageList.Images.SetKeyName(445, "folder_picture.png");
            this.imageList.Images.SetKeyName(446, "folder_star.png");
            this.imageList.Images.SetKeyName(447, "folder_table.png");
            this.imageList.Images.SetKeyName(448, "folder_user.png");
            this.imageList.Images.SetKeyName(449, "folder_wrench.png");
            this.imageList.Images.SetKeyName(450, "font.png");
            this.imageList.Images.SetKeyName(451, "font_add.png");
            this.imageList.Images.SetKeyName(452, "font_delete.png");
            this.imageList.Images.SetKeyName(453, "font_go.png");
            this.imageList.Images.SetKeyName(454, "group.png");
            this.imageList.Images.SetKeyName(455, "group_add.png");
            this.imageList.Images.SetKeyName(456, "group_delete.png");
            this.imageList.Images.SetKeyName(457, "group_edit.png");
            this.imageList.Images.SetKeyName(458, "group_error.png");
            this.imageList.Images.SetKeyName(459, "group_gear.png");
            this.imageList.Images.SetKeyName(460, "group_go.png");
            this.imageList.Images.SetKeyName(461, "group_key.png");
            this.imageList.Images.SetKeyName(462, "group_link.png");
            this.imageList.Images.SetKeyName(463, "heart.png");
            this.imageList.Images.SetKeyName(464, "heart_add.png");
            this.imageList.Images.SetKeyName(465, "heart_delete.png");
            this.imageList.Images.SetKeyName(466, "help.png");
            this.imageList.Images.SetKeyName(467, "hourglass.png");
            this.imageList.Images.SetKeyName(468, "hourglass_add.png");
            this.imageList.Images.SetKeyName(469, "hourglass_delete.png");
            this.imageList.Images.SetKeyName(470, "hourglass_go.png");
            this.imageList.Images.SetKeyName(471, "hourglass_link.png");
            this.imageList.Images.SetKeyName(472, "house.png");
            this.imageList.Images.SetKeyName(473, "house_go.png");
            this.imageList.Images.SetKeyName(474, "house_link.png");
            this.imageList.Images.SetKeyName(475, "html.png");
            this.imageList.Images.SetKeyName(476, "html_add.png");
            this.imageList.Images.SetKeyName(477, "html_delete.png");
            this.imageList.Images.SetKeyName(478, "html_go.png");
            this.imageList.Images.SetKeyName(479, "html_valid.png");
            this.imageList.Images.SetKeyName(480, "image.png");
            this.imageList.Images.SetKeyName(481, "image_add.png");
            this.imageList.Images.SetKeyName(482, "image_delete.png");
            this.imageList.Images.SetKeyName(483, "image_edit.png");
            this.imageList.Images.SetKeyName(484, "image_link.png");
            this.imageList.Images.SetKeyName(485, "images.png");
            this.imageList.Images.SetKeyName(486, "information.png");
            this.imageList.Images.SetKeyName(487, "ipod.png");
            this.imageList.Images.SetKeyName(488, "ipod_cast.png");
            this.imageList.Images.SetKeyName(489, "ipod_cast_add.png");
            this.imageList.Images.SetKeyName(490, "ipod_cast_delete.png");
            this.imageList.Images.SetKeyName(491, "ipod_sound.png");
            this.imageList.Images.SetKeyName(492, "joystick.png");
            this.imageList.Images.SetKeyName(493, "joystick_add.png");
            this.imageList.Images.SetKeyName(494, "joystick_delete.png");
            this.imageList.Images.SetKeyName(495, "joystick_error.png");
            this.imageList.Images.SetKeyName(496, "key.png");
            this.imageList.Images.SetKeyName(497, "key_add.png");
            this.imageList.Images.SetKeyName(498, "key_delete.png");
            this.imageList.Images.SetKeyName(499, "key_go.png");
            this.imageList.Images.SetKeyName(500, "keyboard.png");
            this.imageList.Images.SetKeyName(501, "keyboard_add.png");
            this.imageList.Images.SetKeyName(502, "keyboard_delete.png");
            this.imageList.Images.SetKeyName(503, "keyboard_magnify.png");
            this.imageList.Images.SetKeyName(504, "layers.png");
            this.imageList.Images.SetKeyName(505, "layout.png");
            this.imageList.Images.SetKeyName(506, "layout_add.png");
            this.imageList.Images.SetKeyName(507, "layout_content.png");
            this.imageList.Images.SetKeyName(508, "layout_delete.png");
            this.imageList.Images.SetKeyName(509, "layout_edit.png");
            this.imageList.Images.SetKeyName(510, "layout_error.png");
            this.imageList.Images.SetKeyName(511, "layout_header.png");
            this.imageList.Images.SetKeyName(512, "layout_link.png");
            this.imageList.Images.SetKeyName(513, "layout_sidebar.png");
            this.imageList.Images.SetKeyName(514, "lightbulb.png");
            this.imageList.Images.SetKeyName(515, "lightbulb_add.png");
            this.imageList.Images.SetKeyName(516, "lightbulb_delete.png");
            this.imageList.Images.SetKeyName(517, "lightbulb_off.png");
            this.imageList.Images.SetKeyName(518, "lightning.png");
            this.imageList.Images.SetKeyName(519, "lightning_add.png");
            this.imageList.Images.SetKeyName(520, "lightning_delete.png");
            this.imageList.Images.SetKeyName(521, "lightning_go.png");
            this.imageList.Images.SetKeyName(522, "link.png");
            this.imageList.Images.SetKeyName(523, "link_add.png");
            this.imageList.Images.SetKeyName(524, "link_break.png");
            this.imageList.Images.SetKeyName(525, "link_delete.png");
            this.imageList.Images.SetKeyName(526, "link_edit.png");
            this.imageList.Images.SetKeyName(527, "link_error.png");
            this.imageList.Images.SetKeyName(528, "link_go.png");
            this.imageList.Images.SetKeyName(529, "lock.png");
            this.imageList.Images.SetKeyName(530, "lock_add.png");
            this.imageList.Images.SetKeyName(531, "lock_break.png");
            this.imageList.Images.SetKeyName(532, "lock_delete.png");
            this.imageList.Images.SetKeyName(533, "lock_edit.png");
            this.imageList.Images.SetKeyName(534, "lock_go.png");
            this.imageList.Images.SetKeyName(535, "lock_open.png");
            this.imageList.Images.SetKeyName(536, "lorry.png");
            this.imageList.Images.SetKeyName(537, "lorry_add.png");
            this.imageList.Images.SetKeyName(538, "lorry_delete.png");
            this.imageList.Images.SetKeyName(539, "lorry_error.png");
            this.imageList.Images.SetKeyName(540, "lorry_flatbed.png");
            this.imageList.Images.SetKeyName(541, "lorry_go.png");
            this.imageList.Images.SetKeyName(542, "lorry_link.png");
            this.imageList.Images.SetKeyName(543, "magifier_zoom_out.png");
            this.imageList.Images.SetKeyName(544, "magnifier.png");
            this.imageList.Images.SetKeyName(545, "magnifier_zoom_in.png");
            this.imageList.Images.SetKeyName(546, "male.png");
            this.imageList.Images.SetKeyName(547, "map.png");
            this.imageList.Images.SetKeyName(548, "map_add.png");
            this.imageList.Images.SetKeyName(549, "map_delete.png");
            this.imageList.Images.SetKeyName(550, "map_edit.png");
            this.imageList.Images.SetKeyName(551, "map_go.png");
            this.imageList.Images.SetKeyName(552, "map_magnify.png");
            this.imageList.Images.SetKeyName(553, "medal_bronze_1.png");
            this.imageList.Images.SetKeyName(554, "medal_bronze_2.png");
            this.imageList.Images.SetKeyName(555, "medal_bronze_3.png");
            this.imageList.Images.SetKeyName(556, "medal_bronze_add.png");
            this.imageList.Images.SetKeyName(557, "medal_bronze_delete.png");
            this.imageList.Images.SetKeyName(558, "medal_gold_1.png");
            this.imageList.Images.SetKeyName(559, "medal_gold_2.png");
            this.imageList.Images.SetKeyName(560, "medal_gold_3.png");
            this.imageList.Images.SetKeyName(561, "medal_gold_add.png");
            this.imageList.Images.SetKeyName(562, "medal_gold_delete.png");
            this.imageList.Images.SetKeyName(563, "medal_silver_1.png");
            this.imageList.Images.SetKeyName(564, "medal_silver_2.png");
            this.imageList.Images.SetKeyName(565, "medal_silver_3.png");
            this.imageList.Images.SetKeyName(566, "medal_silver_add.png");
            this.imageList.Images.SetKeyName(567, "medal_silver_delete.png");
            this.imageList.Images.SetKeyName(568, "money.png");
            this.imageList.Images.SetKeyName(569, "money_add.png");
            this.imageList.Images.SetKeyName(570, "money_delete.png");
            this.imageList.Images.SetKeyName(571, "money_dollar.png");
            this.imageList.Images.SetKeyName(572, "money_euro.png");
            this.imageList.Images.SetKeyName(573, "money_pound.png");
            this.imageList.Images.SetKeyName(574, "money_yen.png");
            this.imageList.Images.SetKeyName(575, "monitor.png");
            this.imageList.Images.SetKeyName(576, "monitor_add.png");
            this.imageList.Images.SetKeyName(577, "monitor_delete.png");
            this.imageList.Images.SetKeyName(578, "monitor_edit.png");
            this.imageList.Images.SetKeyName(579, "monitor_error.png");
            this.imageList.Images.SetKeyName(580, "monitor_go.png");
            this.imageList.Images.SetKeyName(581, "monitor_lightning.png");
            this.imageList.Images.SetKeyName(582, "monitor_link.png");
            this.imageList.Images.SetKeyName(583, "mouse.png");
            this.imageList.Images.SetKeyName(584, "mouse_add.png");
            this.imageList.Images.SetKeyName(585, "mouse_delete.png");
            this.imageList.Images.SetKeyName(586, "mouse_error.png");
            this.imageList.Images.SetKeyName(587, "music.png");
            this.imageList.Images.SetKeyName(588, "new.png");
            this.imageList.Images.SetKeyName(589, "newspaper.png");
            this.imageList.Images.SetKeyName(590, "newspaper_add.png");
            this.imageList.Images.SetKeyName(591, "newspaper_delete.png");
            this.imageList.Images.SetKeyName(592, "newspaper_go.png");
            this.imageList.Images.SetKeyName(593, "newspaper_link.png");
            this.imageList.Images.SetKeyName(594, "note.png");
            this.imageList.Images.SetKeyName(595, "note_add.png");
            this.imageList.Images.SetKeyName(596, "note_delete.png");
            this.imageList.Images.SetKeyName(597, "note_edit.png");
            this.imageList.Images.SetKeyName(598, "note_error.png");
            this.imageList.Images.SetKeyName(599, "note_go.png");
            this.imageList.Images.SetKeyName(600, "overlays.png");
            this.imageList.Images.SetKeyName(601, "package.png");
            this.imageList.Images.SetKeyName(602, "package_add.png");
            this.imageList.Images.SetKeyName(603, "package_delete.png");
            this.imageList.Images.SetKeyName(604, "package_go.png");
            this.imageList.Images.SetKeyName(605, "package_green.png");
            this.imageList.Images.SetKeyName(606, "package_link.png");
            this.imageList.Images.SetKeyName(607, "page.png");
            this.imageList.Images.SetKeyName(608, "page_add.png");
            this.imageList.Images.SetKeyName(609, "page_attach.png");
            this.imageList.Images.SetKeyName(610, "page_code.png");
            this.imageList.Images.SetKeyName(611, "page_copy.png");
            this.imageList.Images.SetKeyName(612, "page_delete.png");
            this.imageList.Images.SetKeyName(613, "page_edit.png");
            this.imageList.Images.SetKeyName(614, "page_error.png");
            this.imageList.Images.SetKeyName(615, "page_excel.png");
            this.imageList.Images.SetKeyName(616, "page_find.png");
            this.imageList.Images.SetKeyName(617, "page_gear.png");
            this.imageList.Images.SetKeyName(618, "page_go.png");
            this.imageList.Images.SetKeyName(619, "page_green.png");
            this.imageList.Images.SetKeyName(620, "page_key.png");
            this.imageList.Images.SetKeyName(621, "page_lightning.png");
            this.imageList.Images.SetKeyName(622, "page_link.png");
            this.imageList.Images.SetKeyName(623, "page_paintbrush.png");
            this.imageList.Images.SetKeyName(624, "page_paste.png");
            this.imageList.Images.SetKeyName(625, "page_red.png");
            this.imageList.Images.SetKeyName(626, "page_refresh.png");
            this.imageList.Images.SetKeyName(627, "page_save.png");
            this.imageList.Images.SetKeyName(628, "page_white.png");
            this.imageList.Images.SetKeyName(629, "page_white_acrobat.png");
            this.imageList.Images.SetKeyName(630, "page_white_actionscript.png");
            this.imageList.Images.SetKeyName(631, "page_white_add.png");
            this.imageList.Images.SetKeyName(632, "page_white_c.png");
            this.imageList.Images.SetKeyName(633, "page_white_camera.png");
            this.imageList.Images.SetKeyName(634, "page_white_cd.png");
            this.imageList.Images.SetKeyName(635, "page_white_code.png");
            this.imageList.Images.SetKeyName(636, "page_white_code_red.png");
            this.imageList.Images.SetKeyName(637, "page_white_coldfusion.png");
            this.imageList.Images.SetKeyName(638, "page_white_compressed.png");
            this.imageList.Images.SetKeyName(639, "page_white_copy.png");
            this.imageList.Images.SetKeyName(640, "page_white_cplusplus.png");
            this.imageList.Images.SetKeyName(641, "page_white_csharp.png");
            this.imageList.Images.SetKeyName(642, "page_white_cup.png");
            this.imageList.Images.SetKeyName(643, "page_white_database.png");
            this.imageList.Images.SetKeyName(644, "page_white_delete.png");
            this.imageList.Images.SetKeyName(645, "page_white_dvd.png");
            this.imageList.Images.SetKeyName(646, "page_white_edit.png");
            this.imageList.Images.SetKeyName(647, "page_white_error.png");
            this.imageList.Images.SetKeyName(648, "page_white_excel.png");
            this.imageList.Images.SetKeyName(649, "page_white_find.png");
            this.imageList.Images.SetKeyName(650, "page_white_flash.png");
            this.imageList.Images.SetKeyName(651, "page_white_freehand.png");
            this.imageList.Images.SetKeyName(652, "page_white_gear.png");
            this.imageList.Images.SetKeyName(653, "page_white_get.png");
            this.imageList.Images.SetKeyName(654, "page_white_go.png");
            this.imageList.Images.SetKeyName(655, "page_white_h.png");
            this.imageList.Images.SetKeyName(656, "page_white_horizontal.png");
            this.imageList.Images.SetKeyName(657, "page_white_key.png");
            this.imageList.Images.SetKeyName(658, "page_white_lightning.png");
            this.imageList.Images.SetKeyName(659, "page_white_link.png");
            this.imageList.Images.SetKeyName(660, "page_white_magnify.png");
            this.imageList.Images.SetKeyName(661, "page_white_medal.png");
            this.imageList.Images.SetKeyName(662, "page_white_office.png");
            this.imageList.Images.SetKeyName(663, "page_white_paint.png");
            this.imageList.Images.SetKeyName(664, "page_white_paintbrush.png");
            this.imageList.Images.SetKeyName(665, "page_white_paste.png");
            this.imageList.Images.SetKeyName(666, "page_white_php.png");
            this.imageList.Images.SetKeyName(667, "page_white_picture.png");
            this.imageList.Images.SetKeyName(668, "page_white_powerpoint.png");
            this.imageList.Images.SetKeyName(669, "page_white_put.png");
            this.imageList.Images.SetKeyName(670, "page_white_ruby.png");
            this.imageList.Images.SetKeyName(671, "page_white_stack.png");
            this.imageList.Images.SetKeyName(672, "page_white_star.png");
            this.imageList.Images.SetKeyName(673, "page_white_swoosh.png");
            this.imageList.Images.SetKeyName(674, "page_white_text.png");
            this.imageList.Images.SetKeyName(675, "page_white_text_width.png");
            this.imageList.Images.SetKeyName(676, "page_white_tux.png");
            this.imageList.Images.SetKeyName(677, "page_white_vector.png");
            this.imageList.Images.SetKeyName(678, "page_white_visualstudio.png");
            this.imageList.Images.SetKeyName(679, "page_white_width.png");
            this.imageList.Images.SetKeyName(680, "page_white_word.png");
            this.imageList.Images.SetKeyName(681, "page_white_world.png");
            this.imageList.Images.SetKeyName(682, "page_white_wrench.png");
            this.imageList.Images.SetKeyName(683, "page_white_zip.png");
            this.imageList.Images.SetKeyName(684, "page_word.png");
            this.imageList.Images.SetKeyName(685, "page_world.png");
            this.imageList.Images.SetKeyName(686, "paintbrush.png");
            this.imageList.Images.SetKeyName(687, "paintcan.png");
            this.imageList.Images.SetKeyName(688, "palette.png");
            this.imageList.Images.SetKeyName(689, "paste_plain.png");
            this.imageList.Images.SetKeyName(690, "paste_word.png");
            this.imageList.Images.SetKeyName(691, "pencil.png");
            this.imageList.Images.SetKeyName(692, "pencil_add.png");
            this.imageList.Images.SetKeyName(693, "pencil_delete.png");
            this.imageList.Images.SetKeyName(694, "pencil_go.png");
            this.imageList.Images.SetKeyName(695, "phone.png");
            this.imageList.Images.SetKeyName(696, "phone_add.png");
            this.imageList.Images.SetKeyName(697, "phone_delete.png");
            this.imageList.Images.SetKeyName(698, "phone_sound.png");
            this.imageList.Images.SetKeyName(699, "photo.png");
            this.imageList.Images.SetKeyName(700, "photo_add.png");
            this.imageList.Images.SetKeyName(701, "photo_delete.png");
            this.imageList.Images.SetKeyName(702, "photo_link.png");
            this.imageList.Images.SetKeyName(703, "photos.png");
            this.imageList.Images.SetKeyName(704, "picture.png");
            this.imageList.Images.SetKeyName(705, "picture_add.png");
            this.imageList.Images.SetKeyName(706, "picture_delete.png");
            this.imageList.Images.SetKeyName(707, "picture_edit.png");
            this.imageList.Images.SetKeyName(708, "picture_empty.png");
            this.imageList.Images.SetKeyName(709, "picture_error.png");
            this.imageList.Images.SetKeyName(710, "picture_go.png");
            this.imageList.Images.SetKeyName(711, "picture_key.png");
            this.imageList.Images.SetKeyName(712, "picture_link.png");
            this.imageList.Images.SetKeyName(713, "picture_save.png");
            this.imageList.Images.SetKeyName(714, "pictures.png");
            this.imageList.Images.SetKeyName(715, "pilcrow.png");
            this.imageList.Images.SetKeyName(716, "pill.png");
            this.imageList.Images.SetKeyName(717, "pill_add.png");
            this.imageList.Images.SetKeyName(718, "pill_delete.png");
            this.imageList.Images.SetKeyName(719, "pill_go.png");
            this.imageList.Images.SetKeyName(720, "plugin.png");
            this.imageList.Images.SetKeyName(721, "plugin_add.png");
            this.imageList.Images.SetKeyName(722, "plugin_delete.png");
            this.imageList.Images.SetKeyName(723, "plugin_disabled.png");
            this.imageList.Images.SetKeyName(724, "plugin_edit.png");
            this.imageList.Images.SetKeyName(725, "plugin_error.png");
            this.imageList.Images.SetKeyName(726, "plugin_go.png");
            this.imageList.Images.SetKeyName(727, "plugin_link.png");
            this.imageList.Images.SetKeyName(728, "printer.png");
            this.imageList.Images.SetKeyName(729, "printer_add.png");
            this.imageList.Images.SetKeyName(730, "printer_delete.png");
            this.imageList.Images.SetKeyName(731, "printer_empty.png");
            this.imageList.Images.SetKeyName(732, "printer_error.png");
            this.imageList.Images.SetKeyName(733, "rainbow.png");
            this.imageList.Images.SetKeyName(734, "report.png");
            this.imageList.Images.SetKeyName(735, "report_add.png");
            this.imageList.Images.SetKeyName(736, "report_delete.png");
            this.imageList.Images.SetKeyName(737, "report_disk.png");
            this.imageList.Images.SetKeyName(738, "report_edit.png");
            this.imageList.Images.SetKeyName(739, "report_go.png");
            this.imageList.Images.SetKeyName(740, "report_key.png");
            this.imageList.Images.SetKeyName(741, "report_link.png");
            this.imageList.Images.SetKeyName(742, "report_magnify.png");
            this.imageList.Images.SetKeyName(743, "report_picture.png");
            this.imageList.Images.SetKeyName(744, "report_user.png");
            this.imageList.Images.SetKeyName(745, "report_word.png");
            this.imageList.Images.SetKeyName(746, "resultset_first.png");
            this.imageList.Images.SetKeyName(747, "resultset_last.png");
            this.imageList.Images.SetKeyName(748, "resultset_next.png");
            this.imageList.Images.SetKeyName(749, "resultset_previous.png");
            this.imageList.Images.SetKeyName(750, "rosette.png");
            this.imageList.Images.SetKeyName(751, "rss.png");
            this.imageList.Images.SetKeyName(752, "rss_add.png");
            this.imageList.Images.SetKeyName(753, "rss_delete.png");
            this.imageList.Images.SetKeyName(754, "rss_go.png");
            this.imageList.Images.SetKeyName(755, "rss_valid.png");
            this.imageList.Images.SetKeyName(756, "ruby.png");
            this.imageList.Images.SetKeyName(757, "ruby_add.png");
            this.imageList.Images.SetKeyName(758, "ruby_delete.png");
            this.imageList.Images.SetKeyName(759, "ruby_gear.png");
            this.imageList.Images.SetKeyName(760, "ruby_get.png");
            this.imageList.Images.SetKeyName(761, "ruby_go.png");
            this.imageList.Images.SetKeyName(762, "ruby_key.png");
            this.imageList.Images.SetKeyName(763, "ruby_link.png");
            this.imageList.Images.SetKeyName(764, "ruby_put.png");
            this.imageList.Images.SetKeyName(765, "script.png");
            this.imageList.Images.SetKeyName(766, "script_add.png");
            this.imageList.Images.SetKeyName(767, "script_code.png");
            this.imageList.Images.SetKeyName(768, "script_code_red.png");
            this.imageList.Images.SetKeyName(769, "script_delete.png");
            this.imageList.Images.SetKeyName(770, "script_edit.png");
            this.imageList.Images.SetKeyName(771, "script_error.png");
            this.imageList.Images.SetKeyName(772, "script_gear.png");
            this.imageList.Images.SetKeyName(773, "script_go.png");
            this.imageList.Images.SetKeyName(774, "script_key.png");
            this.imageList.Images.SetKeyName(775, "script_lightning.png");
            this.imageList.Images.SetKeyName(776, "script_link.png");
            this.imageList.Images.SetKeyName(777, "script_palette.png");
            this.imageList.Images.SetKeyName(778, "script_save.png");
            this.imageList.Images.SetKeyName(779, "server.png");
            this.imageList.Images.SetKeyName(780, "server_add.png");
            this.imageList.Images.SetKeyName(781, "server_chart.png");
            this.imageList.Images.SetKeyName(782, "server_compressed.png");
            this.imageList.Images.SetKeyName(783, "server_connect.png");
            this.imageList.Images.SetKeyName(784, "server_database.png");
            this.imageList.Images.SetKeyName(785, "server_delete.png");
            this.imageList.Images.SetKeyName(786, "server_edit.png");
            this.imageList.Images.SetKeyName(787, "server_error.png");
            this.imageList.Images.SetKeyName(788, "server_go.png");
            this.imageList.Images.SetKeyName(789, "server_key.png");
            this.imageList.Images.SetKeyName(790, "server_lightning.png");
            this.imageList.Images.SetKeyName(791, "server_link.png");
            this.imageList.Images.SetKeyName(792, "server_uncompressed.png");
            this.imageList.Images.SetKeyName(793, "shading.png");
            this.imageList.Images.SetKeyName(794, "shape_align_bottom.png");
            this.imageList.Images.SetKeyName(795, "shape_align_center.png");
            this.imageList.Images.SetKeyName(796, "shape_align_left.png");
            this.imageList.Images.SetKeyName(797, "shape_align_middle.png");
            this.imageList.Images.SetKeyName(798, "shape_align_right.png");
            this.imageList.Images.SetKeyName(799, "shape_align_top.png");
            this.imageList.Images.SetKeyName(800, "shape_flip_horizontal.png");
            this.imageList.Images.SetKeyName(801, "shape_flip_vertical.png");
            this.imageList.Images.SetKeyName(802, "shape_group.png");
            this.imageList.Images.SetKeyName(803, "shape_handles.png");
            this.imageList.Images.SetKeyName(804, "shape_move_back.png");
            this.imageList.Images.SetKeyName(805, "shape_move_backwards.png");
            this.imageList.Images.SetKeyName(806, "shape_move_forwards.png");
            this.imageList.Images.SetKeyName(807, "shape_move_front.png");
            this.imageList.Images.SetKeyName(808, "shape_rotate_anticlockwise.png");
            this.imageList.Images.SetKeyName(809, "shape_rotate_clockwise.png");
            this.imageList.Images.SetKeyName(810, "shape_square.png");
            this.imageList.Images.SetKeyName(811, "shape_square_add.png");
            this.imageList.Images.SetKeyName(812, "shape_square_delete.png");
            this.imageList.Images.SetKeyName(813, "shape_square_edit.png");
            this.imageList.Images.SetKeyName(814, "shape_square_error.png");
            this.imageList.Images.SetKeyName(815, "shape_square_go.png");
            this.imageList.Images.SetKeyName(816, "shape_square_key.png");
            this.imageList.Images.SetKeyName(817, "shape_square_link.png");
            this.imageList.Images.SetKeyName(818, "shape_ungroup.png");
            this.imageList.Images.SetKeyName(819, "shield.png");
            this.imageList.Images.SetKeyName(820, "shield_add.png");
            this.imageList.Images.SetKeyName(821, "shield_delete.png");
            this.imageList.Images.SetKeyName(822, "shield_go.png");
            this.imageList.Images.SetKeyName(823, "sitemap.png");
            this.imageList.Images.SetKeyName(824, "sitemap_color.png");
            this.imageList.Images.SetKeyName(825, "sound.png");
            this.imageList.Images.SetKeyName(826, "sound_add.png");
            this.imageList.Images.SetKeyName(827, "sound_delete.png");
            this.imageList.Images.SetKeyName(828, "sound_low.png");
            this.imageList.Images.SetKeyName(829, "sound_mute.png");
            this.imageList.Images.SetKeyName(830, "sound_none.png");
            this.imageList.Images.SetKeyName(831, "spellcheck.png");
            this.imageList.Images.SetKeyName(832, "sport_8ball.png");
            this.imageList.Images.SetKeyName(833, "sport_basketball.png");
            this.imageList.Images.SetKeyName(834, "sport_football.png");
            this.imageList.Images.SetKeyName(835, "sport_golf.png");
            this.imageList.Images.SetKeyName(836, "sport_raquet.png");
            this.imageList.Images.SetKeyName(837, "sport_shuttlecock.png");
            this.imageList.Images.SetKeyName(838, "sport_soccer.png");
            this.imageList.Images.SetKeyName(839, "sport_tennis.png");
            this.imageList.Images.SetKeyName(840, "star.png");
            this.imageList.Images.SetKeyName(841, "status_away.png");
            this.imageList.Images.SetKeyName(842, "status_busy.png");
            this.imageList.Images.SetKeyName(843, "status_offline.png");
            this.imageList.Images.SetKeyName(844, "status_online.png");
            this.imageList.Images.SetKeyName(845, "stop.png");
            this.imageList.Images.SetKeyName(846, "style.png");
            this.imageList.Images.SetKeyName(847, "style_add.png");
            this.imageList.Images.SetKeyName(848, "style_delete.png");
            this.imageList.Images.SetKeyName(849, "style_edit.png");
            this.imageList.Images.SetKeyName(850, "style_go.png");
            this.imageList.Images.SetKeyName(851, "sum.png");
            this.imageList.Images.SetKeyName(852, "tab.png");
            this.imageList.Images.SetKeyName(853, "tab_add.png");
            this.imageList.Images.SetKeyName(854, "tab_delete.png");
            this.imageList.Images.SetKeyName(855, "tab_edit.png");
            this.imageList.Images.SetKeyName(856, "tab_go.png");
            this.imageList.Images.SetKeyName(857, "table.png");
            this.imageList.Images.SetKeyName(858, "table_add.png");
            this.imageList.Images.SetKeyName(859, "table_delete.png");
            this.imageList.Images.SetKeyName(860, "table_edit.png");
            this.imageList.Images.SetKeyName(861, "table_error.png");
            this.imageList.Images.SetKeyName(862, "table_gear.png");
            this.imageList.Images.SetKeyName(863, "table_go.png");
            this.imageList.Images.SetKeyName(864, "table_key.png");
            this.imageList.Images.SetKeyName(865, "table_lightning.png");
            this.imageList.Images.SetKeyName(866, "table_link.png");
            this.imageList.Images.SetKeyName(867, "table_multiple.png");
            this.imageList.Images.SetKeyName(868, "table_refresh.png");
            this.imageList.Images.SetKeyName(869, "table_relationship.png");
            this.imageList.Images.SetKeyName(870, "table_row_delete.png");
            this.imageList.Images.SetKeyName(871, "table_row_insert.png");
            this.imageList.Images.SetKeyName(872, "table_save.png");
            this.imageList.Images.SetKeyName(873, "table_sort.png");
            this.imageList.Images.SetKeyName(874, "tag.png");
            this.imageList.Images.SetKeyName(875, "tag_blue.png");
            this.imageList.Images.SetKeyName(876, "tag_blue_add.png");
            this.imageList.Images.SetKeyName(877, "tag_blue_delete.png");
            this.imageList.Images.SetKeyName(878, "tag_blue_edit.png");
            this.imageList.Images.SetKeyName(879, "tag_green.png");
            this.imageList.Images.SetKeyName(880, "tag_orange.png");
            this.imageList.Images.SetKeyName(881, "tag_pink.png");
            this.imageList.Images.SetKeyName(882, "tag_purple.png");
            this.imageList.Images.SetKeyName(883, "tag_red.png");
            this.imageList.Images.SetKeyName(884, "tag_yellow.png");
            this.imageList.Images.SetKeyName(885, "telephone.png");
            this.imageList.Images.SetKeyName(886, "telephone_add.png");
            this.imageList.Images.SetKeyName(887, "telephone_delete.png");
            this.imageList.Images.SetKeyName(888, "telephone_edit.png");
            this.imageList.Images.SetKeyName(889, "telephone_error.png");
            this.imageList.Images.SetKeyName(890, "telephone_go.png");
            this.imageList.Images.SetKeyName(891, "telephone_key.png");
            this.imageList.Images.SetKeyName(892, "telephone_link.png");
            this.imageList.Images.SetKeyName(893, "television.png");
            this.imageList.Images.SetKeyName(894, "television_add.png");
            this.imageList.Images.SetKeyName(895, "television_delete.png");
            this.imageList.Images.SetKeyName(896, "text_align_center.png");
            this.imageList.Images.SetKeyName(897, "text_align_justify.png");
            this.imageList.Images.SetKeyName(898, "text_align_left.png");
            this.imageList.Images.SetKeyName(899, "text_align_right.png");
            this.imageList.Images.SetKeyName(900, "text_allcaps.png");
            this.imageList.Images.SetKeyName(901, "text_bold.png");
            this.imageList.Images.SetKeyName(902, "text_columns.png");
            this.imageList.Images.SetKeyName(903, "text_dropcaps.png");
            this.imageList.Images.SetKeyName(904, "text_heading_1.png");
            this.imageList.Images.SetKeyName(905, "text_heading_2.png");
            this.imageList.Images.SetKeyName(906, "text_heading_3.png");
            this.imageList.Images.SetKeyName(907, "text_heading_4.png");
            this.imageList.Images.SetKeyName(908, "text_heading_5.png");
            this.imageList.Images.SetKeyName(909, "text_heading_6.png");
            this.imageList.Images.SetKeyName(910, "text_horizontalrule.png");
            this.imageList.Images.SetKeyName(911, "text_indent.png");
            this.imageList.Images.SetKeyName(912, "text_indent_remove.png");
            this.imageList.Images.SetKeyName(913, "text_italic.png");
            this.imageList.Images.SetKeyName(914, "text_kerning.png");
            this.imageList.Images.SetKeyName(915, "text_letter_omega.png");
            this.imageList.Images.SetKeyName(916, "text_letterspacing.png");
            this.imageList.Images.SetKeyName(917, "text_linespacing.png");
            this.imageList.Images.SetKeyName(918, "text_list_bullets.png");
            this.imageList.Images.SetKeyName(919, "text_list_numbers.png");
            this.imageList.Images.SetKeyName(920, "text_lowercase.png");
            this.imageList.Images.SetKeyName(921, "text_padding_bottom.png");
            this.imageList.Images.SetKeyName(922, "text_padding_left.png");
            this.imageList.Images.SetKeyName(923, "text_padding_right.png");
            this.imageList.Images.SetKeyName(924, "text_padding_top.png");
            this.imageList.Images.SetKeyName(925, "text_replace.png");
            this.imageList.Images.SetKeyName(926, "text_signature.png");
            this.imageList.Images.SetKeyName(927, "text_smallcaps.png");
            this.imageList.Images.SetKeyName(928, "text_strikethrough.png");
            this.imageList.Images.SetKeyName(929, "text_subscript.png");
            this.imageList.Images.SetKeyName(930, "text_superscript.png");
            this.imageList.Images.SetKeyName(931, "text_underline.png");
            this.imageList.Images.SetKeyName(932, "text_uppercase.png");
            this.imageList.Images.SetKeyName(933, "textfield.png");
            this.imageList.Images.SetKeyName(934, "textfield_add.png");
            this.imageList.Images.SetKeyName(935, "textfield_delete.png");
            this.imageList.Images.SetKeyName(936, "textfield_key.png");
            this.imageList.Images.SetKeyName(937, "textfield_rename.png");
            this.imageList.Images.SetKeyName(938, "thumb_down.png");
            this.imageList.Images.SetKeyName(939, "thumb_up.png");
            this.imageList.Images.SetKeyName(940, "tick.png");
            this.imageList.Images.SetKeyName(941, "time.png");
            this.imageList.Images.SetKeyName(942, "time_add.png");
            this.imageList.Images.SetKeyName(943, "time_delete.png");
            this.imageList.Images.SetKeyName(944, "time_go.png");
            this.imageList.Images.SetKeyName(945, "timeline_marker.png");
            this.imageList.Images.SetKeyName(946, "transmit.png");
            this.imageList.Images.SetKeyName(947, "transmit_add.png");
            this.imageList.Images.SetKeyName(948, "transmit_blue.png");
            this.imageList.Images.SetKeyName(949, "transmit_delete.png");
            this.imageList.Images.SetKeyName(950, "transmit_edit.png");
            this.imageList.Images.SetKeyName(951, "transmit_error.png");
            this.imageList.Images.SetKeyName(952, "transmit_go.png");
            this.imageList.Images.SetKeyName(953, "tux.png");
            this.imageList.Images.SetKeyName(954, "user.png");
            this.imageList.Images.SetKeyName(955, "user_add.png");
            this.imageList.Images.SetKeyName(956, "user_comment.png");
            this.imageList.Images.SetKeyName(957, "user_delete.png");
            this.imageList.Images.SetKeyName(958, "user_edit.png");
            this.imageList.Images.SetKeyName(959, "user_female.png");
            this.imageList.Images.SetKeyName(960, "user_go.png");
            this.imageList.Images.SetKeyName(961, "user_gray.png");
            this.imageList.Images.SetKeyName(962, "user_green.png");
            this.imageList.Images.SetKeyName(963, "user_orange.png");
            this.imageList.Images.SetKeyName(964, "user_red.png");
            this.imageList.Images.SetKeyName(965, "user_suit.png");
            this.imageList.Images.SetKeyName(966, "vcard.png");
            this.imageList.Images.SetKeyName(967, "vcard_add.png");
            this.imageList.Images.SetKeyName(968, "vcard_delete.png");
            this.imageList.Images.SetKeyName(969, "vcard_edit.png");
            this.imageList.Images.SetKeyName(970, "vector.png");
            this.imageList.Images.SetKeyName(971, "vector_add.png");
            this.imageList.Images.SetKeyName(972, "vector_delete.png");
            this.imageList.Images.SetKeyName(973, "wand.png");
            this.imageList.Images.SetKeyName(974, "weather_clouds.png");
            this.imageList.Images.SetKeyName(975, "weather_cloudy.png");
            this.imageList.Images.SetKeyName(976, "weather_lightning.png");
            this.imageList.Images.SetKeyName(977, "weather_rain.png");
            this.imageList.Images.SetKeyName(978, "weather_snow.png");
            this.imageList.Images.SetKeyName(979, "weather_sun.png");
            this.imageList.Images.SetKeyName(980, "webcam.png");
            this.imageList.Images.SetKeyName(981, "webcam_add.png");
            this.imageList.Images.SetKeyName(982, "webcam_delete.png");
            this.imageList.Images.SetKeyName(983, "webcam_error.png");
            this.imageList.Images.SetKeyName(984, "world.png");
            this.imageList.Images.SetKeyName(985, "world_add.png");
            this.imageList.Images.SetKeyName(986, "world_delete.png");
            this.imageList.Images.SetKeyName(987, "world_edit.png");
            this.imageList.Images.SetKeyName(988, "world_go.png");
            this.imageList.Images.SetKeyName(989, "world_link.png");
            this.imageList.Images.SetKeyName(990, "wrench.png");
            this.imageList.Images.SetKeyName(991, "wrench_orange.png");
            this.imageList.Images.SetKeyName(992, "xhtml.png");
            this.imageList.Images.SetKeyName(993, "xhtml_add.png");
            this.imageList.Images.SetKeyName(994, "xhtml_delete.png");
            this.imageList.Images.SetKeyName(995, "xhtml_go.png");
            this.imageList.Images.SetKeyName(996, "xhtml_valid.png");
            this.imageList.Images.SetKeyName(997, "zoom.png");
            this.imageList.Images.SetKeyName(998, "zoom_in.png");
            this.imageList.Images.SetKeyName(999, "zoom_out.png");
            // 
            // propertyNode
            // 
            this.propertyNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyNode.Enabled = false;
            this.propertyNode.HelpVisible = false;
            this.propertyNode.Location = new System.Drawing.Point(0, 0);
            this.propertyNode.Name = "propertyNode";
            this.propertyNode.Size = new System.Drawing.Size(158, 93);
            this.propertyNode.TabIndex = 0;
            this.propertyNode.ToolbarVisible = false;
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.tabPage1);
            this.tabView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabView.Location = new System.Drawing.Point(0, 0);
            this.tabView.Name = "tabView";
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(618, 222);
            this.tabView.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelPlotControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelPlotControl1
            // 
            this.panelPlotControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlotControl1.Location = new System.Drawing.Point(3, 3);
            this.panelPlotControl1.Name = "panelPlotControl1";
            this.panelPlotControl1.Size = new System.Drawing.Size(604, 190);
            this.panelPlotControl1.TabIndex = 0;
            // 
            // openFileDialogASL
            // 
            this.openFileDialogASL.Filter = "evalab asl (*.xml) |*.xml";
            this.openFileDialogASL.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogASL_FileOk_1);
            // 
            // contextMenuStripTree
            // 
            this.contextMenuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.showToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.referenceToolStripMenuItem,
            this.saccadeToolStripMenuItem,
            this.fixationToolStripMenuItem,
            this.rOIToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.utilitiesToolStripMenuItem,
            this.propertiesToolStripMenuItem1,
            this.purgeAnalysisToolStripMenuItem});
            this.contextMenuStripTree.Name = "contextMenuStripTree";
            this.contextMenuStripTree.Size = new System.Drawing.Size(153, 290);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.duplicateToolStripMenuItem.Text = "Clone";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showVsTimeToolStripMenuItem,
            this.showXYToolStripMenuItem,
            this.mainSequenceToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // showVsTimeToolStripMenuItem
            // 
            this.showVsTimeToolStripMenuItem.Name = "showVsTimeToolStripMenuItem";
            this.showVsTimeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showVsTimeToolStripMenuItem.Text = "Show vs Time";
            this.showVsTimeToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // showXYToolStripMenuItem
            // 
            this.showXYToolStripMenuItem.Name = "showXYToolStripMenuItem";
            this.showXYToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showXYToolStripMenuItem.Text = "Show XY";
            this.showXYToolStripMenuItem.Click += new System.EventHandler(this.showXYToolStripMenuItem_Click);
            // 
            // mainSequenceToolStripMenuItem
            // 
            this.mainSequenceToolStripMenuItem.Name = "mainSequenceToolStripMenuItem";
            this.mainSequenceToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.mainSequenceToolStripMenuItem.Text = "Main Sequence";
            this.mainSequenceToolStripMenuItem.Click += new System.EventHandler(this.mainSequenceToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterApplyToolStripMenuItem,
            this.denoiseToolStripMenuItem,
            this.buttToolStripMenuItem,
            this.medianToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // filterApplyToolStripMenuItem
            // 
            this.filterApplyToolStripMenuItem.Name = "filterApplyToolStripMenuItem";
            this.filterApplyToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.filterApplyToolStripMenuItem.Text = "Filter ...";
            this.filterApplyToolStripMenuItem.Click += new System.EventHandler(this.filterApplyToolStripMenuItem_Click);
            // 
            // denoiseToolStripMenuItem
            // 
            this.denoiseToolStripMenuItem.Name = "denoiseToolStripMenuItem";
            this.denoiseToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.denoiseToolStripMenuItem.Text = "Denoise ...";
            this.denoiseToolStripMenuItem.Click += new System.EventHandler(this.denoiseToolStripMenuItem_Click);
            // 
            // buttToolStripMenuItem
            // 
            this.buttToolStripMenuItem.Name = "buttToolStripMenuItem";
            this.buttToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.buttToolStripMenuItem.Text = "Butterworth 50Hz ...";
            this.buttToolStripMenuItem.Click += new System.EventHandler(this.buttToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.medianToolStripMenuItem.Text = "Median ...";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.medianToolStripMenuItem_Click);
            // 
            // referenceToolStripMenuItem
            // 
            this.referenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.computeDistanceToolStripMenuItem});
            this.referenceToolStripMenuItem.Name = "referenceToolStripMenuItem";
            this.referenceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.referenceToolStripMenuItem.Text = "Reference";
            // 
            // computeDistanceToolStripMenuItem
            // 
            this.computeDistanceToolStripMenuItem.Name = "computeDistanceToolStripMenuItem";
            this.computeDistanceToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.computeDistanceToolStripMenuItem.Text = "Compute Distance";
            this.computeDistanceToolStripMenuItem.Click += new System.EventHandler(this.computeDistanceToolStripMenuItem_Click);
            // 
            // saccadeToolStripMenuItem
            // 
            this.saccadeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saccToolStripMenuItem,
            this.byVelocityXToolStripMenuItem,
            this.byVelocityYToolStripMenuItem});
            this.saccadeToolStripMenuItem.Name = "saccadeToolStripMenuItem";
            this.saccadeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saccadeToolStripMenuItem.Text = "Saccade";
            // 
            // saccToolStripMenuItem
            // 
            this.saccToolStripMenuItem.Name = "saccToolStripMenuItem";
            this.saccToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saccToolStripMenuItem.Text = "By Velocity XY ...";
            this.saccToolStripMenuItem.Click += new System.EventHandler(this.saccToolStripMenuItem_Click);
            // 
            // byVelocityXToolStripMenuItem
            // 
            this.byVelocityXToolStripMenuItem.Name = "byVelocityXToolStripMenuItem";
            this.byVelocityXToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byVelocityXToolStripMenuItem.Text = "By Velocity X ...";
            this.byVelocityXToolStripMenuItem.Click += new System.EventHandler(this.byVelocityXToolStripMenuItem_Click);
            // 
            // byVelocityYToolStripMenuItem
            // 
            this.byVelocityYToolStripMenuItem.Name = "byVelocityYToolStripMenuItem";
            this.byVelocityYToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byVelocityYToolStripMenuItem.Text = "By Velocity Y ...";
            this.byVelocityYToolStripMenuItem.Click += new System.EventHandler(this.byVelocityYToolStripMenuItem_Click);
            // 
            // fixationToolStripMenuItem
            // 
            this.fixationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byDispersionToolStripMenuItem});
            this.fixationToolStripMenuItem.Name = "fixationToolStripMenuItem";
            this.fixationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fixationToolStripMenuItem.Text = "Fixation";
            // 
            // byDispersionToolStripMenuItem
            // 
            this.byDispersionToolStripMenuItem.Name = "byDispersionToolStripMenuItem";
            this.byDispersionToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.byDispersionToolStripMenuItem.Text = "By Dispersion ...";
            this.byDispersionToolStripMenuItem.Click += new System.EventHandler(this.byDispersionToolStripMenuItem_Click);
            // 
            // rOIToolStripMenuItem
            // 
            this.rOIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alignDataToROIsToolStripMenuItem,
            this.computeSequencingToolStripMenuItem});
            this.rOIToolStripMenuItem.Name = "rOIToolStripMenuItem";
            this.rOIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rOIToolStripMenuItem.Text = "ROI";
            // 
            // alignDataToROIsToolStripMenuItem
            // 
            this.alignDataToROIsToolStripMenuItem.Name = "alignDataToROIsToolStripMenuItem";
            this.alignDataToROIsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.alignDataToROIsToolStripMenuItem.Text = "Align data to ROIs ...";
            this.alignDataToROIsToolStripMenuItem.Click += new System.EventHandler(this.alignDataToROIsToolStripMenuItem_Click);
            // 
            // computeSequencingToolStripMenuItem
            // 
            this.computeSequencingToolStripMenuItem.Name = "computeSequencingToolStripMenuItem";
            this.computeSequencingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.computeSequencingToolStripMenuItem.Text = "Compute Sequencing ...";
            this.computeSequencingToolStripMenuItem.Click += new System.EventHandler(this.computeSequencingToolStripMenuItem_Click);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.degreeToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            // 
            // degreeToolStripMenuItem
            // 
            this.degreeToolStripMenuItem.Name = "degreeToolStripMenuItem";
            this.degreeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.degreeToolStripMenuItem.Text = "Degree ...";
            this.degreeToolStripMenuItem.Click += new System.EventHandler(this.degreeToolStripMenuItem_Click);
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importExperimentToolStripMenuItem,
            this.findCaseToolStripMenuItem,
            this.splitByReferenceToolStripMenuItem,
            this.exportRawDataToolStripMenuItem,
            this.importFixationsToolStripMenuItem,
            this.importSaccadesToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // importExperimentToolStripMenuItem
            // 
            this.importExperimentToolStripMenuItem.Enabled = false;
            this.importExperimentToolStripMenuItem.Name = "importExperimentToolStripMenuItem";
            this.importExperimentToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.importExperimentToolStripMenuItem.Text = "Import experiment ...";
            // 
            // findCaseToolStripMenuItem
            // 
            this.findCaseToolStripMenuItem.Name = "findCaseToolStripMenuItem";
            this.findCaseToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.findCaseToolStripMenuItem.Text = "Find case ...";
            this.findCaseToolStripMenuItem.Click += new System.EventHandler(this.findCaseToolStripMenuItem_Click);
            // 
            // splitByReferenceToolStripMenuItem
            // 
            this.splitByReferenceToolStripMenuItem.Name = "splitByReferenceToolStripMenuItem";
            this.splitByReferenceToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.splitByReferenceToolStripMenuItem.Text = "Split by reference ...";
            this.splitByReferenceToolStripMenuItem.Click += new System.EventHandler(this.splitByReferenceToolStripMenuItem_Click);
            // 
            // exportRawDataToolStripMenuItem
            // 
            this.exportRawDataToolStripMenuItem.Name = "exportRawDataToolStripMenuItem";
            this.exportRawDataToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportRawDataToolStripMenuItem.Text = "Export Raw Data ...";
            this.exportRawDataToolStripMenuItem.Click += new System.EventHandler(this.exportRawDataToolStripMenuItem_Click);
            // 
            // importFixationsToolStripMenuItem
            // 
            this.importFixationsToolStripMenuItem.Name = "importFixationsToolStripMenuItem";
            this.importFixationsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.importFixationsToolStripMenuItem.Text = "Import Fixations ...";
            this.importFixationsToolStripMenuItem.Click += new System.EventHandler(this.importFixationsToolStripMenuItem_Click);
            // 
            // importSaccadesToolStripMenuItem
            // 
            this.importSaccadesToolStripMenuItem.Name = "importSaccadesToolStripMenuItem";
            this.importSaccadesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.importSaccadesToolStripMenuItem.Text = "Import Saccades ...";
            this.importSaccadesToolStripMenuItem.Click += new System.EventHandler(this.importSaccadesToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem1
            // 
            this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
            this.propertiesToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.propertiesToolStripMenuItem1.Text = "Properties ...";
            this.propertiesToolStripMenuItem1.Click += new System.EventHandler(this.propertiesToolStripMenuItem1_Click);
            // 
            // contextMenuStripCase
            // 
            this.contextMenuStripCase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem1});
            this.contextMenuStripCase.Name = "contextMenuStripCase";
            this.contextMenuStripCase.Size = new System.Drawing.Size(104, 26);
            // 
            // showToolStripMenuItem1
            // 
            this.showToolStripMenuItem1.Name = "showToolStripMenuItem1";
            this.showToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem1.Text = "Show";
            this.showToolStripMenuItem1.Click += new System.EventHandler(this.showToolStripMenuItem1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "EvaLab";
            this.notifyIcon1.Visible = true;
            // 
            // openFileDialogRawEOG
            // 
            this.openFileDialogRawEOG.Filter = "All (*.*)|*.*";
            this.openFileDialogRawEOG.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogRawEOG_FileOk);
            // 
            // openFileDialogEOG
            // 
            this.openFileDialogEOG.FileName = "EXPERIMENT.xml";
            this.openFileDialogEOG.Filter = "Experiment (*.xml)|*.xml";
            this.openFileDialogEOG.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogEOG_FileOk);
            // 
            // openFileDialogRawASL
            // 
            this.openFileDialogRawASL.Filter = "asl (*.txt)|*.txt";
            this.openFileDialogRawASL.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogRawASL_FileOk);
            // 
            // saveFileDialogRawASL
            // 
            this.saveFileDialogRawASL.DefaultExt = "*.txt";
            this.saveFileDialogRawASL.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogRawASL_FileOk);
            // 
            // openFileDialogROI
            // 
            this.openFileDialogROI.Filter = "Space separated (*.csv)|*.csv";
            this.openFileDialogROI.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogROI_FileOk);
            // 
            // openFileDialogProject
            // 
            this.openFileDialogProject.Filter = "Eva project (*.pexml)|*.pexml";
            this.openFileDialogProject.Title = "Open Project";
            this.openFileDialogProject.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogProject_FileOk);
            // 
            // saveFileDialogProject
            // 
            this.saveFileDialogProject.Filter = "Eva project (*.pexml)|*.pexml";
            this.saveFileDialogProject.Title = "Save Project";
            this.saveFileDialogProject.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogProject_FileOk);
            // 
            // contextMenuStripTrial
            // 
            this.contextMenuStripTrial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
            this.contextMenuStripTrial.Name = "contextMenuStripTrial";
            this.contextMenuStripTrial.Size = new System.Drawing.Size(140, 26);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.propertiesToolStripMenuItem.Text = "Properties ...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // openFileDialogRawCSV
            // 
            this.openFileDialogRawCSV.FileName = "DATA";
            this.openFileDialogRawCSV.Filter = "Space separated (*.txt) |*.txt";
            this.openFileDialogRawCSV.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogRawCSV_FileOk);
            // 
            // openROIToolStripMenuItem1
            // 
            this.openROIToolStripMenuItem1.Name = "openROIToolStripMenuItem1";
            this.openROIToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.openROIToolStripMenuItem1.Text = "Open ROI ...";
            // 
            // contextMenuStripROI
            // 
            this.contextMenuStripROI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openROIToolStripMenuItem1});
            this.contextMenuStripROI.Name = "contextMenuStripROI";
            this.contextMenuStripROI.Size = new System.Drawing.Size(138, 26);
            this.contextMenuStripROI.Text = "ROIs";
            this.contextMenuStripROI.Click += new System.EventHandler(this.openROIToolStripMenuItem_Click);
            // 
            // contextMenuStripDATA
            // 
            this.contextMenuStripDATA.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCSVDATAToolStripMenuItem1,
            this.openASLRawDataToolStripMenuItem});
            this.contextMenuStripDATA.Name = "contextMenuStripDATA";
            this.contextMenuStripDATA.Size = new System.Drawing.Size(191, 48);
            // 
            // openCSVDATAToolStripMenuItem1
            // 
            this.openCSVDATAToolStripMenuItem1.Name = "openCSVDATAToolStripMenuItem1";
            this.openCSVDATAToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.openCSVDATAToolStripMenuItem1.Text = "Open CSV Data ...";
            this.openCSVDATAToolStripMenuItem1.Click += new System.EventHandler(this.openCSVDataToolStripMenuItem_Click);
            // 
            // openASLRawDataToolStripMenuItem
            // 
            this.openASLRawDataToolStripMenuItem.Name = "openASLRawDataToolStripMenuItem";
            this.openASLRawDataToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openASLRawDataToolStripMenuItem.Text = "Open ASL Raw Data ...";
            this.openASLRawDataToolStripMenuItem.Click += new System.EventHandler(this.rawDataToolStripMenuItem1_Click);
            // 
            // purgeAnalysisToolStripMenuItem
            // 
            this.purgeAnalysisToolStripMenuItem.Name = "purgeAnalysisToolStripMenuItem";
            this.purgeAnalysisToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.purgeAnalysisToolStripMenuItem.Text = "Purge Analysis";
            this.purgeAnalysisToolStripMenuItem.Click += new System.EventHandler(this.purgeAnalysisToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 293);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EVALabAnalysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabView.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStripTree.ResumeLayout(false);
            this.contextMenuStripCase.ResumeLayout(false);
            this.contextMenuStripTrial.ResumeLayout(false);
            this.contextMenuStripROI.ResumeLayout(false);
            this.contextMenuStripDATA.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem openBinocularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openASLToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.OpenFileDialog openFileDialogASL;
        private System.Windows.Forms.TabControl tabView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTree;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterApplyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saccadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saccToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDispersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private EVALabAnalysis.Display.PanelPlotControl panelPlotControl1;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem degreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showVsTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showXYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importExperimentToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripAddMainSequence;
        private System.Windows.Forms.ToolStripMenuItem mainSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem denoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCase;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem findCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byVelocityXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byVelocityYToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.OpenFileDialog openFileDialogRawEOG;
        private System.Windows.Forms.OpenFileDialog openFileDialogEOG;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItemOpenASL;
        private System.Windows.Forms.ToolStripMenuItem rawDataToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialogRawASL;
        private System.Windows.Forms.ToolStripMenuItem splitByReferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem openROIToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogRawASL;
        private System.Windows.Forms.OpenFileDialog openFileDialogROI;
        private System.Windows.Forms.ToolStripMenuItem rOIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignDataToROIsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opneProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogProject;
        private System.Windows.Forms.SaveFileDialog saveFileDialogProject;
        private System.Windows.Forms.ToolStripMenuItem computeSequencingToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrial;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openCSVDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogRawCSV;
        private System.Windows.Forms.ToolStripMenuItem importFixationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSaccadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem newMainSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openROIToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripROI;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDATA;
        private System.Windows.Forms.ToolStripMenuItem openCSVDATAToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openASLRawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computeDistanceToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyNode;
        private System.Windows.Forms.ToolStripMenuItem purgeAnalysisToolStripMenuItem;

    }
}

