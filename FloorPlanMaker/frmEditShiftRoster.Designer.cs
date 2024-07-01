namespace FloorPlanMakerUI
{
    partial class frmEditShiftRoster
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
            flowThisFloorplan = new FlowLayoutPanel();
            flowOtherServers = new FlowLayoutPanel();
            lblSelectedDiningArea = new Label();
            cboFloorplans = new ComboBox();
            cbServersNotOnShift = new CheckBox();
            txtSearchServers = new TextBox();
            SuspendLayout();
            // 
            // flowThisFloorplan
            // 
            flowThisFloorplan.AutoScroll = true;
            flowThisFloorplan.BackColor = Color.WhiteSmoke;
            flowThisFloorplan.Location = new Point(12, 86);
            flowThisFloorplan.Name = "flowThisFloorplan";
            flowThisFloorplan.Size = new Size(239, 418);
            flowThisFloorplan.TabIndex = 0;
            // 
            // flowOtherServers
            // 
            flowOtherServers.AutoScroll = true;
            flowOtherServers.BackColor = Color.WhiteSmoke;
            flowOtherServers.Location = new Point(287, 86);
            flowOtherServers.Name = "flowOtherServers";
            flowOtherServers.Size = new Size(239, 418);
            flowOtherServers.TabIndex = 0;
            // 
            // lblSelectedDiningArea
            // 
            lblSelectedDiningArea.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSelectedDiningArea.Location = new Point(12, 41);
            lblSelectedDiningArea.Name = "lblSelectedDiningArea";
            lblSelectedDiningArea.Size = new Size(239, 34);
            lblSelectedDiningArea.TabIndex = 1;
            lblSelectedDiningArea.Text = "Dining Area";
            lblSelectedDiningArea.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboFloorplans
            // 
            cboFloorplans.FlatStyle = FlatStyle.Flat;
            cboFloorplans.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cboFloorplans.FormattingEnabled = true;
            cboFloorplans.Location = new Point(287, 41);
            cboFloorplans.Name = "cboFloorplans";
            cboFloorplans.Size = new Size(239, 29);
            cboFloorplans.TabIndex = 2;
            cboFloorplans.SelectedIndexChanged += cboFloorplans_SelectedIndexChanged;
            // 
            // cbServersNotOnShift
            // 
            cbServersNotOnShift.AutoSize = true;
            cbServersNotOnShift.Location = new Point(287, 12);
            cbServersNotOnShift.Name = "cbServersNotOnShift";
            cbServersNotOnShift.Size = new Size(132, 19);
            cbServersNotOnShift.TabIndex = 3;
            cbServersNotOnShift.Text = "Servers Not On Shift";
            cbServersNotOnShift.UseVisualStyleBackColor = true;
            cbServersNotOnShift.CheckedChanged += cbServersNotOnShift_CheckedChanged;
            // 
            // txtSearchServers
            // 
            txtSearchServers.Location = new Point(426, 10);
            txtSearchServers.Name = "txtSearchServers";
            txtSearchServers.PlaceholderText = "Search Servers";
            txtSearchServers.Size = new Size(100, 23);
            txtSearchServers.TabIndex = 4;
            txtSearchServers.Visible = false;
            txtSearchServers.TextChanged += txtSearchServers_TextChanged;
            // 
            // frmEditShiftRoster
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 516);
            Controls.Add(txtSearchServers);
            Controls.Add(cbServersNotOnShift);
            Controls.Add(cboFloorplans);
            Controls.Add(lblSelectedDiningArea);
            Controls.Add(flowOtherServers);
            Controls.Add(flowThisFloorplan);
            Name = "frmEditShiftRoster";
            Text = "frmEditShiftRoster";
            Load += frmEditShiftRoster_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowThisFloorplan;
        private FlowLayoutPanel flowOtherServers;
        private Label lblSelectedDiningArea;
        private ComboBox cboFloorplans;
        private CheckBox cbServersNotOnShift;
        private TextBox txtSearchServers;
    }
}