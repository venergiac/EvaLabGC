using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nuclex.Windows.Forms.Demo {
  static class Program {
  
    /// <summary>The main entry point for the application.</summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new DemoSelectorForm());
    }

  }

} // namespace Nuclex.Windows.Forms.Demo
