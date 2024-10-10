namespace FloorplanUserControlLibrary
{
    partial class ReservationTimeBlockControl
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
            lblTime = new Label();
            flowPanel = new FlowLayoutPanel();
            lblCoverCount = new Label();
            SuspendLayout();
            // 
            // lblTime
            // 
            lblTime.BackColor = Color.Silver;
            lblTime.Dock = DockStyle.Left;
            lblTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblTime.Location = new Point(0, 0);
            lblTime.Name = "lblTime";
            lblTime.Padding = new Padding(0, 0, 1, 0);
            lblTime.Size = new Size(42, 20);
            lblTime.TabIndex = 0;
            lblTime.Text = "4:00";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowPanel
            // 
            flowPanel.Dock = DockStyle.Right;
            flowPanel.Location = new Point(74, 0);
            flowPanel.Name = "flowPanel";
            flowPanel.Size = new Size(120, 20);
            flowPanel.TabIndex = 1;
            // 
            // lblCoverCount
            // 
            lblCoverCount.BackColor = Color.DarkGray;
            lblCoverCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblCoverCount.Location = new Point(42, 0);
            lblCoverCount.Name = "lblCoverCount";
            lblCoverCount.Padding = new Padding(1, 0, 0, 0);
            lblCoverCount.Size = new Size(30, 20);
            lblCoverCount.TabIndex = 2;
            lblCoverCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ReservationTimeBlockControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(lblCoverCount);
            Controls.Add(flowPanel);
            Controls.Add(lblTime);
            Margin = new Padding(3, 1, 0, 0);
            Name = "ReservationTimeBlockControl";
            Size = new Size(194, 20);
            Load += ReservationTimeBlockControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTime;
        private FlowLayoutPanel flowPanel;
        private Label lblCoverCount;
    }
}
