using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALabAnalysis.CaseBase
{
    public class Result
    {
        public Result(double score, Case thisCase)
        {
            this.score = score;
            this.thisCase = thisCase;
        }
        private Case thisCase = null;

        public Case ThisCase
        {
            get { return thisCase; }
        }
        private double score = 0;

        public double Score
        {
            get { return score; }
            set { score = value; }
        }

    }
}
