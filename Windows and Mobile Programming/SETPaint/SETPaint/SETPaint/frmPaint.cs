/*
    Name: Steven Johnston, Matthew Warren
    File: frmPaint.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Main form that holds all controls.
*/
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
    //Enum for referencing tools
    enum Tool: int{ line, rect, elli };
    public partial class frmPaint : Form
    {
        //Wanted to add shadow had no clue, thankfuly there is this guy
        //http://stackoverflow.com/a/16495142/5348487
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        Tool selectedTool = Tool.line; //Selected drawing tool
        Type[] shapeTypes; // Holds object types: Line, Rect , and Shape
        PictureBox[] tools; // Hold picture boxs for line, rect, and ellip
        Color cLine = Color.Black;//Defualt line color
        Color cFill = Color.Black; //defualt fill color
        Point mouseDown; //Point where mouse was first down (onMouseDown)
        Point mouseUp; //Point where mouse was up down (onMouseUp)
        List<Shape> drawObjects = new List<Shape>();// All of the objects (Shapes) to draw
        Shape newShape = new Shape(new Color(),0f);// Shape that is rubber banding 
        float lineWidth = 5f;//default line width
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
            pctSelectedTool.Image = pctLine.Image;
        }
        /// <summary>
        /// Select tool according to picture box clicked
        /// </summary>
        /// <param name="sender">IN this case the picture box that is selected</param>
        /// <param name="e"></param>
        private void toolSelect(object sender, EventArgs e)
        {
            var pictureBox = (PictureBox) sender;
            for (int i = 0; i < tools.Length; i++)
            {    
                if ( pictureBox == tools[i])
                {
                    selectedTool = (Tool)i;
                    pctSelectedTool.Image = pictureBox.Image;
                    changeTool();
                }
            }
        }
        /// <summary>
        /// show fill color box if line isnt selected
        /// </summary>
        public void changeTool()
        {
            if (selectedTool == (int)Tool.line)
            {
                pnlShape.Visible = false;
            }
            else
            {
                pnlShape.Visible = true;
            }
        }
        /// <summary>
        /// Gets user inputed color using color dialog
        /// </summary>
        /// <param name="colour">reference to the Color struct to change</param>
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
        /// <summary>
        /// Line color has been selected, change line color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptcLineColour_Click(object sender, EventArgs e)
        {
            getColor(ref cLine);
            pctLineColour.BackColor = cLine;
        }
        /// <summary>
        /// Fill color has been selected, change fill color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctFillColour_Click(object sender, EventArgs e)
        {
            getColor(ref cFill);
            pctFillColour.BackColor = cFill;
        }
        /// <summary>
        /// Main Panel for painting. Draws all shapes including the rubberbanding shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPane_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var drawObject in drawObjects)
            {
                drawObject.drawShape(g);
            }
            if (mouseIsDown)
            {
                newShape.drawShape(g);
            }
        }
        
        /// <summary>
        /// Update the rubber banding shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPane_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && newShape != null)
            {
                lblMouse.Text = e.X + ", "+ e.Y+ " px";
                newShape.midDraw(e);
                pnlPane.Invalidate();
            }
        }
        /// <summary>
        /// Handles mouse down on main panel. Creates rubbebanding shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPane_MouseDown(object sender, MouseEventArgs e)
        {
            pnlPane.Focus();
            mouseIsDown = true;
            mouseDown = e.Location;
            if (selectedTool == Tool.line)//Line takes different parameters
            {
                //Uses shapeTypes to get the shape type of selected tool
                newShape = (Shape)Activator.CreateInstance(shapeTypes[(int)selectedTool], cLine, lineWidth);
            }
            else
            {
                //Uses shapeTypes to get the shape type of selected tool
                newShape = (Shape)Activator.CreateInstance(shapeTypes[(int)selectedTool], cLine,cFill, lineWidth);
            }
            newShape.notFullDraw = true;
            newShape.startDraw(e);
        }
        /// <summary>
        /// On main panel mouse up. Add rubberbanding shape to finished shapes list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPane_MouseUp(object sender, MouseEventArgs e)
        {
            //When clicking on a dialog or some sort, mouseUp would sometimes get triggered 
            //with out mouseDown being called
            if (mouseIsDown)
            {
                lblMouse.Text = "";
                mouseIsDown = false;
                mouseUp = e.Location;
                newShape.midDraw(e);
                newShape.notFullDraw = false;
                drawObjects.Add(newShape);
                pnlPane.Invalidate();
            }
        }
        /// <summary>
        /// update tickness of line when changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThickness_TextChanged(object sender, EventArgs e)
        {
            //Check if line thickness is num
            try
            {
                lineWidth = Convert.ToInt16(txtThickness.Text);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is outside the range of the Int32 type.", txtThickness.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("The {0} value '{1}' is not in a recognizable format.",
                                  txtThickness.Text.GetType().Name, txtThickness.Text);
            }
        }
        /// <summary>
        /// Closes program when X is clicked in corner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// If left mouse is down then move the form with the cursor. Simulates title bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point newLocation = new Point(this.Location.X - (mouseDown.X-e.X), this.Location.Y - (mouseDown.Y - e.Y));
                this.Location = newLocation;
            }
        }
        /// <summary>
        /// Get postion of mouse when down. so that forms postion can be adjusted correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPaint_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = e.Location;
        }
        //Check for Ctrl+Z to remove last shape added
        private void frmPaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control && drawObjects.Count > 0 && !mouseIsDown)
            {
                drawObjects.RemoveAt(drawObjects.Count - 1);
                newShape = new Shape();
                mouseIsDown = false;
                pnlPane.Invalidate();
            }
        }
        /// <summary>
        /// Close program when exit menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Open file to load up saved drawings. deserializes file to drawObjects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "SET Paint(.sp)|*.sp";
            openFile.FilterIndex = 1;
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("Opening document will close current without saving \nAre you sure you want to do this?", "Open Document", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    drawObjects.Clear();//Remove all current shapes
                    Stream fileIn = openFile.OpenFile();
                    using (fileIn)
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//binary formatter 

                        drawObjects = (List<Shape>)bformatter.Deserialize(fileIn);//Create all shapes
                    }
                    pnlPane.Invalidate();
                }
                lblTitle.Text = "SET Paint - " + openFile.FileName.Substring(openFile.FileName.LastIndexOf(@"\") + 1);//Gets string after last '\' in path
                this.Text = lblTitle.Text; //Change name in task bar
            }
        }
        /// <summary>
        /// Save drawObjects list to file using binary formatter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//binary 
                    bformatter.Serialize(fileOut, drawObjects);//serialize drawObject to fileOut stream
                }
                pnlPane.Invalidate();
            }
            
            lblTitle.Text = "SET Paint - " + saveFile.FileName.Substring(saveFile.FileName.LastIndexOf(@"\") + 1);//Gets string after last '\' in path
            this.Text = lblTitle.Text; //Change name in task bar
        }
        /// <summary>
        /// Remove all shapes if user agrees.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (drawObjects.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Opening new document will close current without saving \nAre you sure you want to do this?", "New Document", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    drawObjects.Clear();
                    pnlPane.Invalidate();
                    lblTitle.Text = "SET Paint - New Document";
                    this.Text = lblTitle.Text;
                }
            }
        }
        /// <summary>
        /// Opens about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();//Well, thats cool
        }
        /// <summary>
        /// Minimizes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// When mouse enters a tool change the colour
        /// </summary>
        /// <param name="sender">Picture box that is hoverd</param>
        /// <param name="e"></param>
        private void pctTool_MouseEnter(object sender, EventArgs e)
        {
            PictureBox myPic = (PictureBox)sender;
            myPic.BackColor = Color.DarkGray;
        }
        /// <summary>
        /// When mouse leaves a tool change the colour
        /// </summary>
        /// <param name="sender">Picture box that need to be "uncoloured"</param>
        /// <param name="e"></param>
        private void pctTool_MouseLeave(object sender, EventArgs e)
        {
            PictureBox myPic = (PictureBox)sender;
            myPic.BackColor = Color.FromName("Control Dark Dark");
        }
    }
}
