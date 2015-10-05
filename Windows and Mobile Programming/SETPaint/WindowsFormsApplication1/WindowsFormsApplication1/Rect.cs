using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    class Rect : Shape
    {
        private SolidBrush mySBrush;
        public Rect(Color color, float width) : base(color, width)
        {
            mySBrush = new SolidBrush(color);
        }
        public override void drawShape(Graphics e)
        {
            e.DrawRectangle(myPen,rect);
            if (!notFullDraw)
            {
                e.FillRectangle(mySBrush, rect);
            }
        }
        public override void midDraw(MouseEventArgs e)
        {
            
            rect.Width = e.X - rect.X;
            rect.Height = e.Y - rect.Y;
        }
    }
}
