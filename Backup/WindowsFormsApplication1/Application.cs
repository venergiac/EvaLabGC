using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace EVALabGC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText("Debug.txt"));
            Debug.Listeners.Add(tr2);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Test.ControlForm());
        }
    }
}

