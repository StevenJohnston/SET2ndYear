/*
    Name: Steven Johnston, Matthew Warren
    File: Line.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Line Shape. Data members needed to create a line, And methods to create and draw
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    [Serializable]
    class Line : Shape
    {
        public Line(Color color , float width) : base(color,width)
        {
        }
        /// <summary>
        /// Draws this line on the Graphic 
        /// </summary>
        /// <param name="e">Graphic to draw on</param>
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            //Auto dispose of pen when done
            using (Pen myPen = new Pen(penColor, penWidth))
            {
                //line is rubber banding
                if (notFullDraw)
                {
                    //add dash pattern
                    myPen.DashPattern = penPattern;
                }
                else
                {
                    //remove dash pattern
                    myPen.DashPattern = new float[] { 1f };
                }

                e.DrawLine(myPen, new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));
            }
        }
        /// <summary>
        /// Line is in ruber banding state. update end point of line
        /// </summary>
        /// <param name="e">used to set end of line to mouse postion</param>
        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X;
            rect.Height = e.Y;
        }
    }
}
