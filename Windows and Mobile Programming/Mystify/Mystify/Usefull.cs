using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    static class Usefull
    {
        static public int trailLength = 10;
        static public int speed = 1000;
        static public Control drawPanel;
        static Random rnd = new Random();
        public static PointF keepPointInControl(PointF p,PointF v, Control control)
        {
            if (p.X < 0)
            {
                v = randomVelocity(v);
                v.Y *= plusMinusOne();
            }

            if (p.X > control.Width)
            {
                v = randomVelocity(v);
                v.X *= -1;
                v.Y *= plusMinusOne();
            }
            if (p.Y < 0)
            {
                v = randomVelocity(v);
                v.X *= plusMinusOne();
            }
            if (p.Y > control.Height)
            {
                v = randomVelocity(v);
                v.Y *= -1;
                v.X *= plusMinusOne();
            }
            return v;
        }
        public static PointF randomVelocity(PointF p)
        {
            p.X = rnd.Next(2, 10);
            p.Y = rnd.Next(2, 10);
            return p;
        }
        public static int plusMinusOne()
        {
            return rnd.Next(0, 2) == 0 ? -1 : 1;
        }
    }
}
