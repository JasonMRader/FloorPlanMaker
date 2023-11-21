namespace FloorPlanMakerUI
{
    partial class frmDateSelect
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
            calDateSelected = new MonthCalendar();
            SuspendLayout();
            // 
            // calDateSelected
            // 
            calDateSelected.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            calDateSelected.Location = new Point(8, 5);
            calDateSelected.MaxSelectionCount = 1;
            calDateSelected.Name = "calDateSelected";
            calDateSelected.TabIndex = 0;
            calDateSelected.DateChanged += calDateSelected_DateChanged_1;
            calDateSelected.DateSelected += calDateSelected_DateSelected;
            // 
            // frmDateSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(241, 172);
            Controls.Add(calDateSelected);
            Name = "frmDateSelect";
            Text = "frmDateSelect";
            Load += frmDateSelect_Load;
            ResumeLayout(false);
        }

        #endregion

        private MonthCalendar calDateSelected;
    }
}