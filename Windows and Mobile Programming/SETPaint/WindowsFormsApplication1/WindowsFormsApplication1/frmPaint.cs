using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    enum Tool: int{ line, rect, elli };
    public partial class frmPaint : Form
    {
        int selectedTool = 0;
        Type[] shapeTypes;
        PictureBox[] tools;
        Color cLine = Color.White;
        Color cFill = Color.White;
        Point mouseDown;
        Point mouseUp;
        Point mousePosition;
        Graphics g;
        List<Shape> drawObjects = new List<Shape>();
        Shape newShape = new Shape(new Color(),0f);
        float lineWidth = 5f;

        public frmPaint()
        {
            InitializeComponent();
            tools = new PictureBox[3] {pctLine, pctRect, pctEllip };
            shapeTypes = new Type[3]{typeof(Line), typeof(Rect), typeof(Ellip) };
            changeTool();
            txtThickness.Text = Convert.ToString(lineWidth);
        }

        private void pctLine_Click(object sender, EventArgs e)
        {
            selectedTool = (int)Tool.line;
            changeTool();
        }

        private void pctRect_Click(object sender, EventArgs e)
        {
            selectedTool = (int)Tool.rect;
            changeTool();
        }

        private void pctEllip_Click(object sender, EventArgs e)
        {
            selectedTool = (int)Tool.elli;
            changeTool();
        }
        
        public void changeTool()
        {
            pctSelectedTool.Image = tools[selectedTool].Image;
            if (selectedTool == (int)Tool.line)
            {
                pnlLine.Visible = true;
                pnlShape.Visible = false;
            }
            else
            {
                pnlLine.Visible = false;
                pnlShape.Visible = true;
            }
        }

        public void getColor(ref Color colour)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.AllowFullOpen = false;
            cDialog.ShowHelp = true;
            cDialog.Color = colour;
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                colour = cDialog.Color;
            }
        }

        private void ptcLineColour_Click(object sender, EventArgs e)
        {
            getColor(ref cLine);
            ptcLineColour.BackColor = cLine;
        }

        private void pctFillColour_Click(object sender, EventArgs e)
        {
            getColor(ref cFill);
            ptcLineColour.BackColor = cLine;
        }

        private void pnlPane_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            foreach (var drawObject in drawObjects)
            {
                drawObject.drawShape(g);
            }
            Pen pen = new Pen(cLine);
            //g.DrawLine(pen,mouseDown,mousePosition);
        }

        private void pnlPane_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newShape.midDraw(e);
                pnlPane.Invalidate();
            }
        }

        private void pnlPane_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = e.Location;
            newShape = (Shape)Activator.CreateInstance(shapeTypes[selectedTool], cLine, 10f);
            newShape.startDraw(e);
        }

        private void pnlPane_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = e.Location;
            drawObjects.Add(newShape);
            pnlPane.Invalidate();
        }
    }
}
