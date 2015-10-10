/*
    Name: Steven Johnston, Matthew Warren
    File: Ellip.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Ellipsis Shape. Data members needed to create a Ellipsis, And methods to create and draw
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
    class Ellip: Shape
    {
        Color brushColor;
        public Ellip(Color penColor,Color brushColor, float width) : base(penColor,width)
        {
            this.brushColor = brushColor;
        }
        /// <summary>
        /// Draws this Ellipsis on Graphics
        /// </summary>
        /// <param name="e">Graphics object to draw to</param>
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            //Auto dispose of pen when done
            using (Pen myPen = new Pen(penColor, penWidth))
            {
                //Ellipsis is rubber banding
                if (notFullDraw)
                {
                    myPen.DashPattern = penPattern;
                }
                else
                {
                    e.FillEllipse(new SolidBrush(brushColor), rect);
                }
                e.DrawEllipse(myPen, rect);
            }
        }
        /// <summary>
        /// Ellipsis is in rubber banding state and rectangle need to be updated
        /// </summary>
        /// <param name="e">Needed to update rectangle according to mouse postion</param>
        public override void midDraw(MouseEventArgs e)
        {
            rect.Width = e.X - rect.X;
            rect.Height = e.Y - rect.Y;
        }
    }
}
