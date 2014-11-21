using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nuclex.Windows.Forms.Demo {

  /// <summary>Dialog allowing the user to select the demonstration to run</summary>
  public partial class DemoSelectorForm : Form {

    /// <summary>Initializes a new demo selection form</summary>
    public DemoSelectorForm() {
      InitializeComponent();
    }

    /// <summary>Runs the asynchronous progress bar demonstration</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void asyncProgressBarDemoClicked(object sender, EventArgs arguments) {
      using(ProgressBarDemoForm form = new ProgressBarDemoForm()) {
        form.ShowDialog();
      }
    }

    /// <summary>Runs the tracking bar demonstration</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void trackingBarDemoClicked(object sender, EventArgs arguments) {
      using(TrackingBarDemoForm form = new TrackingBarDemoForm()) {
        form.ShowDialog();
      }
    }

    /// <summary>Runs the progress reporter demonstration</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void progressReporterDemoClicked(object sender, EventArgs arguments) {
      ProgressReporterForm.Track(new DelayTransaction(5.0f));
    }

    /// <summary>Runs the container list view demonstration</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void containerListViewDemoClicked(object sender, EventArgs arguments) {
      using(ContainerListViewDemoForm form = new ContainerListViewDemoForm()) {
        form.ShowDialog();
      }
    }

  }

} // namespace Nuclex.Windows.Forms.Demo