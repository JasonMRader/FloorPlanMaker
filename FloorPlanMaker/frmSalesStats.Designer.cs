namespace FloorPlanMakerUI {
    partial class frmSalesStats {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            dgvDiningAreas = new DataGridView();
            rdoDiningAreaSales = new RadioButton();
            rdoServerShifts = new RadioButton();
            btnUpdate = new Button();
            rdoAm = new RadioButton();
            rdoPm = new RadioButton();
            rdoBoth = new RadioButton();
            panel1 = new Panel();
            panel2 = new Panel();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
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
            nudLowTemp = new NumericUpDown();
            cbFilterByTempRange = new CheckBox();
            nudHiTemp = new NumericUpDown();
            lblTo = new Label();
            btnRefreshFilters = new Button();
            dataGridView1 = new DataGridView();
            flowDiningAreas = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLowTemp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHiTemp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dgvDiningAreas
            // 
            dgvDiningAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiningAreas.Location = new Point(1169, 315);
            dgvDiningAreas.Name = "dgvDiningAreas";
            dgvDiningAreas.RowTemplate.Height = 25;
            dgvDiningAreas.Size = new Size(231, 477);
            dgvDiningAreas.TabIndex = 0;
            dgvDiningAreas.Visible = false;
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
            // btnUpdate
            // 
            btnUpdate.Enabled = false;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Location = new Point(1194, 247);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(195, 35);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
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
            rdoAm.CheckedChanged += rdoAm_CheckedChanged;
            // 
            // rdoPm
            // 
            rdoPm.AutoSize = true;
            rdoPm.Location = new Point(73, 3);
            rdoPm.Name = "rdoPm";
            rdoPm.Size = new Size(43, 19);
            rdoPm.TabIndex = 3;
            rdoPm.Text = "PM";
            rdoPm.UseVisualStyleBackColor = true;
            // 
            // rdoBoth
            // 
            rdoBoth.AutoSize = true;
            rdoBoth.Checked = true;
            rdoBoth.Location = new Point(3, 3);
            rdoBoth.Name = "rdoBoth";
            rdoBoth.Size = new Size(62, 19);
            rdoBoth.TabIndex = 3;
            rdoBoth.TabStop = true;
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
            // timer1
            // 
            timer1.Tick += timer1_Tick;
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
            cbSun.CheckedChanged += cbSun_CheckedChanged;
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
            cbSat.CheckedChanged += cbSat_CheckedChanged;
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
            cbFri.CheckedChanged += cbFri_CheckedChanged;
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
            cbThurs.CheckedChanged += cbThurs_CheckedChanged;
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
            cbWed.CheckedChanged += cbWed_CheckedChanged;
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
            cbTues.CheckedChanged += cbTues_CheckedChanged;
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
            cbMon.CheckedChanged += cbMon_CheckedChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnIndividualServerShifts);
            panel4.Controls.Add(lblComboLabel);
            panel4.Controls.Add(btnIndividualStats);
            panel4.Controls.Add(cboServerSelect);
            panel4.Location = new Point(760, 9);
            panel4.Name = "panel4";
            panel4.Size = new Size(287, 164);
            panel4.TabIndex = 8;
            // 
            // btnIndividualServerShifts
            // 
            btnIndividualServerShifts.Location = new Point(13, 85);
            btnIndividualServerShifts.Name = "btnIndividualServerShifts";
            btnIndividualServerShifts.Size = new Size(259, 23);
            btnIndividualServerShifts.TabIndex = 3;
            btnIndividualServerShifts.Text = "See Server Shifts";
            btnIndividualServerShifts.UseVisualStyleBackColor = true;
            btnIndividualServerShifts.Visible = false;
            btnIndividualServerShifts.Click += btnIndividualServerShifts_Click;
            // 
            // lblComboLabel
            // 
            lblComboLabel.AutoSize = true;
            lblComboLabel.Location = new Point(13, 3);
            lblComboLabel.Name = "lblComboLabel";
            lblComboLabel.Size = new Size(74, 15);
            lblComboLabel.TabIndex = 2;
            lblComboLabel.Text = "Dining Areas";
            // 
            // btnIndividualStats
            // 
            btnIndividualStats.Location = new Point(13, 56);
            btnIndividualStats.Name = "btnIndividualStats";
            btnIndividualStats.Size = new Size(259, 23);
            btnIndividualStats.TabIndex = 1;
            btnIndividualStats.Text = "Server Table History";
            btnIndividualStats.UseVisualStyleBackColor = true;
            btnIndividualStats.Click += btnIndividualStats_Click;
            // 
            // cboServerSelect
            // 
            cboServerSelect.FormattingEnabled = true;
            cboServerSelect.Location = new Point(13, 21);
            cboServerSelect.Name = "cboServerSelect";
            cboServerSelect.Size = new Size(259, 23);
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
            cbJul.CheckedChanged += cbJul_CheckedChanged;
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
            cbDec.CheckedChanged += cbDec_CheckedChanged;
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
            cbJun.CheckedChanged += cbJun_CheckedChanged;
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
            cbNov.CheckedChanged += cbNov_CheckedChanged;
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
            cbMay.CheckedChanged += cbMay_CheckedChanged;
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
            cbOct.CheckedChanged += cbOct_CheckedChanged;
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
            cbSep.CheckedChanged += cbSep_CheckedChanged;
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
            cbApr.CheckedChanged += cbApr_CheckedChanged;
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
            cbAug.CheckedChanged += cbAug_CheckedChanged;
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
            cbMar.CheckedChanged += cbMar_CheckedChanged;
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
            cbFeb.CheckedChanged += cbFeb_CheckedChanged;
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
            cbJan.CheckedChanged += cbJan_CheckedChanged;
            // 
            // nudLowTemp
            // 
            nudLowTemp.Enabled = false;
            nudLowTemp.Location = new Point(521, 100);
            nudLowTemp.Name = "nudLowTemp";
            nudLowTemp.Size = new Size(60, 23);
            nudLowTemp.TabIndex = 9;
            nudLowTemp.Value = new decimal(new int[] { 32, 0, 0, 0 });
            nudLowTemp.ValueChanged += nudLowTemp_ValueChanged;
            // 
            // cbFilterByTempRange
            // 
            cbFilterByTempRange.AutoSize = true;
            cbFilterByTempRange.Location = new Point(521, 65);
            cbFilterByTempRange.Name = "cbFilterByTempRange";
            cbFilterByTempRange.Size = new Size(137, 19);
            cbFilterByTempRange.TabIndex = 10;
            cbFilterByTempRange.Text = "Filter By Temperature";
            cbFilterByTempRange.UseVisualStyleBackColor = true;
            cbFilterByTempRange.CheckedChanged += cbFilterByTempRange_CheckedChanged;
            // 
            // nudHiTemp
            // 
            nudHiTemp.Enabled = false;
            nudHiTemp.Location = new Point(618, 100);
            nudHiTemp.Name = "nudHiTemp";
            nudHiTemp.Size = new Size(60, 23);
            nudHiTemp.TabIndex = 9;
            nudHiTemp.Value = new decimal(new int[] { 85, 0, 0, 0 });
            nudHiTemp.ValueChanged += nudHiTemp_ValueChanged;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Enabled = false;
            lblTo.Location = new Point(587, 106);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(21, 15);
            lblTo.TabIndex = 11;
            lblTo.Text = "to:";
            // 
            // btnRefreshFilters
            // 
            btnRefreshFilters.FlatStyle = FlatStyle.Flat;
            btnRefreshFilters.Location = new Point(33, 247);
            btnRefreshFilters.Name = "btnRefreshFilters";
            btnRefreshFilters.Size = new Size(495, 35);
            btnRefreshFilters.TabIndex = 2;
            btnRefreshFilters.Text = "Get Data";
            btnRefreshFilters.UseVisualStyleBackColor = true;
            btnRefreshFilters.Click += btnRefreshFilters_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(30, 306);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1040, 564);
            dataGridView1.TabIndex = 12;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(30, 179);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(724, 53);
            flowDiningAreas.TabIndex = 13;
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1412, 874);
            Controls.Add(flowDiningAreas);
            Controls.Add(dataGridView1);
            Controls.Add(lblTo);
            Controls.Add(nudHiTemp);
            Controls.Add(cbFilterByTempRange);
            Controls.Add(nudLowTemp);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnRefreshFilters);
            Controls.Add(btnUpdate);
            Controls.Add(dgvDiningAreas);
            Name = "frmSalesStats";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmSalesStats";
            Load += frmSalesStats_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudLowTemp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHiTemp).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDiningAreas;
        private RadioButton rdoDiningAreaSales;
        private RadioButton rdoServerShifts;
        private Button btnUpdate;
        private RadioButton rdoAm;
        private RadioButton rdoPm;
        private RadioButton rdoBoth;
        private Panel panel1;
        private Panel panel2;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.Timer timer1;
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
        private NumericUpDown nudLowTemp;
        private CheckBox cbFilterByTempRange;
        private NumericUpDown nudHiTemp;
        private Label lblTo;
        private Button btnRefreshFilters;
        private DataGridView dataGridView1;
        private FlowLayoutPanel flowDiningAreas;
    }
}