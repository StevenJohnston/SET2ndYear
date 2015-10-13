using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class MystLine
    {
        Line frontLine = new Line();
        Random rnd = new Random();
        Point p1Velocity;
        Point p2Velocity;
        List<Line> lines = new List<Line>();
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
            frontLine.P1 = new Point(frontLine.P1.X + p1Velocity.X, frontLine.P1.Y + p1Velocity.Y);
            frontLine.P2 = new Point(frontLine.P2.X + p2Velocity.X, frontLine.P2.Y + p2Velocity.Y);
            lines.Add(frontLine);
            if (lines.Count > 10)
            {
                lines.RemoveAt(0);
            }
            draw((Graphics)e);

        }
        public void draw(Graphics e)
        { 
            foreach (var line in lines)
            {
                line.draw(e);
            } 
        }
    }
}
