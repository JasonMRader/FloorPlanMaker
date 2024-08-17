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
            pnlAccent = new Panel();
            flowMainContainer = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picCutOrder).BeginInit();
            flowServers.SuspendLayout();
            pnlAccent.SuspendLayout();
            flowMainContainer.SuspendLayout();
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
            picCutOrder.Image = Properties.Resources.Close;
            picCutOrder.Location = new Point(110, 0);
            picCutOrder.Margin = new Padding(0);
            picCutOrder.Name = "picCutOrder";
            picCutOrder.Size = new Size(30, 27);
            picCutOrder.SizeMode = PictureBoxSizeMode.Zoom;
            picCutOrder.TabIndex = 3;
            picCutOrder.TabStop = false;
            picCutOrder.Click += CycleCutOrder;
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
            // pnlAccent
            // 
            pnlAccent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlAccent.BackColor = Color.Gray;
            pnlAccent.Controls.Add(flowMainContainer);
            pnlAccent.Location = new Point(4, 4);
            pnlAccent.Name = "pnlAccent";
            pnlAccent.Size = new Size(145, 32);
            pnlAccent.TabIndex = 3;
            pnlAccent.Paint += pnlAccent_Paint;
            // 
            // flowMainContainer
            // 
            flowMainContainer.BackColor = Color.FromArgb(103, 178, 216);
            flowMainContainer.Controls.Add(lblSectionNumber);
            flowMainContainer.Controls.Add(flowServers);
            flowMainContainer.Controls.Add(picCutOrder);
            flowMainContainer.Location = new Point(2, 2);
            flowMainContainer.Name = "flowMainContainer";
            flowMainContainer.Size = new Size(140, 27);
            flowMainContainer.TabIndex = 4;
            // 
            // SectionLabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(pnlAccent);
            Name = "SectionLabel";
            Size = new Size(152, 39);
            ((System.ComponentModel.ISupportInitialize)picCutOrder).EndInit();
            flowServers.ResumeLayout(false);
            flowServers.PerformLayout();
            pnlAccent.ResumeLayout(false);
            flowMainContainer.ResumeLayout(false);
            flowMainContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnServerButton;
        private Label lblSectionNumber;
        private PictureBox picCutOrder;
        private FlowLayoutPanel flowServers;
        private Panel pnlAccent;
        private FlowLayoutPanel flowMainContainer;
    }
}
