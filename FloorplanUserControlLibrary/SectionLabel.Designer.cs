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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnServerButton = new Button();
            lblSectionNumber = new Label();
            picCutOrder = new PictureBox();
            flowServers = new FlowLayoutPanel();
            flowMainContainer = new FlowLayoutPanel();
            flowParent = new FlowLayoutPanel();
            flowAccent = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picCutOrder).BeginInit();
            flowServers.SuspendLayout();
            flowMainContainer.SuspendLayout();
            flowParent.SuspendLayout();
            flowAccent.SuspendLayout();
            SuspendLayout();
            // 
            // btnServerButton
            // 
            btnServerButton.AutoSize = true;
            btnServerButton.FlatStyle = FlatStyle.Flat;
            btnServerButton.Location = new Point(0, 0);
            btnServerButton.Margin = new Padding(0);
            btnServerButton.MinimumSize = new Size(60, 23);
            btnServerButton.Name = "btnServerButton";
            btnServerButton.Size = new Size(80, 27);
            btnServerButton.TabIndex = 0;
            btnServerButton.Text = "Unassigned";
            btnServerButton.UseVisualStyleBackColor = true;
            btnServerButton.Click += btnServerButton_Click;
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Dock = DockStyle.Left;
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(0, 0);
            lblSectionNumber.Margin = new Padding(0);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(30, 27);
            lblSectionNumber.TabIndex = 1;
            lblSectionNumber.Text = "#1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picCutOrder
            // 
            picCutOrder.Dock = DockStyle.Right;
            picCutOrder.Image = Properties.Resources.Close;
            picCutOrder.Location = new Point(110, 0);
            picCutOrder.Margin = new Padding(0);
            picCutOrder.Name = "picCutOrder";
            picCutOrder.Size = new Size(30, 27);
            picCutOrder.SizeMode = PictureBoxSizeMode.Zoom;
            picCutOrder.TabIndex = 3;
            picCutOrder.TabStop = false;
            picCutOrder.Click += CycleCutOrder_Click;
            // 
            // flowServers
            // 
            flowServers.AutoSize = true;
            flowServers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowServers.BackColor = Color.FromArgb(103, 178, 216);
            flowServers.Controls.Add(btnServerButton);
            flowServers.FlowDirection = FlowDirection.TopDown;
            flowServers.Location = new Point(30, 0);
            flowServers.Margin = new Padding(0);
            flowServers.Name = "flowServers";
            flowServers.Size = new Size(80, 27);
            flowServers.TabIndex = 2;
            // 
            // flowMainContainer
            // 
            flowMainContainer.AutoSize = true;
            flowMainContainer.BackColor = Color.FromArgb(103, 178, 216);
            flowMainContainer.Controls.Add(lblSectionNumber);
            flowMainContainer.Controls.Add(flowServers);
            flowMainContainer.Controls.Add(picCutOrder);
            flowMainContainer.Location = new Point(3, 3);
            flowMainContainer.Margin = new Padding(0);
            flowMainContainer.Name = "flowMainContainer";
            flowMainContainer.Size = new Size(140, 27);
            flowMainContainer.TabIndex = 4;
            // 
            // flowParent
            // 
            flowParent.AutoSize = true;
            flowParent.Controls.Add(flowAccent);
            flowParent.Dock = DockStyle.Fill;
            flowParent.FlowDirection = FlowDirection.TopDown;
            flowParent.Location = new Point(0, 0);
            flowParent.Margin = new Padding(0);
            flowParent.Name = "flowParent";
            flowParent.Padding = new Padding(4);
            flowParent.Size = new Size(154, 41);
            flowParent.TabIndex = 4;
            // 
            // flowAccent
            // 
            flowAccent.AutoSize = true;
            flowAccent.BackColor = Color.White;
            flowAccent.Controls.Add(flowMainContainer);
            flowAccent.Location = new Point(4, 4);
            flowAccent.Margin = new Padding(0);
            flowAccent.Name = "flowAccent";
            flowAccent.Padding = new Padding(3);
            flowAccent.Size = new Size(146, 33);
            flowAccent.TabIndex = 4;
            // 
            // SectionLabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(flowParent);
            Name = "SectionLabel";
            Size = new Size(154, 41);
            ((System.ComponentModel.ISupportInitialize)picCutOrder).EndInit();
            flowServers.ResumeLayout(false);
            flowServers.PerformLayout();
            flowMainContainer.ResumeLayout(false);
            flowMainContainer.PerformLayout();
            flowParent.ResumeLayout(false);
            flowParent.PerformLayout();
            flowAccent.ResumeLayout(false);
            flowAccent.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnServerButton;
        private Label lblSectionNumber;
        private PictureBox picCutOrder;
        private FlowLayoutPanel flowServers;
        private FlowLayoutPanel flowMainContainer;
        private FlowLayoutPanel flowParent;
        private FlowLayoutPanel flowAccent;
    }
}
