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
            SuspendLayout();
            // 
            // flowDistributionSelect
            // 
            flowDistributionSelect.Location = new Point(29, 59);
            flowDistributionSelect.Name = "flowDistributionSelect";
            flowDistributionSelect.Size = new Size(645, 345);
            flowDistributionSelect.TabIndex = 0;
            // 
            // frmServerDistributionSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 450);
            Controls.Add(flowDistributionSelect);
            Name = "frmServerDistributionSelection";
            Text = "frmServerDistributionSelection";
            Load += frmServerDistributionSelection_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowDistributionSelect;
    }
}