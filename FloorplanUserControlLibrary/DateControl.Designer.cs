namespace FloorplanUserControlLibrary
{
    partial class DateControl
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
            lblDate = new Label();
            flowInfo = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.Dock = DockStyle.Top;
            lblDate.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDate.Location = new Point(0, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(150, 25);
            lblDate.TabIndex = 0;
            lblDate.Text = "1";
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowInfo
            // 
            flowInfo.Dock = DockStyle.Bottom;
            flowInfo.Location = new Point(0, 25);
            flowInfo.Name = "flowInfo";
            flowInfo.Size = new Size(150, 135);
            flowInfo.TabIndex = 1;
            // 
            // DateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowInfo);
            Controls.Add(lblDate);
            Name = "DateControl";
            Size = new Size(150, 160);
            ResumeLayout(false);
        }

        #endregion

        private Label lblDate;
        private FlowLayoutPanel flowInfo;
    }
}
