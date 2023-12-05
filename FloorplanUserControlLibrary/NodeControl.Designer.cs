namespace FloorplanUserControlLibrary
{
    partial class NodeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblNodeNumber = new Label();
            lblName = new Label();
            SuspendLayout();
            // 
            // lblNodeNumber
            // 
            lblNodeNumber.Dock = DockStyle.Right;
            lblNodeNumber.Location = new Point(15, 0);
            lblNodeNumber.Name = "lblNodeNumber";
            lblNodeNumber.Size = new Size(15, 15);
            lblNodeNumber.TabIndex = 1;
            lblNodeNumber.Text = "1";
            lblNodeNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Right;
            lblName.Location = new Point(0, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(15, 15);
            lblName.TabIndex = 2;
            lblName.Text = "\"\"";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NodeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblName);
            Controls.Add(lblNodeNumber);
            Name = "NodeControl";
            Size = new Size(30, 15);
            ResumeLayout(false);
        }

        #endregion
        private Label lblNodeNumber;
        private Label lblName;
    }
}
