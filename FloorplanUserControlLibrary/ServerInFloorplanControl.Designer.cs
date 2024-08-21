namespace FloorplanUserControlLibrary
{
    partial class ServerInFloorplanControl
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
            btnServer = new Button();
            flowShiftDisplay = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnServer
            // 
            btnServer.BackColor = Color.FromArgb(100, 130, 180);
            btnServer.Dock = DockStyle.Top;
            btnServer.FlatAppearance.BorderSize = 0;
            btnServer.FlatStyle = FlatStyle.Flat;
            btnServer.Font = new Font("Segoe UI Semibold", 12.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnServer.ForeColor = Color.White;
            btnServer.Location = new Point(0, 0);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(281, 27);
            btnServer.TabIndex = 0;
            btnServer.Text = "Adrianna";
            btnServer.UseVisualStyleBackColor = false;
            // 
            // flowShiftDisplay
            // 
            flowShiftDisplay.BackColor = Color.FromArgb(225, 225, 225);
            flowShiftDisplay.Dock = DockStyle.Left;
            flowShiftDisplay.Location = new Point(0, 27);
            flowShiftDisplay.Name = "flowShiftDisplay";
            flowShiftDisplay.Padding = new Padding(3, 3, 0, 0);
            flowShiftDisplay.Size = new Size(196, 62);
            flowShiftDisplay.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(209, 36);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 3;
            label1.Text = "Sct: 8";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(209, 51);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 3;
            label2.Text = "Cls: 7";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(209, 66);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "Tw: 9";
            // 
            // ServerInFloorplanControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowShiftDisplay);
            Controls.Add(btnServer);
            Name = "ServerInFloorplanControl";
            Size = new Size(281, 89);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnServer;
        private FlowLayoutPanel flowShiftDisplay;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
