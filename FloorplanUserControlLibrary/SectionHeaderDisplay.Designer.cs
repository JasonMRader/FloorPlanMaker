namespace FloorplanUserControlLibrary
{
    partial class SectionHeaderDisplay
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
            btnAssignedServer = new Button();
            btnClearSection = new Button();
            btnTeamWaitToggle = new Button();
            pbTotalCovers = new PictureBox();
            lblTotalCovers = new Label();
            pbAverageSales = new PictureBox();
            lblAverageSales = new Label();
            pbSalesDifference = new PictureBox();
            lblSalesDifference = new Label();
            pbCoversDifference = new PictureBox();
            lblCoverDifference = new Label();
            ((System.ComponentModel.ISupportInitialize)pbTotalCovers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbAverageSales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSalesDifference).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCoversDifference).BeginInit();
            SuspendLayout();
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Dock = DockStyle.Left;
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(0, 0);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(33, 40);
            lblSectionNumber.TabIndex = 0;
            lblSectionNumber.Text = "#1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAssignedServer
            // 
            btnAssignedServer.BackColor = SystemColors.ButtonShadow;
            btnAssignedServer.FlatAppearance.BorderSize = 0;
            btnAssignedServer.FlatStyle = FlatStyle.Flat;
            btnAssignedServer.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnAssignedServer.Location = new Point(33, 5);
            btnAssignedServer.Name = "btnAssignedServer";
            btnAssignedServer.Size = new Size(192, 30);
            btnAssignedServer.TabIndex = 1;
            btnAssignedServer.Text = "Unassigned";
            btnAssignedServer.UseVisualStyleBackColor = false;
            btnAssignedServer.Click += btnAssignedServer_Click;
            // 
            // btnClearSection
            // 
            btnClearSection.Dock = DockStyle.Right;
            btnClearSection.FlatAppearance.BorderSize = 0;
            btnClearSection.FlatStyle = FlatStyle.Flat;
            btnClearSection.Image = Properties.Resources.erase_Small;
            btnClearSection.Location = new Point(624, 0);
            btnClearSection.Name = "btnClearSection";
            btnClearSection.Size = new Size(42, 40);
            btnClearSection.TabIndex = 3;
            btnClearSection.UseVisualStyleBackColor = true;
            // 
            // btnTeamWaitToggle
            // 
            btnTeamWaitToggle.Dock = DockStyle.Right;
            btnTeamWaitToggle.FlatAppearance.BorderSize = 0;
            btnTeamWaitToggle.FlatStyle = FlatStyle.Flat;
            btnTeamWaitToggle.Image = Properties.Resources.waiters_28;
            btnTeamWaitToggle.Location = new Point(586, 0);
            btnTeamWaitToggle.Name = "btnTeamWaitToggle";
            btnTeamWaitToggle.Size = new Size(38, 40);
            btnTeamWaitToggle.TabIndex = 4;
            btnTeamWaitToggle.UseVisualStyleBackColor = true;
            // 
            // pbTotalCovers
            // 
            pbTotalCovers.Image = Properties.Resources.covers;
            pbTotalCovers.Location = new Point(231, 5);
            pbTotalCovers.Margin = new Padding(3, 3, 0, 3);
            pbTotalCovers.Name = "pbTotalCovers";
            pbTotalCovers.Size = new Size(28, 30);
            pbTotalCovers.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTotalCovers.TabIndex = 6;
            pbTotalCovers.TabStop = false;
            // 
            // lblTotalCovers
            // 
            lblTotalCovers.BackColor = Color.FromArgb(224, 224, 224);
            lblTotalCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalCovers.Location = new Point(259, 5);
            lblTotalCovers.Margin = new Padding(0, 0, 3, 0);
            lblTotalCovers.Name = "lblTotalCovers";
            lblTotalCovers.Size = new Size(40, 30);
            lblTotalCovers.TabIndex = 7;
            lblTotalCovers.Text = "77";
            lblTotalCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbAverageSales
            // 
            pbAverageSales.Image = Properties.Resources.SalesPerPerson_28px;
            pbAverageSales.Location = new Point(305, 6);
            pbAverageSales.Margin = new Padding(3, 3, 0, 3);
            pbAverageSales.Name = "pbAverageSales";
            pbAverageSales.Size = new Size(28, 30);
            pbAverageSales.SizeMode = PictureBoxSizeMode.StretchImage;
            pbAverageSales.TabIndex = 6;
            pbAverageSales.TabStop = false;
            // 
            // lblAverageSales
            // 
            lblAverageSales.BackColor = Color.FromArgb(224, 224, 224);
            lblAverageSales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblAverageSales.Location = new Point(333, 5);
            lblAverageSales.Margin = new Padding(0, 0, 3, 0);
            lblAverageSales.Name = "lblAverageSales";
            lblAverageSales.Size = new Size(69, 30);
            lblAverageSales.TabIndex = 7;
            lblAverageSales.Text = "$2,777";
            lblAverageSales.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbSalesDifference
            // 
            pbSalesDifference.Image = Properties.Resources.equality;
            pbSalesDifference.Location = new Point(484, 5);
            pbSalesDifference.Margin = new Padding(3, 3, 0, 3);
            pbSalesDifference.Name = "pbSalesDifference";
            pbSalesDifference.Size = new Size(28, 30);
            pbSalesDifference.SizeMode = PictureBoxSizeMode.StretchImage;
            pbSalesDifference.TabIndex = 6;
            pbSalesDifference.TabStop = false;
            // 
            // lblSalesDifference
            // 
            lblSalesDifference.BackColor = Color.FromArgb(224, 224, 224);
            lblSalesDifference.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSalesDifference.Location = new Point(512, 5);
            lblSalesDifference.Margin = new Padding(0, 0, 3, 0);
            lblSalesDifference.Name = "lblSalesDifference";
            lblSalesDifference.Size = new Size(64, 30);
            lblSalesDifference.TabIndex = 7;
            lblSalesDifference.Text = "$2,777";
            lblSalesDifference.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbCoversDifference
            // 
            pbCoversDifference.Image = Properties.Resources.CoversUpBlack;
            pbCoversDifference.Location = new Point(410, 5);
            pbCoversDifference.Margin = new Padding(3, 3, 0, 3);
            pbCoversDifference.Name = "pbCoversDifference";
            pbCoversDifference.Size = new Size(28, 30);
            pbCoversDifference.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCoversDifference.TabIndex = 6;
            pbCoversDifference.TabStop = false;
            // 
            // lblCoverDifference
            // 
            lblCoverDifference.BackColor = Color.FromArgb(224, 224, 224);
            lblCoverDifference.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblCoverDifference.Location = new Point(440, 5);
            lblCoverDifference.Margin = new Padding(0, 0, 3, 0);
            lblCoverDifference.Name = "lblCoverDifference";
            lblCoverDifference.Size = new Size(38, 30);
            lblCoverDifference.TabIndex = 7;
            lblCoverDifference.Text = "77";
            lblCoverDifference.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SectionHeaderDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblSalesDifference);
            Controls.Add(pbSalesDifference);
            Controls.Add(lblAverageSales);
            Controls.Add(pbAverageSales);
            Controls.Add(lblCoverDifference);
            Controls.Add(lblTotalCovers);
            Controls.Add(pbCoversDifference);
            Controls.Add(pbTotalCovers);
            Controls.Add(btnTeamWaitToggle);
            Controls.Add(btnClearSection);
            Controls.Add(btnAssignedServer);
            Controls.Add(lblSectionNumber);
            Name = "SectionHeaderDisplay";
            Size = new Size(666, 40);
            Load += SectionHeaderDisplay_Load;
            ((System.ComponentModel.ISupportInitialize)pbTotalCovers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbAverageSales).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSalesDifference).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCoversDifference).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblSectionNumber;
        private Button btnAssignedServer;
        private Button btnClearSection;
        private Button btnTeamWaitToggle;
        private PictureBox pbTotalCovers;
        private Label lblTotalCovers;
        private PictureBox pbAverageSales;
        private Label lblAverageSales;
        private PictureBox pbSalesDifference;
        private Label lblSalesDifference;
        private PictureBox pbCoversDifference;
        private Label lblCoverDifference;
    }
}
