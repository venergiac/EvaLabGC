using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.WaveForm
{
    [Serializable()]
    public class WaveFormList : DataObject
    {
        public WaveFormList(string name)
        {
            this.Name = name;
        }
        private List<WaveForm> list = new List<WaveForm>();

        public List<WaveForm> List
        {
            get { return list; }
            set { list = value; }
        }

        public Object Clone()
        {
            WaveFormList obj = new WaveFormList(Name);
            foreach (WaveForm it in list)
            {
                obj.List.Add((WaveForm)it.Clone());
            }
            return obj;
        }

    }

    [Serializable()]
    public enum WaveFormType
    {
        Saccade,
        Nystagmus,
        MicroSaccade,
        Blink,
        SquareWave,
        TriangularWave,
        BadData,
        None
    }

    [Serializable()]
    public class WaveForm
    {
        protected WaveFormType type = WaveFormType.Saccade;

        public WaveForm(int idxStart, int idxEnd, WaveFormType type)
        {
            this.idxEnd = idxEnd;
            this.idxStart = idxStart;
            this.type = type;
        }

        public WaveForm(int idxStart, int idxEnd, WaveFormType type, double vMean, double vPeak, double amplitude, double duration, double latency, double error, double gain, double area, double nCrox, double maxCurvature, double overallAngle,bool valid)
        {
            this.idxEnd = idxEnd;
            this.idxStart = idxStart;
            this.type = type;
            this.valid = valid;
            this.vMean = vMean;
            this.vPeak = vPeak;
            this.gain = gain;
            this.error = error;
            this.duration = duration;
            this.latency = latency;
            this.amplitude = amplitude;
            this.area = area;
            this.maxCurvature = maxCurvature;
            this.nCrox = nCrox;
            this.overallAngle = overallAngle;
        }

        public WaveForm(int idxStart, int idxEnd, WaveFormType type, double vMean, double vPeak, double amplitude, double duration, double latency, double error, double gain, double area, double nCrox, double maxCurvature, double overallAngle, ROI.ROI roiStart, ROI.ROI roiEnd, bool valid)
        {
            this.idxEnd = idxEnd;
            this.idxStart = idxStart;
            this.type = type;
            this.valid = valid;
            this.vMean = vMean;
            this.vPeak = vPeak;
            this.gain = gain;
            this.error = error;
            this.duration = duration;
            this.latency = latency;
            this.amplitude = amplitude;
            this.area = area;
            this.maxCurvature = maxCurvature;
            this.nCrox = nCrox;
            this.overallAngle = overallAngle;
            this.roiStart = roiStart;
            this.roiEnd = roiEnd;
        }


        public WaveFormType Type
        {
            get { return type; }
            set { type = value; }
        }

        protected bool valid = true;

        public bool Valid
        {
            get { return valid; }
            set { valid = value; }
        }

        protected int idxStart = 0;

        public int IdxStart
        {
            get { return idxStart; }
            set { idxStart = value; }
        }
        protected int idxEnd = 0;

        public int IdxEnd
        {
            get { return idxEnd; }
            set { idxEnd = value; }
        }

        protected ROI.ROI roiStart = null;
        public ROI.ROI ROIStart
        {
            get { return roiStart; }
            set { roiStart = value; }
        }
        protected ROI.ROI roiEnd = null;
        public ROI.ROI ROIEnd
        {
            get { return roiEnd; }
            set { roiEnd = value; }
        }

        protected double vMean = 0;
        public double VMean
        {
            get { return vMean; }
            set { vMean = value; }
        }
        protected double vPeak = 0;

        public double VPeak
        {
            get { return vPeak; }
            set { vPeak = value; }
        }
        protected double amplitude = 0;

        public double Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }
        }
        protected double duration = 0;

        public double Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        protected double latency = 0;

        public double Latency
        {
            get { return latency; }
            set { latency = value; }
        }
        protected double error = 0;

        public double Error
        {
            get { return error; }
            set { error = value; }
        }
        protected double gain = 0;

        public double Gain
        {
            get { return gain; }
            set { gain = value; }
        }

        protected double area = 0;

        public double Area
        {
            get { return area; }
            set { area = value; }
        }

        protected double maxCurvature = 0;

        public double MaxCurvature
        {
            get { return maxCurvature; }
            set { maxCurvature = value; }
        }

        protected double nCrox= 0;

        public double NumberCrox
        {
            get { return nCrox; }
            set { nCrox = value; }
        }

        protected double overallAngle = 0;

        public double OverallAngle
        {
            get { return overallAngle; }
            set { overallAngle = value; }
        }

        public virtual Object Clone()
        {
            return new WaveForm(idxStart, idxEnd, type, vMean, vPeak, amplitude, duration, latency, error, gain, area, maxCurvature, nCrox, overallAngle, roiStart, roiEnd, valid);
        }
    }
}
