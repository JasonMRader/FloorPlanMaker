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
            flowFilters = new FlowLayoutPanel();
            panel1 = new Panel();
            rdoPM = new RadioButton();
            rdoAM = new RadioButton();
            cbHolidaysExcluded = new CheckBox();
            lblFilteredShiftCount = new Label();
            btnUpdate = new Button();
            btnSaveFilter = new Button();
            button2 = new Button();
            cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            lblAvg = new Label();
            lblMin = new Label();
            lblMax = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowFilters
            // 
            flowFilters.FlowDirection = FlowDirection.TopDown;
            flowFilters.Location = new Point(3, 342);
            flowFilters.Name = "flowFilters";
            flowFilters.Size = new Size(194, 349);
            flowFilters.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoPM);
            panel1.Controls.Add(rdoAM);
            panel1.Location = new Point(3, 282);
            panel1.Name = "panel1";
            panel1.Size = new Size(194, 28);
            panel1.TabIndex = 4;
            // 
            // rdoPM
            // 
            rdoPM.Appearance = Appearance.Button;
            rdoPM.BackColor = SystemColors.ButtonShadow;
            rdoPM.FlatAppearance.BorderSize = 0;
            rdoPM.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoPM.FlatStyle = FlatStyle.Flat;
            rdoPM.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            rdoPM.Location = new Point(97, 0);
            rdoPM.Margin = new Padding(0);
            rdoPM.Name = "rdoPM";
            rdoPM.Size = new Size(97, 27);
            rdoPM.TabIndex = 1;
            rdoPM.TabStop = true;
            rdoPM.Text = "PM";
            rdoPM.TextAlign = ContentAlignment.MiddleCenter;
            rdoPM.UseVisualStyleBackColor = false;
            // 
            // rdoAM
            // 
            rdoAM.Appearance = Appearance.Button;
            rdoAM.BackColor = SystemColors.ButtonShadow;
            rdoAM.FlatAppearance.BorderSize = 0;
            rdoAM.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoAM.FlatStyle = FlatStyle.Flat;
            rdoAM.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            rdoAM.Location = new Point(0, 0);
            rdoAM.Margin = new Padding(0);
            rdoAM.Name = "rdoAM";
            rdoAM.Size = new Size(97, 27);
            rdoAM.TabIndex = 1;
            rdoAM.TabStop = true;
            rdoAM.Text = "AM";
            rdoAM.TextAlign = ContentAlignment.MiddleCenter;
            rdoAM.UseVisualStyleBackColor = false;
            rdoAM.CheckedChanged += rdoAM_CheckedChanged;
            // 
            // cbHolidaysExcluded
            // 
            cbHolidaysExcluded.Appearance = Appearance.Button;
            cbHolidaysExcluded.BackColor = SystemColors.ButtonShadow;
            cbHolidaysExcluded.FlatAppearance.BorderSize = 0;
            cbHolidaysExcluded.FlatStyle = FlatStyle.Flat;
            cbHolidaysExcluded.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbHolidaysExcluded.Location = new Point(3, 313);
            cbHolidaysExcluded.Margin = new Padding(0);
            cbHolidaysExcluded.Name = "cbHolidaysExcluded";
            cbHolidaysExcluded.Size = new Size(194, 27);
            cbHolidaysExcluded.TabIndex = 0;
            cbHolidaysExcluded.Text = "Holidays Included";
            cbHolidaysExcluded.TextAlign = ContentAlignment.MiddleCenter;
            cbHolidaysExcluded.UseVisualStyleBackColor = false;
            cbHolidaysExcluded.CheckedChanged += cbHolidaysExcluded_CheckedChanged;
            // 
            // lblFilteredShiftCount
            // 
            lblFilteredShiftCount.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblFilteredShiftCount.Location = new Point(3, 9);
            lblFilteredShiftCount.Name = "lblFilteredShiftCount";
            lblFilteredShiftCount.Size = new Size(114, 23);
            lblFilteredShiftCount.TabIndex = 5;
            lblFilteredShiftCount.Text = "100 Shifts:";
            lblFilteredShiftCount.TextAlign = ContentAlignment.MiddleCenter;
            lblFilteredShiftCount.Visible = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(100, 130, 180);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(3, 697);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(194, 37);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSaveFilter
            // 
            btnSaveFilter.BackColor = SystemColors.ButtonShadow;
            btnSaveFilter.FlatAppearance.BorderSize = 0;
            btnSaveFilter.FlatStyle = FlatStyle.Flat;
            btnSaveFilter.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSaveFilter.ForeColor = Color.White;
            btnSaveFilter.Image = Properties.Resources.ExtraSmallSave;
            btnSaveFilter.Location = new Point(123, 3);
            btnSaveFilter.Name = "btnSaveFilter";
            btnSaveFilter.Size = new Size(30, 30);
            btnSaveFilter.TabIndex = 6;
            btnSaveFilter.UseVisualStyleBackColor = false;
            btnSaveFilter.Click += btnUpdate_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(100, 130, 180);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.White;
            button2.Image = Properties.Resources.analytics_24px;
            button2.Location = new Point(159, 3);
            button2.Name = "button2";
            button2.Size = new Size(30, 30);
            button2.TabIndex = 6;
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnUpdate_Click;
            // 
            // cartesianChart1
            // 
            cartesianChart1.BackColor = Color.LightGray;
            cartesianChart1.Location = new Point(3, 115);
            cartesianChart1.Name = "cartesianChart1";
            cartesianChart1.Size = new Size(194, 161);
            cartesianChart1.TabIndex = 7;
            // 
            // lblAvg
            // 
            lblAvg.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblAvg.Location = new Point(3, 41);
            lblAvg.Name = "lblAvg";
            lblAvg.Size = new Size(194, 23);
            lblAvg.TabIndex = 5;
            lblAvg.Text = "Avg";
            lblAvg.TextAlign = ContentAlignment.MiddleLeft;
            lblAvg.Visible = false;
            // 
            // lblMin
            // 
            lblMin.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblMin.Location = new Point(3, 64);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(194, 23);
            lblMin.TabIndex = 5;
            lblMin.Text = "Min";
            lblMin.TextAlign = ContentAlignment.MiddleLeft;
            lblMin.Visible = false;
            // 
            // lblMax
            // 
            lblMax.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblMax.Location = new Point(3, 87);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(194, 23);
            lblMax.TabIndex = 5;
            lblMax.Text = "Max";
            lblMax.TextAlign = ContentAlignment.MiddleLeft;
            lblMax.Visible = false;
            // 
            // ShiftFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(cartesianChart1);
            Controls.Add(cbHolidaysExcluded);
            Controls.Add(button2);
            Controls.Add(btnSaveFilter);
            Controls.Add(btnUpdate);
            Controls.Add(lblMax);
            Controls.Add(lblMin);
            Controls.Add(lblAvg);
            Controls.Add(lblFilteredShiftCount);
            Controls.Add(panel1);
            Controls.Add(flowFilters);
            Name = "ShiftFilterControl";
            Size = new Size(200, 737);
            Load += ShiftFilterControl_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private FlowLayoutPanel flowFilters;
        private Label label1;
        private Panel panel1;
        private CheckBox cbHolidaysExcluded;
        private RadioButton rdoPM;
        private RadioButton rdoAM;
        private Label lblFilteredShiftCount;
        private Button btnUpdate;
        private Button btnSaveFilter;
        private Button button2;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private Label lblAvg;
        private Label lblMin;
        private Label lblMax;
    }
}
