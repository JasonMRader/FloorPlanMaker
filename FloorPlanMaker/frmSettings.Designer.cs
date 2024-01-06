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
            SuspendLayout();
            // 
            // btnEditServers
            // 
            btnEditServers.BackColor = Color.FromArgb(100, 130, 180);
            btnEditServers.FlatAppearance.BorderSize = 0;
            btnEditServers.FlatStyle = FlatStyle.Flat;
            btnEditServers.ForeColor = Color.White;
            btnEditServers.Location = new Point(171, 71);
            btnEditServers.Name = "btnEditServers";
            btnEditServers.Size = new Size(146, 43);
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
            btnImportSalesData.Location = new Point(171, 147);
            btnImportSalesData.Name = "btnImportSalesData";
            btnImportSalesData.Size = new Size(146, 43);
            btnImportSalesData.TabIndex = 0;
            btnImportSalesData.Text = "Import Sales Data";
            btnImportSalesData.UseVisualStyleBackColor = false;
            // 
            // btnImportScheduleData
            // 
            btnImportScheduleData.BackColor = Color.FromArgb(100, 130, 180);
            btnImportScheduleData.FlatAppearance.BorderSize = 0;
            btnImportScheduleData.FlatStyle = FlatStyle.Flat;
            btnImportScheduleData.ForeColor = Color.White;
            btnImportScheduleData.Location = new Point(171, 224);
            btnImportScheduleData.Name = "btnImportScheduleData";
            btnImportScheduleData.Size = new Size(146, 43);
            btnImportScheduleData.TabIndex = 0;
            btnImportScheduleData.Text = "Import Schedule Data";
            btnImportScheduleData.UseVisualStyleBackColor = false;
            // 
            // btnCheckForUpdate
            // 
            btnCheckForUpdate.BackColor = Color.FromArgb(100, 130, 180);
            btnCheckForUpdate.FlatAppearance.BorderSize = 0;
            btnCheckForUpdate.FlatStyle = FlatStyle.Flat;
            btnCheckForUpdate.ForeColor = Color.White;
            btnCheckForUpdate.Location = new Point(171, 305);
            btnCheckForUpdate.Name = "btnCheckForUpdate";
            btnCheckForUpdate.Size = new Size(146, 43);
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
            btnBackUpDB.Location = new Point(171, 387);
            btnBackUpDB.Name = "btnBackUpDB";
            btnBackUpDB.Size = new Size(146, 43);
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
            btnChooseDataBase.Location = new Point(171, 466);
            btnChooseDataBase.Name = "btnChooseDataBase";
            btnChooseDataBase.Size = new Size(146, 43);
            btnChooseDataBase.TabIndex = 0;
            btnChooseDataBase.Text = "Select DataBase";
            btnChooseDataBase.UseVisualStyleBackColor = false;
            btnChooseDataBase.Click += btnChooseDataBase_Click;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 999);
            Controls.Add(btnChooseDataBase);
            Controls.Add(btnBackUpDB);
            Controls.Add(btnCheckForUpdate);
            Controls.Add(btnImportScheduleData);
            Controls.Add(btnImportSalesData);
            Controls.Add(btnEditServers);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSettings";
            Text = "frmSettings";
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
    }
}