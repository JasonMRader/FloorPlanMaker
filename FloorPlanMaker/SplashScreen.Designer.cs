namespace FloorPlanMakerUI
{
    partial class SplashScreen
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
            components = new System.ComponentModel.Container();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            panel1 = new Panel();
            lblLoading = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft YaHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 67);
            label1.Name = "label1";
            label1.Size = new Size(598, 76);
            label1.TabIndex = 0;
            label1.Text = "FloorplanMaker 1.0";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(lblLoading);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(34, 34);
            panel1.Margin = new Padding(25);
            panel1.Name = "panel1";
            panel1.Size = new Size(604, 382);
            panel1.TabIndex = 1;
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblLoading.Location = new Point(260, 195);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(84, 25);
            lblLoading.TabIndex = 1;
            lblLoading.Text = "Loading.";
            // 
            // timer1
            // 
            timer1.Interval = 300;
            timer1.Tick += timer1_Tick;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(672, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            TopMost = true;
            FormClosing += SplashScreen_FormClosing;
            Load += SplashScreen_Load;
            Shown += SplashScreen_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label1;
        private Panel panel1;
        private Label lblLoading;
        private System.Windows.Forms.Timer timer1;
    }
}