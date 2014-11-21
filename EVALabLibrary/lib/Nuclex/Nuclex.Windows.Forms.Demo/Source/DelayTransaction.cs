using System;
using System.Collections.Generic;
using System.Threading;

using Nuclex.Support.Tracking;
using Nuclex.Support.Scheduling;

namespace Nuclex.Windows.Forms.Demo {

  /// <summary>A transaction that spends some time in the background thread</summary>
  public class DelayTransaction : Transaction, IAbortable, IProgressReporter {

    /// <summary>will be triggered to report when progress has been achieved</summary>
    public event EventHandler<ProgressReportEventArgs> AsyncProgressChanged;

    /// <summary>Initializes a new delay transaction</summary>
    /// <param name="seconds">Number of seconds to spend in the background task</param>
    public DelayTransaction(float seconds) {
      this.seconds = seconds;

      this.delayThread = new Thread(new ThreadStart(asyncExecute));
      this.delayThread.Name = "Delay Transaction Background Task";
      this.delayThread.IsBackground = true;
      this.delayThread.Start();
    }

    /// <summary>Aborts the transaction</summary>
    public void AsyncAbort() {
      this.aborted = true;
    }

    /// <summary>Fires the progress update event</summary>
    /// <param name="progress">Progress to report (ranging from 0.0 to 1.0)</param>
    /// <remarks>
    ///   Informs the observers of this transaction about the achieved progress.
    /// </remarks>
    protected virtual void OnAsyncProgressChanged(float progress) {
      OnAsyncProgressChanged(new ProgressReportEventArgs(progress));
    }

    /// <summary>Fires the progress update event</summary>
    /// <param name="eventArguments">Progress to report (ranging from 0.0 to 1.0)</param>
    /// <remarks>
    ///   Informs the observers of this transaction about the achieved progress.
    ///   Allows for classes derived from the Transaction class to easily provide
    ///   a custom event arguments class that has been derived from the
    ///   Transaction's ProgressUpdateEventArgs class.
    /// </remarks>
    protected virtual void OnAsyncProgressChanged(ProgressReportEventArgs eventArguments) {
      EventHandler<ProgressReportEventArgs> copy = AsyncProgressChanged;
      if(copy != null)
        copy(this, eventArguments);
    }

    /// <summary>Executes in a background thread to provide the delay</summary>
    private void asyncExecute() {
      try {
        int hundredthSecondsCount = (int)(this.seconds * 100.0f);

        for(int hundredth = 0; hundredth < hundredthSecondsCount; ++hundredth) {
          Thread.Sleep(10);
          float progress = (float)hundredth / (float)hundredthSecondsCount;
          OnAsyncProgressChanged(progress);

          if(aborted)
            break;
        }
      }
      finally {
        OnAsyncEnded();
      }
    }

    /// <summary>Thread that is delaying the completion of the transaction</summary>
    private Thread delayThread;
    /// <summary>Number of seconds the completion will be delayed</summary>
    private float seconds;
    /// <summary>True if the delay transaction has been aborted</summary>
    private volatile bool aborted;

  }

} // namespace Nuclex.Windows.Forms.Demo
