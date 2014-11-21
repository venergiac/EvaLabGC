using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;
using System.Diagnostics;

namespace EVALab.Analysis.WaveForm
{
    public class WaveFormManager
    {
        public static WaveForm MakeWaveForm(DataList data, int idxStart, int idxEnd, WaveFormType type, int onlyOnDataPosition)
        {
            WaveForm wf = new WaveForm(idxStart, idxEnd, type);
            return MakeWaveForm(data, ref wf, onlyOnDataPosition);
        }

        protected static WaveForm MakeWaveForm(DataList data, ref WaveForm saccade, int onlyOnDataPosition)
        {
            
            saccade.Duration = data.List[saccade.IdxEnd].Time - data.List[saccade.IdxStart].Time;
            int startIdx = saccade.IdxStart + 1;
            DataList velocity = data.Velocity(onlyOnDataPosition);

            Debug.WriteLine("Skipped " + saccade.IdxStart + "-" + saccade.IdxEnd + " versus " + data.List.Count);
            if (startIdx >= data.List.Count) return null;

            //look for velocity
            saccade.VPeak = 0;
            saccade.VMean = 0;
            for (int idx = saccade.IdxStart; idx <= saccade.IdxEnd; idx++)
            {
                saccade.VPeak = Math.Max(saccade.VPeak, velocity.List[idx].Value[velocity.List[idx].Value.Length - 1]);
                saccade.VMean += velocity.List[idx].Value[velocity.List[idx].Value.Length - 1];
            }
            saccade.VMean /= (saccade.IdxEnd - saccade.IdxStart);

            //look for amplitude
            if (onlyOnDataPosition >= 0)
            {
                saccade.Amplitude = data.List[saccade.IdxEnd].Value[onlyOnDataPosition] - data.List[saccade.IdxStart].Value[onlyOnDataPosition];
            }
            else
            {
                saccade.Amplitude = (data.List[saccade.IdxEnd].Value[Item.POSITIONX] + data.List[saccade.IdxEnd].Value[Item.POSITIONY]) / 2.0 - (data.List[saccade.IdxStart].Value[Item.POSITIONX] + data.List[saccade.IdxStart].Value[Item.POSITIONY]) / 2.0;
            }
            
            //look for latency
            int idxReference = 0;
            double lastReferenceX = data.List[saccade.IdxEnd].Reference[Item.POSITIONX];
            double lastReferenceY = data.List[saccade.IdxEnd].Reference[Item.POSITIONY];
            //first look back
            for (int idx = saccade.IdxEnd; idx >= 0; idx--)
            {
                if ((data.List[idx].Reference[Item.POSITIONX] != lastReferenceX) || (data.List[idx].Reference[Item.POSITIONY] != lastReferenceY))
                {
                    Debug.WriteLine(idx + " saccadeIdxEnd=" + saccade.IdxEnd + " saccadeIdxStart=" + saccade.IdxStart + " lastReferenceX=" + lastReferenceX + " data[idx]=" + data.List[idx].Value[Item.POSITIONX]);
                    idxReference = idx;
                    break;
                }
            }
            //second look forward
            lastReferenceX = data.List[saccade.IdxStart].Reference[Item.POSITIONX];
            lastReferenceY = data.List[saccade.IdxStart].Reference[Item.POSITIONY];
            for (int idx = saccade.IdxStart; idx < data.List.Count; idx++)
            {
                //break if currente difference is > than previous
                if (idx - saccade.IdxStart > saccade.IdxEnd - idxReference)
                {
                    break;
                }
                if ((data.List[idx].Reference[Item.POSITIONX] != lastReferenceX) || (data.List[idx].Reference[Item.POSITIONY] != lastReferenceY))
                {
                    Debug.WriteLine(idx + " saccadeIdxEnd=" + saccade.IdxEnd + " saccadeIdxStart=" + saccade.IdxStart + " lastReferenceX=" + lastReferenceX + " data[idx]=" + data.List[idx].Value[Item.POSITIONX]);
                    idxReference = idx;
                    break;
                }
            }
            saccade.Latency = data.List[saccade.IdxStart].Time - data.List[idxReference].Time;

            double amplitudeRefreence = 0;
            if (onlyOnDataPosition >= 0)
            {
                amplitudeRefreence =  data.List[idxReference].Value[onlyOnDataPosition];
            }
            else
            {
                amplitudeRefreence = (data.List[idxReference].Reference[Item.POSITIONX] + data.List[idxReference].Reference[Item.POSITIONY]) / 2.0;
            }
            
            //look for error
            saccade.Error = Math.Abs(saccade.Amplitude - amplitudeRefreence);

            //gain
            saccade.Gain = saccade.Amplitude / amplitudeRefreence;

            //trajectory parameters
            DoTrajectory(data, ref saccade, idxReference);


            return saccade;
        }

        protected static void DoTrajectory(DataList data, ref WaveForm saccade, int idxReference)
        {
            double xStart = data.List[saccade.IdxStart].Value[Item.POSITIONX];
            double yStart = data.List[saccade.IdxStart].Value[Item.POSITIONY];
            double xEnd = data.List[saccade.IdxEnd].Value[Item.POSITIONX];
            double yEnd = data.List[saccade.IdxEnd].Value[Item.POSITIONY];
            double xTarget = data.List[idxReference].Reference[Item.POSITIONX];
            double yTarget = data.List[idxReference].Reference[Item.POSITIONY];

            int prevIdx = saccade.IdxStart;
            for (int idx = saccade.IdxStart + 1; idx < saccade.IdxEnd; idx++)
            {
                double x1 = data.List[idx].Value[Item.POSITIONX];
                double y1 = data.List[idx].Value[Item.POSITIONY];
                double x2 = Rect(idx, saccade.IdxStart, xStart, saccade.IdxEnd, xEnd);
                double y2 = Rect(idx, saccade.IdxStart, yStart, saccade.IdxEnd, yEnd);
                double px1 = data.List[prevIdx].Value[Item.POSITIONX];
                double py1 = data.List[prevIdx].Value[Item.POSITIONY];
                double px2 = Rect(prevIdx, saccade.IdxStart, xStart, saccade.IdxEnd, xEnd);
                double py2 = Rect(prevIdx, saccade.IdxStart, yStart, saccade.IdxEnd, yEnd);
                saccade.Area += 100.0*Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)) * Math.Sqrt(Math.Pow(x2 - px2, 2) + Math.Pow(y2 - py2, 2)) / (saccade.IdxEnd - saccade.IdxStart);
                saccade.MaxCurvature = Math.Max(saccade.MaxCurvature, Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
                if ((Math.Sign(px1-px2) != Math.Sign(x1-x2)) || (Math.Sign(py1-py2) != Math.Sign(y1-y2))) {
                    saccade.NumberCrox++;
                }
            }
            saccade.OverallAngle = 100.0*Math.Abs((Angle(xEnd, yEnd, xStart, yStart) - Angle(xTarget, yTarget, xStart, yStart)) / Angle(xTarget, yTarget, xStart, yStart));
        }

        protected static double Rect(double t, double tStart, double xStart, double tEnd, double xEnd)
        {
            return (t - tStart) * (xEnd - xStart) / (tEnd - tStart) + xStart;
        }

        protected static double Angle(double x1, double y1, double x2, double y2)
        {
            return Math.Atan((y2 - y1) / (x2 - x1));
        }
    }
}
