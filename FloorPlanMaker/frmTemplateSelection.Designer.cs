namespace FloorPlanMaker
{
    partial class frmTemplateSelection
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
            panel1 = new Panel();
            btnFirstTemplate = new Button();
            panel2 = new Panel();
            button1 = new Button();
            panel3 = new Panel();
            button3 = new Button();
            panel4 = new Panel();
            button2 = new Button();
            pnlTemplates = new Panel();
            btnCancel = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            pnlTemplates.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(btnFirstTemplate);
            panel1.Location = new Point(36, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(268, 375);
            panel1.TabIndex = 0;
            // 
            // btnFirstTemplate
            // 
            btnFirstTemplate.BackColor = Color.FromArgb(192, 255, 255);
            btnFirstTemplate.Dock = DockStyle.Top;
            btnFirstTemplate.FlatAppearance.BorderSize = 0;
            btnFirstTemplate.FlatStyle = FlatStyle.Flat;
            btnFirstTemplate.Location = new Point(0, 0);
            btnFirstTemplate.Name = "btnFirstTemplate";
            btnFirstTemplate.Size = new Size(268, 27);
            btnFirstTemplate.TabIndex = 4;
            btnFirstTemplate.Text = "X Servers, PickUp, TeamWait";
            btnFirstTemplate.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Controls.Add(button1);
            panel2.Location = new Point(340, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(268, 375);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 255, 255);
            button1.Dock = DockStyle.Top;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(268, 27);
            button1.TabIndex = 5;
            button1.Text = "Select ";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(224, 224, 224);
            panel3.Controls.Add(button3);
            panel3.Location = new Point(36, 438);
            panel3.Name = "panel3";
            panel3.Size = new Size(268, 375);
            panel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(192, 255, 255);
            button3.Dock = DockStyle.Top;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(0, 0);
            button3.Name = "button3";
            button3.Size = new Size(268, 27);
            button3.TabIndex = 5;
            button3.Text = "Select ";
            button3.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(224, 224, 224);
            panel4.Controls.Add(button2);
            panel4.Location = new Point(340, 438);
            panel4.Name = "panel4";
            panel4.Size = new Size(268, 375);
            panel4.TabIndex = 3;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 255, 255);
            button2.Dock = DockStyle.Top;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(0, 0);
            button2.Name = "button2";
            button2.Size = new Size(268, 27);
            button2.TabIndex = 5;
            button2.Text = "Select ";
            button2.UseVisualStyleBackColor = false;
            // 
            // pnlTemplates
            // 
            pnlTemplates.BackColor = Color.Silver;
            pnlTemplates.Controls.Add(btnCancel);
            pnlTemplates.Controls.Add(panel1);
            pnlTemplates.Controls.Add(panel3);
            pnlTemplates.Controls.Add(panel4);
            pnlTemplates.Controls.Add(panel2);
            pnlTemplates.Location = new Point(12, 12);
            pnlTemplates.Name = "pnlTemplates";
            pnlTemplates.Size = new Size(646, 846);
            pnlTemplates.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.IndianRed;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(0, 820);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(646, 26);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmTemplateSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 877);
            Controls.Add(pnlTemplates);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTemplateSelection";
            Text = "frmTemplateSelection";
            Load += frmTemplateSelection_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            pnlTemplates.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel pnlTemplates;
        private Button btnFirstTemplate;
        private Button button1;
        private Button button3;
        private Button button2;
        private Button btnCancel;
    }
}