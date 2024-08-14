namespace FloorPlanMakerUI
{
    partial class frmSectionServerAssign
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
            flowServersInFloorplan = new FlowLayoutPanel();
            cboDiningArea = new ComboBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            flowServersInSection = new FlowLayoutPanel();
            pnlSectionColor = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pnlSectionColor.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.AutoSize = true;
            flowServersInFloorplan.FlowDirection = FlowDirection.TopDown;
            flowServersInFloorplan.Location = new Point(10, 100);
            flowServersInFloorplan.Margin = new Padding(10, 10, 3, 3);
            flowServersInFloorplan.MinimumSize = new Size(300, 30);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Size = new Size(300, 30);
            flowServersInFloorplan.TabIndex = 0;
            // 
            // cboDiningArea
            // 
            cboDiningArea.Enabled = false;
            cboDiningArea.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningArea.FormattingEnabled = true;
            cboDiningArea.Location = new Point(10, 15);
            cboDiningArea.Margin = new Padding(10, 10, 3, 3);
            cboDiningArea.Name = "cboDiningArea";
            cboDiningArea.Size = new Size(300, 29);
            cboDiningArea.TabIndex = 1;
            cboDiningArea.SelectedIndexChanged += cboDiningArea_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(348, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(2, 165);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(64, 64, 64);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(2, 165);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(64, 64, 64);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(2, 163);
            panel3.Name = "panel3";
            panel3.Size = new Size(346, 2);
            panel3.TabIndex = 2;
            // 
            // flowServersInSection
            // 
            flowServersInSection.AutoSize = true;
            flowServersInSection.FlowDirection = FlowDirection.TopDown;
            flowServersInSection.Location = new Point(10, 57);
            flowServersInSection.Margin = new Padding(10, 10, 3, 3);
            flowServersInSection.MinimumSize = new Size(300, 30);
            flowServersInSection.Name = "flowServersInSection";
            flowServersInSection.Size = new Size(300, 30);
            flowServersInSection.TabIndex = 0;
            // 
            // pnlSectionColor
            // 
            pnlSectionColor.AutoSize = true;
            pnlSectionColor.BackColor = Color.FromArgb(255, 128, 0);
            pnlSectionColor.Controls.Add(flowLayoutPanel1);
            pnlSectionColor.Location = new Point(5, 0);
            pnlSectionColor.Name = "pnlSectionColor";
            pnlSectionColor.Size = new Size(340, 160);
            pnlSectionColor.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(180, 190, 200);
            flowLayoutPanel1.Controls.Add(cboDiningArea);
            flowLayoutPanel1.Controls.Add(flowServersInSection);
            flowLayoutPanel1.Controls.Add(flowServersInFloorplan);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(10, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 5, 0, 0);
            flowLayoutPanel1.Size = new Size(320, 150);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // frmSectionServerAssign
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(350, 165);
            Controls.Add(pnlSectionColor);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSectionServerAssign";
            Text = "frmSectionServerAssign";
            Load += frmSectionServerAssign_Load;
            VisibleChanged += frmSectionServerAssign_VisibleChanged;
            pnlSectionColor.ResumeLayout(false);
            pnlSectionColor.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowServersInFloorplan;
        private ComboBox cboDiningArea;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private FlowLayoutPanel flowServersInSection;
        private Panel pnlSectionColor;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}