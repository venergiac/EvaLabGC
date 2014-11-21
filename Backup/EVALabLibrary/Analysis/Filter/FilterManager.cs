using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;
using EVALab.Util.Filter;
using MathNet.SignalProcessing.Filter;
using System.Diagnostics;

namespace EVALab.Analysis.Filter
{
    public class FilterManager
    {

        public static DataList Convert2Degree(DataList input, double pixelPerDegreeX, double pixelPerDegreeY)
        {
            DataList output = new DataList(input.Name + " converted to degree.");
            output.Type = DataObject.DataType.Degree;
            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] /= pixelPerDegreeX;
                item2.Reference[Item.POSITIONX] /= pixelPerDegreeX;
                if (item2.CountValue > 1)
                {
                    item2.Value[Item.POSITIONY] /= pixelPerDegreeY;
                }
                if (item2.CountReference> 1)
                {
                    item2.Reference[Item.POSITIONY] /= pixelPerDegreeY;
                }
                output.List.Add(item2);
            }

            return output;
        }

        public static DataList Convert2Degree(DataList input, double ticksPerDegree)
        {
            DataList output = new DataList(input.Name + " converted to degree.");
            output.Type = DataObject.DataType.Degree;
            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] /= ticksPerDegree;
                item2.Reference[Item.POSITIONX] /= ticksPerDegree;
                item2.Value[Item.POSITIONY] /= ticksPerDegree;
                item2.Reference[Item.POSITIONY] /= ticksPerDegree;

                output.List.Add(item2);
            }

            return output;

        }

        //second order
        public DataList Butterworth2Order50Hz(DataList input)
        {
            return Butterworth(input, new double[] { -0.8093181379, -1.7891095492 }, 1.111596605);
        }

        public DataList Butterworth(DataList input, double[] zCoeff, double gain)
        {
            DataList output = DataList.NewInstance(input);
            output.Name += " filtered";

            Butterworth filterX = new Butterworth(zCoeff,gain);
            Butterworth filterY = new Butterworth(zCoeff, gain);

            //put first data
            int c = 0;
            foreach (Item item in input.List)
            {
                filterX.FilterValue(item.Value[Item.POSITIONX]);
                filterY.FilterValue(item.Value[Item.POSITIONY]);
                if (c++ > zCoeff.Length) break;
            }
            
            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] = filterX.FilterValue(item.Value[Item.POSITIONX]);
                item2.Value[Item.POSITIONY] = filterY.FilterValue(item.Value[Item.POSITIONY]);
                output.List.Add(item2);
            }

            return Scale(output, input);
        }

        public DataList Denoise(DataList input, int order)
        {
            DataList output = DataList.NewInstance(input);
            output.Name += " denoised";

            OnlineFilter filterX = OnlineFilter.CreateDenoise(order);
            OnlineFilter filterY = OnlineFilter.CreateDenoise(order);

            //put first data
            int c = 0;
            foreach (Item item in input.List)
            {
                filterX.ProcessSample(item.Value[Item.POSITIONX]);
                filterY.ProcessSample(item.Value[Item.POSITIONY]);
                if (c++ > order) break;
            }


            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] = filterX.ProcessSample(item.Value[Item.POSITIONX]);
                item2.Value[Item.POSITIONY] = filterY.ProcessSample(item.Value[Item.POSITIONY]);
                output.List.Add(item2);
            }

            return Scale(output, input);
        }

        public DataList Filter(DataList input, int cutOff, int order)
        {
            DataList output = DataList.NewInstance(input);
            output.Name += " filtered";

            double time = 0;
            double diffTime = 0;
            double i = 0;
            foreach (Item item in input.List)
            {
                if (i!=0) diffTime += item.Time - time;
                time = item.Time;
                i++;
                Debug.WriteLine(time);
                if (i > 100) break;
            }

            diffTime = diffTime/i;

            Debug.WriteLine("sample rate " + (1000.0 / diffTime));

            OnlineFilter filterX = OnlineFilter.CreateLowpass(ImpulseResponse.Finite, (1000.0 / diffTime), cutOff, order);
            OnlineFilter filterY = OnlineFilter.CreateLowpass(ImpulseResponse.Finite, (1000.0 / diffTime), cutOff, order);

            //put first data
            int c = 0;
            foreach (Item item in input.List)
            {
                filterX.ProcessSample(item.Value[Item.POSITIONX]);
                filterY.ProcessSample(item.Value[Item.POSITIONY]);
                if (c++ > order) break;
            }


            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] = filterX.ProcessSample(item.Value[Item.POSITIONX]);
                item2.Value[Item.POSITIONY] = filterY.ProcessSample(item.Value[Item.POSITIONY]);
                output.List.Add(item2);
            }

            return Scale(output, input);
        }

        private DataList Scale(DataList input, DataList source)
        {
            double inMinX = input.Min(Item.POSITIONX).Value[Item.POSITIONX];
            double inMaxX = input.Max(Item.POSITIONX).Value[Item.POSITIONX];
            double inMinY = input.Min(Item.POSITIONY).Value[Item.POSITIONY];
            double inMaxY = input.Max(Item.POSITIONY).Value[Item.POSITIONY];

            double minX = source.Min(Item.POSITIONX).Value[Item.POSITIONX];
            double maxX = source.Max(Item.POSITIONX).Value[Item.POSITIONX];
            double minY = source.Min(Item.POSITIONY).Value[Item.POSITIONY];
            double maxY = source.Max(Item.POSITIONY).Value[Item.POSITIONY];

            DataList output = DataList.NewInstance(input);

            foreach (Item item in input.List)
            {
                Item item2 = item.Clone();
                item2.Value[Item.POSITIONX] = (maxX - minX) * (item.Value[Item.POSITIONX] - inMinX) / (inMaxX - inMinX) + minX;
                item2.Value[Item.POSITIONY] = (maxY - minY) * (item.Value[Item.POSITIONY] - inMinY) / (inMaxY - inMinY) + minY; 

                output.List.Add(item2);
            }

            return output;
        }
    }
}
