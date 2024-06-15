namespace FloorPlanMakerUI
{
    partial class frmTemplateCreator
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
            pnlFloorplan = new Panel();
            flowSectionSelect = new FlowLayoutPanel();
            btnAddSection = new Button();
            btnRemoveSection = new Button();
            lblServerCount = new Label();
            lblDiningArea = new Label();
            btnClose = new Button();
            SuspendLayout();
            // 
            // pnlFloorplan
            // 
            pnlFloorplan.Location = new Point(449, 73);
            pnlFloorplan.Name = "pnlFloorplan";
            pnlFloorplan.Size = new Size(672, 877);
            pnlFloorplan.TabIndex = 0;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.Location = new Point(37, 73);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(335, 877);
            flowSectionSelect.TabIndex = 1;
            // 
            // btnAddSection
            // 
            btnAddSection.Location = new Point(37, 34);
            btnAddSection.Name = "btnAddSection";
            btnAddSection.Size = new Size(162, 23);
            btnAddSection.TabIndex = 2;
            btnAddSection.Text = "Add Section";
            btnAddSection.UseVisualStyleBackColor = true;
            btnAddSection.Click += btnAddSection_Click;
            // 
            // btnRemoveSection
            // 
            btnRemoveSection.Location = new Point(210, 34);
            btnRemoveSection.Name = "btnRemoveSection";
            btnRemoveSection.Size = new Size(162, 23);
            btnRemoveSection.TabIndex = 2;
            btnRemoveSection.Text = "Remove Section";
            btnRemoveSection.UseVisualStyleBackColor = true;
            // 
            // lblServerCount
            // 
            lblServerCount.AutoSize = true;
            lblServerCount.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerCount.Location = new Point(449, 26);
            lblServerCount.Name = "lblServerCount";
            lblServerCount.Size = new Size(101, 30);
            lblServerCount.TabIndex = 3;
            lblServerCount.Text = "6 Servers";
            // 
            // lblDiningArea
            // 
            lblDiningArea.AutoSize = true;
            lblDiningArea.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningArea.Location = new Point(726, 26);
            lblDiningArea.Name = "lblDiningArea";
            lblDiningArea.Size = new Size(137, 30);
            lblDiningArea.TabIndex = 3;
            lblDiningArea.Text = "Inside Dining";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(1089, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmTemplateCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 997);
            Controls.Add(btnClose);
            Controls.Add(lblDiningArea);
            Controls.Add(lblServerCount);
            Controls.Add(btnRemoveSection);
            Controls.Add(btnAddSection);
            Controls.Add(flowSectionSelect);
            Controls.Add(pnlFloorplan);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTemplateCreator";
            Text = "frmTemplateCreator";
            Load += frmTemplateCreator_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlFloorplan;
        private FlowLayoutPanel flowSectionSelect;
        private Button btnAddSection;
        private Button btnRemoveSection;
        private Label lblServerCount;
        private Label lblDiningArea;
        private Button btnClose;
    }
}