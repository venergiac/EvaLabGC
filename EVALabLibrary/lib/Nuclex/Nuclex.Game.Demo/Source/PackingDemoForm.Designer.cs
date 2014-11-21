namespace Nuclex.Game.Demo {

  partial class PackingDemoForm {

    /// <summary>Required designer variable.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Cleans up any resources being used.</summary>
    /// <param name="disposing">
    ///   True if managed resources should be disposed; otherwise, false.
    /// </param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///   Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.packingControlsGroup = new System.Windows.Forms.GroupBox();
      this.packButton = new System.Windows.Forms.Button();
      this.maximumHeightLabel = new System.Windows.Forms.Label();
      this.areaLabel = new System.Windows.Forms.Label();
      this.packingAlgorithmLabel = new System.Windows.Forms.Label();
      this.minimumHeightLabel = new System.Windows.Forms.Label();
      this.packingAlgorithmCombo = new System.Windows.Forms.ComboBox();
      this.maximumWidthLabel = new System.Windows.Forms.Label();
      this.minimumWidthLabel = new System.Windows.Forms.Label();
      this.maximumRectangleSizeLabel = new System.Windows.Forms.Label();
      this.maximumHeightEdit = new System.Windows.Forms.NumericUpDown();
      this.minimumRectangleSizeLabel = new System.Windows.Forms.Label();
      this.maximumWidthEdit = new System.Windows.Forms.NumericUpDown();
      this.minimumHeightEdit = new System.Windows.Forms.NumericUpDown();
      this.minimumWidthEdit = new System.Windows.Forms.NumericUpDown();
      this.packingViewPicture = new System.Windows.Forms.PictureBox();
      this.packingControlsGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.maximumHeightEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.maximumWidthEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.minimumHeightEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.minimumWidthEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.packingViewPicture)).BeginInit();
      this.SuspendLayout();
      // 
      // packingControlsGroup
      // 
      this.packingControlsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.packingControlsGroup.Controls.Add(this.packButton);
      this.packingControlsGroup.Controls.Add(this.maximumHeightLabel);
      this.packingControlsGroup.Controls.Add(this.areaLabel);
      this.packingControlsGroup.Controls.Add(this.packingAlgorithmLabel);
      this.packingControlsGroup.Controls.Add(this.minimumHeightLabel);
      this.packingControlsGroup.Controls.Add(this.packingAlgorithmCombo);
      this.packingControlsGroup.Controls.Add(this.maximumWidthLabel);
      this.packingControlsGroup.Controls.Add(this.minimumWidthLabel);
      this.packingControlsGroup.Controls.Add(this.maximumRectangleSizeLabel);
      this.packingControlsGroup.Controls.Add(this.maximumHeightEdit);
      this.packingControlsGroup.Controls.Add(this.minimumRectangleSizeLabel);
      this.packingControlsGroup.Controls.Add(this.maximumWidthEdit);
      this.packingControlsGroup.Controls.Add(this.minimumHeightEdit);
      this.packingControlsGroup.Controls.Add(this.minimumWidthEdit);
      this.packingControlsGroup.Location = new System.Drawing.Point(12, 532);
      this.packingControlsGroup.Name = "packingControlsGroup";
      this.packingControlsGroup.Size = new System.Drawing.Size(514, 98);
      this.packingControlsGroup.TabIndex = 0;
      this.packingControlsGroup.TabStop = false;
      this.packingControlsGroup.Text = "Packing Controls";
      // 
      // packButton
      // 
      this.packButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.packButton.Location = new System.Drawing.Point(393, 46);
      this.packButton.Name = "packButton";
      this.packButton.Size = new System.Drawing.Size(115, 46);
      this.packButton.TabIndex = 12;
      this.packButton.Text = "&Pack";
      this.packButton.UseVisualStyleBackColor = true;
      this.packButton.Click += new System.EventHandler(this.packButtonClicked);
      // 
      // maximumHeightLabel
      // 
      this.maximumHeightLabel.AutoSize = true;
      this.maximumHeightLabel.Location = new System.Drawing.Point(267, 74);
      this.maximumHeightLabel.Name = "maximumHeightLabel";
      this.maximumHeightLabel.Size = new System.Drawing.Size(41, 13);
      this.maximumHeightLabel.TabIndex = 10;
      this.maximumHeightLabel.Text = "Height:";
      // 
      // areaLabel
      // 
      this.areaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.areaLabel.AutoSize = true;
      this.areaLabel.Location = new System.Drawing.Point(390, 22);
      this.areaLabel.Name = "areaLabel";
      this.areaLabel.Size = new System.Drawing.Size(58, 13);
      this.areaLabel.TabIndex = 0;
      this.areaLabel.Text = "Area: ? x ?";
      // 
      // packingAlgorithmLabel
      // 
      this.packingAlgorithmLabel.AutoSize = true;
      this.packingAlgorithmLabel.Location = new System.Drawing.Point(6, 22);
      this.packingAlgorithmLabel.Name = "packingAlgorithmLabel";
      this.packingAlgorithmLabel.Size = new System.Drawing.Size(95, 13);
      this.packingAlgorithmLabel.TabIndex = 0;
      this.packingAlgorithmLabel.Text = "Packing Algorithm:";
      // 
      // minimumHeightLabel
      // 
      this.minimumHeightLabel.AutoSize = true;
      this.minimumHeightLabel.Location = new System.Drawing.Point(267, 48);
      this.minimumHeightLabel.Name = "minimumHeightLabel";
      this.minimumHeightLabel.Size = new System.Drawing.Size(41, 13);
      this.minimumHeightLabel.TabIndex = 5;
      this.minimumHeightLabel.Text = "Height:";
      // 
      // packingAlgorithmCombo
      // 
      this.packingAlgorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.packingAlgorithmCombo.FormattingEnabled = true;
      this.packingAlgorithmCombo.Location = new System.Drawing.Point(107, 19);
      this.packingAlgorithmCombo.Name = "packingAlgorithmCombo";
      this.packingAlgorithmCombo.Size = new System.Drawing.Size(266, 21);
      this.packingAlgorithmCombo.TabIndex = 1;
      // 
      // maximumWidthLabel
      // 
      this.maximumWidthLabel.AutoSize = true;
      this.maximumWidthLabel.Location = new System.Drawing.Point(142, 74);
      this.maximumWidthLabel.Name = "maximumWidthLabel";
      this.maximumWidthLabel.Size = new System.Drawing.Size(38, 13);
      this.maximumWidthLabel.TabIndex = 8;
      this.maximumWidthLabel.Text = "Width:";
      // 
      // minimumWidthLabel
      // 
      this.minimumWidthLabel.AutoSize = true;
      this.minimumWidthLabel.Location = new System.Drawing.Point(142, 48);
      this.minimumWidthLabel.Name = "minimumWidthLabel";
      this.minimumWidthLabel.Size = new System.Drawing.Size(38, 13);
      this.minimumWidthLabel.TabIndex = 3;
      this.minimumWidthLabel.Text = "Width:";
      // 
      // maximumRectangleSizeLabel
      // 
      this.maximumRectangleSizeLabel.AutoSize = true;
      this.maximumRectangleSizeLabel.Location = new System.Drawing.Point(6, 74);
      this.maximumRectangleSizeLabel.Name = "maximumRectangleSizeLabel";
      this.maximumRectangleSizeLabel.Size = new System.Drawing.Size(126, 13);
      this.maximumRectangleSizeLabel.TabIndex = 7;
      this.maximumRectangleSizeLabel.Text = "Maximum Rectangle Size";
      // 
      // maximumHeightEdit
      // 
      this.maximumHeightEdit.Location = new System.Drawing.Point(311, 72);
      this.maximumHeightEdit.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
      this.maximumHeightEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.maximumHeightEdit.Name = "maximumHeightEdit";
      this.maximumHeightEdit.Size = new System.Drawing.Size(62, 20);
      this.maximumHeightEdit.TabIndex = 11;
      this.maximumHeightEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.maximumHeightEdit.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
      // 
      // minimumRectangleSizeLabel
      // 
      this.minimumRectangleSizeLabel.AutoSize = true;
      this.minimumRectangleSizeLabel.Location = new System.Drawing.Point(6, 48);
      this.minimumRectangleSizeLabel.Name = "minimumRectangleSizeLabel";
      this.minimumRectangleSizeLabel.Size = new System.Drawing.Size(123, 13);
      this.minimumRectangleSizeLabel.TabIndex = 2;
      this.minimumRectangleSizeLabel.Text = "Minimum Rectangle Size";
      // 
      // maximumWidthEdit
      // 
      this.maximumWidthEdit.Location = new System.Drawing.Point(186, 72);
      this.maximumWidthEdit.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
      this.maximumWidthEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.maximumWidthEdit.Name = "maximumWidthEdit";
      this.maximumWidthEdit.Size = new System.Drawing.Size(62, 20);
      this.maximumWidthEdit.TabIndex = 9;
      this.maximumWidthEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.maximumWidthEdit.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
      // 
      // minimumHeightEdit
      // 
      this.minimumHeightEdit.Location = new System.Drawing.Point(311, 46);
      this.minimumHeightEdit.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
      this.minimumHeightEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.minimumHeightEdit.Name = "minimumHeightEdit";
      this.minimumHeightEdit.Size = new System.Drawing.Size(62, 20);
      this.minimumHeightEdit.TabIndex = 6;
      this.minimumHeightEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.minimumHeightEdit.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      // 
      // minimumWidthEdit
      // 
      this.minimumWidthEdit.Location = new System.Drawing.Point(186, 46);
      this.minimumWidthEdit.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
      this.minimumWidthEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.minimumWidthEdit.Name = "minimumWidthEdit";
      this.minimumWidthEdit.Size = new System.Drawing.Size(62, 20);
      this.minimumWidthEdit.TabIndex = 4;
      this.minimumWidthEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.minimumWidthEdit.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      // 
      // packingViewPicture
      // 
      this.packingViewPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.packingViewPicture.BackColor = System.Drawing.Color.White;
      this.packingViewPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.packingViewPicture.Location = new System.Drawing.Point(12, 12);
      this.packingViewPicture.Name = "packingViewPicture";
      this.packingViewPicture.Size = new System.Drawing.Size(514, 514);
      this.packingViewPicture.TabIndex = 1;
      this.packingViewPicture.TabStop = false;
      this.packingViewPicture.Resize += new System.EventHandler(this.packingViewResized);
      this.packingViewPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.paintPackingViewPicture);
      // 
      // PackingDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(538, 642);
      this.Controls.Add(this.packingViewPicture);
      this.Controls.Add(this.packingControlsGroup);
      this.MinimumSize = new System.Drawing.Size(538, 165);
      this.Name = "PackingDemoForm";
      this.Text = "Rectangle Packing Demo";
      this.packingControlsGroup.ResumeLayout(false);
      this.packingControlsGroup.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.maximumHeightEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.maximumWidthEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.minimumHeightEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.minimumWidthEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.packingViewPicture)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    /// <summary>Group that holds the controls to configure the packing demo</summary>
    private System.Windows.Forms.GroupBox packingControlsGroup;
    /// <summary>Runs the packing algorithm with the current settings</summary>
    private System.Windows.Forms.Button packButton;
    /// <summary>Description for the maximum rectangle height field</summary>
    private System.Windows.Forms.Label maximumHeightLabel;
    /// <summary>Description for the packing algorithm selection combo</summary>
    private System.Windows.Forms.Label packingAlgorithmLabel;
    /// <summary>Description for the minimum rectangle height field</summary>
    private System.Windows.Forms.Label minimumHeightLabel;
    /// <summary>List for the selection of a rectangle packing algorithm</summary>
    private System.Windows.Forms.ComboBox packingAlgorithmCombo;
    /// <summary>Description of the maximum rectangle width field</summary>
    private System.Windows.Forms.Label maximumWidthLabel;
    /// <summary>Description of the minimum rectangle width field</summary>
    private System.Windows.Forms.Label minimumWidthLabel;
    /// <summary>Descrtiption for the maximum rectangle size row</summary>
    private System.Windows.Forms.Label maximumRectangleSizeLabel;
    /// <summary>Controls the maximum height of the generated rectangles</summary>
    private System.Windows.Forms.NumericUpDown maximumHeightEdit;
    /// <summary>Descrtiption for the minimum rectangle size row</summary>
    private System.Windows.Forms.Label minimumRectangleSizeLabel;
    /// <summary>Controls the maximum width of the generated rectangles</summary>
    private System.Windows.Forms.NumericUpDown maximumWidthEdit;
    /// <summary>Controls the minimum height of the generated rectangles</summary>
    private System.Windows.Forms.NumericUpDown minimumHeightEdit;
    /// <summary>Controls the minimum width of the generated rectangles</summary>
    private System.Windows.Forms.NumericUpDown minimumWidthEdit;
    /// <summary>Displays the results of the executed packing algorithm</summary>
    private System.Windows.Forms.PictureBox packingViewPicture;
    private System.Windows.Forms.Label areaLabel;

  }

} // namespace Nuclex.Game.Demo