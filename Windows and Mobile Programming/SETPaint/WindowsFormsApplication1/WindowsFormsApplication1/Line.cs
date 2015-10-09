/*
    Name: Steven Johnston
    File: Line.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Line Shape. Data members needed to create a line, And methods to create and draw
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    [Serializable]
    class Line : Shape
    {
        public Line() : base(new Color(), 0f)
        {
            
        }
        public Line(Color color , float width) : base(color,width)
        {
        }
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            using (Pen myPen = new Pen(penColor, penWidth))
            {
                if (notFullDraw)
                {
                    //e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
                    //Pen myPen = new Pen(penColor, penWidth);
                    myPen.DashPattern = penPattern;
                    e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
                }
                else
                {
                    //Pen myPen = new Pen(penColor, penWidth);
                    myPen.DashPattern = new float[] { 1f };
                    e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
                }
            }
        }

        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X;
            rect.Height = e.Y;
        }
    }
}
