/*
    Name: Steven Johnston
    File: Shape.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Shape class to be used as a parent class to all shapes
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
    class Shape
    {
        //protected Pen myPen { get; set; }
        protected Color penColor;
        protected float penWidth;
        protected float[] penPattern;
        public bool notFullDraw { get; set; }
        protected Rectangle rect;
        public Shape()
        {
        }
        public Shape(Color color, float width)
        {
            //myPen = new Pen(color,width);
            penColor = color;
            penWidth = width;
            //myPen.DashPattern = new float[] { 2f, 0.25f };
            penPattern = new float[] { 2f, 0.25f };
            rect = new Rectangle();
        }

        public virtual void drawShape(Graphics e)
        {
            if (!notFullDraw)
            {
                //myPen = new Pen(myPen.Color, myPen.Width);
                penPattern = null;
            }
        }
        public void startDraw(MouseEventArgs e)
        {
            rect.X = e.X;
            rect.Y = e.Y;
        }

        public virtual void midDraw(MouseEventArgs e){}

    }
}
