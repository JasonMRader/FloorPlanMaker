namespace FloorPlanMakerUI
{
    partial class frmLoading
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
            if (disposing && (components != null)) {
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
            components = new System.ComponentModel.Container();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(8, 8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3);
            panel2.Size = new Size(328, 328);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.statistics;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(328, 328);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 300;
            timer1.Tick += timer_Tick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(344, 344);
            panel1.TabIndex = 2;
            // 
            // frmLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(350, 350);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmLoading";
            Padding = new Padding(3);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmLoading";
            TopMost = true;
            FormClosing += FormLoading_FormClosing;
            Load += frmLoading_Load;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private Panel panel1;
        private PictureBox pictureBox1;
    }
}