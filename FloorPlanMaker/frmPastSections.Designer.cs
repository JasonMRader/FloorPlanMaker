namespace FloorPlanMakerUI
{
    partial class frmPastSections
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
            tvPastServerTables = new TreeView();
            btnReadEntireFile = new Button();
            btnReadSpecificDate = new Button();
            dtpShiftDate = new DateTimePicker();
            rdoAM = new RadioButton();
            rdoPM = new RadioButton();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // tvPastServerTables
            // 
            tvPastServerTables.Location = new Point(12, 449);
            tvPastServerTables.Name = "tvPastServerTables";
            tvPastServerTables.Size = new Size(913, 75);
            tvPastServerTables.TabIndex = 1;
            // 
            // btnReadEntireFile
            // 
            btnReadEntireFile.Location = new Point(12, 12);
            btnReadEntireFile.Name = "btnReadEntireFile";
            btnReadEntireFile.Size = new Size(160, 23);
            btnReadEntireFile.TabIndex = 3;
            btnReadEntireFile.Text = "Entire File";
            btnReadEntireFile.UseVisualStyleBackColor = true;
            btnReadEntireFile.Click += btnReadEntireFile_Click;
            // 
            // btnReadSpecificDate
            // 
            btnReadSpecificDate.Location = new Point(214, 12);
            btnReadSpecificDate.Name = "btnReadSpecificDate";
            btnReadSpecificDate.Size = new Size(160, 23);
            btnReadSpecificDate.TabIndex = 3;
            btnReadSpecificDate.Text = "Specific Shift";
            btnReadSpecificDate.UseVisualStyleBackColor = true;
            btnReadSpecificDate.Click += btnReadSpecificDate_Click;
            // 
            // dtpShiftDate
            // 
            dtpShiftDate.Location = new Point(413, 12);
            dtpShiftDate.Name = "dtpShiftDate";
            dtpShiftDate.Size = new Size(200, 23);
            dtpShiftDate.TabIndex = 4;
            // 
            // rdoAM
            // 
            rdoAM.AutoSize = true;
            rdoAM.Location = new Point(646, 14);
            rdoAM.Name = "rdoAM";
            rdoAM.Size = new Size(44, 19);
            rdoAM.TabIndex = 5;
            rdoAM.TabStop = true;
            rdoAM.Text = "AM";
            rdoAM.UseVisualStyleBackColor = true;
            // 
            // rdoPM
            // 
            rdoPM.AutoSize = true;
            rdoPM.Location = new Point(746, 14);
            rdoPM.Name = "rdoPM";
            rdoPM.Size = new Size(43, 19);
            rdoPM.TabIndex = 5;
            rdoPM.TabStop = true;
            rdoPM.Text = "PM";
            rdoPM.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(40, 85);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(450, 349);
            listBox1.TabIndex = 6;
            // 
            // frmPastSections
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 552);
            Controls.Add(listBox1);
            Controls.Add(rdoPM);
            Controls.Add(rdoAM);
            Controls.Add(dtpShiftDate);
            Controls.Add(btnReadSpecificDate);
            Controls.Add(btnReadEntireFile);
            Controls.Add(tvPastServerTables);
            Name = "frmPastSections";
            Text = "frmPastSections";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView tvPastServerTables;
        private Button btnReadEntireFile;
        private Button btnReadSpecificDate;
        private DateTimePicker dtpShiftDate;
        private RadioButton rdoAM;
        private RadioButton rdoPM;
        private ListBox listBox1;
    }
}