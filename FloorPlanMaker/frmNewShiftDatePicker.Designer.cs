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
            components = new System.ComponentModel.Container();
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
            flowLast4 = new FlowLayoutPanel();
            lblLast4 = new Label();
            lblLastWeek = new Label();
            lblDayBefore = new Label();
            label1 = new Label();
            panel3 = new Panel();
            txtServerSearch = new TextBox();
            flowAllServers = new FlowLayoutPanel();
            label6 = new Label();
            label5 = new Label();
            pbAddPerson = new PictureBox();
            btnImportFromHotSchedules = new Button();
            panel4 = new Panel();
            btnSubtractBartender = new Button();
            btnAddBartender = new Button();
            flowServersOnShift = new FlowLayoutPanel();
            lblBartenderCount = new Label();
            label2 = new Label();
            lblServersOnShift = new Label();
            lblMissingServers = new Label();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAddPerson).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(120, 180, 120);
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.Location = new Point(25, 922);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(1208, 35);
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
            lblDate.BackColor = Color.FromArgb(100, 130, 180);
            lblDate.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(268, 9);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(674, 39);
            lblDate.TabIndex = 1;
            lblDate.Text = "November, 3";
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblDate, "Click To Select Date");
            lblDate.Click += lblDate_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(cbIsAm);
            panel1.Controls.Add(btnForwardDay);
            panel1.Controls.Add(btnBackDay);
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(lblIsToday);
            panel1.Location = new Point(12, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(1234, 88);
            panel1.TabIndex = 2;
            // 
            // cbIsAm
            // 
            cbIsAm.Appearance = Appearance.Button;
            cbIsAm.BackColor = Color.FromArgb(251, 175, 0);
            cbIsAm.Checked = true;
            cbIsAm.CheckState = CheckState.Checked;
            cbIsAm.FlatAppearance.BorderSize = 0;
            cbIsAm.FlatStyle = FlatStyle.Flat;
            cbIsAm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAm.Image = Properties.Resources.smallSunrise;
            cbIsAm.Location = new Point(1030, 20);
            cbIsAm.Name = "cbIsAm";
            cbIsAm.Size = new Size(116, 45);
            cbIsAm.TabIndex = 2;
            cbIsAm.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbIsAm, "Toggle AM /PM");
            cbIsAm.UseVisualStyleBackColor = false;
            cbIsAm.CheckedChanged += cbIsAm_CheckedChanged;
            // 
            // btnForwardDay
            // 
            btnForwardDay.BackColor = Color.FromArgb(100, 130, 180);
            btnForwardDay.Dock = DockStyle.Right;
            btnForwardDay.FlatAppearance.BorderSize = 0;
            btnForwardDay.FlatStyle = FlatStyle.Flat;
            btnForwardDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnForwardDay.Image = Properties.Resources.forward;
            btnForwardDay.Location = new Point(1202, 0);
            btnForwardDay.Name = "btnForwardDay";
            btnForwardDay.Size = new Size(32, 88);
            btnForwardDay.TabIndex = 0;
            toolTip1.SetToolTip(btnForwardDay, "Next Day");
            btnForwardDay.UseVisualStyleBackColor = false;
            btnForwardDay.Click += btnForwardDay_Click;
            // 
            // btnBackDay
            // 
            btnBackDay.BackColor = Color.FromArgb(100, 130, 180);
            btnBackDay.Dock = DockStyle.Left;
            btnBackDay.FlatAppearance.BorderSize = 0;
            btnBackDay.FlatStyle = FlatStyle.Flat;
            btnBackDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackDay.Image = Properties.Resources.back;
            btnBackDay.Location = new Point(0, 0);
            btnBackDay.Name = "btnBackDay";
            btnBackDay.Size = new Size(32, 88);
            btnBackDay.TabIndex = 0;
            toolTip1.SetToolTip(btnBackDay, "Previous Day");
            btnBackDay.UseVisualStyleBackColor = false;
            btnBackDay.Click += btnBackDay_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(190, 80, 70);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.Location = new Point(25, 881);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(1208, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.BackColor = Color.WhiteSmoke;
            flowDiningAreas.Location = new Point(144, 34);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(1068, 40);
            flowDiningAreas.TabIndex = 3;
            // 
            // flowYesterdayCounts
            // 
            flowYesterdayCounts.BackColor = Color.WhiteSmoke;
            flowYesterdayCounts.Location = new Point(144, 73);
            flowYesterdayCounts.Name = "flowYesterdayCounts";
            flowYesterdayCounts.Size = new Size(1068, 40);
            flowYesterdayCounts.TabIndex = 3;
            // 
            // flowLastWeekdayCounts
            // 
            flowLastWeekdayCounts.BackColor = Color.WhiteSmoke;
            flowLastWeekdayCounts.Location = new Point(144, 112);
            flowLastWeekdayCounts.Name = "flowLastWeekdayCounts";
            flowLastWeekdayCounts.Size = new Size(1068, 40);
            flowLastWeekdayCounts.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Controls.Add(flowLast4);
            panel2.Controls.Add(flowLastWeekdayCounts);
            panel2.Controls.Add(lblLast4);
            panel2.Controls.Add(lblLastWeek);
            panel2.Controls.Add(lblDayBefore);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(flowDiningAreas);
            panel2.Controls.Add(flowYesterdayCounts);
            panel2.Location = new Point(12, 130);
            panel2.Name = "panel2";
            panel2.Size = new Size(1231, 231);
            panel2.TabIndex = 4;
            // 
            // flowLast4
            // 
            flowLast4.BackColor = Color.WhiteSmoke;
            flowLast4.Location = new Point(144, 152);
            flowLast4.Name = "flowLast4";
            flowLast4.Size = new Size(1068, 50);
            flowLast4.TabIndex = 6;
            // 
            // lblLast4
            // 
            lblLast4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblLast4.Location = new Point(3, 165);
            lblLast4.Name = "lblLast4";
            lblLast4.Size = new Size(135, 21);
            lblLast4.TabIndex = 4;
            lblLast4.Text = "Last 4 Mon PMs";
            lblLast4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLastWeek
            // 
            lblLastWeek.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblLastWeek.Location = new Point(3, 125);
            lblLastWeek.Name = "lblLastWeek";
            lblLastWeek.Size = new Size(135, 21);
            lblLastWeek.TabIndex = 4;
            lblLastWeek.Text = "Last Mon PM";
            lblLastWeek.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDayBefore
            // 
            lblDayBefore.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDayBefore.Location = new Point(3, 85);
            lblDayBefore.Name = "lblDayBefore";
            lblDayBefore.Size = new Size(135, 21);
            lblDayBefore.TabIndex = 4;
            lblDayBefore.Text = "Yesterday PM";
            lblDayBefore.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(530, 6);
            label1.Name = "label1";
            label1.Size = new Size(190, 25);
            label1.TabIndex = 4;
            label1.Text = "Choose Dining Areas";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 190, 200);
            panel3.Controls.Add(txtServerSearch);
            panel3.Controls.Add(flowAllServers);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(12, 400);
            panel3.Name = "panel3";
            panel3.Size = new Size(608, 439);
            panel3.TabIndex = 5;
            // 
            // txtServerSearch
            // 
            txtServerSearch.BorderStyle = BorderStyle.None;
            txtServerSearch.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtServerSearch.Location = new Point(348, 10);
            txtServerSearch.Name = "txtServerSearch";
            txtServerSearch.Size = new Size(118, 26);
            txtServerSearch.TabIndex = 3;
            txtServerSearch.TextChanged += txtServerSearch_TextChanged;
            txtServerSearch.KeyDown += TxtServerSearch_KeyDown;
            // 
            // flowAllServers
            // 
            flowAllServers.AutoScroll = true;
            flowAllServers.BackColor = Color.WhiteSmoke;
            flowAllServers.Location = new Point(9, 45);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(578, 384);
            flowAllServers.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(268, 11);
            label6.Name = "label6";
            label6.Size = new Size(74, 25);
            label6.TabIndex = 0;
            label6.Text = "Search:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(9, 12);
            label5.Name = "label5";
            label5.Size = new Size(188, 25);
            label5.TabIndex = 0;
            label5.Text = "Servers Not On Shift";
            // 
            // pbAddPerson
            // 
            pbAddPerson.BackColor = SystemColors.ButtonShadow;
            pbAddPerson.Image = Properties.Resources.addPerson;
            pbAddPerson.Location = new Point(960, 364);
            pbAddPerson.Name = "pbAddPerson";
            pbAddPerson.Size = new Size(40, 30);
            pbAddPerson.SizeMode = PictureBoxSizeMode.Zoom;
            pbAddPerson.TabIndex = 2;
            pbAddPerson.TabStop = false;
            toolTip1.SetToolTip(pbAddPerson, "Add a New Server to Database");
            pbAddPerson.Visible = false;
            pbAddPerson.Click += pbAddPerson_Click;
            // 
            // btnImportFromHotSchedules
            // 
            btnImportFromHotSchedules.BackColor = Color.FromArgb(100, 130, 180);
            btnImportFromHotSchedules.FlatAppearance.BorderSize = 0;
            btnImportFromHotSchedules.FlatStyle = FlatStyle.Flat;
            btnImportFromHotSchedules.Image = Properties.Resources.cloudDownloadSmall;
            btnImportFromHotSchedules.Location = new Point(8, 7);
            btnImportFromHotSchedules.Name = "btnImportFromHotSchedules";
            btnImportFromHotSchedules.Size = new Size(33, 33);
            btnImportFromHotSchedules.TabIndex = 1;
            toolTip1.SetToolTip(btnImportFromHotSchedules, "Get From HotSchedules");
            btnImportFromHotSchedules.UseVisualStyleBackColor = false;
            btnImportFromHotSchedules.Click += btnGetHotSchedulesServers;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(180, 190, 200);
            panel4.Controls.Add(btnSubtractBartender);
            panel4.Controls.Add(btnImportFromHotSchedules);
            panel4.Controls.Add(btnAddBartender);
            panel4.Controls.Add(flowServersOnShift);
            panel4.Controls.Add(lblBartenderCount);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(lblServersOnShift);
            panel4.Location = new Point(638, 400);
            panel4.Name = "panel4";
            panel4.Size = new Size(605, 439);
            panel4.TabIndex = 5;
            // 
            // btnSubtractBartender
            // 
            btnSubtractBartender.BackColor = Color.FromArgb(100, 130, 180);
            btnSubtractBartender.FlatAppearance.BorderSize = 0;
            btnSubtractBartender.FlatStyle = FlatStyle.Flat;
            btnSubtractBartender.ForeColor = Color.White;
            btnSubtractBartender.Image = Properties.Resources.MinusThick12;
            btnSubtractBartender.Location = new Point(544, 25);
            btnSubtractBartender.Name = "btnSubtractBartender";
            btnSubtractBartender.Size = new Size(28, 15);
            btnSubtractBartender.TabIndex = 2;
            toolTip1.SetToolTip(btnSubtractBartender, "Subtract Bartender");
            btnSubtractBartender.UseVisualStyleBackColor = false;
            btnSubtractBartender.Click += btnSubtractBartender_Click;
            // 
            // btnAddBartender
            // 
            btnAddBartender.BackColor = Color.FromArgb(100, 130, 180);
            btnAddBartender.FlatAppearance.BorderSize = 0;
            btnAddBartender.FlatStyle = FlatStyle.Flat;
            btnAddBartender.ForeColor = Color.White;
            btnAddBartender.Image = Properties.Resources.AddThick12;
            btnAddBartender.Location = new Point(544, 8);
            btnAddBartender.Name = "btnAddBartender";
            btnAddBartender.Size = new Size(28, 15);
            btnAddBartender.TabIndex = 2;
            toolTip1.SetToolTip(btnAddBartender, "Add Bartender");
            btnAddBartender.UseVisualStyleBackColor = false;
            btnAddBartender.Click += btnAddBartender_Click;
            // 
            // flowServersOnShift
            // 
            flowServersOnShift.BackColor = Color.WhiteSmoke;
            flowServersOnShift.Location = new Point(8, 45);
            flowServersOnShift.Name = "flowServersOnShift";
            flowServersOnShift.Size = new Size(578, 384);
            flowServersOnShift.TabIndex = 0;
            flowServersOnShift.ControlAdded += flowServersOnShift_ControlsChanged;
            flowServersOnShift.ControlRemoved += flowServersOnShift_ControlsChanged;
            // 
            // lblBartenderCount
            // 
            lblBartenderCount.AutoSize = true;
            lblBartenderCount.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblBartenderCount.Location = new Point(407, 11);
            lblBartenderCount.Name = "lblBartenderCount";
            lblBartenderCount.Size = new Size(20, 25);
            lblBartenderCount.TabIndex = 0;
            lblBartenderCount.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(433, 11);
            label2.Name = "label2";
            label2.Size = new Size(105, 25);
            label2.TabIndex = 0;
            label2.Text = "Bartenders";
            // 
            // lblServersOnShift
            // 
            lblServersOnShift.AutoSize = true;
            lblServersOnShift.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblServersOnShift.Location = new Point(186, 11);
            lblServersOnShift.Name = "lblServersOnShift";
            lblServersOnShift.Size = new Size(150, 25);
            lblServersOnShift.TabIndex = 0;
            lblServersOnShift.Text = "Servers On Shift";
            // 
            // lblMissingServers
            // 
            lblMissingServers.AutoSize = true;
            lblMissingServers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblMissingServers.ForeColor = Color.DarkRed;
            lblMissingServers.Location = new Point(778, 372);
            lblMissingServers.Name = "lblMissingServers";
            lblMissingServers.Size = new Size(176, 21);
            lblMissingServers.TabIndex = 3;
            lblMissingServers.Text = "!! MISSING SERVERS !!";
            lblMissingServers.Visible = false;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 20000;
            toolTip1.InitialDelay = 200;
            toolTip1.ReshowDelay = 40;
            // 
            // frmNewShiftDatePicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(225, 225, 225);
            CancelButton = btnCancel;
            ClientSize = new Size(1255, 994);
            Controls.Add(lblMissingServers);
            Controls.Add(pbAddPerson);
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
            Shown += frmNewShiftDatePicker_Shown;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbAddPerson).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Label lblDayBefore;
        private Label label1;
        private Panel panel3;
        private Label label5;
        private Panel panel4;
        private Label lblServersOnShift;
        private FlowLayoutPanel flowAllServers;
        private FlowLayoutPanel flowServersOnShift;
        private PictureBox pbAddPerson;
        private ToolTip toolTip1;
        private TextBox txtServerSearch;
        private Label label6;
        private Button btnImportFromHotSchedules;
        private Button btnSubtractBartender;
        private Button btnAddBartender;
        private Label lblBartenderCount;
        private Label label2;
        private Label lblMissingServers;
        private FlowLayoutPanel flowLast4;
        private Label lblLast4;
        private Label lblLastWeek;
    }
}