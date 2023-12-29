namespace FloorPlanMaker
{
    partial class frmEditStaff
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
            btnAssignTables = new Button();
            flowDiningAreaAssignment = new FlowLayoutPanel();
            flowUnassignedServers = new FlowLayoutPanel();
            lblShiftDate = new Label();
            cbIsAM = new CheckBox();
            btnCreateANewShift = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            pbBack = new PictureBox();
            pbForward = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbForward).BeginInit();
            SuspendLayout();
            // 
            // btnAssignTables
            // 
            btnAssignTables.BackColor = Color.FromArgb(100, 130, 180);
            btnAssignTables.FlatAppearance.BorderSize = 0;
            btnAssignTables.FlatStyle = FlatStyle.Flat;
            btnAssignTables.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnAssignTables.ForeColor = Color.Black;
            btnAssignTables.Location = new Point(25, 930);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(1217, 46);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = false;
            btnAssignTables.Click += btnAssignTables_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.BackColor = Color.FromArgb(180, 190, 200);
            flowDiningAreaAssignment.Location = new Point(429, 98);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(794, 783);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.AutoScroll = true;
            flowUnassignedServers.BackColor = Color.WhiteSmoke;
            flowUnassignedServers.Location = new Point(17, 15);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(313, 785);
            flowUnassignedServers.TabIndex = 11;
            // 
            // lblShiftDate
            // 
            lblShiftDate.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.ForeColor = Color.Black;
            lblShiftDate.Location = new Point(505, 17);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(551, 39);
            lblShiftDate.TabIndex = 16;
            lblShiftDate.Text = "Date";
            lblShiftDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(251, 175, 0);
            cbIsAM.Checked = true;
            cbIsAM.CheckState = CheckState.Checked;
            cbIsAM.FlatAppearance.BorderSize = 0;
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.ForeColor = Color.Black;
            cbIsAM.Image = FloorPlanMakerUI.Properties.Resources.smallSunrise;
            cbIsAM.Location = new Point(1062, 17);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(79, 39);
            cbIsAM.TabIndex = 19;
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsPM_CheckedChanged;
            // 
            // btnCreateANewShift
            // 
            btnCreateANewShift.BackColor = Color.FromArgb(100, 130, 180);
            btnCreateANewShift.FlatAppearance.BorderSize = 0;
            btnCreateANewShift.FlatStyle = FlatStyle.Flat;
            btnCreateANewShift.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateANewShift.ForeColor = Color.Black;
            btnCreateANewShift.Location = new Point(25, 15);
            btnCreateANewShift.Name = "btnCreateANewShift";
            btnCreateANewShift.Size = new Size(352, 43);
            btnCreateANewShift.TabIndex = 25;
            btnCreateANewShift.Text = "Edit / Create Shifts";
            btnCreateANewShift.UseVisualStyleBackColor = false;
            btnCreateANewShift.Click += btnCreateANewShift_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(flowUnassignedServers);
            panel1.Location = new Point(25, 81);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 816);
            panel1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Location = new Point(409, 81);
            panel2.Name = "panel2";
            panel2.Size = new Size(833, 816);
            panel2.TabIndex = 27;
            // 
            // pbBack
            // 
            pbBack.Image = FloorPlanMakerUI.Properties.Resources.back;
            pbBack.Location = new Point(429, 15);
            pbBack.Name = "pbBack";
            pbBack.Size = new Size(55, 43);
            pbBack.SizeMode = PictureBoxSizeMode.Zoom;
            pbBack.TabIndex = 28;
            pbBack.TabStop = false;
            pbBack.Click += btnDateDown_Click;
            // 
            // pbForward
            // 
            pbForward.Image = FloorPlanMakerUI.Properties.Resources.forward;
            pbForward.Location = new Point(1168, 15);
            pbForward.Name = "pbForward";
            pbForward.Size = new Size(55, 43);
            pbForward.SizeMode = PictureBoxSizeMode.Zoom;
            pbForward.TabIndex = 28;
            pbForward.TabStop = false;
            pbForward.Click += btnDateUp_Click;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1264, 997);
            Controls.Add(pbForward);
            Controls.Add(pbBack);
            Controls.Add(btnCreateANewShift);
            Controls.Add(cbIsAM);
            Controls.Add(lblShiftDate);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignTables);
            Controls.Add(panel1);
            Controls.Add(panel2);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "frmEditStaff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEditStaff";
            Load += frmEditStaff_Load;
            KeyDown += frmEditStaff_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbForward).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnAssignTables;
        private FlowLayoutPanel flowDiningAreaAssignment;
        private FlowLayoutPanel flowUnassignedServers;
        private Label lblShiftDate;
        private CheckBox cbIsAM;
        private Button btnCreateANewShift;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pbBack;
        private PictureBox pbForward;
    }
}