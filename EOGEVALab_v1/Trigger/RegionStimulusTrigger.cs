using System;
using System.Collections.Generic;
using System.Text;
using EvaLab.EOG.Model;
using System.Diagnostics;

namespace EvaLab.EOG.Trigger
{
    public class RegionStimulusTrigger : ITrigger
    {
        string name = null;
        Calibration calibration = null;
        ExperimentRunner runner = null;

        private int[] deltaTarget = null; //inf
        private double deltaPosition = -1; //inf
        private double deltaVelocity = -1; //inf

        private bool done = false;

        private Random rnd = new Random();
        private int targetShift = -1;


        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public void Init(string name, string[] parameters) {
            this.name = name;
            if (parameters.Length > 0)
            {
                deltaPosition = Double.Parse(parameters[0]);
            }
            if (parameters.Length > 1)
            {
                deltaVelocity = Double.Parse(parameters[1]);
            }
            else
            {
                return;
            }

            deltaTarget = new int[parameters.Length-2];
            for (int i = 0; i < deltaTarget.Length; i++)
            {
                deltaTarget[i] = Int16.Parse(parameters[i+2]);
            }
        }

        public void DoAfter(IContext ctx) {
            Debug.WriteLine("TRIGGER " + name + ": DOBEFORE DONE " + done);
        }

        public void DoBefore(IContext ctx)
        {
            targetShift = deltaTarget[rnd.Next(deltaTarget.Length - 1)];
            this.calibration = ctx.GetCalibration();
            this.runner = ctx.GetExperimentRunner();
            done = false;
            Debug.WriteLine("TRIGGER " + name + ": DOBEFORE DONE " + done);
        }

        public void Do(double[] data)
        {
            if (done) return;
            if (deltaPosition < 0) return;
            if (targetShift < 0) return;
            double v1 = calibration.ScaleCh1(data[0]);
            double v2 = 0;
            if (data.Length > 1)
            {
                v2 = calibration.ScaleCh1(data[1]); 
            }

            if ((v1 >= deltaPosition) || (v2 >= deltaPosition))
            {
                Debug.WriteLine("TRIGGER "+name+": DO "+targetShift);
                this.runner.DoCurrentExperimentValue(this.runner.CurrentExperimentValue + targetShift);
                done = true;
            }

        }
    }
}
