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
        //Line frontLine = new Line();
        Random rnd = new Random();
        PointVelocity P1;
        PointVelocity P2;
        public List<Line> lines = new List<Line>();
        SETPaint.Pane drawForm;
        public MystLine()
        {
        
        }
        public MystLine(SETPaint.Pane pane, Point start)
        {
            P1 = new PointVelocity(start, Usefull.randomVelocity(new PointF()));
            P2 = new PointVelocity(start, Usefull.randomVelocity(new PointF()));
            drawForm = pane;
            c = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }
        public MystLine(PointVelocity pV1, PointVelocity pV2)
        {
            P1 = pV1;
            P2 = pV2;
        }

        public void update(object e)
        {
            lock(_m)
            {
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
                }
                P1.move();
                P1.direction = Usefull.keepPointInControl(P1.position,P1.direction,Usefull.drawPanel);
                P2.move();
                P2.direction = Usefull.keepPointInControl(P2.position, P2.direction, Usefull.drawPanel);
                lines.Add(new Line(P1.position,P2.position, c));
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
