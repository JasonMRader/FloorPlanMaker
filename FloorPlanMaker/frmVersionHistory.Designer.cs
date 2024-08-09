namespace FloorPlanMakerUI
{
    partial class frmVersionHistory
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
            rdoKnownBugs = new RadioButton();
            rdoUpcomingFeatures = new RadioButton();
            rdoRecentUpdates = new RadioButton();
            txtInfoBox = new TextBox();
            pnlUpdates = new Panel();
            rdoUpdate8_8_24 = new RadioButton();
            pnlUpdates.SuspendLayout();
            SuspendLayout();
            // 
            // rdoKnownBugs
            // 
            rdoKnownBugs.AutoSize = true;
            rdoKnownBugs.Checked = true;
            rdoKnownBugs.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoKnownBugs.Location = new Point(12, 12);
            rdoKnownBugs.Name = "rdoKnownBugs";
            rdoKnownBugs.Size = new Size(114, 25);
            rdoKnownBugs.TabIndex = 0;
            rdoKnownBugs.TabStop = true;
            rdoKnownBugs.Text = "Known Bugs";
            rdoKnownBugs.UseVisualStyleBackColor = true;
            rdoKnownBugs.CheckedChanged += rdoKnownBugs_CheckedChanged;
            // 
            // rdoUpcomingFeatures
            // 
            rdoUpcomingFeatures.AutoSize = true;
            rdoUpcomingFeatures.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoUpcomingFeatures.Location = new Point(132, 12);
            rdoUpcomingFeatures.Name = "rdoUpcomingFeatures";
            rdoUpcomingFeatures.Size = new Size(163, 25);
            rdoUpcomingFeatures.TabIndex = 0;
            rdoUpcomingFeatures.Text = "Upcoming Features";
            rdoUpcomingFeatures.UseVisualStyleBackColor = true;
            rdoUpcomingFeatures.CheckedChanged += rdoKnownBugs_CheckedChanged;
            // 
            // rdoRecentUpdates
            // 
            rdoRecentUpdates.AutoSize = true;
            rdoRecentUpdates.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoRecentUpdates.Location = new Point(301, 12);
            rdoRecentUpdates.Name = "rdoRecentUpdates";
            rdoRecentUpdates.Size = new Size(136, 25);
            rdoRecentUpdates.TabIndex = 0;
            rdoRecentUpdates.Text = "Recent Updates";
            rdoRecentUpdates.UseVisualStyleBackColor = true;
            rdoRecentUpdates.CheckedChanged += rdoKnownBugs_CheckedChanged;
            // 
            // txtInfoBox
            // 
            txtInfoBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtInfoBox.Location = new Point(12, 83);
            txtInfoBox.Multiline = true;
            txtInfoBox.Name = "txtInfoBox";
            txtInfoBox.ReadOnly = true;
            txtInfoBox.Size = new Size(858, 502);
            txtInfoBox.TabIndex = 1;
            // 
            // pnlUpdates
            // 
            pnlUpdates.Controls.Add(rdoUpdate8_8_24);
            pnlUpdates.Location = new Point(12, 43);
            pnlUpdates.Name = "pnlUpdates";
            pnlUpdates.Size = new Size(858, 34);
            pnlUpdates.TabIndex = 2;
            pnlUpdates.Visible = false;
            // 
            // rdoUpdate8_8_24
            // 
            rdoUpdate8_8_24.AutoSize = true;
            rdoUpdate8_8_24.Checked = true;
            rdoUpdate8_8_24.Location = new Point(3, 3);
            rdoUpdate8_8_24.Name = "rdoUpdate8_8_24";
            rdoUpdate8_8_24.Size = new Size(108, 19);
            rdoUpdate8_8_24.TabIndex = 0;
            rdoUpdate8_8_24.TabStop = true;
            rdoUpdate8_8_24.Text = "Aug 8th Update";
            rdoUpdate8_8_24.UseVisualStyleBackColor = true;
            // 
            // frmVersionHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 597);
            Controls.Add(pnlUpdates);
            Controls.Add(txtInfoBox);
            Controls.Add(rdoRecentUpdates);
            Controls.Add(rdoUpcomingFeatures);
            Controls.Add(rdoKnownBugs);
            Name = "frmVersionHistory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmVersionHistory";
            Load += frmVersionHistory_Load;
            pnlUpdates.ResumeLayout(false);
            pnlUpdates.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rdoKnownBugs;
        private RadioButton rdoUpcomingFeatures;
        private RadioButton rdoRecentUpdates;
        private TextBox txtInfoBox;
        private Panel pnlUpdates;
        private RadioButton rdoUpdate8_8_24;
    }
}