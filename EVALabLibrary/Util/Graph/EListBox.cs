using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EVALab.Util.Graph
{
    partial class EListBox : ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public EListBox()
        {
            // Set owner draw mode
            if (_myImageList == null)
            {
                System.Reflection.Assembly thisExe;
                thisExe = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream file = 
                    thisExe.GetManifestResourceStream("EVALab.Util.Graph.Icons.clipboard_sign.png");
                _myImageList = new ImageList();
                _myImageList.Images.Add(Image.FromStream(file));
            }
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            EListBoxItem item;
            Rectangle bounds = e.Bounds;
            Size imageSize = _myImageList.ImageSize;
            try
            {
                item = (EListBoxItem)Items[e.Index];
                if (item.ImageIndex != -1)
                {
                    _myImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                        bounds.Left + imageSize.Width, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                        bounds.Left, bounds.Top);
                }
            }
            catch
            {
                if (e.Index >=0)
                {
                    _myImageList.Draw(e.Graphics, bounds.Left, bounds.Top, 0);
                    e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                        new SolidBrush(e.ForeColor), bounds.Left + imageSize.Width, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                        bounds.Left, bounds.Top);
                }
            }
            base.OnDrawItem(e);
        }
    }//End of GListBox class

    public class EListBoxItem
    {
        private string _myText;
        private int _myImageIndex;
        // properties 

        public string Text
        {
            get { return _myText; }
            set { _myText = value; }
        }
        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }
        //constructor

        public EListBoxItem(string text, int index)
        {
            _myText = text;
            _myImageIndex = index;
        }
        public EListBoxItem(string text) : this(text, -1) { }
        public EListBoxItem() : this("") { }
        public override string ToString()
        {
            return _myText;
        }
    }//End of GListBoxItem class


}
