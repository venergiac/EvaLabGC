using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EVALab.Analysis
{
    [Serializable()]
    public class DataObject
    {
        [Serializable()]
        public enum DataType
        {
            Pixels,
            Degree,
            Tick,
            Unknown,
            Analysis,
            VelocityDegreePerSec,
            VelocityTickPerSec,
            VelocityPixelsPerSec
        }

        private string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private DataType type = DataType.Pixels;

        public DataType Type
        {
            get { return type; }
            set { type = value; }
        }

    }
}
