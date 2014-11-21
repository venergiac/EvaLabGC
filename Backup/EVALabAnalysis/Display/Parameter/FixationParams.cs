using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALabAnalysis.Parameter
{
    public class FixationParams : DataParams
    {

        protected static new FixationParams singletone = new FixationParams();
        public static new FixationParams Instance()
        {
            return singletone;
        }
        protected FixationParams() { }

        double dispersion = 2.5;

        public double FixationMaxDispersion
        {
            get { return dispersion; }
            set { dispersion = value; }
        }

        double duration = 50;

        public double FixationMinDuration
        {
            get { return duration; }
            set { duration = value; }
        }
    }
}
