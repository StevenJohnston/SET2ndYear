using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    /// <summary>
    /// 
    /// </summary>
    class Line : Shape
    {
        /// <summary>
        /// The p1
        /// </summary>
        PointF p1;
        /// <summary>
        /// Gets or sets the p1.
        /// </summary>
        /// <value>
        /// The p1.
        /// </value>
        public PointF P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        /// <summary>
        /// The p2
        /// </summary>
        PointF p2;

        /// <summary>
        /// Gets or sets the p2.
        /// </summary>
        /// <value>
        /// The p2.
        /// </value>
        public PointF P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        /// <summary>
        /// The random
        /// </summary>
        Random rnd = new Random();
        /// <summary>
        /// The c
        /// </summary>
        Color c = Color.Black;
        /// <summary>
        /// Gets or sets the c.
        /// </summary>
        /// <value>
        /// The c.
        /// </value>
        public Color C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        /// <summary>
        /// The trail
        /// </summary>
        List<Line> trail = new List<Line>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        public Line()
        { 
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line(PointF p1, PointF p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="newC">The new c.</param>
        public Line(PointF p1, PointF p2,Color newC)
        {
            this.p1 = p1;
            this.p2 = p2;
            c = newC;
        }
        /// <summary>
        /// Draws the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public override void draw(Graphics e)
        {
            e.DrawLine(new Pen(C),p1,p2);
        }
    }
}
