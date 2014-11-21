using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using EVALab.Util.Box;
using EVALab.Analysis.Filter;
using System.Collections;

namespace EVALab.Analysis.Data
{
    public class DataManager
    {
        public DataList ImportXMLDataASL(StreamReader readFile, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            XPathDocument doc = new XPathDocument(readFile);
            XPathNavigator nav = doc.CreateNavigator();

            DataList list = null;
            try
            {
                string id = GetXPathNode(nav, "//settings[@id]","id");
                string name = GetXPathNode(nav, "//settings/subject/name");
                string surname = GetXPathNode(nav, "//settings/subject/surname");
                string type = GetXPathNode(nav, "//settings/experiment/name");

                string data = GetXPathNode(nav, "/experiment/data");

                list = ImportRawDataASL(new StreamReader( new System.IO.MemoryStream(
                                        System.Text.Encoding.UTF8.GetBytes(data))), maxX, maxY, scaleX, scaleY, type);

                list.Name = name + " " + surname + " " + id;
                //list = AdjustReferenceByType(type, list, maxX, maxY, scaleX, scaleY);
            }
            catch (Exception ex)
            {
                ExceptionForm.Show(null, "Error on parsing file", ex);
                return null;
            }

            return list;
        }

        public DataList ImportRawDataASL(StreamReader readFile, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            return ImportRawDataASL(readFile, maxX, maxY, scaleX, scaleY, "RAW");
        }

        /// <summary>
        /// Import Dta from ASL system (CSV)
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public DataList ImportRawDataASL(StreamReader readFile, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY, string adjustReferenceByType)
        {
            DataList parsedData = new DataList("Data of ");

            string line;
            string[] row;

            while ((line = readFile.ReadLine()) != null)
            {
                line = line.Replace(',', '.');
                row = line.Split(' ');
                double[] d = new double[6];
                int idx = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Trim().Length > 0)
                    {
                        d[idx++] = Double.Parse(row[i],NumberStyles.Float,CultureInfo.InvariantCulture);
                        if (idx >= d.Length) break;
                    }
                }
                if ((d[0] > 0) && (d[1] > 0) && (d[2] > 0) && (d[0] <= (double)maxX) && (d[1] <= (double)maxY))
                {
                    if (scaleX > 0) d[0] = d[0] * (double)scaleX / (double)maxX;
                    if (scaleY > 0) d[1] = d[1] * (double)scaleY / (double)maxY;
                    Item item = new Item(d[0], d[1], d[2], d[4], d[5], (long)(d[3]));
                    parsedData.List.Add(item);
                }
            }

            return AdjustReferenceByType(adjustReferenceByType, parsedData, maxX, maxY, scaleX, scaleY);
        }

        /// <summary>
        /// Import Dta from ASL system (CSV)
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public void ExportRawDataASL(StreamWriter writerFile, DataList input, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {

            DataList data = AdjustReferenceByScale(input, maxX, maxY, scaleX, scaleY);
            foreach (Item t in data.List)
            {
                writerFile.WriteLine(t.ToString().Replace(',', '.'));
            }
         }

        private DataList AdjustReferenceByType(string type, DataList list, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            if (type.Equals("T"))
            {
                return AdjustReferenceByTheeuwes(list, maxX, maxY, scaleX, scaleY);
            }
            else if (type.Equals("GC"))
            {
                return AdjustReferenceByGC(list, maxX, maxY, scaleX, scaleY);
            }
            else if (type.Equals("ROI"))
            {
                return AdjustReferenceByGC(list, maxX, maxY, scaleX, scaleY);
            }
            else
            {
                return AdjustReferenceByScale(list, maxX, maxY, scaleX, scaleY);
            }
        }

        private DataList AdjustReferenceByGC(DataList list, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            foreach (Item item in list.List)
            {
                item.Reference = new double[] {0,0};
            }

            return list;
        }

        private DataList AdjustReferenceByScale(DataList list, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            foreach (Item item in list.List)
            {
                if (scaleX > 0) item.Reference[0] = item.Reference[0] * (double)scaleX / (double)maxX;
                if (scaleY > 0) item.Reference[1] = item.Reference[1] * (double)scaleY / (double)maxY;
            }

            return list;
        }

        private DataList AdjustReferenceByTheeuwes(DataList list, decimal maxX, decimal maxY, decimal scaleX, decimal scaleY)
        {
            double ray = 265;
            double centerX = 1024/2;
            double centerY = 768/2;
            foreach (Item item in list.List)
            {
                double[] references = item.Reference;
                double[] newReference = { centerX, centerY};
                double[] distracter = { centerX, centerY };
                int value = (int)references[0];
                int onPosition = (int)(value);
                
                double position = -1;
                for (int i = 1; i < 9; i++)
                {
                    if ((onPosition & (1 << i)) != 0)
                    {
                        position = i -1;
                        break;
                    }
                }
                if (position >= 0)
                {
                    newReference[0] = centerX + ray * Math.Cos(2 * Math.PI / 6.0 * position);
                    newReference[1] = centerY + ray * Math.Sin(2 * Math.PI / 6.0 * position);
                }

                position = -1;
                int abruptPosition = (int)(value >> 9);
                for (int i = 0; i < 12; i++)
                {
                    if ((abruptPosition & (1 << i)) != 0)
                    {
                        position = i;
                        break;
                    }
                }
                Debug.WriteLineIf(onPosition > 1048576, "FOUND " + value + " is " + position);
                if (position >= 0)
                {
                    distracter[0] = centerX + ray * Math.Cos(2 * Math.PI / 12.0 * position);
                    distracter[1] = centerY + ray * Math.Sin(2 * Math.PI / 12.0 * position);
                }
                item.Reference = newReference;
                item.Distracter = distracter;
            }

            return list;
        }

        /// <summary>
        /// Import data from not calibrated data
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="nCalibration"></param>
        /// <param name="blackReference"></param>
        /// <returns></returns>
        public DataList[] ImportXMLDataEOG(StreamReader readFile, int nCalibration, int blackReference)
        {
            XPathDocument doc = new XPathDocument(readFile);
            XPathNavigator nav = doc.CreateNavigator();
            try
            {
                string id = GetXPathNode(nav, "//settings[@id]","id");
                string width = GetXPathNode(nav, "//experiment/width");
                string distance = GetXPathNode(nav, "//experiment/distance");
                string maxCh1 = GetXPathNode(nav, "//calibration/ch1[@max]", "max");
                string minCh1 = GetXPathNode(nav, "//calibration/ch1[@min]", "min");
                string maxCh2 = GetXPathNode(nav, "//calibration/ch2[@max]", "max");
                string minCh2 = GetXPathNode(nav, "//calibration/ch2[@min]", "min");
                string maxReference = GetXPathNode(nav, "//calibration/reference[@max]", "max");
                string minReference = GetXPathNode(nav, "//calibration/reference[@min]", "min");

                string data = GetXPathNode(nav, "/experiment/data");

                int minReferenceInt = Int32.Parse(minReference);
                int maxReferenceInt = Int32.Parse(maxReference);
                double widthDouble = Double.Parse(width);
                double distanceDouble = Int32.Parse(distance);

                DataList[] list = ParseDataEOG(new StreamReader( new System.IO.MemoryStream(
                                    System.Text.Encoding.UTF8.GetBytes( data)) ),
                                    minReferenceInt,
                                    maxReferenceInt,
                                    blackReference,
                                    Double.Parse(maxCh1),
                                    Double.Parse(minCh1),
                                    Double.Parse(maxCh2),
                                    Double.Parse(minCh2));

                double th = 360.0 * Math.Atan((widthDouble / 2.0) / distanceDouble) / Math.PI;
                double tickPerDegree = (double)(maxReferenceInt - minReferenceInt) / th;



                for (int i = 0; i < list.Length; i++)
                {
                    list[i].Name = list[i].Name + " " + id;
                    list[i] = FilterManager.Convert2Degree(list[i], tickPerDegree);
                }
                return list;
            }
            catch (Exception ex)
            {
                ExceptionForm.Show(null,"Error on parsing file", ex);
                return null;
            }
        }

        public static string GetXPathNode(XPathNavigator nav, string xpath)
        {
            return GetXPathNode(nav, xpath, null);
        }

        public static string GetXPathNode(XPathNavigator nav, string xpath, string attribute)
        {
            // Compile a standard XPath expression

            XPathExpression expr;
            expr = nav.Compile(xpath);
            XPathNodeIterator iterator = nav.Select(expr);

            // Iterate on the node set
            while (iterator.MoveNext())
            {
                
                XPathNavigator nav2 = iterator.Current.Clone();
                if (attribute != null) return nav2.GetAttribute(attribute, nav2.BaseURI);
                return iterator.Current.Value;
            }

            return null;
        }

        public static Hashtable GetXPathNodes(XPathNavigator nav, string xpath, string attribute)
        {
            // Compile a standard XPath expression
            Hashtable map = new Hashtable();
            XPathExpression expr;
            expr = nav.Compile(xpath);
            XPathNodeIterator iterator = nav.Select(expr);

            // Iterate on the node set
            while (iterator.MoveNext())
            {

                XPathNavigator nav2 = iterator.Current.Clone();
                Debug.WriteLine( nav2.HasAttributes );
                string key = nav2.GetAttribute(attribute, nav2.BaseURI);
                string value = iterator.Current.Value;
                map.Add(key, value);
            }

            return map;
        }

        /// <summary>
        /// Import data from not calibrated data
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="nCalibration"></param>
        /// <param name="blackReference"></param>
        /// <returns></returns>
        public DataList[] ImportRawDataEOG(StreamReader readFile, int nCalibration, int blackReference)
        {
            DataList parsedDataDx = new DataList("Calibration EYEDX", new string[] { "X", "Y" }, DataList.DataType.Unknown);
            DataList parsedDataSx = new DataList("Calibration EYESX", new string[] { "X", "Y" }, DataList.DataType.Unknown);
            
            string line;
            string[] row;

            int lastReference = -9999;
            int countCalibration = 0;

            int[] calibrationIdx = new int[nCalibration + 1];

            calibrationIdx[0] = 0; //the start
            int idxCalibration = 1;

            int minReference = Int32.MaxValue; 
            int maxReference = 0; 

            //read data for calibration
            while ((line = readFile.ReadLine()) != null)
            {
                if (countCalibration >= nCalibration) break;
                line.Replace(',', '.');
                row = line.Split(' ');
                double[] d = new double[6];
                int idx = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Trim().Length > 0)
                    {
                        d[idx++] = Double.Parse(row[i]
                        ,
NumberStyles.Float,
CultureInfo.InvariantCulture);
                        if (idx >= d.Length) break;
                    }
                }

                maxReference = (int)Math.Max(maxReference, d[4]);
                minReference = (int)Math.Min(minReference, d[4]);

                if (lastReference != d[4])
                {
                    calibrationIdx[countCalibration] = idxCalibration;
                    countCalibration++;
                }
                lastReference = (int)d[4];
                

                Item itemSx = new Item(d[0], 0, 0, d[4], d[4], (long)(d[2]));
                Item itemDx = new Item(d[1], 0, 0, d[4], d[4], (long)(d[2]));
                parsedDataSx.List.Add(itemSx);
                parsedDataDx.List.Add(itemDx);

                idxCalibration++;
            }


            double nMax = 0;
            double nMin = 0;
            double maxEyeSx = 0;
            double minEyeSx = 0;
            double maxEyeDx = 0;
            double minEyeDx = 0;
            for (int i = 0; i < calibrationIdx.Length - 1; i++)
            {
                int idx = (int)Math.Round(((double)(calibrationIdx[i] + calibrationIdx[i + 1])) / 2.0);
                Debug.WriteLine(calibrationIdx[i] +" "+ calibrationIdx[i + 1] + " " + idx);
                for (int j = (int)Math.Max(calibrationIdx[i], idx - 10); (j < idx + 10) && (j < calibrationIdx[i + 1]); j++)
                {
                    Item itemSx = parsedDataSx.List[j];
                    Item itemDx = parsedDataDx.List[j];
                    if (itemSx.Reference[0] == minReference)
                    {
                        minEyeSx += itemSx.Value[0];
                        minEyeDx += itemDx.Value[0];
                        nMin++;
                    }
                    else if (itemSx.Reference[0] == maxReference)
                    {
                        maxEyeSx += itemSx.Value[0];
                        maxEyeDx += itemDx.Value[0];
                        nMax++;
                    }
                }
            }

            if (nMax > 0)
            {
                maxEyeSx /= nMax;
                maxEyeDx /= nMax;
            }
            else
            {
                maxEyeSx = 1;
                maxEyeDx = 1;
                maxReference=1;
            }

            if (nMin > 0)
            {
                minEyeSx /= nMin;
                minEyeDx /= nMin;
            }
            else
            {
                minEyeSx = 0;
                minEyeDx = 0;
                minReference = 0;
            }

            //read data
            return ParseDataEOG(readFile,
             minReference,
             maxReference,
             blackReference,
             maxEyeSx,
             minEyeSx,
             maxEyeDx,
             minEyeDx);
        }

        /// <summary>
        /// Parse remaining data
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="minReference"></param>
        /// <param name="maxReference"></param>
        /// <param name="blackReference"></param>
        /// <param name="maxEyeSx"></param>
        /// <param name="minEyeSx"></param>
        /// <param name="maxEyeDx"></param>
        /// <param name="minEyeDx"></param>
        /// <returns></returns>
        private DataList[] ParseDataEOG(StreamReader readFile, 
            int minReference, 
            int maxReference, 
            int blackReference,             
            double maxEyeSx,
            double minEyeSx,
            double maxEyeDx,
            double minEyeDx)
        {
            //read data
            //look for next data
            DataList parsedDataSx = new DataList("Data EYESX", new string[] { "X", "Y" }, DataList.DataType.Tick);
            DataList parsedDataDx = new DataList("Data EYEDX", new string[] { "X", "Y" }, DataList.DataType.Tick);

            string line;
            string[] row;
            while ((line = readFile.ReadLine()) != null)
            {
                row = line.Split(' ');
                double[] d = new double[6];
                int idx = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Trim().Length > 0)
                    {
                        d[idx++] = Double.Parse(row[i]);
                        if (idx >= d.Length) break;
                    }
                }

                //Adjust reference
                //double referenceSx = (maxEyeSx - minEyeSx) * (d[4] - minReference) / (double)(maxReference - minReference) + minEyeSx;
                //double referenceDx = (maxEyeDx - minEyeDx) * (d[4] - minReference) / (double)(maxReference - minReference) + minEyeDx;
                double vSx = (double)(maxReference - minReference) * (d[0] - minEyeSx) / (double)(maxEyeSx - minEyeSx) + minReference;
                double vDx = (double)(maxReference - minReference) * (d[1] - minEyeDx) / (double)(maxEyeDx - minEyeDx) + minReference;

                Item itemSx = new Item(vSx, 0, 0, d[4], d[4], (long)(d[2]));
                parsedDataSx.List.Add(itemSx);
                Item itemDx = new Item(vDx, 0, 0, d[4], d[4], (long)(d[2]));
                parsedDataDx.List.Add(itemDx);
            }

            return new DataList[] { parsedDataSx, parsedDataDx };
        }

    }
}
