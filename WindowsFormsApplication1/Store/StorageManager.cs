using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Util.Meta;
using System.IO;
using EVALabGC.Tasks.Forms.Validation;
using System.Diagnostics;
using EVALab.Util.Performance;
using EVALab.Util.IO;

namespace EVALabGC.Store
{
    public class StorageManager
    {


        private StorageManager()
        {
        }

        private List<Experiment> experiments = new List<Experiment>();

        public List<Experiment> Experiments
        {
            get { return experiments; }
            set { experiments = value; }
        }

        private static StorageManager mngr = new StorageManager();
        public static StorageManager Instance()
        {
            return mngr;
        }

        public void Add(double x, double y, int p, double time, long id, int jx, int jy, int jb)
        {
            if (!ready) return;
#if DEBUG
            PerformanceUtil.Instance.BeginAverageTime();
#endif
            //stBuilder.AppendFormat("{0}  {1}  {2}  {3}  {4}  {5}  {6}  {7}\n",
            //    x, y, p, time, id, jx, jy, jb);

            x = x / Context.WIDTH * Context.STORAGEWIDTH;
            y = y / Context.HEIGHT * Context.STORAGEHEIGHT;

            stBuilder.AppendLine(String.Concat(x, delimiter, y, delimiter, p, delimiter, time, delimiter, id, delimiter, jx, delimiter, jy, delimiter, jb));
            //stBuilder.AppendLine(x + "  " + y + "  " + p + "  " + time + "  " + id + "  " + jx + "  " + jy + "  " + jb);
#if DEBUG
            PerformanceUtil.Instance.EndAverageTime();
#endif
        }
        StringBuilder stBuilder = new StringBuilder();
        string delimiter = "  ";
        public string Buffer
        {
            get { return stBuilder.ToString(); }
        }

        private volatile bool ready = true;
        public bool Ready
        {
            get { return ready; }
            set { ready = value; }
        }

        public void Init(Context ctx)
        {
            Reset();
            ready = true;
            ctx.Log("StorageManager Ready");
        }

        public void Reset()
        {
            this.stBuilder = new StringBuilder("");
            startTime = DateTime.Now;
        }

        public Experiment AcceptCurrentBuffer(Experiment experiment)
        {
            experiment.Buffer = this.Buffer;
            experiments.Add(experiment);

            return experiment;
        }

        public void Clear()
        {
            Reset();
            experiments.Clear();
        }

        public void RemoveExperiment(Experiment experiment)
        {
            int i = 0;
            int ii = -1;
            foreach (Experiment e in experiments)
            {
                if (e.Id.Equals(experiment.Id))
                {
                    ii = i;
                    break;
                }
                i++;
            }
            if (ii >= 0) experiments.RemoveAt(ii);
        }

        private DateTime startTime = DateTime.Now;

        protected DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public void Store(string pathBase, ExperimentSettings expSettings, Context ctx)
        {
            string filenameRaw = "DATA.txt";
            string filename = "EXPERIMENT.xml";

            string lastValidation = "";

            foreach (Experiment exp in experiments)
            {

                if (exp is ValidationExperiment) 
                {
                    lastValidation = exp.ToXml();
                    continue;
                }
                string path = pathBase + "\\" + StringConverter.SafeFilePath(expSettings.Id) + "_" + StringConverter.SafeFilePath(exp.Id);

                ctx.Log("Making directory " + path, 0);
                Directory.CreateDirectory(@"" + path);
                ctx.Log("Directory " + path + " made", 20);

                ctx.Log("Saving raw file data " + filenameRaw, 40);
                StreamWriter sw = new StreamWriter(@"" + path + "\\" + filenameRaw);
                sw.Write(exp.Buffer);
                sw.Close();
                ctx.Log("Raw file data " + filenameRaw + " saved", 60);

                string buffer2 = "<experiment>";
                buffer2 += exp.ToXml();
                buffer2 += expSettings.ToXML();
                buffer2 += lastValidation;
                buffer2 += "<data><![CDATA[";
                buffer2 += exp.Buffer;
                buffer2 += "]]></data>";
                buffer2 += "</experiment>";

                ctx.Log("Saving raw file data " + filename, 80);
                sw = new StreamWriter(@"" + path + "\\" + filename);
                sw.Write(buffer2);


                sw.Close();
                ctx.Log("Data saved on" + path, 100);
            }

            Clear();
        }
    }
}
