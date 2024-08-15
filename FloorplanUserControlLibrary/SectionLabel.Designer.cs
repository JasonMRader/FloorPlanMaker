namespace FloorplanUserControlLibrary
{
    partial class SectionLabel
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
            button1 = new Button();
            label1 = new Label();
            pnlMainContainer = new Panel();
            pnlAccent = new Panel();
            flowServers = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            pnlMainContainer.SuspendLayout();
            pnlAccent.SuspendLayout();
            flowServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(150, 23);
            button1.TabIndex = 0;
            button1.Text = "Unassigned";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(25, 21);
            label1.TabIndex = 1;
            label1.Text = "#1";
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.AutoSize = true;
            pnlMainContainer.Controls.Add(pictureBox1);
            pnlMainContainer.Controls.Add(flowServers);
            pnlMainContainer.Controls.Add(label1);
            pnlMainContainer.Location = new Point(16, 18);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(247, 59);
            pnlMainContainer.TabIndex = 2;
            // 
            // pnlAccent
            // 
            pnlAccent.Controls.Add(pnlMainContainer);
            pnlAccent.Location = new Point(18, 3);
            pnlAccent.Name = "pnlAccent";
            pnlAccent.Size = new Size(283, 94);
            pnlAccent.TabIndex = 3;
            // 
            // flowServers
            // 
            flowServers.BackColor = Color.Silver;
            flowServers.Controls.Add(button1);
            flowServers.FlowDirection = FlowDirection.TopDown;
            flowServers.Location = new Point(43, 9);
            flowServers.Name = "flowServers";
            flowServers.Size = new Size(158, 38);
            flowServers.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Close;
            pictureBox1.Location = new Point(207, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // SectionLabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(pnlAccent);
            Name = "SectionLabel";
            Size = new Size(656, 243);
            pnlMainContainer.ResumeLayout(false);
            pnlMainContainer.PerformLayout();
            pnlAccent.ResumeLayout(false);
            pnlAccent.PerformLayout();
            flowServers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label label1;
        private Panel pnlMainContainer;
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowServers;
        private Panel pnlAccent;
    }
}
