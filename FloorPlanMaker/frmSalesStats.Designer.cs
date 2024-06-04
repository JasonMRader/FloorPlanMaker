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
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDiningAreas
            // 
            dgvDiningAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiningAreas.Location = new Point(12, 138);
            dgvDiningAreas.Name = "dgvDiningAreas";
            dgvDiningAreas.RowTemplate.Height = 25;
            dgvDiningAreas.Size = new Size(892, 352);
            dgvDiningAreas.TabIndex = 0;
            // 
            // rdoDiningAreaSales
            // 
            rdoDiningAreaSales.AutoSize = true;
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
            rdoServerShifts.TabStop = true;
            rdoServerShifts.Text = "Server Shift History";
            rdoServerShifts.UseVisualStyleBackColor = true;
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
            rdoPm.TabStop = true;
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
            rdoBoth.TabStop = true;
            rdoBoth.Text = "Both";
            rdoBoth.UseVisualStyleBackColor = true;
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
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(701, 29);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 5;
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
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(916, 566);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnUpdate);
            Controls.Add(dgvDiningAreas);
            Name = "frmSalesStats";
            Text = "frmSalesStats";
            Load += frmSalesStats_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
    }
}