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
            SuspendLayout();
            // 
            // flowServerSelect
            // 
            flowServerSelect.Location = new Point(12, 47);
            flowServerSelect.Name = "flowServerSelect";
            flowServerSelect.Size = new Size(192, 414);
            flowServerSelect.TabIndex = 0;
            flowServerSelect.Paint += flowServerSelect_Paint;
            // 
            // cboDiningArea
            // 
            cboDiningArea.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningArea.FormattingEnabled = true;
            cboDiningArea.Location = new Point(12, 12);
            cboDiningArea.Name = "cboDiningArea";
            cboDiningArea.Size = new Size(192, 29);
            cboDiningArea.TabIndex = 1;
            cboDiningArea.SelectedIndexChanged += cboDiningArea_SelectedIndexChanged;
            // 
            // frmSectionServerAssign
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(216, 473);
            Controls.Add(cboDiningArea);
            Controls.Add(flowServerSelect);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSectionServerAssign";
            Text = "frmSectionServerAssign";
            Load += frmSectionServerAssign_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowServerSelect;
        private ComboBox cboDiningArea;
    }
}