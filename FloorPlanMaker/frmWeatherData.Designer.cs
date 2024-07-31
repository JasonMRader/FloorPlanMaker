namespace FloorPlanMakerUI
{
    partial class frmWeatherData
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
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            lbMissingDates = new ListBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(68, 24);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 0;
            dtpStart.ValueChanged += dtpStart_ValueChanged;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(295, 24);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 0;
            dtpEnd.ValueChanged += dtpEnd_ValueChanged;
            // 
            // lbMissingDates
            // 
            lbMissingDates.FormattingEnabled = true;
            lbMissingDates.ItemHeight = 15;
            lbMissingDates.Location = new Point(553, 30);
            lbMissingDates.Name = "lbMissingDates";
            lbMissingDates.Size = new Size(214, 169);
            lbMissingDates.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(68, 110);
            button1.Name = "button1";
            button1.Size = new Size(200, 34);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmWeatherData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 580);
            Controls.Add(button1);
            Controls.Add(lbMissingDates);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Name = "frmWeatherData";
            Text = "frmWeatherData";
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ListBox lbMissingDates;
        private Button button1;
    }
}