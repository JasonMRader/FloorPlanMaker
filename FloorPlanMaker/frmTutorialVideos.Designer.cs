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
            ((System.ComponentModel.ISupportInitialize)pbTutorial).BeginInit();
            SuspendLayout();
            // 
            // pbTutorial
            // 
            pbTutorial.Image = Properties.TutorialResources.Form1Overview1;
            pbTutorial.Location = new Point(50, 58);
            pbTutorial.Name = "pbTutorial";
            pbTutorial.Size = new Size(1159, 919);
            pbTutorial.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTutorial.TabIndex = 3;
            pbTutorial.TabStop = false;
            // 
            // rdoGettingStarted
            // 
            rdoGettingStarted.Appearance = Appearance.Button;
            rdoGettingStarted.BackColor = Color.FromArgb(100, 130, 180);
            rdoGettingStarted.FlatAppearance.BorderSize = 0;
            rdoGettingStarted.FlatStyle = FlatStyle.Flat;
            rdoGettingStarted.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoGettingStarted.ForeColor = Color.White;
            rdoGettingStarted.Location = new Point(109, 3);
            rdoGettingStarted.Name = "rdoGettingStarted";
            rdoGettingStarted.Size = new Size(360, 40);
            rdoGettingStarted.TabIndex = 4;
            rdoGettingStarted.TabStop = true;
            rdoGettingStarted.Text = "Getting Started";
            rdoGettingStarted.TextAlign = ContentAlignment.MiddleCenter;
            rdoGettingStarted.UseVisualStyleBackColor = false;
            // 
            // rdoCreatingAShiftWalkthrough
            // 
            rdoCreatingAShiftWalkthrough.Appearance = Appearance.Button;
            rdoCreatingAShiftWalkthrough.BackColor = Color.FromArgb(100, 130, 180);
            rdoCreatingAShiftWalkthrough.FlatAppearance.BorderSize = 0;
            rdoCreatingAShiftWalkthrough.FlatStyle = FlatStyle.Flat;
            rdoCreatingAShiftWalkthrough.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoCreatingAShiftWalkthrough.ForeColor = Color.White;
            rdoCreatingAShiftWalkthrough.Location = new Point(475, 3);
            rdoCreatingAShiftWalkthrough.Name = "rdoCreatingAShiftWalkthrough";
            rdoCreatingAShiftWalkthrough.Size = new Size(394, 40);
            rdoCreatingAShiftWalkthrough.TabIndex = 4;
            rdoCreatingAShiftWalkthrough.TabStop = true;
            rdoCreatingAShiftWalkthrough.Text = "Creating a Shift Walkthrough";
            rdoCreatingAShiftWalkthrough.TextAlign = ContentAlignment.MiddleCenter;
            rdoCreatingAShiftWalkthrough.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Image = Properties.Resources.X15x;
            btnClose.Location = new Point(1158, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 23);
            btnClose.TabIndex = 5;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnNextPic
            // 
            btnNextPic.BackColor = Color.FromArgb(100, 130, 180);
            btnNextPic.FlatAppearance.BorderSize = 0;
            btnNextPic.FlatStyle = FlatStyle.Flat;
            btnNextPic.Image = Properties.Resources.forward;
            btnNextPic.Location = new Point(1215, 99);
            btnNextPic.Name = "btnNextPic";
            btnNextPic.Size = new Size(37, 289);
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
            btnPreviousPic.Location = new Point(3, 87);
            btnPreviousPic.Name = "btnPreviousPic";
            btnPreviousPic.Size = new Size(41, 289);
            btnPreviousPic.TabIndex = 6;
            btnPreviousPic.UseVisualStyleBackColor = false;
            btnPreviousPic.Click += btnPreviousPic_Click;
            // 
            // frmTutorialVideos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 1042);
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
            ((System.ComponentModel.ISupportInitialize)pbTutorial).EndInit();
            ResumeLayout(false);
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
    }
}