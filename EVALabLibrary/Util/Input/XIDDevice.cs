using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace EVALab.Util.Input
{
    /// <summary>
    /// Interface for eXperimental Interface Devices. License: LGPL. Author: B. Vaessen, Launch IT, www.launchit.nl, 2010
    /// </summary>
    public class XIDDevice
    {
        private SerialPort _SerialPort;

        public bool Connected { get; internal set; }

        public event EventHandler DataReceived;

        public List<XIDEvent> Events;


        // Device Properties
        public string ProductName;
        public string ProductID;
        public string ModelID;
        public string MajorFirmwareRevision;
        public string MinorFirmwareRevision;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address"></param>
        /// <param name="baudrate"></param>
        public XIDDevice(string address, int baudrate)
        {

            ProductName = "XID";
            ProductID = "XID";
            ModelID = "XID";
            MajorFirmwareRevision = "0";
            MinorFirmwareRevision = "0";

            try
            {

                Events = new List<XIDEvent>();

                _SerialPort = new SerialPort(address, baudrate);

                _SerialPort.ReadTimeout = 1500;

                _SerialPort.DataReceived += new SerialDataReceivedEventHandler(_SerialPort_DataReceived);

                Connected = false;
            }
            catch
            {
                Connected = false;
            }
        }

        ~XIDDevice()
        {
            Close();
        }

        public void Open()
        {

            try
            {
                if (Connected)
                {
                    Close();
                }
            }
            catch (Exception)
            {

            }
            try
            {

                _SerialPort.Open();

                Connected = true;
            }
            catch
            {
                Connected = false;
            }
        }

        public void Close()
        {
            try
            {
                _SerialPort.Close();
                Connected = false;
            }
            catch
            { }
        }

        /// <summary>
        /// Data received event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Key pressed?

            int stx = GetByte();

            //if (stx == 107) //'k'
            {
                try
                {
                    int portnumber = 0;
                    int actionflag = 0;
                    int buttoncode = 0;

                    int keyinfo = GetByte();
                    if (keyinfo >= 0)
                    {
                        portnumber = (keyinfo & 0x0F);
                        actionflag = (keyinfo & 0x10) >> 4;
                        buttoncode = (keyinfo & 0xE0) >> 5;
                    }

                    int timervalue = 0;
                    for (int idx = 0; idx < 4; idx++)
                    {
                        // Get the timer
                        int val = GetByte();
                        if (val >= 0)
                        {
                            timervalue += val << (idx * 8);
                        }
                    }

                    XIDEvent Event;
                    if (actionflag == 1)
                    {
                        Event = new XIDEvent(enumXIDEvent.ButtonPressed, buttoncode, timervalue);
                    }
                    else
                    {
                        Event = new XIDEvent(enumXIDEvent.ButtonReleased, buttoncode, timervalue);
                    }

                    Events.Add(Event);

                }
                catch
                {
                }

            }


            if (DataReceived != null)
            {
                DataReceived(sender, e);
            }

        }

        /// <summary>
        /// Gets the currently available data
        /// </summary>
        /// <returns></returns>
        public string GetDataAsString()
        {
            if (_SerialPort.BytesToRead > 0)
            {
                int buffersize = 4096;

                char[] msg = new char[buffersize];
                for (int x = 0; x < buffersize; x++)
                {
                    msg[x] = '\0';
                }

                int idx = 0;
                bool done = false;

                while ((idx < buffersize) && (done == false))
                {
                    try
                    {
                        msg[idx] = (char)_SerialPort.ReadChar();
                        idx++;
                    }
                    catch
                    {
                        done = true;
                    }
                }

                return new string(msg);
            }

            return "";
        }

        /// <summary>
        /// Gets the data as byte
        /// </summary>
        /// <returns></returns>
        public int GetByte()
        {
            int retval = -1;

            try
            {
                retval = _SerialPort.ReadByte();
            }
            catch
            {
                retval = -1;
            }

            return retval;
        }
    }
}
