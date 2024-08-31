namespace FloorplanUserControlLibrary
{
    partial class ShiftFilterControl
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dateTimePicker1 = new DateTimePicker();
            flowFilters = new FlowLayoutPanel();
            label1 = new Label();
            label4 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            panel2 = new Panel();
            checkBox4 = new CheckBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(3, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(194, 23);
            dateTimePicker1.TabIndex = 0;
            // 
            // flowFilters
            // 
            flowFilters.FlowDirection = FlowDirection.TopDown;
            flowFilters.Location = new Point(3, 226);
            flowFilters.Name = "flowFilters";
            flowFilters.Size = new Size(194, 508);
            flowFilters.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 29);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 3;
            label1.Text = "Dates:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 101);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 3;
            label4.Text = "Weekdays:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 125);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 3;
            label5.Text = "Months:";
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Location = new Point(3, 177);
            panel1.Name = "panel1";
            panel1.Size = new Size(194, 28);
            panel1.TabIndex = 4;
            // 
            // checkBox1
            // 
            checkBox1.Appearance = Appearance.Button;
            checkBox1.BackColor = SystemColors.ButtonShadow;
            checkBox1.FlatAppearance.BorderSize = 0;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Location = new Point(0, 0);
            checkBox1.Margin = new Padding(0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(97, 27);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "AM";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = false;
            // 
            // checkBox2
            // 
            checkBox2.Appearance = Appearance.Button;
            checkBox2.BackColor = SystemColors.ButtonShadow;
            checkBox2.FlatAppearance.BorderSize = 0;
            checkBox2.FlatStyle = FlatStyle.Flat;
            checkBox2.Location = new Point(97, 0);
            checkBox2.Margin = new Padding(0);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(97, 27);
            checkBox2.TabIndex = 0;
            checkBox2.Text = "PM";
            checkBox2.TextAlign = ContentAlignment.MiddleCenter;
            checkBox2.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBox4);
            panel2.Location = new Point(6, 47);
            panel2.Name = "panel2";
            panel2.Size = new Size(194, 28);
            panel2.TabIndex = 4;
            // 
            // checkBox4
            // 
            checkBox4.Appearance = Appearance.Button;
            checkBox4.BackColor = SystemColors.ButtonShadow;
            checkBox4.FlatAppearance.BorderSize = 0;
            checkBox4.FlatStyle = FlatStyle.Flat;
            checkBox4.Location = new Point(0, 0);
            checkBox4.Margin = new Padding(0);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(191, 27);
            checkBox4.TabIndex = 0;
            checkBox4.Text = "Holidays Included";
            checkBox4.TextAlign = ContentAlignment.MiddleCenter;
            checkBox4.UseVisualStyleBackColor = false;
            // 
            // ShiftFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(flowFilters);
            Controls.Add(dateTimePicker1);
            Name = "ShiftFilterControl";
            Size = new Size(200, 737);
            Load += ShiftFilterControl_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private FlowLayoutPanel flowFilters;
        private Label label1;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Panel panel2;
        private CheckBox checkBox4;
    }
}
