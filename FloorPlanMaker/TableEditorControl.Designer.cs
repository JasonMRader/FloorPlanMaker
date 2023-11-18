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
            panel1 = new Panel();
            btnCopy = new PictureBox();
            btnDelete = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnSmaller).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnBigger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnShorter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnNarrower).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnWider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnTaller).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnCopy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnDelete).BeginInit();
            SuspendLayout();
            // 
            // btnSmaller
            // 
            btnSmaller.BackColor = Color.FromArgb(100, 130, 180);
            btnSmaller.Image = Properties.Resources.Smaller;
            btnSmaller.Location = new Point(3, 3);
            btnSmaller.Name = "btnSmaller";
            btnSmaller.Size = new Size(45, 45);
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
            btnBigger.Size = new Size(45, 45);
            btnBigger.SizeMode = PictureBoxSizeMode.Zoom;
            btnBigger.TabIndex = 0;
            btnBigger.TabStop = false;
            btnBigger.Click += btnBigger_Click;
            // 
            // btnShorter
            // 
            btnShorter.BackColor = Color.FromArgb(100, 130, 180);
            btnShorter.Image = Properties.Resources.Shorter;
            btnShorter.Location = new Point(3, 106);
            btnShorter.Name = "btnShorter";
            btnShorter.Size = new Size(45, 45);
            btnShorter.SizeMode = PictureBoxSizeMode.Zoom;
            btnShorter.TabIndex = 0;
            btnShorter.TabStop = false;
            btnShorter.Click += btnShorter_Click;
            // 
            // btnNarrower
            // 
            btnNarrower.BackColor = Color.FromArgb(100, 130, 180);
            btnNarrower.Image = Properties.Resources.Narrower;
            btnNarrower.Location = new Point(3, 55);
            btnNarrower.Name = "btnNarrower";
            btnNarrower.Size = new Size(45, 45);
            btnNarrower.SizeMode = PictureBoxSizeMode.Zoom;
            btnNarrower.TabIndex = 0;
            btnNarrower.TabStop = false;
            btnNarrower.Click += btnNarrower_Click;
            // 
            // btnWider
            // 
            btnWider.BackColor = Color.FromArgb(100, 130, 180);
            btnWider.Image = Properties.Resources.Wider;
            btnWider.Location = new Point(55, 55);
            btnWider.Name = "btnWider";
            btnWider.Size = new Size(45, 45);
            btnWider.SizeMode = PictureBoxSizeMode.Zoom;
            btnWider.TabIndex = 0;
            btnWider.TabStop = false;
            btnWider.Click += btnWider_Click;
            // 
            // btnTaller
            // 
            btnTaller.BackColor = Color.FromArgb(100, 130, 180);
            btnTaller.Image = Properties.Resources.Taller;
            btnTaller.Location = new Point(54, 106);
            btnTaller.Name = "btnTaller";
            btnTaller.Size = new Size(45, 45);
            btnTaller.SizeMode = PictureBoxSizeMode.Zoom;
            btnTaller.TabIndex = 0;
            btnTaller.TabStop = false;
            btnTaller.Click += btnTaller_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(btnCopy);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnSmaller);
            panel1.Controls.Add(btnTaller);
            panel1.Controls.Add(btnBigger);
            panel1.Controls.Add(btnShorter);
            panel1.Controls.Add(btnNarrower);
            panel1.Controls.Add(btnWider);
            panel1.Location = new Point(7, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(105, 209);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = Color.FromArgb(120, 180, 120);
            btnCopy.Image = Properties.Resources.copy;
            btnCopy.Location = new Point(55, 157);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(45, 45);
            btnCopy.SizeMode = PictureBoxSizeMode.Zoom;
            btnCopy.TabIndex = 1;
            btnCopy.TabStop = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(190, 80, 70);
            btnDelete.Image = Properties.Resources.Trash;
            btnDelete.Location = new Point(3, 157);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(45, 45);
            btnDelete.SizeMode = PictureBoxSizeMode.Zoom;
            btnDelete.TabIndex = 1;
            btnDelete.TabStop = false;
            // 
            // TableEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            Controls.Add(panel1);
            Name = "TableEditorControl";
            Size = new Size(118, 221);
            ((System.ComponentModel.ISupportInitialize)btnSmaller).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnBigger).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnShorter).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnNarrower).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnWider).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnTaller).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnCopy).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnDelete).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnSmaller;
        private PictureBox btnBigger;
        private PictureBox btnShorter;
        private PictureBox btnNarrower;
        private PictureBox btnWider;
        private PictureBox btnTaller;
        private Panel panel1;
        private PictureBox btnDelete;
        private PictureBox btnCopy;
    }
}
