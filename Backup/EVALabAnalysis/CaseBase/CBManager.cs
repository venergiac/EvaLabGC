using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;                      // State variables
using System.Globalization;
using System.Data.OleDb;             // Date

namespace EVALabAnalysis.CaseBase
{
    public class CBManager
    {

        private List<Case> cases = new List<Case>();
        private List<MetaFeature> metaFeatures = new List<MetaFeature>();

        public List<MetaFeature> MetaFeatures
        {
            get { return metaFeatures; }
        }

        public List<Case> Cases
        {
            get { return cases; }
        }
        public string DB_CONN_STRING =
            "Driver={Microsoft Access Driver (*.mdb)}; " +
            "DBQ=CB.mdb";

        public void Init(string dbConnection)
        {
            DB_CONN_STRING = dbConnection;
            OleDbConnection  conn = new OleDbConnection(DB_CONN_STRING);
            try
            {
                conn.Open();
                
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM CASEBASE", conn);
                OleDbDataReader dr = cmd.ExecuteReader();

                int i = 0;

                while (dr.Read())
                {
                    Case thisCase = new Case((string)dr["CASENAME"], (string)dr["CASEDESCRIPTION"], (string)dr["DIAGNOSIS"]);
                    
                    //Age
                    NumericFeature feature = new NumericFeature("Age", ((DateTime)dr["SUBJ_BIRTH"]).Year - DateTime.Now.Year, 1);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("Age", true));

                    //VPeak
                    feature = new NumericFeature("Saccade VPeak", (double)dr["SACC_VPEAK"], (double)dr["SACC_VPEAK_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("Saccade VPeak", true));

                    //Errors
                    feature = new NumericFeature("Saccade Error", (double)dr["SACC_ERROR"], (double)dr["SACC_ERROR_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("Saccade Error", true));

                    //Errors
                    feature = new NumericFeature("Saccade Delay", (double)dr["SACC_DELAY"], (double)dr["SACC_DELAY_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("Saccade Delay", true));

                    //AntiSaccade Errors
                    feature = new NumericFeature("AntiSaccade Error", (double)dr["ASACC_ERROR"], (double)dr["ASACC_ERROR_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("AntiSaccade Error", true));

                    //TMT Fixations Dispersion
                    feature = new NumericFeature("TMT Fixations Dispersion", (double)dr["TMT_FIX_DISP"], (double)dr["TMT_FIX_DISP_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("TMT Fixations Dispersion", true));

                    //TMT Revisited ROIs
                    feature = new NumericFeature("TMT Revisited ROIs", (double)dr["TMT_REV_ROI"], (double)dr["TMT_REV_ROI_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i == 0) metaFeatures.Add(new MetaFeature("TMT Revisited ROIs", true));

                    //TMT Time ROI to ROI
                    feature = new NumericFeature("TMT Time ROI to ROI", (double)dr["TMT_TIME_ROI"], (double)dr["TMT_TIME_ROI_VAR"]);
                    thisCase.Features.Add(feature);
                    if (i==0) metaFeatures.Add(new MetaFeature("TMT Time ROI to ROI", true));

                    i++;
                    cases.Add(thisCase);
                }
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// Ritorna la lista dei casi migliori
        /// </summary>
        /// <param name="features"></param>
        /// <returns></returns>
        public List<Result> BestCases(List<IFeature> features)
        {
            Random rnd = new Random();
            List<Result> results = new List<Result>();
            foreach (Case thisCase in cases)
            {
                double score = 0;
                foreach (IFeature feature in features)
                {
                    IFeature featureCase = thisCase.Features.Find(delegate(IFeature feature1) { return feature1.Name.Equals(feature.Name); });

                    score += featureCase.Compare(feature);
                }

                
                score = rnd.NextDouble();

                results.Add(new Result(score, thisCase));
            }

            return results;
        }

    }
}
