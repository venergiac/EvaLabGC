using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis;
using EVALab.Analysis.Data;
using System.IO;
using EVALabAnalysis.Parameter;
using EVALab.Analysis.Filter;
using EVALab.Analysis.Saccade;
using EVALab.Analysis.Fixation;
using EVALabAnalysis.CaseBase;
using EVALab.Analysis.Util;
using EVALab.Analysis.ROI;
using System.Runtime.Serialization.Formatters.Binary;
using EVALab.Analysis.Indicator;
using EVALab.Util.IO;
using System.Data;

namespace EVALabAnalysis
{
    /// <summary>
    /// Questa classe si occupa di gestire tutte le funzionalità dell'interfaccia grafica e gestisce i trial attualmente importati.
    /// </summary>
    public class AnalysisManager
    {

        public enum AnalysisType
        {
            X,
            Y,
            XY
        }

        #region Manager
        /// <summary>
        /// Gestire dei dati
        /// </summary>
        DataManager dataMngr = new DataManager();
        ROIManager roiMngr = new ROIManager();
        FilterManager filterMngr = new FilterManager();
        SaccadeManager saccadeMngr = new SaccadeManager();
        FixationManager fixationMngr = new FixationManager();
        CBManager cbMngr = new CBManager();
        #endregion


        private static AnalysisManager singletone = null;
        private AnalysisManager()
        {
            cbMngr.Init(Properties.Settings.Default.DB);
        }

        public static AnalysisManager Instance
        {
           get {
               if (singletone == null)
               {
                   singletone = new AnalysisManager();
               }
               return singletone;
           }

        }

        #region
        private List<ROIList> rois = new List<ROIList>();
        public List<ROIList> ROIs
        {
            get { return rois; }
        }
        #endregion

        #region Trial
        /// <summary>
        /// Trials importati
        /// </summary>
        private List<Trial> trials = new List<Trial>();

        public List<Trial> Trials
        {
            get { return trials; }
            set { trials = value; }
        }
        #endregion

        #region Case
        public List<Case> Cases
        {
            get { return cbMngr.Cases; }
        }
                public List<Result> BestCases(DataList list)
        {
            List<IFeature> features = new List<IFeature>();
            //TODO convert
            
            return cbMngr.BestCases(features);
        }
        #endregion

        public void CreateROIListFromDataFile(string file)
        {
            //apro il file
            StreamReader readFile = new StreamReader(file);
            ROIList roiList = roiMngr.ImportROI(readFile);
            roiList.Name = roiList.Name + ":" +file;
            readFile.Close();
            rois.Add(roiList);
        }

        public void CreateBinocularTrialFromDataFile(string file)
        {
            //apro il file
            StreamReader readFile = new StreamReader(file);

            DataList[] dataList = dataMngr.ImportXMLDataEOG(readFile,5,255);
            readFile.Close();

            //creo il trial
            Trial trial = new Trial();
            trial.Name = file;
            trial.Data.Add(dataList[0]);
            trial.Data.Add(dataList[1]);
            trials.Add(trial);
        }

        public void CreateBinocularTrialFromRawDataFile(string file)
        {
            //apro il file
            StreamReader readFile = new StreamReader(file);

            DataList[] dataList = dataMngr.ImportRawDataEOG(readFile, 0, 255);
            readFile.Close();
            //creo il trial
            Trial trial = new Trial();
            trial.Name = file;
            trial.Data.Add(dataList[0]);
            trial.Data.Add(dataList[1]);
            trials.Add(trial);
        }

        public void CreateASLTrialFromDataFile(string file)
        {

            //apro il file
            StreamReader readFile = new StreamReader(file);
           
            DataList dataList = dataMngr.ImportXMLDataASL(readFile, Properties.Settings.Default.ASLMaxX, Properties.Settings.Default.ASLMaxY,Properties.Settings.Default.PixelX, Properties.Settings.Default.PixelY);
            dataList.Name = dataList.Name + ": " + file;
            readFile.Close();
            //creo il trial
            Trial trial = new Trial();
            trial.Name = file;
            trial.Data.Add(dataList);
            trials.Add(trial);
        }

        public void CreateRawASLTrialFromDataFile(string file)
        {

            //apro il file
            StreamReader readFile = new StreamReader(file);

            DataList dataList = dataMngr.ImportRawDataASL(readFile, Properties.Settings.Default.ASLMaxX, Properties.Settings.Default.ASLMaxY, Properties.Settings.Default.PixelX, Properties.Settings.Default.PixelY);
            dataList.Name = dataList.Name + ": " + file;
            readFile.Close();
            //creo il trial
            Trial trial = new Trial();
            trial.Name = file;
            trial.Data.Add(dataList);
            trials.Add(trial);
        }

        public void CreateRawTrialFromCSVDataFile(string file)
        {

            //apro il file
            StreamReader readFile = new StreamReader(file);

            DataList dataList = dataMngr.ImportRawDataASL(readFile, Properties.Settings.Default.PixelX, Properties.Settings.Default.PixelY, Properties.Settings.Default.PixelX, Properties.Settings.Default.PixelY);
            dataList.Name = dataList.Name + ": " + file;
            readFile.Close();
            //creo il trial
            Trial trial = new Trial();
            trial.Name = file;
            trial.Data.Add(dataList);
            trials.Add(trial);
        }

        public void CreateRawDataFileFromDataList(string file, DataList data)
        {

            //apro il file
            StreamWriter writerFile = new StreamWriter(file);

            dataMngr.ExportRawDataASL(writerFile, data, Properties.Settings.Default.PixelX, Properties.Settings.Default.PixelY, Properties.Settings.Default.ASLMaxX, Properties.Settings.Default.ASLMaxY);
            writerFile.Close();
        }

        /// <summary>
        /// Clone Data
        /// </summary>
        /// <param name="dataList"></param>
        public void CloneData(DataList dataList)
        {
            Trial trial = FindTrial(dataList);
            trial.Data.Add((DataList)dataList.Clone());
        }

        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="dataList"></param>
        public void DeleteData(DataList dataList)
        {
            Trial trial = FindTrial(dataList);
            if (trial.Data.Count == 1)
            {
                trials.Remove(trial);
            }
            else
            {
                trial.Data.Remove(dataList);
            }
        }

        public void ConvertToDegreeData(DataList input, DataParams parms)
        {
            Trial trial = FindTrial(input);
            DataList data = null;
            if (input.Type == DataList.DataType.Tick)
            {
                data = FilterManager.Convert2Degree(input, parms.TicksPerDegree);
            }
            else if (input.Type == DataList.DataType.Pixels)
            {
                data = FilterManager.Convert2Degree(input, parms.PixelsPerDegreeX, parms.PixelsPerDegreeY);
            }
            if (data!=null) trial.Data.Add(data);
        }

        public void FilterData(DataList input, FilterParams parms)
        {
            Trial trial = FindTrial(input);
            DataList data = data = filterMngr.Filter(input, parms.CutOff, parms.Order,parms.OnlyOnWaveForm, parms.Scale);
            if (data!=null) trial.Data.Add(data);
        }

        public void Butterworth2Order50Hz(DataList input, FilterParams parms)
        {
            Trial trial = FindTrial(input);
            DataList data = data = filterMngr.Butterworth2Order50Hz(input, parms.OnlyOnWaveForm, parms.Scale);
            if (data != null) trial.Data.Add(data);
        }

        public void DenoiseData(DataList input, FilterParams parms)
        {
            Trial trial = FindTrial(input);
            DataList data = data = filterMngr.Denoise(input, parms.Order, parms.OnlyOnWaveForm, parms.Scale);
            if (data != null) trial.Data.Add(data);
        }

        public void MedianFilterData(DataList input, FilterParams parms)
        {
            Trial trial = FindTrial(input);
            DataList data = data = filterMngr.Median(input, parms.Order, parms.OnlyOnWaveForm, parms.Scale);
            if (data != null) trial.Data.Add(data);
        }

        public void FindSaccade(DataList input, SaccadeParams parms, AnalysisType an)
        {
            DataList data = input;
            if (input.Type == DataObject.DataType.Pixels)
            {
                data = FilterManager.Convert2Degree(input, parms.PixelsPerDegreeX, parms.PixelsPerDegreeY);
            }
            else if (input.Type == DataObject.DataType.Tick)
            {
                data = FilterManager.Convert2Degree(input, parms.TicksPerDegree);
            }

            if (an == AnalysisType.XY)
            {
                input.Saccades = saccadeMngr.DoSaccade(data, parms.SaccadeVelThresh, parms.PctPeak, parms.SaccadeWindow, parms.SaccadeMinDuration, true, -1);
            }
            else if (an == AnalysisType.X)
            {
                input.Saccades = saccadeMngr.DoSaccade(data, parms.SaccadeVelThresh, parms.PctPeak, parms.SaccadeWindow, parms.SaccadeMinDuration, true, Item.POSITIONX);

            }
            else if (an == AnalysisType.Y)
            {
                input.Saccades = saccadeMngr.DoSaccade(data, parms.SaccadeVelThresh, parms.PctPeak, parms.SaccadeWindow, parms.SaccadeMinDuration, true, Item.POSITIONY);
            }
        }

        public void ImportSaccade(DataList input, String fileName)
        {

            DataTable table = CSVReader.ReadCSVFile(fileName, true);
            foreach (DataRow row in table.Rows)
            {
                Saccade sac = SaccadeManager.MakeSaccade(input, Int32.Parse(row["idxStart"].ToString()), Int32.Parse(row["idxEnd"].ToString()), -1);
                if (sac != null) input.Saccades.List.Add(sac);
            }
        }

        public void FindFixation(DataList input, FixationParams parms)
        {
            DataList data = input;
            double maxDispersion = parms.FixationMaxDispersion;
            if (input.Type == DataObject.DataType.Pixels)
            {
                maxDispersion *= (double)(parms.PixelsPerDegreeX + parms.PixelsPerDegreeY) /2.0;
            }
            else if (input.Type == DataObject.DataType.Tick)
            {
                maxDispersion *= (double)(parms.TicksPerDegree);
            }
            input.Fixations = fixationMngr.DoFixation(input, maxDispersion, parms.FixationMinDuration, true, parms.AdjustBorder);
        }

        public void ImportFixation(DataList input, String fileName)
        {

            DataTable table = CSVReader.ReadCSVFile(fileName, true);
            foreach (DataRow row in table.Rows)
            {
                Fixation fix = FixationManager.MakeFixation(input, Int32.Parse(row["idxStart"].ToString()), Int32.Parse(row["idxEnd"].ToString()));
                if (fix!=null) input.Fixations.List.Add(fix);
            }
        }


        public void FindROITransitionInSaccade(ref DataList datas, ROIList roiList)
        {
            foreach (Saccade s in datas.Saccades.List)
            {
                Item item = datas.List[s.IdxStart];
                ROI r = roiList.FindNearestROI(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY]);
                if (r.Distance(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY]) <= 0)
                {
                    s.ROIStart = r;
                }
                item = datas.List[s.IdxEnd];
                r = roiList.FindNearestROI(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY]);
                if (r.Distance(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY]) <= 0)
                {
                    s.ROIEnd = r;
                }
            }
        }

        public void FindNearestROIFromFixations(ref DataList datas, ROIList roiList)
        {
            foreach (Fixation f in datas.Fixations.List)
            {
                f.NearestROI = roiList.FindNearestROI(f.X, f.Y);
            }
        }

        public void AnalyzeROIs(DataList datas, ROIList roiList)
        {
            IndicatorList indicators = roiMngr.AnalyzeROIs(datas, roiList);
            datas.Indicators = indicators;
        }

        public void PurgeAnalysis(DataList datas)
        {
            datas.Saccades.List.RemoveRange(0, datas.Saccades.List.Count);
            datas.WaveForms.List.RemoveRange(0, datas.WaveForms.List.Count);
            datas.Fixations.List.RemoveRange(0, datas.Fixations.List.Count);
            datas.Indicators.List.RemoveRange(0, datas.Indicators.List.Count);
        }

        private Trial FindTrial(DataList dataList)
        {
            foreach (Trial trial in trials)
            {
                foreach (DataList list in trial.Data)
                {
                    if (list.GetHashCode() == dataList.GetHashCode())
                    {
                        return trial;
                    }
                }
            }
            return null;
        }

        public void ReferenceDifference(DataList data)
        {

            IndicatorList indicators = TaskManager.ReferenceDifference(data);
            data.Indicators = indicators; 
        }

        public void SplitByReference(DataList data)
        {
            Trial trial = new Trial();

            trial.Name = data.Name + " splitted";

            trial.Data = TaskManager.SplitByTrial(data);
            trials.Add(trial);
        }

        public void AlignData(DataList input, ROIList rois)
        {
            Trial trial = FindTrial(input);
            DataList data = roiMngr.Calibrate(input, rois, -100, 100, -100, 100, 0.9, 1.5);
            if (data != null) trial.Data.Add(data);
        }



        #region Save Project

        public void NewAnalysis()
        {
            this.trials = new List<Trial>();
        }

        public void OpenAnalysis(Stream stream)
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            this.trials = (List<Trial>)bformatter.Deserialize(stream);
            stream.Close();
        }

        public void SaveAnalysis(Stream stream)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, this.trials);
            stream.Close();
        }

        #endregion

    }
}
