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
        /// The timers
        /// </summary>
        List<MyTimer> timers = new List<MyTimer>();
        /// <summary>
        /// The img
        /// </summary>
        Bitmap img;
        /// <summary>
        /// The g
        /// </summary>
        Graphics g;
        /// <summary>
        /// The draw timer
        /// </summary>
        MyTimer drawTimer;
        /// <summary>
        /// The paused
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
            Usefull.speed = 1000 / trcSpeed.Value;
            drawTimer = new MyTimer(1000/144, draw, new object());
            //timers.Add(drawTimer);
            img = new Bitmap(pnlPane.Width,pnlPane.Height);
            g = Graphics.FromImage(img);
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
            if (!paused)
            {
                MystLine newMyst = new MystLine(pnlPane, e.Location);
                MyTimer newTimer = new MyTimer(1000 / trcSpeed.Value, newMyst.update, this);
                timers.Add(newTimer);
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
                g.Clear(Color.Black);
                foreach (var myst in mystShapes)
                {
                    myst.draw(g);
                }
            }

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
                foreach (var timer in timers)
                {
                    timer.stop();
                }
            }
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
        /// Handles the Click event of the btnDrawTriangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDrawTriangle_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Scroll event of the trcTrail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trcTrail_Scroll(object sender, EventArgs e)
        {
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
                foreach (var timer in timers)
                {
                    timer.DelayAmount = 1000 / trcSpeed.Value;
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
            pnlPane.Width = (int)(this.Width - pnlTools.Width);
            pnlPane.Height = (int)(this.Height * paneHeightPercent);
            if(pnlPane.Height > 0 && pnlPane.Width > 0)
            {
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
            killLineTimers();
            drawTimer.stop();
        }
    }
}
