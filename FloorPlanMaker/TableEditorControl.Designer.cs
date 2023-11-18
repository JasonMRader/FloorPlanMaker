namespace FloorPlanMakerUI
{
    partial class TableEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSmaller = new PictureBox();
            btnBigger = new PictureBox();
            btnShorter = new PictureBox();
            btnNarrower = new PictureBox();
            btnWider = new PictureBox();
            btnTaller = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnSmaller).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnBigger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnShorter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnNarrower).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnWider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnTaller).BeginInit();
            SuspendLayout();
            // 
            // btnSmaller
            // 
            btnSmaller.BackColor = Color.FromArgb(100, 130, 180);
            btnSmaller.Image = Properties.Resources.Smaller;
            btnSmaller.Location = new Point(3, 3);
            btnSmaller.Name = "btnSmaller";
            btnSmaller.Size = new Size(46, 46);
            btnSmaller.SizeMode = PictureBoxSizeMode.Zoom;
            btnSmaller.TabIndex = 0;
            btnSmaller.TabStop = false;
            btnSmaller.Click += btnSmaller_Click;
            // 
            // btnBigger
            // 
            btnBigger.BackColor = Color.FromArgb(100, 130, 180);
            btnBigger.Image = Properties.Resources.Bigger;
            btnBigger.Location = new Point(55, 3);
            btnBigger.Name = "btnBigger";
            btnBigger.Size = new Size(46, 46);
            btnBigger.SizeMode = PictureBoxSizeMode.Zoom;
            btnBigger.TabIndex = 0;
            btnBigger.TabStop = false;
            btnBigger.Click += btnBigger_Click;
            // 
            // btnShorter
            // 
            btnShorter.BackColor = Color.FromArgb(100, 130, 180);
            btnShorter.Image = Properties.Resources.Shorter;
            btnShorter.Location = new Point(3, 115);
            btnShorter.Name = "btnShorter";
            btnShorter.Size = new Size(46, 46);
            btnShorter.SizeMode = PictureBoxSizeMode.Zoom;
            btnShorter.TabIndex = 0;
            btnShorter.TabStop = false;
            btnShorter.Click += btnShorter_Click;
            // 
            // btnNarrower
            // 
            btnNarrower.BackColor = Color.FromArgb(100, 130, 180);
            btnNarrower.Image = Properties.Resources.Narrower;
            btnNarrower.Location = new Point(3, 59);
            btnNarrower.Name = "btnNarrower";
            btnNarrower.Size = new Size(46, 46);
            btnNarrower.SizeMode = PictureBoxSizeMode.Zoom;
            btnNarrower.TabIndex = 0;
            btnNarrower.TabStop = false;
            btnNarrower.Click += btnNarrower_Click;
            // 
            // btnWider
            // 
            btnWider.BackColor = Color.FromArgb(100, 130, 180);
            btnWider.Image = Properties.Resources.Wider;
            btnWider.Location = new Point(55, 59);
            btnWider.Name = "btnWider";
            btnWider.Size = new Size(46, 46);
            btnWider.SizeMode = PictureBoxSizeMode.Zoom;
            btnWider.TabIndex = 0;
            btnWider.TabStop = false;
            btnWider.Click += btnWider_Click;
            // 
            // btnTaller
            // 
            btnTaller.BackColor = Color.FromArgb(100, 130, 180);
            btnTaller.Image = Properties.Resources.Taller;
            btnTaller.Location = new Point(55, 115);
            btnTaller.Name = "btnTaller";
            btnTaller.Size = new Size(46, 46);
            btnTaller.SizeMode = PictureBoxSizeMode.Zoom;
            btnTaller.TabIndex = 0;
            btnTaller.TabStop = false;
            btnTaller.Click += btnTaller_Click;
            // 
            // TableEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnTaller);
            Controls.Add(btnShorter);
            Controls.Add(btnWider);
            Controls.Add(btnNarrower);
            Controls.Add(btnBigger);
            Controls.Add(btnSmaller);
            Name = "TableEditorControl";
            Size = new Size(105, 168);
            ((System.ComponentModel.ISupportInitialize)btnSmaller).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnBigger).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnShorter).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnNarrower).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnWider).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnTaller).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnSmaller;
        private PictureBox btnBigger;
        private PictureBox btnShorter;
        private PictureBox btnNarrower;
        private PictureBox btnWider;
        private PictureBox btnTaller;
    }
}
