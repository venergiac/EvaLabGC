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
            // prevent crash
            idxStart = Math.Min(idxStart, input.List.Count-1);
            idxEnd = Math.Min(idxEnd, input.List.Count-1);

            double disp = Dispersion(input, idxStart, idxEnd);
            double x = input.Mean(Item.POSITIONX, idxStart, idxEnd + 1);
            double y = input.Mean(Item.POSITIONY, idxStart, idxEnd + 1);
            Debug.WriteLine("Make fixation in " + idxStart + " " + idxEnd);
            //ASSEMBLE FIXATIONS
            return new Fixation(x, y, disp, input.List[idxEnd].Time - input.List[idxStart].Time, idxStart, idxEnd, 0);
        }

        private static Fixation RebuildFixation(DataList input, Fixation fixation)
        {
            // prevent crash
            int idxStart = Math.Min(fixation.IdxStart, input.List.Count - 1);
            int idxEnd = Math.Min(fixation.IdxEnd, input.List.Count - 1);

            fixation.Dispersion = Dispersion(input, idxStart, idxEnd);
            fixation.X = input.Mean(Item.POSITIONX, idxStart, idxEnd + 1);
            fixation.Y = input.Mean(Item.POSITIONY, idxStart, idxEnd + 1);
            fixation.Duration = input.List[idxEnd].Time - input.List[idxStart].Time;
            Debug.WriteLine("Rebuild fixation in " + idxStart + " " + idxEnd);
            //ASSEMBLE FIXATIONS
            return fixation;
        }

        public static void MergeFixation(DataList input, int idxFix1, int idxFix2)
        {
            if (input==null) return;
            if (input.Fixations==null) return;
            if ((input.Fixations.List.Count <= 0) || (idxFix1 < 0) || (idxFix2 < 0) || (input.Fixations.List.Count <= idxFix1) || (input.Fixations.List.Count <= idxFix2)) return;
            int idx1 = Math.Min(idxFix1, idxFix2);
            int idx2 = Math.Max(idxFix1, idxFix2);

            int idxStart = input.Fixations.List[idx1].IdxStart;
            int idxEnd = input.Fixations.List[idx2].IdxEnd;

            input.Fixations.List[idx1] = MakeFixation(input, idxStart, idxEnd);
            input.Fixations.List.RemoveAt(idx2);
        }

        public static Fixation AdjustBorderFixation(DataList input, int idxFix, int window)
        {
            Fixation fixO = input.Fixations.List[idxFix];
            Fixation fix = (Fixation)fixO.Clone();

            AdjustBorderFixation(input, ref fix, window);

            return fix;
        }

        private static void AdjustBorderFixation(DataList input, ref Fixation fix, int window)
        {
            Item itemEnd = input.List[fix.IdxEnd];
            double x = itemEnd.Value[Item.POSITIONX];
            double y = itemEnd.Value[Item.POSITIONY];

            double Dx = fix.X - x;
            double Dy = fix.Y - y;

            //back reduction
            for (int i = 1; i < window; i++)
            {
                if (fix.IdxEnd - 1 <= fix.IdxStart) break;
                itemEnd = input.List[fix.IdxEnd-1];
                x = itemEnd.Value[Item.POSITIONX];
                y = itemEnd.Value[Item.POSITIONY];

                if ( ((Math.Abs(fix.X - x) <= Math.Abs(Dx)) && (Math.Sign(fix.X - x) == Math.Sign(Dx)))
                    || ( (Math.Abs(fix.Y - y) <= Math.Abs(Dy)) && (Math.Sign(fix.Y - y) == Math.Sign(Dy))))
                {
                    Dx = fix.X - x;
                    Dy = fix.Y - y;
                    fix.IdxEnd -= 1;
                    Debug.Write("->" + fix.IdxEnd);
                }
                else
                {
                    break;
                }

            }

            itemEnd = input.List[fix.IdxStart];
            x = itemEnd.Value[Item.POSITIONX];
            y = itemEnd.Value[Item.POSITIONY];

            Dx = fix.X - x;
            Dy = fix.Y - y;


            //forward reduction
            for (int i = 1; i < window; i++)
            {

                if (fix.IdxStart + 1 >= fix.IdxEnd) break;
                itemEnd = input.List[fix.IdxStart + 1];
                x = itemEnd.Value[Item.POSITIONX];
                y = itemEnd.Value[Item.POSITIONY];

                if ((Math.Abs(fix.X - x) <= Dx) && (Math.Sign(fix.X - x) == Math.Sign(Dx))
                    && (Math.Abs(fix.Y - y) <= Dy) && (Math.Sign(fix.Y - y) == Math.Sign(Dy)))
                {
                    Dx = fix.X - x;
                    Dy = fix.Y - y;
                    fix.IdxStart += 1;
                }
                else
                {
                    break;
                }

            }

            RebuildFixation(input, fix);
        }

        public FixationList DoFixation(DataList input, double maxDispersion, double minDuration, bool applyFilter, bool adjustBorder)
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
                input = manager.Filter(input, 20, 5, WaveForm.WaveFormType.None, false);
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

                    if (adjustBorder)
                    {
                        Debug.Write(fixation.IdxEnd);
                        AdjustBorderFixation(input, ref fixation, minDurationSample);
                        Debug.WriteLine("\t" + fixation.IdxEnd);
                    }

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
            return Math.Sqrt( Math.Pow(maxItemX.Value[Item.POSITIONX] - minItemX.Value[Item.POSITIONX],2) + Math.Pow(maxItemY.Value[Item.POSITIONY] - minItemY.Value[Item.POSITIONY],2));
        }

    }
}
