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

            if (notFullDraw)
            {
                //e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
                Pen myPen = new Pen(penColor, penWidth);
                myPen.DashPattern = penPattern;
                e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
            }
            else
            {
                Pen myPen = new Pen(penColor,penWidth);
                myPen.DashPattern = new float[] { 1f };
                e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
            }
        }

        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X;
            rect.Height = e.Y;
        }
    }
}
