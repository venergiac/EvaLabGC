using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Nuclex.Support.Tracking;

namespace Nuclex.Windows.Forms.Demo {

  /// <summary>Demonstration form for the background task tracking bar</summary>
  public partial class TrackingBarDemoForm : Form {

    /// <summary>Initializes a new tracking bar demonstration form</summary>
    public TrackingBarDemoForm() {
      InitializeComponent();
    }

    /// <summary>Adds a 1 second task for the tracking bar</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="e">Not used</param>
    private void add1SecondTaskClicked(object sender, EventArgs e) {
      this.demoTrackingBar.Track(new DelayTransaction(1.0f), 1.0f);
    }

    /// <summary>Adds a 2 second task for the tracking bar</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="e">Not used</param>
    private void add2SecondTaskClicked(object sender, EventArgs e) {
      this.demoTrackingBar.Track(new DelayTransaction(2.0f), 2.0f);
    }

    /// <summary>Adds a 4 second task for the tracking bar</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="e">Not used</param>
    private void add4SecondTaskClicked(object sender, EventArgs e) {
      this.demoTrackingBar.Track(new DelayTransaction(4.0f), 4.0f);
    }

  }

} // namespace Nuclex.Windows.Forms.Demo
