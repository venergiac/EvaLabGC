namespace Nuclex.Windows.Forms.Demo {

  partial class DemoSelectorForm {
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
      this.asyncProgressBarDemoButton = new System.Windows.Forms.Button();
      this.trackingBarDemoButton = new System.Windows.Forms.Button();
      this.progressReporterDemoButton = new System.Windows.Forms.Button();
      this.containerListViewDemoButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // asyncProgressBarDemoButton
      // 
      this.asyncProgressBarDemoButton.Location = new System.Drawing.Point(12, 12);
      this.asyncProgressBarDemoButton.Name = "asyncProgressBarDemoButton";
      this.asyncProgressBarDemoButton.Size = new System.Drawing.Size(220, 23);
      this.asyncProgressBarDemoButton.TabIndex = 0;
      this.asyncProgressBarDemoButton.Text = "Run Asynchronous Progress Bar Demo";
      this.asyncProgressBarDemoButton.UseVisualStyleBackColor = true;
      this.asyncProgressBarDemoButton.Click += new System.EventHandler(this.asyncProgressBarDemoClicked);
      // 
      // trackingBarDemoButton
      // 
      this.trackingBarDemoButton.Location = new System.Drawing.Point(12, 41);
      this.trackingBarDemoButton.Name = "trackingBarDemoButton";
      this.trackingBarDemoButton.Size = new System.Drawing.Size(220, 23);
      this.trackingBarDemoButton.TabIndex = 1;
      this.trackingBarDemoButton.Text = "Run Tracking Bar Demo";
      this.trackingBarDemoButton.UseVisualStyleBackColor = true;
      this.trackingBarDemoButton.Click += new System.EventHandler(this.trackingBarDemoClicked);
      // 
      // progressReporterDemoButton
      // 
      this.progressReporterDemoButton.Location = new System.Drawing.Point(12, 70);
      this.progressReporterDemoButton.Name = "progressReporterDemoButton";
      this.progressReporterDemoButton.Size = new System.Drawing.Size(220, 23);
      this.progressReporterDemoButton.TabIndex = 2;
      this.progressReporterDemoButton.Text = "Run Progress Reporter Demo";
      this.progressReporterDemoButton.UseVisualStyleBackColor = true;
      this.progressReporterDemoButton.Click += new System.EventHandler(this.progressReporterDemoClicked);
      // 
      // containerListViewDemoButton
      // 
      this.containerListViewDemoButton.Location = new System.Drawing.Point(12, 99);
      this.containerListViewDemoButton.Name = "containerListViewDemoButton";
      this.containerListViewDemoButton.Size = new System.Drawing.Size(220, 23);
      this.containerListViewDemoButton.TabIndex = 2;
      this.containerListViewDemoButton.Text = "Run Control Container ListView Demo";
      this.containerListViewDemoButton.UseVisualStyleBackColor = true;
      this.containerListViewDemoButton.Click += new System.EventHandler(this.containerListViewDemoClicked);
      // 
      // DemoSelectorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(244, 266);
      this.Controls.Add(this.containerListViewDemoButton);
      this.Controls.Add(this.progressReporterDemoButton);
      this.Controls.Add(this.trackingBarDemoButton);
      this.Controls.Add(this.asyncProgressBarDemoButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "DemoSelectorForm";
      this.Text = "Demo Selector";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button asyncProgressBarDemoButton;
    private System.Windows.Forms.Button trackingBarDemoButton;
    private System.Windows.Forms.Button progressReporterDemoButton;
    private System.Windows.Forms.Button containerListViewDemoButton;
  }

} // namespace Nuclex.Windows.Forms.Demo