using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.ROI
{
    [Serializable()]
    public class SquareROI : ROI
    {
        string id;
        double cx;

        public double Cx
        {
            get { return cx; }
            set { cx = value; }
        }
        double cy;

        public double Cy
        {
            get { return cy; }
            set { cy = value; }
        }
        double width;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        public SquareROI(string id, double cx, double cy, double width, double height)
        {
            this.id = id;
            this.cx = cx;
            this.cy = cy;
            this.height = height;
            this.width = width;
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public bool IsInRoi(double x, double y)
        {
            return (x >= this.cx - width) && (x <= this.cx + width) && (y >= this.cy - height) && (y <= this.cy + height);
        }

        public double Distance(double x, double y)
        {
            if (IsInRoi(x, y)) return 0; 
            return Math.Sqrt(Math.Pow(x-cx,2) + Math.Pow(y-cy,2));
        }
    }
}
