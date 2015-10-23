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
    class MystLine : Shape
    {
        /// <summary>
        /// The random
        /// </summary>
        Random rnd = new Random();
        /// <summary>
        /// The point and Velocity of first point
        /// </summary>
        PointVelocity pointVelocity1;
        /// <summary>
        /// The point and Velocity of first point
        /// </summary>
        PointVelocity pointVelocity2;
        /// <summary>
        /// The lines
        /// </summary>
        public List<Line> lines = new List<Line>();
        /// <summary>
        /// Initializes a new instance of the <see cref="MystLine" /> class.
        /// </summary>
        public MystLine()
        {
        
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MystLine" /> class.
        /// </summary>
        /// <param name="pane">The pane.</param>
        /// <param name="start">The start.</param>
        public MystLine(SETPaint.Pane pane, Point start)
        {
            //Set poistion and speed for point one
            pointVelocity1 = new PointVelocity(start, Usefull.randomVelocity(new PointF()));
            pointVelocity1.direction.X *= Usefull.plusMinusOne();
            pointVelocity1.direction.Y *= Usefull.plusMinusOne();
            //Set poistion and speed for point two
            pointVelocity2 = new PointVelocity(start, Usefull.randomVelocity(new PointF()));
            pointVelocity2.direction.X *= Usefull.plusMinusOne();
            pointVelocity2.direction.Y *= Usefull.plusMinusOne();
            //Create random colour MIGHT BE BLACK 
            c = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }

        /// <summary>
        /// Updates line postion, line trail, and line colour.
        /// </summary>
        /// <param name="e">The e.</param>
        public void update(object e)
        {
            lock(linesLock)
            {
                //removes all lines after the maximum number of trail lines
                for (; lines.Count > Usefull.trailLength;)
                {
                    lines.RemoveAt(0);
                }
                //Updares the colour of each line. Making them slightly less visable be decresing the Alpha value
                foreach (var line in lines)
                {
                    if (line.C.A - (255 / Usefull.trailLength) >= 0)
                    {
                        line.C = Color.FromArgb(line.C.A - (255 / Usefull.trailLength), line.C);
                    }
                }
                //Move point one and keep it in the control(Panle)
                pointVelocity1.move();
                pointVelocity1.direction = Usefull.keepPointInControl(pointVelocity1.position, pointVelocity1.direction, Usefull.drawPanel);
                //Move point two and keep it in the control(Panle)
                pointVelocity2.move();
                pointVelocity2.direction = Usefull.keepPointInControl(pointVelocity2.position, pointVelocity2.direction, Usefull.drawPanel);
                //Add new line(The front line in the Trail) to the list
                lines.Add(new Line(pointVelocity1.position,pointVelocity2.position, c));
            }
        }
        /// <summary>
        /// Calls the draw function of each line in the trail.
        /// </summary>
        /// <param name="e">The e.</param>
        public override void draw(Graphics e)
        {
            lock(linesLock)
            {
                foreach (var line in lines)
                {
                    line.draw(e);
                }
            }
        }
        /// <summary>
        /// The linesLock used to lock the lines list.
        /// </summary>
        private readonly System.Threading.Mutex linesLock = new System.Threading.Mutex();
    }
}
