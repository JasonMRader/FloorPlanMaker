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
            flowLayoutPanel1 = new FlowLayoutPanel();
            pnlShift1 = new Panel();
            pnlShift2 = new Panel();
            pnlShift3 = new Panel();
            pnlShift4 = new Panel();
            pnlShift5 = new Panel();
            btnServer = new Button();
            lblDescription = new Label();
            lblOutsidePercentage = new Label();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.FromArgb(225, 225, 225);
            flowLayoutPanel1.Controls.Add(pnlShift1);
            flowLayoutPanel1.Controls.Add(pnlShift2);
            flowLayoutPanel1.Controls.Add(pnlShift3);
            flowLayoutPanel1.Controls.Add(pnlShift4);
            flowLayoutPanel1.Controls.Add(pnlShift5);
            flowLayoutPanel1.Location = new Point(3, 30);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(3, 3, 0, 0);
            flowLayoutPanel1.Size = new Size(196, 50);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // pnlShift1
            // 
            pnlShift1.BackColor = Color.WhiteSmoke;
            pnlShift1.Location = new Point(6, 6);
            pnlShift1.Name = "pnlShift1";
            pnlShift1.Size = new Size(32, 39);
            pnlShift1.TabIndex = 0;
            // 
            // pnlShift2
            // 
            pnlShift2.BackColor = Color.WhiteSmoke;
            pnlShift2.Location = new Point(44, 6);
            pnlShift2.Name = "pnlShift2";
            pnlShift2.Size = new Size(32, 39);
            pnlShift2.TabIndex = 1;
            // 
            // pnlShift3
            // 
            pnlShift3.BackColor = Color.WhiteSmoke;
            pnlShift3.Location = new Point(82, 6);
            pnlShift3.Name = "pnlShift3";
            pnlShift3.Size = new Size(32, 39);
            pnlShift3.TabIndex = 2;
            // 
            // pnlShift4
            // 
            pnlShift4.BackColor = Color.WhiteSmoke;
            pnlShift4.Location = new Point(120, 6);
            pnlShift4.Name = "pnlShift4";
            pnlShift4.Size = new Size(32, 39);
            pnlShift4.TabIndex = 3;
            // 
            // pnlShift5
            // 
            pnlShift5.BackColor = Color.WhiteSmoke;
            pnlShift5.Location = new Point(158, 6);
            pnlShift5.Name = "pnlShift5";
            pnlShift5.Size = new Size(32, 39);
            pnlShift5.TabIndex = 4;
            // 
            // btnServer
            // 
            btnServer.BackColor = Color.FromArgb(100, 130, 180);
            btnServer.FlatAppearance.BorderSize = 0;
            btnServer.FlatStyle = FlatStyle.Flat;
            btnServer.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnServer.ForeColor = Color.White;
            btnServer.Location = new Point(0, 0);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(300, 30);
            btnServer.TabIndex = 2;
            btnServer.Text = "Adrianna";
            btnServer.UseVisualStyleBackColor = false;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(199, 33);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(98, 15);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Last 20 PM shifts:";
            // 
            // lblOutsidePercentage
            // 
            lblOutsidePercentage.AutoSize = true;
            lblOutsidePercentage.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblOutsidePercentage.Location = new Point(199, 48);
            lblOutsidePercentage.Name = "lblOutsidePercentage";
            lblOutsidePercentage.Size = new Size(94, 20);
            lblOutsidePercentage.TabIndex = 4;
            lblOutsidePercentage.Text = "55% Outside";
            // 
            // ServerHistoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            Controls.Add(lblOutsidePercentage);
            Controls.Add(lblDescription);
            Controls.Add(btnServer);
            Controls.Add(flowLayoutPanel1);
            Name = "ServerHistoryControl";
            Size = new Size(300, 84);
            Load += ServerHistoryControl_Load;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel pnlShift1;
        private Panel pnlShift2;
        private Panel pnlShift3;
        private Panel pnlShift4;
        private Panel pnlShift5;
        private Button btnServer;
        private Label lblDescription;
        private Label lblOutsidePercentage;
        private CheckBox cbTimeSpan;
        private NumericUpDown numericUpDown1;
    }
}
