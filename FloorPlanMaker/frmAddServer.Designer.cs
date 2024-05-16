namespace FloorPlanMakerUI
{
    partial class frmAddServer
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
            label1 = new Label();
            txtServerName = new TextBox();
            label2 = new Label();
            btnAddServer = new Button();
            btnDone = new Button();
            lbServersToAdd = new ListBox();
            btnCancel = new Button();
            label3 = new Label();
            panel1 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel4 = new Panel();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(85, 36);
            label1.Name = "label1";
            label1.Size = new Size(53, 21);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // txtServerName
            // 
            txtServerName.Location = new Point(10, 60);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(206, 23);
            txtServerName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(209, 19);
            label2.Name = "label2";
            label2.Size = new Size(182, 30);
            label2.TabIndex = 0;
            label2.Text = "Add New Servers";
            // 
            // btnAddServer
            // 
            btnAddServer.BackColor = Color.FromArgb(100, 130, 180);
            btnAddServer.FlatAppearance.BorderSize = 0;
            btnAddServer.FlatStyle = FlatStyle.Flat;
            btnAddServer.Location = new Point(10, 133);
            btnAddServer.Name = "btnAddServer";
            btnAddServer.Size = new Size(206, 34);
            btnAddServer.TabIndex = 2;
            btnAddServer.Text = "Add More";
            btnAddServer.UseVisualStyleBackColor = false;
            btnAddServer.Click += btnAddServer_Click;
            // 
            // btnDone
            // 
            btnDone.BackColor = Color.FromArgb(120, 180, 120);
            btnDone.FlatAppearance.BorderSize = 0;
            btnDone.FlatStyle = FlatStyle.Flat;
            btnDone.Location = new Point(10, 173);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(206, 36);
            btnDone.TabIndex = 2;
            btnDone.Text = "Done";
            btnDone.UseVisualStyleBackColor = false;
            btnDone.Click += btnDone_Click;
            // 
            // lbServersToAdd
            // 
            lbServersToAdd.FormattingEnabled = true;
            lbServersToAdd.ItemHeight = 15;
            lbServersToAdd.Location = new Point(18, 24);
            lbServersToAdd.Name = "lbServersToAdd";
            lbServersToAdd.Size = new Size(189, 244);
            lbServersToAdd.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(190, 80, 70);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(10, 215);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(206, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(77, 0);
            label3.Name = "label3";
            label3.Size = new Size(66, 21);
            label3.TabIndex = 0;
            label3.Text = "Servers";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(25, 66);
            panel1.Name = "panel1";
            panel1.Size = new Size(260, 315);
            panel1.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(btnAddServer);
            panel3.Controls.Add(btnDone);
            panel3.Controls.Add(txtServerName);
            panel3.Controls.Add(btnCancel);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(13, 15);
            panel3.Name = "panel3";
            panel3.Size = new Size(226, 285);
            panel3.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Controls.Add(panel4);
            panel2.Location = new Point(319, 66);
            panel2.Name = "panel2";
            panel2.Size = new Size(261, 315);
            panel2.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Controls.Add(label3);
            panel4.Controls.Add(lbServersToAdd);
            panel4.Location = new Point(16, 15);
            panel4.Name = "panel4";
            panel4.Size = new Size(226, 285);
            panel4.TabIndex = 0;
            // 
            // frmAddServer
            // 
            AcceptButton = btnAddServer;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(615, 420);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAddServer";
            Text = "frmAddServer";
            Load += frmAddServer_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtServerName;
        private Label label2;
        private Button btnAddServer;
        private Button btnDone;
        private ListBox lbServersToAdd;
        private Button btnCancel;
        private Label label3;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
    }
}