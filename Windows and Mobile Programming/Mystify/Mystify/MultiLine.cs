using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class MultiLine : Shape
    {
        public List<PointVelocity> points = new List<PointVelocity>();
        bool connected = false;
        Random rnd = new Random();
        public MultiLine(int nLines, bool connected)
        {
            PointF pos;
            for (; nLines > 0; nLines--)
            {
                pos = new PointF(rnd.Next(Usefull.drawPanel.Width), rnd.Next(Usefull.drawPanel.Height));
                points.Add(new PointVelocity(pos,Usefull.randomVelocity(pos)));
            }
            this.connected = connected;
        }
        public override void update()
        {
            foreach (var point in points)
            {
                point.position.X += point.direction.X;
                point.position.Y += point.direction.Y;
                point.direction = Usefull.keepPointInControl(point.position, point.direction, Usefull.drawPanel);
            }
        }
        public override void draw(Graphics e)
        {
            for (int i = 0; i < points.Count-1; i++)
            {
                e.DrawLine(new Pen(c), points[i].position, points[i+1].position);
            }
            if (connected)
            {
                e.DrawLine(new Pen(c), points[0].position, points[points.Count-1].position);
            }
        }
    }
}
