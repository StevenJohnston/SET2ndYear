using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    class Shape
    {
        protected Pen myPen { get; set; }
        protected bool notFullDraw { get; set; }
        protected Rectangle rect;
        public Shape(Color color, float width)
        {
            myPen = new Pen(color,width);
            rect = new Rectangle();
        }

        public virtual void drawShape(Graphics e) { }
        public void startDraw(MouseEventArgs e)
        {
            rect.X = e.X;
            rect.Y = e.Y;
        }

        public virtual void midDraw(MouseEventArgs e){}
    }
}
