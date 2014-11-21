using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Diagnostics;

namespace EVALab.Util.Graph
{
    public partial class ZedChartControlForm : UserControl
    {
        public ZedChartControlForm()
        {
            InitializeComponent();
        }

        private void ZedChartControlForm_Load(object sender, EventArgs e)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.XAxis.IsVisible = true;
            myPane.YAxis.Title.IsVisible = false;
            myPane.YAxis.Color = Color.White;
            myPane.Legend.IsVisible = false;
            zedGraphControl1.GraphPane.Chart.Fill.Type = FillType.Solid;
            zedGraphControl1.GraphPane.Chart.Fill.Color = Color.Black;

            // Save 1200 points.  At 50 ms sample rate, this is one minute
            // The RollingPointPairList is an efficient storage class that always
            // keeps a rolling set of point data without needing to shift any data values
            RollingPointPairList list1 = new RollingPointPairList(6000);
            RollingPointPairList list2 = new RollingPointPairList(6000);
            RollingPointPairList list3 = new RollingPointPairList(6000);

            // Initially, a curve is added with no data points (list is empty)
            // Color is blue, and there will be no symbols
            LineItem curve1 = myPane.AddCurve("Voltage DX", list1, Color.Green, SymbolType.None);
            LineItem curve2 = myPane.AddCurve("Voltage SX", list2, Color.Red, SymbolType.None);
            LineItem curve3 = myPane.AddCurve("Reference", list3, Color.Blue, SymbolType.None);

            
            Reset();
        }

        // Set the size and location of the ZedGraphControl
        private void SetSize()
        {
            // Control is always 10 pixels inset from the client rectangle of the form
            Rectangle formRect = this.ClientRectangle;
            formRect.Inflate(-10, -10);

            if (zedGraphControl1.Size != formRect.Size)
            {
                zedGraphControl1.Location = formRect.Location;
                zedGraphControl1.Size = formRect.Size;
            }
        }

        public void AddValue(double time, double value1)
        {
            // Make sure that the curvelist has at least one curve
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;

            // Get the first CurveItem in the graph
            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            if (curve1 == null)
                return;

            // Get the PointPairList
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list1 == null)
                return;

            // 3 seconds per cycle
            list1.Add(time, value1);

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - 5000.0;
            }

            // Make sure the Y axis is rescaled to accommodate actual data
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
        }

        public void AddReference(double time, double value)
        {
            // Make sure that the curvelist has at least one curve
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;

            // Get the first CurveItem in the graph
            LineItem curve3 = zedGraphControl1.GraphPane.CurveList[2] as LineItem;
            if (curve3 == null)
                return;

            // Get the PointPairList
            IPointListEdit list3 = curve3.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list3 == null)
                return;

            // 3 seconds per cycle
            list3.Add(time, value);

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - 5000.0;
            }

            // Make sure the Y axis is rescaled to accommodate actual data
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
        }


        public void AddValue(double time, double value1, double value2)
        {
            // Make sure that the curvelist has at least one curve
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;

            // Get the first CurveItem in the graph
            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            if (curve1 == null)
                return;

            // Get the PointPairList
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list1 == null)
                return;

            // 3 seconds per cycle
            list1.Add(time, value1);

            // Get the first CurveItem in the graph
            LineItem curve2 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            if (curve2 != null)
            {

                // Get the PointPairList
                IPointListEdit list2 = curve2.Points as IPointListEdit;
                // If this is null, it means the reference at curve.Points does not
                // support IPointListEdit, so we won't be able to modify it
                if (list2 != null)
                {
                    list2.Add(time, value2);
                }
            }
            

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + 10;
                xScale.Min = xScale.Max - 5000.0;
            }

            // Make sure the Y axis is rescaled to accommodate actual data
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
        }

        private void ZedChartControlForm_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        public void Reset()
        {
            Debug.WriteLine("Rest graph");
            GraphPane myPane = zedGraphControl1.GraphPane;            
            
            // Just manually control the X axis range so it scrolls continuously
            // instead of discrete step-sized jumps
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 5000;
            myPane.XAxis.Scale.MinorStep = 100;
            myPane.XAxis.Scale.MajorStep = 1000;

            for (int i = 0; i < myPane.CurveList.Count; i++)
            {
                zedGraphControl1.GraphPane.CurveList[i].Clear();
            }
            // Make sure the Y axis is rescaled to accommodate actual data
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
        }
    }
}
