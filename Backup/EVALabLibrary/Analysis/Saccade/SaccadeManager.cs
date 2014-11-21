using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;
using System.Diagnostics;
using EVALab.Analysis.Filter;

namespace EVALab.Analysis.Saccade
{
    public class SaccadeManager : WaveForm.WaveFormManager
    {

        public static Saccade MakeSaccade(DataList data, int idxStart, int idxEnd, int onlyOnDataPosition)
        {
            WaveForm.WaveForm wf = new Saccade(idxStart, idxEnd);
            return (Saccade)MakeWaveForm(data, ref wf, onlyOnDataPosition);
        }

        public SaccadeList DoSaccade(DataList data, 
            double saccadeVelThresh,
            double pctPeak,
            double saccadeWindow,
            double saccadeMinDuration,
            bool applyFilter,
            int onlyOnPosition)
        {
            //init array
            SaccadeList list = new SaccadeList("Saccades on " + data.Name);
            
            //Calcolo della velocità critica
            double vCrit = saccadeVelThresh;
            
            //min duration of ROI fixation (in samples)
            //double fLen = Math.Round(saccadeMinFixDuration / acqIntvl);
            FilterManager manager = new FilterManager();
            DataList velocity = null;

            if (applyFilter)
            {
                DataList dataFiltered = manager.Filter(data, 20, 5);
                velocity = dataFiltered.Velocity(onlyOnPosition);
            }
            else
            {
                velocity = data.Velocity(onlyOnPosition);
            }

            Debug.Assert(velocity.Type == DataList.DataType.VelocityDegreePerSec);

            int startIdx = 0;
            for (; ; )
            {

                int[] idxs = findSaccade(velocity, startIdx, vCrit, pctPeak, saccadeWindow, onlyOnPosition);

                if ((idxs[0] >= 0) && (idxs[1] >= 0))
                {
                    startIdx = idxs[1] + 1;
                    Debug.WriteLine("Skipped " + idxs[0] + "-" + idxs[1] + " versus " + data.List.Count);
                    if (startIdx >= data.List.Count) break;

                    Saccade saccade = MakeSaccade(data, idxs[0], idxs[1], onlyOnPosition);
                    if ((saccade != null) && (saccade.Duration >= saccadeMinDuration))
                    {
                        list.List.Add(saccade);
                    }
                }
                else
                {
                    break;
                }

            }


            return list;
        }

        private int[] findSaccade(DataList velocity, int startIdx, double vCrit, double pctPeak, double saccadeWindow, int onlyOnPosition)
        {
            //cerco dove la velocità  > vCrit
            int idxCrit = -1;
            for (int i=startIdx;i<velocity.List.Count; i++) {
                if (onlyOnPosition < 0) onlyOnPosition = velocity.List[i].Value.Length - 1;
                double v = Math.Abs(velocity.List[i].Value[onlyOnPosition]);
                Debug.WriteLine( i +"" +v + " " + vCrit);
                if (v >= vCrit)
                {
                    Debug.WriteLine("Found " + i);
                    idxCrit = i;
                    break;
                }
            }

            if (idxCrit < 0)
            {
                Debug.WriteLine("Exit1 " + idxCrit + "<0 versus " + velocity.List.Count);
                return new int[] { -1, -1 };
            }

            //trovo velocità di picco
            double vPeak = 0;
            int idxPeak = 0;
            long timeStart = velocity.List[idxCrit].Time;
            for (int i = idxCrit; i < velocity.List.Count; i++)
            {
                if (onlyOnPosition < 0) onlyOnPosition = velocity.List[i].Value.Length - 1;
                double v = velocity.List[i].Value[onlyOnPosition];
                if (Math.Abs(v) >= Math.Abs(vPeak))
                {
                    vPeak = v;
                    idxPeak = i;
                }
                if (velocity.List[i].Time - timeStart > saccadeWindow)
                {
                    Debug.WriteLine("Exit2 due to saccadeWindow at " +i +" starting from " + idxCrit + " " + idxPeak);
                    break;
                }
            }

            //calcolo gli indici avanti ed indietro rispetto
            double vCutOff = vPeak * pctPeak / 100.0;

            int idxStartSaccade = startIdx;
            for (int i = idxPeak; i >= startIdx; i--)
            {
                if (onlyOnPosition < 0) onlyOnPosition = velocity.List[i].Value.Length - 1;
                double v = velocity.List[i].Value[onlyOnPosition];
                if (Math.Abs(v) <= Math.Abs(vCutOff))
                {
                    idxStartSaccade = i;
                    Debug.WriteLine("Exit3 due to v <= vCutOff at " + i + " starting from " + idxPeak);
                    break;
                }
            }

            int idxEndSaccade = idxPeak;
            for (int i = idxPeak; i < velocity.List.Count; i++)
            {
                if (onlyOnPosition < 0) onlyOnPosition = velocity.List[i].Value.Length - 1;
                double v = velocity.List[i].Value[onlyOnPosition];
                if (Math.Abs(v) <= Math.Abs(vCutOff))
                {
                    idxEndSaccade = i;
                    Debug.WriteLine("Exit4 due to v <= vCutOff at " + i + " starting from " + idxPeak);
                    break;
                }
            }

            return new int[] { idxStartSaccade, idxEndSaccade };
        }

    }
}
