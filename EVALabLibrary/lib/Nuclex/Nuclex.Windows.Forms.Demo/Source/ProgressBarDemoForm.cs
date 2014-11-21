using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Nuclex.Windows.Forms.Demo {

  /// <summary>Demonstration showing off the asynchronous progress bar</summary>
  public partial class ProgressBarDemoForm : Form {

    /// <summary>Initializes a new progress bar demo dialog</summary>
    public ProgressBarDemoForm() {
      InitializeComponent();

      this.Disposed += new EventHandler(formDisposed);
    }

    /// <summary>Called when the form is being manually disposed (not by the GC)</summary>
    /// <param name="sender">Form that is being disposed</param>
    /// <param name="arguments">Not used</param>
    private void formDisposed(object sender, EventArgs arguments) {

      // Make sure the worker thread for the asynchronous progress bar is stopped
      Thread myAsyncProgressUpdateThread = this.asyncProgressUpdateThread;
      if(myAsyncProgressUpdateThread != null) {
        myAsyncProgressUpdateThread.Abort();
        myAsyncProgressUpdateThread.Join();
      }

      // Make sure the worker thread for the normal progress bar is stopped
      Thread myNormalProgressUpdateThread = this.normalProgressUpdateThread;
      if(myNormalProgressUpdateThread != null) {
        myNormalProgressUpdateThread.Abort();
        myNormalProgressUpdateThread.Join();
      }
    }

    /// <summary>Runs the demo of the normal progress bar</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void runNormalDemoClicked(object sender, EventArgs arguments) {

      // Make sure the demo is not run multiple times in parallel (not a problem,
      // but when two threads are updating a single progress bar, the progress bar
      // tends to get rather jumpy :p)
      lock(this) {
        if(this.normalProgressUpdateThread != null)
          return;

        this.normalProgressUpdateThread = new Thread(new ThreadStart(normalProgressBarWorker));
        this.normalProgressUpdateThread.IsBackground = true;
        this.normalProgressUpdateThread.Name = "Normal Progress Bar Updater";
      }

      // Disable the button first, then run the demo (if the order was reversed,
      // the demo could theoretically complete before the button was disabled, meaning
      // it would be disabled after the demo ran and never be enabled again).
      this.runNormalDemoButton.Enabled = false;
      this.normalProgressUpdateThread.Start();

    }

    /// <summary>Runs the demo of the asynchronous progress bar</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="arguments">Not used</param>
    private void runAsyncDemoClicked(object sender, EventArgs arguments) {

      // Make sure the demo is not run multiple times in parallel (not a problem,
      // but when two threads are updating a single progress bar, the progress bar
      // tends to get rather jumpy :p)
      lock(this) {
        if(this.asyncProgressUpdateThread != null)
          return;

        this.asyncProgressUpdateThread = new Thread(new ThreadStart(asyncProgressBarWorker));
        this.asyncProgressUpdateThread.IsBackground = true;
        this.asyncProgressUpdateThread.Name = "Async Progress Bar Updater";
      }

      // Disable the button first, then run the demo (if the order was reversed,
      // the demo could theoretically complete before the button was disabled, meaning
      // it would be disabled after the demo ran and never be enabled again).
      this.runAsyncDemoButton.Enabled = false;
      this.asyncProgressUpdateThread.Start();

    }

    /// <summary>Worker thread that updates the asynchronous progress bar</summary>
    private void asyncProgressBarWorker() {
      const int TickCount = 1000000;

      for(int tick = 0; tick < TickCount; ++tick)
        this.asyncProgressBar.AsyncSetValue((float)tick / (float)TickCount);

      // Set the thread field to null in order to notify the UI thread that no
      // test is running anymore
      this.asyncProgressUpdateThread = null;

      // Reenable the test start button
      this.runAsyncDemoButton.Invoke(
        (MethodInvoker)delegate() { this.runAsyncDemoButton.Enabled = true; }
      );

    }

    /// <summary>Worker thread that updates the normal progress bar</summary>
    private void normalProgressBarWorker() {
      const int TickCount = 1000000;

      for(int tick = 0; tick < TickCount; ++tick) {

        // Try using BeginInvoke() here. You will totally trash the windows message queue,
        // but still not be any faster than the asynchronous progress bar. Better yet,
        // since windows cannot handle 10 million messages, the message to reenable the
        // demo button might get lost and the button might stay disabled :D
        this.normalProgressBar.Invoke(
          (MethodInvoker)delegate() {
            float progress = (float)tick / (float)TickCount;
            int value = (int)(
              progress * (this.normalProgressBar.Maximum - this.normalProgressBar.Minimum) +
              this.normalProgressBar.Minimum
            );
            this.normalProgressBar.Value = value;
          }
        );
      }

      // Set the thread field to null in order to notify the UI thread that no
      // test is running anymore
      this.normalProgressUpdateThread = null;

      // Reenable the test start button
      this.runNormalDemoButton.Invoke(
        (MethodInvoker)delegate() { this.runNormalDemoButton.Enabled = true; }
      );

    }

    /// <summary>
    ///   Thread performing the progress updates for the asynchronous progress bar
    /// </summary>
    private volatile Thread asyncProgressUpdateThread;
    /// <summary>
    ///   Thread performing the progress updates for the normal progress bar
    /// </summary>
    private volatile Thread normalProgressUpdateThread;

  }

} // namespace Nuclex.Windows.Forms.Demo