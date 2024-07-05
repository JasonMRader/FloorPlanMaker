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
            ((System.ComponentModel.ISupportInitialize)pbTutorial).BeginInit();
            SuspendLayout();
            // 
            // pbTutorial
            // 
            pbTutorial.Image = Properties.TutorialResources.Form1Overview1;
            pbTutorial.Location = new Point(87, 134);
            pbTutorial.Name = "pbTutorial";
            pbTutorial.Size = new Size(1066, 875);
            pbTutorial.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTutorial.TabIndex = 3;
            pbTutorial.TabStop = false;
            // 
            // rdoGettingStarted
            // 
            rdoGettingStarted.Appearance = Appearance.Button;
            rdoGettingStarted.Location = new Point(109, 86);
            rdoGettingStarted.Name = "rdoGettingStarted";
            rdoGettingStarted.Size = new Size(138, 24);
            rdoGettingStarted.TabIndex = 4;
            rdoGettingStarted.TabStop = true;
            rdoGettingStarted.Text = "Getting Started";
            rdoGettingStarted.TextAlign = ContentAlignment.MiddleCenter;
            rdoGettingStarted.UseVisualStyleBackColor = true;
            // 
            // frmTutorialVideos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1248, 1003);
            Controls.Add(rdoGettingStarted);
            Controls.Add(pbTutorial);
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
    }
}