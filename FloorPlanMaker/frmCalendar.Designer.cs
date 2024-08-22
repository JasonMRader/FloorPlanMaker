namespace FloorPlanMakerUI
{
    partial class frmCalendar
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
            flowLayoutPanel5 = new FlowLayoutPanel();
            weekViewControl1 = new FloorplanUserControlLibrary.WeekViewControl();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            weekViewControl2 = new FloorplanUserControlLibrary.WeekViewControl();
            flowLayoutPanel3 = new FlowLayoutPanel();
            weekViewControl3 = new FloorplanUserControlLibrary.WeekViewControl();
            flowLayoutPanel4 = new FlowLayoutPanel();
            weekViewControl4 = new FloorplanUserControlLibrary.WeekViewControl();
            flowLayoutPanel6 = new FlowLayoutPanel();
            weekViewControl5 = new FloorplanUserControlLibrary.WeekViewControl();
            cboMonthSelect = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(weekViewControl1);
            flowLayoutPanel5.Location = new Point(0, 0);
            flowLayoutPanel5.Margin = new Padding(0);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(1080, 160);
            flowLayoutPanel5.TabIndex = 0;
            // 
            // weekViewControl1
            // 
            weekViewControl1.Location = new Point(0, 0);
            weekViewControl1.Margin = new Padding(0);
            weekViewControl1.Name = "weekViewControl1";
            weekViewControl1.Size = new Size(1080, 160);
            weekViewControl1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel6);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(22, 116);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1080, 820);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(weekViewControl2);
            flowLayoutPanel2.Location = new Point(0, 165);
            flowLayoutPanel2.Margin = new Padding(0, 5, 0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(1080, 160);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // weekViewControl2
            // 
            weekViewControl2.Location = new Point(0, 0);
            weekViewControl2.Margin = new Padding(0);
            weekViewControl2.Name = "weekViewControl2";
            weekViewControl2.Size = new Size(1080, 160);
            weekViewControl2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(weekViewControl3);
            flowLayoutPanel3.Location = new Point(0, 330);
            flowLayoutPanel3.Margin = new Padding(0, 5, 0, 0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(1080, 160);
            flowLayoutPanel3.TabIndex = 0;
            // 
            // weekViewControl3
            // 
            weekViewControl3.Location = new Point(0, 0);
            weekViewControl3.Margin = new Padding(0);
            weekViewControl3.Name = "weekViewControl3";
            weekViewControl3.Size = new Size(1080, 160);
            weekViewControl3.TabIndex = 1;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(weekViewControl4);
            flowLayoutPanel4.Location = new Point(0, 495);
            flowLayoutPanel4.Margin = new Padding(0, 5, 0, 0);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(1080, 160);
            flowLayoutPanel4.TabIndex = 0;
            // 
            // weekViewControl4
            // 
            weekViewControl4.Location = new Point(0, 0);
            weekViewControl4.Margin = new Padding(0);
            weekViewControl4.Name = "weekViewControl4";
            weekViewControl4.Size = new Size(1080, 160);
            weekViewControl4.TabIndex = 1;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(weekViewControl5);
            flowLayoutPanel6.Location = new Point(0, 660);
            flowLayoutPanel6.Margin = new Padding(0, 5, 0, 0);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(1080, 160);
            flowLayoutPanel6.TabIndex = 0;
            // 
            // weekViewControl5
            // 
            weekViewControl5.Location = new Point(0, 0);
            weekViewControl5.Margin = new Padding(0);
            weekViewControl5.Name = "weekViewControl5";
            weekViewControl5.Size = new Size(1080, 160);
            weekViewControl5.TabIndex = 1;
            // 
            // cboMonthSelect
            // 
            cboMonthSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cboMonthSelect.FormattingEnabled = true;
            cboMonthSelect.Location = new Point(399, 22);
            cboMonthSelect.Name = "cboMonthSelect";
            cboMonthSelect.Size = new Size(315, 29);
            cboMonthSelect.TabIndex = 2;
            cboMonthSelect.SelectedIndexChanged += cboMonthSelect_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ControlLight;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(22, 90);
            label1.Margin = new Padding(0, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(150, 23);
            label1.TabIndex = 3;
            label1.Text = "Monday";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.ControlLight;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(177, 90);
            label2.Margin = new Padding(0, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(150, 23);
            label2.TabIndex = 3;
            label2.Text = "Tuesday";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = SystemColors.ControlLight;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(332, 90);
            label3.Margin = new Padding(0, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(150, 23);
            label3.TabIndex = 3;
            label3.Text = "Wednesday";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.ControlLight;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(487, 90);
            label4.Margin = new Padding(0, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(150, 23);
            label4.TabIndex = 3;
            label4.Text = "Thursday";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = SystemColors.ControlLight;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(642, 90);
            label5.Margin = new Padding(0, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(150, 23);
            label5.TabIndex = 3;
            label5.Text = "Friday";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = SystemColors.ControlLight;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(797, 90);
            label6.Margin = new Padding(0, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(150, 23);
            label6.TabIndex = 3;
            label6.Text = "Saturday";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.BackColor = SystemColors.ControlLight;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(952, 90);
            label7.Margin = new Padding(0, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(150, 23);
            label7.TabIndex = 3;
            label7.Text = "Sunday";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmCalendar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1114, 960);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboMonthSelect);
            Controls.Add(flowLayoutPanel1);
            Name = "frmCalendar";
            Text = "frmCalendar";
            Load += frmCalendar_Load;
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel6;
        private FloorplanUserControlLibrary.WeekViewControl weekViewControl1;
        private FloorplanUserControlLibrary.WeekViewControl weekViewControl2;
        private FloorplanUserControlLibrary.WeekViewControl weekViewControl3;
        private FloorplanUserControlLibrary.WeekViewControl weekViewControl4;
        private FloorplanUserControlLibrary.WeekViewControl weekViewControl5;
        private ComboBox cboMonthSelect;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}