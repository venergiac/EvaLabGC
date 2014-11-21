using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;
using EVALab.Analysis.Indicator;

namespace EVALab.Analysis.Util
{
    public class TaskManager
    {


        public static IndicatorList ReferenceDifference(DataList data)
        {
            double meanDifferenceX = 0;
            double meanDifferenceY = 0;
            foreach(Item item in data.List) {
                meanDifferenceX += Math.Abs(item.Value[Item.POSITIONX] - item.Reference[Item.POSITIONX]) / (double)data.List.Count;
                meanDifferenceY += Math.Abs(item.Value[Item.POSITIONY] - item.Reference[Item.POSITIONY]) / (double)data.List.Count;
            }
            IndicatorList list = new IndicatorList(data.Name);
            list.List.Add(new Indicator.Indicator("Target", "Distance X", meanDifferenceX,0,"px",0,data.List.Count));
            list.List.Add(new Indicator.Indicator("Target", "Distance Y", meanDifferenceY, 0, "px", 0, data.List.Count));
            list.List.Add(new Indicator.Indicator("Target", "Distance mean", (meanDifferenceX + meanDifferenceY)/2.0, 0, "px", 0, data.List.Count));

            return list;
        }

        public static List<DataList> SplitByTrial(DataList data)
        {
            List<DataList> lists = new List<DataList>();

            double[] lastReference = null;
            int idxStart = 0;
            int idxEnd = 0;
            int i = 0;
            int t = 1;
            foreach (Item item in data.List)
            {
                if ((lastReference == null) || !(ReferenceEquals(item.Reference, lastReference)))
                {
                    lastReference = item.Reference;
                    idxEnd = i;
                    DataList splittedList = Split("Trial #" + t + " of " + data.Name, data, idxStart, idxEnd);
                    lists.Add(splittedList);
                    idxStart = i;
                    t++;
                }
                i++;
            }

            return lists;
        }

        private static bool ReferenceEquals(double[] d1, double[] d2)
        {
            for (int i = 0; i < d1.Length && i < d2.Length; i++ )
            {
                if (d1[i] != d2[i]) return false;
            }
            return true;
        }

        private static DataList Split(string name, DataList data, int idxStart, int idxEnd)
        {

            DataList list = new DataList(name);
            for (int i = idxStart; i < idxEnd; i++)
            {
                list.List.Add(data.List[i]);
            }

            Saccade.SaccadeList sList = new Saccade.SaccadeList("saccade of " + name);
            foreach (Saccade.Saccade s in data.Saccades.List)
            {
                if ((s.IdxStart >= idxStart) && (s.IdxStart <= idxEnd) || (s.IdxEnd >= idxStart) && (s.IdxEnd <= idxEnd))
                {
                    Saccade.Saccade sc = (Saccade.Saccade)s.Clone();
                    sc.IdxStart -= idxStart;
                    if (sc.IdxStart<0) sc.IdxStart = 0;
                    sc.IdxEnd -= idxStart;
                    if (sc.IdxEnd < 0) sc.IdxEnd = 0;
                    sList.List.Add(sc);
                }
            }
            list.Saccades = sList;

            Fixation.FixationList fList = new Fixation.FixationList("fixation of " + name);
            foreach (Fixation.Fixation s in data.Fixations.List)
            {
                if ((s.IdxStart >= idxStart) && (s.IdxStart <= idxEnd) || (s.IdxEnd >= idxStart) && (s.IdxEnd <= idxEnd))
                {
                    Fixation.Fixation sc = (Fixation.Fixation)s.Clone();
                    sc.IdxStart -= idxStart;
                    if (sc.IdxStart < 0) sc.IdxStart = 0;
                    sc.IdxEnd -= idxStart;
                    if (sc.IdxEnd < 0) sc.IdxEnd = 0;
                    fList.List.Add(sc);
                }
            }
            list.Fixations = fList;

            WaveForm.WaveFormList wList = new WaveForm.WaveFormList("waveform of " + name);
            foreach (WaveForm.WaveForm s in data.WaveForms.List)
            {
                if ((s.IdxStart >= idxStart) && (s.IdxStart <= idxEnd) || (s.IdxEnd >= idxStart) && (s.IdxEnd <= idxEnd))
                {
                    WaveForm.WaveForm sc = (WaveForm.WaveForm)s.Clone();
                    sc.IdxStart -= idxStart;
                    if (sc.IdxStart < 0) sc.IdxStart = 0;
                    sc.IdxEnd -= idxStart;
                    if (sc.IdxEnd < 0) sc.IdxEnd = 0;
                    wList.List.Add(sc);
                }
            }
            list.WaveForms = wList;

            return list;
        }
    }
}
