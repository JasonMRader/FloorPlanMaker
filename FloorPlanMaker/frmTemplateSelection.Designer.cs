namespace FloorPlanMaker
{
    partial class frmTemplateSelection
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
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            panel11 = new Panel();
            rdoBothPickUp = new RadioButton();
            rdoNoPickUp = new RadioButton();
            rdoYesPickUp = new RadioButton();
            panel10 = new Panel();
            rdoBothTeam = new RadioButton();
            rdoNoTeam = new RadioButton();
            rdoYesTeam = new RadioButton();
            btnDecreaseServers = new Button();
            btnIncreaseServers = new Button();
            lblServerCount = new Label();
            label2 = new Label();
            label1 = new Label();
            lblServerDisplay = new Label();
            btnClose = new Button();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            panel11.SuspendLayout();
            panel10.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(268, 375);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Location = new Point(10, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(268, 375);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Location = new Point(10, 10);
            panel3.Name = "panel3";
            panel3.Size = new Size(268, 375);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Location = new Point(10, 10);
            panel4.Name = "panel4";
            panel4.Size = new Size(268, 375);
            panel4.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(180, 190, 200);
            panel5.Controls.Add(panel1);
            panel5.Location = new Point(39, 100);
            panel5.Name = "panel5";
            panel5.Size = new Size(288, 395);
            panel5.TabIndex = 4;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(180, 190, 200);
            panel6.Controls.Add(panel4);
            panel6.Location = new Point(356, 510);
            panel6.Name = "panel6";
            panel6.Size = new Size(288, 395);
            panel6.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(180, 190, 200);
            panel7.Controls.Add(panel3);
            panel7.Location = new Point(39, 510);
            panel7.Name = "panel7";
            panel7.Size = new Size(288, 395);
            panel7.TabIndex = 6;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(180, 190, 200);
            panel8.Controls.Add(panel2);
            panel8.Location = new Point(356, 100);
            panel8.Name = "panel8";
            panel8.Size = new Size(288, 395);
            panel8.TabIndex = 7;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(180, 190, 200);
            panel9.Controls.Add(panel11);
            panel9.Controls.Add(panel10);
            panel9.Controls.Add(btnDecreaseServers);
            panel9.Controls.Add(btnIncreaseServers);
            panel9.Controls.Add(lblServerCount);
            panel9.Controls.Add(label2);
            panel9.Controls.Add(label1);
            panel9.Controls.Add(lblServerDisplay);
            panel9.Location = new Point(39, 15);
            panel9.Name = "panel9";
            panel9.Size = new Size(605, 71);
            panel9.TabIndex = 8;
            // 
            // panel11
            // 
            panel11.BackColor = Color.WhiteSmoke;
            panel11.Controls.Add(rdoBothPickUp);
            panel11.Controls.Add(rdoNoPickUp);
            panel11.Controls.Add(rdoYesPickUp);
            panel11.Location = new Point(415, 31);
            panel11.Name = "panel11";
            panel11.Size = new Size(163, 30);
            panel11.TabIndex = 3;
            // 
            // rdoBothPickUp
            // 
            rdoBothPickUp.Appearance = Appearance.Button;
            rdoBothPickUp.BackColor = Color.FromArgb(100, 130, 180);
            rdoBothPickUp.Checked = true;
            rdoBothPickUp.FlatAppearance.BorderSize = 0;
            rdoBothPickUp.FlatStyle = FlatStyle.Flat;
            rdoBothPickUp.Location = new Point(110, 3);
            rdoBothPickUp.Name = "rdoBothPickUp";
            rdoBothPickUp.Size = new Size(50, 24);
            rdoBothPickUp.TabIndex = 0;
            rdoBothPickUp.TabStop = true;
            rdoBothPickUp.Text = "Both";
            rdoBothPickUp.TextAlign = ContentAlignment.MiddleCenter;
            rdoBothPickUp.UseVisualStyleBackColor = false;
            rdoBothPickUp.CheckedChanged += rdoBothPickUp_CheckedChanged;
            // 
            // rdoNoPickUp
            // 
            rdoNoPickUp.Appearance = Appearance.Button;
            rdoNoPickUp.BackColor = Color.FromArgb(100, 130, 180);
            rdoNoPickUp.FlatAppearance.BorderSize = 0;
            rdoNoPickUp.FlatStyle = FlatStyle.Flat;
            rdoNoPickUp.Location = new Point(56, 3);
            rdoNoPickUp.Name = "rdoNoPickUp";
            rdoNoPickUp.Size = new Size(50, 24);
            rdoNoPickUp.TabIndex = 0;
            rdoNoPickUp.Text = "No";
            rdoNoPickUp.TextAlign = ContentAlignment.MiddleCenter;
            rdoNoPickUp.UseVisualStyleBackColor = false;
            rdoNoPickUp.CheckedChanged += rdoNoPickUp_CheckedChanged;
            // 
            // rdoYesPickUp
            // 
            rdoYesPickUp.Appearance = Appearance.Button;
            rdoYesPickUp.BackColor = Color.FromArgb(100, 130, 180);
            rdoYesPickUp.FlatAppearance.BorderSize = 0;
            rdoYesPickUp.FlatStyle = FlatStyle.Flat;
            rdoYesPickUp.Location = new Point(3, 3);
            rdoYesPickUp.Name = "rdoYesPickUp";
            rdoYesPickUp.Size = new Size(50, 24);
            rdoYesPickUp.TabIndex = 0;
            rdoYesPickUp.Text = "Yes";
            rdoYesPickUp.TextAlign = ContentAlignment.MiddleCenter;
            rdoYesPickUp.UseVisualStyleBackColor = false;
            rdoYesPickUp.CheckedChanged += rdoYesPickUp_CheckedChanged;
            // 
            // panel10
            // 
            panel10.BackColor = Color.WhiteSmoke;
            panel10.Controls.Add(rdoBothTeam);
            panel10.Controls.Add(rdoNoTeam);
            panel10.Controls.Add(rdoYesTeam);
            panel10.Location = new Point(226, 31);
            panel10.Name = "panel10";
            panel10.Size = new Size(163, 30);
            panel10.TabIndex = 3;
            // 
            // rdoBothTeam
            // 
            rdoBothTeam.Appearance = Appearance.Button;
            rdoBothTeam.BackColor = Color.FromArgb(100, 130, 180);
            rdoBothTeam.Checked = true;
            rdoBothTeam.FlatAppearance.BorderSize = 0;
            rdoBothTeam.FlatStyle = FlatStyle.Flat;
            rdoBothTeam.Location = new Point(110, 3);
            rdoBothTeam.Name = "rdoBothTeam";
            rdoBothTeam.Size = new Size(50, 24);
            rdoBothTeam.TabIndex = 0;
            rdoBothTeam.TabStop = true;
            rdoBothTeam.Text = "Both";
            rdoBothTeam.TextAlign = ContentAlignment.MiddleCenter;
            rdoBothTeam.UseVisualStyleBackColor = false;
            rdoBothTeam.CheckedChanged += rdoBothTeam_CheckedChanged;
            // 
            // rdoNoTeam
            // 
            rdoNoTeam.Appearance = Appearance.Button;
            rdoNoTeam.BackColor = Color.FromArgb(100, 130, 180);
            rdoNoTeam.FlatAppearance.BorderSize = 0;
            rdoNoTeam.FlatStyle = FlatStyle.Flat;
            rdoNoTeam.Location = new Point(56, 3);
            rdoNoTeam.Name = "rdoNoTeam";
            rdoNoTeam.Size = new Size(50, 24);
            rdoNoTeam.TabIndex = 0;
            rdoNoTeam.Text = "No";
            rdoNoTeam.TextAlign = ContentAlignment.MiddleCenter;
            rdoNoTeam.UseVisualStyleBackColor = false;
            rdoNoTeam.CheckedChanged += rdoNoTeam_CheckedChanged;
            // 
            // rdoYesTeam
            // 
            rdoYesTeam.Appearance = Appearance.Button;
            rdoYesTeam.BackColor = Color.FromArgb(100, 130, 180);
            rdoYesTeam.FlatAppearance.BorderSize = 0;
            rdoYesTeam.FlatStyle = FlatStyle.Flat;
            rdoYesTeam.Location = new Point(3, 3);
            rdoYesTeam.Name = "rdoYesTeam";
            rdoYesTeam.Size = new Size(50, 24);
            rdoYesTeam.TabIndex = 0;
            rdoYesTeam.Text = "Yes";
            rdoYesTeam.TextAlign = ContentAlignment.MiddleCenter;
            rdoYesTeam.UseVisualStyleBackColor = false;
            rdoYesTeam.CheckedChanged += rdoYesTeam_CheckedChanged;
            // 
            // btnDecreaseServers
            // 
            btnDecreaseServers.FlatAppearance.BorderSize = 0;
            btnDecreaseServers.FlatStyle = FlatStyle.Flat;
            btnDecreaseServers.Image = FloorPlanMakerUI.Properties.Resources.ResizedDown;
            btnDecreaseServers.Location = new Point(130, 43);
            btnDecreaseServers.Name = "btnDecreaseServers";
            btnDecreaseServers.Size = new Size(30, 12);
            btnDecreaseServers.TabIndex = 2;
            btnDecreaseServers.UseVisualStyleBackColor = true;
            btnDecreaseServers.Click += btnDecreaseServers_Click;
            // 
            // btnIncreaseServers
            // 
            btnIncreaseServers.FlatAppearance.BorderSize = 0;
            btnIncreaseServers.FlatStyle = FlatStyle.Flat;
            btnIncreaseServers.Image = FloorPlanMakerUI.Properties.Resources.ResizedUp;
            btnIncreaseServers.Location = new Point(130, 31);
            btnIncreaseServers.Name = "btnIncreaseServers";
            btnIncreaseServers.Size = new Size(30, 12);
            btnIncreaseServers.TabIndex = 2;
            btnIncreaseServers.UseVisualStyleBackColor = true;
            btnIncreaseServers.Click += btnIncreaseServers_Click;
            // 
            // lblServerCount
            // 
            lblServerCount.BackColor = Color.WhiteSmoke;
            lblServerCount.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerCount.ForeColor = Color.Black;
            lblServerCount.Location = new Point(59, 31);
            lblServerCount.Name = "lblServerCount";
            lblServerCount.Size = new Size(54, 23);
            lblServerCount.TabIndex = 1;
            lblServerCount.Text = "6";
            lblServerCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.BackColor = Color.WhiteSmoke;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(415, 9);
            label2.Name = "label2";
            label2.Size = new Size(163, 52);
            label2.TabIndex = 0;
            label2.Text = "Pick Up Section";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.WhiteSmoke;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(226, 9);
            label1.Name = "label1";
            label1.Size = new Size(163, 52);
            label1.TabIndex = 0;
            label1.Text = "Team Wait Sections";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblServerDisplay
            // 
            lblServerDisplay.BackColor = Color.WhiteSmoke;
            lblServerDisplay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerDisplay.ForeColor = Color.Black;
            lblServerDisplay.Location = new Point(23, 9);
            lblServerDisplay.Name = "lblServerDisplay";
            lblServerDisplay.Size = new Size(175, 52);
            lblServerDisplay.TabIndex = 0;
            lblServerDisplay.Text = "Server Count";
            lblServerDisplay.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(190, 80, 70);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Image = FloorPlanMakerUI.Properties.Resources.SmallSkinnyX;
            btnClose.Location = new Point(650, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(27, 27);
            btnClose.TabIndex = 9;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnCancel_Click;
            // 
            // frmTemplateSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(684, 921);
            Controls.Add(btnClose);
            Controls.Add(panel9);
            Controls.Add(panel5);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel6);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTemplateSelection";
            Text = "frmTemplateSelection";
            Load += frmTemplateSelection_Load;
            Shown += frmTemplateSelection_Shown;
            VisibleChanged += frmTemplateSelection_Shown;
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel10.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private Label lblServerCount;
        private Label label2;
        private Label label1;
        private Label lblServerDisplay;
        private Button btnDecreaseServers;
        private Button btnIncreaseServers;
        private Panel panel11;
        private RadioButton rdoBothPickUp;
        private RadioButton rdoNoPickUp;
        private RadioButton rdoYesPickUp;
        private Panel panel10;
        private RadioButton rdoBothTeam;
        private RadioButton rdoNoTeam;
        private RadioButton rdoYesTeam;
        private Button btnClose;
    }
}