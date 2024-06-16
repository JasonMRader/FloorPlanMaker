namespace FloorplanUserControlLibrary
{
    partial class ShiftImgDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picWeekDay = new PictureBox();
            picShiftType = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picWeekDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picShiftType).BeginInit();
            SuspendLayout();
            // 
            // picWeekDay
            // 
            picWeekDay.Location = new Point(0, 0);
            picWeekDay.Name = "picWeekDay";
            picWeekDay.Size = new Size(32, 15);
            picWeekDay.SizeMode = PictureBoxSizeMode.Zoom;
            picWeekDay.TabIndex = 0;
            picWeekDay.TabStop = false;
            // 
            // picShiftType
            // 
            picShiftType.Location = new Point(0, 15);
            picShiftType.Name = "picShiftType";
            picShiftType.Size = new Size(32, 24);
            picShiftType.SizeMode = PictureBoxSizeMode.Zoom;
            picShiftType.TabIndex = 0;
            picShiftType.TabStop = false;
            // 
            // ShiftImgDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(picShiftType);
            Controls.Add(picWeekDay);
            Name = "ShiftImgDisplay";
            Size = new Size(32, 39);
            ((System.ComponentModel.ISupportInitialize)picWeekDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)picShiftType).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picWeekDay;
        private PictureBox picShiftType;
    }
}
