namespace FloorplanUserControlLibrary
{
    partial class ColorSelection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbB = new TrackBar();
            tbG = new TrackBar();
            tbR = new TrackBar();
            cbForeColor = new CheckBox();
            flowDefaults = new FlowLayoutPanel();
            lblDefaults = new Label();
            lblSectionNumber = new Label();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            lbl2Before = new Label();
            lbl3Before = new Label();
            lbl1Before = new Label();
            lbl2After = new Label();
            lbl3After = new Label();
            lbl1After = new Label();
            ((System.ComponentModel.ISupportInitialize)tbB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbR).BeginInit();
            SuspendLayout();
            // 
            // tbB
            // 
            tbB.LargeChange = 25;
            tbB.Location = new Point(30, 298);
            tbB.Maximum = 255;
            tbB.Name = "tbB";
            tbB.Size = new Size(952, 45);
            tbB.TabIndex = 0;
            tbB.Scroll += tbB_Scroll;
            // 
            // tbG
            // 
            tbG.LargeChange = 25;
            tbG.Location = new Point(30, 247);
            tbG.Maximum = 255;
            tbG.Name = "tbG";
            tbG.Size = new Size(952, 45);
            tbG.TabIndex = 0;
            tbG.Scroll += tbG_Scroll;
            // 
            // tbR
            // 
            tbR.LargeChange = 25;
            tbR.Location = new Point(30, 196);
            tbR.Maximum = 255;
            tbR.Name = "tbR";
            tbR.Size = new Size(952, 45);
            tbR.TabIndex = 0;
            tbR.Scroll += tbR_Scroll;
            // 
            // cbForeColor
            // 
            cbForeColor.Appearance = Appearance.Button;
            cbForeColor.BackColor = Color.White;
            cbForeColor.FlatAppearance.BorderSize = 0;
            cbForeColor.FlatStyle = FlatStyle.Flat;
            cbForeColor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbForeColor.Location = new Point(8, 136);
            cbForeColor.Name = "cbForeColor";
            cbForeColor.Size = new Size(974, 36);
            cbForeColor.TabIndex = 2;
            cbForeColor.Text = "Text Color White";
            cbForeColor.TextAlign = ContentAlignment.MiddleCenter;
            cbForeColor.UseVisualStyleBackColor = false;
            cbForeColor.CheckedChanged += cbForeColor_CheckedChanged;
            // 
            // flowDefaults
            // 
            flowDefaults.Location = new Point(8, 387);
            flowDefaults.Name = "flowDefaults";
            flowDefaults.Size = new Size(974, 72);
            flowDefaults.TabIndex = 3;
            // 
            // lblDefaults
            // 
            lblDefaults.AutoSize = true;
            lblDefaults.Location = new Point(8, 362);
            lblDefaults.Name = "lblDefaults";
            lblDefaults.Size = new Size(50, 15);
            lblDefaults.TabIndex = 4;
            lblDefaults.Text = "Defaults";
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(385, 39);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(240, 23);
            lblSectionNumber.TabIndex = 5;
            lblSectionNumber.Text = "Section #1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Blue;
            panel7.Location = new Point(8, 299);
            panel7.Name = "panel7";
            panel7.Size = new Size(18, 29);
            panel7.TabIndex = 6;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Green;
            panel8.Location = new Point(8, 247);
            panel8.Name = "panel8";
            panel8.Size = new Size(18, 29);
            panel8.TabIndex = 6;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Red;
            panel9.Location = new Point(8, 196);
            panel9.Name = "panel9";
            panel9.Size = new Size(18, 29);
            panel9.TabIndex = 6;
            // 
            // lbl2Before
            // 
            lbl2Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl2Before.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl2Before.Location = new Point(132, 21);
            lbl2Before.Margin = new Padding(2);
            lbl2Before.Name = "lbl2Before";
            lbl2Before.Size = new Size(120, 60);
            lbl2Before.TabIndex = 7;
            lbl2Before.Text = "1";
            lbl2Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl3Before
            // 
            lbl3Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl3Before.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl3Before.Location = new Point(8, 21);
            lbl3Before.Margin = new Padding(2);
            lbl3Before.Name = "lbl3Before";
            lbl3Before.Size = new Size(120, 60);
            lbl3Before.TabIndex = 7;
            lbl3Before.Text = "1";
            lbl3Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl1Before
            // 
            lbl1Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl1Before.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl1Before.Location = new Point(256, 21);
            lbl1Before.Margin = new Padding(2);
            lbl1Before.Name = "lbl1Before";
            lbl1Before.Size = new Size(120, 60);
            lbl1Before.TabIndex = 7;
            lbl1Before.Text = "1";
            lbl1Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl2After
            // 
            lbl2After.BackColor = Color.FromArgb(224, 224, 224);
            lbl2After.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl2After.Location = new Point(748, 21);
            lbl2After.Margin = new Padding(2);
            lbl2After.Name = "lbl2After";
            lbl2After.Size = new Size(120, 60);
            lbl2After.TabIndex = 7;
            lbl2After.Text = "1";
            lbl2After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl3After
            // 
            lbl3After.BackColor = Color.FromArgb(224, 224, 224);
            lbl3After.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl3After.Location = new Point(872, 21);
            lbl3After.Margin = new Padding(2);
            lbl3After.Name = "lbl3After";
            lbl3After.Size = new Size(120, 60);
            lbl3After.TabIndex = 7;
            lbl3After.Text = "1";
            lbl3After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl1After
            // 
            lbl1After.BackColor = Color.FromArgb(224, 224, 224);
            lbl1After.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl1After.Location = new Point(624, 21);
            lbl1After.Margin = new Padding(2);
            lbl1After.Name = "lbl1After";
            lbl1After.Size = new Size(120, 60);
            lbl1After.TabIndex = 7;
            lbl1After.Text = "1";
            lbl1After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ColorSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl1After);
            Controls.Add(lbl3Before);
            Controls.Add(lbl3After);
            Controls.Add(lbl1Before);
            Controls.Add(lbl2After);
            Controls.Add(lbl2Before);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(lblSectionNumber);
            Controls.Add(lblDefaults);
            Controls.Add(flowDefaults);
            Controls.Add(cbForeColor);
            Controls.Add(tbR);
            Controls.Add(tbG);
            Controls.Add(tbB);
            Margin = new Padding(0);
            Name = "ColorSelection";
            Size = new Size(1000, 475);
            Load += ColorSelection_Load;
            ((System.ComponentModel.ISupportInitialize)tbB).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbG).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar tbB;
        private TrackBar tbG;
        private TrackBar tbR;
        private CheckBox cbForeColor;
        private FlowLayoutPanel flowDefaults;
        private Label lblDefaults;
        private Label lblSectionNumber;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private Label lbl2Before;
        private Label lbl3Before;
        private Label lbl1Before;
        private Label lbl2After;
        private Label lbl3After;
        private Label lbl1After;
    }
}
