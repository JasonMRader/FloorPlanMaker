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
            panel2 = new Panel();
            cbHolidaysExcluded = new CheckBox();
            lblFilteredShiftCount = new Label();
            btnUpdate = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // flowFilters
            // 
            flowFilters.FlowDirection = FlowDirection.TopDown;
            flowFilters.Location = new Point(3, 197);
            flowFilters.Name = "flowFilters";
            flowFilters.Size = new Size(194, 494);
            flowFilters.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoPM);
            panel1.Controls.Add(rdoAM);
            panel1.Location = new Point(3, 158);
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
            rdoPM.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
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
            rdoAM.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
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
            // panel2
            // 
            panel2.Controls.Add(cbHolidaysExcluded);
            panel2.Location = new Point(3, 124);
            panel2.Name = "panel2";
            panel2.Size = new Size(194, 28);
            panel2.TabIndex = 4;
            // 
            // cbHolidaysExcluded
            // 
            cbHolidaysExcluded.Appearance = Appearance.Button;
            cbHolidaysExcluded.BackColor = SystemColors.ButtonShadow;
            cbHolidaysExcluded.FlatAppearance.BorderSize = 0;
            cbHolidaysExcluded.FlatStyle = FlatStyle.Flat;
            cbHolidaysExcluded.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbHolidaysExcluded.Location = new Point(0, 0);
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
            lblFilteredShiftCount.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblFilteredShiftCount.Location = new Point(3, 10);
            lblFilteredShiftCount.Name = "lblFilteredShiftCount";
            lblFilteredShiftCount.Size = new Size(194, 23);
            lblFilteredShiftCount.TabIndex = 5;
            lblFilteredShiftCount.Text = "Total Filtered Shifts:";
            lblFilteredShiftCount.TextAlign = ContentAlignment.MiddleCenter;
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
            // 
            // ShiftFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnUpdate);
            Controls.Add(lblFilteredShiftCount);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(flowFilters);
            Name = "ShiftFilterControl";
            Size = new Size(200, 737);
            Load += ShiftFilterControl_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private FlowLayoutPanel flowFilters;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private CheckBox cbHolidaysExcluded;
        private RadioButton rdoPM;
        private RadioButton rdoAM;
        private Label lblFilteredShiftCount;
        private Button btnUpdate;
    }
}
