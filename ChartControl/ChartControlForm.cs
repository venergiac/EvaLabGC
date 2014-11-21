using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ChartControl
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class ChartControlForm : System.Windows.Forms.UserControl
	{    
		/// <summary>
		/// Required designer variable.
		/// </summary>

    /* Public Properties */

    public float Maximum
    {
      get
      {
        return m_Maximum;
      }
    }
    public float Minimum
    {
      get
      {
        return m_Minimum;
      }
    }

    /* Public Vars */

    public int LineWidth;
    public int PixelsPer1; //Per One Gridbox
    public int LineDifference;
    public float ValueMultiplier; //in order to make values larger if you need to improve scale
    public Color AboveColor, UnderColor, GridColor, ChartBackColor, AxesColor;
    public ChartControlOpenType OpenType;

    /* Private Vars */

    private System.Windows.Forms.Panel ChartPanel;
    private System.ComponentModel.Container components = null;
    private Graphics g;
    private float[] Values;
    private float m_Maximum,m_Minimum;
    private int CurrentYGridStart;
    private int CurrentNumberOfValues;

    private Size CurrentSize = new Size(0, 0);

    public ChartControlForm() 
    {
      InitializeComponent();

      OpenType = ChartControlOpenType.Bar; //Default Value

      LoadDefaultValues();

      InitChart();
    }

    public ChartControlForm(ChartControlOpenType NOpenType)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      OpenType = NOpenType;

      LoadDefaultValues();

      InitChart();

		}

    private void LoadDefaultValues()
    {
      /* Loading Default Values */

      
      g = ChartPanel.CreateGraphics();
      PixelsPer1 = 10;
      ChartBackColor = Color.Black;
      GridColor = Color.Green;
      AboveColor = Color.Chartreuse;
      UnderColor = Color.Red;
      AxesColor = Color.White;
      CurrentYGridStart = 0;
      ValueMultiplier = 1;
      m_Maximum = ChartPanel.Size.Height / 2;
      m_Minimum = (-1) * (ChartPanel.Size.Height / 2);
      LineDifference = 1;

      /* Initializing Value Array (Size is width since every Line width is 1 pixel) */

      if (OpenType == ChartControlOpenType.Bar) //WIDTH because there's no space between lines
        Values = new float[ChartPanel.Size.Width];
      else
        Values = new float[ChartPanel.Size.Width / 2]; //WIDTH/2 because there's a 2 pixel space between dots

      for (int i = 0; i < Values.Length; i++)
        Values[i] = 0;

      CurrentNumberOfValues = 0;
    }

    public void RefreshControl()
    {
      PostInitChart();
      DrawChart();
    }

    public void AddValue (float val)
    {
      /* Adding value at end of array */
      if ((Minimum != 0) && (Maximum != 0)) /* Prevent first draw errors */
        if ((val * ValueMultiplier > Maximum) || (val * ValueMultiplier < Minimum))
          return; //Value is too high or too low for display.
      for (int i=0;i<Values.Length-1;i++)
        Values[i] = Values[i+1];
      Values[Values.Length - 1] = val * ValueMultiplier;

      if (CurrentNumberOfValues < Values.Length)
        CurrentNumberOfValues++;

      if (CurrentYGridStart < PixelsPer1 * LineDifference - 1)
      {
        if (OpenType == ChartControlOpenType.Bar)
          CurrentYGridStart++;
        else
          CurrentYGridStart += 2;
      }
      else
      {
        CurrentYGridStart = 0;
      }

      /* Redrawing chart */

      DrawChart();
    }

    public void LoadFromValues(ArrayList NewValues)
    {
      for (int i = 0; i < NewValues.Count; i++)
        Values[Values.Length - i - 1] = ValueMultiplier * (float)Convert.ToDouble(NewValues[i]);

      CurrentNumberOfValues = NewValues.Count;

      PostInitChart();
    }
    public void LoadFromValues(float[] NewValues)
    {
      for (int i = 0; i < NewValues.Length; i++)
        Values[Values.Length - i - 1] = NewValues[i] * ValueMultiplier;

      CurrentNumberOfValues = NewValues.Length;

      PostInitChart();
    }

    public void InitChart()
    {

      /* First Time Chart Init */

      CurrentYGridStart = 0;

      PostInitChart();
    }

    int IntCmp(float num1, float num2)
    {
      /* Compares 2 floats,
       * if num1 > 0 and num2 < 0, returns 1, 
       * \if both are > 0 or both are < 0 returns 0, 
       * if num2 > 0 and num1 < 0 returns -1
       */

      if ((num1 >= 0) && (num2 >= 0))
        return 0;
      if ((num1 < 0) && (num2 < 0))
        return 0;
      if ((num1 >= 0) && (num2 < 0))
        return 1;
      if ((num1 < 0) && (num2 >= 0))
        return -1;

      return 0;

    }

    public void ChangeInnerChartControlSize(Size NewSize)
    {
      this.Size = NewSize;
      if (ChartPanel != null) 
        PostInitChart();
    }
    
    public void PostInitChart()
    {

      /* Refresh of Component, Clear and redraw of grid, which also redraws values */

      if ((ChartPanel.Height != 0) && (ChartPanel.Width != 0)) //To avoid drawing a 0 width or 0 size rectangle
      {
        g.Clear(ChartBackColor);

        DrawGrid();
      }
    }

    private void DrawGrid()
    {      
      /* Drawing X Grid */

      for (int i=(ChartPanel.Size.Height/2) + PixelsPer1*LineDifference;i<ChartPanel.Size.Height;i+=PixelsPer1*LineDifference)
        g.DrawLine(new Pen(GridColor),0,i,ChartPanel.Size.Width,i); 
      for (int i=(ChartPanel.Size.Height/2) - PixelsPer1*LineDifference;i>0;i-=PixelsPer1*LineDifference)
        g.DrawLine(new Pen(GridColor),0,i,ChartPanel.Size.Width,i);

      /* Drawing Y Grid */

      for (int i = CurrentYGridStart; i < ChartPanel.Size.Width; i += PixelsPer1 * LineDifference)
        g.DrawLine(new Pen(GridColor),i,0,i,ChartPanel.Size.Height);

      /* Drawing Axes */

      g.DrawLine(new Pen(AxesColor),0,(int)(ChartPanel.Size.Height/2),ChartPanel.Size.Width,(int)(ChartPanel.Size.Height/2));

    }

    private void DrawChart()
    {
      PostInitChart(); //Refresh of the chart

      Pen AbovePen = new Pen(AboveColor);
      Pen UnderPen = new Pen(UnderColor);

      if (OpenType == ChartControlOpenType.Bar) //Drawing bar
      {

        for (int i = Values.Length - CurrentNumberOfValues; i < Values.Length; i++)
        {
          if (Values[i] > 0) // More than 0, AboveColor is used
          {
            g.DrawLine(AbovePen, Values.Length - i - 1, (int)(ChartPanel.Size.Height / 2) - 1, Values.Length - i - 1, (int)(ChartPanel.Size.Height / 2) - Values[i]);
          }
          if (Values[i] < 0) // Less than 0, UnderColor is used
          {
            g.DrawLine(UnderPen, Values.Length - i - 1, (int)(ChartPanel.Size.Height / 2) + 1, Values.Length - i - 1, (int)(ChartPanel.Size.Height / 2) - Values[i]);
          }
        }
      }
      else if (OpenType == ChartControlOpenType.Graph) //Drawing Graph
      {
        for (int i = Values.Length - CurrentNumberOfValues; i < Values.Length; i++)
        {
          if (Values[i] >= 0) // More than 0, AboveColor is used
          {            
            if (IntCmp(Values[i], Values[i - 1]) > 0) //if it goes bellow 0
            {
              g.DrawLine(UnderPen, (Values.Length - i) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i - 1], (Values.Length - i) * 2 - 1, (int)(ChartPanel.Size.Height / 2));
              g.DrawLine(AbovePen, (Values.Length - i) * 2 - 1, (int)(ChartPanel.Size.Height / 2), (Values.Length - i - 1) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i]);
            }
            else //if it stays above 0
            {
              g.DrawLine(AbovePen, (Values.Length - i) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i - 1], (Values.Length - i - 1) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i]);
            }
          }
          if (Values[i] < 0) // Less than 0, UnderColor is used
          {
            if (IntCmp(Values[i], Values[i - 1]) < 0) //if it goes above 0
            {
              g.DrawLine(AbovePen, (Values.Length - i) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i - 1], (Values.Length - i) * 2 - 1, (int)(ChartPanel.Size.Height / 2));
              g.DrawLine(UnderPen, (Values.Length - i) * 2 - 1, (int)(ChartPanel.Size.Height / 2), (Values.Length - i - 1) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i]);
            }
            else //if it stays bellow 0
            {
              g.DrawLine(UnderPen, (Values.Length - i) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i - 1], (Values.Length - i - 1) * 2, (int)(ChartPanel.Size.Height / 2) - Values[i]);
            }
          }
        }
      }

      //Dont forget to dispose variables...
      UnderPen.Dispose();
      AbovePen.Dispose();
    }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

    private void RecalculateSize()
    {
      if ((CurrentSize.Height != 0) && (CurrentSize.Width != 0)) //avoid divide by 0
      {
        m_Maximum = ChartPanel.Size.Height / 2;
        m_Minimum = (-1) * (ChartPanel.Size.Height / 2);

        float SizeChange = ((float)Size.Height / (float)CurrentSize.Height);

        if (Size.Height != 0)
          ValueMultiplier *= SizeChange;

        int i, j;

        float[] NewValues = new float[Size.Width];

        for (i = Values.Length - 1, j = NewValues.Length - 1; ((i >= 0) && (j >= 0)); i--, j--)
        {
          if (SizeChange != 0)
            NewValues[j] = Values[i] * SizeChange;
        }

        Values = NewValues;

        g.Dispose();
        g = ChartPanel.CreateGraphics();

        DrawChart();
      }
    }

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
		{
      this.ChartPanel = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // ChartPanel
      // 
      this.ChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ChartPanel.Location = new System.Drawing.Point(0, 0);
      this.ChartPanel.Name = "ChartPanel";
      this.ChartPanel.Size = new System.Drawing.Size(192, 88);
      this.ChartPanel.TabIndex = 0;
      this.ChartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartPanel_Paint);
      // 
      // UserControl1
      // 
      this.Controls.Add(this.ChartPanel);
      this.Name = "UserControl1";
      this.Size = new System.Drawing.Size(192, 88);
      this.Resize += new System.EventHandler(this.UserControl1_Resize);
      this.ResumeLayout(false);

    }
		#endregion

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      if (ChartPanel != null)
      {

        if ((Size.Height == 0) || (Size.Width == 0))
          return;

        if ((CurrentSize.Height == 0) && (CurrentSize.Width == 0))
        {
          CurrentSize = Size;
          return;
        }

        RecalculateSize();
        CurrentSize = Size;
      }
    } 
    
    private void UserControl1_Resize(object sender, EventArgs e)
    {
    }

    private void ChartPanel_Paint(object sender, PaintEventArgs e)
    {
      if (ChartPanel != null)
        OnResize(new EventArgs());
    }
	}
  public enum ChartControlOpenType
  {
    Bar,
    Graph
  };
}
