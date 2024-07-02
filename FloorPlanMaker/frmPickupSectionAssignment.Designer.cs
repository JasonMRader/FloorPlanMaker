namespace FloorPlanMakerUI
{
    partial class frmPickupSectionAssignment
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
            btnOK = new Button();
            flowDiningAreas = new FlowLayoutPanel();
            flowSections = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            btnUnassignSection = new Button();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(100, 130, 180);
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.Location = new Point(32, 519);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(563, 54);
            btnOK.TabIndex = 1;
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.BackColor = Color.WhiteSmoke;
            flowDiningAreas.Location = new Point(32, 62);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(269, 412);
            flowDiningAreas.TabIndex = 2;
            // 
            // flowSections
            // 
            flowSections.BackColor = Color.WhiteSmoke;
            flowSections.Location = new Point(326, 62);
            flowSections.Name = "flowSections";
            flowSections.Size = new Size(269, 412);
            flowSections.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(326, 33);
            label1.Name = "label1";
            label1.Size = new Size(135, 21);
            label1.TabIndex = 3;
            label1.Text = "Assign Pickup To:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(32, 33);
            label2.Name = "label2";
            label2.Size = new Size(148, 21);
            label2.TabIndex = 3;
            label2.Text = "Select Dining Area:";
            // 
            // btnUnassignSection
            // 
            btnUnassignSection.BackColor = Color.FromArgb(190, 80, 70);
            btnUnassignSection.FlatAppearance.BorderSize = 0;
            btnUnassignSection.FlatStyle = FlatStyle.Flat;
            btnUnassignSection.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnUnassignSection.Location = new Point(32, 477);
            btnUnassignSection.Name = "btnUnassignSection";
            btnUnassignSection.Size = new Size(563, 36);
            btnUnassignSection.TabIndex = 1;
            btnUnassignSection.Text = "Unassign Pickup Section";
            btnUnassignSection.UseVisualStyleBackColor = false;
            btnUnassignSection.Click += btnUnassignSection_Click;
            // 
            // frmPickupSectionAssignment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 585);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowSections);
            Controls.Add(flowDiningAreas);
            Controls.Add(btnUnassignSection);
            Controls.Add(btnOK);
            Name = "frmPickupSectionAssignment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Assign Pickup Section";
            Load += frmPickupSectionAssignment_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private FlowLayoutPanel flowDiningAreas;
        private FlowLayoutPanel flowSections;
        private Label label1;
        private Label label2;
        private Button btnUnassignSection;
    }
}