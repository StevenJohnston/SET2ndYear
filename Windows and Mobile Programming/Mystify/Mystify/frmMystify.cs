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
        public frmMystify()
        {
            InitializeComponent();
        }

        private void frmMystify_Load(object sender, EventArgs e)
        {

        }

        private void frmMystify_MouseDown(object sender, MouseEventArgs e)
        {
            MystLine newMyst = new MystLine(e.Location);
            mystLines.Add(newMyst);
            TimerCallback TimerDelegate = new TimerCallback(newMyst.update);

            System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, this.CreateGraphics(), 2000, 2000);
        }
    }
}
