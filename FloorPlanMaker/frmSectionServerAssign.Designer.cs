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
            pbClose = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbClose).BeginInit();
            SuspendLayout();
            // 
            // flowServerSelect
            // 
            flowServerSelect.AutoSize = true;
            flowServerSelect.FlowDirection = FlowDirection.TopDown;
            flowServerSelect.Location = new Point(12, 64);
            flowServerSelect.Name = "flowServerSelect";
            flowServerSelect.Size = new Size(192, 200);
            flowServerSelect.TabIndex = 0;
            // 
            // cboDiningArea
            // 
            cboDiningArea.Enabled = false;
            cboDiningArea.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningArea.FormattingEnabled = true;
            cboDiningArea.Location = new Point(12, 29);
            cboDiningArea.Name = "cboDiningArea";
            cboDiningArea.Size = new Size(192, 29);
            cboDiningArea.TabIndex = 1;
            cboDiningArea.SelectedIndexChanged += cboDiningArea_SelectedIndexChanged;
            // 
            // pbClose
            // 
            pbClose.BackColor = Color.Red;
            pbClose.Image = Properties.Resources.X;
            pbClose.Location = new Point(193, 3);
            pbClose.Name = "pbClose";
            pbClose.Size = new Size(20, 20);
            pbClose.SizeMode = PictureBoxSizeMode.Zoom;
            pbClose.TabIndex = 2;
            pbClose.TabStop = false;
            pbClose.Click += pbClose_Click;
            // 
            // frmSectionServerAssign
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(216, 276);
            Controls.Add(pbClose);
            Controls.Add(cboDiningArea);
            Controls.Add(flowServerSelect);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSectionServerAssign";
            Text = "frmSectionServerAssign";
            Load += frmSectionServerAssign_Load;
            ((System.ComponentModel.ISupportInitialize)pbClose).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowServerSelect;
        private ComboBox cboDiningArea;
        private PictureBox pbClose;
    }
}