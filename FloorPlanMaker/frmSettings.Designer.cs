﻿namespace FloorPlanMakerUI
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
            panel3 = new Panel();
            btnDeleteFloorplans = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnEditServers
            // 
            btnEditServers.BackColor = Color.FromArgb(100, 130, 180);
            btnEditServers.Enabled = false;
            btnEditServers.FlatAppearance.BorderSize = 0;
            btnEditServers.FlatStyle = FlatStyle.Flat;
            btnEditServers.ForeColor = Color.White;
            btnEditServers.Location = new Point(14, 66);
            btnEditServers.Name = "btnEditServers";
            btnEditServers.Size = new Size(322, 43);
            btnEditServers.TabIndex = 0;
            btnEditServers.Text = "Add Servers";
            btnEditServers.UseVisualStyleBackColor = false;
            btnEditServers.Click += button1_Click;
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
            btnBackUpDB.Location = new Point(14, 66);
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
            panel2.Controls.Add(btnBackUpDB);
            panel2.Controls.Add(btnChooseDataBase);
            panel2.Location = new Point(459, 92);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 800);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 190, 200);
            panel3.Controls.Add(btnEditServers);
            panel3.Controls.Add(btnImportScheduleData);
            panel3.Controls.Add(btnDeleteFloorplans);
            panel3.Controls.Add(btnCheckForUpdate);
            panel3.Location = new Point(871, 92);
            panel3.Name = "panel3";
            panel3.Size = new Size(350, 800);
            panel3.TabIndex = 2;
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
            panel3.ResumeLayout(false);
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
    }
}