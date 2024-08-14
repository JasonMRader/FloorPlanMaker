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
            flowServerSelect = new FlowLayoutPanel();
            cboDiningArea = new ComboBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            SuspendLayout();
            // 
            // flowServerSelect
            // 
            flowServerSelect.AutoSize = true;
            flowServerSelect.FlowDirection = FlowDirection.TopDown;
            flowServerSelect.Location = new Point(12, 47);
            flowServerSelect.Name = "flowServerSelect";
            flowServerSelect.Size = new Size(291, 55);
            flowServerSelect.TabIndex = 0;
            // 
            // cboDiningArea
            // 
            cboDiningArea.Enabled = false;
            cboDiningArea.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningArea.FormattingEnabled = true;
            cboDiningArea.Location = new Point(12, 12);
            cboDiningArea.Name = "cboDiningArea";
            cboDiningArea.Size = new Size(291, 29);
            cboDiningArea.TabIndex = 1;
            cboDiningArea.SelectedIndexChanged += cboDiningArea_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(313, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(2, 116);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(64, 64, 64);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(2, 116);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(64, 64, 64);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(2, 114);
            panel3.Name = "panel3";
            panel3.Size = new Size(311, 2);
            panel3.TabIndex = 2;
            // 
            // frmSectionServerAssign
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 116);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(cboDiningArea);
            Controls.Add(flowServerSelect);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSectionServerAssign";
            Text = "frmSectionServerAssign";
            Load += frmSectionServerAssign_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowServerSelect;
        private ComboBox cboDiningArea;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
    }
}