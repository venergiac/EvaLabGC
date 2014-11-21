using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EVALab.Analysis.Data;
using System.Diagnostics;
using EVALab.Analysis.Indicator;

namespace EVALab.Analysis.ROI
{
    public class ROIManager
    {
        public ROIList ImportROI(StreamReader readFile)
        {
            ROIList list = new ROIList("ROI of ");

            string line;
            string[] row;

            while ((line = readFile.ReadLine()) != null)
            {
                line = line.Replace(',', '.');
                line = line.Replace('\t', ' ');
                row = line.Split(' ');
                double[] d = new double[row.Length-1];
                string id = "";
                for (int i = 0; i < row.Length; i++)
                {
                    if (i == 0)
                    {
                        id = row[i].Trim();
                    }else if (row[i].Trim().Length > 0)
                    {
                        d[i - 1] = Double.Parse(row[i].Trim());
                    }
                }
                if (d.Length >= 4)
                {
                    list.List.Add(new SquareROI(id, d[0], d[1], d[2], d[3]));
                }
            }

            return list;
        }

        public DataList Calibrate(DataList data, ROIList rois, double minX, double maxX, double minY, double maxY)
        {

            double bestX = 0;
            double bestY = 0;
            double[] bestScore = new double[rois.List.Count];
            for (double x = minX; x <= maxX; x+=5)
            {
                for (double y = minY; y <= maxY; y += 5)
                {
                    double[] currentScore = new double[rois.List.Count];
                    foreach (Item t in data.List)
                    {
                        int ri = 0;
                        foreach (ROI r in rois.List)
                        {
                            if (r.IsInRoi(t.Value[Item.POSITIONX] + x, t.Value[Item.POSITIONY] + y)) currentScore[ri]++;
                            ri++;
                        }
                    }
                    //evlaute score
                    double score = 0;
                    Debug.Write(x + " " + y + ":");
                    for (int i = 0; i < bestScore.Length; i++)
                    {
                        double c=currentScore[i] - bestScore[i];
                        if (c>0) {
                            score += Math.Log10(c);
                        } else if (c<0){
                            score -= Math.Log10(Math.Abs(c));
                        }
                        Debug.Write(currentScore[i] + "-" + bestScore[i] + "=>" + c + " ");
                    }
                    Debug.WriteLine(bestX + "," + bestY + " vs " + x + "," + y);
                    if (score > 0)
                    {
                        bestScore = currentScore;
                        bestX = x;
                        bestY = y;
                    }
                }
            }

            DataList dataRet = new DataList(data.Name + "calibrated");
            double[] bestValue = {bestX, bestY};
            foreach (Item item in data.List)
            {
                Item item2 = item.Clone();
                item2.sum(bestValue);
                dataRet.List.Add(item2);
            }
            return dataRet;

        }

        /// <summary>
        /// Main Method to calculate main indicators
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rois"></param>
        /// <returns></returns>
        public IndicatorList AnalyzeROIs(DataList data, ROIList rois)
        {
            IndicatorList indicators = new IndicatorList("Indicators of " + data.Name);
            double[] timeSpentPerROI = new double[rois.List.Count];
            double[] fixationsPerROI = new double[rois.List.Count];
            List<Counter> sequence = new List<Counter>();

            //First: DATA POINT
            long t = -1;
            foreach (Item item in data.List)
            {
                //skip first sample
                if (t < 0)
                {
                    t = item.Time;
                    continue;
                }

                for (int i = 0; i<rois.List.Count; i++)
                {
                    ROI r = rois.List[i];
                    
                    if (r.IsInRoi(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY]))
                    {
                        Debug.WriteLine(r.Id + ": Time spent " + (item.Time - t));
                        timeSpentPerROI[i] +=  item.Time - t;

                        //push data on counter
                       
                        if ((sequence.Count>0) && (sequence[sequence.Count-1].idx==i)) 
                        {
                            Counter c = sequence[sequence.Count - 1];
                            c.value+=timeSpentPerROI[i];
                        } else {
                            Counter c = new Counter(i, r.Id, timeSpentPerROI[i]);
                            sequence.Add(c);
                        }
                        break;
                    }
                }
                t = item.Time;
            }

            //push result on indicator
            for (int i = 0; i<rois.List.Count; i++)
            {
                ROI r = rois.List[i];
                indicators.List.Add(new Indicator.Indicator("Time on :" + r.Id,timeSpentPerROI[i],0,"ms"));
            }

            //Second: Fixations
            if ((data.Fixations != null) && (data.Fixations.List.Count>0)) 
            {
                foreach(Fixation.Fixation fix in data.Fixations.List) 
                {
                    for (int i = 0; i < rois.List.Count; i++)
                    {
                        ROI r = rois.List[i];
                        if (r.IsInRoi(fix.X, fix.Y))
                        {
                            fixationsPerROI[i]++;
                        }
                    }
                }
            }
            //push result on indicator
            for (int i = 0; i<rois.List.Count; i++)
            {
                ROI r = rois.List[i];
                indicators.List.Add(new Indicator.Indicator("Fixations on :" + r.Id,fixationsPerROI[i],0,"#"));
            }

            //Third: sequencing
            List<int> idxBest = BestSequence(sequence, rois);
            indicators.List.Add(new Indicator.Indicator("Sequencing", idxBest.Count, 0, "#", rois.List.Count));

            //Four: revisited ROI
            Dictionary<string,int> rroi = new Dictionary<string,int>();
            foreach (int idx in idxBest)
            {
                Counter c1 = sequence[idx];

                int v = 0;
                for(int j = idx+1; j<sequence.Count; j++) {
                    Counter c2 = sequence[j];
                    if (c1.Equals(c2))
                    {
                        v++;
                    }
                }

                rroi.Add(c1.label,v);

            }

            //put on indicators
            foreach(string label in rroi.Keys)
            {
                indicators.List.Add(new Indicator.Indicator("Revisited ROI :" + label, rroi[label], 0, "#", 0));
            }

            return indicators;

        }

        public class Counter
        {
            public string label;
            public int idx;
            public double value;

            public Counter(int idx, string label)
            {
                this.idx = idx;
                this.label = label;
            }

            public Counter(int idx, string label, double value)
            {
                this.idx = idx;
                this.label = label;
                this.value = value;
            }

            public override bool Equals(Object o1) {

                return ((Counter)o1).idx == idx;
            }

            public override int GetHashCode()
            {
                return idx;
            }
        }

        private List<int> BestSequence(List<Counter> sequence, ROIList rois)
        {
            int i = 0;
            List<Counter> sequenceToMatch = new List<Counter>();
            foreach (ROI r in rois.List)
            {
                sequenceToMatch.Add(new Counter(i++, r.Id));
            }
            return BestMatch(sequence, sequenceToMatch);
        }

        public List<int> BestMatch(List<Counter> sequence, List<Counter> sequenceToMatch)
        {
            idxsBest = new List<int>();
            BestMatch(sequence, sequenceToMatch, null, 0, 0);
            return idxsBest;
        }


        private List<int> idxsBest = new List<int>();
        private void BestMatch(List<Counter> sequenceSource, List<Counter> sequenceToMatch, List<int> idxsSource, int siStart, int miStart)
        {

            //init solo per la prima volta
            if (idxsSource == null)
            {
                idxsSource = new List<int>();
            }

            //int mi = miStart;
            for (int mi = miStart; mi<sequenceToMatch.Count; mi++)
            {
                
                
                //si sposta fino al simbolo da matchare più vicino
                for (int si = siStart; si < sequenceSource.Count; si++)
                {
                    //simbolo corrente
                    Counter s = sequenceSource[si];
                    //simbolo da matchare
                    Counter m = sequenceToMatch[mi];
                    if (s.Equals(m))
                    {
                        //push on the current queue
                        idxsSource.Add(si);
                        Debug.WriteLine(siStart + " " + miStart + " Adding " + sequenceSource[si].idx + " (si=" + si + " mi="+mi + ")");

                        //move to next symbol
                        mi++;

                        //skip se siamo in fondo
                        if ((si + 1 >= sequenceSource.Count) || (mi>= sequenceToMatch.Count))
                        {
                            //it is best?
                            if (idxsBest.Count < idxsSource.Count)
                            {
                                Debug.WriteLine("Update best");
                                idxsBest = idxsSource;
                            }
                            return;
                        }
                        //move next
                        List<int> idxsCopy = new List<int>(idxsSource);
                        BestMatch(sequenceSource, sequenceToMatch, idxsCopy, si + 1, mi);
                    }
                }
            }
            
        }

    }
}
