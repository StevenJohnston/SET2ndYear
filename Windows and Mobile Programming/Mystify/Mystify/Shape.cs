/*
    Name: Steven Johnston, Matthew Warren
    File: Shape.cs
    Assignment: SET Paint (#4)
    Date: 10/8/2015
    Description: Shape object that will most often be inherited from
*/
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
    class Shape
    {
        /// <summary>
        /// The color of the shape
        /// </summary>
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        
        /// <summary>
        /// The shape trail
        /// </summary>
        private int shapeTrail = 10;

        /// <summary>
        /// Gets or sets the shape trail.
        /// </summary>
        /// <value>
        /// The shape trail.
        /// </value>
        public int ShapeTrail
        {
            get
            {
                return shapeTrail;
            }

            set
            {
                shapeTrail = value;
            }
        }

        /// <summary>
        /// Draws the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public virtual void draw(Graphics e)
        {
        }
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void update()
        {
        }
    }
}
