using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using EVALab.Util.License;
using System.ComponentModel;
using EVALab.Util.Box;

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
            try{
                Application.Run(new ControlForm());
            } // try
            catch (LicenseException ex)
            {
                // Catch any error, but especially licensing errors...
                string strErr = String.Format("Error executing application: '{0}'", ex.Message);
                LicenseForm.Show(null, ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Catch any error, but especially licensing errors...
                string strErr = String.Format("Error executing application: '{0}'", ex.Message);
                ExceptionForm.Show(null, ex.Message, ex);
            } // catch
        }
    }
}

