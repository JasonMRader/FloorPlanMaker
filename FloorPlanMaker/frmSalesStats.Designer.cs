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
            lblComboLabel = new Label();
            btnIndividualStats = new Button();
            cboServerSelect = new ComboBox();
            btnIndividualServerShifts = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDiningAreas
            // 
            dgvDiningAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiningAreas.Location = new Point(12, 179);
            dgvDiningAreas.Name = "dgvDiningAreas";
            dgvDiningAreas.RowTemplate.Height = 25;
            dgvDiningAreas.Size = new Size(1035, 683);
            dgvDiningAreas.TabIndex = 0;
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
            btnUpdate.Location = new Point(33, 138);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(688, 23);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // rdoAm
            // 
            rdoAm.AutoSize = true;
            rdoAm.Checked = true;
            rdoAm.Location = new Point(0, 3);
            rdoAm.Name = "rdoAm";
            rdoAm.Size = new Size(44, 19);
            rdoAm.TabIndex = 3;
            rdoAm.TabStop = true;
            rdoAm.Text = "AM";
            rdoAm.UseVisualStyleBackColor = true;
            rdoAm.CheckedChanged += rdoAm_CheckedChanged;
            // 
            // rdoPm
            // 
            rdoPm.AutoSize = true;
            rdoPm.Location = new Point(69, 3);
            rdoPm.Name = "rdoPm";
            rdoPm.Size = new Size(43, 19);
            rdoPm.TabIndex = 3;
            rdoPm.Text = "PM";
            rdoPm.UseVisualStyleBackColor = true;
            // 
            // rdoBoth
            // 
            rdoBoth.AutoSize = true;
            rdoBoth.Location = new Point(126, 3);
            rdoBoth.Name = "rdoBoth";
            rdoBoth.Size = new Size(50, 19);
            rdoBoth.TabIndex = 3;
            rdoBoth.Text = "Both";
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
            cbAllWeekdays.Text = "Uncheck All";
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
            panel4.Location = new Point(760, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(287, 146);
            panel4.TabIndex = 8;
            // 
            // lblComboLabel
            // 
            lblComboLabel.AutoSize = true;
            lblComboLabel.Location = new Point(13, 18);
            lblComboLabel.Name = "lblComboLabel";
            lblComboLabel.Size = new Size(44, 15);
            lblComboLabel.TabIndex = 2;
            lblComboLabel.Text = "Servers";
            // 
            // btnIndividualStats
            // 
            btnIndividualStats.Location = new Point(13, 71);
            btnIndividualStats.Name = "btnIndividualStats";
            btnIndividualStats.Size = new Size(259, 23);
            btnIndividualStats.TabIndex = 1;
            btnIndividualStats.Text = "See Individual Stats";
            btnIndividualStats.UseVisualStyleBackColor = true;
            btnIndividualStats.Click += btnIndividualStats_Click;
            // 
            // cboServerSelect
            // 
            cboServerSelect.FormattingEnabled = true;
            cboServerSelect.Location = new Point(13, 36);
            cboServerSelect.Name = "cboServerSelect";
            cboServerSelect.Size = new Size(259, 23);
            cboServerSelect.TabIndex = 0;
            // 
            // btnIndividualServerShifts
            // 
            btnIndividualServerShifts.Location = new Point(13, 100);
            btnIndividualServerShifts.Name = "btnIndividualServerShifts";
            btnIndividualServerShifts.Size = new Size(259, 23);
            btnIndividualServerShifts.TabIndex = 3;
            btnIndividualServerShifts.Text = "See Server Shifts";
            btnIndividualServerShifts.UseVisualStyleBackColor = true;
            btnIndividualServerShifts.Click += btnIndividualServerShifts_Click;
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1059, 874);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(panel2);
            Controls.Add(panel1);
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
    }
}