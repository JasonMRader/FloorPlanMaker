namespace FloorPlanMakerUI
{
    partial class frmNewShiftDatePicker
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
            btnOK = new Button();
            lblIsToday = new Label();
            lblDate = new Label();
            panel1 = new Panel();
            cbIsAm = new CheckBox();
            btnForwardDay = new Button();
            btnBackDay = new Button();
            btnCancel = new Button();
            flowDiningAreas = new FlowLayoutPanel();
            flowYesterdayCounts = new FlowLayoutPanel();
            flowLastWeekdayCounts = new FlowLayoutPanel();
            panel2 = new Panel();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            panel3 = new Panel();
            flowAllServers = new FlowLayoutPanel();
            label5 = new Label();
            panel4 = new Panel();
            flowServersOnShift = new FlowLayoutPanel();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(192, 255, 192);
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.Location = new Point(31, 907);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(1205, 35);
            btnOK.TabIndex = 0;
            btnOK.Text = "OK!";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // lblIsToday
            // 
            lblIsToday.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblIsToday.ForeColor = Color.Black;
            lblIsToday.Location = new Point(446, 48);
            lblIsToday.Name = "lblIsToday";
            lblIsToday.Size = new Size(293, 31);
            lblIsToday.TabIndex = 1;
            lblIsToday.Text = "(Today)";
            lblIsToday.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDate.ForeColor = Color.Black;
            lblDate.Location = new Point(3, 9);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(1205, 39);
            lblDate.TabIndex = 1;
            lblDate.Text = "November, 3";
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(178, 87, 46);
            panel1.Controls.Add(cbIsAm);
            panel1.Controls.Add(btnForwardDay);
            panel1.Controls.Add(btnBackDay);
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(lblIsToday);
            panel1.Location = new Point(25, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(1211, 88);
            panel1.TabIndex = 2;
            // 
            // cbIsAm
            // 
            cbIsAm.Appearance = Appearance.Button;
            cbIsAm.Checked = true;
            cbIsAm.CheckState = CheckState.Checked;
            cbIsAm.FlatAppearance.BorderSize = 0;
            cbIsAm.FlatStyle = FlatStyle.Flat;
            cbIsAm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAm.Location = new Point(1030, 20);
            cbIsAm.Name = "cbIsAm";
            cbIsAm.Size = new Size(116, 45);
            cbIsAm.TabIndex = 2;
            cbIsAm.Text = "AM";
            cbIsAm.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAm.UseVisualStyleBackColor = true;
            cbIsAm.CheckedChanged += cbIsAm_CheckedChanged;
            // 
            // btnForwardDay
            // 
            btnForwardDay.BackColor = Color.FromArgb(255, 128, 128);
            btnForwardDay.FlatAppearance.BorderSize = 0;
            btnForwardDay.FlatStyle = FlatStyle.Flat;
            btnForwardDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnForwardDay.Image = Resource1.forwardArrow;
            btnForwardDay.Location = new Point(1176, 0);
            btnForwardDay.Name = "btnForwardDay";
            btnForwardDay.Size = new Size(32, 85);
            btnForwardDay.TabIndex = 0;
            btnForwardDay.UseVisualStyleBackColor = false;
            btnForwardDay.Click += btnForwardDay_Click;
            // 
            // btnBackDay
            // 
            btnBackDay.BackColor = Color.FromArgb(255, 128, 128);
            btnBackDay.FlatAppearance.BorderSize = 0;
            btnBackDay.FlatStyle = FlatStyle.Flat;
            btnBackDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackDay.Image = Resource1.BackArrow;
            btnBackDay.Location = new Point(3, 0);
            btnBackDay.Name = "btnBackDay";
            btnBackDay.Size = new Size(23, 88);
            btnBackDay.TabIndex = 0;
            btnBackDay.UseVisualStyleBackColor = false;
            btnBackDay.Click += btnBackDay_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(255, 128, 128);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.Location = new Point(31, 866);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(1205, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(109, 34);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(964, 40);
            flowDiningAreas.TabIndex = 3;
            // 
            // flowYesterdayCounts
            // 
            flowYesterdayCounts.Location = new Point(109, 73);
            flowYesterdayCounts.Name = "flowYesterdayCounts";
            flowYesterdayCounts.Size = new Size(964, 40);
            flowYesterdayCounts.TabIndex = 3;
            // 
            // flowLastWeekdayCounts
            // 
            flowLastWeekdayCounts.Location = new Point(109, 114);
            flowLastWeekdayCounts.Name = "flowLastWeekdayCounts";
            flowLastWeekdayCounts.Size = new Size(964, 40);
            flowLastWeekdayCounts.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(178, 87, 46);
            panel2.Controls.Add(flowLastWeekdayCounts);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(flowDiningAreas);
            panel2.Controls.Add(flowYesterdayCounts);
            panel2.Location = new Point(28, 130);
            panel2.Name = "panel2";
            panel2.Size = new Size(1208, 196);
            panel2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(4, 119);
            label4.Name = "label4";
            label4.Size = new Size(99, 25);
            label4.TabIndex = 4;
            label4.Text = "Last Week";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(9, 73);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 4;
            label3.Text = "Yesterday";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(479, 6);
            label1.Name = "label1";
            label1.Size = new Size(190, 25);
            label1.TabIndex = 4;
            label1.Text = "Choose Dining Areas";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(178, 87, 46);
            panel3.Controls.Add(flowAllServers);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(28, 351);
            panel3.Name = "panel3";
            panel3.Size = new Size(590, 492);
            panel3.TabIndex = 5;
            // 
            // flowAllServers
            // 
            flowAllServers.BackColor = Color.FromArgb(178, 87, 46);
            flowAllServers.Location = new Point(9, 28);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(578, 461);
            flowAllServers.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(228, 0);
            label5.Name = "label5";
            label5.Size = new Size(139, 25);
            label5.TabIndex = 0;
            label5.Text = "Servers To Add";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(178, 87, 46);
            panel4.Controls.Add(flowServersOnShift);
            panel4.Controls.Add(label2);
            panel4.Location = new Point(646, 351);
            panel4.Name = "panel4";
            panel4.Size = new Size(590, 492);
            panel4.TabIndex = 5;
            // 
            // flowServersOnShift
            // 
            flowServersOnShift.BackColor = Color.FromArgb(178, 87, 46);
            flowServersOnShift.Location = new Point(3, 28);
            flowServersOnShift.Name = "flowServersOnShift";
            flowServersOnShift.Size = new Size(584, 461);
            flowServersOnShift.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(214, 0);
            label2.Name = "label2";
            label2.Size = new Size(150, 25);
            label2.TabIndex = 0;
            label2.Text = "Servers On Shift";
            // 
            // frmNewShiftDatePicker
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 56, 82);
            CancelButton = btnCancel;
            ClientSize = new Size(1264, 970);
            Controls.Add(panel2);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmNewShiftDatePicker";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmNewShiftDatePicker";
            Load += frmNewShiftDatePicker_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnOK;
        private Label lblIsToday;
        private Label lblDate;
        private Panel panel1;
        private Button btnCancel;
        private Button btnForwardDay;
        private Button btnBackDay;
        private CheckBox cbIsAm;
        private FlowLayoutPanel flowDiningAreas;
        private FlowLayoutPanel flowYesterdayCounts;
        private FlowLayoutPanel flowLastWeekdayCounts;
        private Panel panel2;
        private Label label4;
        private Label label3;
        private Label label1;
        private Panel panel3;
        private Label label5;
        private Panel panel4;
        private Label label2;
        private FlowLayoutPanel flowAllServers;
        private FlowLayoutPanel flowServersOnShift;
    }
}