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
            picButtonClose = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picButtonClose).BeginInit();
            SuspendLayout();
            // 
            // calDateSelected
            // 
            calDateSelected.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            calDateSelected.Location = new Point(0, 17);
            calDateSelected.MaxSelectionCount = 1;
            calDateSelected.Name = "calDateSelected";
            calDateSelected.TabIndex = 0;
            calDateSelected.DateChanged += calDateSelected_DateChanged_1;
            calDateSelected.DateSelected += calDateSelected_DateSelected;
            // 
            // picButtonClose
            // 
            picButtonClose.BackColor = Color.FromArgb(190, 80, 70);
            picButtonClose.Image = Properties.Resources.X;
            picButtonClose.Location = new Point(210, 0);
            picButtonClose.Name = "picButtonClose";
            picButtonClose.Size = new Size(17, 17);
            picButtonClose.SizeMode = PictureBoxSizeMode.Zoom;
            picButtonClose.TabIndex = 1;
            picButtonClose.TabStop = false;
            picButtonClose.Click += picButtonClose_Click;
            // 
            // frmDateSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(228, 179);
            Controls.Add(picButtonClose);
            Controls.Add(calDateSelected);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmDateSelect";
            Text = "frmDateSelect";
            Load += frmDateSelect_Load;
            ((System.ComponentModel.ISupportInitialize)picButtonClose).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MonthCalendar calDateSelected;
        private PictureBox picButtonClose;
    }
}