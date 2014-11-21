using System;
using System.Collections.Generic;
using System.Text;
using EvaLab.EOG.Model;

namespace EvaLab.EOG
{
    public interface IContext
    {
        ParallelPort GetParallelPort();
        Calibration GetCalibration();
        ExperimentRunner GetExperimentRunner();
        NIDAQMxReader GetExperimentReader();
        List<ExperimentItem> GetExperiment();
    }
}
