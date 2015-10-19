using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    class MystLine : Shape
    {
        Line frontLine = new Line();
        Random rnd = new Random();
        PointVelocity P1;
        PointVelocity P2;
        Point p1Velocity;
        Point p2Velocity;
        public List<Line> lines = new List<Line>();
        SETPaint.Pane drawForm;
        public MystLine()
        {
        
        }
        public MystLine(SETPaint.Pane pane, Point start)
        {
            frontLine = new Line(start,start);
            Usefull.randomVelocity(ref p1Velocity);
            Usefull.randomVelocity(ref p2Velocity);
            drawForm = pane;
            c = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }
        public void update(object e)
        {
            lock(_m)
            {
                frontLine = new Line(new Point(frontLine.P1.X + p1Velocity.X, frontLine.P1.Y + p1Velocity.Y), new Point(frontLine.P2.X + p2Velocity.X, frontLine.P2.Y + p2Velocity.Y),c);
                p1Velocity = Usefull.keepPointInControl(frontLine.P1, p1Velocity,drawForm);
                p2Velocity = Usefull.keepPointInControl(frontLine.P2, p2Velocity, drawForm);
                for (; lines.Count > Usefull.trailLength;)
                {
                    lines.RemoveAt(0);
                }
                foreach (var line in lines)
                {
                    if (line.C.A - (255 / Usefull.trailLength) >= 0)
                    {
                        line.C = Color.FromArgb(line.C.A - (255 / Usefull.trailLength), line.C);
                    }
                    else {
                        line.C = Color.White;
                    }
                }
                lines.Add(frontLine);
            }
        }
        public override void draw(Graphics e)
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
