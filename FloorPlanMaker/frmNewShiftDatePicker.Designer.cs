namespace FloorPlanMakerUI
{
    partial class frmNewShiftDatePicker
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
            btnOK = new Button();
            lblIsToday = new Label();
            label2 = new Label();
            lblDate = new Label();
            panel1 = new Panel();
            cbIsAm = new CheckBox();
            btnForwardDay = new Button();
            btnBackDay = new Button();
            btnCancel = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(192, 255, 192);
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.Location = new Point(50, 386);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(569, 52);
            btnOK.TabIndex = 0;
            btnOK.Text = "OK!";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // lblIsToday
            // 
            lblIsToday.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblIsToday.ForeColor = Color.Black;
            lblIsToday.Location = new Point(50, 75);
            lblIsToday.Name = "lblIsToday";
            lblIsToday.Size = new Size(469, 40);
            lblIsToday.TabIndex = 1;
            lblIsToday.Text = "(Today)";
            lblIsToday.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 31);
            label2.Name = "label2";
            label2.Size = new Size(643, 40);
            label2.TabIndex = 1;
            label2.Text = "Create a Shift For:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDate.ForeColor = Color.Black;
            lblDate.Location = new Point(50, 13);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(469, 62);
            lblDate.TabIndex = 1;
            lblDate.Text = "November, 3";
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(178, 87, 46);
            panel1.Controls.Add(cbIsAm);
            panel1.Controls.Add(btnForwardDay);
            panel1.Controls.Add(btnBackDay);
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(lblIsToday);
            panel1.Location = new Point(50, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(569, 218);
            panel1.TabIndex = 2;
            // 
            // cbIsAm
            // 
            cbIsAm.Appearance = Appearance.Button;
            cbIsAm.Checked = true;
            cbIsAm.CheckState = CheckState.Checked;
            cbIsAm.FlatAppearance.BorderSize = 0;
            cbIsAm.FlatStyle = FlatStyle.Flat;
            cbIsAm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAm.Location = new Point(195, 146);
            cbIsAm.Name = "cbIsAm";
            cbIsAm.Size = new Size(179, 40);
            cbIsAm.TabIndex = 2;
            cbIsAm.Text = "AM";
            cbIsAm.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAm.UseVisualStyleBackColor = true;
            cbIsAm.CheckedChanged += cbIsAm_CheckedChanged;
            // 
            // btnForwardDay
            // 
            btnForwardDay.BackColor = Color.FromArgb(255, 128, 128);
            btnForwardDay.FlatAppearance.BorderSize = 0;
            btnForwardDay.FlatStyle = FlatStyle.Flat;
            btnForwardDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnForwardDay.Image = Resource1.forwardArrow;
            btnForwardDay.Location = new Point(525, 13);
            btnForwardDay.Name = "btnForwardDay";
            btnForwardDay.Size = new Size(41, 62);
            btnForwardDay.TabIndex = 0;
            btnForwardDay.UseVisualStyleBackColor = false;
            btnForwardDay.Click += btnForwardDay_Click;
            // 
            // btnBackDay
            // 
            btnBackDay.BackColor = Color.FromArgb(255, 128, 128);
            btnBackDay.FlatAppearance.BorderSize = 0;
            btnBackDay.FlatStyle = FlatStyle.Flat;
            btnBackDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackDay.Image = Resource1.BackArrow;
            btnBackDay.Location = new Point(3, 13);
            btnBackDay.Name = "btnBackDay";
            btnBackDay.Size = new Size(41, 62);
            btnBackDay.TabIndex = 0;
            btnBackDay.UseVisualStyleBackColor = false;
            btnBackDay.Click += btnBackDay_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(255, 128, 128);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.Location = new Point(50, 347);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(569, 33);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmNewShiftDatePicker
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 56, 82);
            CancelButton = btnCancel;
            ClientSize = new Size(667, 484);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmNewShiftDatePicker";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmNewShiftDatePicker";
            Load += frmNewShiftDatePicker_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnOK;
        private Label lblIsToday;
        private Label label2;
        private Label lblDate;
        private Panel panel1;
        private Button btnCancel;
        private Button btnForwardDay;
        private Button btnBackDay;
        private CheckBox cbIsAm;
    }
}