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
            btnViewStatsForm = new Button();
            lblAvg = new Label();
            lblMin = new Label();
            lblMax = new Label();
            flowDiningAreas = new FlowLayoutPanel();
            lblAreaAvg = new Label();
            lblAreaMin = new Label();
            lblAreaMax = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowFilters
            // 
            flowFilters.FlowDirection = FlowDirection.TopDown;
            flowFilters.Location = new Point(3, 370);
            flowFilters.Name = "flowFilters";
            flowFilters.Size = new Size(194, 321);
            flowFilters.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoPM);
            panel1.Controls.Add(rdoAM);
            panel1.Location = new Point(3, 309);
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
            rdoPM.Location = new Point(97, 1);
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
            cbHolidaysExcluded.Location = new Point(3, 340);
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
            lblFilteredShiftCount.Location = new Point(3, 45);
            lblFilteredShiftCount.Name = "lblFilteredShiftCount";
            lblFilteredShiftCount.Size = new Size(194, 23);
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
            btnSaveFilter.Visible = false;
            // 
            // btnViewStatsForm
            // 
            btnViewStatsForm.BackColor = Color.FromArgb(100, 130, 180);
            btnViewStatsForm.FlatAppearance.BorderSize = 0;
            btnViewStatsForm.FlatStyle = FlatStyle.Flat;
            btnViewStatsForm.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnViewStatsForm.ForeColor = Color.White;
            btnViewStatsForm.Image = Properties.Resources.analytics_24px;
            btnViewStatsForm.Location = new Point(159, 3);
            btnViewStatsForm.Name = "btnViewStatsForm";
            btnViewStatsForm.Size = new Size(30, 30);
            btnViewStatsForm.TabIndex = 6;
            btnViewStatsForm.UseVisualStyleBackColor = false;
            btnViewStatsForm.Click += btnViewStatsForm_Click;
            // 
            // lblAvg
            // 
            lblAvg.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblAvg.Location = new Point(3, 74);
            lblAvg.Name = "lblAvg";
            lblAvg.Size = new Size(194, 23);
            lblAvg.TabIndex = 5;
            lblAvg.Text = "Avg";
            lblAvg.TextAlign = ContentAlignment.MiddleLeft;
            lblAvg.Visible = false;
            // 
            // lblMin
            // 
            lblMin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblMin.Location = new Point(3, 101);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(194, 23);
            lblMin.TabIndex = 5;
            lblMin.Text = "Min";
            lblMin.TextAlign = ContentAlignment.MiddleLeft;
            lblMin.Visible = false;
            // 
            // lblMax
            // 
            lblMax.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblMax.Location = new Point(3, 128);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(194, 23);
            lblMax.TabIndex = 5;
            lblMax.Text = "Max";
            lblMax.TextAlign = ContentAlignment.MiddleLeft;
            lblMax.Visible = false;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(3, 174);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(194, 38);
            flowDiningAreas.TabIndex = 7;
            // 
            // lblAreaAvg
            // 
            lblAreaAvg.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblAreaAvg.Location = new Point(3, 215);
            lblAreaAvg.Name = "lblAreaAvg";
            lblAreaAvg.Size = new Size(194, 23);
            lblAreaAvg.TabIndex = 5;
            lblAreaAvg.Text = "Avg";
            lblAreaAvg.TextAlign = ContentAlignment.MiddleLeft;
            lblAreaAvg.Visible = false;
            // 
            // lblAreaMin
            // 
            lblAreaMin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblAreaMin.Location = new Point(3, 242);
            lblAreaMin.Name = "lblAreaMin";
            lblAreaMin.Size = new Size(194, 23);
            lblAreaMin.TabIndex = 5;
            lblAreaMin.Text = "Min";
            lblAreaMin.TextAlign = ContentAlignment.MiddleLeft;
            lblAreaMin.Visible = false;
            // 
            // lblAreaMax
            // 
            lblAreaMax.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblAreaMax.Location = new Point(3, 269);
            lblAreaMax.Name = "lblAreaMax";
            lblAreaMax.Size = new Size(194, 23);
            lblAreaMax.TabIndex = 5;
            lblAreaMax.Text = "Max";
            lblAreaMax.TextAlign = ContentAlignment.MiddleLeft;
            lblAreaMax.Visible = false;
            // 
            // ShiftFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(flowDiningAreas);
            Controls.Add(cbHolidaysExcluded);
            Controls.Add(btnViewStatsForm);
            Controls.Add(btnSaveFilter);
            Controls.Add(btnUpdate);
            Controls.Add(lblAreaMax);
            Controls.Add(lblMax);
            Controls.Add(lblAreaMin);
            Controls.Add(lblAreaAvg);
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
        private Button btnViewStatsForm;
        private Label lblAvg;
        private Label lblMin;
        private Label lblMax;
        private FlowLayoutPanel flowDiningAreas;
        private Label lblAreaAvg;
        private Label lblAreaMin;
        private Label lblAreaMax;
    }
}
