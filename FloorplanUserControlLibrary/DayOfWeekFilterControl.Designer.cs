namespace FloorplanUserControlLibrary
{
    partial class DayOfWeekFilterControl
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
            pnlDaySelect = new Panel();
            cbAll = new CheckBox();
            cbThurs = new CheckBox();
            cbSun = new CheckBox();
            cbWed = new CheckBox();
            cbSat = new CheckBox();
            cbFri = new CheckBox();
            cbTues = new CheckBox();
            cbMon = new CheckBox();
            pnlDaySelect.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonShadow;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(194, 27);
            button1.TabIndex = 0;
            button1.Text = "Filter By Day Of Week";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pnlDaySelect
            // 
            pnlDaySelect.Controls.Add(cbAll);
            pnlDaySelect.Controls.Add(cbThurs);
            pnlDaySelect.Controls.Add(cbSun);
            pnlDaySelect.Controls.Add(cbWed);
            pnlDaySelect.Controls.Add(cbSat);
            pnlDaySelect.Controls.Add(cbFri);
            pnlDaySelect.Controls.Add(cbTues);
            pnlDaySelect.Controls.Add(cbMon);
            pnlDaySelect.Location = new Point(0, 30);
            pnlDaySelect.Margin = new Padding(0);
            pnlDaySelect.Name = "pnlDaySelect";
            pnlDaySelect.Size = new Size(194, 63);
            pnlDaySelect.TabIndex = 1;
            // 
            // cbAll
            // 
            cbAll.Appearance = Appearance.Button;
            cbAll.BackColor = SystemColors.ButtonShadow;
            cbAll.Checked = true;
            cbAll.CheckState = CheckState.Checked;
            cbAll.FlatAppearance.BorderSize = 0;
            cbAll.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbAll.FlatStyle = FlatStyle.Flat;
            cbAll.Location = new Point(145, 31);
            cbAll.Margin = new Padding(0);
            cbAll.Name = "cbAll";
            cbAll.Size = new Size(48, 30);
            cbAll.TabIndex = 0;
            cbAll.Text = "All";
            cbAll.TextAlign = ContentAlignment.MiddleCenter;
            cbAll.UseVisualStyleBackColor = false;
            cbAll.CheckedChanged += cbAll_CheckedChanged;
            // 
            // cbThurs
            // 
            cbThurs.Appearance = Appearance.Button;
            cbThurs.BackColor = SystemColors.ButtonShadow;
            cbThurs.Checked = true;
            cbThurs.CheckState = CheckState.Checked;
            cbThurs.FlatAppearance.BorderSize = 0;
            cbThurs.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbThurs.FlatStyle = FlatStyle.Flat;
            cbThurs.Location = new Point(145, 0);
            cbThurs.Margin = new Padding(0);
            cbThurs.Name = "cbThurs";
            cbThurs.Size = new Size(48, 30);
            cbThurs.TabIndex = 0;
            cbThurs.Text = "Thu";
            cbThurs.TextAlign = ContentAlignment.MiddleCenter;
            cbThurs.UseVisualStyleBackColor = false;
            cbThurs.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbThurs.Click += cbDayOfWeek_Clicked;
            // 
            // cbSun
            // 
            cbSun.Appearance = Appearance.Button;
            cbSun.BackColor = SystemColors.ButtonShadow;
            cbSun.Checked = true;
            cbSun.CheckState = CheckState.Checked;
            cbSun.FlatAppearance.BorderSize = 0;
            cbSun.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbSun.FlatStyle = FlatStyle.Flat;
            cbSun.Location = new Point(96, 31);
            cbSun.Margin = new Padding(0);
            cbSun.Name = "cbSun";
            cbSun.Size = new Size(48, 30);
            cbSun.TabIndex = 0;
            cbSun.Text = "Sun";
            cbSun.TextAlign = ContentAlignment.MiddleCenter;
            cbSun.UseVisualStyleBackColor = false;
            cbSun.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbSun.Click += cbDayOfWeek_Clicked;
            // 
            // cbWed
            // 
            cbWed.Appearance = Appearance.Button;
            cbWed.BackColor = SystemColors.ButtonShadow;
            cbWed.Checked = true;
            cbWed.CheckState = CheckState.Checked;
            cbWed.FlatAppearance.BorderSize = 0;
            cbWed.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbWed.FlatStyle = FlatStyle.Flat;
            cbWed.Location = new Point(96, 0);
            cbWed.Margin = new Padding(0);
            cbWed.Name = "cbWed";
            cbWed.Size = new Size(48, 30);
            cbWed.TabIndex = 0;
            cbWed.Text = "Wed";
            cbWed.TextAlign = ContentAlignment.MiddleCenter;
            cbWed.UseVisualStyleBackColor = false;
            cbWed.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbWed.Click += cbDayOfWeek_Clicked;
            // 
            // cbSat
            // 
            cbSat.Appearance = Appearance.Button;
            cbSat.BackColor = SystemColors.ButtonShadow;
            cbSat.Checked = true;
            cbSat.CheckState = CheckState.Checked;
            cbSat.FlatAppearance.BorderSize = 0;
            cbSat.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbSat.FlatStyle = FlatStyle.Flat;
            cbSat.Location = new Point(48, 31);
            cbSat.Margin = new Padding(0);
            cbSat.Name = "cbSat";
            cbSat.Size = new Size(47, 30);
            cbSat.TabIndex = 0;
            cbSat.Text = "Sat";
            cbSat.TextAlign = ContentAlignment.MiddleCenter;
            cbSat.UseVisualStyleBackColor = false;
            cbSat.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbSat.Click += cbDayOfWeek_Clicked;
            // 
            // cbFri
            // 
            cbFri.Appearance = Appearance.Button;
            cbFri.BackColor = SystemColors.ButtonShadow;
            cbFri.Checked = true;
            cbFri.CheckState = CheckState.Checked;
            cbFri.FlatAppearance.BorderSize = 0;
            cbFri.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbFri.FlatStyle = FlatStyle.Flat;
            cbFri.Location = new Point(0, 31);
            cbFri.Margin = new Padding(0);
            cbFri.Name = "cbFri";
            cbFri.Size = new Size(47, 30);
            cbFri.TabIndex = 0;
            cbFri.Text = "Fri";
            cbFri.TextAlign = ContentAlignment.MiddleCenter;
            cbFri.UseVisualStyleBackColor = false;
            cbFri.Click += cbDayOfWeek_Clicked;
            cbFri.CheckedChanged += cbDayOfWeek_CheckedChanged;
            
            // 
            // cbTues
            // 
            cbTues.Appearance = Appearance.Button;
            cbTues.BackColor = SystemColors.ButtonShadow;
            cbTues.Checked = true;
            cbTues.CheckState = CheckState.Checked;
            cbTues.FlatAppearance.BorderSize = 0;
            cbTues.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbTues.FlatStyle = FlatStyle.Flat;
            cbTues.Location = new Point(48, 0);
            cbTues.Margin = new Padding(0);
            cbTues.Name = "cbTues";
            cbTues.Size = new Size(47, 30);
            cbTues.TabIndex = 0;
            cbTues.Text = "Tue";
            cbTues.TextAlign = ContentAlignment.MiddleCenter;
            cbTues.UseVisualStyleBackColor = false;
            cbTues.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbTues.Click += cbDayOfWeek_Clicked;
            // 
            // cbMon
            // 
            cbMon.Appearance = Appearance.Button;
            cbMon.BackColor = SystemColors.ButtonShadow;
            cbMon.Checked = true;
            cbMon.CheckState = CheckState.Checked;
            cbMon.FlatAppearance.BorderSize = 0;
            cbMon.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbMon.FlatStyle = FlatStyle.Flat;
            cbMon.Location = new Point(0, 0);
            cbMon.Margin = new Padding(0);
            cbMon.Name = "cbMon";
            cbMon.Size = new Size(47, 30);
            cbMon.TabIndex = 0;
            cbMon.Text = "Mon";
            cbMon.TextAlign = ContentAlignment.MiddleCenter;
            cbMon.UseVisualStyleBackColor = false;
            cbMon.CheckedChanged += cbDayOfWeek_CheckedChanged;
            cbMon.Click += cbDayOfWeek_Clicked;
            // 
            // DayOfWeekFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(pnlDaySelect);
            Controls.Add(button1);
            Name = "DayOfWeekFilterControl";
            Size = new Size(194, 93);
            pnlDaySelect.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel pnlDaySelect;
        private CheckBox cbMon;
        private CheckBox cbAll;
        private CheckBox cbThurs;
        private CheckBox cbSun;
        private CheckBox cbWed;
        private CheckBox cbSat;
        private CheckBox cbFri;
        private CheckBox cbTues;
    }
}
