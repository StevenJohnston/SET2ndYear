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
        private readonly System.Threading.Mutex _m = new System.Threading.Mutex();
        List<Shape> mystShapes = new List<Shape>();
        List<MyTimer> timers = new List<MyTimer>();
        Bitmap img;
        Graphics g;
        MyTimer drawTimer;
        bool paused = false;
        public frmMystify()
        {
            InitializeComponent();
            Usefull.speed = 1000 / trcSpeed.Value;
            drawTimer = new MyTimer(1000/144, draw, new object());
            //timers.Add(drawTimer);
            img = new Bitmap(pnlPane.Width,pnlPane.Height);
            g = Graphics.FromImage(img);
            Usefull.drawPanel = pnlPane;
        }
        public void draw(object e)
        {
            pnlPane.Invalidate();
        }

        private void frmMystify_Load(object sender, EventArgs e)
        {

        }

        private void pane_mouse_down(object sender, MouseEventArgs e)
        {
            if (!paused)
            {
                MystLine newMyst = new MystLine(pnlPane, e.Location);
                MyTimer newTimer = new MyTimer(1000 / 144, newMyst.update, this);
                timers.Add(newTimer);
                lock (_m)
                {
                    mystShapes.Add(newMyst);
                }
            }
        }
        

        private void pnlPane_Paint(object sender, PaintEventArgs e)
        {
            lock (_m)
            {
                g.Clear(Color.Black);
                foreach (var myst in mystShapes)
                {
                    myst.draw(g);
                }
            }

            pnlPane.BackgroundImage = img;
        }

        private void btnPauseResume_Click(object sender, EventArgs e)
        {
            paused = !paused;
            if (paused)
            {
                btnPauseResume.Text = "Resume";
                foreach (var time in timers)
                {
                    time.pause();
                }
            }
            else
            {
                btnPauseResume.Text = "Pause";
                foreach (var time in timers)
                {
                    time.resume();
                }
            }
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            foreach (var timer in timers)
            {
                timer.stop();
            }
            timers.Clear();
            mystShapes.Clear();
        }

        private void btnDrawTriangle_Click(object sender, EventArgs e)
        {

        }

        private void trcTrail_Scroll(object sender, EventArgs e)
        {
            foreach (Shape mystShape in mystShapes)
            {
                Usefull.trailLength = trcTrail.Value;
            }
        }

        private void trcSpeed_Scroll(object sender, EventArgs e)
        {
            Usefull.speed = 1000/trcSpeed.Value;
        }
    }
}
