namespace EVALabAnalysis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBinocularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openASLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItemOpenASL = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opneProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tabView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.openFileDialogASL = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showVsTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterApplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.denoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panelPlotControl1 = new EVALabAnalysis.Display.PanelPlotControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabView.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStripTree.SuspendLayout();
            this.contextMenuStripCase.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 435);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.openROIToolStripMenuItem});
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
            // openROIToolStripMenuItem
            // 
            this.openROIToolStripMenuItem.Name = "openROIToolStripMenuItem";
            this.openROIToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openROIToolStripMenuItem.Text = "Open ROI";
            this.openROIToolStripMenuItem.Click += new System.EventHandler(this.openROIToolStripMenuItem_Click);
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
            this.newGraphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGraphToolStripMenuItem.Text = "New Graph";
            this.newGraphToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // removeGraphToolStripMenuItem
            // 
            this.removeGraphToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeGraphToolStripMenuItem.Image")));
            this.removeGraphToolStripMenuItem.Name = "removeGraphToolStripMenuItem";
            this.removeGraphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeGraphToolStripMenuItem.Text = "Remove Graph";
            this.removeGraphToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem.Image")));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStrip1
            // 
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
            this.toolStrip1.Size = new System.Drawing.Size(780, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStripOpen";
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
            this.toolStripButton3.Text = "toolStripButton3";
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
            this.splitContainer1.Size = new System.Drawing.Size(780, 386);
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
            this.splitContainer2.Panel2.Controls.Add(this.textBoxLog);
            this.splitContainer2.Size = new System.Drawing.Size(158, 386);
            this.splitContainer2.SplitterDistance = 327;
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
            this.treeView.Size = new System.Drawing.Size(158, 327);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "accept.png");
            this.imageList.Images.SetKeyName(1, "add.png");
            this.imageList.Images.SetKeyName(2, "alarm.png");
            this.imageList.Images.SetKeyName(3, "anchor.png");
            this.imageList.Images.SetKeyName(4, "application.png");
            this.imageList.Images.SetKeyName(5, "application2.png");
            this.imageList.Images.SetKeyName(6, "application_add.png");
            this.imageList.Images.SetKeyName(7, "application_cascade.png");
            this.imageList.Images.SetKeyName(8, "application_delete.png");
            this.imageList.Images.SetKeyName(9, "application_double.png");
            this.imageList.Images.SetKeyName(10, "application_edit.png");
            this.imageList.Images.SetKeyName(11, "application_error.png");
            this.imageList.Images.SetKeyName(12, "application_form.png");
            this.imageList.Images.SetKeyName(13, "application_get.png");
            this.imageList.Images.SetKeyName(14, "application_go.png");
            this.imageList.Images.SetKeyName(15, "application_home.png");
            this.imageList.Images.SetKeyName(16, "application_key.png");
            this.imageList.Images.SetKeyName(17, "application_lightning.png");
            this.imageList.Images.SetKeyName(18, "application_link.png");
            this.imageList.Images.SetKeyName(19, "application_osx.png");
            this.imageList.Images.SetKeyName(20, "application_osx_terminal.png");
            this.imageList.Images.SetKeyName(21, "application_put.png");
            this.imageList.Images.SetKeyName(22, "application_side_boxes.png");
            this.imageList.Images.SetKeyName(23, "application_side_contract.png");
            this.imageList.Images.SetKeyName(24, "application_side_expand.png");
            this.imageList.Images.SetKeyName(25, "application_side_list.png");
            this.imageList.Images.SetKeyName(26, "application_side_tree.png");
            this.imageList.Images.SetKeyName(27, "application_split.png");
            this.imageList.Images.SetKeyName(28, "application_tile_horizontal.png");
            this.imageList.Images.SetKeyName(29, "application_tile_vertical.png");
            this.imageList.Images.SetKeyName(30, "application_view_columns.png");
            this.imageList.Images.SetKeyName(31, "application_view_detail.png");
            this.imageList.Images.SetKeyName(32, "application_view_gallery.png");
            this.imageList.Images.SetKeyName(33, "application_view_icons.png");
            this.imageList.Images.SetKeyName(34, "application_view_list.png");
            this.imageList.Images.SetKeyName(35, "application_view_tile.png");
            this.imageList.Images.SetKeyName(36, "application_view_xp.png");
            this.imageList.Images.SetKeyName(37, "application_view_xp_terminal.png");
            this.imageList.Images.SetKeyName(38, "arrow_branch.png");
            this.imageList.Images.SetKeyName(39, "arrow_divide.png");
            this.imageList.Images.SetKeyName(40, "arrow_in.png");
            this.imageList.Images.SetKeyName(41, "arrow_inout.png");
            this.imageList.Images.SetKeyName(42, "arrow_join.png");
            this.imageList.Images.SetKeyName(43, "arrow_left.png");
            this.imageList.Images.SetKeyName(44, "arrow_merge.png");
            this.imageList.Images.SetKeyName(45, "arrow_out.png");
            this.imageList.Images.SetKeyName(46, "arrow_redo.png");
            this.imageList.Images.SetKeyName(47, "arrow_refresh.png");
            this.imageList.Images.SetKeyName(48, "arrow_right.png");
            this.imageList.Images.SetKeyName(49, "arrow_undo.png");
            this.imageList.Images.SetKeyName(50, "asterisk_orange.png");
            this.imageList.Images.SetKeyName(51, "attach.png");
            this.imageList.Images.SetKeyName(52, "attach_2.png");
            this.imageList.Images.SetKeyName(53, "award_star_gold.png");
            this.imageList.Images.SetKeyName(54, "bandaid.png");
            this.imageList.Images.SetKeyName(55, "basket.png");
            this.imageList.Images.SetKeyName(56, "bell.png");
            this.imageList.Images.SetKeyName(57, "bin_closed.png");
            this.imageList.Images.SetKeyName(58, "blog.png");
            this.imageList.Images.SetKeyName(59, "blueprint.png");
            this.imageList.Images.SetKeyName(60, "blueprint_horizontal.png");
            this.imageList.Images.SetKeyName(61, "bluetooth.png");
            this.imageList.Images.SetKeyName(62, "bomb.png");
            this.imageList.Images.SetKeyName(63, "book.png");
            this.imageList.Images.SetKeyName(64, "book_addresses.png");
            this.imageList.Images.SetKeyName(65, "book_next.png");
            this.imageList.Images.SetKeyName(66, "book_open.png");
            this.imageList.Images.SetKeyName(67, "book_previous.png");
            this.imageList.Images.SetKeyName(68, "bookmark.png");
            this.imageList.Images.SetKeyName(69, "bookmark_book.png");
            this.imageList.Images.SetKeyName(70, "bookmark_book_open.png");
            this.imageList.Images.SetKeyName(71, "bookmark_document.png");
            this.imageList.Images.SetKeyName(72, "bookmark_folder.png");
            this.imageList.Images.SetKeyName(73, "books.png");
            this.imageList.Images.SetKeyName(74, "box.png");
            this.imageList.Images.SetKeyName(75, "brick.png");
            this.imageList.Images.SetKeyName(76, "bricks.png");
            this.imageList.Images.SetKeyName(77, "briefcase.png");
            this.imageList.Images.SetKeyName(78, "bug.png");
            this.imageList.Images.SetKeyName(79, "buildings.png");
            this.imageList.Images.SetKeyName(80, "bullet_add_1.png");
            this.imageList.Images.SetKeyName(81, "bullet_add_2.png");
            this.imageList.Images.SetKeyName(82, "bullet_key.png");
            this.imageList.Images.SetKeyName(83, "cake.png");
            this.imageList.Images.SetKeyName(84, "calculator.png");
            this.imageList.Images.SetKeyName(85, "calendar_1.png");
            this.imageList.Images.SetKeyName(86, "calendar_2.png");
            this.imageList.Images.SetKeyName(87, "camera.png");
            this.imageList.Images.SetKeyName(88, "cancel.png");
            this.imageList.Images.SetKeyName(89, "car.png");
            this.imageList.Images.SetKeyName(90, "cart.png");
            this.imageList.Images.SetKeyName(91, "cd.png");
            this.imageList.Images.SetKeyName(92, "chart_bar.png");
            this.imageList.Images.SetKeyName(93, "chart_curve.png");
            this.imageList.Images.SetKeyName(94, "chart_line.png");
            this.imageList.Images.SetKeyName(95, "chart_organisation.png");
            this.imageList.Images.SetKeyName(96, "chart_pie.png");
            this.imageList.Images.SetKeyName(97, "clipboard_paste_image.png");
            this.imageList.Images.SetKeyName(98, "clipboard_sign.png");
            this.imageList.Images.SetKeyName(99, "clipboard_text.png");
            this.imageList.Images.SetKeyName(100, "clock.png");
            this.imageList.Images.SetKeyName(101, "cog.png");
            this.imageList.Images.SetKeyName(102, "coins.png");
            this.imageList.Images.SetKeyName(103, "color_swatch_1.png");
            this.imageList.Images.SetKeyName(104, "color_swatch_2.png");
            this.imageList.Images.SetKeyName(105, "comment.png");
            this.imageList.Images.SetKeyName(106, "compass.png");
            this.imageList.Images.SetKeyName(107, "compress.png");
            this.imageList.Images.SetKeyName(108, "computer.png");
            this.imageList.Images.SetKeyName(109, "connect.png");
            this.imageList.Images.SetKeyName(110, "contrast.png");
            this.imageList.Images.SetKeyName(111, "control_eject.png");
            this.imageList.Images.SetKeyName(112, "control_end.png");
            this.imageList.Images.SetKeyName(113, "control_equalizer.png");
            this.imageList.Images.SetKeyName(114, "control_fastforward.png");
            this.imageList.Images.SetKeyName(115, "control_pause.png");
            this.imageList.Images.SetKeyName(116, "control_play.png");
            this.imageList.Images.SetKeyName(117, "control_repeat.png");
            this.imageList.Images.SetKeyName(118, "control_rewind.png");
            this.imageList.Images.SetKeyName(119, "control_start.png");
            this.imageList.Images.SetKeyName(120, "control_stop.png");
            this.imageList.Images.SetKeyName(121, "control_wheel.png");
            this.imageList.Images.SetKeyName(122, "counter.png");
            this.imageList.Images.SetKeyName(123, "counter_count.png");
            this.imageList.Images.SetKeyName(124, "counter_count_up.png");
            this.imageList.Images.SetKeyName(125, "counter_reset.png");
            this.imageList.Images.SetKeyName(126, "counter_stop.png");
            this.imageList.Images.SetKeyName(127, "cross.png");
            this.imageList.Images.SetKeyName(128, "cross_octagon.png");
            this.imageList.Images.SetKeyName(129, "cross_octagon_fram.png");
            this.imageList.Images.SetKeyName(130, "cross_shield.png");
            this.imageList.Images.SetKeyName(131, "cross_shield_2.png");
            this.imageList.Images.SetKeyName(132, "crown.png");
            this.imageList.Images.SetKeyName(133, "crown_bronze.png");
            this.imageList.Images.SetKeyName(134, "crown_silver.png");
            this.imageList.Images.SetKeyName(135, "css.png");
            this.imageList.Images.SetKeyName(136, "cursor.png");
            this.imageList.Images.SetKeyName(137, "cut.png");
            this.imageList.Images.SetKeyName(138, "dashboard.png");
            this.imageList.Images.SetKeyName(139, "data.png");
            this.imageList.Images.SetKeyName(140, "database.png");
            this.imageList.Images.SetKeyName(141, "databases.png");
            this.imageList.Images.SetKeyName(142, "delete.png");
            this.imageList.Images.SetKeyName(143, "delivery.png");
            this.imageList.Images.SetKeyName(144, "desktop.png");
            this.imageList.Images.SetKeyName(145, "desktop_empty.png");
            this.imageList.Images.SetKeyName(146, "direction.png");
            this.imageList.Images.SetKeyName(147, "disconnect.png");
            this.imageList.Images.SetKeyName(148, "disk.png");
            this.imageList.Images.SetKeyName(149, "doc_access.png");
            this.imageList.Images.SetKeyName(150, "doc_break.png");
            this.imageList.Images.SetKeyName(151, "doc_convert.png");
            this.imageList.Images.SetKeyName(152, "doc_excel_csv.png");
            this.imageList.Images.SetKeyName(153, "doc_excel_table.png");
            this.imageList.Images.SetKeyName(154, "doc_film.png");
            this.imageList.Images.SetKeyName(155, "doc_illustrator.png");
            this.imageList.Images.SetKeyName(156, "doc_music.png");
            this.imageList.Images.SetKeyName(157, "doc_music_playlist.png");
            this.imageList.Images.SetKeyName(158, "doc_offlice.png");
            this.imageList.Images.SetKeyName(159, "doc_page.png");
            this.imageList.Images.SetKeyName(160, "doc_page_previous.png");
            this.imageList.Images.SetKeyName(161, "doc_pdf.png");
            this.imageList.Images.SetKeyName(162, "doc_photoshop.png");
            this.imageList.Images.SetKeyName(163, "doc_resize.png");
            this.imageList.Images.SetKeyName(164, "doc_resize_actual.png");
            this.imageList.Images.SetKeyName(165, "doc_shred.png");
            this.imageList.Images.SetKeyName(166, "doc_stand.png");
            this.imageList.Images.SetKeyName(167, "doc_table.png");
            this.imageList.Images.SetKeyName(168, "doc_tag.png");
            this.imageList.Images.SetKeyName(169, "doc_text_image.png");
            this.imageList.Images.SetKeyName(170, "door.png");
            this.imageList.Images.SetKeyName(171, "door_in.png");
            this.imageList.Images.SetKeyName(172, "drawer.png");
            this.imageList.Images.SetKeyName(173, "drink.png");
            this.imageList.Images.SetKeyName(174, "drink_empty.png");
            this.imageList.Images.SetKeyName(175, "drive.png");
            this.imageList.Images.SetKeyName(176, "drive_burn.png");
            this.imageList.Images.SetKeyName(177, "drive_cd.png");
            this.imageList.Images.SetKeyName(178, "drive_cd_empty.png");
            this.imageList.Images.SetKeyName(179, "drive_delete.png");
            this.imageList.Images.SetKeyName(180, "drive_disk.png");
            this.imageList.Images.SetKeyName(181, "drive_error.png");
            this.imageList.Images.SetKeyName(182, "drive_go.png");
            this.imageList.Images.SetKeyName(183, "drive_link.png");
            this.imageList.Images.SetKeyName(184, "drive_network.png");
            this.imageList.Images.SetKeyName(185, "drive_rename.png");
            this.imageList.Images.SetKeyName(186, "dvd.png");
            this.imageList.Images.SetKeyName(187, "email.png");
            this.imageList.Images.SetKeyName(188, "email_open.png");
            this.imageList.Images.SetKeyName(189, "email_open_image.png");
            this.imageList.Images.SetKeyName(190, "emoticon_evilgrin.png");
            this.imageList.Images.SetKeyName(191, "emoticon_grin.png");
            this.imageList.Images.SetKeyName(192, "emoticon_happy.png");
            this.imageList.Images.SetKeyName(193, "emoticon_smile.png");
            this.imageList.Images.SetKeyName(194, "emoticon_surprised.png");
            this.imageList.Images.SetKeyName(195, "emoticon_tongue.png");
            this.imageList.Images.SetKeyName(196, "emoticon_unhappy.png");
            this.imageList.Images.SetKeyName(197, "emoticon_waii.png");
            this.imageList.Images.SetKeyName(198, "emoticon_wink.png");
            this.imageList.Images.SetKeyName(199, "envelope.png");
            this.imageList.Images.SetKeyName(200, "envelope_2.png");
            this.imageList.Images.SetKeyName(201, "error.png");
            this.imageList.Images.SetKeyName(202, "exclamation.png");
            this.imageList.Images.SetKeyName(203, "exclamation_octagon_fram.png");
            this.imageList.Images.SetKeyName(204, "eye.png");
            this.imageList.Images.SetKeyName(205, "feed.png");
            this.imageList.Images.SetKeyName(206, "feed_ballon.png");
            this.imageList.Images.SetKeyName(207, "feed_document.png");
            this.imageList.Images.SetKeyName(208, "female.png");
            this.imageList.Images.SetKeyName(209, "film.png");
            this.imageList.Images.SetKeyName(210, "films.png");
            this.imageList.Images.SetKeyName(211, "find.png");
            this.imageList.Images.SetKeyName(212, "flag_blue.png");
            this.imageList.Images.SetKeyName(213, "folder.png");
            this.imageList.Images.SetKeyName(214, "font.png");
            this.imageList.Images.SetKeyName(215, "funnel.png");
            this.imageList.Images.SetKeyName(216, "grid.png");
            this.imageList.Images.SetKeyName(217, "grid_dot.png");
            this.imageList.Images.SetKeyName(218, "group.png");
            this.imageList.Images.SetKeyName(219, "hammer.png");
            this.imageList.Images.SetKeyName(220, "hammer_screwdriver.png");
            this.imageList.Images.SetKeyName(221, "hand.png");
            this.imageList.Images.SetKeyName(222, "hand_point.png");
            this.imageList.Images.SetKeyName(223, "heart.png");
            this.imageList.Images.SetKeyName(224, "heart_break.png");
            this.imageList.Images.SetKeyName(225, "heart_empty.png");
            this.imageList.Images.SetKeyName(226, "heart_half.png");
            this.imageList.Images.SetKeyName(227, "heart_small.png");
            this.imageList.Images.SetKeyName(228, "help.png");
            this.imageList.Images.SetKeyName(229, "highlighter.png");
            this.imageList.Images.SetKeyName(230, "house.png");
            this.imageList.Images.SetKeyName(231, "html.png");
            this.imageList.Images.SetKeyName(232, "image_1.png");
            this.imageList.Images.SetKeyName(233, "image_2.png");
            this.imageList.Images.SetKeyName(234, "images.png");
            this.imageList.Images.SetKeyName(235, "inbox.png");
            this.imageList.Images.SetKeyName(236, "ipod.png");
            this.imageList.Images.SetKeyName(237, "ipod_cast.png");
            this.imageList.Images.SetKeyName(238, "joystick.png");
            this.imageList.Images.SetKeyName(239, "key.png");
            this.imageList.Images.SetKeyName(240, "keyboard.png");
            this.imageList.Images.SetKeyName(241, "layer_treansparent.png");
            this.imageList.Images.SetKeyName(242, "layers.png");
            this.imageList.Images.SetKeyName(243, "layout.png");
            this.imageList.Images.SetKeyName(244, "layout_header_footer_3.png");
            this.imageList.Images.SetKeyName(245, "layout_header_footer_3_mix.png");
            this.imageList.Images.SetKeyName(246, "layout_join.png");
            this.imageList.Images.SetKeyName(247, "layout_join_vertical.png");
            this.imageList.Images.SetKeyName(248, "layout_select.png");
            this.imageList.Images.SetKeyName(249, "layout_select_content.png");
            this.imageList.Images.SetKeyName(250, "layout_select_footer.png");
            this.imageList.Images.SetKeyName(251, "layout_select_sidebar.png");
            this.imageList.Images.SetKeyName(252, "layout_split.png");
            this.imageList.Images.SetKeyName(253, "layout_split_vertical.png");
            this.imageList.Images.SetKeyName(254, "lifebuoy.png");
            this.imageList.Images.SetKeyName(255, "lightbulb.png");
            this.imageList.Images.SetKeyName(256, "lightbulb_off.png");
            this.imageList.Images.SetKeyName(257, "lightning.png");
            this.imageList.Images.SetKeyName(258, "link.png");
            this.imageList.Images.SetKeyName(259, "link_break.png");
            this.imageList.Images.SetKeyName(260, "lock.png");
            this.imageList.Images.SetKeyName(261, "lock_unlock.png");
            this.imageList.Images.SetKeyName(262, "magnet.png");
            this.imageList.Images.SetKeyName(263, "magnifier.png");
            this.imageList.Images.SetKeyName(264, "magnifier_zoom_in.png");
            this.imageList.Images.SetKeyName(265, "male.png");
            this.imageList.Images.SetKeyName(266, "map.png");
            this.imageList.Images.SetKeyName(267, "marker.png");
            this.imageList.Images.SetKeyName(268, "medal_bronze_1.png");
            this.imageList.Images.SetKeyName(269, "medal_gold_1.png");
            this.imageList.Images.SetKeyName(270, "media_player_small_blue.png");
            this.imageList.Images.SetKeyName(271, "microphone.png");
            this.imageList.Images.SetKeyName(272, "mobile_phone.png");
            this.imageList.Images.SetKeyName(273, "money.png");
            this.imageList.Images.SetKeyName(274, "money_dollar.png");
            this.imageList.Images.SetKeyName(275, "money_euro.png");
            this.imageList.Images.SetKeyName(276, "money_pound.png");
            this.imageList.Images.SetKeyName(277, "money_yen.png");
            this.imageList.Images.SetKeyName(278, "monitor.png");
            this.imageList.Images.SetKeyName(279, "mouse.png");
            this.imageList.Images.SetKeyName(280, "music.png");
            this.imageList.Images.SetKeyName(281, "music_beam.png");
            this.imageList.Images.SetKeyName(282, "neutral.png");
            this.imageList.Images.SetKeyName(283, "new.png");
            this.imageList.Images.SetKeyName(284, "newspaper.png");
            this.imageList.Images.SetKeyName(285, "note.png");
            this.imageList.Images.SetKeyName(286, "nuclear.png");
            this.imageList.Images.SetKeyName(287, "package.png");
            this.imageList.Images.SetKeyName(288, "page.png");
            this.imageList.Images.SetKeyName(289, "page_2.png");
            this.imageList.Images.SetKeyName(290, "page_2_copy.png");
            this.imageList.Images.SetKeyName(291, "page_code.png");
            this.imageList.Images.SetKeyName(292, "page_copy.png");
            this.imageList.Images.SetKeyName(293, "page_excel.png");
            this.imageList.Images.SetKeyName(294, "page_lightning.png");
            this.imageList.Images.SetKeyName(295, "page_paste.png");
            this.imageList.Images.SetKeyName(296, "page_red.png");
            this.imageList.Images.SetKeyName(297, "page_refresh.png");
            this.imageList.Images.SetKeyName(298, "page_save.png");
            this.imageList.Images.SetKeyName(299, "page_white_cplusplus.png");
            this.imageList.Images.SetKeyName(300, "page_white_csharp.png");
            this.imageList.Images.SetKeyName(301, "page_white_cup.png");
            this.imageList.Images.SetKeyName(302, "page_white_database.png");
            this.imageList.Images.SetKeyName(303, "page_white_delete.png");
            this.imageList.Images.SetKeyName(304, "page_white_dvd.png");
            this.imageList.Images.SetKeyName(305, "page_white_edit.png");
            this.imageList.Images.SetKeyName(306, "page_white_error.png");
            this.imageList.Images.SetKeyName(307, "page_white_excel.png");
            this.imageList.Images.SetKeyName(308, "page_white_find.png");
            this.imageList.Images.SetKeyName(309, "page_white_flash.png");
            this.imageList.Images.SetKeyName(310, "page_white_freehand.png");
            this.imageList.Images.SetKeyName(311, "page_white_gear.png");
            this.imageList.Images.SetKeyName(312, "page_white_get.png");
            this.imageList.Images.SetKeyName(313, "page_white_paintbrush.png");
            this.imageList.Images.SetKeyName(314, "page_white_paste.png");
            this.imageList.Images.SetKeyName(315, "page_white_php.png");
            this.imageList.Images.SetKeyName(316, "page_white_picture.png");
            this.imageList.Images.SetKeyName(317, "page_white_powerpoint.png");
            this.imageList.Images.SetKeyName(318, "page_white_put.png");
            this.imageList.Images.SetKeyName(319, "page_white_ruby.png");
            this.imageList.Images.SetKeyName(320, "page_white_stack.png");
            this.imageList.Images.SetKeyName(321, "page_white_star.png");
            this.imageList.Images.SetKeyName(322, "page_white_swoosh.png");
            this.imageList.Images.SetKeyName(323, "page_white_text.png");
            this.imageList.Images.SetKeyName(324, "page_white_text_width.png");
            this.imageList.Images.SetKeyName(325, "page_white_tux.png");
            this.imageList.Images.SetKeyName(326, "page_white_vector.png");
            this.imageList.Images.SetKeyName(327, "page_white_visualstudio.png");
            this.imageList.Images.SetKeyName(328, "page_white_width.png");
            this.imageList.Images.SetKeyName(329, "page_white_word.png");
            this.imageList.Images.SetKeyName(330, "page_white_world.png");
            this.imageList.Images.SetKeyName(331, "page_white_wrench.png");
            this.imageList.Images.SetKeyName(332, "page_white_zip.png");
            this.imageList.Images.SetKeyName(333, "paintbrush.png");
            this.imageList.Images.SetKeyName(334, "paintcan.png");
            this.imageList.Images.SetKeyName(335, "palette.png");
            this.imageList.Images.SetKeyName(336, "paper_bag.png");
            this.imageList.Images.SetKeyName(337, "paste_plain.png");
            this.imageList.Images.SetKeyName(338, "paste_word.png");
            this.imageList.Images.SetKeyName(339, "pencil.png");
            this.imageList.Images.SetKeyName(340, "photo.png");
            this.imageList.Images.SetKeyName(341, "photo_album.png");
            this.imageList.Images.SetKeyName(342, "photos.png");
            this.imageList.Images.SetKeyName(343, "piano.png");
            this.imageList.Images.SetKeyName(344, "picture.png");
            this.imageList.Images.SetKeyName(345, "pilcrow.png");
            this.imageList.Images.SetKeyName(346, "pill.png");
            this.imageList.Images.SetKeyName(347, "pin.png");
            this.imageList.Images.SetKeyName(348, "pipette.png");
            this.imageList.Images.SetKeyName(349, "plaing_card.png");
            this.imageList.Images.SetKeyName(350, "plug.png");
            this.imageList.Images.SetKeyName(351, "plugin.png");
            this.imageList.Images.SetKeyName(352, "printer.png");
            this.imageList.Images.SetKeyName(353, "projection_screen.png");
            this.imageList.Images.SetKeyName(354, "projection_screen_present.png");
            this.imageList.Images.SetKeyName(355, "rainbow.png");
            this.imageList.Images.SetKeyName(356, "report.png");
            this.imageList.Images.SetKeyName(357, "rocket.png");
            this.imageList.Images.SetKeyName(358, "rosette.png");
            this.imageList.Images.SetKeyName(359, "rss.png");
            this.imageList.Images.SetKeyName(360, "ruby.png");
            this.imageList.Images.SetKeyName(361, "ruler_1.png");
            this.imageList.Images.SetKeyName(362, "ruler_2.png");
            this.imageList.Images.SetKeyName(363, "ruler_crop.png");
            this.imageList.Images.SetKeyName(364, "ruler_triangle.png");
            this.imageList.Images.SetKeyName(365, "safe.png");
            this.imageList.Images.SetKeyName(366, "script.png");
            this.imageList.Images.SetKeyName(367, "selection.png");
            this.imageList.Images.SetKeyName(368, "selection_select.png");
            this.imageList.Images.SetKeyName(369, "server.png");
            this.imageList.Images.SetKeyName(370, "shading.png");
            this.imageList.Images.SetKeyName(371, "shape_aling_bottom.png");
            this.imageList.Images.SetKeyName(372, "shape_aling_center.png");
            this.imageList.Images.SetKeyName(373, "shape_aling_left.png");
            this.imageList.Images.SetKeyName(374, "shape_aling_middle.png");
            this.imageList.Images.SetKeyName(375, "shape_aling_right.png");
            this.imageList.Images.SetKeyName(376, "shape_aling_top.png");
            this.imageList.Images.SetKeyName(377, "shape_flip_horizontal.png");
            this.imageList.Images.SetKeyName(378, "shape_flip_vertical.png");
            this.imageList.Images.SetKeyName(379, "shape_group.png");
            this.imageList.Images.SetKeyName(380, "shape_handles.png");
            this.imageList.Images.SetKeyName(381, "shape_move_back.png");
            this.imageList.Images.SetKeyName(382, "shape_move_backwards.png");
            this.imageList.Images.SetKeyName(383, "shape_move_forwards.png");
            this.imageList.Images.SetKeyName(384, "shape_move_front.png");
            this.imageList.Images.SetKeyName(385, "shape_square.png");
            this.imageList.Images.SetKeyName(386, "shield.png");
            this.imageList.Images.SetKeyName(387, "sitemap.png");
            this.imageList.Images.SetKeyName(388, "slide.png");
            this.imageList.Images.SetKeyName(389, "slides.png");
            this.imageList.Images.SetKeyName(390, "slides_stack.png");
            this.imageList.Images.SetKeyName(391, "smiley_confuse.png");
            this.imageList.Images.SetKeyName(392, "smiley_cool.png");
            this.imageList.Images.SetKeyName(393, "smiley_cry.png");
            this.imageList.Images.SetKeyName(394, "smiley_fat.png");
            this.imageList.Images.SetKeyName(395, "smiley_mad.png");
            this.imageList.Images.SetKeyName(396, "smiley_red.png");
            this.imageList.Images.SetKeyName(397, "smiley_roll.png");
            this.imageList.Images.SetKeyName(398, "smiley_slim.png");
            this.imageList.Images.SetKeyName(399, "smiley_yell.png");
            this.imageList.Images.SetKeyName(400, "socket.png");
            this.imageList.Images.SetKeyName(401, "sockets.png");
            this.imageList.Images.SetKeyName(402, "sort.png");
            this.imageList.Images.SetKeyName(403, "sort_alphabet.png");
            this.imageList.Images.SetKeyName(404, "sort_date.png");
            this.imageList.Images.SetKeyName(405, "sort_disable.png");
            this.imageList.Images.SetKeyName(406, "sort_number.png");
            this.imageList.Images.SetKeyName(407, "sort_price.png");
            this.imageList.Images.SetKeyName(408, "sort_quantity.png");
            this.imageList.Images.SetKeyName(409, "sort_rating.png");
            this.imageList.Images.SetKeyName(410, "sound.png");
            this.imageList.Images.SetKeyName(411, "sound_note.png");
            this.imageList.Images.SetKeyName(412, "spellcheck.png");
            this.imageList.Images.SetKeyName(413, "sport_8ball.png");
            this.imageList.Images.SetKeyName(414, "sport_basketball.png");
            this.imageList.Images.SetKeyName(415, "sport_football.png");
            this.imageList.Images.SetKeyName(416, "sport_golf.png");
            this.imageList.Images.SetKeyName(417, "sport_raquet.png");
            this.imageList.Images.SetKeyName(418, "sport_shuttlecock.png");
            this.imageList.Images.SetKeyName(419, "sport_soccer.png");
            this.imageList.Images.SetKeyName(420, "sport_tennis.png");
            this.imageList.Images.SetKeyName(421, "stamp.png");
            this.imageList.Images.SetKeyName(422, "star_1.png");
            this.imageList.Images.SetKeyName(423, "star_2.png");
            this.imageList.Images.SetKeyName(424, "status_online.png");
            this.imageList.Images.SetKeyName(425, "stop.png");
            this.imageList.Images.SetKeyName(426, "style.png");
            this.imageList.Images.SetKeyName(427, "sum.png");
            this.imageList.Images.SetKeyName(428, "sum_2.png");
            this.imageList.Images.SetKeyName(429, "switch.png");
            this.imageList.Images.SetKeyName(430, "tab.png");
            this.imageList.Images.SetKeyName(431, "table.png");
            this.imageList.Images.SetKeyName(432, "tag.png");
            this.imageList.Images.SetKeyName(433, "tag_blue.png");
            this.imageList.Images.SetKeyName(434, "target.png");
            this.imageList.Images.SetKeyName(435, "telephone.png");
            this.imageList.Images.SetKeyName(436, "television.png");
            this.imageList.Images.SetKeyName(437, "text_align_center.png");
            this.imageList.Images.SetKeyName(438, "text_align_justify.png");
            this.imageList.Images.SetKeyName(439, "text_align_left.png");
            this.imageList.Images.SetKeyName(440, "text_align_right.png");
            this.imageList.Images.SetKeyName(441, "text_allcaps.png");
            this.imageList.Images.SetKeyName(442, "text_bold.png");
            this.imageList.Images.SetKeyName(443, "text_columns.png");
            this.imageList.Images.SetKeyName(444, "text_dropcaps.png");
            this.imageList.Images.SetKeyName(445, "text_heading_1.png");
            this.imageList.Images.SetKeyName(446, "text_horizontalrule.png");
            this.imageList.Images.SetKeyName(447, "text_indent.png");
            this.imageList.Images.SetKeyName(448, "text_indent_remove.png");
            this.imageList.Images.SetKeyName(449, "text_italic.png");
            this.imageList.Images.SetKeyName(450, "text_kerning.png");
            this.imageList.Images.SetKeyName(451, "text_letter_omega.png");
            this.imageList.Images.SetKeyName(452, "text_letterspacing.png");
            this.imageList.Images.SetKeyName(453, "text_linespacing.png");
            this.imageList.Images.SetKeyName(454, "text_list_bullets.png");
            this.imageList.Images.SetKeyName(455, "text_list_numbers.png");
            this.imageList.Images.SetKeyName(456, "text_lowercase.png");
            this.imageList.Images.SetKeyName(457, "text_padding_bottom.png");
            this.imageList.Images.SetKeyName(458, "text_padding_left.png");
            this.imageList.Images.SetKeyName(459, "text_padding_right.png");
            this.imageList.Images.SetKeyName(460, "text_padding_top.png");
            this.imageList.Images.SetKeyName(461, "text_signature.png");
            this.imageList.Images.SetKeyName(462, "text_smallcaps.png");
            this.imageList.Images.SetKeyName(463, "text_strikethrough.png");
            this.imageList.Images.SetKeyName(464, "text_subscript.png");
            this.imageList.Images.SetKeyName(465, "textfield.png");
            this.imageList.Images.SetKeyName(466, "textfield_rename.png");
            this.imageList.Images.SetKeyName(467, "ticket.png");
            this.imageList.Images.SetKeyName(468, "timeline_marker.png");
            this.imageList.Images.SetKeyName(469, "traffic.png");
            this.imageList.Images.SetKeyName(470, "transmit.png");
            this.imageList.Images.SetKeyName(471, "trophy.png");
            this.imageList.Images.SetKeyName(472, "trophy_bronze.png");
            this.imageList.Images.SetKeyName(473, "trophy_silver.png");
            this.imageList.Images.SetKeyName(474, "ui_combo_box.png");
            this.imageList.Images.SetKeyName(475, "ui_saccordion.png");
            this.imageList.Images.SetKeyName(476, "ui_slider_1.png");
            this.imageList.Images.SetKeyName(477, "ui_slider_2.png");
            this.imageList.Images.SetKeyName(478, "ui_tab_bottom.png");
            this.imageList.Images.SetKeyName(479, "ui_tab_content.png");
            this.imageList.Images.SetKeyName(480, "ui_tab_disable.png");
            this.imageList.Images.SetKeyName(481, "ui_tab_side.png");
            this.imageList.Images.SetKeyName(482, "ui_text_field_hidden.png");
            this.imageList.Images.SetKeyName(483, "ui_text_field_password.png");
            this.imageList.Images.SetKeyName(484, "umbrella.png");
            this.imageList.Images.SetKeyName(485, "user.png");
            this.imageList.Images.SetKeyName(486, "user_black_female.png");
            this.imageList.Images.SetKeyName(487, "user_business.png");
            this.imageList.Images.SetKeyName(488, "user_business_boss.png");
            this.imageList.Images.SetKeyName(489, "user_female.png");
            this.imageList.Images.SetKeyName(490, "user_silhouette.png");
            this.imageList.Images.SetKeyName(491, "user_thief.png");
            this.imageList.Images.SetKeyName(492, "user_thief_baldie.png");
            this.imageList.Images.SetKeyName(493, "vcard.png");
            this.imageList.Images.SetKeyName(494, "vector.png");
            this.imageList.Images.SetKeyName(495, "wait.png");
            this.imageList.Images.SetKeyName(496, "wall.png");
            this.imageList.Images.SetKeyName(497, "wall_break.png");
            this.imageList.Images.SetKeyName(498, "wall_brick.png");
            this.imageList.Images.SetKeyName(499, "wall_disable.png");
            this.imageList.Images.SetKeyName(500, "wand.png");
            this.imageList.Images.SetKeyName(501, "weather_clouds.png");
            this.imageList.Images.SetKeyName(502, "weather_cloudy.png");
            this.imageList.Images.SetKeyName(503, "weather_lightning.png");
            this.imageList.Images.SetKeyName(504, "weather_rain.png");
            this.imageList.Images.SetKeyName(505, "weather_snow.png");
            this.imageList.Images.SetKeyName(506, "weather_sun.png");
            this.imageList.Images.SetKeyName(507, "webcam.png");
            this.imageList.Images.SetKeyName(508, "world.png");
            this.imageList.Images.SetKeyName(509, "zone.png");
            this.imageList.Images.SetKeyName(510, "zone_money.png");
            this.imageList.Images.SetKeyName(511, "zones.png");
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(158, 55);
            this.textBoxLog.TabIndex = 0;
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.tabPage1);
            this.tabView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabView.Location = new System.Drawing.Point(0, 0);
            this.tabView.Name = "tabView";
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(618, 386);
            this.tabView.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelPlotControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // openFileDialogASL
            // 
            this.openFileDialogASL.Filter = "evalab asl (*.xml) |*.xml";
            this.openFileDialogASL.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogASL_FileOk_1);
            // 
            // contextMenuStripTree
            // 
            this.contextMenuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.showToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.saccadeToolStripMenuItem,
            this.fixationToolStripMenuItem,
            this.rOIToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.utilitiesToolStripMenuItem});
            this.contextMenuStripTree.Name = "contextMenuStripTree";
            this.contextMenuStripTree.Size = new System.Drawing.Size(118, 180);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.showToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.buttToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // filterApplyToolStripMenuItem
            // 
            this.filterApplyToolStripMenuItem.Name = "filterApplyToolStripMenuItem";
            this.filterApplyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.filterApplyToolStripMenuItem.Text = "Filter ...";
            this.filterApplyToolStripMenuItem.Click += new System.EventHandler(this.filterApplyToolStripMenuItem_Click);
            // 
            // denoiseToolStripMenuItem
            // 
            this.denoiseToolStripMenuItem.Name = "denoiseToolStripMenuItem";
            this.denoiseToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.denoiseToolStripMenuItem.Text = "Denoise ...";
            this.denoiseToolStripMenuItem.Click += new System.EventHandler(this.denoiseToolStripMenuItem_Click);
            // 
            // buttToolStripMenuItem
            // 
            this.buttToolStripMenuItem.Name = "buttToolStripMenuItem";
            this.buttToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.buttToolStripMenuItem.Text = "Butterworth 50Hz";
            this.buttToolStripMenuItem.Click += new System.EventHandler(this.buttToolStripMenuItem_Click);
            // 
            // saccadeToolStripMenuItem
            // 
            this.saccadeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saccToolStripMenuItem,
            this.byVelocityXToolStripMenuItem,
            this.byVelocityYToolStripMenuItem});
            this.saccadeToolStripMenuItem.Name = "saccadeToolStripMenuItem";
            this.saccadeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.fixationToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.rOIToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            this.exportRawDataToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
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
            // panelPlotControl1
            // 
            this.panelPlotControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlotControl1.Location = new System.Drawing.Point(3, 3);
            this.panelPlotControl1.Name = "panelPlotControl1";
            this.panelPlotControl1.Size = new System.Drawing.Size(604, 354);
            this.panelPlotControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 457);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EVALabAnalysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.tabView.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStripTree.ResumeLayout(false);
            this.contextMenuStripCase.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBoxLog;
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

    }
}

