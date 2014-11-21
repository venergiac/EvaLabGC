using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.WaveForm;

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

        private WaveFormType waveForm = WaveFormType.None;

        public WaveFormType OnlyOnWaveForm
        {
            get { return waveForm; }
            set { waveForm = value; }
        }

        private bool scale = false;
        public bool Scale
        {
            get { return scale; }
            set { scale = value; }
        }
    }
}
