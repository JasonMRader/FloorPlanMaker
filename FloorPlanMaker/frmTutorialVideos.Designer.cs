namespace FloorPlanMakerUI
{
    partial class frmTutorialVideos
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
            pbTutorial = new PictureBox();
            rdoGettingStarted = new RadioButton();
            rdoCreatingAShiftWalkthrough = new RadioButton();
            btnClose = new Button();
            btnNextPic = new Button();
            btnPreviousPic = new Button();
            lblIndex = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbTutorial).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            SuspendLayout();
            // 
            // pbTutorial
            // 
            pbTutorial.Image = Properties.TutorialResources.Form1Overview1;
            pbTutorial.Location = new Point(60, 46);
            pbTutorial.Name = "pbTutorial";
            pbTutorial.Size = new Size(1000, 793);
            pbTutorial.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTutorial.TabIndex = 3;
            pbTutorial.TabStop = false;
            pbTutorial.Click += pbTutorial_Click;
            // 
            // rdoGettingStarted
            // 
            rdoGettingStarted.Appearance = Appearance.Button;
            rdoGettingStarted.BackColor = Color.FromArgb(100, 130, 180);
            rdoGettingStarted.Checked = true;
            rdoGettingStarted.FlatAppearance.BorderSize = 0;
            rdoGettingStarted.FlatStyle = FlatStyle.Flat;
            rdoGettingStarted.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoGettingStarted.ForeColor = Color.White;
            rdoGettingStarted.Location = new Point(1068, 46);
            rdoGettingStarted.Name = "rdoGettingStarted";
            rdoGettingStarted.Size = new Size(184, 40);
            rdoGettingStarted.TabIndex = 4;
            rdoGettingStarted.TabStop = true;
            rdoGettingStarted.Text = "This Form";
            rdoGettingStarted.TextAlign = ContentAlignment.MiddleCenter;
            rdoGettingStarted.UseVisualStyleBackColor = false;
            rdoGettingStarted.CheckedChanged += rdoGettingStarted_CheckedChanged;
            // 
            // rdoCreatingAShiftWalkthrough
            // 
            rdoCreatingAShiftWalkthrough.Appearance = Appearance.Button;
            rdoCreatingAShiftWalkthrough.BackColor = Color.FromArgb(100, 130, 180);
            rdoCreatingAShiftWalkthrough.FlatAppearance.BorderSize = 0;
            rdoCreatingAShiftWalkthrough.FlatStyle = FlatStyle.Flat;
            rdoCreatingAShiftWalkthrough.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoCreatingAShiftWalkthrough.ForeColor = Color.White;
            rdoCreatingAShiftWalkthrough.Location = new Point(1068, 101);
            rdoCreatingAShiftWalkthrough.Name = "rdoCreatingAShiftWalkthrough";
            rdoCreatingAShiftWalkthrough.Size = new Size(184, 40);
            rdoCreatingAShiftWalkthrough.TabIndex = 4;
            rdoCreatingAShiftWalkthrough.Text = "Creating a Shift";
            rdoCreatingAShiftWalkthrough.TextAlign = ContentAlignment.MiddleCenter;
            rdoCreatingAShiftWalkthrough.UseVisualStyleBackColor = false;
            rdoCreatingAShiftWalkthrough.CheckedChanged += rdoCreatingAShiftWalkthrough_CheckedChanged;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Image = Properties.Resources.X15x;
            btnClose.Location = new Point(1205, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 23);
            btnClose.TabIndex = 5;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnNextPic
            // 
            btnNextPic.BackColor = Color.FromArgb(100, 130, 180);
            btnNextPic.FlatAppearance.BorderSize = 0;
            btnNextPic.FlatStyle = FlatStyle.Flat;
            btnNextPic.Image = Properties.Resources.forward;
            btnNextPic.Location = new Point(1151, 855);
            btnNextPic.Name = "btnNextPic";
            btnNextPic.Size = new Size(51, 117);
            btnNextPic.TabIndex = 6;
            btnNextPic.UseVisualStyleBackColor = false;
            btnNextPic.Click += btnNextPic_Click;
            // 
            // btnPreviousPic
            // 
            btnPreviousPic.BackColor = Color.FromArgb(100, 130, 180);
            btnPreviousPic.FlatAppearance.BorderSize = 0;
            btnPreviousPic.FlatStyle = FlatStyle.Flat;
            btnPreviousPic.Image = Properties.Resources.back;
            btnPreviousPic.Location = new Point(12, 855);
            btnPreviousPic.Name = "btnPreviousPic";
            btnPreviousPic.Size = new Size(53, 117);
            btnPreviousPic.TabIndex = 6;
            btnPreviousPic.UseVisualStyleBackColor = false;
            btnPreviousPic.Click += btnPreviousPic_Click;
            // 
            // lblIndex
            // 
            lblIndex.AutoSize = true;
            lblIndex.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblIndex.Location = new Point(1014, 13);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new Size(46, 30);
            lblIndex.TabIndex = 7;
            lblIndex.Text = "0/0";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(pictureBox1);
            flowLayoutPanel1.Controls.Add(pictureBox2);
            flowLayoutPanel1.Controls.Add(pictureBox3);
            flowLayoutPanel1.Controls.Add(pictureBox4);
            flowLayoutPanel1.Controls.Add(pictureBox5);
            flowLayoutPanel1.Controls.Add(pictureBox6);
            flowLayoutPanel1.Controls.Add(pictureBox7);
            flowLayoutPanel1.Location = new Point(82, 855);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1063, 117);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 112);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(152, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(143, 112);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(301, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(143, 112);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(450, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(143, 112);
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(599, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(143, 112);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Location = new Point(748, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(143, 112);
            pictureBox6.TabIndex = 0;
            pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Location = new Point(897, 3);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(143, 112);
            pictureBox7.TabIndex = 1;
            pictureBox7.TabStop = false;
            // 
            // frmTutorialVideos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1264, 1042);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(lblIndex);
            Controls.Add(btnPreviousPic);
            Controls.Add(btnNextPic);
            Controls.Add(btnClose);
            Controls.Add(rdoCreatingAShiftWalkthrough);
            Controls.Add(rdoGettingStarted);
            Controls.Add(pbTutorial);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTutorialVideos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmTutorialVideos";
            Load += frmTutorialVideos_Load;
            ((System.ComponentModel.ISupportInitialize)pbTutorial).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private ComboBox comboBox1;
        private Label label1;
        private PictureBox pbTutorial;
        private RadioButton rdoGettingStarted;
        private RadioButton rdoCreatingAShiftWalkthrough;
        private Button btnClose;
        private Button btnNextPic;
        private Button btnPreviousPic;
        private Label lblIndex;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
    }
}