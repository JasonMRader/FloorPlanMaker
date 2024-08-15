namespace FloorplanUserControlLibrary
{
    partial class SectionInfoPanel
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
            lblSectionNumber = new Label();
            lblCovers = new Label();
            lblSales = new Label();
            lblSalesDif = new Label();
            picSetTeamWait = new PictureBox();
            flowServers = new FlowLayoutPanel();
            pnlMainContainer = new Panel();
            pnlHighlightBuffer = new Panel();
            ((System.ComponentModel.ISupportInitialize)picSetTeamWait).BeginInit();
            pnlMainContainer.SuspendLayout();
            pnlHighlightBuffer.SuspendLayout();
            SuspendLayout();
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(0, 0);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(45, 21);
            lblSectionNumber.TabIndex = 0;
            lblSectionNumber.Text = "BAR";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCovers
            // 
            lblCovers.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCovers.Location = new Point(51, 0);
            lblCovers.Name = "lblCovers";
            lblCovers.Size = new Size(35, 21);
            lblCovers.TabIndex = 0;
            lblCovers.Text = "333";
            lblCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSales
            // 
            lblSales.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSales.Location = new Point(98, 0);
            lblSales.Name = "lblSales";
            lblSales.Size = new Size(59, 21);
            lblSales.TabIndex = 0;
            lblSales.Text = "$3,333";
            lblSales.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSalesDif
            // 
            lblSalesDif.BackColor = Color.FromArgb(120, 180, 120);
            lblSalesDif.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSalesDif.Location = new Point(169, 0);
            lblSalesDif.Name = "lblSalesDif";
            lblSalesDif.Size = new Size(59, 21);
            lblSalesDif.TabIndex = 0;
            lblSalesDif.Text = "-$3,333";
            lblSalesDif.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picSetTeamWait
            // 
            picSetTeamWait.Image = Properties.Resources.waiters_28;
            picSetTeamWait.InitialImage = Properties.Resources.waiters;
            picSetTeamWait.Location = new Point(235, 0);
            picSetTeamWait.Name = "picSetTeamWait";
            picSetTeamWait.Size = new Size(30, 21);
            picSetTeamWait.SizeMode = PictureBoxSizeMode.Zoom;
            picSetTeamWait.TabIndex = 1;
            picSetTeamWait.TabStop = false;
            // 
            // flowServers
            // 
            flowServers.Location = new Point(0, 21);
            flowServers.Name = "flowServers";
            flowServers.Size = new Size(265, 28);
            flowServers.TabIndex = 2;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.FromArgb(103, 178, 216);
            pnlMainContainer.Controls.Add(lblSectionNumber);
            pnlMainContainer.Controls.Add(flowServers);
            pnlMainContainer.Controls.Add(lblCovers);
            pnlMainContainer.Controls.Add(picSetTeamWait);
            pnlMainContainer.Controls.Add(lblSales);
            pnlMainContainer.Controls.Add(lblSalesDif);
            pnlMainContainer.Location = new Point(3, 3);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(265, 49);
            pnlMainContainer.TabIndex = 3;
            // 
            // pnlHighlightBuffer
            // 
            pnlHighlightBuffer.BackColor = Color.WhiteSmoke;
            pnlHighlightBuffer.Controls.Add(pnlMainContainer);
            pnlHighlightBuffer.Location = new Point(5, 5);
            pnlHighlightBuffer.Name = "pnlHighlightBuffer";
            pnlHighlightBuffer.Size = new Size(271, 55);
            pnlHighlightBuffer.TabIndex = 4;
            // 
            // SectionInfoPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(pnlHighlightBuffer);
            Name = "SectionInfoPanel";
            Size = new Size(281, 65);
            ((System.ComponentModel.ISupportInitialize)picSetTeamWait).EndInit();
            pnlMainContainer.ResumeLayout(false);
            pnlHighlightBuffer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblSectionNumber;
        private Label lblCovers;
        private Label lblSales;
        private Label lblSalesDif;
        private PictureBox picSetTeamWait;
        private FlowLayoutPanel flowServers;
        private Panel pnlMainContainer;
        private Panel pnlHighlightBuffer;
    }
}
