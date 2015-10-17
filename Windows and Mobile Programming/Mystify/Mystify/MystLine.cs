using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    class MystLine
    {
        Line frontLine = new Line();
        Random rnd = new Random();
        Point p1Velocity;
        Point p2Velocity;
        public List<Line> lines = new List<Line>();
        Graphics e;
        public MystLine()
        {
        
        }
        public MystLine(Point start)
        {
            frontLine = new Line(start,start);
            p1Velocity = new Point(rnd.Next(-10,10),rnd.Next(-10,10));
            p2Velocity = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
        }
        public void update(object e)
        {
            lock(_m)
            {
                frontLine = new Line(new Point(frontLine.P1.X + p1Velocity.X, frontLine.P1.Y + p1Velocity.Y), new Point(frontLine.P2.X + p2Velocity.X, frontLine.P2.Y + p2Velocity.Y));
                lines.Add(frontLine);
                if (lines.Count > 10)
                {
                    ((Form)e).CreateGraphics().DrawLine(new Pen(Color.White),lines[0].P1,lines[0].P2);
                    lines.RemoveAt(0);
                }
            }
            //((Form)e).Invalidate();
            //draw(((Form)e).CreateGraphics());
        }
        public void draw(Graphics e)
        {
            lock(_m)
            {
                foreach (var line in lines)
                {
                    line.draw(e);
                }
            }
        }
        private readonly System.Threading.Mutex _m = new System.Threading.Mutex();
    }
}
