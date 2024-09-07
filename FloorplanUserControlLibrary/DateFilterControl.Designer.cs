namespace FloorplanUserControlLibrary
{
    partial class DateFilterControl
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
            button1 = new Button();
            flowRangeSelection = new FlowLayoutPanel();
            cbCustom = new CheckBox();
            rdoLast30 = new RadioButton();
            rdoLast90 = new RadioButton();
            rdoLast365 = new RadioButton();
            rdoAllRecords = new RadioButton();
            flowRangeSelection.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(100, 130, 180);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(194, 27);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // flowRangeSelection
            // 
            flowRangeSelection.Controls.Add(cbCustom);
            flowRangeSelection.Controls.Add(rdoLast30);
            flowRangeSelection.Controls.Add(rdoLast90);
            flowRangeSelection.Controls.Add(rdoLast365);
            flowRangeSelection.Controls.Add(rdoAllRecords);
            flowRangeSelection.FlowDirection = FlowDirection.TopDown;
            flowRangeSelection.Location = new Point(0, 30);
            flowRangeSelection.Margin = new Padding(0);
            flowRangeSelection.Name = "flowRangeSelection";
            flowRangeSelection.Size = new Size(194, 135);
            flowRangeSelection.TabIndex = 1;
            // 
            // cbCustom
            // 
            cbCustom.Appearance = Appearance.Button;
            cbCustom.BackColor = SystemColors.ButtonShadow;
            cbCustom.FlatAppearance.BorderSize = 0;
            cbCustom.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbCustom.FlatStyle = FlatStyle.Flat;
            cbCustom.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            cbCustom.Location = new Point(0, 0);
            cbCustom.Margin = new Padding(0);
            cbCustom.Name = "cbCustom";
            cbCustom.Size = new Size(194, 27);
            cbCustom.TabIndex = 2;
            cbCustom.Text = "Custom";
            cbCustom.TextAlign = ContentAlignment.MiddleCenter;
            cbCustom.UseVisualStyleBackColor = false;
            cbCustom.Click += cbCustom_CheckedChanged;
            // 
            // rdoLast30
            // 
            rdoLast30.Appearance = Appearance.Button;
            rdoLast30.BackColor = SystemColors.ButtonShadow;
            rdoLast30.FlatAppearance.BorderSize = 0;
            rdoLast30.FlatStyle = FlatStyle.Flat;
            rdoLast30.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            rdoLast30.Location = new Point(0, 27);
            rdoLast30.Margin = new Padding(0);
            rdoLast30.Name = "rdoLast30";
            rdoLast30.Size = new Size(194, 27);
            rdoLast30.TabIndex = 1;
            rdoLast30.Text = "Last 30 Days";
            rdoLast30.TextAlign = ContentAlignment.MiddleCenter;
            rdoLast30.UseVisualStyleBackColor = false;
            rdoLast30.Click += rdoTimeFrame_Clicked;
            // 
            // rdoLast90
            // 
            rdoLast90.Appearance = Appearance.Button;
            rdoLast90.BackColor = Color.FromArgb(100, 130, 180);
            rdoLast90.Checked = true;
            rdoLast90.FlatAppearance.BorderSize = 0;
            rdoLast90.FlatStyle = FlatStyle.Flat;
            rdoLast90.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            rdoLast90.Location = new Point(0, 54);
            rdoLast90.Margin = new Padding(0);
            rdoLast90.Name = "rdoLast90";
            rdoLast90.Size = new Size(194, 27);
            rdoLast90.TabIndex = 1;
            rdoLast90.TabStop = true;
            rdoLast90.Text = "Last 90 Days";
            rdoLast90.TextAlign = ContentAlignment.MiddleCenter;
            rdoLast90.UseVisualStyleBackColor = false;
            rdoLast90.Click += rdoTimeFrame_Clicked;
            // 
            // rdoLast365
            // 
            rdoLast365.Appearance = Appearance.Button;
            rdoLast365.BackColor = SystemColors.ButtonShadow;
            rdoLast365.FlatAppearance.BorderSize = 0;
            rdoLast365.FlatStyle = FlatStyle.Flat;
            rdoLast365.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            rdoLast365.Location = new Point(0, 81);
            rdoLast365.Margin = new Padding(0);
            rdoLast365.Name = "rdoLast365";
            rdoLast365.Size = new Size(194, 27);
            rdoLast365.TabIndex = 1;
            rdoLast365.Text = "Last 365 Days";
            rdoLast365.TextAlign = ContentAlignment.MiddleCenter;
            rdoLast365.UseVisualStyleBackColor = false;
            rdoLast365.Click += rdoTimeFrame_Clicked;
            // 
            // rdoAllRecords
            // 
            rdoAllRecords.Appearance = Appearance.Button;
            rdoAllRecords.BackColor = SystemColors.ButtonShadow;
            rdoAllRecords.FlatAppearance.BorderSize = 0;
            rdoAllRecords.FlatStyle = FlatStyle.Flat;
            rdoAllRecords.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            rdoAllRecords.Location = new Point(0, 108);
            rdoAllRecords.Margin = new Padding(0);
            rdoAllRecords.Name = "rdoAllRecords";
            rdoAllRecords.Size = new Size(194, 27);
            rdoAllRecords.TabIndex = 1;
            rdoAllRecords.Text = "All Records";
            rdoAllRecords.TextAlign = ContentAlignment.MiddleCenter;
            rdoAllRecords.UseVisualStyleBackColor = false;
            rdoAllRecords.Click += rdoTimeFrame_Clicked;
            // 
            // DateFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(flowRangeSelection);
            Controls.Add(button1);
            Name = "DateFilterControl";
            Size = new Size(194, 165);
            Load += DateFilterControl_Load;
            flowRangeSelection.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private FlowLayoutPanel flowRangeSelection;
        private RadioButton rdoLast30;
        private RadioButton rdoLast90;
        private RadioButton rdoLast365;
        private RadioButton rdoAllRecords;
        private CheckBox cbCustom;
    }
}
