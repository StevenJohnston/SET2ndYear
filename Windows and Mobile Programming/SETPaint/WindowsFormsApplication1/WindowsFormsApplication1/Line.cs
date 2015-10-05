using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint 
{
    class Line : Shape
    {
        public Line() : base(new Color(), 0f)
        {
            MessageBox.Show("sss");
        }
        public Line(Color color , float width) : base(color,width)
        {
            myPen = new Pen(color, width);
        }
        public override void drawShape(Graphics e)
        {
            e.DrawLine(myPen,new Point(rect.X,rect.Y),new Point(rect.Width,rect.Height));
        }

        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X;
            rect.Height = e.Y;
        }
    }
}
