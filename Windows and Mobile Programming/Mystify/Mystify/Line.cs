using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class Line : Shape
    {
        PointF p1;
        public PointF P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        PointF p2;

        public PointF P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        Random rnd = new Random();
        Color c = Color.Black;
        public Color C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        List<Line> trail = new List<Line>();
        public Line()
        { 
            
        }
        public Line(PointF p1, PointF p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        public Line(PointF p1, PointF p2,Color newC)
        {
            this.p1 = p1;
            this.p2 = p2;
            c = newC;
        }
        public override void draw(Graphics e)
        {
            e.DrawLine(new Pen(C),p1,p2);
        }
    }
}
