namespace Nuclex.Windows.Forms.Demo {

  partial class ContainerListViewDemoForm {

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Resolution",
            "800x600"}, -1);
      System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Antialiasing",
            "Disabled"}, -1);
      System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Wait for Vertical Retrace",
            "No"}, -1);
      this.containerListView = new Nuclex.Windows.Forms.ContainerListView();
      this.nameColumn = new System.Windows.Forms.ColumnHeader();
      this.defaultColumn = new System.Windows.Forms.ColumnHeader();
      this.valueColumn = new System.Windows.Forms.ColumnHeader();
      this.spacingImages = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // containerListView
      // 
      this.containerListView.AllowColumnReorder = true;
      this.containerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.containerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.defaultColumn,
            this.valueColumn});
      this.containerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
      this.containerListView.Location = new System.Drawing.Point(12, 12);
      this.containerListView.MultiSelect = false;
      this.containerListView.Name = "containerListView";
      this.containerListView.Size = new System.Drawing.Size(429, 277);
      this.containerListView.SmallImageList = this.spacingImages;
      this.containerListView.TabIndex = 0;
      this.containerListView.UseCompatibleStateImageBehavior = false;
      this.containerListView.View = System.Windows.Forms.View.Details;
      // 
      // nameColumn
      // 
      this.nameColumn.Text = "Name";
      this.nameColumn.Width = 180;
      // 
      // defaultColumn
      // 
      this.defaultColumn.Text = "Default";
      this.defaultColumn.Width = 120;
      // 
      // valueColumn
      // 
      this.valueColumn.Text = "Value";
      this.valueColumn.Width = 120;
      // 
      // spacingImages
      // 
      this.spacingImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.spacingImages.ImageSize = new System.Drawing.Size(1, 20);
      this.spacingImages.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // ContainerListViewDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(453, 301);
      this.Controls.Add(this.containerListView);
      this.Name = "ContainerListViewDemoForm";
      this.Text = "Control Container ListView Demo";
      this.ResumeLayout(false);

    }

    #endregion

    private ContainerListView containerListView;
    private System.Windows.Forms.ColumnHeader nameColumn;
    private System.Windows.Forms.ColumnHeader defaultColumn;
    private System.Windows.Forms.ColumnHeader valueColumn;
    private System.Windows.Forms.ImageList spacingImages;

  }

} // namespace Nuclex.Windows.Forms.Demo