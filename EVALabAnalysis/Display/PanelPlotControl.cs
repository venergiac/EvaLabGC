using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EVALab.Analysis.Saccade;
using EVALab.Analysis.Fixation;
using EVALab.Analysis.Data;
using ZedGraph;
using System.Diagnostics;
using RKLib.ExportData;
using EVALab.Analysis.WaveForm;
using System.IO;
using EVALab.Analysis.ROI;
using EVALabAnalysis.Dialog;
using EVALab.Analysis.Indicator;
using EVALab.Util.IO;
using EVALabAnalysis.Parameter;
using EVALab.Util.View.DataGridViewNumericUpDownElements;

namespace EVALabAnalysis.Display
{
    public partial class PanelPlotControl : UserControl
    {

        private enum PlotType
        {
            Time, XY
        }

        private const string markerPrefix = "User Marker ";
        private const string saccadePrefix = "Saccade ";
        private const string fixationPrefix = "Fixation ";
        private const string waveFormPrefix = "Special Waveform ";
        private const string indicatorPrefix = "Indicator ";
        private PlotType type = PlotType.XY;

        private Image image = null;

        private DataList list = null;

        private Color colorMarker = Color.Green;

        private ROIList roiList = null;

        private Color colorDataX = Color.Blue;
        private Color colorDataY = Color.Green;
        private Color colorRefX = Color.LightBlue;
        private Color colorRefY = Color.LightGreen;
        private Color colorDistracterX = Color.Fuchsia;
        private Color colorDistracterY = Color.Fuchsia;

        public PanelPlotControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Data Analysis
        /// </summary>
        /// <param name="list"></param>
        #region Set Data and Analysis
        public void SetDataList(ref DataList list)
        {
            if (type == PlotType.XY)
            {
                SetDataListXY(ref list);
            }
            else
            {
                SetDataListVSTime(ref list);
            }
        }

        public void SetDataListXY(ref DataList list)
        {
            if (list == null) return;
            this.list = list;
            type = PlotType.XY;
            toolStripButton3.Enabled = true;

            toolStripComboBox1.Items.Clear();
            ResetEdit();

            // Get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.Clear();
            toolStripButtonShowROI.Enabled = true;
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;

            myPane.XAxis.Scale.Min = 1;
            myPane.XAxis.Scale.Max = (int)Properties.Settings.Default.PixelX;
            myPane.YAxis.Scale.IsReverse = true;
            myPane.YAxis.Scale.Min = 1;
            myPane.YAxis.Scale.Max = (int)Properties.Settings.Default.PixelY;

            myPane.XAxis.Scale.MinAuto = false;
            myPane.XAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.MinAuto = false;
            myPane.YAxis.Scale.MaxAuto = false;

            myPane.XAxis.Title.Text = "X" + list.Type;
            myPane.YAxis.Title.Text = "Y" + list.Type;

            myPane.Title.Text = list.Name;

            PointPairList spl = new PointPairList();
            PointPairList splRef = new PointPairList();
            PointPairList splDis = null;
            //Plot data
            foreach (Item item in list.List)
            {
                if ((item.Value != null) && (item.Value.Length > 1))
                {
                    spl.Add(item.Value[Item.POSITIONX], item.Value[Item.POSITIONY], item.Time / 1000.0);
                }

                if ((item.Reference != null) && (item.Reference.Length > 1))
                {
                    splRef.Add(item.Reference[Item.POSITIONX], item.Reference[Item.POSITIONY], item.Time / 1000.0);
                }

                if ((item.Distracter != null) && (item.Distracter.Length > 1))
                {
                    if (splDis == null) splDis = new PointPairList();
                    splDis.Add(item.Distracter[Item.POSITIONX], item.Distracter[Item.POSITIONY], item.Time / 1000.0);
                }
            }

            int id = toolStripComboBox1.Items.Add("All");
            toolStripComboBox1.Select(id, 1);
            //set max 
            trackBar1.Maximum = spl.Count;
            numericUpDown2.Maximum = spl.Count;
            trackBar1.Value = 0;
            numericUpDown2.Value = 0;

            toolStripComboBox1.Items.Add("Data & Reference");
            myPane.AddCurve("Reference", splRef, colorRefX, SymbolType.Square);
            if (splDis != null)
            {
                myPane.AddCurve("Distracter", splDis, colorDistracterX, SymbolType.Triangle);
                toolStripComboBox1.Items.Add("Data & Distracter");
            }
            toolStripComboBox1.Items.Add("Data");
            LineItem curve = myPane.AddCurve("Data", spl, colorDataX, SymbolType.None);
            curve.Tag = list; //marked as data curve


            //add custom label
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            UpdateAnalysis();
        }

        public void SetDataListVSTime(ref DataList list)
        {
            if (list == null) return;
            this.list = list;
            type = PlotType.Time;
            toolStripButton3.Enabled = false;
            toolStripComboBox1.Items.Clear();
            ResetEdit();

            // Get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.Clear();
            toolStripButtonShowROI.Enabled = false;
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.Title.Text = "Second";
            myPane.YAxis.Title.Text = "" + list.Type;

            myPane.Title.Text = list.Name;

            PointPairList splX = new PointPairList();
            PointPairList splRefX = new PointPairList();
            PointPairList splDisX = null;

            PointPairList splY = null;
            PointPairList splRefY = null;
            PointPairList splDisY = null;

            //Plot data
            foreach (Item item in list.List)
            {
                //X
                splX.Add(item.Time / 1000.0, item.Value[Item.POSITIONX]);
                splRefX.Add(item.Time / 1000.0, item.Reference[Item.POSITIONX]);
                if (item.Distracter != null)
                {
                    if (splDisX == null) splDisX = new PointPairList();
                    splDisX.Add(item.Time / 1000.0, item.Distracter[Item.POSITIONX]);
                }
                //Y
                if (item.CountValue > 1)
                {
                    if (splY == null) splY = new PointPairList();
                    if (splRefY == null) splRefY = new PointPairList();
                    splY.Add(item.Time / 1000.0, item.Value[Item.POSITIONY]);
                    splRefY.Add(item.Time / 1000.0, item.Reference[Item.POSITIONY]);
                    if (item.Distracter != null)
                    {
                        if (splDisY == null) splDisY = new PointPairList();
                        splDisY.Add(item.Time / 1000.0, item.Distracter[Item.POSITIONY]);
                    }
                }
            }


            
            int id = toolStripComboBox1.Items.Add("All");
            toolStripComboBox1.Select(id, 1);
            toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONX));
            LineItem curve = myPane.AddCurve("Data " + list.GetLabel(Item.POSITIONX), splX, colorDataX, SymbolType.None);
            
            //set max 
            trackBar1.Maximum = splX.Count;
            numericUpDown2.Maximum = splX.Count;
            trackBar1.Value = 0;
            numericUpDown2.Value = 0;
            
            curve.Tag = list; //marked as data curve
            toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONX) + " & Reference " + list.GetLabel(Item.POSITIONX));
            myPane.AddCurve("Reference " + list.GetLabel(Item.POSITIONX), splRefX, colorRefX, SymbolType.None);
            if (splDisX != null)
            {
                myPane.AddCurve("Distracter " + list.GetLabel(Item.POSITIONX), splDisX, colorDistracterX, SymbolType.None);
                toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONX) + " & Distracter " + list.GetLabel(Item.POSITIONX));
            }

            if (splY != null)
            {
                toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONY));
                curve = myPane.AddCurve("Data " + list.GetLabel(Item.POSITIONY), splY, colorDataY, SymbolType.None);
                curve.Tag = list; //marked as data curve
                toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONY) + " & Reference " + list.GetLabel(Item.POSITIONY));
                myPane.AddCurve("Reference " + list.GetLabel(Item.POSITIONY), splRefY, colorRefY, SymbolType.None);
                if (splDisY != null)
                {
                    myPane.AddCurve("Distracter " + list.GetLabel(Item.POSITIONY), splDisY, colorDistracterY, SymbolType.None);
                    toolStripComboBox1.Items.Add("Data " + list.GetLabel(Item.POSITIONY) + " & Distracter " + list.GetLabel(Item.POSITIONY));
                } 
                
            }

            //add custom label
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            UpdateAnalysis();
        }

        private void UpdateAnalysis()
        {
            SetSaccadeList(list.Saccades);
            SetWaveFormList(list.WaveForms);
            SetFixationList(list.Fixations);
            SetIndicatorList(list.Indicators);
        }

        private void SetSaccadeList(SaccadeList list)
        {

            DataTable table = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "valid";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxStart";
            column.Caption = "This column is for System purpose";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxEnd";
            column.Caption = "This column is for System purpose";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "amplitude";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "velocity mean";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "velocity peak";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "latency";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "error";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "duration";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "gain";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "area";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "number crox";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "max curvature";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "overall direction error";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "from roi";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "to roi";
            table.Columns.Add(column);

            
            // Create new DataRow objects and add to DataTable.
            int i = 0;
            foreach (Saccade item in list.List)
            {
                i++;
                Debug.WriteLine(" " + i);
                row = table.NewRow();
                row["valid"] = true;
                row["id"] = i;
                row["idxStart"] = item.IdxStart;
                row["idxEnd"] = item.IdxEnd;
                row["amplitude"] = item.Amplitude;
                row["velocity mean"] = item.VMean;
                row["velocity peak"] = item.VPeak;
                row["latency"] = item.Latency;
                row["error"] = item.Error;
                row["duration"] = item.Duration;
                row["gain"] = item.Gain;
                row["area"] = item.Area;
                row["number crox"] = item.NumberCrox;
                row["max curvature"] = item.MaxCurvature;
                row["overall direction error"] = item.OverallAngle;

                if (item.ROIStart != null)
                {
                    row["from roi"] = item.ROIStart.Id;
                }
                if (item.ROIEnd != null)
                {
                    row["to roi"] = item.ROIEnd.Id;
                }
                
                table.Rows.Add(row);
            }

            int idxScrollBar = dataGridSaccades.FirstDisplayedScrollingRowIndex;

            dataGridSaccades.DataSource = table;

            int ii = 0;
            foreach (DataGridViewColumn col in dataGridSaccades.Columns)
            {
                
                col.ReadOnly = (ii++ > 0);
                col.Frozen = true;

            }

            
            if ((idxScrollBar < dataGridSaccades.RowCount) && (idxScrollBar>0))
            {
                dataGridSaccades.FirstDisplayedScrollingRowIndex = idxScrollBar;
            }
            dataGridSaccades.Invalidate();
        }

        private void SetWaveFormList(WaveFormList list)
        {

            DataTable table = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "type";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "valid";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);


            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxStart";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxEnd";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "amplitude";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "velocity mean";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "velocity peak";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "latency";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "error";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "duration";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "gain";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "area";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "number crox";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "max curvature";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "overall direction error";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "from roi";
            table.Columns.Add(column);


            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "to roi";
            table.Columns.Add(column);



            // Create new DataRow objects and add to DataTable.
            int i = 0;
            foreach (WaveForm item in list.List)
            {
                i++;
                Debug.WriteLine(" " + i);
                row = table.NewRow();
                row["type"] = item.Type.ToString();
                row["valid"] = true;
                row["id"] = i;
                row["idxStart"] = item.IdxStart;
                row["idxEnd"] = item.IdxEnd;
                row["amplitude"] = item.Amplitude;
                row["velocity mean"] = item.VMean;
                row["velocity peak"] = item.VPeak;
                row["latency"] = item.Latency;
                row["error"] = item.Error;
                row["duration"] = item.Duration;
                row["gain"] = item.Gain;
                row["area"] = item.Area;
                row["number crox"] = item.NumberCrox;
                row["max curvature"] = item.MaxCurvature;
                row["overall direction error"] = item.OverallAngle;
                if (item.ROIStart != null) row["from roi"] = item.ROIStart.Id;
                if (item.ROIEnd != null) row["to roi"] = item.ROIEnd.Id;

                table.Rows.Add(row);
            }

            dataGridViewWaveForm.DataSource = table;

            int ii = 0;
            foreach (DataGridViewColumn col in dataGridViewWaveForm.Columns)
            {
                col.ReadOnly = (ii++ != 1);
                col.Frozen = true;
            }

            dataGridViewWaveForm.Invalidate();
        }

        private void SetFixationList(FixationList list)
        {

            DataTable table = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "valid";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);


            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxStart";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxEnd";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);
            

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "x";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "y";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "dispersion";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "duration";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "error";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "nearest roi";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "distance from nearest roi";
            table.Columns.Add(column);



            // Create new DataRow objects and add to DataTable.
            int i = 0;
            foreach (Fixation item in list.List)
            {
                i++;
                row = table.NewRow();
                row["valid"] = true;
                row["id"] = i;
                row["idxStart"] = item.IdxStart;
                row["idxEnd"] = item.IdxEnd;
                row["x"] = item.X;
                row["y"] = item.Y;
                row["dispersion"] = item.Dispersion;
                row["duration"] = item.Duration;
                row["error"] = item.Error;
                if (item.NearestROI != null)
                {
                    row["nearest roi"] = item.NearestROI.Id;
                    row["distance from nearest roi"] = item.NearestROI.Distance(item.X, item.Y);
                }
                table.Rows.Add(row);
            }

            int idxScrollBar = dataGridFixations.FirstDisplayedScrollingRowIndex;
            dataGridFixations.DataSource = table;

            int ii = 0;
            foreach (DataGridViewColumn col in dataGridSaccades.Columns)
            {
                col.ReadOnly = (ii++ > 0);
                col.Frozen = true;
            }
            if ((idxScrollBar < dataGridFixations.RowCount) && (idxScrollBar > 0))
            {
                dataGridFixations.FirstDisplayedScrollingRowIndex = idxScrollBar;
            }
            dataGridFixations.Invalidate();
        }

        private void SetIndicatorList(IndicatorList list)
        {
            DataTable table = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "family";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxStart";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idxEnd";
            column.Caption = "This column is fo System purpose";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "value";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "variance";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "unit";
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "reference";
            table.Columns.Add(column);

            int i = 0;
            foreach (Indicator item in list.List)
            {
                i++;
                row = table.NewRow();
                row["id"] = i;
                row["family"] = item.Family;
                row["name"] = item.Name;
                row["idxStart"] = item.IdxStart;
                row["idxEnd"] = item.IdxEnd;
                row["value"] = item.Value;
                row["variance"] = item.Variance;
                row["unit"] = item.UnitName;
                row["reference"] = item.Reference;
                table.Rows.Add(row);
            }

            dataGridIndicators.DataSource = table;

            int ii = 0;
            foreach (DataGridViewColumn col in dataGridIndicators.Columns)
            {
                col.ReadOnly = true;
                col.Frozen = true;
            }

            dataGridIndicators.Invalidate();
        }
        #endregion


        /// <summary>
        /// Push image on background
        /// </summary>
        /// <param name="file"></param>
        private void SetBackground(string file)
        {
            // Get an image for the background (use your own filename here)
            image = Bitmap.FromFile(@file);

            SetBackground(0, 0, (int)Properties.Settings.Default.PixelX, (int)Properties.Settings.Default.PixelY);
        }

        private void SetBackground(float x, float y, float width, float height)
        {
            //image = image.GetThumbnailImage(width, height, null, IntPtr.Zero);

            // Fill the pane background with the image
            TextureBrush texBrush = null;

            if ((x >= 0) && (y >= 0) && (width <= image.Width) && (height <= image.Height))
            {
                texBrush = new TextureBrush(image, new RectangleF(x, y, width, height));
            }
            else
            {
                //SCALE
            }

            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Chart.Fill = new Fill(texBrush, true);

            // Turn off the axis background fill
            myPane.Chart.Fill.IsVisible = true;

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane,
                        CurveItem curve, int iPt)
        {
            string l = "";
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];
            double velocity = 0;
            if (iPt > 0)
            {
                PointPair pt0 = curve[iPt - 1];
                velocity = (pt.Y - pt0.Y) / (pt.X - pt0.X);
            }
            if (type == PlotType.Time)
            {
                l =  curve.Label.Text + " is " + pt.Y.ToString("f2") + " at " + pt.X.ToString("f2") + " sec velocity=" + velocity.ToString("f2");
            }
            else
            {
                string r = WhichROI(pt.X, pt.Y);
                l = curve.Label.Text + " X=" + pt.X.ToString("f2") + " Y=" + pt.Y.ToString("f2") + " velocity=" + velocity.ToString("f2");
                if (r != null) l+= " " + r;
            }
            return l;
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            // show the horizontal scroll bar
            zedGraphControl1.IsShowHScrollBar = true;

            // automatically set the scrollable range to cover the data range from the curves
            zedGraphControl1.IsAutoScrollRange = true;

            // Horizontal pan and zoom allowed
            zedGraphControl1.IsEnableHPan = true;
            zedGraphControl1.IsEnableHZoom = true;

            // Vertical pan and zoom not allowed
            zedGraphControl1.IsEnableVPan = true;
            zedGraphControl1.IsEnableVZoom = true;
        }

        private void toolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButton1.Checked)
            {
                zedGraphControl1.ZoomButtons = MouseButtons.Left;
            }
            else
            {
                zedGraphControl1.ZoomButtons = MouseButtons.None;
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            string selection = (string)toolStripComboBox1.SelectedItem;
            if (selection == null) return;
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList list = myPane.CurveList;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].IsVisible = (selection.Equals("All") || selection.IndexOf(list[i].Label.Text) >= 0 || list[i].Label.Text.IndexOf(selection) >= 0);
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar1.Value;
            MoveCurveMarker();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown2.Value;
            MoveCurveMarker();
        }

        private void MoveCurveMarker()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList list = myPane.CurveList;
            double value = (double)trackBar1.Value / (double)trackBar1.Maximum;
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Tag == this.list) && (list[i].IsVisible))
                {
                    AddCurveMarker(markerPrefix + list[i].Label.Text, list[i], (int)(value * (double)(list[i].NPts)), numericUpDown1.Value, colorMarker);
                }
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void RemoveCurveMarker(string name, CurveItem itemSource)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList list = myPane.CurveList;
            CurveItem item = list[name];
            if (item != null)
            {
                list.Remove(item);
            }
        }

        private void AddCurveMarker(string name, CurveItem itemSource, int idxStart, decimal size, Color color)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList list = myPane.CurveList;
            CurveItem item = list[name];
            if (item == null)
            {
                item = myPane.AddCurve(name, new PointPairList(), color, SymbolType.None);
                list.Remove(item);
                list.Insert(0, item);
            }
            item.Clear();

            double hMin = Double.MaxValue;
            double hMax = 0;
            double vMin = Double.MaxValue;
            double vMax = 0;
            double timeMin = Double.MaxValue;
            double timeMax = 0;
            Debug.WriteLine(""+idxStart + " " + (idxStart + size));
            for (int i = idxStart; (i < idxStart + size) && (i < itemSource.Points.Count) && (i<this.list.List.Count); i++)
            {

                hMin = Math.Min(hMin, this.list.List[i].Value[Item.POSITIONX]);
                hMax = Math.Max(hMax, this.list.List[i].Value[Item.POSITIONX]);
                if (Item.POSITIONY<=this.list.List[i].Value.Length)
                {
                vMin = Math.Min(vMin, this.list.List[i].Value[Item.POSITIONY]);
                vMax = Math.Max(vMax, this.list.List[i].Value[Item.POSITIONY]);
                }
                timeMin = Math.Min(timeMin, this.list.List[i].Time);
                timeMax = Math.Max(timeMax, this.list.List[i].Time);
                item.AddPoint(itemSource[i]);
            }
            item.Color = color;

            //add BOX
            if (this.type == PlotType.Time)
            {
                AddBox(timeMin / 1000.0, 0, timeMax / 1000.0 - timeMin / 1000.0, 1, color);
            }

            textBoxHMax.Text = "" + hMax;
            textBoxHMin.Text = "" + hMin;
            textBoxVMin.Text = "" + vMin;
            textBoxVMax.Text = "" + vMax;

            textBoxVelocityX.Text = "" + (1000*(vMax - vMin) / (timeMax - timeMin));
            textBoxVelocityY.Text = "" + (1000 * (hMax - hMin) / (timeMax - timeMin));

            textBoxVelocityXY.Text = "" + (1000 * Math.Sqrt(Math.Pow(vMax - vMin, 2) + Math.Pow(hMax - hMin, 2)) / (timeMax - timeMin));


        }

        BoxObj box = null;
        private void AddBox(double x, double y, double w, double h, Color color)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            if (box != null)
            {
                myPane.GraphObjList.Remove(box);
            }

            // Draw a box item to highlight a value range
            box = new BoxObj(x, y, w, h, Color.Empty,
                    Color.FromArgb(200, color));
            box.Fill = new Fill(Color.FromArgb(100, color));
            // Use the BehindAxis zorder to draw the highlight beneath the grid lines
            box.ZOrder = ZOrder.A_InFront;
            // Make sure that the boxObj does not extend outside the chart rect if the chart is zoomed
            box.IsClippedToChartRect = true;
            // Use a hybrid coordinate system so the X axis always covers the full x range
            // from chart fraction 0.0 to 1.0
            box.Location.CoordinateFrame = CoordType.XScaleYChartFraction;

            myPane.GraphObjList.Add(box);
        }

        private void openFileDialogImage_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                this.SetBackground(openFileDialogImage.FileName);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            openFileDialogImage.ShowDialog(this);
        }

        #region Selection
        private void dataGridSaccades_SelectionChanged(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Saccades == null) || (list.Saccades.List == null) || (list.Saccades.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridSaccades.SelectedRows;

            //update edit button
            this.editSaccadeCurrentToolStripMenuItem1.Enabled = collections.Count == 1;
            
            foreach (DataGridViewRow row in collections)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if (idx >= list.Saccades.List.Count) continue;
                Saccade saccade = list.Saccades.List[idx];

                //draw saccade
                GraphPane myPane = zedGraphControl1.GraphPane;
                CurveList curveList = myPane.CurveList;
                for (int i = 0; i < curveList.Count; i++)
                {
                    if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                    {
                        AddCurveMarker(saccadePrefix + curveList[i].Label.Text, curveList[i], saccade.IdxStart, saccade.IdxEnd - saccade.IdxStart, Color.Red);
                    }
                }
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void dataGridFixations_SelectionChanged(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Fixations == null) || (list.Fixations.List == null) || (list.Fixations.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridFixations.SelectedRows;

            //update edit button
            this.editFixationCurrentToolStripMenuItem.Enabled = collections.Count == 1;
            
            foreach (DataGridViewRow row in collections)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if (idx >= list.Fixations.List.Count) continue;
                Fixation fixation = list.Fixations.List[idx];

                //draw fixation
                GraphPane myPane = zedGraphControl1.GraphPane;
                CurveList curveList = myPane.CurveList;
                for (int i = 0; i < curveList.Count; i++)
                {
                    if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                    {
                        AddCurveMarker(fixationPrefix + curveList[i].Label.Text, curveList[i], fixation.IdxStart, fixation.IdxEnd - fixation.IdxStart, Color.Fuchsia);
                    }
                }
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void dataGridViewWaveForm_SelectionChanged(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.WaveForms == null) || (list.WaveForms.List == null) || (list.WaveForms.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridViewWaveForm.SelectedRows;
            foreach (DataGridViewRow row in collections)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if (idx >= list.WaveForms.List.Count) continue;
                WaveForm wf = list.WaveForms.List[idx];

                //draw fixation
                GraphPane myPane = zedGraphControl1.GraphPane;
                CurveList curveList = myPane.CurveList;
                for (int i = 0; i < curveList.Count; i++)
                {
                    if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                    {
                        AddCurveMarker(waveFormPrefix + curveList[i].Label.Text, curveList[i], wf.IdxStart, wf.IdxEnd - wf.IdxStart, Color.Violet);
                    }
                }
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void dataGridIndicators_SelectionChanged(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Indicators == null) || (list.Indicators.List == null) || (list.Indicators.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridIndicators.SelectedRows;
            foreach (DataGridViewRow row in collections)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if (idx >= list.Indicators.List.Count) continue;
                Indicator wf = list.Indicators.List[idx];

                //draw fixation
                GraphPane myPane = zedGraphControl1.GraphPane;
                CurveList curveList = myPane.CurveList;
                for (int i = 0; i < curveList.Count; i++)
                {
                    if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                    {
                        AddCurveMarker(indicatorPrefix + curveList[i].Label.Text, curveList[i], wf.IdxStart, wf.IdxEnd - wf.IdxStart, Color.Orange);
                    }
                }
            }

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }


        #endregion

        private void zedGraphControl1_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if ((string)item.Tag == "set_default")
                {
                    // remove the menu item
                    item.Click += new EventHandler(item_Click);
                    break;
                }
            }

        }

        /// <summary>
        /// Overrided scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            SetToDefaultScale();
            //Redraw
            zedGraphControl1.Invalidate();
        }

        void SetToDefaultScale()
        {
            if (type == PlotType.XY)
            {
                GraphPane myPane = zedGraphControl1.GraphPane;
                myPane.XAxis.Scale.Min = 1;
                myPane.XAxis.Scale.Max = (int)Properties.Settings.Default.PixelX;
                myPane.YAxis.Scale.Min = 1;
                myPane.YAxis.Scale.Max = (int)Properties.Settings.Default.PixelY;
                zedGraphControl1.AxisChange();
            }
        }

        private void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            GraphPane myPane = sender.GraphPane;
            Debug.WriteLine(myPane.XAxis.Scale.Min + " " + myPane.XAxis.Scale.Max);

            if (image != null)
            {
                if (myPane.XAxis.Scale.Min < 0)
                {
                    myPane.XAxis.Scale.Min = 0;
                }
                if (myPane.YAxis.Scale.Min < 0)
                {
                    myPane.YAxis.Scale.Min = 0;
                }
                if (myPane.XAxis.Scale.Max > (int)Properties.Settings.Default.PixelX)
                {
                    myPane.XAxis.Scale.Max = (int)Properties.Settings.Default.PixelX;
                }
                if (myPane.YAxis.Scale.Max > (int)Properties.Settings.Default.PixelY)
                {
                    myPane.YAxis.Scale.Max = (int)Properties.Settings.Default.PixelY;
                }
                //Redraw
                zedGraphControl1.AxisChange();

                SetBackground((int)myPane.XAxis.Scale.Min, (int)myPane.YAxis.Scale.Min, (int)(myPane.XAxis.Scale.Max - myPane.XAxis.Scale.Min), (int)(myPane.YAxis.Scale.Max - myPane.YAxis.Scale.Min));
                //Redraw
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }

        #region Export
        private void saveFileDialogExport_FileOk(object sender, CancelEventArgs e)
        {

        }

        //saccadi
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "Excel (*.xls)|*.xls";
            saveFileDialogExport.FileName = "saccades.xls";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                /*Export objExport = new Export("Win");
                    objExport.ExportDetails((DataTable)this.dataGridSaccades.DataSource,
                     Export.ExportFormat.Excel, fileName);*/
                EVALab.Util.IO.ExcelExporter.DataTableToExcel((DataTable)this.dataGridSaccades.DataSource, "saccades", fileName);
            }
        }

        //fissazione
        private void excellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "Excel (*.xls)|*.xls";
            saveFileDialogExport.FileName = "fixations.xls";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                /*Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridFixations.DataSource,
                     Export.ExportFormat.Excel, fileName);*/
                EVALab.Util.IO.ExcelExporter.DataTableToExcel((DataTable)this.dataGridFixations.DataSource, "fixations", fileName);

            }
        }



        //indicators
        private void excelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "Excel (*.xls)|*.xls";
            saveFileDialogExport.FileName = "indicators.xls";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                /*Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridViewWaveForm.DataSource,
                     Export.ExportFormat.Excel, fileName);*/
                EVALab.Util.IO.ExcelExporter.DataTableToExcel((DataTable)this.dataGridIndicators.DataSource, "indicators", fileName);
            }
        }

        //waveforms
        private void excelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "Excel (*.xls)|*.xls";
            saveFileDialogExport.FileName = "waves.xls";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                /*Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridViewWaveForm.DataSource,
                     Export.ExportFormat.Excel, fileName);*/
                EVALab.Util.IO.ExcelExporter.DataTableToExcel((DataTable)this.dataGridViewWaveForm.DataSource, "waveform", fileName);
            }
        }

        //saccadi
        private void cSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "CSV (*.csv)|*.csv";
            saveFileDialogExport.FileName = "saccades.csv";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridSaccades.DataSource,
                     Export.ExportFormat.CSV, fileName);
            }
        }

        //fissazione
        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "CSV (*.csv)|*.csv";
            saveFileDialogExport.FileName = "fixations.csv";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridFixations.DataSource,
                     Export.ExportFormat.CSV, fileName);
            }
        }

        private void cSVToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "CSV (*.csv)|*.csv";
            saveFileDialogExport.FileName = "indicators.csv";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridIndicators.DataSource,
                     Export.ExportFormat.CSV, fileName);
            }
        }



        private void cSVToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFileDialogExport.Filter = "CSV (*.csv)|*.csv";
            saveFileDialogExport.FileName = "waves.csv";
            DialogResult result = saveFileDialogExport.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialogExport.FileName;
                Export objExport = new Export("Win");
                objExport.ExportDetails((DataTable)this.dataGridViewWaveForm.DataSource,
                     Export.ExportFormat.CSV, fileName);
            }
        }
        #endregion

        #region Delete
        private void deleteFixationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Fixations == null) || (list.Fixations.List == null) || (list.Fixations.List.Count <= 0)) return;
            List<int> toBeRemoved = new List<int>();
           
            foreach (DataGridViewRow row in this.dataGridFixations.Rows)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if ((bool)row.Cells["valid"].Value) continue;
                //remove parallel
                toBeRemoved.Add(idx);
            }

            toBeRemoved.Sort();
            for (int i = toBeRemoved.Count - 1; i >= 0; i--)
            {
                //remove parallel
                list.Fixations.List.RemoveAt(toBeRemoved[i]);
            }


            //draw fixation
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList curveList = myPane.CurveList;
            for (int i = 0; i < curveList.Count; i++)
            {
                if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                {
                    RemoveCurveMarker(fixationPrefix + curveList[i].Label.Text, curveList[i]);
                }
            }

            this.SetFixationList(list.Fixations);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        

        private void deleteSaccadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Saccades == null) || (list.Saccades.List == null) || (list.Saccades.List.Count <= 0)) return;
            List<int> toBeRemoved = new List<int>();
            
            foreach (DataGridViewRow row in this.dataGridSaccades.Rows)
            {
                if (row.IsNewRow) continue;
                if ((bool)row.Cells["valid"].Value) continue;

                int idx = (int)row.Cells["id"].Value - 1;
               
                //remove parallel
                toBeRemoved.Add(idx);
            }

            toBeRemoved.Sort();
            for (int i = toBeRemoved.Count - 1; i >= 0; i--)
            {
                //remove parallel
                list.Saccades.List.RemoveAt(toBeRemoved[i]);
            }

            //draw saccade
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList curveList = myPane.CurveList;
            for (int i = 0; i < curveList.Count; i++)
            {
                if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                {
                    RemoveCurveMarker(saccadePrefix + curveList[i].Label.Text, curveList[i]);
                }
            }

            //refresh sacacde
            this.SetSaccadeList(list.Saccades);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }


        private void removeInvalidWaveFormFromDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this,"Warning this action will purge data from source data. Fixations, saccades and waveforms must be recalculated.","Purge data?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if (result != DialogResult.OK) return;
            if (list == null) return;
            if ((list.WaveForms == null) || (list.WaveForms.List == null) || (list.WaveForms.List.Count <= 0)) return;
            if ((this.dataGridViewWaveForm.SelectedRows == null) || (this.dataGridViewWaveForm.SelectedRows.Count <= 0)) return;
            
            foreach (DataGridViewRow row in this.dataGridViewWaveForm.SelectedRows)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;

                //remove from data
                int idxStart = (int)row.Cells["idxStart"].Value;
                int size = (int)row.Cells["idxEnd"].Value - idxStart+1;
                this.list.List.RemoveRange(idxStart, size);
            }

            //update data
            list.Fixations.List.Clear();
            list.Saccades.List.Clear();
            list.WaveForms.List.Clear();
            list.Indicators.List.Clear();
            this.SetDataList(ref list);

            dataGridViewWaveForm.Refresh();

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        /// <summary>
        /// Remove Invalid waveforms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.WaveForms == null) || (list.WaveForms.List == null) || (list.WaveForms.List.Count <= 0)) return;
            
            List<int> toBeRemoved = new List<int>();
            
            foreach (DataGridViewRow row in this.dataGridViewWaveForm.Rows)
            {
                if (row.IsNewRow) continue;
                int idx = (int)row.Cells["id"].Value - 1;
                if ((bool)row.Cells["valid"].Value) continue;
                toBeRemoved.Add(idx);
            }
            
            toBeRemoved.Sort();
            for (int i=toBeRemoved.Count-1; i>=0; i--)
            {
                //remove parallel
                list.WaveForms.List.RemoveAt(toBeRemoved[i]);
            }

            //draw saccade
            GraphPane myPane = zedGraphControl1.GraphPane;
            CurveList curveList = myPane.CurveList;
            for (int i = 0; i < curveList.Count; i++)
            {
                if ((curveList[i].Tag == list) && (curveList[i].IsVisible))
                {
                    RemoveCurveMarker(waveFormPrefix + curveList[i].Label.Text, curveList[i]);
                }
            }

            this.SetWaveFormList(list.WaveForms);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        #endregion

        private void buttonColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog(this);
            if (result != DialogResult.Cancel)
            {
                colorMarker = colorDialog1.Color;
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string type = (string)this.comboBoxType.SelectedItem;
            if ((type == null) || (type.Length <= 0) || (list==null)) return;

             //ask for confirmation
            DialogResult result = MessageBox.Show(this.ParentForm, "The current operation will add a " + type + " on the corresponding table. Are you sure?", "Adding " + type, MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.Cancel) return;


            double value = (double)trackBar1.Value / (double)trackBar1.Maximum;
            int idxStart = (int)(value * (double)(list.List.Count));
            int idxEnd = idxStart+(int)numericUpDown1.Value;

            if (type.Equals(WaveFormType.Saccade.ToString()))
            {
                this.list.Saccades.List.Add(SaccadeManager.MakeSaccade(this.list, idxStart, idxEnd, -1));
            }
            else if (type.Equals(WaveFormType.Nystagmus.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.Nystagmus, -1));
            }
            else if (type.Equals(WaveFormType.MicroSaccade.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.MicroSaccade, -1));
            }
            else if (type.Equals(WaveFormType.Blink.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.Blink, -1));
            }
            else if (type.Equals(WaveFormType.SquareWave.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.SquareWave, -1));
            }
            else if (type.Equals(WaveFormType.TriangularWave.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.TriangularWave, -1));
            }
            else if (type.Equals(WaveFormType.BadData.ToString()))
            {
                this.list.WaveForms.List.Add(WaveFormManager.MakeWaveForm(this.list, idxStart, idxEnd, WaveFormType.BadData, -1));
            }
            else
            {
                this.list.Fixations.List.Add(FixationManager.MakeFixation(this.list, idxStart, idxEnd));
            }
            UpdateAnalysis();
        }
        
        private void buttonAdjust_Click_1(object sender, EventArgs e)
        {
            string type = (string)this.comboBoxType.SelectedItem;
            if ((type == null) || (type.Length <= 0) || (list == null)) return;
            if (this.textBoxID.Text.Equals("")) return;

            if (type.Equals("Fixation"))
            {
                int i = Int32.Parse(this.textBoxID.Text);
                int w = Int32.Parse(Properties.Settings.Default.SampleWindow);
                Fixation fix = FixationManager.AdjustBorderFixation(this.list, i, w);

                trackBar1.Value = fix.IdxStart;
                numericUpDown2.Value = trackBar1.Value;
                numericUpDown1.Value = fix.IdxEnd - fix.IdxStart;
                MoveCurveMarker();
            } 
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string type = (string)this.comboBoxType.SelectedItem;
            if ((type == null) || (type.Length <= 0) || (list == null)) return;
            if (this.textBoxID.Text.Equals("")) return;

            //ask for confirmation
            DialogResult result = MessageBox.Show(this.ParentForm, "The current operation will override " + type + " " + this.textBoxID.Text + ". You will NOT able to restore it. Are you sure?", "Adding " + type, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel) return;

            double value = (double)trackBar1.Value / (double)trackBar1.Maximum;
            int idxStart = (int)(value * (double)(list.List.Count));
            int idxEnd = idxStart + (int)numericUpDown1.Value;
            int i = Int32.Parse( this.textBoxID.Text );

            if (type.Equals(WaveFormType.Saccade.ToString()))
            {
                this.list.Saccades.List[i]= SaccadeManager.MakeSaccade(this.list, idxStart, idxEnd, -1);
            }
            else
            {
                this.list.Fixations.List[i] = FixationManager.MakeFixation(this.list, idxStart, idxEnd);
            }
            UpdateAnalysis();
            ResetEdit();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialogExportMarker.ShowDialog(this);
        }

        private void saveFileDialogExportMarker_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter stream = new StreamWriter(saveFileDialogExportMarker.OpenFile());
            
            double value = (double)trackBar1.Value / (double)trackBar1.Maximum;
            int idxStart = (int)(value * (double)(list.List.Count));
            int idxEnd = idxStart + (int)numericUpDown1.Value;

            for (int i = idxStart; (i < idxEnd) && (i<list.List.Count); i++)
            {
                Item item = list.List[i];
                stream.WriteLine(item.ToString());
            }
            stream.Close();
        }

        public override void Refresh()
        {
            if (type == PlotType.Time)
            {
                this.SetDataListVSTime(ref this.list);
            }
            else
            {
                this.SetDataListXY(ref this.list);
            }
            base.Refresh();
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.YAxis.Scale.IsReverse = !myPane.YAxis.Scale.IsReverse;
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void transitionMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROIList roiList = chooseROI();
            if (roiList != null)
            {
                AnalysisManager.Instance.FindROITransitionInSaccade(ref list, roiList);
                this.SetSaccadeList(list.Saccades);
            }
        }

        private void distanceFromNearestROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROIList roiList = chooseROI();
            if (roiList != null)
            {
                AnalysisManager.Instance.FindNearestROIFromFixations(ref list, roiList);
                this.SetFixationList(list.Fixations);
            }
        }

        #region ROIS
        private void toolStripButtonShowROI_Click(object sender, EventArgs e)
        {
            if (type == PlotType.Time)
            {
                toolStripButtonShowROI.Enabled = false;
                return;
            }
            roiList = chooseROI();
            ShowRoi(roiList);
        }

        private void ShowRoi(ROIList roiList)
        {
            if (roiList == null) return;

            //clear all roi
            for (int i = 0; i < this.zedGraphControl1.GraphPane.GraphObjList.Count; i++)
            {
                this.zedGraphControl1.GraphPane.GraphObjList.RemoveAt(i);
            }

            //design square roi
            foreach (ROI r in roiList.List)
            {
                SquareROI sr = (SquareROI)r;

                // Add a BoxObj to show a colored band behind the graph data
                BoxObj box = new BoxObj(sr.Cx - sr.Width, sr.Cy - sr.Height, 2*sr.Width, 2*sr.Height,
                      Color.FromArgb(150, 255, 0, 0), Color.Empty);
                box.Tag = r.Id;
                box.Location.CoordinateFrame = CoordType.AxisXYScale;
                box.ZOrder = ZOrder.E_BehindCurves;
                this.zedGraphControl1.GraphPane.GraphObjList.Add(box);
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private ROIList chooseROI()
        {
            ROIDialog roiDlg = new ROIDialog();
            roiDlg.SetList(AnalysisManager.Instance.ROIs);
            if (roiDlg.ShowDialog(this) == DialogResult.OK)
            {
                return roiDlg.SelectedObject;
            }
            return null;
        }

        private string WhichROI(double x, double y)
        {
            if (roiList == null) return null;
            ROI r = roiList.FindNearestROI(x, y);

            return r.IsInRoi(x,y) ? "in " +r.Id : "near " +r.Id;
        }
        #endregion

        #region Global Indicators
        private void sequencingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void toolStripButtonCurveSwitch_Click(object sender, EventArgs e)
        {
            if (this.type == PlotType.Time)
            {
                this.type = PlotType.XY;
            } else {
                this.type = PlotType.Time;
            }
            this.SetDataList(ref list);
        }

        //saccade
        private void hideSystemColumsSaccadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideSystemColumsSaccadeToolStripMenuItem.Checked = !hideSystemColumsSaccadeToolStripMenuItem.Checked;
            int i = 0;
            foreach (DataGridViewColumn col in dataGridSaccades.Columns)
            {
                if (i++ <= 3) col.Visible = !hideSystemColumsSaccadeToolStripMenuItem.Checked;
            }

            dataGridSaccades.Invalidate();
        }

        //fixations
        private void hideSystemColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideSystemColumnsFixationToolStripMenuItem.Checked = !hideSystemColumnsFixationToolStripMenuItem.Checked;
            int i = 0;
            foreach (DataGridViewColumn col in dataGridFixations.Columns)
            {
                if (i++ <= 3) col.Visible = !hideSystemColumnsFixationToolStripMenuItem.Checked;
            }

            dataGridFixations.Invalidate();
        }

        //waveform
        private void hideSystemColumnsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            hideSystemColumnsToolStripMenuItem1.Checked=!hideSystemColumnsToolStripMenuItem1.Checked;
            int i = 0;
            foreach (DataGridViewColumn col in dataGridViewWaveForm.Columns)
            {
                if (i++ <= 3) col.Visible = !hideSystemColumnsToolStripMenuItem1.Checked;
            }

            dataGridViewWaveForm.Invalidate();
        }

        private void hideSyetmColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideSyetmColumnsToolStripMenuItem.Checked = !hideSyetmColumnsToolStripMenuItem.Checked;
            int i = 0;
            foreach (DataGridViewColumn col in dataGridIndicators.Columns)
            {
                if ((i == 0) || (i == 2) || (i == 3)) col.Visible = !hideSyetmColumnsToolStripMenuItem.Checked;
                i++;
            }

            dataGridIndicators.Invalidate();
        }

        //fixation
        private void editCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridFixations.SelectedRows.Count != 1) return;
            EditNow((int)(dataGridFixations.SelectedRows[0].Index), 1);
        }

        //Saccade
        private void editCurrentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridSaccades.SelectedRows.Count != 1) return;
            EditNow((int)(dataGridSaccades.SelectedRows[0].Index), 0);
        }

        private void EditNow(int id, int typeIdx)
        {
            textBoxID.Text = ""+id;

            int idxS = 0;
            int idxE = 0;
            if (typeIdx == 0) //saccade
            {
                idxS = list.Saccades.List[id].IdxStart;
                idxE = list.Saccades.List[id].IdxEnd;
            }
            else //fixation
            {
                idxS = list.Fixations.List[id].IdxStart;
                idxE = list.Fixations.List[id].IdxEnd;
            }

            comboBoxType.SelectedIndex = typeIdx;
            buttonUpdate.Enabled = true;
            buttonAdjust.Enabled = buttonUpdate.Enabled;
            comboBoxType.Enabled = !buttonUpdate.Enabled;
            buttonAdd.Enabled = !buttonUpdate.Enabled;

            trackBar1.Value = idxS;
            numericUpDown2.Value = trackBar1.Value;
            numericUpDown1.Value = Math.Max(idxE - idxS,1);
            MoveCurveMarker();

            //set the 
            this.tabControl.SelectedIndex = 4;
        }

        private void ResetEdit()
        {
            textBoxID.Text = "";

            buttonUpdate.Enabled = false;
            buttonAdjust.Enabled = buttonUpdate.Enabled;
            comboBoxType.Enabled = !buttonUpdate.Enabled;
            buttonAdd.Enabled = !buttonUpdate.Enabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetEdit();
        }

        private void nextMergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Fixations == null) || (list.Fixations.List == null) || (list.Fixations.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridFixations.SelectedRows;
            
            
            DialogResult result = MessageBox.Show(this.Parent, "Warning this action will merge two fixation.", "Are you sure?", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                //update edit button
                this.editFixationCurrentToolStripMenuItem.Enabled = collections.Count == 1;
                this.mergeFixationToolStripMenuItem.Enabled = collections.Count == 1;

                foreach (DataGridViewRow row in collections)
                {
                    if (row.IsNewRow) continue;
                    int idx = (int)row.Cells["id"].Value - 1;
                    if (idx >= list.Fixations.List.Count) continue;

                    FixationManager.MergeFixation(this.list, idx, idx + 1);
                }

                //update
                UpdateAnalysis();
                //Redraw
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null) return;
            if ((list.Fixations == null) || (list.Fixations.List == null) || (list.Fixations.List.Count <= 0)) return;
            DataGridViewSelectedRowCollection collections = dataGridFixations.SelectedRows;

            DialogResult result = MessageBox.Show(this.Parent, "Warning this action will merge two fixation.", "Are you sure?", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {

                //update edit button
                this.editFixationCurrentToolStripMenuItem.Enabled = collections.Count == 1;
                this.mergeFixationToolStripMenuItem.Enabled = collections.Count == 1;

                foreach (DataGridViewRow row in collections)
                {
                    if (row.IsNewRow) continue;
                    int idx = (int)row.Cells["id"].Value - 1;
                    if (idx >= list.Fixations.List.Count) continue;

                    FixationManager.MergeFixation(this.list, idx, idx - 1);
                }

                //update
                UpdateAnalysis();
                //Redraw
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }

        private void dataXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorDataX = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

        private void dataYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorDataY = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

        private void referenceXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorRefX = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

        private void referenceYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorRefY = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

        private void distracterXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorDistracterX = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

        private void distracterYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            DialogResult result = dlg.ShowDialog(this.Parent);
            if (result == DialogResult.OK)
            {
                this.colorDistracterY = dlg.Color;
            }
            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            this.Refresh();
        }

    }
}
