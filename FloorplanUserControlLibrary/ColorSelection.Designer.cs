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
            txtB = new TextBox();
            panel8 = new Panel();
            txtG = new TextBox();
            panel9 = new Panel();
            txtR = new TextBox();
            lbl2Before = new Label();
            lbl3Before = new Label();
            lbl1Before = new Label();
            lbl2After = new Label();
            lbl3After = new Label();
            lbl1After = new Label();
            pnlMain = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            btnApply = new Button();
            btnChooseFromPallet = new Button();
            ((System.ComponentModel.ISupportInitialize)tbB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbR).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            pnlMain.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tbB
            // 
            tbB.BackColor = Color.WhiteSmoke;
            tbB.LargeChange = 25;
            tbB.Location = new Point(73, 4);
            tbB.Maximum = 255;
            tbB.Name = "tbB";
            tbB.Size = new Size(892, 45);
            tbB.TabIndex = 0;
            tbB.Scroll += tbB_Scroll;
            // 
            // tbG
            // 
            tbG.BackColor = Color.WhiteSmoke;
            tbG.LargeChange = 25;
            tbG.Location = new Point(73, 5);
            tbG.Maximum = 255;
            tbG.Name = "tbG";
            tbG.Size = new Size(892, 45);
            tbG.TabIndex = 0;
            tbG.Scroll += tbG_Scroll;
            // 
            // tbR
            // 
            tbR.BackColor = Color.WhiteSmoke;
            tbR.LargeChange = 25;
            tbR.Location = new Point(73, 4);
            tbR.Maximum = 255;
            tbR.Name = "tbR";
            tbR.Size = new Size(892, 45);
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
            cbForeColor.Location = new Point(64, 97);
            cbForeColor.Name = "cbForeColor";
            cbForeColor.Size = new Size(869, 36);
            cbForeColor.TabIndex = 2;
            cbForeColor.Text = "Text Color White";
            cbForeColor.TextAlign = ContentAlignment.MiddleCenter;
            cbForeColor.UseVisualStyleBackColor = false;
            cbForeColor.CheckedChanged += cbForeColor_CheckedChanged;
            // 
            // flowDefaults
            // 
            flowDefaults.Location = new Point(11, 350);
            flowDefaults.Name = "flowDefaults";
            flowDefaults.Size = new Size(980, 60);
            flowDefaults.TabIndex = 3;
            // 
            // lblDefaults
            // 
            lblDefaults.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDefaults.Location = new Point(10, 322);
            lblDefaults.Name = "lblDefaults";
            lblDefaults.Size = new Size(980, 28);
            lblDefaults.TabIndex = 4;
            lblDefaults.Text = "Defaults";
            lblDefaults.TextAlign = ContentAlignment.MiddleCenter;
            lblDefaults.Click += lblDefaults_Click;
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(384, 22);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(234, 60);
            lblSectionNumber.TabIndex = 5;
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Blue;
            panel7.Controls.Add(txtB);
            panel7.Controls.Add(tbB);
            panel7.Location = new Point(4, 124);
            panel7.Name = "panel7";
            panel7.Size = new Size(972, 53);
            panel7.TabIndex = 6;
            // 
            // txtB
            // 
            txtB.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtB.Location = new Point(4, 4);
            txtB.Name = "txtB";
            txtB.Size = new Size(64, 46);
            txtB.TabIndex = 1;
            txtB.TextChanged += txtB_TextChanged;
            txtB.Leave += txtG_TextChanged;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Green;
            panel8.Controls.Add(txtG);
            panel8.Controls.Add(tbG);
            panel8.Location = new Point(4, 64);
            panel8.Name = "panel8";
            panel8.Size = new Size(972, 53);
            panel8.TabIndex = 6;
            // 
            // txtG
            // 
            txtG.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtG.Location = new Point(4, 4);
            txtG.Name = "txtG";
            txtG.Size = new Size(64, 46);
            txtG.TabIndex = 1;
            txtG.Leave += txtG_TextChanged;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Red;
            panel9.Controls.Add(txtR);
            panel9.Controls.Add(tbR);
            panel9.Location = new Point(4, 4);
            panel9.Name = "panel9";
            panel9.Size = new Size(972, 53);
            panel9.TabIndex = 6;
            // 
            // txtR
            // 
            txtR.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtR.Location = new Point(4, 4);
            txtR.Name = "txtR";
            txtR.Size = new Size(64, 46);
            txtR.TabIndex = 1;
            txtR.Leave += txtR_TextChanged;
            // 
            // lbl2Before
            // 
            lbl2Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl2Before.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl2Before.Location = new Point(131, 22);
            lbl2Before.Margin = new Padding(2);
            lbl2Before.Name = "lbl2Before";
            lbl2Before.Size = new Size(120, 60);
            lbl2Before.TabIndex = 7;
            lbl2Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl3Before
            // 
            lbl3Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl3Before.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl3Before.Location = new Point(7, 22);
            lbl3Before.Margin = new Padding(2);
            lbl3Before.Name = "lbl3Before";
            lbl3Before.Size = new Size(120, 60);
            lbl3Before.TabIndex = 7;
            lbl3Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl1Before
            // 
            lbl1Before.BackColor = Color.FromArgb(224, 224, 224);
            lbl1Before.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl1Before.Location = new Point(255, 22);
            lbl1Before.Margin = new Padding(2);
            lbl1Before.Name = "lbl1Before";
            lbl1Before.Size = new Size(120, 60);
            lbl1Before.TabIndex = 7;
            lbl1Before.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl2After
            // 
            lbl2After.BackColor = Color.FromArgb(224, 224, 224);
            lbl2After.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl2After.Location = new Point(747, 22);
            lbl2After.Margin = new Padding(2);
            lbl2After.Name = "lbl2After";
            lbl2After.Size = new Size(120, 60);
            lbl2After.TabIndex = 7;
            lbl2After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl3After
            // 
            lbl3After.BackColor = Color.FromArgb(224, 224, 224);
            lbl3After.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl3After.Location = new Point(871, 22);
            lbl3After.Margin = new Padding(2);
            lbl3After.Name = "lbl3After";
            lbl3After.Size = new Size(120, 60);
            lbl3After.TabIndex = 7;
            lbl3After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl1After
            // 
            lbl1After.BackColor = Color.FromArgb(224, 224, 224);
            lbl1After.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl1After.Location = new Point(623, 22);
            lbl1After.Margin = new Padding(2);
            lbl1After.Name = "lbl1After";
            lbl1After.Size = new Size(120, 60);
            lbl1After.TabIndex = 7;
            lbl1After.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.WhiteSmoke;
            pnlMain.Controls.Add(btnChooseFromPallet);
            pnlMain.Controls.Add(panel2);
            pnlMain.Controls.Add(lbl1After);
            pnlMain.Controls.Add(flowDefaults);
            pnlMain.Controls.Add(lbl3Before);
            pnlMain.Controls.Add(lblDefaults);
            pnlMain.Controls.Add(lbl3After);
            pnlMain.Controls.Add(cbForeColor);
            pnlMain.Controls.Add(lbl1Before);
            pnlMain.Controls.Add(lbl2After);
            pnlMain.Controls.Add(lblSectionNumber);
            pnlMain.Controls.Add(lbl2Before);
            pnlMain.Location = new Point(10, 10);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1000, 425);
            pnlMain.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel9);
            panel2.Location = new Point(10, 142);
            panel2.Name = "panel2";
            panel2.Size = new Size(980, 182);
            panel2.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(btnApply);
            panel1.Location = new Point(10, 426);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 58);
            panel1.TabIndex = 9;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(120, 180, 120);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(10, 9);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(981, 37);
            btnApply.TabIndex = 0;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // btnChooseFromPallet
            // 
            btnChooseFromPallet.BackColor = Color.White;
            btnChooseFromPallet.FlatAppearance.BorderSize = 0;
            btnChooseFromPallet.FlatStyle = FlatStyle.Flat;
            btnChooseFromPallet.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnChooseFromPallet.ForeColor = Color.White;
            btnChooseFromPallet.Image = Properties.Resources.color_palette__1_;
            btnChooseFromPallet.Location = new Point(939, 96);
            btnChooseFromPallet.Name = "btnChooseFromPallet";
            btnChooseFromPallet.Size = new Size(51, 37);
            btnChooseFromPallet.TabIndex = 0;
            btnChooseFromPallet.UseVisualStyleBackColor = false;
            btnChooseFromPallet.Click += btnChooseFromPallet_Click;
            // 
            // ColorSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            Controls.Add(panel1);
            Controls.Add(pnlMain);
            Margin = new Padding(0);
            Name = "ColorSelection";
            Size = new Size(1020, 495);
            Load += ColorSelection_Load;
            ((System.ComponentModel.ISupportInitialize)tbB).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbG).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbR).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            pnlMain.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
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
        private Panel pnlMain;
        private Panel panel2;
        private TextBox txtB;
        private TextBox txtG;
        private TextBox txtR;
        private Panel panel1;
        private Button btnApply;
        private Button btnChooseFromPallet;
    }
}
