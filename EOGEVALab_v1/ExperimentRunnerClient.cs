using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EvaLab.EOG
{
    class ExperimentRunnerClient : ExperimentRunner, NIDAQMxClient
    {

        private double[] dataValues = null;
          public override void Stop() {
              Debug.WriteLine("ExperimentRunnerClient: STOP has been called");
              ((NIDAQMxReaderService)reader).Dispose();
              base.Stop();
          }

          public override void Start(){
              requestData = false;
              ((NIDAQMxReaderService)reader).CheckTask();
              base.Start();
              ((NIDAQMxReaderService)reader).Client = this;
              ((NIDAQMxReaderService)reader).Start();
          }

          public override double[] DoData(){
              this.Start();
              System.Threading.Thread.Sleep(1000);
              this.Stop();
              if (currentTrigger != null) currentTrigger.Do(dataValues);

              return dataValues;
          }

          public void DoReceive(double[] data) {
              dataValues = data;
              time = GetTimeMS() - startTime;
              if (currentTrigger != null) currentTrigger.Do(dataValues);
              DoEventRunner(currentExperimentItem, CurrentExperimentValue, data, time);
          }
    }
}
