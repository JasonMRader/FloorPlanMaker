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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTutorialVideos));
            axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            comboBox1 = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).BeginInit();
            SuspendLayout();
            // 
            // axWindowsMediaPlayer
            // 
            axWindowsMediaPlayer.Enabled = true;
            axWindowsMediaPlayer.Location = new Point(12, 120);
            axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            axWindowsMediaPlayer.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer.OcxState");
            axWindowsMediaPlayer.Size = new Size(1224, 847);
            axWindowsMediaPlayer.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 63);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(1224, 33);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(91, 30);
            label1.TabIndex = 2;
            label1.Text = "How To:";
            // 
            // frmTutorialVideos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1248, 1003);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(axWindowsMediaPlayer);
            Name = "frmTutorialVideos";
            Text = "frmTutorialVideos";
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private ComboBox comboBox1;
        private Label label1;
    }
}