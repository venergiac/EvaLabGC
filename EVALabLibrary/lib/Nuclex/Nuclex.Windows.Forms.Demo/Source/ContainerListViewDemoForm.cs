using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nuclex.Windows.Forms.Demo {

  /// <summary>Demonstration dialog for the container list view</summary>
  public partial class ContainerListViewDemoForm : Form {

    /// <summary>Initializes a new container list view demonstration dialog</summary>
    public ContainerListViewDemoForm() {
      InitializeComponent();

      this.Disposed += new EventHandler(onDisposed);

      this.resolutionsCombo = new ComboBox();
      this.resolutionsCombo.Items.Add("640x480");
      this.resolutionsCombo.Items.Add("800x600");
      this.resolutionsCombo.Items.Add("1024x768");
      this.resolutionsCombo.SelectedIndex = 1;
      this.resolutionsCombo.DropDownStyle = ComboBoxStyle.DropDownList;

      this.antialiasingCombo = new ComboBox();
      this.antialiasingCombo.Items.Add("Disabled");
      this.antialiasingCombo.Items.Add("2x2 Samples");
      this.antialiasingCombo.Items.Add("2x4 Samples");
      this.antialiasingCombo.Items.Add("4x4 Samples");
      this.antialiasingCombo.SelectedIndex = 0;
      this.antialiasingCombo.DropDownStyle = ComboBoxStyle.DropDownList;

      this.verticalRetraceCheck = new CheckBox();
      this.verticalRetraceCheck.Checked = false;

      this.containerListView.EmbeddedControls.Add(
        new ListViewEmbeddedControl(this.resolutionsCombo, 0, 2)
      );
      this.containerListView.EmbeddedControls.Add(
        new ListViewEmbeddedControl(this.antialiasingCombo, 1, 2)
      );
      this.containerListView.EmbeddedControls.Add(
        new ListViewEmbeddedControl(this.verticalRetraceCheck, 2, 2)
      );
    }

    /// <summary>Called when the demonstration dialog is being disposed</summary>
    /// <param name="sender">Demonstration dialog that is being disposed</param>
    /// <param name="arguments">Not used</param>
    private void onDisposed(object sender, EventArgs arguments) {
      if(this.verticalRetraceCheck != null) {
        this.verticalRetraceCheck.Dispose();
        this.verticalRetraceCheck = null;
      }
      if(this.antialiasingCombo != null) {
        this.antialiasingCombo.Dispose();
        this.antialiasingCombo = null;
      }
      if(this.resolutionsCombo != null) {
        this.resolutionsCombo.Dispose();
        this.resolutionsCombo = null;
      }
    }

    /// <summary>Combo box with the screen resolutions the user can select from</summary>
    private ComboBox resolutionsCombo;
    /// <summary>Combo box with the antialiasing modes the user can select from</summary>
    private ComboBox antialiasingCombo;
    /// <summary>Check box allowing the vertical retrace wait to be enabled</summary>
    private CheckBox verticalRetraceCheck;

  }

} // namespace Nuclex.Windows.Forms.Demo
