using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {
        SerialPort sp = new SerialPort();
        List<byte[]> dataList = new List<byte[]>();

        public Program()
        {
        }

        public void Start()
        {
            //ports
            sp.PortName = "COM9";
            sp.BaudRate = 57600;
            sp.WriteBufferSize = 8;
            sp.ReadTimeout = 20;
            sp.WriteTimeout = 1000;
            
            ReadDataFile();
            sp.Open();
            

            if (sp.IsOpen)
            {
                sp.DataReceived+=new SerialDataReceivedEventHandler(sp_DataReceived);
            }
            else
            {
                throw (new Exception("Port COM is not opened."));
            }
        }

        public void Stop()
        {
            sp.Close();
        }

        byte[] dataB = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        void ReadDataFile()
        {
            StreamReader stream = new StreamReader("DATA.txt");
            string str = null;
            while ((str = stream.ReadLine()) != null)
            {
                string[] datas = str.Split(' ');

                byte[] dataBOut = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                int x = Int32.Parse(datas[0]);
                int y = Int32.Parse(datas[2]);
                int p = Int32.Parse(datas[4]);
                dataBOut[2] = (byte)(p >> 8);
                dataBOut[3] = (byte)(p);
                dataBOut[4] = (byte)(x >> 8);
                dataBOut[5] = (byte)(x);
                dataBOut[6] = (byte)(y >> 8);
                dataBOut[7] = (byte)(y);

                dataList.Add(dataBOut);
            }

            stream.Close();
        }
        
        int idx=0;
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            sp.Write(dataList[idx++], 0, 8);
            if (idx >= dataList.Count)
            {
                idx = 0;
                Console.WriteLine("restart");
            }
            if (idx%240==0)Console.WriteLine("" + idx + " " + dataList.Count);
        }

        static void Main(string[] args)
        {
            Program prg = new Program();
            /*prg.Start();
            Console.ReadKey();
            prg.Stop();*/
            long t = prg.GetTimeMS();
            while (true)
                Console.WriteLine((prg.GetTimeMS() - t));
        }

        /// Unfortunatly the DateTime.Now.Ticks doesn't work at resolution < 16ms
        /// </summary>
        /// <param name="lpSystemTime"></param>
        #region GetTimeMSByKernel
        [DllImport("Kernel32.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        public long GetTimeMS()
        {
            // Call the native GetSystemTime method
            // with the defined structure.
            SYSTEMTIME stime = new SYSTEMTIME();
            GetSystemTime(ref stime);

            return (long)stime.wMilliseconds + (long)stime.wSecond * 1000 + (long)stime.wMinute * 60000;
        }
        #endregion
    }
}
