/*
    Name: Steven Johnston, Matthew Warren
    File: Rect.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Rectangle class. A shape that has difficulties drawing with negative width/height
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
    class Rect : Shape
    {
        Color brushColor;
        public Rect(Color penColor, Color brushColor, float width) : base(penColor, width)
        {
            this.brushColor = brushColor;
        }
        /// <summary>
        /// Draws a rectangle on the graphic (in this case panel)
        /// </summary>
        /// <param name="e"></param>
        public override void drawShape(Graphics e)
        {
            base.drawShape(e);
            Rectangle newRect = rect;
            //Auto dispose of pen when done
            using (Pen myPen = new Pen(penColor, penWidth))
            {
                //If width is negative, make width positive and move x width amount
                if (rect.Width < 0)
                {
                    newRect.X = rect.X + rect.Width;
                    newRect.Width = Math.Abs(rect.Width);
                }
                //If Height is negative, make Height positive and move y width amount
                if (rect.Height < 0)
                {
                    newRect.Y = rect.Y + rect.Height;
                    newRect.Height = Math.Abs(rect.Height);
                }
                //if shape is rubber banding
                if (notFullDraw)
                {
                    //pen has dash pattern
                    myPen.DashPattern = penPattern;
                }
                else
                {
                    //shape needs fill color 
                    e.FillRectangle(new SolidBrush(brushColor), newRect);
                }
                e.DrawRectangle(myPen, newRect);
            }
        }
        /// <summary>
        /// Shape is in rubber banding state
        /// </summary>
        /// <param name="e">used to get mouse postion</param>
        public override void midDraw(MouseEventArgs e)
        {
            
            rect.Width = e.X - rect.X;
            rect.Height = e.Y - rect.Y;
        }
    }
}
