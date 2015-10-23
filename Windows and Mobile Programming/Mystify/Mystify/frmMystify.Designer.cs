namespace Mystify
{
    partial class frmMystify
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
            this.btnPauseResume = new System.Windows.Forms.Button();
            this.btnShutDown = new System.Windows.Forms.Button();
            this.trcTrail = new System.Windows.Forms.TrackBar();
            this.lblTrailSize = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trcSpeed = new System.Windows.Forms.TrackBar();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.pnlPane = new SETPaint.Pane();
            ((System.ComponentModel.ISupportInitialize)(this.trcTrail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trcSpeed)).BeginInit();
            this.pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPauseResume
            // 
            this.btnPauseResume.Location = new System.Drawing.Point(-1, 0);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Size = new System.Drawing.Size(154, 48);
            this.btnPauseResume.TabIndex = 1;
            this.btnPauseResume.Text = "Pause";
            this.btnPauseResume.UseVisualStyleBackColor = true;
            this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
            // 
            // btnShutDown
            // 
            this.btnShutDown.Location = new System.Drawing.Point(-1, 54);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(154, 48);
            this.btnShutDown.TabIndex = 2;
            this.btnShutDown.Text = "Shut Down";
            this.btnShutDown.UseVisualStyleBackColor = true;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // trcTrail
            // 
            this.trcTrail.Location = new System.Drawing.Point(-1, 121);
            this.trcTrail.Maximum = 255;
            this.trcTrail.Minimum = 1;
            this.trcTrail.Name = "trcTrail";
            this.trcTrail.Size = new System.Drawing.Size(153, 45);
            this.trcTrail.TabIndex = 4;
            this.trcTrail.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trcTrail.Value = 10;
            this.trcTrail.Scroll += new System.EventHandler(this.trcTrail_Scroll);
            // 
            // lblTrailSize
            // 
            this.lblTrailSize.AutoSize = true;
            this.lblTrailSize.Location = new System.Drawing.Point(-4, 105);
            this.lblTrailSize.Name = "lblTrailSize";
            this.lblTrailSize.Size = new System.Drawing.Size(50, 13);
            this.lblTrailSize.TabIndex = 5;
            this.lblTrailSize.Text = "Trail Size";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(-4, 156);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(38, 13);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "Speed";
            // 
            // trcSpeed
            // 
            this.trcSpeed.Location = new System.Drawing.Point(-1, 172);
            this.trcSpeed.Maximum = 144;
            this.trcSpeed.Minimum = 10;
            this.trcSpeed.Name = "trcSpeed";
            this.trcSpeed.Size = new System.Drawing.Size(153, 45);
            this.trcSpeed.TabIndex = 6;
            this.trcSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trcSpeed.Value = 60;
            this.trcSpeed.Scroll += new System.EventHandler(this.trcSpeed_Scroll);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.btnPauseResume);
            this.pnlTools.Controls.Add(this.lblSpeed);
            this.pnlTools.Controls.Add(this.btnShutDown);
            this.pnlTools.Controls.Add(this.trcSpeed);
            this.pnlTools.Controls.Add(this.lblTrailSize);
            this.pnlTools.Controls.Add(this.trcTrail);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTools.Location = new System.Drawing.Point(969, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(166, 568);
            this.pnlTools.TabIndex = 8;
            // 
            // pnlPane
            // 
            this.pnlPane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPane.Location = new System.Drawing.Point(0, 0);
            this.pnlPane.Name = "pnlPane";
            this.pnlPane.Size = new System.Drawing.Size(963, 562);
            this.pnlPane.TabIndex = 0;
            this.pnlPane.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPane_Paint);
            this.pnlPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pane_mouse_down);
            // 
            // frmMystify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1135, 568);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.pnlPane);
            this.Name = "frmMystify";
            this.Text = "Mystify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMystify_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pane_mouse_down);
            this.Resize += new System.EventHandler(this.frmMystify_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trcTrail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trcSpeed)).EndInit();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SETPaint.Pane pnlPane;
        private System.Windows.Forms.Button btnPauseResume;
        private System.Windows.Forms.Button btnShutDown;
        private System.Windows.Forms.TrackBar trcTrail;
        private System.Windows.Forms.Label lblTrailSize;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trcSpeed;
        private System.Windows.Forms.Panel pnlTools;
    }
}

