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
            dtpMissingDateStart = new DateTimePicker();
            dtpMissingDateEnd = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbMissingData = new ListBox();
            panel2 = new Panel();
            label5 = new Label();
            btnDeleteTemplate = new Button();
            nudTemplateID = new NumericUpDown();
            btnUpdateNotes = new Button();
            panel3 = new Panel();
            label4 = new Label();
            txtSales = new TextBox();
            rdoPM = new RadioButton();
            rdoAM = new RadioButton();
            dtpTestDataDate = new DateTimePicker();
            cboDiningAreas = new ComboBox();
            btnCreateTestData = new Button();
            btnDeleteFloorplans = new Button();
            btnSaleStats = new Button();
            btnPastSection = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTemplateID).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnEditServers
            // 
            btnEditServers.BackColor = Color.FromArgb(100, 130, 180);
            btnEditServers.FlatAppearance.BorderSize = 0;
            btnEditServers.FlatStyle = FlatStyle.Flat;
            btnEditServers.ForeColor = Color.White;
            btnEditServers.Location = new Point(14, 66);
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
            btnImportScheduleData.Enabled = false;
            btnImportScheduleData.FlatAppearance.BorderSize = 0;
            btnImportScheduleData.FlatStyle = FlatStyle.Flat;
            btnImportScheduleData.ForeColor = Color.White;
            btnImportScheduleData.Location = new Point(14, 115);
            btnImportScheduleData.Name = "btnImportScheduleData";
            btnImportScheduleData.Size = new Size(322, 43);
            btnImportScheduleData.TabIndex = 0;
            btnImportScheduleData.Text = "Import Schedule Data";
            btnImportScheduleData.UseVisualStyleBackColor = false;
            // 
            // btnCheckForUpdate
            // 
            btnCheckForUpdate.BackColor = Color.FromArgb(100, 130, 180);
            btnCheckForUpdate.Enabled = false;
            btnCheckForUpdate.FlatAppearance.BorderSize = 0;
            btnCheckForUpdate.FlatStyle = FlatStyle.Flat;
            btnCheckForUpdate.ForeColor = Color.White;
            btnCheckForUpdate.Location = new Point(14, 164);
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
            panel1.Controls.Add(dtpMissingDateStart);
            panel1.Controls.Add(dtpMissingDateEnd);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbMissingData);
            panel1.Controls.Add(btnImportSalesData);
            panel1.Location = new Point(47, 92);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 800);
            panel1.TabIndex = 1;
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
            lbMissingData.FormattingEnabled = true;
            lbMissingData.ItemHeight = 15;
            lbMissingData.Location = new Point(12, 229);
            lbMissingData.Name = "lbMissingData";
            lbMissingData.Size = new Size(322, 199);
            lbMissingData.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(btnDeleteTemplate);
            panel2.Controls.Add(nudTemplateID);
            panel2.Controls.Add(btnBackUpDB);
            panel2.Controls.Add(btnChooseDataBase);
            panel2.Controls.Add(btnUpdateNotes);
            panel2.Location = new Point(459, 92);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 800);
            panel2.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 490);
            label5.Name = "label5";
            label5.Size = new Size(18, 15);
            label5.TabIndex = 3;
            label5.Text = "ID";
            // 
            // btnDeleteTemplate
            // 
            btnDeleteTemplate.Location = new Point(79, 508);
            btnDeleteTemplate.Name = "btnDeleteTemplate";
            btnDeleteTemplate.Size = new Size(152, 23);
            btnDeleteTemplate.TabIndex = 2;
            btnDeleteTemplate.Text = "Delete Template by ID";
            btnDeleteTemplate.UseVisualStyleBackColor = true;
            btnDeleteTemplate.Click += btnDeleteTemplate_Click;
            // 
            // nudTemplateID
            // 
            nudTemplateID.Location = new Point(14, 508);
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
            btnUpdateNotes.Location = new Point(14, 208);
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
            panel3.Controls.Add(label4);
            panel3.Controls.Add(txtSales);
            panel3.Controls.Add(rdoPM);
            panel3.Controls.Add(rdoAM);
            panel3.Controls.Add(dtpTestDataDate);
            panel3.Controls.Add(cboDiningAreas);
            panel3.Controls.Add(btnCreateTestData);
            panel3.Controls.Add(btnEditServers);
            panel3.Controls.Add(btnImportScheduleData);
            panel3.Controls.Add(btnDeleteFloorplans);
            panel3.Controls.Add(btnSaleStats);
            panel3.Controls.Add(btnPastSection);
            panel3.Controls.Add(btnCheckForUpdate);
            panel3.Location = new Point(871, 92);
            panel3.Name = "panel3";
            panel3.Size = new Size(350, 800);
            panel3.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 508);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 6;
            label4.Text = "Amount Per Table";
            // 
            // txtSales
            // 
            txtSales.Location = new Point(14, 526);
            txtSales.Name = "txtSales";
            txtSales.Size = new Size(100, 23);
            txtSales.TabIndex = 5;
            // 
            // rdoPM
            // 
            rdoPM.AutoSize = true;
            rdoPM.Location = new Point(132, 477);
            rdoPM.Name = "rdoPM";
            rdoPM.Size = new Size(43, 19);
            rdoPM.TabIndex = 4;
            rdoPM.Text = "PM";
            rdoPM.UseVisualStyleBackColor = true;
            // 
            // rdoAM
            // 
            rdoAM.AutoSize = true;
            rdoAM.Checked = true;
            rdoAM.Location = new Point(14, 477);
            rdoAM.Name = "rdoAM";
            rdoAM.Size = new Size(44, 19);
            rdoAM.TabIndex = 4;
            rdoAM.TabStop = true;
            rdoAM.Text = "AM";
            rdoAM.UseVisualStyleBackColor = true;
            // 
            // dtpTestDataDate
            // 
            dtpTestDataDate.Location = new Point(14, 448);
            dtpTestDataDate.Name = "dtpTestDataDate";
            dtpTestDataDate.Size = new Size(322, 23);
            dtpTestDataDate.TabIndex = 3;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(14, 419);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(322, 23);
            cboDiningAreas.TabIndex = 2;
            // 
            // btnCreateTestData
            // 
            btnCreateTestData.BackColor = Color.FromArgb(255, 192, 192);
            btnCreateTestData.Location = new Point(14, 555);
            btnCreateTestData.Name = "btnCreateTestData";
            btnCreateTestData.Size = new Size(322, 23);
            btnCreateTestData.TabIndex = 1;
            btnCreateTestData.Text = "Create Test Data";
            btnCreateTestData.UseVisualStyleBackColor = false;
            btnCreateTestData.Click += btnCreateTestData_Click;
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
            // btnSaleStats
            // 
            btnSaleStats.BackColor = Color.FromArgb(100, 130, 180);
            btnSaleStats.FlatAppearance.BorderSize = 0;
            btnSaleStats.FlatStyle = FlatStyle.Flat;
            btnSaleStats.ForeColor = Color.White;
            btnSaleStats.Location = new Point(14, 262);
            btnSaleStats.Name = "btnSaleStats";
            btnSaleStats.Size = new Size(322, 43);
            btnSaleStats.TabIndex = 0;
            btnSaleStats.Text = "Sales Stats";
            btnSaleStats.UseVisualStyleBackColor = false;
            btnSaleStats.Click += btnSaleStats_Click;
            // 
            // btnPastSection
            // 
            btnPastSection.BackColor = Color.FromArgb(100, 130, 180);
            btnPastSection.FlatAppearance.BorderSize = 0;
            btnPastSection.FlatStyle = FlatStyle.Flat;
            btnPastSection.ForeColor = Color.White;
            btnPastSection.Location = new Point(14, 213);
            btnPastSection.Name = "btnPastSection";
            btnPastSection.Size = new Size(322, 43);
            btnPastSection.TabIndex = 0;
            btnPastSection.Text = "Get Sections for Past Shift";
            btnPastSection.UseVisualStyleBackColor = false;
            btnPastSection.Click += btnPastSection_Click;
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
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTemplateID).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
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
    }
}