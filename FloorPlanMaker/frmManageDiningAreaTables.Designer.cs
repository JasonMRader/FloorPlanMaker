namespace FloorPlanMakerUI
{
    partial class frmManageDiningAreaTables
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
            label1 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            listBox3 = new ListBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(905, 39);
            label1.TabIndex = 0;
            label1.Text = "DiningAreaNameLabel";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(29, 113);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(138, 589);
            listBox1.TabIndex = 1;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(219, 113);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(152, 589);
            listBox2.TabIndex = 1;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(417, 113);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(152, 589);
            listBox3.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 79);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 2;
            label2.Text = "Tables Currently In Area";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(219, 79);
            label3.Name = "label3";
            label3.Size = new Size(129, 15);
            label3.TabIndex = 2;
            label3.Text = "Tables Counted in Stats";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(417, 79);
            label4.Name = "label4";
            label4.Size = new Size(149, 15);
            label4.TabIndex = 2;
            label4.Text = "Tables Excluded From Stats";
            // 
            // frmManageDiningAreaTables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(905, 720);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(listBox3);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Name = "frmManageDiningAreaTables";
            Text = "frmManageDiningAreaTables";
            Load += frmManageDiningAreaTables_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}