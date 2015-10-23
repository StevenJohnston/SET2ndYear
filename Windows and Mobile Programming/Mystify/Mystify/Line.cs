/*
 * Name: Steven Johnston
 * File: line.cs
 * Assignment: Mystifiy #03
 * Date: 10/23/2015
 * Description: Holds draw function and field for a line
 */
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
        /// <summary>
        /// first point for the line
        /// </summary>
        PointF point1;

        /// <summary>
        /// Gets or sets the point1.
        /// </summary>
        /// <value>
        /// The point1.
        /// </value>
        public PointF Point1
        {
            get { return point1; }
            set { point1 = value; }
        }
        
        /// <summary>
        /// second point for the line
        /// </summary>
        PointF point2;

        /// <summary>
        /// Gets or sets the point2.
        /// </summary>
        /// <value>
        /// The point2.
        /// </value>
        public PointF Point2
        {
            get { return point2; }
            set { point2 = value; }
        }
        /// <summary>
        /// The random
        /// </summary>
        Random rnd = new Random();
        /// <summary>
        /// The trail
        /// </summary>
        List<Line> trail = new List<Line>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line(PointF p1, PointF p2)
        {
            this.point1 = p1;
            this.point2 = p2;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="newC">The new c.</param>
        public Line(PointF p1, PointF p2,Color newC)
        {
            this.point1 = p1;
            this.point2 = p2;
            Color = newC;
        }
        /// <summary>
        /// Draws this line on e Graphics
        /// </summary>
        /// <param name="e">The e.</param>
        public override void draw(Graphics e)
        {
            e.DrawLine(new Pen(Color), point1, point2);
        }
    }
}
