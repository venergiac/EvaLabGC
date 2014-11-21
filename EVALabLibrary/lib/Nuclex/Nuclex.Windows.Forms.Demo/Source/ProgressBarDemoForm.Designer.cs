namespace Nuclex.Windows.Forms.Demo {

  partial class ProgressBarDemoForm {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBarDemoForm));
      this.asyncProgressBar = new Nuclex.Windows.Forms.AsyncProgressBar();
      this.runAsyncDemoButton = new System.Windows.Forms.Button();
      this.normalProgressBar = new System.Windows.Forms.ProgressBar();
      this.runNormalDemoButton = new System.Windows.Forms.Button();
      this.asyncProgressBarLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // asyncProgressBar
      // 
      this.asyncProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.asyncProgressBar.Location = new System.Drawing.Point(15, 116);
      this.asyncProgressBar.Name = "asyncProgressBar";
      this.asyncProgressBar.Size = new System.Drawing.Size(325, 24);
      this.asyncProgressBar.TabIndex = 0;
      // 
      // runAsyncDemoButton
      // 
      this.runAsyncDemoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.runAsyncDemoButton.Location = new System.Drawing.Point(346, 116);
      this.runAsyncDemoButton.Name = "runAsyncDemoButton";
      this.runAsyncDemoButton.Size = new System.Drawing.Size(81, 24);
      this.runAsyncDemoButton.TabIndex = 1;
      this.runAsyncDemoButton.Text = "Test";
      this.runAsyncDemoButton.UseVisualStyleBackColor = true;
      this.runAsyncDemoButton.Click += new System.EventHandler(this.runAsyncDemoClicked);
      // 
      // normalProgressBar
      // 
      this.normalProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.normalProgressBar.Location = new System.Drawing.Point(15, 259);
      this.normalProgressBar.Name = "normalProgressBar";
      this.normalProgressBar.Size = new System.Drawing.Size(325, 24);
      this.normalProgressBar.TabIndex = 2;
      // 
      // runNormalDemoButton
      // 
      this.runNormalDemoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.runNormalDemoButton.Location = new System.Drawing.Point(346, 259);
      this.runNormalDemoButton.Name = "runNormalDemoButton";
      this.runNormalDemoButton.Size = new System.Drawing.Size(81, 24);
      this.runNormalDemoButton.TabIndex = 3;
      this.runNormalDemoButton.Text = "Test";
      this.runNormalDemoButton.UseVisualStyleBackColor = true;
      this.runNormalDemoButton.Click += new System.EventHandler(this.runNormalDemoClicked);
      // 
      // asyncProgressBarLabel
      // 
      this.asyncProgressBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.asyncProgressBarLabel.AutoEllipsis = true;
      this.asyncProgressBarLabel.Location = new System.Drawing.Point(12, 165);
      this.asyncProgressBarLabel.Name = "asyncProgressBarLabel";
      this.asyncProgressBarLabel.Size = new System.Drawing.Size(412, 91);
      this.asyncProgressBarLabel.TabIndex = 4;
      this.asyncProgressBarLabel.Text = resources.GetString("asyncProgressBarLabel.Text");
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoEllipsis = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(412, 104);
      this.label1.TabIndex = 5;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // ProgressBarDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(436, 295);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.runNormalDemoButton);
      this.Controls.Add(this.normalProgressBar);
      this.Controls.Add(this.runAsyncDemoButton);
      this.Controls.Add(this.asyncProgressBar);
      this.Controls.Add(this.asyncProgressBarLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "ProgressBarDemoForm";
      this.Text = "Asynchronous Progress Bar Demo";
      this.ResumeLayout(false);

    }

    #endregion

    private AsyncProgressBar asyncProgressBar;
    private System.Windows.Forms.Button runAsyncDemoButton;
    private System.Windows.Forms.ProgressBar normalProgressBar;
    private System.Windows.Forms.Button runNormalDemoButton;
    private System.Windows.Forms.Label asyncProgressBarLabel;
    private System.Windows.Forms.Label label1;
  }

} // namespace Nuclex.Windows.Forms.Demo
