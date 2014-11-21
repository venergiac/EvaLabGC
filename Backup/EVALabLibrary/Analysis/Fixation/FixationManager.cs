using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;
using EVALab.Analysis.Filter;
using System.Diagnostics;

namespace EVALab.Analysis.Fixation
{
    public class FixationManager
    {

        public static Fixation MakeFixation(DataList input, int idxStart, int idxEnd)
        {
            double disp = Dispersion(input, idxStart, idxEnd);
            double x = input.Mean(Item.POSITIONX, idxStart, idxEnd + 1);
            double y = input.Mean(Item.POSITIONY, idxStart, idxEnd + 1);
            Debug.WriteLine("Make fixation in " + idxStart + " " + idxEnd);
            //ASSEMBLE FIXATIONS
            return new Fixation(x, y, disp, input.List[idxEnd].Time - input.List[idxStart].Time, idxStart, idxEnd, 0);
        }

        public FixationList DoFixation(DataList input, double maxDispersion, double minDuration, bool applyFilter)
        {

            //fixations
            FixationList list = new FixationList("Fixation of " + input.Name);

            //extract sample rate
            double time = 0;
            double diffTime = 0;
            double ii = 0;
            foreach (Item item in input.List)
            {
                if (ii != 0) diffTime += item.Time - time;
                time = item.Time;
                ii++;
                if (ii > 1000) break;
            }

            diffTime = diffTime / ii;

            int minDurationSample = (int)Math.Round( minDuration / diffTime );
            double disp = 0;
            FilterManager manager = new FilterManager();

            DataList inputOriginal = input;
            if (applyFilter)
            {
                input = manager.Filter(input, 20, 5);
            }

            for (int i = 0; i < input.List.Count - minDurationSample; i++)
            {
                // once i is closer to the end of the trial than the size
                // of the minimum duration window then we cannot define further fixations 

                //the end point
                int j = (int)(i + minDurationSample - 1);
                disp = Dispersion(input, i, j);

                //move to next j having dispersion > MaxDisp
                while ((disp <= maxDispersion) && (j < input.List.Count - 1))
                {
                    disp = Dispersion(input, i, ++j);
                }

                //adjustj
                j--;

                if (inputOriginal.List[j].Time - inputOriginal.List[i].Time>= minDuration)
                {
                    //assemble fixations
                    Fixation fixation = MakeFixation(inputOriginal, i, j);
                    list.List.Add(fixation);
                    i = j + 1;
                }
            }

            return list;
        }

        private static double Dispersion(DataList input, int i, int j)
        {
            Item maxItemX = input.Max(Item.POSITIONX, i, j + 1);
            Item maxItemY = input.Max(Item.POSITIONY, i, j + 1);
            Item minItemX = input.Min(Item.POSITIONX, i, j + 1);
            Item minItemY = input.Min(Item.POSITIONY, i, j + 1);
            return maxItemX.Value[Item.POSITIONX] - minItemX.Value[Item.POSITIONX] + maxItemY.Value[Item.POSITIONY] - minItemY.Value[Item.POSITIONY];
        }

    }
}
