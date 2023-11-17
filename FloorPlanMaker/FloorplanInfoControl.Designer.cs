namespace FloorPlanMakerUI
{
    partial class FloorplanInfoControl
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
            lblYesterdayCount = new Label();
            lblLastWeekCount = new Label();
            lblCurrentServerCount = new Label();
            lblCoversPerServer = new Label();
            lblSalesPerServer = new Label();
            SuspendLayout();
            // 
            // lblYesterdayCount
            // 
            lblYesterdayCount.BackColor = Color.FromArgb(180, 190, 200);
            lblYesterdayCount.Location = new Point(4, 4);
            lblYesterdayCount.Margin = new Padding(5, 3, 0, 3);
            lblYesterdayCount.Name = "lblYesterdayCount";
            lblYesterdayCount.Size = new Size(144, 20);
            lblYesterdayCount.TabIndex = 0;
            lblYesterdayCount.Text = "Yesterday";
            lblYesterdayCount.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // lblLastWeekCount
            // 
            lblLastWeekCount.BackColor = Color.FromArgb(180, 190, 200);
            lblLastWeekCount.Location = new Point(152, 4);
            lblLastWeekCount.Margin = new Padding(5, 3, 5, 3);
            lblLastWeekCount.Name = "lblLastWeekCount";
            lblLastWeekCount.Size = new Size(144, 20);
            lblLastWeekCount.TabIndex = 0;
            lblLastWeekCount.Text = "Last Week";
            lblLastWeekCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCurrentServerCount
            // 
            lblCurrentServerCount.BackColor = Color.FromArgb(100, 130, 180);
            lblCurrentServerCount.Location = new Point(4, 28);
            lblCurrentServerCount.Margin = new Padding(3);
            lblCurrentServerCount.Name = "lblCurrentServerCount";
            lblCurrentServerCount.Size = new Size(292, 20);
            lblCurrentServerCount.TabIndex = 0;
            lblCurrentServerCount.Text = "Current Server Count";
            lblCurrentServerCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCoversPerServer
            // 
            lblCoversPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblCoversPerServer.Location = new Point(4, 52);
            lblCoversPerServer.Margin = new Padding(3);
            lblCoversPerServer.Name = "lblCoversPerServer";
            lblCoversPerServer.Size = new Size(144, 20);
            lblCoversPerServer.TabIndex = 0;
            lblCoversPerServer.Text = "Covers";
            lblCoversPerServer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSalesPerServer
            // 
            lblSalesPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblSalesPerServer.Location = new Point(152, 52);
            lblSalesPerServer.Margin = new Padding(3);
            lblSalesPerServer.Name = "lblSalesPerServer";
            lblSalesPerServer.Size = new Size(144, 20);
            lblSalesPerServer.TabIndex = 0;
            lblSalesPerServer.Text = "Sales";
            lblSalesPerServer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FloorplanInfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblSalesPerServer);
            Controls.Add(lblCoversPerServer);
            Controls.Add(lblCurrentServerCount);
            Controls.Add(lblLastWeekCount);
            Controls.Add(lblYesterdayCount);
            Name = "FloorplanInfoControl";
            Size = new Size(300, 76);
            ResumeLayout(false);
        }

        #endregion

        private Label lblYesterdayCount;
        private Label lblLastWeekCount;
        private Label lblCurrentServerCount;
        private Label lblCoversPerServer;
        private Label lblSalesPerServer;
    }
}
