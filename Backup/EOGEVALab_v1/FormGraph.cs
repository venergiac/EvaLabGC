using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace EvaLab.EOG
{
    /// <summary>
    /// Simple line Graph
    /// </summary>
    public partial class FormGraph : Form
    {
        PointPairList list1 = new PointPairList();
        PointPairList list2 = new PointPairList();
        PointPairList listRef = new PointPairList();
        public FormGraph()
        {
            InitializeComponent();
        }

        public void AddData(double data1, double data2, long time, double refValue)
        {
            list1.Add(time, data1);
            list2.Add(time, data2);
            listRef.Add(time, refValue);
        }

        public void Reset()
        {
            list1.RemoveRange(0, list1.Count);
            list2.RemoveRange(0, list2.Count);
            listRef.RemoveRange(0, listRef.Count);
        }

        public void CreateGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.RemoveRange(0, myPane.CurveList.Count);

            // Set the titles and axis labels
            myPane.Title.Text = "EOG";
            myPane.XAxis.Title.Text = "Voltage/Reference";
            myPane.YAxis.Title.Text = "Time";

            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
            LineItem myCurve1 = myPane.AddCurve("Channel 1", list1, Color.Blue,
                                    SymbolType.None);

            LineItem myCurve2 = myPane.AddCurve("Channel 2", list2, Color.Green,
                                  SymbolType.None);

            LineItem myCurve3 = myPane.AddCurve("Reference", listRef, Color.Red,
                                  SymbolType.None);

            // Calculate the Axis Scale Ranges
            zedGraphControl1.AxisChange();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }
    }
}
