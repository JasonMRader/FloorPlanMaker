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
            picTeamWait = new PictureBox();
            lblServerNames = new Label();
            cbSectionSelect = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)picClearSection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTeamWait).BeginInit();
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
            // picTeamWait
            // 
            picTeamWait.BackColor = Color.FromArgb(120, 180, 120);
            picTeamWait.Image = Properties.Resources.waiter;
            picTeamWait.Location = new Point(285, 26);
            picTeamWait.Margin = new Padding(0);
            picTeamWait.Name = "picTeamWait";
            picTeamWait.Size = new Size(40, 25);
            picTeamWait.SizeMode = PictureBoxSizeMode.Zoom;
            picTeamWait.TabIndex = 2;
            picTeamWait.TabStop = false;
            picTeamWait.Click += picTeamWait_Click;
            // 
            // lblServerNames
            // 
            lblServerNames.BorderStyle = BorderStyle.FixedSingle;
            lblServerNames.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerNames.Location = new Point(0, 25);
            lblServerNames.Margin = new Padding(0);
            lblServerNames.Name = "lblServerNames";
            lblServerNames.Size = new Size(285, 25);
            lblServerNames.TabIndex = 1;
            lblServerNames.Text = "Unassigned";
            lblServerNames.TextAlign = ContentAlignment.MiddleCenter;
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
            // SectionPanelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(cbSectionSelect);
            Controls.Add(picTeamWait);
            Controls.Add(picClearSection);
            Controls.Add(lblSales);
            Controls.Add(lblServerNames);
            Controls.Add(lblCovers);
            Name = "SectionPanelControl";
            Size = new Size(325, 50);
            ((System.ComponentModel.ISupportInitialize)picClearSection).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTeamWait).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblCovers;
        private Label lblSales;
        private PictureBox picClearSection;
        private PictureBox picTeamWait;
        private Label lblServerNames;
        private CheckBox cbSectionSelect;
    }
}
