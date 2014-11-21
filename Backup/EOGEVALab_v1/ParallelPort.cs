using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EvaLab.EOG
{
    public class ParallelPort
    {
        const int LP_PIN01 = 0x100;
        const int LP_PIN02 = 0x1;
        const int LP_PIN03 = 0x2;
        const int LP_PIN04 = 0x4;
        const int LP_PIN05 = 0x8;
        const int LP_PIN06 = 0x10;
        const int LP_PIN07 = 0x20;
        const int LP_PIN08 = 0x40;
        const int LP_PIN09 = 0x80;
        const int LP_PIN10 = 0x8000;
        const int LP_PIN11 = 0x10000;
        const int LP_PIN12 = 0x4000;
        const int LP_PIN13 = 0x2000;
        const int LP_PIN14 = 0x200;
        const int LP_PIN15 = 0x1000;
        const int LP_PIN16 = 0x400;
        const int LP_PIN17 = 0x800;

        const int LP_CLEAR = 0; // for change_pin 
        const int LP_SET = 1; // for change_pin 

        // pins that can act as input 
        const int LP_INPUT_PINS = (LP_PIN10 | LP_PIN11 | LP_PIN12 | LP_PIN13 | LP_PIN15);

        // pins that can act as output 
        const int LP_OUTPUT_PINS = (LP_PIN01 | LP_PIN02 | LP_PIN03 | LP_PIN04 | LP_PIN05 | LP_PIN06 | LP_PIN07 | LP_PIN08 | LP_PIN09 | LP_PIN14 | LP_PIN16 | LP_PIN17);


        const int data_reg = 0x0;
        const int status_reg = 0x1;
        const int control_reg = 0x2;

        // The state of some pins is inverted relative to the state of their
        // corresponding register bits.  These masks correct the bits that
        // need to be corrected.

        const int positive_mask_control = 0xB; // 1011
        const int positive_mask_status = 0x10; // 10000

        const int negative_mask_control = positive_mask_control ^ 0x1FFFF; // 11111111111110100

        public static int MAX_VALUE= 207;
        public static int MIN_VALUE = -207;

        private bool isLedBar = false;
        public bool LedBar
        {
            get
            {
                return isLedBar;
            }
            set
            {
                isLedBar = value;
            }
        }

        private int baseAddress = 0;
        public int BaseAddress
        {
            get
            {
                return baseAddress;
            }
            set
            {
                baseAddress = value;
            }
        }

        public void Set(int value)
        {
            Reset();
            Output(baseAddress, value);
        }

        public void Show(int value)
        {
            if (isLedBar) ShowLedBar(value);
            else ShowLed(value);
        }

        public void ShowLedBar(int value)
        {
            Reset();
            SetDirection(value);

            //reallign directio
            if ((value > MAX_VALUE) || (value < -MAX_VALUE)) return;
            value = MAX_VALUE - Math.Abs(value);
            int largevalue = value >> 4;
            int smallvalue = (value & 0x0F);
            smallvalue = (smallvalue & 0x01) << 3 | (smallvalue & 0x02) << 1 | (smallvalue & 0x04) >> 1 | (smallvalue & 0x08) >> 3;

            int finalvalue = ((smallvalue << 4) & 0xF0) | (largevalue & 0x0F);
            
            set_pin(finalvalue);
        }

        public void ShowLed(int value)
        {
            Reset();
            SetDirection(value);
            if ((value > MAX_VALUE) || (value < -MAX_VALUE)) return;
            set_pin(value);
        }

        /// <summary>
        /// Set the side (Sx or Dx
        /// </summary>
        /// <param name="value"></param>
        private void SetDirection(int value)
        {
            if (value > 0)
            {
                set_pin(LP_PIN14);
                clear_pin(LP_PIN17);
            }
            else
            {
                set_pin(LP_PIN17);
                clear_pin(LP_PIN14);
            }
        }

        public void Reset()
        {
            Output(baseAddress, 0);
            clear_pin(LP_PIN14);
            clear_pin(LP_PIN17);
        }

        //public void set_pin(int pin){

        //    // make sure the user is only trying to set an output pin 
        //    pin &= LP_OUTPUT_PINS;
        //    Output(baseAddress, pin);
        //}

        public void set_pin(int pins)
        {

            int new_reg;
            int port_data = baseAddress + data_reg;
            int port_control = baseAddress + control_reg;
            int port_status = baseAddress + status_reg;

            // make sure the user is only trying to set an output pin
            pins &= LP_OUTPUT_PINS;

            new_reg = Input(port_data);

            new_reg |= pins & 0xFF;

            Output(port_data, new_reg);

            new_reg = Input(port_control);

            pins = (pins >> 8) & 0xF;

            new_reg |= pins;

            new_reg &= (pins & positive_mask_control) ^ 0x1FFFF;

            Output(port_control, new_reg);
        }

        public void clear_pin(int pins)
        {

            int new_reg;
            int port_data = baseAddress + data_reg;
            int port_control = baseAddress + control_reg;
            int port_status = baseAddress + status_reg;

            // make sure the user is only trying to set an output pin 
            pins &= LP_OUTPUT_PINS;

            new_reg = Input(port_data);

            new_reg = new_reg & ((pins & 0xFF) ^ 0xFF);

            Output(port_data, new_reg);

            new_reg = Input(port_control);

            pins = (pins >> 8) & 0xF;

            new_reg |= pins & positive_mask_control;

            new_reg &= (pins & negative_mask_control) ^ 0x1FFFF;

            Output(port_control, new_reg);

        }

        //Call Input functionfrom DLL file
        [DllImport("inpout32.dll", EntryPoint = "Out32")]
        //[DllImport("inpoutx64.dll", EntryPoint = "Out32")]
        public static extern void Output(int adress, int value);

        //Call Input functionfrom DLL file
        [DllImport("inpout32.dll", EntryPoint = "Inp32")]
        //[DllImport("inpoutx64.dll", EntryPoint = "Inp32")]
        public static extern int Input(int adress);
    }
}
