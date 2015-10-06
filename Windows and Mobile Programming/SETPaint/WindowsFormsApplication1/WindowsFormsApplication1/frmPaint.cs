using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Color cLine = Color.Wheat;
        Color cFill = Color.Wheat;
        Point mouseDown;
        Point mouseUp;
        Graphics g;
        List<Shape> drawObjects = new List<Shape>();
        Shape newShape = new Shape(new Color(),0f);
        float lineWidth = 5f;
        bool objectCreated = false;
        bool mouseIsDown = false;

        public frmPaint()
        {
            InitializeComponent();
            pctLineColour.BackColor = cLine;
            pctFillColour.BackColor = cFill;
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
                pnlShape.Visible = false;
            }
            else
            {
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
            pctLineColour.BackColor = cLine;
        }

        private void pctFillColour_Click(object sender, EventArgs e)
        {
            getColor(ref cFill);
            pctFillColour.BackColor = cFill;
        }

        private void pnlPane_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            foreach (var drawObject in drawObjects)
            {
                drawObject.drawShape(g);
            }
            if (objectCreated)
            {
                newShape.drawShape(g);
            }
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
            mouseIsDown = true;
            mouseDown = e.Location;
            if (selectedTool == 0)
            {
                newShape = (Shape)Activator.CreateInstance(shapeTypes[selectedTool], cLine, lineWidth);
            }
            else
            {
                newShape = (Shape)Activator.CreateInstance(shapeTypes[selectedTool], cLine,cFill, lineWidth);
            }
            newShape.notFullDraw = true;
            newShape.startDraw(e);
            objectCreated = true;
        }

        private void pnlPane_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            mouseUp = e.Location;
            newShape.midDraw(e);
            newShape.notFullDraw = false;
            drawObjects.Add(newShape);
            objectCreated = false;
            pnlPane.Invalidate();
        }

        private void txtThickness_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lineWidth = Convert.ToInt16(txtThickness.Text);
            } catch (Exception ex)
            {
            }
        }

        private void mnuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPaint_Load(object sender, EventArgs e)
        {

        }

        private void frmPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point newLocation = new Point(this.Location.X - (mouseDown.X-e.X), this.Location.Y - (mouseDown.Y - e.Y));
                this.Location = newLocation;
            }
        }

        private void frmPaint_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = e.Location;
        }

        private void frmPaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control && drawObjects.Count > 0 && !mouseIsDown)
            {
                drawObjects.RemoveAt(drawObjects.Count - 1);
                newShape = new Shape();
                objectCreated = false;
                pnlPane.Invalidate();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "SET Paint(.sp)|*.sp";
            openFile.FilterIndex = 1;
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Stream fileIn = openFile.OpenFile();
                using (fileIn)
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    drawObjects = (List<Shape>)bformatter.Deserialize(fileIn);
                }
                pnlPane.Invalidate();
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "SET Paint(.sp)|*.sp";
            saveFile.FilterIndex = 1;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Stream fileOut = saveFile.OpenFile();
                using (fileOut)
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Serialize(fileOut, drawObjects);
                }
                pnlPane.Invalidate();
            }
        }
    }
}
