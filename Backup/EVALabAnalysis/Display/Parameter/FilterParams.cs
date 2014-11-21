using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALabAnalysis.Parameter
{
    public class FilterParams : DataParams
    {
        protected FilterParams()
        {
        }

        protected static new FilterParams singletone = new FilterParams();
        public static new FilterParams Instance()
        {
            return singletone;
        }

        private int order = 3;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        private int cutOff = 50;

        public int CutOff
        {
            get { return cutOff; }
            set { cutOff = value; }
        }
    }
}
