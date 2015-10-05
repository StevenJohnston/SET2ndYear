namespace SETPaint
{
    partial class frmPaint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaint));
            this.pnlToolBox = new System.Windows.Forms.Panel();
            this.pctEllip = new System.Windows.Forms.PictureBox();
            this.pctRect = new System.Windows.Forms.PictureBox();
            this.pctLine = new System.Windows.Forms.PictureBox();
            this.pnlPane = new System.Windows.Forms.Panel();
            this.frmStatbar = new System.Windows.Forms.StatusBar();
            this.mousePos = new System.Windows.Forms.StatusBarPanel();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.ptcLineColour = new System.Windows.Forms.PictureBox();
            this.pnlShape = new System.Windows.Forms.Panel();
            this.pctFillColour = new System.Windows.Forms.PictureBox();
            this.lblFillColour = new System.Windows.Forms.Label();
            this.lblLineColour = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.lblLineThickness = new System.Windows.Forms.Label();
            this.txtThickness = new System.Windows.Forms.TextBox();
            this.pctSelectedTool = new System.Windows.Forms.PictureBox();
            this.pnlToolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctEllip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctRect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mousePos)).BeginInit();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptcLineColour)).BeginInit();
            this.pnlShape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFillColour)).BeginInit();
            this.pnlLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctSelectedTool)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolBox
            // 
            this.pnlToolBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlToolBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolBox.Controls.Add(this.pctEllip);
            this.pnlToolBox.Controls.Add(this.pctRect);
            this.pnlToolBox.Controls.Add(this.pctLine);
            this.pnlToolBox.Location = new System.Drawing.Point(0, 41);
            this.pnlToolBox.Name = "pnlToolBox";
            this.pnlToolBox.Size = new System.Drawing.Size(40, 630);
            this.pnlToolBox.TabIndex = 0;
            // 
            // pctEllip
            // 
            this.pctEllip.Image = ((System.Drawing.Image)(resources.GetObject("pctEllip.Image")));
            this.pctEllip.Location = new System.Drawing.Point(0, 92);
            this.pctEllip.Name = "pctEllip";
            this.pctEllip.Size = new System.Drawing.Size(40, 40);
            this.pctEllip.TabIndex = 2;
            this.pctEllip.TabStop = false;
            this.pctEllip.Click += new System.EventHandler(this.pctEllip_Click);
            // 
            // pctRect
            // 
            this.pctRect.Image = ((System.Drawing.Image)(resources.GetObject("pctRect.Image")));
            this.pctRect.Location = new System.Drawing.Point(0, 46);
            this.pctRect.Name = "pctRect";
            this.pctRect.Size = new System.Drawing.Size(40, 40);
            this.pctRect.TabIndex = 1;
            this.pctRect.TabStop = false;
            this.pctRect.Click += new System.EventHandler(this.pctRect_Click);
            // 
            // pctLine
            // 
            this.pctLine.Image = ((System.Drawing.Image)(resources.GetObject("pctLine.Image")));
            this.pctLine.Location = new System.Drawing.Point(0, 0);
            this.pctLine.Name = "pctLine";
            this.pctLine.Size = new System.Drawing.Size(40, 40);
            this.pctLine.TabIndex = 0;
            this.pctLine.TabStop = false;
            this.pctLine.Click += new System.EventHandler(this.pctLine_Click);
            // 
            // pnlPane
            // 
            this.pnlPane.BackColor = System.Drawing.Color.White;
            this.pnlPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPane.ForeColor = System.Drawing.Color.White;
            this.pnlPane.Location = new System.Drawing.Point(40, 40);
            this.pnlPane.Name = "pnlPane";
            this.pnlPane.Size = new System.Drawing.Size(964, 630);
            this.pnlPane.TabIndex = 1;
            this.pnlPane.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPane_Paint);
            this.pnlPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPane_MouseDown);
            this.pnlPane.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlPane_MouseMove);
            this.pnlPane.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPane_MouseUp);
            // 
            // frmStatbar
            // 
            this.frmStatbar.Location = new System.Drawing.Point(0, 669);
            this.frmStatbar.Name = "frmStatbar";
            this.frmStatbar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.mousePos});
            this.frmStatbar.Size = new System.Drawing.Size(1004, 22);
            this.frmStatbar.TabIndex = 2;
            // 
            // mousePos
            // 
            this.mousePos.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.mousePos.Name = "mousePos";
            this.mousePos.Text = "Application started. No action yet.";
            this.mousePos.ToolTipText = "Last Activity";
            this.mousePos.Width = 988;
            // 
            // pnlOptions
            // 
            this.pnlOptions.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlOptions.Controls.Add(this.ptcLineColour);
            this.pnlOptions.Controls.Add(this.pnlShape);
            this.pnlOptions.Controls.Add(this.lblLineColour);
            this.pnlOptions.Controls.Add(this.pnlLine);
            this.pnlOptions.Controls.Add(this.pctSelectedTool);
            this.pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(1004, 40);
            this.pnlOptions.TabIndex = 3;
            // 
            // ptcLineColour
            // 
            this.ptcLineColour.BackColor = System.Drawing.Color.White;
            this.ptcLineColour.Location = new System.Drawing.Point(144, 2);
            this.ptcLineColour.Name = "ptcLineColour";
            this.ptcLineColour.Size = new System.Drawing.Size(36, 36);
            this.ptcLineColour.TabIndex = 6;
            this.ptcLineColour.TabStop = false;
            this.ptcLineColour.Click += new System.EventHandler(this.ptcLineColour_Click);
            // 
            // pnlShape
            // 
            this.pnlShape.Controls.Add(this.pctFillColour);
            this.pnlShape.Controls.Add(this.lblFillColour);
            this.pnlShape.Location = new System.Drawing.Point(186, 0);
            this.pnlShape.Name = "pnlShape";
            this.pnlShape.Size = new System.Drawing.Size(157, 40);
            this.pnlShape.TabIndex = 4;
            this.pnlShape.Visible = false;
            // 
            // pctFillColour
            // 
            this.pctFillColour.BackColor = System.Drawing.Color.White;
            this.pctFillColour.Location = new System.Drawing.Point(85, 2);
            this.pctFillColour.Name = "pctFillColour";
            this.pctFillColour.Size = new System.Drawing.Size(36, 36);
            this.pctFillColour.TabIndex = 4;
            this.pctFillColour.TabStop = false;
            this.pctFillColour.Click += new System.EventHandler(this.pctFillColour_Click);
            // 
            // lblFillColour
            // 
            this.lblFillColour.AutoSize = true;
            this.lblFillColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillColour.Location = new System.Drawing.Point(4, 10);
            this.lblFillColour.Name = "lblFillColour";
            this.lblFillColour.Size = new System.Drawing.Size(75, 18);
            this.lblFillColour.TabIndex = 3;
            this.lblFillColour.Text = "Fill Colour";
            // 
            // lblLineColour
            // 
            this.lblLineColour.AutoSize = true;
            this.lblLineColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineColour.Location = new System.Drawing.Point(58, 9);
            this.lblLineColour.Name = "lblLineColour";
            this.lblLineColour.Size = new System.Drawing.Size(84, 18);
            this.lblLineColour.TabIndex = 5;
            this.lblLineColour.Text = "Line Colour";
            // 
            // pnlLine
            // 
            this.pnlLine.Controls.Add(this.lblLineThickness);
            this.pnlLine.Controls.Add(this.txtThickness);
            this.pnlLine.Location = new System.Drawing.Point(186, 0);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(160, 40);
            this.pnlLine.TabIndex = 3;
            // 
            // lblLineThickness
            // 
            this.lblLineThickness.AutoSize = true;
            this.lblLineThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineThickness.Location = new System.Drawing.Point(3, 9);
            this.lblLineThickness.Name = "lblLineThickness";
            this.lblLineThickness.Size = new System.Drawing.Size(76, 18);
            this.lblLineThickness.TabIndex = 2;
            this.lblLineThickness.Text = "Thickness";
            // 
            // txtThickness
            // 
            this.txtThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThickness.Location = new System.Drawing.Point(80, 2);
            this.txtThickness.Name = "txtThickness";
            this.txtThickness.Size = new System.Drawing.Size(62, 35);
            this.txtThickness.TabIndex = 1;
            // 
            // pctSelectedTool
            // 
            this.pctSelectedTool.Location = new System.Drawing.Point(12, 0);
            this.pctSelectedTool.Name = "pctSelectedTool";
            this.pctSelectedTool.Size = new System.Drawing.Size(40, 40);
            this.pctSelectedTool.TabIndex = 0;
            this.pctSelectedTool.TabStop = false;
            // 
            // frmPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1004, 691);
            this.Controls.Add(this.pnlPane);
            this.Controls.Add(this.pnlToolBox);
            this.Controls.Add(this.frmStatbar);
            this.Controls.Add(this.pnlOptions);
            this.Name = "frmPaint";
            this.Text = "SET Paint";
            this.pnlToolBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctEllip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctRect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mousePos)).EndInit();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptcLineColour)).EndInit();
            this.pnlShape.ResumeLayout(false);
            this.pnlShape.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFillColour)).EndInit();
            this.pnlLine.ResumeLayout(false);
            this.pnlLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctSelectedTool)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolBox;
        private System.Windows.Forms.PictureBox pctEllip;
        private System.Windows.Forms.PictureBox pctRect;
        private System.Windows.Forms.PictureBox pctLine;
        private System.Windows.Forms.Panel pnlPane;

        private System.Windows.Forms.StatusBar frmStatbar;
        private System.Windows.Forms.StatusBarPanel mousePos;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.PictureBox pctSelectedTool;
        private System.Windows.Forms.TextBox txtThickness;
        private System.Windows.Forms.Label lblLineThickness;
        private System.Windows.Forms.Panel pnlShape;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Label lblFillColour;
        private System.Windows.Forms.PictureBox pctFillColour;
        private System.Windows.Forms.PictureBox ptcLineColour;
        private System.Windows.Forms.Label lblLineColour;
    }
}

