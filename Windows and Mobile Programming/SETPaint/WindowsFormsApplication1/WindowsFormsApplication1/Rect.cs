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
    class Rect : Shape
    {
        //private SolidBrush mySBrush;
        Color brushColor;
        public Rect(Color penColor, Color brushColor, float width) : base(penColor, width)
        {
            this.brushColor = brushColor;
            //mySBrush = new SolidBrush(brushColor);
        }
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            Rectangle newRect = rect;
            using (Pen myPen = new Pen(penColor, penWidth))
            {
                if (rect.Width < 0)
                {
                    newRect.X = rect.X + rect.Width;
                    newRect.Width = Math.Abs(rect.Width);
                }
                if (rect.Height < 0)
                {
                    newRect.Y = rect.Y + rect.Height;
                    newRect.Height = Math.Abs(rect.Height);
                }
                //e.DrawRectangle(myPen, newRect);

                if (notFullDraw)
                {
                    myPen.DashPattern = penPattern;
                    e.DrawRectangle(myPen, newRect);
                }
                else
                {
                    e.DrawRectangle(myPen, newRect);
                    e.FillRectangle(new SolidBrush(brushColor), newRect);
                }
            }
        }
        
        public override void midDraw(MouseEventArgs e)
        {
            
            rect.Width = e.X - rect.X;
            rect.Height = e.Y - rect.Y;
        }
    }
}
