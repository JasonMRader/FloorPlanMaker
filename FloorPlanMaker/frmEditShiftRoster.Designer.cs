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
            btnDone = new Button();
            panel1 = new Panel();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowThisFloorplan
            // 
            flowThisFloorplan.AutoScroll = true;
            flowThisFloorplan.BackColor = Color.WhiteSmoke;
            flowThisFloorplan.Location = new Point(31, 131);
            flowThisFloorplan.Name = "flowThisFloorplan";
            flowThisFloorplan.Size = new Size(239, 408);
            flowThisFloorplan.TabIndex = 0;
            // 
            // flowOtherServers
            // 
            flowOtherServers.AutoScroll = true;
            flowOtherServers.BackColor = Color.WhiteSmoke;
            flowOtherServers.Location = new Point(306, 131);
            flowOtherServers.Name = "flowOtherServers";
            flowOtherServers.Size = new Size(239, 408);
            flowOtherServers.TabIndex = 0;
            // 
            // lblSelectedDiningArea
            // 
            lblSelectedDiningArea.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSelectedDiningArea.Location = new Point(31, 87);
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
            cboFloorplans.Location = new Point(306, 92);
            cboFloorplans.Name = "cboFloorplans";
            cboFloorplans.Size = new Size(239, 29);
            cboFloorplans.TabIndex = 2;
            cboFloorplans.SelectedIndexChanged += cboFloorplans_SelectedIndexChanged;
            // 
            // cbServersNotOnShift
            // 
            cbServersNotOnShift.AutoSize = true;
            cbServersNotOnShift.Location = new Point(306, 63);
            cbServersNotOnShift.Name = "cbServersNotOnShift";
            cbServersNotOnShift.Size = new Size(132, 19);
            cbServersNotOnShift.TabIndex = 3;
            cbServersNotOnShift.Text = "Servers Not On Shift";
            cbServersNotOnShift.UseVisualStyleBackColor = true;
            cbServersNotOnShift.CheckedChanged += cbServersNotOnShift_CheckedChanged;
            // 
            // txtSearchServers
            // 
            txtSearchServers.Location = new Point(445, 61);
            txtSearchServers.Name = "txtSearchServers";
            txtSearchServers.PlaceholderText = "Search Servers";
            txtSearchServers.Size = new Size(100, 23);
            txtSearchServers.TabIndex = 4;
            txtSearchServers.Visible = false;
            txtSearchServers.TextChanged += txtSearchServers_TextChanged;
            // 
            // btnDone
            // 
            btnDone.Location = new Point(31, 545);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(514, 36);
            btnDone.TabIndex = 5;
            btnDone.Text = "Done";
            btnDone.UseVisualStyleBackColor = true;
            btnDone.Click += btnDone_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(225, 225, 225);
            panel1.Controls.Add(btnDone);
            panel1.Controls.Add(txtSearchServers);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblSelectedDiningArea);
            panel1.Controls.Add(cbServersNotOnShift);
            panel1.Controls.Add(cboFloorplans);
            panel1.Controls.Add(flowThisFloorplan);
            panel1.Controls.Add(flowOtherServers);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(579, 605);
            panel1.TabIndex = 6;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(180, 190, 200);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(579, 42);
            label1.TabIndex = 1;
            label1.Text = "Move Servers";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmEditShiftRoster
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(603, 633);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEditShiftRoster";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEditShiftRoster";
            Load += frmEditShiftRoster_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowThisFloorplan;
        private FlowLayoutPanel flowOtherServers;
        private Label lblSelectedDiningArea;
        private ComboBox cboFloorplans;
        private CheckBox cbServersNotOnShift;
        private TextBox txtSearchServers;
        private Button btnDone;
        private Panel panel1;
        private Label label1;
    }
}