namespace FloorPlanMakerUI
{
    partial class frmReporting
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
            rdoBug = new RadioButton();
            rdoFeature = new RadioButton();
            rdoOther = new RadioButton();
            textBox1 = new TextBox();
            lblBugDescription = new Label();
            btnSend = new Button();
            SuspendLayout();
            // 
            // rdoBug
            // 
            rdoBug.Appearance = Appearance.Button;
            rdoBug.BackColor = Color.FromArgb(100, 130, 180);
            rdoBug.FlatAppearance.BorderSize = 0;
            rdoBug.FlatStyle = FlatStyle.Flat;
            rdoBug.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoBug.ForeColor = Color.White;
            rdoBug.Location = new Point(29, 12);
            rdoBug.Name = "rdoBug";
            rdoBug.Size = new Size(200, 30);
            rdoBug.TabIndex = 0;
            rdoBug.TabStop = true;
            rdoBug.Text = "Report Bug";
            rdoBug.TextAlign = ContentAlignment.MiddleCenter;
            rdoBug.UseVisualStyleBackColor = false;
            // 
            // rdoFeature
            // 
            rdoFeature.Appearance = Appearance.Button;
            rdoFeature.BackColor = Color.FromArgb(100, 130, 180);
            rdoFeature.FlatAppearance.BorderSize = 0;
            rdoFeature.FlatStyle = FlatStyle.Flat;
            rdoFeature.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoFeature.ForeColor = Color.White;
            rdoFeature.Location = new Point(296, 12);
            rdoFeature.Name = "rdoFeature";
            rdoFeature.Size = new Size(200, 30);
            rdoFeature.TabIndex = 0;
            rdoFeature.TabStop = true;
            rdoFeature.Text = "Request Feature";
            rdoFeature.TextAlign = ContentAlignment.MiddleCenter;
            rdoFeature.UseVisualStyleBackColor = false;
            // 
            // rdoOther
            // 
            rdoOther.Appearance = Appearance.Button;
            rdoOther.BackColor = Color.FromArgb(100, 130, 180);
            rdoOther.FlatAppearance.BorderSize = 0;
            rdoOther.FlatStyle = FlatStyle.Flat;
            rdoOther.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdoOther.ForeColor = Color.White;
            rdoOther.Location = new Point(563, 12);
            rdoOther.Name = "rdoOther";
            rdoOther.Size = new Size(200, 30);
            rdoOther.TabIndex = 0;
            rdoOther.TabStop = true;
            rdoOther.Text = "Other";
            rdoOther.TextAlign = ContentAlignment.MiddleCenter;
            rdoOther.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(29, 118);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(734, 285);
            textBox1.TabIndex = 1;
            // 
            // lblBugDescription
            // 
            lblBugDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblBugDescription.Location = new Point(29, 61);
            lblBugDescription.Name = "lblBugDescription";
            lblBugDescription.Size = new Size(734, 54);
            lblBugDescription.TabIndex = 2;
            lblBugDescription.Text = "When reporting a bug, it is VERY helpful to be as detailed as possible. What did you do before it happened, what were the effects, so on.";
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(100, 130, 180);
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(29, 437);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(734, 41);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            // 
            // frmReporting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 504);
            Controls.Add(btnSend);
            Controls.Add(lblBugDescription);
            Controls.Add(textBox1);
            Controls.Add(rdoOther);
            Controls.Add(rdoFeature);
            Controls.Add(rdoBug);
            Name = "frmReporting";
            Text = "Send Report";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rdoBug;
        private RadioButton rdoFeature;
        private RadioButton rdoOther;
        private TextBox textBox1;
        private Label lblBugDescription;
        private Button btnSend;
    }
}