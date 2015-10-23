/*
    Name: Steven Johnston, Matthew Warren
    File: Usefull.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: static class to hold Usefull fields and mehtods used throught the project
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    /// <summary>
    /// 
    /// </summary>
    static class Usefull
    {
        /// <summary>
        /// The trail length
        /// </summary>
        static public int trailLength = 10;
        /// <summary>
        /// The speed the update calls are called
        /// </summary>
        static public int speed = 1000;
        /// <summary>
        /// The control that is drawn to. this allows for width and height to be grabed when resize happens
        /// </summary>
        static public Control drawPanel;
        /// <summary>
        /// The random
        /// </summary>
        static Random rnd = new Random();
        /// <summary>
        /// Keeps the point the in control.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="v">The v.</param>
        /// <param name="control">The control.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Randomizes the velocity of a point.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public static PointF randomVelocity(PointF p)
        {
            p.X = rnd.Next(2, 10);
            p.Y = rnd.Next(2, 10);
            return p;
        }
        /// <summary>
        /// Pluses the minus one.
        /// </summary>
        /// <returns></returns>
        public static int plusMinusOne()
        {
            return rnd.Next(0, 2) == 0 ? -1 : 1;
        }
    }
}
