using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using EVALab.Util;
using Win32;
using EVALabAnalysis;
using EVALab.Analysis;
using EVALab.Analysis.Data;
using EVALab.Analysis.Fixation;
using EVALab.Analysis.Saccade;
using EVALab.Analysis.WaveForm;

using EVALab.Analysis.ROI;


namespace WindowsFormsApplicationTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            ROIManager mngr = new ROIManager();
            List<EVALab.Analysis.ROI.ROIManager.Counter> matchs = new List<EVALab.Analysis.ROI.ROIManager.Counter>();
            List<EVALab.Analysis.ROI.ROIManager.Counter> source = new List<EVALab.Analysis.ROI.ROIManager.Counter>();

            Random rnd = new Random();
           
            //match
            for (int i = 1; i <= 10; i++)
            {
                EVALab.Analysis.ROI.ROIManager.Counter c = new EVALab.Analysis.ROI.ROIManager.Counter(i,""+i);
                matchs.Add(c);
            }


            for (int j = 1; j <= 50; j++)
            {
                int x = rnd.Next(11);
                EVALab.Analysis.ROI.ROIManager.Counter c = new EVALab.Analysis.ROI.ROIManager.Counter(x,""+x);
                source.Add(c);
            }

            List<int> idxs = mngr.BestMatch(source, matchs);
            Debug.WriteLine("FOUND\n======================");
            foreach (int i in idxs)
            {
                Debug.Write(i +":"+ source[i].idx + " ");
            }
            Debug.WriteLine("\n======================");

            Debug.WriteLine("FROM\n======================");
            int h = 0;
            foreach (EVALab.Analysis.ROI.ROIManager.Counter s in source)
            {
                Debug.Write(h + ":" +s.idx + " ");
                h++;
            }
            Debug.WriteLine("\n======================");

            Debug.WriteLine("FROM\n======================");
            h = 0;
            foreach (EVALab.Analysis.ROI.ROIManager.Counter s in source)
            {
                Debug.Write(s.idx + " ");
                h++;
            }
            Debug.WriteLine("\n======================");


            return;
        }

        public static void testBigIntegerPerformance()
        {
            BigInteger i = new BigInteger();
            HiPerfTimer timer = new HiPerfTimer();
            timer.Start();
            uint c = 0;
            for (uint ii = 1; ii < 150 * 60 * 60 * 15; ii++)
            {
                if (c > 700) c = 0;
                i.setBit(c++);
            }
            timer.Stop();
            Debug.WriteLine("BigInteger: " + i.ToString() + " in " + timer.GetDuration());

            BigInteger i2 = new BigInteger(i.ToString(), 10);
            Debug.WriteLine("BigInteger: " + i2.ToString());

            int x = 0;

            timer.Start();
            for (int ii = 1; ii < 150 * 60 * 60 * 15; ii++)
            {
                x &= (1 >> ii);
            }
            timer.Stop();
            Debug.WriteLine("int: " + timer.GetDuration());

            return;
        }

        public static void testTheeuwes()
        {
            Random rnd = new Random();
            /*while (true)
            {
                if (rnd.Next(0, 6 + 6) >= 6 + 6) break;
                Debug.WriteLine(rnd.Next(0, 6 + 6));
            }*/

            for (int abruptPosition = -1; abruptPosition < 6 + 6; abruptPosition++)
            {
                for (int onPosition = 0; onPosition < 6; onPosition++)
                {
                    int v = 1 << (onPosition + 1) | 1 << (abruptPosition + 9);

                    double position = -1;
                    double aposition = -1;
                    for (int i = 1; i < 9; i++)
                    {
                        if ((v & (1 << i)) != 0)
                        {
                            position = i - 1;
                            break;
                        }
                    }
                    int va = (int)(v >> 9);
                    for (int i = 0; i < 12; i++)
                    {
                        if ((va & (1 << i)) != 0)
                        {
                            aposition = i;
                            break;
                        }
                    }

                    Debug.WriteLine(onPosition + " " + abruptPosition + " = " + v + " " + position + " " + aposition);

                }
            }

        }
    }
}
