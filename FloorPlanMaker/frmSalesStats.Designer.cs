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
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDiningAreas
            // 
            dgvDiningAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiningAreas.Location = new Point(12, 138);
            dgvDiningAreas.Name = "dgvDiningAreas";
            dgvDiningAreas.RowTemplate.Height = 25;
            dgvDiningAreas.Size = new Size(892, 683);
            dgvDiningAreas.TabIndex = 0;
            // 
            // rdoDiningAreaSales
            // 
            rdoDiningAreaSales.AutoSize = true;
            rdoDiningAreaSales.Checked = true;
            rdoDiningAreaSales.Location = new Point(3, 17);
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
            rdoServerShifts.Location = new Point(137, 17);
            rdoServerShifts.Name = "rdoServerShifts";
            rdoServerShifts.Size = new Size(125, 19);
            rdoServerShifts.TabIndex = 1;
            rdoServerShifts.Text = "Server Shift History";
            rdoServerShifts.UseVisualStyleBackColor = true;
            rdoServerShifts.CheckedChanged += rdoServerShifts_CheckedChanged;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(746, 109);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(158, 23);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // rdoAm
            // 
            rdoAm.AutoSize = true;
            rdoAm.Checked = true;
            rdoAm.Location = new Point(0, 14);
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
            rdoPm.Location = new Point(68, 14);
            rdoPm.Name = "rdoPm";
            rdoPm.Size = new Size(43, 19);
            rdoPm.TabIndex = 3;
            rdoPm.Text = "PM";
            rdoPm.UseVisualStyleBackColor = true;
            // 
            // rdoBoth
            // 
            rdoBoth.AutoSize = true;
            rdoBoth.Location = new Point(117, 14);
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
            panel1.Size = new Size(280, 54);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(rdoBoth);
            panel2.Controls.Add(rdoAm);
            panel2.Controls.Add(rdoPm);
            panel2.Location = new Point(33, 72);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 54);
            panel2.TabIndex = 4;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(490, 29);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 5;
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(701, 29);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 5;
            dtpEndDate.ValueChanged += dtpEndDate_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(490, 11);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 6;
            label1.Text = "From:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(701, 11);
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
            panel3.Location = new Point(276, 72);
            panel3.Name = "panel3";
            panel3.Size = new Size(464, 54);
            panel3.TabIndex = 7;
            // 
            // cbAllWeekdays
            // 
            cbAllWeekdays.Appearance = Appearance.Button;
            cbAllWeekdays.Checked = true;
            cbAllWeekdays.CheckState = CheckState.Checked;
            cbAllWeekdays.Location = new Point(377, 14);
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
            cbSun.Location = new Point(325, 14);
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
            cbSat.Location = new Point(273, 14);
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
            cbFri.Location = new Point(221, 14);
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
            cbThurs.Location = new Point(169, 14);
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
            cbWed.Location = new Point(117, 14);
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
            cbTues.Location = new Point(65, 14);
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
            cbMon.Location = new Point(13, 14);
            cbMon.Name = "cbMon";
            cbMon.Size = new Size(46, 25);
            cbMon.TabIndex = 0;
            cbMon.Text = "Mon";
            cbMon.UseVisualStyleBackColor = true;
            cbMon.CheckedChanged += cbMon_CheckedChanged;
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(916, 833);
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
    }
}