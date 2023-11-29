namespace FloorplanUserControlLibrary
{
    partial class SectionPanelControl
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
                Section.RemoveObserver(this);
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
            lblCovers = new Label();
            lblSales = new Label();
            picClearSection = new PictureBox();
            lblDisplay = new Label();
            cbSectionSelect = new CheckBox();
            picSetTeamWait = new PictureBox();
            picMinusOneServer = new PictureBox();
            picPlusOneServer = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picClearSection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetTeamWait).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMinusOneServer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPlusOneServer).BeginInit();
            SuspendLayout();
            // 
            // lblCovers
            // 
            lblCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblCovers.Location = new Point(110, 1);
            lblCovers.Margin = new Padding(0);
            lblCovers.Name = "lblCovers";
            lblCovers.Size = new Size(52, 25);
            lblCovers.TabIndex = 1;
            lblCovers.Text = "10";
            lblCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSales
            // 
            lblSales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSales.Location = new Point(162, 2);
            lblSales.Margin = new Padding(0);
            lblSales.Name = "lblSales";
            lblSales.Size = new Size(68, 23);
            lblSales.TabIndex = 1;
            lblSales.Text = "$1023";
            lblSales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picClearSection
            // 
            picClearSection.BackColor = Color.FromArgb(190, 80, 70);
            picClearSection.Image = Properties.Resources.erase;
            picClearSection.Location = new Point(285, 0);
            picClearSection.Margin = new Padding(0);
            picClearSection.Name = "picClearSection";
            picClearSection.Size = new Size(40, 25);
            picClearSection.SizeMode = PictureBoxSizeMode.Zoom;
            picClearSection.TabIndex = 2;
            picClearSection.TabStop = false;
            picClearSection.Click += picClearSection_Click;
            // 
            // lblDisplay
            // 
            lblDisplay.BorderStyle = BorderStyle.FixedSingle;
            lblDisplay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDisplay.Location = new Point(0, 25);
            lblDisplay.Margin = new Padding(0);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(325, 25);
            lblDisplay.TabIndex = 1;
            lblDisplay.Text = "Unassigned";
            lblDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbSectionSelect
            // 
            cbSectionSelect.Appearance = Appearance.Button;
            cbSectionSelect.FlatAppearance.BorderSize = 0;
            cbSectionSelect.FlatStyle = FlatStyle.Flat;
            cbSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbSectionSelect.Location = new Point(0, -2);
            cbSectionSelect.Name = "cbSectionSelect";
            cbSectionSelect.Size = new Size(104, 25);
            cbSectionSelect.TabIndex = 3;
            cbSectionSelect.Text = "Section 1";
            cbSectionSelect.TextAlign = ContentAlignment.MiddleCenter;
            cbSectionSelect.UseVisualStyleBackColor = true;
            cbSectionSelect.CheckedChanged += cbSectionSelect_CheckedChanged;
            // 
            // picSetTeamWait
            // 
            picSetTeamWait.BackColor = Color.FromArgb(120, 180, 120);
            picSetTeamWait.BackgroundImageLayout = ImageLayout.None;
            picSetTeamWait.Image = Properties.Resources.waiter;
            picSetTeamWait.Location = new Point(245, 0);
            picSetTeamWait.Margin = new Padding(0);
            picSetTeamWait.Name = "picSetTeamWait";
            picSetTeamWait.Size = new Size(40, 25);
            picSetTeamWait.SizeMode = PictureBoxSizeMode.Zoom;
            picSetTeamWait.TabIndex = 2;
            picSetTeamWait.TabStop = false;
            picSetTeamWait.Click += picTeamWait_Click;
            // 
            // picMinusOneServer
            // 
            picMinusOneServer.BackColor = Color.FromArgb(254, 185, 95);
            picMinusOneServer.BackgroundImageLayout = ImageLayout.None;
            picMinusOneServer.Image = Properties.Resources.RemovePerson;
            picMinusOneServer.Location = new Point(285, 25);
            picMinusOneServer.Margin = new Padding(0);
            picMinusOneServer.Name = "picMinusOneServer";
            picMinusOneServer.Size = new Size(40, 25);
            picMinusOneServer.SizeMode = PictureBoxSizeMode.Zoom;
            picMinusOneServer.TabIndex = 2;
            picMinusOneServer.TabStop = false;
            picMinusOneServer.Visible = false;
            // 
            // picPlusOneServer
            // 
            picPlusOneServer.BackColor = Color.FromArgb(120, 180, 120);
            picPlusOneServer.BackgroundImageLayout = ImageLayout.None;
            picPlusOneServer.Image = Properties.Resources.AddServer;
            picPlusOneServer.Location = new Point(245, 25);
            picPlusOneServer.Margin = new Padding(0);
            picPlusOneServer.Name = "picPlusOneServer";
            picPlusOneServer.Size = new Size(40, 25);
            picPlusOneServer.SizeMode = PictureBoxSizeMode.Zoom;
            picPlusOneServer.TabIndex = 2;
            picPlusOneServer.TabStop = false;
            picPlusOneServer.Visible = false;
            // 
            // SectionPanelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(cbSectionSelect);
            Controls.Add(picPlusOneServer);
            Controls.Add(picMinusOneServer);
            Controls.Add(picSetTeamWait);
            Controls.Add(picClearSection);
            Controls.Add(lblSales);
            Controls.Add(lblDisplay);
            Controls.Add(lblCovers);
            Name = "SectionPanelControl";
            Size = new Size(325, 50);
            ((System.ComponentModel.ISupportInitialize)picClearSection).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetTeamWait).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMinusOneServer).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPlusOneServer).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblCovers;
        private Label lblSales;
        private PictureBox picClearSection;
        private PictureBox picSetTeamWait;
        private Label lblDisplay;
        private CheckBox cbSectionSelect;
        private PictureBox picMinusOneServer;
        private PictureBox picPlusOneServer;
        //private PictureBox picSetTeamWait;
    }
}
