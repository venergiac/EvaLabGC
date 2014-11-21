	
namespace User
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Collections;
	using System.Windows.Forms;


	public class Chart
	{
		private ArrayList m_data_sets;
		private Color m_chart_color, m_back_color;
		private Color m_div_color, m_axes_color;
		private Font m_font;
		private int m_margin;
		private RectangleF m_data_rect, m_limits_rect, m_screen_rect;
		private string m_x_label, m_y_label;

		public class Study
		{
			private PointF[] m_vector;
			private Pen m_pen;
			private Chart m_parent;
			internal bool m_is_primary;

			// Study objects should not be externally creatable
			private Study()
			{
			}

			internal Study(Chart parent, PointF[] vector)
			{
				m_parent = parent;
				m_vector = vector;
				m_pen = new Pen(Color.Black);
			}

			public Pen StudyPen
			{
				get
				{
					return m_pen;
				}
				set
				{
					m_pen = value;
				}
			}

			public PointF[] Vector
			{
				get
				{
					return m_vector;
				}
			}

			public bool IsPrimary
			{
				get
				{
					return m_is_primary;
				}
				set
				{
					m_parent.MakePrimary(this, value);
				}
			}
		}
		
		// Instantiate and return a new study on request,
		// first adding it to the studies collection
		public Study AddStudy(PointF[] vector)
		{
			Study study = new Study(this, vector);
			
			m_data_sets.Add(study);

			// Always set the first study to primary
			if (m_data_sets.Count == 1)
			{
				MakePrimary(study, true);
			}
			else
			{
				// Update status of study set each time
				// a new study is added
				MakePrimary(study, study.IsPrimary);
			}

			return study;
		}

		public Color ChartColor
		{
			get
			{
				return m_chart_color;
			}
			set
			{
				m_chart_color = value;
			}
		}

		public Color AxesColor
		{
			get
			{
				return m_axes_color;
			}
			set
			{
				m_axes_color = value;
			}
		}

		public Color DivisionColor
		{
			get
			{
				return m_div_color;
			}
			set
			{
				m_div_color = value;
			}
		}

		public Color BackgroundColor
		{
			get
			{
				return m_back_color;
			}
			set
			{
				m_back_color = value;
			}
		}

		public Font Font
		{
			get
			{
				return m_font;
			}
			set
			{
				m_font = value;
			}
		}

		public int Margin
		{
			set
			{
				m_margin = value;
			}
			get
			{
				return m_margin;
			}
		}

		public RectangleF ScreenRect
		{
			get
			{
				return m_screen_rect;
			}
		}

		public RectangleF DataRect
		{
			get
			{
				return m_data_rect;
			}
			set
			{
				m_data_rect = value;
			}
		}

		public string XLabel
		{
			get
			{
				return m_x_label;
			}
			set
			{
				m_x_label = value;
			}
		}

		public string YLabel
		{
			get
			{
				return m_y_label;
			}
			set
			{
				m_y_label = value;
			}
		}
		
		public virtual string TranslateXValue(float val, int disp_precision)
		{
			// Default implementation
			return Math.Round(val, disp_precision).ToString();
		}

		public virtual string TranslateYValue(float val, int disp_precision)
		{
			// Default implementation
			return Math.Round(val, disp_precision).ToString();
		}

		public bool AtMinX()
		{
			return m_data_rect.Left <= m_limits_rect.Left;
		}

		public bool AtMaxX()
		{
			return m_data_rect.Right >= m_limits_rect.Right;
		}

		public bool AtMaxY()
		{
			return m_data_rect.Top <= m_limits_rect.Top;
		}

		public bool AtMinY()
		{
			return m_data_rect.Bottom >= m_limits_rect.Bottom;
		}

		public void Reset()
		{
			m_data_rect = m_limits_rect;
		}
	
		private void CalcMinMax(PointF[] vector)
		{
			if (vector.Length == 0)
				throw new IndexOutOfRangeException("Data study cannot be empty");
				
			float min_x, min_y, max_x, max_y;

			min_x = max_x = vector[0].X;
			min_y = max_y = vector[0].Y;
			
			for (int i = 0; i < vector.Length; i ++)
			{
				if (vector[i].X < min_x)
					min_x = vector[i].X;
				else if (vector[i].X > max_x)
					max_x = vector[i].X;

				if (vector[i].Y < min_y)
					min_y = vector[i].Y;
				else if (vector[i].Y > max_y)
					max_y = vector[i].Y;
			}

			// Use limits rect for default data rect
			m_limits_rect = new RectangleF(min_x, min_y, max_x - min_x, max_y - min_y);
			m_data_rect = m_limits_rect;
			
			// Can't visualize a dataset without a width or height;
			// since we can't even draw the axes, it's best to flag an 
			// error at this stage
			if (m_data_rect.Width == 0 || m_data_rect.Height == 0)
				throw new InvalidOperationException("Data study must have non-zero width and height");
		}

		public Chart()
		{
			Init();
		}
		
		private void Init()
		{
			// Create default preference scheme
			m_div_color = System.Drawing.Color.Red;
			m_axes_color = System.Drawing.Color.Black;
			m_chart_color = System.Drawing.Color.Brown;
			m_back_color = System.Drawing.Color.BurlyWood;
			m_font = new Font("Arial", 7);
			m_margin = 20;
			m_x_label = "X axis";
			m_y_label = "Y axis";

			m_data_sets = new ArrayList();
		}

		private void MakePrimary(Study study, bool primary)
		{
			// Note throughout this method we explicitly access m_is_primary
			// when making an assignment, to avoid reentrancy

			// If the state isn't different from the study's existing state,
			// do nothing
			if (primary == study.IsPrimary)
				return;

			// If you've asked to make the study primary, ensure
			// no other study is primary. If you've asked to make
			// the study secondary, ensure some other is primary.
			foreach(object obj in m_data_sets)
				((Study) obj).m_is_primary = false;

			if (primary)
			{
				study.m_is_primary = true;
				CalcMinMax(study.Vector);
			}
			else
			{
				// If there's only one study, it has to stay primary (ie.
				// do nothing); otherwise make some different data set primary;
				study.m_is_primary = true;

				foreach(Study new_study in m_data_sets)
				{
					if (new_study != study)
					{
						study.m_is_primary = false;
						new_study.m_is_primary = true;
						CalcMinMax(new_study.Vector);
						break;
					}
				}
			}
		}

		public PointF Transform(RectangleF from, RectangleF to, PointF pt_src)
		{
			PointF pt_dst = new PointF();
			pt_dst.X = (((pt_src.X - from.Left) / from.Width) * to.Width) + to.Left;
			pt_dst.Y = (((pt_src.Y - from.Top) / from.Height) * to.Height) + to.Top;
			return pt_dst;
		}

		public SizeF Transform(RectangleF from, RectangleF to, SizeF sz_src)
		{
			SizeF sz_dst = new SizeF();
			sz_dst.Width = (sz_src.Width / from.Width) * to.Width;
			sz_dst.Height = (sz_src.Height / from.Height) * to.Height;
			return sz_dst;
		}

		public void Snap(PointF pt)
		{
			// Correct for inverted y axis
			pt.Y = m_screen_rect.Bottom - pt.Y + m_margin;
			
			// Transform into data coordinates
			PointF origin = Transform(m_screen_rect, m_data_rect, pt);
	
			// Build a new rectangle from scratch
			float min_x, max_x, min_y, max_y;

			min_x = origin.X - (m_data_rect.Width /2);
			max_x = origin.X + (m_data_rect.Width /2);
			min_y = origin.Y - (m_data_rect.Height /2);
			max_y = origin.Y + (m_data_rect.Height /2);
			m_data_rect = new RectangleF(min_x, min_y, m_data_rect.Width, 
				m_data_rect.Height);
		}


		public void Zoom(float percent)
		{
			float dx = (percent / 200) * m_data_rect.Width;
			float dy = (percent / 200) * m_data_rect.Height;
			
			m_data_rect = new RectangleF(m_data_rect.Left + dx,
				m_data_rect.Top + dy,
				m_data_rect.Width - (dx*2),
				m_data_rect.Height - (dy*2));
		}

		private void DrawXLabel(Graphics g)
		{
			Brush brush = new SolidBrush(m_axes_color);
			SizeF size = g.MeasureString(m_x_label, m_font);
			
			// Only draw if the label would fit on the screen
			if (size.Width < m_screen_rect.Width)
			{
				g.DrawString(m_x_label, m_font, brush, new PointF(
					m_screen_rect.Left + ((m_screen_rect.Width - size.Width) / 2),
					m_screen_rect.Height + (2*m_margin) - size.Height));
			}
		}

		private void DrawYLabel(Graphics g)
		{
			g.RotateTransform(-90);
			Brush brush = new SolidBrush(m_axes_color);
			SizeF size = g.MeasureString(m_y_label, m_font);
			
			// Only draw if the label would fit on the screen
			if (size.Width < m_screen_rect.Height)
			{
				g.DrawString(m_y_label, m_font, brush, new PointF(
					-(m_screen_rect.Height + size.Width)/2 - m_margin, 
					0));
			}
			
			g.ResetTransform();
		}

		public void Draw(Control control)
		{
			// Clear the background
			Graphics g = control.CreateGraphics();
			g.Clear(m_back_color);
				
			// Calculate the main drawing region; axis markers will be drawn
			// outside of this
			m_screen_rect = g.VisibleClipBounds;
			
			// Reduce the rectangle to accommodate the margins
			m_screen_rect.Inflate(new Size(-m_margin, -m_margin));
		
			// Draw the labels
			DrawXLabel(g);
			DrawYLabel(g);
			
			// Draw the axes
			DrawXAxis(g);
			DrawYAxis(g);
			
			// Now set the clipping region, based on any margin settings;
			// the clipping region is inclusive, so we need to inflate it
			RectangleF clip_rect = m_screen_rect;
			clip_rect.Inflate(1, 1);
			g.SetClip(clip_rect);
			
			// Screen origin is top-left, so y-axis needs to be reversed
			float y_offset = m_screen_rect.Bottom + m_margin;
						
			// Draw the data sets
			for (int i = 0; i < m_data_sets.Count; i ++)
			{
				Study study = (Study) m_data_sets[i];
				PointF[] vector = study.Vector;
				Pen pen = study.StudyPen;

				// Since points are joined consecutively, we can cache the previous
				// point each time we go through the loop
				PointF from = Transform(m_data_rect, m_screen_rect, vector[0]);
				from.Y = y_offset - from.Y;

				for (int j = 1; j < vector.Length; j ++)
				{
					PointF to = Transform(m_data_rect, m_screen_rect, vector[j]);
					to.Y = y_offset - to.Y;
					
					g.DrawLine(pen, from, to);

					from = to;
				}			
			}

			// Release resources
			g.Dispose();
		}

		protected virtual void CalcDivIntervals(float ax_start, float ax_size, 
			out float start_pos, out float major_div, out float minor_div, 
			out int subdiv_index, out int mag)
		{
			// Calculate a suitable exponent for the division marker based on the 
			// magnitude of the data range
			mag = Math.Abs(((int) Math.Log10(1 / ax_size)) +1);

			// major_div is the major division interval, and is a power of 10
			major_div = (float) Math.Pow(10, mag);
			
			// For fractional extents, we need to take the reciprocal
			if (ax_size < 1)
				major_div = 1 / major_div;

			// Round the start position to an integer multiple of major_div
			start_pos = ((int) (ax_start / major_div)) * major_div;
			
			// If the initial position is negative, rounding start_pos will
			// increase its value, but start_pos should be <= ax_start
			if (start_pos > ax_start)
				start_pos -= major_div;

			// Calculate how many major divisions would actually get displayed
			// in this data range, and adjust the major division extent accordingly
			int major_divs_displayed = 0;

			while (major_divs_displayed < 3)
			{
				major_divs_displayed = 0;
				
				for (float test_pos = start_pos; test_pos < ax_start + ax_size; 
					test_pos += major_div)
				{
					if (test_pos >= ax_start)
						major_divs_displayed ++;
				}

				if (major_divs_displayed > 10)
				{
					major_div *= 5;
					break;
				}

				if (major_divs_displayed >= 3)
					break;
				
				major_div /= 10;
			}
			
			// There are 10 minor divisions to a major division
			minor_div = (float) major_div / 10;
			
			// Now offset start_pos by multiples of minor_div so that it's 
			// just inside the data range.
			subdiv_index = 0;
			
			if (start_pos > ax_start)
			{
				while (start_pos >= ax_start + minor_div)
				{
					start_pos -= minor_div;
					subdiv_index = (subdiv_index + 1) % 10;
				}
		
				subdiv_index = (10 - subdiv_index) % 10;
			} 
			else
			{
				while (start_pos < ax_start)
				{
					start_pos += minor_div;
					subdiv_index = (subdiv_index + 1) % 10;
				}
			}
		}

		public void DrawXAxis(Graphics g)
		{
			int mag, subdiv_index;
			float start_pos, major_div, minor_div;
			
			CalcDivIntervals(m_data_rect.Left, m_data_rect.Width,
				out start_pos, out major_div, out minor_div, 
				out subdiv_index, out mag);

			// Draw the axis line
			Pen pen_axes = new Pen(m_axes_color);
			g.DrawLine(pen_axes, new PointF(m_screen_rect.Left, m_screen_rect.Bottom),
				new PointF(m_screen_rect.Right, m_screen_rect.Bottom));
			
			// Remember where we last drew a division marker, and use this to
			// decide how closely we should space them
			float last_string_end = Transform(m_data_rect, m_screen_rect, 
				new PointF(start_pos - major_div, m_data_rect.Bottom)).X;
			
			// Draw the divisions
			Brush brush = new SolidBrush(m_div_color);
			
			for (float d_iter = start_pos; d_iter < m_data_rect.Right; d_iter += minor_div)
			{
				PointF pt = Transform(m_data_rect, m_screen_rect, new PointF(d_iter, m_data_rect.Bottom));
				
				// If it's a major division, draw a large marker and possibly a
				// division annotation as well
				if (subdiv_index == 0)
				{
					g.DrawLine(pen_axes, pt, new PointF(pt.X, pt.Y + 5));
					
					string str_val = TranslateXValue(d_iter, mag+1);

					if (str_val != "")
					{
						SizeF size = g.MeasureString(str_val, m_font);
						PointF string_start = new PointF(pt.X - (size.Width/2), pt.Y + 7);
					
						// Only draw the string if it wouldn't run into a previous annotation
						if (string_start.X > (last_string_end + 2) || d_iter == start_pos) 
						{
							g.DrawString(str_val, m_font, brush, string_start);
							last_string_end = pt.X + (size.Width/2);
						}
					}
				}
				else
				{
					// Otherwise draw a small marker
					g.DrawLine(pen_axes, pt, new PointF(pt.X, pt.Y + 2));
				}
		
				subdiv_index = (subdiv_index + 1) % 10;
			}
		}

		public void DrawYAxis(Graphics g)
		{
			int mag, subdiv_index;
			float start_pos, major_div, minor_div;
			
			CalcDivIntervals(m_data_rect.Top, m_data_rect.Height,
				out start_pos, out major_div, out minor_div, 
				out subdiv_index, out mag);

			// Draw the axis line
			Pen pen_axes = new Pen(m_axes_color);
			g.DrawLine(pen_axes, new PointF(m_screen_rect.Left, m_screen_rect.Top),
				new PointF(m_screen_rect.Left, m_screen_rect.Bottom));
			
			// Draw the divisions
			Brush brush = new SolidBrush(m_div_color);
			float y_offset = m_screen_rect.Bottom + m_margin;
			
			float last_string_end = y_offset - Transform(m_data_rect, m_screen_rect, 
				new PointF(m_data_rect.Left, start_pos - major_div)).Y;
			
			for (float d_iter = start_pos; d_iter < m_data_rect.Bottom; d_iter += minor_div)
			{
				PointF pt = Transform(m_data_rect, m_screen_rect, new PointF(m_data_rect.Left, d_iter));
				pt.Y = y_offset - pt.Y;

				if (subdiv_index == 0)
				{
					g.DrawLine(pen_axes, pt, new PointF(pt.X - 5, pt.Y));
	
					string str_val = TranslateYValue(d_iter, mag+1);

					if (str_val != "")
					{
						SizeF size = g.MeasureString(str_val, m_font);
						PointF string_start = new PointF(pt.X - size.Width - 7, pt.Y - 
							(size.Height/2));
					
						if (string_start.Y + size.Height < (last_string_end - 2) || d_iter == start_pos) 
						{
							g.DrawString(str_val, m_font, brush, string_start);
							last_string_end = string_start.Y;
						}
					}
				}
				else
				{
					g.DrawLine(pen_axes, pt, new PointF(pt.X - 2, pt.Y));
				}
		
				subdiv_index = (subdiv_index + 1) % 10;
			}
		}
	}
}
