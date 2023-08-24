namespace FloorPlanMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            pnlAddTables = new Panel();
            label2 = new Label();
            pnlFloorPlan = new Panel();
            textBox1 = new TextBox();
            cbDesignMode = new CheckBox();
            panel1.SuspendLayout();
            pnlAddTables.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 970);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(54, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 21);
            label1.TabIndex = 0;
            label1.Text = "Assign Sections";
            // 
            // pnlAddTables
            // 
            pnlAddTables.BackColor = SystemColors.ActiveCaption;
            pnlAddTables.Controls.Add(label2);
            pnlAddTables.Dock = DockStyle.Left;
            pnlAddTables.Location = new Point(250, 0);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(250, 970);
            pnlAddTables.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(77, 9);
            label2.Name = "label2";
            label2.Size = new Size(93, 21);
            label2.TabIndex = 0;
            label2.Text = "Add Tables";
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.FromArgb(255, 244, 232);
            pnlFloorPlan.Location = new Point(540, 77);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(670, 870);
            pnlFloorPlan.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(737, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(303, 35);
            textBox1.TabIndex = 3;
            // 
            // cbDesignMode
            // 
            cbDesignMode.Appearance = Appearance.Button;
            cbDesignMode.FlatStyle = FlatStyle.Flat;
            cbDesignMode.Location = new Point(540, 12);
            cbDesignMode.Name = "cbDesignMode";
            cbDesignMode.Size = new Size(148, 46);
            cbDesignMode.TabIndex = 4;
            cbDesignMode.Text = "Edit DIning Area";
            cbDesignMode.TextAlign = ContentAlignment.MiddleCenter;
            cbDesignMode.UseVisualStyleBackColor = true;
            cbDesignMode.CheckedChanged += cbDesignMode_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(cbDesignMode);
            Controls.Add(textBox1);
            Controls.Add(pnlFloorPlan);
            Controls.Add(pnlAddTables);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel pnlAddTables;
        private Label label2;
        private Panel pnlFloorPlan;
        private TextBox textBox1;
        private CheckBox cbDesignMode;
    }
}