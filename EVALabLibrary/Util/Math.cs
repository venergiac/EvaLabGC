using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabGC.Util
{
    class Math
    {

        public static void Rotate(double centerX, double centerY, ref double X, ref double Y, double th)
        {
            X = (X - centerX) * System.Math.Cos(th) + (Y - centerY) * System.Math.Sin(th) + centerX;
            Y = -(X - centerX) * System.Math.Sin(th) + (Y - centerY) * System.Math.Cos(th) + centerY;

        }

        public static double StandardDeviation(double[] num)
        {
            double Sum = 0.0, SumOfSqrs = 0.0;
            for (int i = 0; i < num.Length; i++)
            {
                Sum += num[i];
                SumOfSqrs += num[i]*num[i];
            }
            double topSum = (num.Length * SumOfSqrs) - (System.Math.Pow(Sum, 2));
            double n = (double)num.Length;
            return System.Math.Sqrt(topSum / (n * (n - 1)));
        }

        public static double StandardDeviationCol(double[,] num, int col)
        {
            double Sum = 0.0, SumOfSqrs = 0.0;
            int len = num.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                Sum += num[i, col];
                SumOfSqrs += System.Math.Pow(num[i, col], 2);
            }
            double topSum = (len * SumOfSqrs) - (System.Math.Pow(Sum, 2));
            double n = System.Convert.ToDouble(len);
            return System.Math.Sqrt(topSum / (n * (n - 1)));
        }

        public static double StandardDeviationRow(double[,] num, int row)
        {
            double Sum = 0.0, SumOfSqrs = 0.0;
            int len = num.GetLength(1);
            for (int j = 0; j < len; j++)
            {
                Sum += num[row,j];
                SumOfSqrs += System.Math.Pow(num[row, j], 2);
            }
            double topSum = (len * SumOfSqrs) - (System.Math.Pow(Sum, 2));
            double n = System.Convert.ToDouble(len);
            return System.Math.Sqrt(topSum / (n * (n - 1)));
        } 

        public static double Average(double[] num)
        {
            double sum = 0.0;
            for (int i = 0; i < num.Length; i++)
            {
                sum += num[i];
            }
            return sum / System.Convert.ToDouble(num.Length);
        }

        public static double NormalDistribution(double x, double mean, double deviation)
        {
            return NormalDensity(x, mean, deviation);
        }

        private static double NormalDensity(double x, double mean, double deviation)
        {
            return System.Math.Exp(-(System.Math.Pow((x - mean) / deviation, 2) / 2)) / System.Math.Sqrt(2 * System.Math.PI) / deviation;
        }   
    }
}
