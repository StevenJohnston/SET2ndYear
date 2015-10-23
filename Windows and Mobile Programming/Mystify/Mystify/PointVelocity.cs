/*
    Name: Steven Johnston, Matthew Warren
    File: PointVelocity.cs
    Assignment: SET Paint (#4)
    Date: 10/8/2015
    Description: allows for Point(postion) and Point(direction) to be in one object. Direction is also speed(velocity)
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class PointVelocity
    {
        /// <summary>
        /// The position of the point
        /// </summary>
        public PointF position;
        /// <summary>
        /// The direction of the point
        /// </summary>
        public PointF direction;
        /// <summary>
        /// Initializes a new instance of the <see cref="PointVelocity"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="direc">The direc.</param>
        public PointVelocity(PointF pos, PointF direc)
        {
            position = pos;
            direction = direc;
        }
        /// <summary>
        /// Moves the postion by the direction amout for both X and Y.
        /// </summary>
        public void move()
        {
            position.X += direction.X;
            position.Y += direction.Y;
        }
    }
}
