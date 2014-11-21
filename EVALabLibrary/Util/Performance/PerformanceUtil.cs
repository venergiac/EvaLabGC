using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EVALab.Util.Performance
{
    public class PerformanceUtil
    {
        /// <summary>
        /// Imports the <code>QueryPerformanceFrequency</code> method into the class. The method is used to measure the current
        /// tickcount of the system.
        /// </summary>
        /// <param name="ticks">current tick count</param>
        [DllImport("Kernel32.dll")]
        public static extern void QueryPerformanceCounter(ref long ticks);

        private static PerformanceUtil pf = null;
        public static PerformanceUtil Instance
        {
            get
            {
                if (pf == null)
                {
                    pf = new PerformanceUtil();
                }
                return pf;
            }
        }


        long startTime = 0;
        long endTime = 0;
        long totTime = 0;
        long call = 0;
        public void BeginAverageTime()
        {
            QueryPerformanceCounter(ref startTime);
        }

        public void EndAverageTime()
        {
            QueryPerformanceCounter(ref endTime);
            call++;
            totTime += endTime - startTime;
            if (call >= 100)
            {
                Trace.WriteLine("Performance " + (totTime / call));
                totTime = 0;
                call = 0;
                Trace.Flush();
            }
        }
    }
}
