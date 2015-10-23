/*
 * Name: Steven Johnston
 * File: frmMystify.cs
 * Assignment: Mystifiy #03
 * Date: 10/23/2015
 * Description: Form for Mystify.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    public partial class frmMystify : Form
    {
        const int ONE_SECOND_IN_MILISECONDS = 1000;
        const int FRAMES_PER_SECOND = 144;
        /// <summary>
        /// The lock myst shapes
        /// </summary>
        private readonly System.Threading.Mutex lockMystShapes = new System.Threading.Mutex();
        /// <summary>
        /// The lock timers
        /// </summary>
        private readonly System.Threading.Mutex lockTimers = new System.Threading.Mutex();
        /// <summary>
        /// The myst shapes
        /// </summary>
        List<Shape> mystShapes = new List<Shape>();
        /// <summary>
        /// The timers for shapes
        /// </summary>
        List<MyTimer> timers = new List<MyTimer>();
        /// <summary>
        /// The img to be drawn to before drawing to pane
        /// </summary>
        Bitmap img;
        /// <summary>
        /// The graphic to be drawn to
        /// </summary>
        Graphics g;
        /// <summary>
        /// The Time that handles draw calls. 
        /// </summary>
        MyTimer drawTimer;
        /// <summary>
        /// Used to change the state of the lines. Pause or un-pasued (resumed)
        /// </summary>
        bool paused = false;
        /// <summary>
        /// The pane width percent
        /// </summary>
        float paneWidthPercent;
        /// <summary>
        /// The pane height percent
        /// </summary>
        float paneHeightPercent;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMystify"/> class.
        /// </summary>
        public frmMystify()
        {
            InitializeComponent();
            Usefull.speed = ONE_SECOND_IN_MILISECONDS / trcSpeed.Value;
            //Create Draw Timer. Call draw function with blank object  
            drawTimer = new MyTimer(ONE_SECOND_IN_MILISECONDS / FRAMES_PER_SECOND, draw, new object());
            //Create bitmap sized to fit the pane
            img = new Bitmap(pnlPane.Width,pnlPane.Height);
            //Create graphic from bitmap
            g = Graphics.FromImage(img);
            //Easy way to give access to pane from all classes. allowing resizing.
            Usefull.drawPanel = pnlPane;
            paneWidthPercent = (float)pnlPane.Width / this.Width;
            paneHeightPercent = (float)pnlPane.Height / this.Height;
        }
        /// <summary>
        /// Draws the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public void draw(object e)
        {
            pnlPane.Invalidate();
        }

        /// <summary>
        /// Handles the down event of the pane_mouse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void pane_mouse_down(object sender, MouseEventArgs e)
        {
            //Allow new lines if not paused
            if (!paused)
            {
                //Create new Myst
                MystLine newMyst = new MystLine(pnlPane, e.Location);
                //Create new Timer for line (calls update method of new Myst)
                MyTimer newTimer = new MyTimer(ONE_SECOND_IN_MILISECONDS / trcSpeed.Value, newMyst.update, this);
                timers.Add(newTimer);
                //Add when mystShapes if free.
                lock (lockMystShapes)
                {
                    mystShapes.Add(newMyst);
                }
            }
        }


        /// <summary>
        /// Handles the Paint event of the pnlPane control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void pnlPane_Paint(object sender, PaintEventArgs e)
        {
            lock (lockMystShapes)
            {
                //Sets bitmap to all black
                g.Clear(Color.Black);
                //Draw each mystShape on to bitmap
                foreach (var myst in mystShapes)
                {
                    myst.draw(g);
                }
            }
            //Set backgroudn to bitmap
            pnlPane.BackgroundImage = img;
        }

        /// <summary>
        /// Handles the Click event of the btnPauseResume control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPauseResume_Click(object sender, EventArgs e)
        {
            paused = !paused;
            if (paused)
            {
                btnPauseResume.Text = "Resume";
                lock (lockTimers)
                {
                    //Set all mystShape timers to paused state.
                    foreach (var time in timers)
                    {
                        time.pause();
                    }
                }
            }
            else
            {
                btnPauseResume.Text = "Pause";
                lock (lockTimers)
                {
                    //Set all mystShape timers to resumed state (running)
                    foreach (var time in timers)
                    {
                        time.resume();
                    }
                }
            }
        }
        /// <summary>
        /// Kills the line timers.
        /// </summary>
        private void killLineTimers()
        {
            lock (lockTimers)
            {
                //Stop each myst Shape timers
                foreach (var timer in timers)
                {
                    timer.stop();
                }
            }
            //Remove all timers and shapes
            timers.Clear();
            mystShapes.Clear();
        
        }
        /// <summary>
        /// Handles the Click event of the btnShutDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnShutDown_Click(object sender, EventArgs e)
        {
            killLineTimers();
        }
        /// <summary>
        /// Handles the Scroll event of the trcTrail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trcTrail_Scroll(object sender, EventArgs e)
        {
            //Change trailLength of lines
            foreach (Shape mystShape in mystShapes)
            {
                Usefull.trailLength = trcTrail.Value;
            }
        }

        /// <summary>
        /// Handles the Scroll event of the trcSpeed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trcSpeed_Scroll(object sender, EventArgs e)
        {
            lock (lockTimers)
            {
                //Change myst line timers to sleep a different amount
                foreach (var timer in timers)
                {
                    timer.DelayAmount = ONE_SECOND_IN_MILISECONDS / trcSpeed.Value;
                }
            }
        }

        /// <summary>
        /// Handles the Resize event of the frmMystify control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMystify_Resize(object sender, EventArgs e)
        {
            //Set width so it fills left of toosl panle 
            pnlPane.Width = (int)(this.Width - pnlTools.Width);
            //Set height so it keeps same ratio as when launched
            pnlPane.Height = (int)(this.Height * paneHeightPercent);
            if(pnlPane.Height > 0 && pnlPane.Width > 0)
            {
                //Resize bitmap
                img = new Bitmap(pnlPane.Width, pnlPane.Height);
                g = Graphics.FromImage(img);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the frmMystify control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmMystify_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Remove all line timers and remove the draw timer
            killLineTimers();
            drawTimer.stop();
        }
    }
}
