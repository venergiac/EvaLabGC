using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALab.Util.Input
{
    public enum enumXIDEvent{ButtonReleased = 0, ButtonPressed = 1};

    public class XIDEvent
    {
        public int time {get; set;}
        public enumXIDEvent xidEvent {get; set;}
        public int code { get; set; }

        public XIDEvent(enumXIDEvent xidevent, int code, int time)
        {
            this.xidEvent = xidevent;
            this.code = code;
            this.time = time;
        }


        public string ToString()
        {
            return xidEvent.ToString() + "(" + code.ToString() + ", " + time.ToString() + ")";
        }

    }
}
