namespace FloorPlanMakerUI
{
    partial class frmServerDistributionSelection
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
            flowDistributionSelect = new FlowLayoutPanel();
            lblDistribution = new Label();
            lblServerCount = new Label();
            lblServerRemainder = new Label();
            SuspendLayout();
            // 
            // flowDistributionSelect
            // 
            flowDistributionSelect.Location = new Point(29, 156);
            flowDistributionSelect.Name = "flowDistributionSelect";
            flowDistributionSelect.Size = new Size(645, 248);
            flowDistributionSelect.TabIndex = 0;
            // 
            // lblDistribution
            // 
            lblDistribution.AutoSize = true;
            lblDistribution.Location = new Point(382, 34);
            lblDistribution.Name = "lblDistribution";
            lblDistribution.Size = new Size(69, 15);
            lblDistribution.TabIndex = 1;
            lblDistribution.Text = "Distribution";
            // 
            // lblServerCount
            // 
            lblServerCount.AutoSize = true;
            lblServerCount.Location = new Point(29, 33);
            lblServerCount.Name = "lblServerCount";
            lblServerCount.Size = new Size(44, 15);
            lblServerCount.TabIndex = 2;
            lblServerCount.Text = "Servers";
            // 
            // lblServerRemainder
            // 
            lblServerRemainder.AutoSize = true;
            lblServerRemainder.Location = new Point(196, 33);
            lblServerRemainder.Name = "lblServerRemainder";
            lblServerRemainder.Size = new Size(44, 15);
            lblServerRemainder.TabIndex = 2;
            lblServerRemainder.Text = "Servers";
            // 
            // frmServerDistributionSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 450);
            Controls.Add(lblServerRemainder);
            Controls.Add(lblServerCount);
            Controls.Add(lblDistribution);
            Controls.Add(flowDistributionSelect);
            Name = "frmServerDistributionSelection";
            Text = "frmServerDistributionSelection";
            Load += frmServerDistributionSelection_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowDistributionSelect;
        private Label lblDistribution;
        private Label lblServerCount;
        private Label lblServerRemainder;
    }
}