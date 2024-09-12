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
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            cbForeColor = new CheckBox();
            flowDefaults = new FlowLayoutPanel();
            lblDefaults = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            lblSectionNumber = new Label();
            ((System.ComponentModel.ISupportInitialize)tbB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbR).BeginInit();
            SuspendLayout();
            // 
            // tbB
            // 
            tbB.LargeChange = 25;
            tbB.Location = new Point(3, 341);
            tbB.Maximum = 255;
            tbB.Name = "tbB";
            tbB.Size = new Size(974, 45);
            tbB.TabIndex = 0;
            tbB.Scroll += tbB_Scroll;
            // 
            // tbG
            // 
            tbG.LargeChange = 25;
            tbG.Location = new Point(3, 290);
            tbG.Maximum = 255;
            tbG.Name = "tbG";
            tbG.Size = new Size(974, 45);
            tbG.TabIndex = 0;
            tbG.Scroll += tbG_Scroll;
            // 
            // tbR
            // 
            tbR.LargeChange = 25;
            tbR.Location = new Point(3, 239);
            tbR.Maximum = 255;
            tbR.Name = "tbR";
            tbR.Size = new Size(974, 45);
            tbR.TabIndex = 0;
            tbR.Scroll += tbR_Scroll;
            // 
            // panel1
            // 
            panel1.Location = new Point(10, 71);
            panel1.Name = "panel1";
            panel1.Size = new Size(72, 58);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Location = new Point(86, 71);
            panel2.Name = "panel2";
            panel2.Size = new Size(72, 58);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Location = new Point(164, 71);
            panel3.Name = "panel3";
            panel3.Size = new Size(72, 58);
            panel3.TabIndex = 1;
            // 
            // cbForeColor
            // 
            cbForeColor.Appearance = Appearance.Button;
            cbForeColor.BackColor = Color.White;
            cbForeColor.FlatAppearance.BorderSize = 0;
            cbForeColor.FlatStyle = FlatStyle.Flat;
            cbForeColor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbForeColor.Location = new Point(3, 187);
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
            flowDefaults.Location = new Point(3, 430);
            flowDefaults.Name = "flowDefaults";
            flowDefaults.Size = new Size(974, 72);
            flowDefaults.TabIndex = 3;
            // 
            // lblDefaults
            // 
            lblDefaults.AutoSize = true;
            lblDefaults.Location = new Point(3, 405);
            lblDefaults.Name = "lblDefaults";
            lblDefaults.Size = new Size(50, 15);
            lblDefaults.TabIndex = 4;
            lblDefaults.Text = "Defaults";
            // 
            // panel4
            // 
            panel4.Location = new Point(736, 71);
            panel4.Name = "panel4";
            panel4.Size = new Size(72, 58);
            panel4.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Location = new Point(812, 71);
            panel5.Name = "panel5";
            panel5.Size = new Size(72, 58);
            panel5.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Location = new Point(890, 71);
            panel6.Name = "panel6";
            panel6.Size = new Size(72, 58);
            panel6.TabIndex = 1;
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(242, 91);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(488, 23);
            lblSectionNumber.TabIndex = 5;
            lblSectionNumber.Text = "Section #1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ColorSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblSectionNumber);
            Controls.Add(lblDefaults);
            Controls.Add(flowDefaults);
            Controls.Add(cbForeColor);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(tbR);
            Controls.Add(tbG);
            Controls.Add(tbB);
            Margin = new Padding(0);
            Name = "ColorSelection";
            Size = new Size(1000, 515);
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
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private CheckBox cbForeColor;
        private FlowLayoutPanel flowDefaults;
        private Label lblDefaults;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Label lblSectionNumber;
    }
}
