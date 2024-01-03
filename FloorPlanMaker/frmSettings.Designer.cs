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
            this.btnImportScheduleData.BackColor = Color.FromArgb(100, 130, 180);
            this.btnImportScheduleData.FlatAppearance.BorderSize = 0;
            this.btnImportScheduleData.FlatStyle = FlatStyle.Flat;
            this.btnImportScheduleData.ForeColor = Color.White;
            this.btnImportScheduleData.Location = new Point(171, 224);
            this.btnImportScheduleData.Name = "btnImportScheduleData";
            this.btnImportScheduleData.Size = new Size(146, 43);
            this.btnImportScheduleData.TabIndex = 0;
            this.btnImportScheduleData.Text = "Import Schedule Data";
            this.btnImportScheduleData.UseVisualStyleBackColor = false;
            // 
            // btnCheckForUpdate
            // 
            this.btnCheckForUpdate.BackColor = Color.FromArgb(100, 130, 180);
            this.btnCheckForUpdate.FlatAppearance.BorderSize = 0;
            this.btnCheckForUpdate.FlatStyle = FlatStyle.Flat;
            this.btnCheckForUpdate.ForeColor = Color.White;
            this.btnCheckForUpdate.Location = new Point(171, 305);
            this.btnCheckForUpdate.Name = "btnCheckForUpdate";
            this.btnCheckForUpdate.Size = new Size(146, 43);
            this.btnCheckForUpdate.TabIndex = 0;
            this.btnCheckForUpdate.Text = "Check For Update";
            this.btnCheckForUpdate.UseVisualStyleBackColor = false;
            // 
            // btnBackUpDB
            // 
            this.btnBackUpDB.BackColor = Color.FromArgb(100, 130, 180);
            this.btnBackUpDB.FlatAppearance.BorderSize = 0;
            this.btnBackUpDB.FlatStyle = FlatStyle.Flat;
            this.btnBackUpDB.ForeColor = Color.White;
            this.btnBackUpDB.Location = new Point(171, 387);
            this.btnBackUpDB.Name = "btnBackUpDB";
            this.btnBackUpDB.Size = new Size(146, 43);
            this.btnBackUpDB.TabIndex = 0;
            this.btnBackUpDB.Text = "Back Up DataBase";
            this.btnBackUpDB.UseVisualStyleBackColor = false;
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
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 999);
            Controls.Add(btnChooseDataBase);
            Controls.Add(this.btnBackUpDB);
            Controls.Add(this.btnCheckForUpdate);
            Controls.Add(this.btnImportScheduleData);
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