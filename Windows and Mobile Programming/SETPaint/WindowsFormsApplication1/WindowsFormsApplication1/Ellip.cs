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
    class Ellip: Shape
    {
        //protected SolidBrush mySBrush;
        Color brushColor;
        public Ellip(Color penColor,Color brushColor, float width) : base(penColor,width)
        {
            this.brushColor = brushColor;
            //mySBrush = new SolidBrush(brushColor);
        }
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            //e.DrawEllipse(myPen, rect);
            Pen myPen = new Pen(penColor, penWidth);
            if (notFullDraw)
            {
                myPen.DashPattern = penPattern;
                e.DrawEllipse(myPen, rect);
            }
            else
            {
                e.DrawEllipse(myPen, rect);
                e.FillEllipse(new SolidBrush(brushColor), rect);
            }
        }

        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X - rect.X;
            rect.Height = e.Y - rect.Y;
        }
    }
}
