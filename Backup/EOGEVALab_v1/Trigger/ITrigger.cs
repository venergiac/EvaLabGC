using System;
using System.Collections.Generic;
using System.Text;

namespace EvaLab.EOG.Trigger
{
    public interface ITrigger
    {
        string Name
        {
            get;
        }
        void Init(string name, string[] parameters);

        void DoAfter(IContext ctx);
        void DoBefore(IContext ctx);
        void Do(double[] data);
    }
}
