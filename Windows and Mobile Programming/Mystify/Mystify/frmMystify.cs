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
        List<MystLine> mystLines = new List<MystLine>();
        List<MyTimer> timers = new List<MyTimer>();
        MyTimer drawTimer;
        public frmMystify()
        {
            InitializeComponent();
            drawTimer = new MyTimer(10,draw,this);
            
        }
        public void draw(object e)
        {
            //((Form)e).Invalidate();
            Graphics temp = ((Form)e).CreateGraphics();
            foreach (var mystline in mystLines)
            {
                mystline.draw(temp);
            }
        }

        private void frmMystify_Load(object sender, EventArgs e)
        {

        }

        private void frmMystify_MouseDown(object sender, MouseEventArgs e)
        { 
            MystLine newMyst = new MystLine(e.Location);
            MyTimer newTimer = new MyTimer(50, newMyst.update, this);
            mystLines.Add(newMyst);
        }

        private void frmMystify_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
