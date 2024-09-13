namespace FloorPlanMakerUI
{
    partial class frmCustomColors
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
            if (disposing && (components != null)) {
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            colorDialog = new ColorDialog();
            btnDefault = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(12, 25);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1164, 100);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // btnDefault
            // 
            btnDefault.BackColor = Color.FromArgb(100, 130, 180);
            btnDefault.FlatAppearance.BorderSize = 0;
            btnDefault.FlatStyle = FlatStyle.Flat;
            btnDefault.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDefault.ForeColor = Color.White;
            btnDefault.Location = new Point(12, 656);
            btnDefault.Name = "btnDefault";
            btnDefault.Size = new Size(372, 34);
            btnDefault.TabIndex = 1;
            btnDefault.Text = "Set To Default";
            btnDefault.UseVisualStyleBackColor = false;
            btnDefault.Click += btnDefault_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(100, 130, 180);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(410, 656);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(372, 34);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(100, 130, 180);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(804, 656);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(372, 34);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(91, 145);
            panel1.Name = "panel1";
            panel1.Size = new Size(1020, 495);
            panel1.TabIndex = 2;
            // 
            // frmCustomColors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1188, 711);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnDefault);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCustomColors";
            Text = "frmCustomColors";
            Load += frmCustomColors_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private ColorDialog colorDialog;
        private Button btnDefault;
        private Button btnSave;
        private Button btnCancel;
        private Panel panel1;
    }
}