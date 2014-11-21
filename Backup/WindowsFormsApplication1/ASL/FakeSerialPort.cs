using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace EVALabGC.ASL
{
    class FakeSerialPort
    {
        public event SerialErrorReceivedEventHandler ErrorReceived;

        Win32.HiPerfTimer timer = new Win32.HiPerfTimer();
        Win32.HiPerfTimer timerGlobal = new Win32.HiPerfTimer();
        
        byte[] dataBOut = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        Random rnd = new Random();
        int c = 0;

        int x = 1;
        int y = 1;
        int t = 0;

        private bool open = false;
        public void Open()
        {
            open = true;
            timerGlobal.Start();
            timer.Start();
        }

        public void Close()
        {
            open = false;
        }

        public bool IsOpen
        {
            get{return open;}
        }

        public void Write(char[] data, int s, int l)
        {
            DiscardInBuffer();
        }

        public byte ReadByte()
        {
            return dataBOut[c++];
        }

        public void DiscardInBuffer() {
            //x++;
            //y++;

            DoSetTimeTask();

            if (x > ASL.Reader.ASLWIDTH) x = 1;
            if (y > ASL.Reader.ASLHEIGHT) y = 1;
            int p = 40;
            dataBOut[1] = (byte)(p >> 8);
            dataBOut[2] = (byte)(p);
            dataBOut[4] = (byte)(x >> 8);
            dataBOut[5] = (byte)(x);
            dataBOut[6] = (byte)(y >> 8);
            dataBOut[7] = (byte)(y);
            c = 0;
        }

        public void DoSetGC()
        {
            t++;
            if (t > 10000) t = 0;
            if (t % 10000 < 1000)
            {
                Debug.WriteLineIf(t % 10 == 0, "SACCADE");
                x++;
                y++;
            }
            else
            {
                Debug.WriteLineIf(t % 10 == 0, "FIX");
                x += (int)Math.Round( 5-rnd.NextDouble() * 10 );
                y += (int)Math.Round(5 - rnd.NextDouble() * 10);
            }
        }

        public void DoSetTimeTask()
        {
            int T = 1400 + 500;
            int startX = (int)(200.0 / 1024.0 * ASL.Reader.ASLWIDTH);
            int startY = (int)(600.0 / 768.0 * ASL.Reader.ASLHEIGHT);
            int targetX = (int)(800.0 / 1024.0 * ASL.Reader.ASLWIDTH);
            int targetY = (int)(600.0 / 768.0 * ASL.Reader.ASLHEIGHT);
            timer.Stop();
            timerGlobal.Stop();

            if ((timer.GetDuration() > 1100 + 500) && (timer.GetDuration() < 1300 + 500))
            {
                //Debug.WriteLine(timerGlobal.Duration);
                x = targetX;
                y = targetY;
            }
            else if (timer.GetDuration() >= T)
            {
                timer.Start();
            } else {
                x = startX;
                y = startY;
            }
        }

        public int BytesToRead {
            get
            {
                return dataBOut.Length - c;
            }
        }

        public string PortName {get{return "";} set{}}
        public int BaudRate {get{return 0;} set{}}
        public int ReadBufferSize {get{return 0;} set{}}
        public bool DtrEnable {get{return false;} set{}}
        public bool RtsEnable {get{return false;} set{}}
        public int ReadTimeout {get{return 0;} set{}}
        public int WriteTimeout {get{return 0;} set{}}
    }
}
