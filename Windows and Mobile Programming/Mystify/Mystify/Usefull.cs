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
        static Random rnd = new Random();
        public static Point keepPointInControl(Point p,Point v, Control control)
        {
            if (p.X < 0)
            {
                randomVelocity(ref v);
                v.Y *= plusMinusOne();
            }

            if (p.X > control.Width)
            {
                randomVelocity(ref v);
                v.X *= -1;
                v.Y *= plusMinusOne();
            }
            if (p.Y < 0)
            {
                randomVelocity(ref v);
                v.X *= plusMinusOne();
            }
            if (p.Y > control.Height)
            {
                randomVelocity(ref p);
                v.Y *= -1;
                v.X *= plusMinusOne();
            }
            return v;
        }
        public static void randomVelocity(ref Point p)
        {
            p.X = rnd.Next(2, 10);
            p.Y = rnd.Next(2, 10);
        }
        private static int plusMinusOne()
        {
            return rnd.Next(0, 2) == 0 ? -1 : 1;
        }
    }
}
