namespace FloorplanUserControlLibrary
{
    partial class ServerHistoryControl
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
            lblName = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.BackColor = Color.FromArgb(100, 130, 180);
            lblName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(0, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(191, 27);
            lblName.TabIndex = 0;
            lblName.Text = "Adrianna P.";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.FromArgb(225, 225, 225);
            flowLayoutPanel1.Location = new Point(3, 30);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(188, 42);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // ServerHistoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(lblName);
            Name = "ServerHistoryControl";
            Size = new Size(310, 75);
            ResumeLayout(false);
        }

        #endregion

        private Label lblName;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
