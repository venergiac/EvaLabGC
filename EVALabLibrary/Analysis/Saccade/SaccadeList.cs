using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.WaveForm;

namespace EVALab.Analysis.Saccade
{
    [Serializable()]
    public class SaccadeList : DataObject
    {
        public SaccadeList(string name)
        {
            this.Name = name;
        }
        private List<Saccade> list = new List<Saccade>();

        public List<Saccade> List
        {
            get { return list; }
            set { list = value; }
        }

        public Object Clone()
        {
            SaccadeList obj = new SaccadeList(Name);
            foreach (Saccade it in list)
            {
                obj.List.Add((Saccade)it.Clone());
            }
            return obj;
        }
    }

    [Serializable()]
    public class Saccade : WaveForm.WaveForm
    {

        public Saccade(int idxStart, int idxEnd)
            : base(idxStart, idxEnd, WaveForm.WaveFormType.Saccade)
        {
        }

        public Saccade(int idxStart, int idxEnd, WaveFormType type, double vMean, double vPeak, double amplitude, double duration, double latency, double error, double gain, double area, double nCrox, double maxCurvature, double overallAngle, bool valid) :
            base(idxStart, idxEnd, type, vMean, vPeak, amplitude, duration, latency, error, gain, area, nCrox, maxCurvature, overallAngle, valid)
        {
        }

        public Saccade(int idxStart, int idxEnd, WaveFormType type, double vMean, double vPeak, double amplitude, double duration, double latency, double error, double gain, double area, double nCrox, double maxCurvature, double overallAngle, ROI.ROI roiStart, ROI.ROI roiEnd, bool valid) :
            base(idxStart, idxEnd, type, vMean, vPeak, amplitude, duration, latency, error, gain, area, nCrox, maxCurvature, overallAngle, roiStart, roiEnd, valid)
        {
        }

        public override Object Clone()
        {
            return new Saccade(idxStart, idxEnd, type, vMean, vPeak, amplitude, duration, latency, error, gain, area, maxCurvature, nCrox, overallAngle, roiStart, roiEnd, valid);
        }
    }
}
