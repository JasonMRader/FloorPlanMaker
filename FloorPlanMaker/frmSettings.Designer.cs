namespace FloorPlanMakerUI
{
    partial class frmSettings
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
            btnEditServers = new Button();
            btnImportSalesData = new Button();
            btnImportScheduleData = new Button();
            btnCheckForUpdate = new Button();
            btnBackUpDB = new Button();
            btnChooseDataBase = new Button();
            openFileDialog1 = new OpenFileDialog();
            panel1 = new Panel();
            label6 = new Label();
            lbUpcomingEvents = new ListBox();
            dtpMissingDateStart = new DateTimePicker();
            dtpMissingDateEnd = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbMissingData = new ListBox();
            btnEventDates = new Button();
            panel2 = new Panel();
            btnSaleStats = new Button();
            label5 = new Label();
            btnDeleteTemplate = new Button();
            nudTemplateID = new NumericUpDown();
            btnUpdateNotes = new Button();
            panel3 = new Panel();
            label7 = new Label();
            panel5 = new Panel();
            panel4 = new Panel();
            cboDiningAreas = new ComboBox();
            btnCreateTestData = new Button();
            label4 = new Label();
            dtpTestDataDate = new DateTimePicker();
            rdoAM = new RadioButton();
            rdoPM = new RadioButton();
            txtSales = new TextBox();
            btnTest = new Button();
            btnDeleteFloorplans = new Button();
            btnPastSection = new Button();
            btnHelpImportSales = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTemplateID).BeginInit();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // btnEditServers
            // 
            btnEditServers.BackColor = Color.FromArgb(100, 130, 180);
            btnEditServers.FlatAppearance.BorderSize = 0;
            btnEditServers.FlatStyle = FlatStyle.Flat;
            btnEditServers.ForeColor = Color.White;
            btnEditServers.Location = new Point(14, 180);
            btnEditServers.Name = "btnEditServers";
            btnEditServers.Size = new Size(322, 43);
            btnEditServers.TabIndex = 0;
            btnEditServers.Text = "Edit Servers";
            btnEditServers.UseVisualStyleBackColor = false;
            btnEditServers.Click += btnEditServers_Click;
            // 
            // btnImportSalesData
            // 
            btnImportSalesData.BackColor = Color.FromArgb(100, 130, 180);
            btnImportSalesData.FlatAppearance.BorderSize = 0;
            btnImportSalesData.FlatStyle = FlatStyle.Flat;
            btnImportSalesData.ForeColor = Color.White;
            btnImportSalesData.Location = new Point(12, 66);
            btnImportSalesData.Name = "btnImportSalesData";
            btnImportSalesData.Size = new Size(322, 43);
            btnImportSalesData.TabIndex = 0;
            btnImportSalesData.Text = "Import Sales Data";
            btnImportSalesData.UseVisualStyleBackColor = false;
            btnImportSalesData.Click += btnImportSalesData_Click;
            // 
            // btnImportScheduleData
            // 
            btnImportScheduleData.BackColor = Color.FromArgb(100, 130, 180);
            btnImportScheduleData.FlatAppearance.BorderSize = 0;
            btnImportScheduleData.FlatStyle = FlatStyle.Flat;
            btnImportScheduleData.ForeColor = Color.White;
            btnImportScheduleData.Location = new Point(14, 246);
            btnImportScheduleData.Name = "btnImportScheduleData";
            btnImportScheduleData.Size = new Size(322, 43);
            btnImportScheduleData.TabIndex = 0;
            btnImportScheduleData.Text = "Import Weather Data";
            btnImportScheduleData.UseVisualStyleBackColor = false;
            btnImportScheduleData.Click += btnImportScheduleData_Click;
            // 
            // btnCheckForUpdate
            // 
            btnCheckForUpdate.BackColor = Color.FromArgb(100, 130, 180);
            btnCheckForUpdate.Enabled = false;
            btnCheckForUpdate.FlatAppearance.BorderSize = 0;
            btnCheckForUpdate.FlatStyle = FlatStyle.Flat;
            btnCheckForUpdate.ForeColor = Color.White;
            btnCheckForUpdate.Location = new Point(14, 194);
            btnCheckForUpdate.Name = "btnCheckForUpdate";
            btnCheckForUpdate.Size = new Size(322, 43);
            btnCheckForUpdate.TabIndex = 0;
            btnCheckForUpdate.Text = "Check For Update";
            btnCheckForUpdate.UseVisualStyleBackColor = false;
            // 
            // btnBackUpDB
            // 
            btnBackUpDB.BackColor = Color.FromArgb(100, 130, 180);
            btnBackUpDB.FlatAppearance.BorderSize = 0;
            btnBackUpDB.FlatStyle = FlatStyle.Flat;
            btnBackUpDB.ForeColor = Color.White;
            btnBackUpDB.Location = new Point(14, 47);
            btnBackUpDB.Name = "btnBackUpDB";
            btnBackUpDB.Size = new Size(322, 43);
            btnBackUpDB.TabIndex = 0;
            btnBackUpDB.Text = "Back Up DataBase";
            btnBackUpDB.UseVisualStyleBackColor = false;
            btnBackUpDB.Click += btnBackUpDB_Click;
            // 
            // btnChooseDataBase
            // 
            btnChooseDataBase.BackColor = Color.FromArgb(100, 130, 180);
            btnChooseDataBase.FlatAppearance.BorderSize = 0;
            btnChooseDataBase.FlatStyle = FlatStyle.Flat;
            btnChooseDataBase.ForeColor = Color.White;
            btnChooseDataBase.Location = new Point(14, 115);
            btnChooseDataBase.Name = "btnChooseDataBase";
            btnChooseDataBase.Size = new Size(322, 43);
            btnChooseDataBase.TabIndex = 0;
            btnChooseDataBase.Text = "Select DataBase Location";
            btnChooseDataBase.UseVisualStyleBackColor = false;
            btnChooseDataBase.Click += btnChooseDataBase_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(btnHelpImportSales);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(lbUpcomingEvents);
            panel1.Controls.Add(dtpMissingDateStart);
            panel1.Controls.Add(dtpMissingDateEnd);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbMissingData);
            panel1.Controls.Add(btnImportSalesData);
            panel1.Controls.Add(btnEventDates);
            panel1.Location = new Point(47, 92);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 800);
            panel1.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(12, 526);
            label6.Name = "label6";
            label6.Size = new Size(137, 21);
            label6.TabIndex = 5;
            label6.Text = "Upcoming Events";
            // 
            // lbUpcomingEvents
            // 
            lbUpcomingEvents.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lbUpcomingEvents.FormattingEnabled = true;
            lbUpcomingEvents.ItemHeight = 25;
            lbUpcomingEvents.Location = new Point(12, 557);
            lbUpcomingEvents.Name = "lbUpcomingEvents";
            lbUpcomingEvents.Size = new Size(322, 204);
            lbUpcomingEvents.TabIndex = 4;
            // 
            // dtpMissingDateStart
            // 
            dtpMissingDateStart.Location = new Point(134, 123);
            dtpMissingDateStart.Name = "dtpMissingDateStart";
            dtpMissingDateStart.Size = new Size(200, 23);
            dtpMissingDateStart.TabIndex = 3;
            dtpMissingDateStart.ValueChanged += dtpMissingDateStart_ValueChanged;
            // 
            // dtpMissingDateEnd
            // 
            dtpMissingDateEnd.Location = new Point(134, 160);
            dtpMissingDateEnd.Name = "dtpMissingDateEnd";
            dtpMissingDateEnd.Size = new Size(200, 23);
            dtpMissingDateEnd.TabIndex = 3;
            dtpMissingDateEnd.ValueChanged += dtpMissingDateEnd_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(109, 164);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 2;
            label3.Text = "To";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 127);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "From";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 208);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 2;
            label1.Text = "Missing Sales Data";
            // 
            // lbMissingData
            // 
            lbMissingData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbMissingData.FormattingEnabled = true;
            lbMissingData.ItemHeight = 21;
            lbMissingData.Location = new Point(12, 229);
            lbMissingData.Name = "lbMissingData";
            lbMissingData.Size = new Size(322, 193);
            lbMissingData.TabIndex = 1;
            // 
            // btnEventDates
            // 
            btnEventDates.BackColor = Color.FromArgb(100, 130, 180);
            btnEventDates.FlatAppearance.BorderSize = 0;
            btnEventDates.FlatStyle = FlatStyle.Flat;
            btnEventDates.ForeColor = Color.White;
            btnEventDates.Location = new Point(12, 465);
            btnEventDates.Name = "btnEventDates";
            btnEventDates.Size = new Size(322, 43);
            btnEventDates.TabIndex = 0;
            btnEventDates.Text = "Manage Event Dates";
            btnEventDates.UseVisualStyleBackColor = false;
            btnEventDates.Click += btnEventDates_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Controls.Add(btnBackUpDB);
            panel2.Controls.Add(btnChooseDataBase);
            panel2.Controls.Add(btnImportScheduleData);
            panel2.Controls.Add(btnEditServers);
            panel2.Controls.Add(btnSaleStats);
            panel2.Location = new Point(459, 92);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 800);
            panel2.TabIndex = 2;
            // 
            // btnSaleStats
            // 
            btnSaleStats.BackColor = Color.FromArgb(100, 130, 180);
            btnSaleStats.FlatAppearance.BorderSize = 0;
            btnSaleStats.FlatStyle = FlatStyle.Flat;
            btnSaleStats.ForeColor = Color.White;
            btnSaleStats.Location = new Point(14, 317);
            btnSaleStats.Name = "btnSaleStats";
            btnSaleStats.Size = new Size(322, 43);
            btnSaleStats.TabIndex = 0;
            btnSaleStats.Text = "Sales Stats";
            btnSaleStats.UseVisualStyleBackColor = false;
            btnSaleStats.Click += btnSaleStats_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 27);
            label5.Name = "label5";
            label5.Size = new Size(18, 15);
            label5.TabIndex = 3;
            label5.Text = "ID";
            // 
            // btnDeleteTemplate
            // 
            btnDeleteTemplate.Location = new Point(77, 45);
            btnDeleteTemplate.Name = "btnDeleteTemplate";
            btnDeleteTemplate.Size = new Size(152, 23);
            btnDeleteTemplate.TabIndex = 2;
            btnDeleteTemplate.Text = "Delete Template by ID";
            btnDeleteTemplate.UseVisualStyleBackColor = true;
            btnDeleteTemplate.Click += btnDeleteTemplate_Click;
            // 
            // nudTemplateID
            // 
            nudTemplateID.Location = new Point(12, 45);
            nudTemplateID.Name = "nudTemplateID";
            nudTemplateID.Size = new Size(48, 23);
            nudTemplateID.TabIndex = 1;
            // 
            // btnUpdateNotes
            // 
            btnUpdateNotes.BackColor = Color.FromArgb(100, 130, 180);
            btnUpdateNotes.Enabled = false;
            btnUpdateNotes.FlatAppearance.BorderSize = 0;
            btnUpdateNotes.FlatStyle = FlatStyle.Flat;
            btnUpdateNotes.ForeColor = Color.White;
            btnUpdateNotes.Location = new Point(14, 140);
            btnUpdateNotes.Name = "btnUpdateNotes";
            btnUpdateNotes.Size = new Size(322, 43);
            btnUpdateNotes.TabIndex = 0;
            btnUpdateNotes.Text = "Version History";
            btnUpdateNotes.UseVisualStyleBackColor = false;
            btnUpdateNotes.Click += btnUpdateNotes_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 190, 200);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(btnTest);
            panel3.Controls.Add(btnUpdateNotes);
            panel3.Controls.Add(btnDeleteFloorplans);
            panel3.Controls.Add(btnPastSection);
            panel3.Controls.Add(btnCheckForUpdate);
            panel3.Location = new Point(871, 92);
            panel3.Name = "panel3";
            panel3.Size = new Size(350, 800);
            panel3.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 18);
            label7.Name = "label7";
            label7.Size = new Size(159, 15);
            label7.TabIndex = 9;
            label7.Text = "Testing / under development";
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(255, 224, 192);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(nudTemplateID);
            panel5.Controls.Add(btnDeleteTemplate);
            panel5.Location = new Point(22, 559);
            panel5.Name = "panel5";
            panel5.Size = new Size(314, 100);
            panel5.TabIndex = 8;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 224, 192);
            panel4.Controls.Add(cboDiningAreas);
            panel4.Controls.Add(btnCreateTestData);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(dtpTestDataDate);
            panel4.Controls.Add(rdoAM);
            panel4.Controls.Add(rdoPM);
            panel4.Controls.Add(txtSales);
            panel4.Location = new Point(14, 301);
            panel4.Name = "panel4";
            panel4.Size = new Size(322, 205);
            panel4.TabIndex = 7;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(0, 21);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(322, 23);
            cboDiningAreas.TabIndex = 2;
            // 
            // btnCreateTestData
            // 
            btnCreateTestData.BackColor = Color.FromArgb(255, 192, 192);
            btnCreateTestData.Location = new Point(0, 148);
            btnCreateTestData.Name = "btnCreateTestData";
            btnCreateTestData.Size = new Size(322, 23);
            btnCreateTestData.TabIndex = 1;
            btnCreateTestData.Text = "Create Test Data";
            btnCreateTestData.UseVisualStyleBackColor = false;
            btnCreateTestData.Click += btnCreateTestData_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 101);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 6;
            label4.Text = "Amount Per Table";
            // 
            // dtpTestDataDate
            // 
            dtpTestDataDate.Location = new Point(0, 50);
            dtpTestDataDate.Name = "dtpTestDataDate";
            dtpTestDataDate.Size = new Size(322, 23);
            dtpTestDataDate.TabIndex = 3;
            // 
            // rdoAM
            // 
            rdoAM.AutoSize = true;
            rdoAM.Checked = true;
            rdoAM.Location = new Point(8, 79);
            rdoAM.Name = "rdoAM";
            rdoAM.Size = new Size(44, 19);
            rdoAM.TabIndex = 4;
            rdoAM.TabStop = true;
            rdoAM.Text = "AM";
            rdoAM.UseVisualStyleBackColor = true;
            // 
            // rdoPM
            // 
            rdoPM.AutoSize = true;
            rdoPM.Location = new Point(118, 79);
            rdoPM.Name = "rdoPM";
            rdoPM.Size = new Size(43, 19);
            rdoPM.TabIndex = 4;
            rdoPM.Text = "PM";
            rdoPM.UseVisualStyleBackColor = true;
            // 
            // txtSales
            // 
            txtSales.Location = new Point(0, 119);
            txtSales.Name = "txtSales";
            txtSales.Size = new Size(100, 23);
            txtSales.TabIndex = 5;
            // 
            // btnTest
            // 
            btnTest.BackColor = Color.FromArgb(100, 130, 180);
            btnTest.FlatAppearance.BorderSize = 0;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.ForeColor = Color.White;
            btnTest.Location = new Point(14, 66);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(322, 43);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test Button";
            btnTest.UseVisualStyleBackColor = false;
            btnTest.Click += btnTest_Click;
            // 
            // btnDeleteFloorplans
            // 
            btnDeleteFloorplans.BackColor = Color.FromArgb(190, 80, 70);
            btnDeleteFloorplans.FlatAppearance.BorderSize = 0;
            btnDeleteFloorplans.FlatStyle = FlatStyle.Flat;
            btnDeleteFloorplans.ForeColor = Color.White;
            btnDeleteFloorplans.Location = new Point(115, 749);
            btnDeleteFloorplans.Name = "btnDeleteFloorplans";
            btnDeleteFloorplans.Size = new Size(136, 25);
            btnDeleteFloorplans.TabIndex = 0;
            btnDeleteFloorplans.Text = "Delete All Floorplans";
            btnDeleteFloorplans.UseVisualStyleBackColor = false;
            btnDeleteFloorplans.Click += btnDeleteFloorplans_Click;
            // 
            // btnPastSection
            // 
            btnPastSection.BackColor = Color.FromArgb(100, 130, 180);
            btnPastSection.FlatAppearance.BorderSize = 0;
            btnPastSection.FlatStyle = FlatStyle.Flat;
            btnPastSection.ForeColor = Color.White;
            btnPastSection.Location = new Point(14, 243);
            btnPastSection.Name = "btnPastSection";
            btnPastSection.Size = new Size(322, 43);
            btnPastSection.TabIndex = 0;
            btnPastSection.Text = "Get Sections for Past Shift";
            btnPastSection.UseVisualStyleBackColor = false;
            btnPastSection.Click += btnPastSection_Click;
            // 
            // btnHelpImportSales
            // 
            btnHelpImportSales.BackColor = Color.FromArgb(120, 180, 120);
            btnHelpImportSales.FlatAppearance.BorderSize = 0;
            btnHelpImportSales.FlatStyle = FlatStyle.Flat;
            btnHelpImportSales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnHelpImportSales.ForeColor = Color.Black;
            btnHelpImportSales.Location = new Point(303, 18);
            btnHelpImportSales.Name = "btnHelpImportSales";
            btnHelpImportSales.Size = new Size(31, 29);
            btnHelpImportSales.TabIndex = 14;
            btnHelpImportSales.Text = "?";
            btnHelpImportSales.UseVisualStyleBackColor = false;
            btnHelpImportSales.Click += btnHelpImportSales_Click;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 999);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSettings";
            Text = "frmSettings";
            Load += frmSettings_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudTemplateID).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnEditServers;
        private Button button2;
        private Button button3;
        private Button btnImportSalesData;
        private Button btnImportScheduleData;
        private Button btnCheckForUpdate;
        private Button btnBackUpDB;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button btnChooseDataBase;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Label label1;
        private ListBox lbMissingData;
        private Panel panel2;
        private Panel panel3;
        private DateTimePicker dtpMissingDateStart;
        private DateTimePicker dtpMissingDateEnd;
        private Label label3;
        private Label label2;
        private Button btnDeleteFloorplans;
        private Button btnPastSection;
        private Button btnCreateTestData;
        private Label label4;
        private TextBox txtSales;
        private RadioButton rdoPM;
        private RadioButton rdoAM;
        private DateTimePicker dtpTestDataDate;
        private ComboBox cboDiningAreas;
        private Button btnDeleteTemplate;
        private NumericUpDown nudTemplateID;
        private Label label5;
        private Button btnUpdateNotes;
        private Button btnSaleStats;
        private Label label6;
        private ListBox lbUpcomingEvents;
        private Button btnEventDates;
        private Label label7;
        private Panel panel5;
        private Panel panel4;
        private Button btnTest;
        private Button btnHelpImportSales;
    }
}