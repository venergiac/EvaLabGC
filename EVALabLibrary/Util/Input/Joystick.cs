/******************************************************************************
 * C# Joystick Library - Copyright (c) 2006 Mark Harris - MarkH@rris.com.au
 ******************************************************************************
 * You may use this library in your application, however please do give credit
 * to me for writing it and supplying it. If you modify this library you must
 * leave this notice at the top of this file. I'd love to see any changes you
 * do make, so please email them to me :)
 *****************************************************************************/
//#define DIRECTX
//#define XID
#define NUCLEX
using System;
using System.Collections.Generic;
using System.Text;
#if DIRECTX
using Microsoft.DirectX.DirectInput;
#else
    #if NUCLEX
    using Nuclex.Input;
    using Nuclex.Input.Devices;
    #endif

#endif
using System.Diagnostics;

using EVALab.Util.Xna.Object;
using System.Windows.Forms;

namespace EVALab.Util.Input
{

    /// <summary>
    /// Class to interface with a joystick device.
    /// </summary>
    public class Joystick
    {
#if DIRECTX
        private Device joystickDevice;
        private JoystickState state;
#else
#if NUCLEX

        InputManager inputManager = null;
        IGamePad gamePad = null;
        ExtendedGamePadState state;
#endif
#endif

#if XID
        const string XID_DEVICE_NAME = "Generic XID Device";
        XIDDevice xidDevice;

        public XIDDevice XidDevice
        {
            get { return xidDevice; }
            set { xidDevice = value; }
        }

#endif
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private int axisCount = 0;
        /// <summary>
        /// Number of axes on the joystick.
        /// </summary>
        public int AxisCount
        {
            get { return axisCount; }
        }

        private int axisA = 0;
        /// <summary>
        /// The first axis on the joystick.
        /// </summary>
        public int AxisA
        {
            get { return axisA; }
        }

        private int axisB = 0;
        /// <summary>
        /// The second axis on the joystick.
        /// </summary>
        public int AxisB
        {
            get { return axisB; }
        }

        private int axisC = 0;
        /// <summary>
        /// The third axis on the joystick.
        /// </summary>
        public int AxisC
        {
            get { return axisC; }
        }

        private int axisD = 0;
        /// <summary>
        /// The fourth axis on the joystick.
        /// </summary>
        public int AxisD
        {
            get { return axisD; }
        }

        private int axisE = 0;
        /// <summary>
        /// The fifth axis on the joystick.
        /// </summary>
        public int AxisE
        {
            get { return axisE; }
        }

        private int axisF = 0;
        /// <summary>
        /// The sixth axis on the joystick.
        /// </summary>
        public int AxisF
        {
            get { return axisF; }
        }
        private IntPtr hWnd;

        private bool[] buttons = null;
        /// <summary>
        /// Array of buttons availiable on the joystick. This also includes PoV hats.
        /// </summary>
        public bool[] Buttons
        {
            get { return buttons; }
        }

#if XID
        private int xidButton;
        /// <summary>
        /// Array of buttons availiable on the joystick. This also includes PoV hats.
        /// </summary>
        public int XidButton
        {
            get { return xidButton; }
        }
#endif
        private string[] systemJoysticks;



        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="window_handle">Handle of the window which the joystick will be "attached" to.</param>
        public Joystick(IntPtr window_handle, string xidPort, int xidBaud)
        {
#if DIRECTX
            hWnd = window_handle;
            axisA = -1;
            axisB = -1;
            axisC = -1;
            axisD = -1;
            axisE = -1;
            axisF = -1;
            axisCount = 0;
        }
#else
            #if NUCLEX
            inputManager = new InputManager(window_handle);
            #endif

#if XID
            if (xidPort != null)
            {
                xidDevice = new XIDDevice(xidPort, xidBaud);
                xidDevice.Open();
                if (xidDevice.Connected)
                {
                    xidDevice.ProductID=XID_DEVICE_NAME;
                    xidDevice.Close();
                }
                else
                {
                    xidDevice = null;
                }
            }
#endif
        }
#endif

        private void Poll()
        {
#if DIRECTX

            try
            {
                // poll the joystick
                joystickDevice.Poll();
                // update the joystick state field
                state = joystickDevice.CurrentJoystickState;
            }
            catch (Exception err)
            {
                // we probably lost connection to the joystick
                // was it unplugged or locked by another application?
                Debug.WriteLine("Poll()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
            }
#else
#if NUCLEX
            if (gamePad != null)
            {
                gamePad.Update();
            }
#endif
#endif
        }

        /// <summary>
        /// Retrieves a list of joysticks attached to the computer.
        /// </summary>
        /// <example>
        /// [C#]
        /// <code>
        /// JoystickInterface.Joystick jst = new JoystickInterface.Joystick(this.Handle);
        /// string[] sticks = jst.FindJoysticks();
        /// </code>
        /// </example>
        /// <returns>A list of joysticks as an array of strings.</returns>
        public string[] FindJoysticks()
        {
#if DIRECTX
            systemJoysticks = null;

            try
            {
                // Find all the GameControl devices that are attached.
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);

                // check that we have at least one device.
                if (gameControllerList.Count > 0)
                {
                    systemJoysticks = new string[gameControllerList.Count];
                    int i = 0;
                    // loop through the devices.
                    foreach (DeviceInstance deviceInstance in gameControllerList)
                    {
                        // create a device from this controller so we can retrieve info.
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        joystickDevice.SetCooperativeLevel(hWnd,
                            CooperativeLevelFlags.Background |
                            CooperativeLevelFlags.NonExclusive);

                        systemJoysticks[i] = joystickDevice.DeviceInformation.InstanceName;

                        i++;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("FindJoysticks()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
            }

            return systemJoysticks;
#else
            List<string> joys = new List<string>();
            #if XID
            if (xidDevice != null)
            {
                xidDevice.Open();
                if (xidDevice.Connected) { 
                    joys.Add(XID_DEVICE_NAME);
                }
                xidDevice.Close();
                return new String[] { XID_DEVICE_NAME };
            }
            return new String[] { };
#endif
#if NUCLEX
            foreach (IGamePad pad in this.inputManager.GamePads)
            {
                if (pad.IsAttached) joys.Add(pad.Name);
            }
            systemJoysticks = joys.ToArray();

            return systemJoysticks;
#endif
#endif
        }

        /// <summary>
        /// Acquire the named joystick. You can find this joystick through the <see cref="FindJoysticks"/> method.
        /// </summary>
        /// <param name="name">Name of the joystick.</param>
        /// <returns>The success of the connection.</returns>
        public bool AcquireJoystick(string name)
        {
            this.name = name;
#if XID

            if ((xidDevice!=null) && (XID_DEVICE_NAME.Equals(name)))
            {
                xidDevice.Open();
                xidDevice.DataReceived += new EventHandler(xidDevice_DataReceived);
                return xidDevice.Connected;
            }
            return false;
#endif
#if DIRECTX
            try
            {
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                int i = 0;
                bool found = false;
                // loop through the devices.
                foreach (DeviceInstance deviceInstance in gameControllerList)
                {
                    if (deviceInstance.InstanceName == name)
                    {
                        found = true;
                        // create a device from this controller so we can retrieve info.
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        joystickDevice.SetCooperativeLevel(hWnd,
                            CooperativeLevelFlags.Background |
                            CooperativeLevelFlags.NonExclusive);
                        break;
                    }

                    i++;
                }

                if (!found)
                    return false;

                // Tell DirectX that this is a Joystick.
                joystickDevice.SetDataFormat(DeviceDataFormat.Joystick);

                // Finally, acquire the device.
                joystickDevice.Acquire();

                // How many axes?
                // Find the capabilities of the joystick
                DeviceCaps cps = joystickDevice.Caps;
                Debug.WriteLine("Joystick Axis: " + cps.NumberAxes);
                Debug.WriteLine("Joystick Buttons: " + cps.NumberButtons);

                axisCount = cps.NumberAxes + 1;

                UpdateStatus();
            }
            catch (Exception err)
            {
                Debug.WriteLine("FindJoysticks()");
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                return false;
            }

            return true;
#else
#if NUCLEX
            gamePad = null;
            foreach (IGamePad pad in this.inputManager.GamePads)
            {
                if (pad.Name.Equals(name))
                {
                    gamePad = pad;
                    break;
                }
            }
            if (gamePad != null)
            {
                //gamePad.ButtonPressed += new GamePadButtonDelegate(gamePad_ButtonPressed);
                return gamePad.IsAttached;
            }
            else
            {
                return false;
            }
#endif
#endif
        }

#if XID

        void xidDevice_DataReceived(object sender, EventArgs e)
        {
            UpdateXidStatus();
        }
#endif
        /// <summary>
        /// Unaquire a joystick releasing it back to the system.
        /// </summary>
        public void ReleaseJoystick()
        {
#if DIRECTX
            joystickDevice.Unacquire();
#else
#if NUCLEX
            inputManager.Dispose();
#endif
#endif
#if XID
            if ((xidDevice!=null) && (xidDevice.Connected)) {
                xidDevice.Close();
                //xidDevice=null;
            }
#endif
        }

        /// <summary>
        /// Update the properties of button and axis positions.
        /// </summary>
        public void UpdateStatus()
        {
#if DIRECTX
            Poll();

            int[] extraAxis = state.GetSlider();
            //Rz Rx X Y Axis1 Axis2
            axisA = state.Rz;
            axisB = state.Rx;
            axisC = state.X;
            axisD = state.Y;
            axisE = extraAxis[0];
            axisF = extraAxis[1];

            // not using buttons, so don't take the tiny amount of time it takes to get/parse
            byte[] jsButtons = state.GetButtons();
            buttons = new bool[jsButtons.Length];

            int i = 0;
            foreach (byte button in jsButtons)
            {
                buttons[i] = button >= 128;
                i++;
            }
#else
#if NUCLEX
            if (gamePad != null)
            {
                this.Poll();
                state = gamePad.GetExtendedState();

                axisA = (int)Math.Round(state.RotationZ * 1000.0);
                axisB = (int)Math.Round(state.RotationX * 1000.0);
                axisC = (int)Math.Round(state.X * 1000.0);
                axisD = (int)Math.Round(state.Y * 1000.0);
                axisE = (int)Math.Round(state.Z * 1000.0);
                axisF = (int)Math.Round(state.RotationZ * 1000.0);
                Debug.WriteLine(state.X);
                axisCount = state.AxisCount;

                // not using buttons, so don't take the tiny amount of time it takes to get/parse
                buttons = new bool[state.ButtonCount];
                
                for (int i = 0; i < state.ButtonCount; i++)
                {
                    buttons[i] = state.IsButtonDown(i);
                }
            }
#endif
#endif

#if XID
            //update xi
            if (xidDevice != null)
            {
                xidButton = 0;
            }
#endif
        }

#if XID

        public void UpdateXidStatus()
        {
            if ((xidDevice != null) && (xidDevice.Events.Count > 0))
            {
                XIDEvent E = xidDevice.Events[0];
                string[] strItem = new string[3];
                if (E.xidEvent.Equals(enumXIDEvent.ButtonPressed))
                {
                    xidButton = E.code;
                }
                else
                {
                    xidButton = 0;
                }
                //xidDevice.Events.RemoveAt(0);
                xidDevice.Events.Clear();
            }
        }
#endif
        public int GetJoystickButtonsValue()
        {

#if XID
            if ((xidDevice != null) && (xidDevice.Connected))
            {
                return xidButton;
            }

#endif
            int value = 0;
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i])
                {
                    value |= 1 << i;
                }
            }
            return value;
        }

        public int GetJoystickXValue()
        {
            return -1 * AxisC;
        }

        public int GetJoystickYValue()
        {
            return -1 * AxisD;
        }
    }
}