using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVALab.Util.License;

namespace EvaLab.EOG
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());

            } // try
            catch (Exception ex)
            {
                // Catch any error, but especially licensing errors...
                string strErr = String.Format("Error executing application: '{0}'", ex.Message);
                LicenseForm.Show(null, ex.Message, ex);
            } // catch
        }
    }
}
