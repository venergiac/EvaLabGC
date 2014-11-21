using System;
using System.Collections.Generic;
using System.Text;
using EVALabGC.ASL;

namespace EVALabGC
{
    public interface StimulusController
    {
        int GetCurrentStimulusId(Data data);
        void OnStart();
        void OnStop();
    }
}
