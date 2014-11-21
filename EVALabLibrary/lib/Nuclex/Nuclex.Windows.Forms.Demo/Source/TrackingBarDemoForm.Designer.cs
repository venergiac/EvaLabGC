namespace Nuclex.Windows.Forms.Demo {

  partial class TrackingBarDemoForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackingBarDemoForm));
      this.demoStatusStrip = new System.Windows.Forms.StatusStrip();
      this.demoStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.demoTrackingBar = new Nuclex.Windows.Forms.ToolStripTrackingBar();
      this.add1SecondTaskButton = new System.Windows.Forms.Button();
      this.add2SecondTaskButton = new System.Windows.Forms.Button();
      this.add4SecondTaskButton = new System.Windows.Forms.Button();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.demoStatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // demoStatusStrip
      // 
      this.demoStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demoStatusLabel,
            this.demoTrackingBar});
      this.demoStatusStrip.Location = new System.Drawing.Point(0, 150);
      this.demoStatusStrip.Name = "demoStatusStrip";
      this.demoStatusStrip.Size = new System.Drawing.Size(447, 22);
      this.demoStatusStrip.SizingGrip = false;
      this.demoStatusStrip.TabIndex = 1;
      this.demoStatusStrip.Text = "statusStrip1";
      // 
      // demoStatusLabel
      // 
      this.demoStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.demoStatusLabel.Name = "demoStatusLabel";
      this.demoStatusLabel.Size = new System.Drawing.Size(310, 17);
      this.demoStatusLabel.Spring = true;
      this.demoStatusLabel.Text = "Ready";
      this.demoStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // demoTrackingBar
      // 
      this.demoTrackingBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.demoTrackingBar.Name = "demoTrackingBar";
      this.demoTrackingBar.Size = new System.Drawing.Size(120, 16);
      // 
      // add1SecondTaskButton
      // 
      this.add1SecondTaskButton.Location = new System.Drawing.Point(12, 117);
      this.add1SecondTaskButton.Name = "add1SecondTaskButton";
      this.add1SecondTaskButton.Size = new System.Drawing.Size(137, 23);
      this.add1SecondTaskButton.TabIndex = 2;
      this.add1SecondTaskButton.Text = "Add 1 Second Task";
      this.add1SecondTaskButton.UseVisualStyleBackColor = true;
      this.add1SecondTaskButton.Click += new System.EventHandler(this.add1SecondTaskClicked);
      // 
      // add2SecondTaskButton
      // 
      this.add2SecondTaskButton.Location = new System.Drawing.Point(155, 117);
      this.add2SecondTaskButton.Name = "add2SecondTaskButton";
      this.add2SecondTaskButton.Size = new System.Drawing.Size(137, 23);
      this.add2SecondTaskButton.TabIndex = 3;
      this.add2SecondTaskButton.Text = "Add 2 Second Task";
      this.add2SecondTaskButton.UseVisualStyleBackColor = true;
      this.add2SecondTaskButton.Click += new System.EventHandler(this.add2SecondTaskClicked);
      // 
      // add4SecondTaskButton
      // 
      this.add4SecondTaskButton.Location = new System.Drawing.Point(298, 117);
      this.add4SecondTaskButton.Name = "add4SecondTaskButton";
      this.add4SecondTaskButton.Size = new System.Drawing.Size(137, 23);
      this.add4SecondTaskButton.TabIndex = 4;
      this.add4SecondTaskButton.Text = "Add 4 Second Task";
      this.add4SecondTaskButton.UseVisualStyleBackColor = true;
      this.add4SecondTaskButton.Click += new System.EventHandler(this.add4SecondTaskClicked);
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoEllipsis = true;
      this.descriptionLabel.Location = new System.Drawing.Point(12, 9);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(423, 105);
      this.descriptionLabel.TabIndex = 5;
      this.descriptionLabel.Text = resources.GetString("descriptionLabel.Text");
      // 
      // TrackingBarDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(447, 172);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.add4SecondTaskButton);
      this.Controls.Add(this.add2SecondTaskButton);
      this.Controls.Add(this.add1SecondTaskButton);
      this.Controls.Add(this.demoStatusStrip);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "TrackingBarDemoForm";
      this.Text = "Tracking Bar Demo";
      this.demoStatusStrip.ResumeLayout(false);
      this.demoStatusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip demoStatusStrip;
    private Nuclex.Windows.Forms.ToolStripTrackingBar demoTrackingBar;
    private System.Windows.Forms.Button add1SecondTaskButton;
    private System.Windows.Forms.Button add2SecondTaskButton;
    private System.Windows.Forms.Button add4SecondTaskButton;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.ToolStripStatusLabel demoStatusLabel;

  }

} // namespace Nuclex.Windows.Forms.Demo