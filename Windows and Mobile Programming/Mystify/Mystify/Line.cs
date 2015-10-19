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
        Point p1;
        public Point P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        Point p2;

        public Point P2
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
        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        public Line(Point p1, Point p2,Color newC)
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
