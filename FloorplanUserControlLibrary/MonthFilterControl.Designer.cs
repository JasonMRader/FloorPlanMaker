namespace FloorplanUserControlLibrary
{
    partial class MonthFilterControl
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
            cbDec = new CheckBox();
            cbNov = new CheckBox();
            cbOct = new CheckBox();
            cbSep = new CheckBox();
            pnlMonthSelect = new Panel();
            cbAug = new CheckBox();
            cbApr = new CheckBox();
            cbJul = new CheckBox();
            cbMar = new CheckBox();
            cbJun = new CheckBox();
            cbMay = new CheckBox();
            cbFeb = new CheckBox();
            cbJan = new CheckBox();
            pnlMonthSelect.SuspendLayout();
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
            button1.TabIndex = 1;
            button1.Text = "Filter By Month";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // cbDec
            // 
            cbDec.Appearance = Appearance.Button;
            cbDec.BackColor = SystemColors.ButtonShadow;
            cbDec.Checked = true;
            cbDec.CheckState = CheckState.Checked;
            cbDec.FlatAppearance.BorderSize = 0;
            cbDec.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbDec.FlatStyle = FlatStyle.Flat;
            cbDec.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbDec.ForeColor = Color.White;
            cbDec.Location = new Point(145, 62);
            cbDec.Margin = new Padding(0);
            cbDec.Name = "cbDec";
            cbDec.Size = new Size(49, 30);
            cbDec.TabIndex = 2;
            cbDec.Text = "Dec";
            cbDec.TextAlign = ContentAlignment.MiddleCenter;
            cbDec.UseVisualStyleBackColor = false;
            cbDec.Click += cbMonth_Clicked;
            // 
            // cbNov
            // 
            cbNov.Appearance = Appearance.Button;
            cbNov.BackColor = SystemColors.ButtonShadow;
            cbNov.Checked = true;
            cbNov.CheckState = CheckState.Checked;
            cbNov.FlatAppearance.BorderSize = 0;
            cbNov.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbNov.FlatStyle = FlatStyle.Flat;
            cbNov.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbNov.ForeColor = Color.White;
            cbNov.Location = new Point(96, 62);
            cbNov.Margin = new Padding(0);
            cbNov.Name = "cbNov";
            cbNov.Size = new Size(48, 30);
            cbNov.TabIndex = 3;
            cbNov.Text = "Nov";
            cbNov.TextAlign = ContentAlignment.MiddleCenter;
            cbNov.UseVisualStyleBackColor = false;
            cbNov.Click += cbMonth_Clicked;
            // 
            // cbOct
            // 
            cbOct.Appearance = Appearance.Button;
            cbOct.BackColor = SystemColors.ButtonShadow;
            cbOct.Checked = true;
            cbOct.CheckState = CheckState.Checked;
            cbOct.FlatAppearance.BorderSize = 0;
            cbOct.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbOct.FlatStyle = FlatStyle.Flat;
            cbOct.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbOct.ForeColor = Color.White;
            cbOct.Location = new Point(48, 62);
            cbOct.Margin = new Padding(0);
            cbOct.Name = "cbOct";
            cbOct.Size = new Size(47, 30);
            cbOct.TabIndex = 4;
            cbOct.Text = "Oct";
            cbOct.TextAlign = ContentAlignment.MiddleCenter;
            cbOct.UseVisualStyleBackColor = false;
            cbOct.Click += cbMonth_Clicked;
            // 
            // cbSep
            // 
            cbSep.Appearance = Appearance.Button;
            cbSep.BackColor = SystemColors.ButtonShadow;
            cbSep.Checked = true;
            cbSep.CheckState = CheckState.Checked;
            cbSep.FlatAppearance.BorderSize = 0;
            cbSep.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbSep.FlatStyle = FlatStyle.Flat;
            cbSep.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbSep.ForeColor = Color.White;
            cbSep.Location = new Point(0, 62);
            cbSep.Margin = new Padding(0);
            cbSep.Name = "cbSep";
            cbSep.Size = new Size(47, 30);
            cbSep.TabIndex = 5;
            cbSep.Text = "Sep";
            cbSep.TextAlign = ContentAlignment.MiddleCenter;
            cbSep.UseVisualStyleBackColor = false;
            cbSep.Click += cbMonth_Clicked;
            // 
            // pnlMonthSelect
            // 
            pnlMonthSelect.Controls.Add(cbAug);
            pnlMonthSelect.Controls.Add(cbDec);
            pnlMonthSelect.Controls.Add(cbApr);
            pnlMonthSelect.Controls.Add(cbNov);
            pnlMonthSelect.Controls.Add(cbOct);
            pnlMonthSelect.Controls.Add(cbJul);
            pnlMonthSelect.Controls.Add(cbSep);
            pnlMonthSelect.Controls.Add(cbMar);
            pnlMonthSelect.Controls.Add(cbJun);
            pnlMonthSelect.Controls.Add(cbMay);
            pnlMonthSelect.Controls.Add(cbFeb);
            pnlMonthSelect.Controls.Add(cbJan);
            pnlMonthSelect.Location = new Point(0, 30);
            pnlMonthSelect.Margin = new Padding(0);
            pnlMonthSelect.Name = "pnlMonthSelect";
            pnlMonthSelect.Size = new Size(194, 94);
            pnlMonthSelect.TabIndex = 6;
            // 
            // cbAug
            // 
            cbAug.Appearance = Appearance.Button;
            cbAug.BackColor = SystemColors.ButtonShadow;
            cbAug.Checked = true;
            cbAug.CheckState = CheckState.Checked;
            cbAug.FlatAppearance.BorderSize = 0;
            cbAug.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbAug.FlatStyle = FlatStyle.Flat;
            cbAug.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbAug.ForeColor = Color.White;
            cbAug.Location = new Point(145, 31);
            cbAug.Margin = new Padding(0);
            cbAug.Name = "cbAug";
            cbAug.Size = new Size(48, 30);
            cbAug.TabIndex = 0;
            cbAug.Text = "Aug";
            cbAug.TextAlign = ContentAlignment.MiddleCenter;
            cbAug.UseVisualStyleBackColor = false;
            cbAug.Click += cbMonth_Clicked;
            // 
            // cbApr
            // 
            cbApr.Appearance = Appearance.Button;
            cbApr.BackColor = SystemColors.ButtonShadow;
            cbApr.Checked = true;
            cbApr.CheckState = CheckState.Checked;
            cbApr.FlatAppearance.BorderSize = 0;
            cbApr.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbApr.FlatStyle = FlatStyle.Flat;
            cbApr.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbApr.ForeColor = Color.White;
            cbApr.Location = new Point(145, 0);
            cbApr.Margin = new Padding(0);
            cbApr.Name = "cbApr";
            cbApr.Size = new Size(48, 30);
            cbApr.TabIndex = 0;
            cbApr.Text = "Apr";
            cbApr.TextAlign = ContentAlignment.MiddleCenter;
            cbApr.UseVisualStyleBackColor = false;
            cbApr.Click += cbMonth_Clicked;
            // 
            // cbJul
            // 
            cbJul.Appearance = Appearance.Button;
            cbJul.BackColor = SystemColors.ButtonShadow;
            cbJul.Checked = true;
            cbJul.CheckState = CheckState.Checked;
            cbJul.FlatAppearance.BorderSize = 0;
            cbJul.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbJul.FlatStyle = FlatStyle.Flat;
            cbJul.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbJul.ForeColor = Color.White;
            cbJul.Location = new Point(96, 31);
            cbJul.Margin = new Padding(0);
            cbJul.Name = "cbJul";
            cbJul.Size = new Size(48, 30);
            cbJul.TabIndex = 0;
            cbJul.Text = "Jul";
            cbJul.TextAlign = ContentAlignment.MiddleCenter;
            cbJul.UseVisualStyleBackColor = false;
            cbJul.Click += cbMonth_Clicked;
            // 
            // cbMar
            // 
            cbMar.Appearance = Appearance.Button;
            cbMar.BackColor = SystemColors.ButtonShadow;
            cbMar.Checked = true;
            cbMar.CheckState = CheckState.Checked;
            cbMar.FlatAppearance.BorderSize = 0;
            cbMar.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbMar.FlatStyle = FlatStyle.Flat;
            cbMar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbMar.ForeColor = Color.White;
            cbMar.Location = new Point(96, 0);
            cbMar.Margin = new Padding(0);
            cbMar.Name = "cbMar";
            cbMar.Size = new Size(48, 30);
            cbMar.TabIndex = 0;
            cbMar.Text = "Mar";
            cbMar.TextAlign = ContentAlignment.MiddleCenter;
            cbMar.UseVisualStyleBackColor = false;
            cbMar.Click += cbMonth_Clicked;
            // 
            // cbJun
            // 
            cbJun.Appearance = Appearance.Button;
            cbJun.BackColor = SystemColors.ButtonShadow;
            cbJun.Checked = true;
            cbJun.CheckState = CheckState.Checked;
            cbJun.FlatAppearance.BorderSize = 0;
            cbJun.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbJun.FlatStyle = FlatStyle.Flat;
            cbJun.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbJun.ForeColor = Color.White;
            cbJun.Location = new Point(48, 31);
            cbJun.Margin = new Padding(0);
            cbJun.Name = "cbJun";
            cbJun.Size = new Size(47, 30);
            cbJun.TabIndex = 0;
            cbJun.Text = "Jun";
            cbJun.TextAlign = ContentAlignment.MiddleCenter;
            cbJun.UseVisualStyleBackColor = false;
            cbJun.Click += cbMonth_Clicked;
            // 
            // cbMay
            // 
            cbMay.Appearance = Appearance.Button;
            cbMay.BackColor = SystemColors.ButtonShadow;
            cbMay.Checked = true;
            cbMay.CheckState = CheckState.Checked;
            cbMay.FlatAppearance.BorderSize = 0;
            cbMay.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbMay.FlatStyle = FlatStyle.Flat;
            cbMay.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbMay.ForeColor = Color.White;
            cbMay.Location = new Point(0, 31);
            cbMay.Margin = new Padding(0);
            cbMay.Name = "cbMay";
            cbMay.Size = new Size(47, 30);
            cbMay.TabIndex = 0;
            cbMay.Text = "May";
            cbMay.TextAlign = ContentAlignment.MiddleCenter;
            cbMay.UseVisualStyleBackColor = false;
            cbMay.Click += cbMonth_Clicked;
            // 
            // cbFeb
            // 
            cbFeb.Appearance = Appearance.Button;
            cbFeb.BackColor = SystemColors.ButtonShadow;
            cbFeb.Checked = true;
            cbFeb.CheckState = CheckState.Checked;
            cbFeb.FlatAppearance.BorderSize = 0;
            cbFeb.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbFeb.FlatStyle = FlatStyle.Flat;
            cbFeb.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbFeb.ForeColor = Color.White;
            cbFeb.Location = new Point(48, 0);
            cbFeb.Margin = new Padding(0);
            cbFeb.Name = "cbFeb";
            cbFeb.Size = new Size(47, 30);
            cbFeb.TabIndex = 0;
            cbFeb.Text = "Feb";
            cbFeb.TextAlign = ContentAlignment.MiddleCenter;
            cbFeb.UseVisualStyleBackColor = false;
            cbFeb.Click += cbMonth_Clicked;
            // 
            // cbJan
            // 
            cbJan.Appearance = Appearance.Button;
            cbJan.BackColor = SystemColors.ButtonShadow;
            cbJan.Checked = true;
            cbJan.CheckState = CheckState.Checked;
            cbJan.FlatAppearance.BorderSize = 0;
            cbJan.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            cbJan.FlatStyle = FlatStyle.Flat;
            cbJan.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbJan.ForeColor = Color.White;
            cbJan.Location = new Point(0, 0);
            cbJan.Margin = new Padding(0);
            cbJan.Name = "cbJan";
            cbJan.Size = new Size(47, 30);
            cbJan.TabIndex = 0;
            cbJan.Text = "Jan";
            cbJan.TextAlign = ContentAlignment.MiddleCenter;
            cbJan.UseVisualStyleBackColor = false;
            cbJan.Click += cbMonth_Clicked;
            // 
            // MonthFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(pnlMonthSelect);
            Controls.Add(button1);
            Name = "MonthFilterControl";
            Size = new Size(194, 124);
            pnlMonthSelect.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private CheckBox cbDec;
        private CheckBox cbNov;
        private CheckBox cbOct;
        private CheckBox cbSep;
        //private CheckBox checkBox1;
        //private CheckBox checkBox2;
        //private CheckBox checkBox3;
        //private CheckBox checkBox4;
        //private CheckBox checkBox5;
        //private CheckBox checkBox6;
        //private CheckBox checkBox7;
        //private CheckBox checkBox8;
        private Panel pnlMonthSelect;
        private CheckBox cbAug;
        private CheckBox cbJul;
        private CheckBox cbJun;
        private CheckBox cbMay;
        private CheckBox cbApr;
        private CheckBox cbMar;
        private CheckBox cbFeb;
        private CheckBox cbJan;
    }
}
