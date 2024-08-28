namespace FloorPlanMakerUI
{
    partial class frmSalesStats
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rdoDiningAreaSales = new RadioButton();
            rdoServerShifts = new RadioButton();
            rdoAm = new RadioButton();
            rdoPm = new RadioButton();
            rdoBoth = new RadioButton();
            panel1 = new Panel();
            panel2 = new Panel();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            panel3 = new Panel();
            cbAllWeekdays = new CheckBox();
            cbSun = new CheckBox();
            cbSat = new CheckBox();
            cbFri = new CheckBox();
            cbThurs = new CheckBox();
            cbWed = new CheckBox();
            cbTues = new CheckBox();
            cbMon = new CheckBox();
            panel4 = new Panel();
            btnIndividualServerShifts = new Button();
            lblComboLabel = new Label();
            btnIndividualStats = new Button();
            cboServerSelect = new ComboBox();
            panel5 = new Panel();
            cbAllMonths = new CheckBox();
            cbJul = new CheckBox();
            cbDec = new CheckBox();
            cbJun = new CheckBox();
            cbNov = new CheckBox();
            cbMay = new CheckBox();
            cbOct = new CheckBox();
            cbSep = new CheckBox();
            cbApr = new CheckBox();
            cbAug = new CheckBox();
            cbMar = new CheckBox();
            cbFeb = new CheckBox();
            cbJan = new CheckBox();
            nudTempAnchor = new NumericUpDown();
            cbFilterByTempRange = new CheckBox();
            nudTempRange = new NumericUpDown();
            lblTo = new Label();
            btnRefreshFilters = new Button();
            dataGridView1 = new DataGridView();
            flowDiningAreas = new FlowLayoutPanel();
            dgvAreaStats = new DataGridView();
            panel6 = new Panel();
            rdoSpecialAndNormal = new RadioButton();
            rdoEventsOnly = new RadioButton();
            rdoExcludeEvents = new RadioButton();
            nudRainAnchor = new NumericUpDown();
            cbFilterByRain = new CheckBox();
            nudRainRange = new NumericUpDown();
            label3 = new Label();
            nudCloudAnchor = new NumericUpDown();
            cbFilterByClouds = new CheckBox();
            nudCloudRange = new NumericUpDown();
            label4 = new Label();
            nudWindMaxAnchor = new NumericUpDown();
            cbFilterByWindMax = new CheckBox();
            nudWindMaxRange = new NumericUpDown();
            label5 = new Label();
            nudWindAvgAnchor = new NumericUpDown();
            cbFilterByWindAvg = new CheckBox();
            nudWindAvgRange = new NumericUpDown();
            label6 = new Label();
            lblSampleSizeDisplay = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTempAnchor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTempRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAreaStats).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRainAnchor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRainRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCloudAnchor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCloudRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWindMaxAnchor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWindMaxRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWindAvgAnchor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWindAvgRange).BeginInit();
            SuspendLayout();
            // 
            // rdoDiningAreaSales
            // 
            rdoDiningAreaSales.AutoSize = true;
            rdoDiningAreaSales.Checked = true;
            rdoDiningAreaSales.Location = new Point(0, 3);
            rdoDiningAreaSales.Name = "rdoDiningAreaSales";
            rdoDiningAreaSales.Size = new Size(116, 19);
            rdoDiningAreaSales.TabIndex = 1;
            rdoDiningAreaSales.TabStop = true;
            rdoDiningAreaSales.Text = "Dining Area Sales";
            rdoDiningAreaSales.UseVisualStyleBackColor = true;
            // 
            // rdoServerShifts
            // 
            rdoServerShifts.AutoSize = true;
            rdoServerShifts.Location = new Point(126, 3);
            rdoServerShifts.Name = "rdoServerShifts";
            rdoServerShifts.Size = new Size(125, 19);
            rdoServerShifts.TabIndex = 1;
            rdoServerShifts.Text = "Server Shift History";
            rdoServerShifts.UseVisualStyleBackColor = true;
            rdoServerShifts.CheckedChanged += rdoServerShifts_CheckedChanged;
            // 
            // rdoAm
            // 
            rdoAm.AutoSize = true;
            rdoAm.Location = new Point(126, 3);
            rdoAm.Name = "rdoAm";
            rdoAm.Size = new Size(44, 19);
            rdoAm.TabIndex = 3;
            rdoAm.Text = "AM";
            rdoAm.UseVisualStyleBackColor = true;
            rdoAm.CheckedChanged += rdoPm_CheckedChanged;
            // 
            // rdoPm
            // 
            rdoPm.AutoSize = true;
            rdoPm.Checked = true;
            rdoPm.Location = new Point(73, 3);
            rdoPm.Name = "rdoPm";
            rdoPm.Size = new Size(43, 19);
            rdoPm.TabIndex = 3;
            rdoPm.TabStop = true;
            rdoPm.Text = "PM";
            rdoPm.UseVisualStyleBackColor = true;
            rdoPm.CheckedChanged += rdoPm_CheckedChanged;
            // 
            // rdoBoth
            // 
            rdoBoth.AutoSize = true;
            rdoBoth.Enabled = false;
            rdoBoth.Location = new Point(3, 3);
            rdoBoth.Name = "rdoBoth";
            rdoBoth.Size = new Size(62, 19);
            rdoBoth.TabIndex = 3;
            rdoBoth.Text = "All Day";
            rdoBoth.UseVisualStyleBackColor = true;
            rdoBoth.CheckedChanged += rdoBoth_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoServerShifts);
            panel1.Controls.Add(rdoDiningAreaSales);
            panel1.Location = new Point(30, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(258, 31);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(rdoBoth);
            panel2.Controls.Add(rdoAm);
            panel2.Controls.Add(rdoPm);
            panel2.Location = new Point(30, 49);
            panel2.Name = "panel2";
            panel2.Size = new Size(177, 25);
            panel2.TabIndex = 4;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(310, 27);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 5;
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(521, 27);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 5;
            dtpEndDate.ValueChanged += dtpEndDate_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(310, 9);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 6;
            label1.Text = "From:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(521, 9);
            label2.Name = "label2";
            label2.Size = new Size(22, 15);
            label2.TabIndex = 6;
            label2.Text = "To:";
            // 
            // panel3
            // 
            panel3.Controls.Add(cbAllWeekdays);
            panel3.Controls.Add(cbSun);
            panel3.Controls.Add(cbSat);
            panel3.Controls.Add(cbFri);
            panel3.Controls.Add(cbThurs);
            panel3.Controls.Add(cbWed);
            panel3.Controls.Add(cbTues);
            panel3.Controls.Add(cbMon);
            panel3.Location = new Point(30, 77);
            panel3.Name = "panel3";
            panel3.Size = new Size(464, 44);
            panel3.TabIndex = 7;
            // 
            // cbAllWeekdays
            // 
            cbAllWeekdays.Appearance = Appearance.Button;
            cbAllWeekdays.Checked = true;
            cbAllWeekdays.CheckState = CheckState.Checked;
            cbAllWeekdays.Location = new Point(367, 14);
            cbAllWeekdays.Name = "cbAllWeekdays";
            cbAllWeekdays.Size = new Size(84, 25);
            cbAllWeekdays.TabIndex = 0;
            cbAllWeekdays.Text = "No Days";
            cbAllWeekdays.UseVisualStyleBackColor = true;
            cbAllWeekdays.CheckedChanged += cbAllWeekdays_CheckedChanged;
            // 
            // cbSun
            // 
            cbSun.Appearance = Appearance.Button;
            cbSun.Checked = true;
            cbSun.CheckState = CheckState.Checked;
            cbSun.Location = new Point(315, 14);
            cbSun.Name = "cbSun";
            cbSun.Size = new Size(46, 25);
            cbSun.TabIndex = 0;
            cbSun.Text = "Sun";
            cbSun.UseVisualStyleBackColor = true;
            cbSun.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbSat
            // 
            cbSat.Appearance = Appearance.Button;
            cbSat.Checked = true;
            cbSat.CheckState = CheckState.Checked;
            cbSat.Location = new Point(263, 14);
            cbSat.Name = "cbSat";
            cbSat.Size = new Size(46, 25);
            cbSat.TabIndex = 0;
            cbSat.Text = "Sat";
            cbSat.UseVisualStyleBackColor = true;
            cbSat.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbFri
            // 
            cbFri.Appearance = Appearance.Button;
            cbFri.Checked = true;
            cbFri.CheckState = CheckState.Checked;
            cbFri.Location = new Point(211, 14);
            cbFri.Name = "cbFri";
            cbFri.Size = new Size(46, 25);
            cbFri.TabIndex = 0;
            cbFri.Text = "Fri";
            cbFri.UseVisualStyleBackColor = true;
            cbFri.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbThurs
            // 
            cbThurs.Appearance = Appearance.Button;
            cbThurs.Checked = true;
            cbThurs.CheckState = CheckState.Checked;
            cbThurs.Location = new Point(159, 14);
            cbThurs.Name = "cbThurs";
            cbThurs.Size = new Size(46, 25);
            cbThurs.TabIndex = 0;
            cbThurs.Text = "Thurs";
            cbThurs.UseVisualStyleBackColor = true;
            cbThurs.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbWed
            // 
            cbWed.Appearance = Appearance.Button;
            cbWed.Checked = true;
            cbWed.CheckState = CheckState.Checked;
            cbWed.Location = new Point(107, 14);
            cbWed.Name = "cbWed";
            cbWed.Size = new Size(46, 25);
            cbWed.TabIndex = 0;
            cbWed.Text = "Wed";
            cbWed.UseVisualStyleBackColor = true;
            cbWed.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbTues
            // 
            cbTues.Appearance = Appearance.Button;
            cbTues.Checked = true;
            cbTues.CheckState = CheckState.Checked;
            cbTues.Location = new Point(55, 14);
            cbTues.Name = "cbTues";
            cbTues.Size = new Size(46, 25);
            cbTues.TabIndex = 0;
            cbTues.Text = "Tues";
            cbTues.UseVisualStyleBackColor = true;
            cbTues.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // cbMon
            // 
            cbMon.Appearance = Appearance.Button;
            cbMon.Checked = true;
            cbMon.CheckState = CheckState.Checked;
            cbMon.Location = new Point(3, 14);
            cbMon.Name = "cbMon";
            cbMon.Size = new Size(46, 25);
            cbMon.TabIndex = 0;
            cbMon.Text = "Mon";
            cbMon.UseVisualStyleBackColor = true;
            cbMon.CheckedChanged += cbDayOfWeek_CheckedChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnIndividualServerShifts);
            panel4.Controls.Add(lblComboLabel);
            panel4.Controls.Add(btnIndividualStats);
            panel4.Controls.Add(cboServerSelect);
            panel4.Location = new Point(1296, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(104, 125);
            panel4.TabIndex = 8;
            // 
            // btnIndividualServerShifts
            // 
            btnIndividualServerShifts.Location = new Point(6, 84);
            btnIndividualServerShifts.Name = "btnIndividualServerShifts";
            btnIndividualServerShifts.Size = new Size(86, 23);
            btnIndividualServerShifts.TabIndex = 3;
            btnIndividualServerShifts.Text = "See Server Shifts";
            btnIndividualServerShifts.UseVisualStyleBackColor = true;
            btnIndividualServerShifts.Visible = false;
            btnIndividualServerShifts.Click += btnIndividualServerShifts_Click;
            // 
            // lblComboLabel
            // 
            lblComboLabel.AutoSize = true;
            lblComboLabel.Location = new Point(6, 0);
            lblComboLabel.Name = "lblComboLabel";
            lblComboLabel.Size = new Size(74, 15);
            lblComboLabel.TabIndex = 2;
            lblComboLabel.Text = "Dining Areas";
            // 
            // btnIndividualStats
            // 
            btnIndividualStats.Location = new Point(6, 56);
            btnIndividualStats.Name = "btnIndividualStats";
            btnIndividualStats.Size = new Size(86, 23);
            btnIndividualStats.TabIndex = 1;
            btnIndividualStats.Text = "Server Table History";
            btnIndividualStats.UseVisualStyleBackColor = true;
            btnIndividualStats.Click += btnIndividualStats_Click;
            // 
            // cboServerSelect
            // 
            cboServerSelect.FormattingEnabled = true;
            cboServerSelect.Location = new Point(6, 21);
            cboServerSelect.Name = "cboServerSelect";
            cboServerSelect.Size = new Size(86, 23);
            cboServerSelect.TabIndex = 0;
            cboServerSelect.SelectedIndexChanged += cboServerSelect_SelectedIndexChanged;
            // 
            // panel5
            // 
            panel5.Controls.Add(cbAllMonths);
            panel5.Controls.Add(cbJul);
            panel5.Controls.Add(cbDec);
            panel5.Controls.Add(cbJun);
            panel5.Controls.Add(cbNov);
            panel5.Controls.Add(cbMay);
            panel5.Controls.Add(cbOct);
            panel5.Controls.Add(cbSep);
            panel5.Controls.Add(cbApr);
            panel5.Controls.Add(cbAug);
            panel5.Controls.Add(cbMar);
            panel5.Controls.Add(cbFeb);
            panel5.Controls.Add(cbJan);
            panel5.Location = new Point(30, 129);
            panel5.Name = "panel5";
            panel5.Size = new Size(724, 44);
            panel5.TabIndex = 7;
            // 
            // cbAllMonths
            // 
            cbAllMonths.Appearance = Appearance.Button;
            cbAllMonths.Checked = true;
            cbAllMonths.CheckState = CheckState.Checked;
            cbAllMonths.Location = new Point(635, 14);
            cbAllMonths.Name = "cbAllMonths";
            cbAllMonths.Size = new Size(84, 25);
            cbAllMonths.TabIndex = 0;
            cbAllMonths.Text = "No Months";
            cbAllMonths.UseVisualStyleBackColor = true;
            cbAllMonths.CheckedChanged += cbAllMonths_CheckedChanged;
            // 
            // cbJul
            // 
            cbJul.Appearance = Appearance.Button;
            cbJul.Checked = true;
            cbJul.CheckState = CheckState.Checked;
            cbJul.Location = new Point(315, 14);
            cbJul.Name = "cbJul";
            cbJul.Size = new Size(46, 25);
            cbJul.TabIndex = 0;
            cbJul.Text = "Jul";
            cbJul.UseVisualStyleBackColor = true;
            cbJul.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbDec
            // 
            cbDec.Appearance = Appearance.Button;
            cbDec.Checked = true;
            cbDec.CheckState = CheckState.Checked;
            cbDec.Location = new Point(574, 14);
            cbDec.Name = "cbDec";
            cbDec.Size = new Size(46, 25);
            cbDec.TabIndex = 0;
            cbDec.Text = "Dec";
            cbDec.UseVisualStyleBackColor = true;
            cbDec.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbJun
            // 
            cbJun.Appearance = Appearance.Button;
            cbJun.Checked = true;
            cbJun.CheckState = CheckState.Checked;
            cbJun.Location = new Point(263, 14);
            cbJun.Name = "cbJun";
            cbJun.Size = new Size(46, 25);
            cbJun.TabIndex = 0;
            cbJun.Text = "Jun";
            cbJun.UseVisualStyleBackColor = true;
            cbJun.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbNov
            // 
            cbNov.Appearance = Appearance.Button;
            cbNov.Checked = true;
            cbNov.CheckState = CheckState.Checked;
            cbNov.Location = new Point(522, 14);
            cbNov.Name = "cbNov";
            cbNov.Size = new Size(46, 25);
            cbNov.TabIndex = 0;
            cbNov.Text = "Nov";
            cbNov.UseVisualStyleBackColor = true;
            cbNov.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbMay
            // 
            cbMay.Appearance = Appearance.Button;
            cbMay.Checked = true;
            cbMay.CheckState = CheckState.Checked;
            cbMay.Location = new Point(211, 14);
            cbMay.Name = "cbMay";
            cbMay.Size = new Size(46, 25);
            cbMay.TabIndex = 0;
            cbMay.Text = "May";
            cbMay.UseVisualStyleBackColor = true;
            cbMay.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbOct
            // 
            cbOct.Appearance = Appearance.Button;
            cbOct.Checked = true;
            cbOct.CheckState = CheckState.Checked;
            cbOct.Location = new Point(470, 14);
            cbOct.Name = "cbOct";
            cbOct.Size = new Size(46, 25);
            cbOct.TabIndex = 0;
            cbOct.Text = "Oct";
            cbOct.UseVisualStyleBackColor = true;
            cbOct.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbSep
            // 
            cbSep.Appearance = Appearance.Button;
            cbSep.Checked = true;
            cbSep.CheckState = CheckState.Checked;
            cbSep.Location = new Point(418, 14);
            cbSep.Name = "cbSep";
            cbSep.Size = new Size(46, 25);
            cbSep.TabIndex = 0;
            cbSep.Text = "Sep";
            cbSep.UseVisualStyleBackColor = true;
            cbSep.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbApr
            // 
            cbApr.Appearance = Appearance.Button;
            cbApr.Checked = true;
            cbApr.CheckState = CheckState.Checked;
            cbApr.Location = new Point(159, 14);
            cbApr.Name = "cbApr";
            cbApr.Size = new Size(46, 25);
            cbApr.TabIndex = 0;
            cbApr.Text = "Apr";
            cbApr.UseVisualStyleBackColor = true;
            cbApr.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbAug
            // 
            cbAug.Appearance = Appearance.Button;
            cbAug.Checked = true;
            cbAug.CheckState = CheckState.Checked;
            cbAug.Location = new Point(366, 14);
            cbAug.Name = "cbAug";
            cbAug.Size = new Size(46, 25);
            cbAug.TabIndex = 0;
            cbAug.Text = "Aug";
            cbAug.UseVisualStyleBackColor = true;
            cbAug.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbMar
            // 
            cbMar.Appearance = Appearance.Button;
            cbMar.Checked = true;
            cbMar.CheckState = CheckState.Checked;
            cbMar.Location = new Point(107, 14);
            cbMar.Name = "cbMar";
            cbMar.Size = new Size(46, 25);
            cbMar.TabIndex = 0;
            cbMar.Text = "Mar";
            cbMar.UseVisualStyleBackColor = true;
            cbMar.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbFeb
            // 
            cbFeb.Appearance = Appearance.Button;
            cbFeb.Checked = true;
            cbFeb.CheckState = CheckState.Checked;
            cbFeb.Location = new Point(55, 14);
            cbFeb.Name = "cbFeb";
            cbFeb.Size = new Size(46, 25);
            cbFeb.TabIndex = 0;
            cbFeb.Text = "Feb";
            cbFeb.UseVisualStyleBackColor = true;
            cbFeb.CheckedChanged += cbMonth_CheckChanged;
            // 
            // cbJan
            // 
            cbJan.Appearance = Appearance.Button;
            cbJan.Checked = true;
            cbJan.CheckState = CheckState.Checked;
            cbJan.Location = new Point(3, 14);
            cbJan.Name = "cbJan";
            cbJan.Size = new Size(46, 25);
            cbJan.TabIndex = 0;
            cbJan.Text = "Jan";
            cbJan.UseVisualStyleBackColor = true;
            cbJan.CheckedChanged += cbMonth_CheckChanged;
            // 
            // nudTempAnchor
            // 
            nudTempAnchor.Enabled = false;
            nudTempAnchor.Location = new Point(1110, 18);
            nudTempAnchor.Name = "nudTempAnchor";
            nudTempAnchor.Size = new Size(60, 23);
            nudTempAnchor.TabIndex = 9;
            nudTempAnchor.Value = new decimal(new int[] { 80, 0, 0, 0 });
            nudTempAnchor.ValueChanged += nudTemp_ValueChanged;
            // 
            // cbFilterByTempRange
            // 
            cbFilterByTempRange.AutoSize = true;
            cbFilterByTempRange.Location = new Point(812, 20);
            cbFilterByTempRange.Name = "cbFilterByTempRange";
            cbFilterByTempRange.Size = new Size(137, 19);
            cbFilterByTempRange.TabIndex = 10;
            cbFilterByTempRange.Text = "Filter By Temperature";
            cbFilterByTempRange.UseVisualStyleBackColor = true;
            cbFilterByTempRange.CheckedChanged += cbFilterByTempRange_CheckedChanged;
            // 
            // nudTempRange
            // 
            nudTempRange.Enabled = false;
            nudTempRange.Location = new Point(955, 18);
            nudTempRange.Name = "nudTempRange";
            nudTempRange.Size = new Size(60, 23);
            nudTempRange.TabIndex = 9;
            nudTempRange.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudTempRange.ValueChanged += nudTemp_ValueChanged;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Enabled = false;
            lblTo.Location = new Point(1021, 22);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(83, 15);
            lblTo.TabIndex = 11;
            lblTo.Text = "Degrees From:";
            // 
            // btnRefreshFilters
            // 
            btnRefreshFilters.BackColor = Color.FromArgb(100, 130, 180);
            btnRefreshFilters.FlatAppearance.BorderSize = 0;
            btnRefreshFilters.FlatStyle = FlatStyle.Flat;
            btnRefreshFilters.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefreshFilters.ForeColor = Color.White;
            btnRefreshFilters.Location = new Point(30, 312);
            btnRefreshFilters.Name = "btnRefreshFilters";
            btnRefreshFilters.Size = new Size(1370, 35);
            btnRefreshFilters.TabIndex = 2;
            btnRefreshFilters.Text = "Get Data";
            btnRefreshFilters.UseVisualStyleBackColor = false;
            btnRefreshFilters.Click += btnRefreshFilters_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(30, 547);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1370, 323);
            dataGridView1.TabIndex = 12;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(30, 179);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(724, 53);
            flowDiningAreas.TabIndex = 13;
            // 
            // dgvAreaStats
            // 
            dgvAreaStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAreaStats.Location = new Point(30, 353);
            dgvAreaStats.Name = "dgvAreaStats";
            dgvAreaStats.RowTemplate.Height = 25;
            dgvAreaStats.Size = new Size(890, 188);
            dgvAreaStats.TabIndex = 14;
            // 
            // panel6
            // 
            panel6.Controls.Add(rdoSpecialAndNormal);
            panel6.Controls.Add(rdoEventsOnly);
            panel6.Controls.Add(rdoExcludeEvents);
            panel6.Location = new Point(30, 238);
            panel6.Name = "panel6";
            panel6.Size = new Size(301, 25);
            panel6.TabIndex = 4;
            // 
            // rdoSpecialAndNormal
            // 
            rdoSpecialAndNormal.AutoSize = true;
            rdoSpecialAndNormal.Checked = true;
            rdoSpecialAndNormal.Location = new Point(3, 3);
            rdoSpecialAndNormal.Name = "rdoSpecialAndNormal";
            rdoSpecialAndNormal.Size = new Size(50, 19);
            rdoSpecialAndNormal.TabIndex = 3;
            rdoSpecialAndNormal.TabStop = true;
            rdoSpecialAndNormal.Text = "Both";
            rdoSpecialAndNormal.UseVisualStyleBackColor = true;
            rdoSpecialAndNormal.CheckedChanged += rdoEvents_CheckedChanged;
            // 
            // rdoEventsOnly
            // 
            rdoEventsOnly.AutoSize = true;
            rdoEventsOnly.Location = new Point(207, 3);
            rdoEventsOnly.Name = "rdoEventsOnly";
            rdoEventsOnly.Size = new Size(87, 19);
            rdoEventsOnly.TabIndex = 3;
            rdoEventsOnly.Text = "Events Only";
            rdoEventsOnly.UseVisualStyleBackColor = true;
            rdoEventsOnly.CheckedChanged += rdoEvents_CheckedChanged;
            // 
            // rdoExcludeEvents
            // 
            rdoExcludeEvents.AutoSize = true;
            rdoExcludeEvents.Location = new Point(59, 3);
            rdoExcludeEvents.Name = "rdoExcludeEvents";
            rdoExcludeEvents.Size = new Size(143, 19);
            rdoExcludeEvents.TabIndex = 3;
            rdoExcludeEvents.Text = "Exclude Special Events";
            rdoExcludeEvents.UseVisualStyleBackColor = true;
            rdoExcludeEvents.CheckedChanged += rdoEvents_CheckedChanged;
            // 
            // nudRainAnchor
            // 
            nudRainAnchor.DecimalPlaces = 1;
            nudRainAnchor.Enabled = false;
            nudRainAnchor.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRainAnchor.Location = new Point(1110, 47);
            nudRainAnchor.Name = "nudRainAnchor";
            nudRainAnchor.Size = new Size(60, 23);
            nudRainAnchor.TabIndex = 9;
            nudRainAnchor.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            nudRainAnchor.ValueChanged += nudRain_ValueChanged;
            // 
            // cbFilterByRain
            // 
            cbFilterByRain.AutoSize = true;
            cbFilterByRain.Location = new Point(812, 49);
            cbFilterByRain.Name = "cbFilterByRain";
            cbFilterByRain.Size = new Size(94, 19);
            cbFilterByRain.TabIndex = 10;
            cbFilterByRain.Text = "Filter By Rain";
            cbFilterByRain.UseVisualStyleBackColor = true;
            cbFilterByRain.CheckedChanged += cbFilterByRain_CheckedChanged;
            // 
            // nudRainRange
            // 
            nudRainRange.DecimalPlaces = 1;
            nudRainRange.Enabled = false;
            nudRainRange.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRainRange.Location = new Point(955, 47);
            nudRainRange.Name = "nudRainRange";
            nudRainRange.Size = new Size(60, 23);
            nudRainRange.TabIndex = 9;
            nudRainRange.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            nudRainRange.ValueChanged += nudRain_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Enabled = false;
            label3.Location = new Point(1021, 51);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 11;
            label3.Text = "Inches From:";
            // 
            // nudCloudAnchor
            // 
            nudCloudAnchor.Enabled = false;
            nudCloudAnchor.Location = new Point(1110, 75);
            nudCloudAnchor.Name = "nudCloudAnchor";
            nudCloudAnchor.Size = new Size(60, 23);
            nudCloudAnchor.TabIndex = 9;
            nudCloudAnchor.Value = new decimal(new int[] { 50, 0, 0, 0 });
            nudCloudAnchor.ValueChanged += nudCloud_ValueChanged;
            // 
            // cbFilterByClouds
            // 
            cbFilterByClouds.AutoSize = true;
            cbFilterByClouds.Location = new Point(812, 77);
            cbFilterByClouds.Name = "cbFilterByClouds";
            cbFilterByClouds.Size = new Size(108, 19);
            cbFilterByClouds.TabIndex = 10;
            cbFilterByClouds.Text = "Filter By Clouds";
            cbFilterByClouds.UseVisualStyleBackColor = true;
            cbFilterByClouds.CheckedChanged += cbFilterByClouds_CheckedChanged;
            // 
            // nudCloudRange
            // 
            nudCloudRange.Enabled = false;
            nudCloudRange.Location = new Point(955, 75);
            nudCloudRange.Name = "nudCloudRange";
            nudCloudRange.Size = new Size(60, 23);
            nudCloudRange.TabIndex = 9;
            nudCloudRange.Value = new decimal(new int[] { 30, 0, 0, 0 });
            nudCloudRange.ValueChanged += nudCloud_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Enabled = false;
            label4.Location = new Point(1021, 79);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 11;
            label4.Text = "Percent From:";
            // 
            // nudWindMaxAnchor
            // 
            nudWindMaxAnchor.Enabled = false;
            nudWindMaxAnchor.Location = new Point(1110, 100);
            nudWindMaxAnchor.Name = "nudWindMaxAnchor";
            nudWindMaxAnchor.Size = new Size(60, 23);
            nudWindMaxAnchor.TabIndex = 9;
            nudWindMaxAnchor.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudWindMaxAnchor.ValueChanged += nudWindMax_ValueChanged;
            // 
            // cbFilterByWindMax
            // 
            cbFilterByWindMax.AutoSize = true;
            cbFilterByWindMax.Location = new Point(812, 102);
            cbFilterByWindMax.Name = "cbFilterByWindMax";
            cbFilterByWindMax.Size = new Size(125, 19);
            cbFilterByWindMax.TabIndex = 10;
            cbFilterByWindMax.Text = "Filter By Wind Max";
            cbFilterByWindMax.UseVisualStyleBackColor = true;
            cbFilterByWindMax.CheckedChanged += cbFilterByWindMax_CheckedChanged;
            // 
            // nudWindMaxRange
            // 
            nudWindMaxRange.Enabled = false;
            nudWindMaxRange.Location = new Point(955, 100);
            nudWindMaxRange.Name = "nudWindMaxRange";
            nudWindMaxRange.Size = new Size(60, 23);
            nudWindMaxRange.TabIndex = 9;
            nudWindMaxRange.Value = new decimal(new int[] { 5, 0, 0, 0 });
            nudWindMaxRange.ValueChanged += nudWindMax_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Enabled = false;
            label5.Location = new Point(1021, 104);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 11;
            label5.Text = "MPH From:";
            // 
            // nudWindAvgAnchor
            // 
            nudWindAvgAnchor.Enabled = false;
            nudWindAvgAnchor.Location = new Point(1110, 127);
            nudWindAvgAnchor.Name = "nudWindAvgAnchor";
            nudWindAvgAnchor.Size = new Size(60, 23);
            nudWindAvgAnchor.TabIndex = 9;
            nudWindAvgAnchor.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudWindAvgAnchor.ValueChanged += nudWindAvg_ValueChanged;
            // 
            // cbFilterByWindAvg
            // 
            cbFilterByWindAvg.AutoSize = true;
            cbFilterByWindAvg.Location = new Point(812, 129);
            cbFilterByWindAvg.Name = "cbFilterByWindAvg";
            cbFilterByWindAvg.Size = new Size(123, 19);
            cbFilterByWindAvg.TabIndex = 10;
            cbFilterByWindAvg.Text = "Filter By Wind Avg";
            cbFilterByWindAvg.UseVisualStyleBackColor = true;
            cbFilterByWindAvg.CheckedChanged += cbFilterByWindAvg_CheckedChanged;
            // 
            // nudWindAvgRange
            // 
            nudWindAvgRange.Enabled = false;
            nudWindAvgRange.Location = new Point(955, 127);
            nudWindAvgRange.Name = "nudWindAvgRange";
            nudWindAvgRange.Size = new Size(60, 23);
            nudWindAvgRange.TabIndex = 9;
            nudWindAvgRange.Value = new decimal(new int[] { 5, 0, 0, 0 });
            nudWindAvgRange.ValueChanged += nudWindAvg_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Enabled = false;
            label6.Location = new Point(1021, 131);
            label6.Name = "label6";
            label6.Size = new Size(68, 15);
            label6.TabIndex = 11;
            label6.Text = "MPH From:";
            // 
            // lblSampleSizeDisplay
            // 
            lblSampleSizeDisplay.AutoSize = true;
            lblSampleSizeDisplay.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSampleSizeDisplay.Location = new Point(945, 353);
            lblSampleSizeDisplay.Name = "lblSampleSizeDisplay";
            lblSampleSizeDisplay.Size = new Size(119, 25);
            lblSampleSizeDisplay.TabIndex = 15;
            lblSampleSizeDisplay.Text = "Sample Size:";
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1412, 874);
            Controls.Add(lblSampleSizeDisplay);
            Controls.Add(dgvAreaStats);
            Controls.Add(flowDiningAreas);
            Controls.Add(dataGridView1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lblTo);
            Controls.Add(nudWindAvgRange);
            Controls.Add(nudWindMaxRange);
            Controls.Add(nudCloudRange);
            Controls.Add(nudRainRange);
            Controls.Add(nudTempRange);
            Controls.Add(cbFilterByWindAvg);
            Controls.Add(nudWindAvgAnchor);
            Controls.Add(cbFilterByWindMax);
            Controls.Add(nudWindMaxAnchor);
            Controls.Add(cbFilterByClouds);
            Controls.Add(nudCloudAnchor);
            Controls.Add(cbFilterByRain);
            Controls.Add(nudRainAnchor);
            Controls.Add(cbFilterByTempRange);
            Controls.Add(nudTempAnchor);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(panel6);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnRefreshFilters);
            Name = "frmSalesStats";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmSalesStats";
            Load += frmSalesStats_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudTempAnchor).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTempRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAreaStats).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRainAnchor).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRainRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCloudAnchor).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCloudRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWindMaxAnchor).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWindMaxRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWindAvgAnchor).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWindAvgRange).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RadioButton rdoDiningAreaSales;
        private RadioButton rdoServerShifts;
        private RadioButton rdoAm;
        private RadioButton rdoPm;
        private RadioButton rdoBoth;
        private Panel panel1;
        private Panel panel2;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Label label1;
        private Label label2;
        private Panel panel3;
        private CheckBox cbAllWeekdays;
        private CheckBox cbSun;
        private CheckBox cbSat;
        private CheckBox cbFri;
        private CheckBox cbThurs;
        private CheckBox cbWed;
        private CheckBox cbTues;
        private CheckBox cbMon;
        private Panel panel4;
        private ComboBox cboServerSelect;
        private Label lblComboLabel;
        private Button btnIndividualStats;
        private Button btnIndividualServerShifts;
        private Panel panel5;
        private CheckBox cbAllMonths;
        private CheckBox cbJul;
        private CheckBox cbDec;
        private CheckBox cbJun;
        private CheckBox cbNov;
        private CheckBox cbMay;
        private CheckBox cbOct;
        private CheckBox cbSep;
        private CheckBox cbApr;
        private CheckBox cbAug;
        private CheckBox cbMar;
        private CheckBox cbFeb;
        private CheckBox cbJan;
        private NumericUpDown nudTempAnchor;
        private CheckBox cbFilterByTempRange;
        private NumericUpDown nudTempRange;
        private Label lblTo;
        private Button btnRefreshFilters;
        private DataGridView dataGridView1;
        private FlowLayoutPanel flowDiningAreas;
        private DataGridView dgvAreaStats;
        private Panel panel6;
        private RadioButton rdoSpecialAndNormal;
        private RadioButton rdoEventsOnly;
        private RadioButton rdoExcludeEvents;
        private NumericUpDown nudRainAnchor;
        private CheckBox cbFilterByRain;
        private NumericUpDown nudRainRange;
        private Label label3;
        private NumericUpDown nudCloudAnchor;
        private CheckBox cbFilterByClouds;
        private NumericUpDown nudCloudRange;
        private Label label4;
        private NumericUpDown nudWindMaxAnchor;
        private CheckBox cbFilterByWindMax;
        private NumericUpDown nudWindMaxRange;
        private Label label5;
        private NumericUpDown nudWindAvgAnchor;
        private CheckBox cbFilterByWindAvg;
        private NumericUpDown nudWindAvgRange;
        private Label label6;
        private Label lblSampleSizeDisplay;
    }
}