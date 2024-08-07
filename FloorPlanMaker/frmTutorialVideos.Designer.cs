﻿namespace FloorPlanMakerUI
{
    partial class frmTutorialVideos
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbTutorial = new PictureBox();
            rdoGettingStarted = new RadioButton();
            rdoCreatingAShiftWalkthrough = new RadioButton();
            btnClose = new Button();
            btnNextPic = new Button();
            btnPreviousPic = new Button();
            lblIndex = new Label();
            flowThumbnails = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            panel1 = new Panel();
            pnlHighlight = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            rdoCreateNewShift = new RadioButton();
            rdoDistributeServers = new RadioButton();
            rdoCreatingSections = new RadioButton();
            rdoAssigningServersToSections = new RadioButton();
            rdoUpdatingSalesData = new RadioButton();
            rdoViewingSales = new RadioButton();
            rdoServerRatings = new RadioButton();
            rdoSavingTemplates = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pbTutorial).BeginInit();
            flowThumbnails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pbTutorial
            // 
            pbTutorial.Image = Properties.TutorialResources.Form1Overview1;
            pbTutorial.Location = new Point(12, 12);
            pbTutorial.Name = "pbTutorial";
            pbTutorial.Size = new Size(1031, 811);
            pbTutorial.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTutorial.TabIndex = 3;
            pbTutorial.TabStop = false;
            pbTutorial.Click += pbTutorial_Click;
            // 
            // rdoGettingStarted
            // 
            rdoGettingStarted.Appearance = Appearance.Button;
            rdoGettingStarted.BackColor = Color.FromArgb(100, 130, 180);
            rdoGettingStarted.Checked = true;
            rdoGettingStarted.FlatAppearance.BorderSize = 0;
            rdoGettingStarted.FlatStyle = FlatStyle.Flat;
            rdoGettingStarted.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoGettingStarted.ForeColor = Color.White;
            rdoGettingStarted.Location = new Point(3, 3);
            rdoGettingStarted.Name = "rdoGettingStarted";
            rdoGettingStarted.Size = new Size(200, 40);
            rdoGettingStarted.TabIndex = 4;
            rdoGettingStarted.TabStop = true;
            rdoGettingStarted.Text = "This Form";
            rdoGettingStarted.TextAlign = ContentAlignment.MiddleCenter;
            rdoGettingStarted.UseVisualStyleBackColor = false;
            rdoGettingStarted.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoCreatingAShiftWalkthrough
            // 
            rdoCreatingAShiftWalkthrough.Appearance = Appearance.Button;
            rdoCreatingAShiftWalkthrough.BackColor = Color.FromArgb(100, 130, 180);
            rdoCreatingAShiftWalkthrough.FlatAppearance.BorderSize = 0;
            rdoCreatingAShiftWalkthrough.FlatStyle = FlatStyle.Flat;
            rdoCreatingAShiftWalkthrough.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoCreatingAShiftWalkthrough.ForeColor = Color.White;
            rdoCreatingAShiftWalkthrough.Location = new Point(3, 49);
            rdoCreatingAShiftWalkthrough.Name = "rdoCreatingAShiftWalkthrough";
            rdoCreatingAShiftWalkthrough.Size = new Size(200, 40);
            rdoCreatingAShiftWalkthrough.TabIndex = 4;
            rdoCreatingAShiftWalkthrough.Text = "Walkthrough";
            rdoCreatingAShiftWalkthrough.TextAlign = ContentAlignment.MiddleCenter;
            rdoCreatingAShiftWalkthrough.UseVisualStyleBackColor = false;
            rdoCreatingAShiftWalkthrough.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Image = Properties.Resources.X15x;
            btnClose.Location = new Point(1217, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 23);
            btnClose.TabIndex = 5;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnNextPic
            // 
            btnNextPic.BackColor = Color.FromArgb(100, 130, 180);
            btnNextPic.FlatAppearance.BorderSize = 0;
            btnNextPic.FlatStyle = FlatStyle.Flat;
            btnNextPic.Image = Properties.Resources.forward;
            btnNextPic.Location = new Point(1222, 842);
            btnNextPic.Name = "btnNextPic";
            btnNextPic.Size = new Size(42, 133);
            btnNextPic.TabIndex = 6;
            btnNextPic.UseVisualStyleBackColor = false;
            btnNextPic.Click += btnNextPic_Click;
            // 
            // btnPreviousPic
            // 
            btnPreviousPic.BackColor = Color.FromArgb(100, 130, 180);
            btnPreviousPic.FlatAppearance.BorderSize = 0;
            btnPreviousPic.FlatStyle = FlatStyle.Flat;
            btnPreviousPic.Image = Properties.Resources.back;
            btnPreviousPic.Location = new Point(12, 842);
            btnPreviousPic.Name = "btnPreviousPic";
            btnPreviousPic.Size = new Size(42, 133);
            btnPreviousPic.TabIndex = 6;
            btnPreviousPic.UseVisualStyleBackColor = false;
            btnPreviousPic.Click += btnPreviousPic_Click;
            // 
            // lblIndex
            // 
            lblIndex.AutoSize = true;
            lblIndex.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblIndex.Location = new Point(1102, 6);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new Size(75, 37);
            lblIndex.TabIndex = 7;
            lblIndex.Text = "0 / 0";
            // 
            // flowThumbnails
            // 
            flowThumbnails.Controls.Add(pictureBox1);
            flowThumbnails.Controls.Add(pictureBox2);
            flowThumbnails.Controls.Add(pictureBox3);
            flowThumbnails.Controls.Add(pictureBox4);
            flowThumbnails.Controls.Add(pictureBox5);
            flowThumbnails.Controls.Add(pictureBox6);
            flowThumbnails.Controls.Add(pictureBox7);
            flowThumbnails.Location = new Point(60, 858);
            flowThumbnails.Name = "flowThumbnails";
            flowThumbnails.Size = new Size(1156, 135);
            flowThumbnails.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 112);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(143, 0);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(143, 112);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(286, 0);
            pictureBox3.Margin = new Padding(0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(143, 112);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(429, 0);
            pictureBox4.Margin = new Padding(0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(143, 112);
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(572, 0);
            pictureBox5.Margin = new Padding(0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(143, 112);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Location = new Point(715, 0);
            pictureBox6.Margin = new Padding(0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(143, 112);
            pictureBox6.TabIndex = 0;
            pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Location = new Point(858, 0);
            pictureBox7.Margin = new Padding(0);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(143, 112);
            pictureBox7.TabIndex = 1;
            pictureBox7.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(pnlHighlight);
            panel1.Location = new Point(60, 845);
            panel1.Name = "panel1";
            panel1.Size = new Size(1156, 10);
            panel1.TabIndex = 9;
            // 
            // pnlHighlight
            // 
            pnlHighlight.BackColor = Color.FromArgb(255, 103, 0);
            pnlHighlight.Location = new Point(3, 2);
            pnlHighlight.Name = "pnlHighlight";
            pnlHighlight.Size = new Size(143, 7);
            pnlHighlight.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.WhiteSmoke;
            flowLayoutPanel1.Controls.Add(rdoGettingStarted);
            flowLayoutPanel1.Controls.Add(rdoCreatingAShiftWalkthrough);
            flowLayoutPanel1.Controls.Add(rdoCreateNewShift);
            flowLayoutPanel1.Controls.Add(rdoDistributeServers);
            flowLayoutPanel1.Controls.Add(rdoCreatingSections);
            flowLayoutPanel1.Controls.Add(rdoAssigningServersToSections);
            flowLayoutPanel1.Controls.Add(rdoUpdatingSalesData);
            flowLayoutPanel1.Controls.Add(rdoViewingSales);
            flowLayoutPanel1.Controls.Add(rdoServerRatings);
            flowLayoutPanel1.Controls.Add(rdoSavingTemplates);
            flowLayoutPanel1.Location = new Point(1049, 46);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(209, 777);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // rdoCreateNewShift
            // 
            rdoCreateNewShift.Appearance = Appearance.Button;
            rdoCreateNewShift.BackColor = Color.FromArgb(100, 130, 180);
            rdoCreateNewShift.FlatAppearance.BorderSize = 0;
            rdoCreateNewShift.FlatStyle = FlatStyle.Flat;
            rdoCreateNewShift.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoCreateNewShift.ForeColor = Color.White;
            rdoCreateNewShift.Location = new Point(3, 95);
            rdoCreateNewShift.Name = "rdoCreateNewShift";
            rdoCreateNewShift.Size = new Size(200, 40);
            rdoCreateNewShift.TabIndex = 4;
            rdoCreateNewShift.Text = "Creating a Shift";
            rdoCreateNewShift.TextAlign = ContentAlignment.MiddleCenter;
            rdoCreateNewShift.UseVisualStyleBackColor = false;
            rdoCreateNewShift.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoDistributeServers
            // 
            rdoDistributeServers.Appearance = Appearance.Button;
            rdoDistributeServers.BackColor = Color.FromArgb(100, 130, 180);
            rdoDistributeServers.FlatAppearance.BorderSize = 0;
            rdoDistributeServers.FlatStyle = FlatStyle.Flat;
            rdoDistributeServers.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoDistributeServers.ForeColor = Color.White;
            rdoDistributeServers.Location = new Point(3, 141);
            rdoDistributeServers.Name = "rdoDistributeServers";
            rdoDistributeServers.Size = new Size(200, 40);
            rdoDistributeServers.TabIndex = 4;
            rdoDistributeServers.Text = "Distributing Servers";
            rdoDistributeServers.TextAlign = ContentAlignment.MiddleCenter;
            rdoDistributeServers.UseVisualStyleBackColor = false;
            rdoDistributeServers.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoCreatingSections
            // 
            rdoCreatingSections.Appearance = Appearance.Button;
            rdoCreatingSections.BackColor = Color.FromArgb(100, 130, 180);
            rdoCreatingSections.FlatAppearance.BorderSize = 0;
            rdoCreatingSections.FlatStyle = FlatStyle.Flat;
            rdoCreatingSections.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoCreatingSections.ForeColor = Color.White;
            rdoCreatingSections.Location = new Point(3, 187);
            rdoCreatingSections.Name = "rdoCreatingSections";
            rdoCreatingSections.Size = new Size(200, 40);
            rdoCreatingSections.TabIndex = 4;
            rdoCreatingSections.Text = "Creating Sections";
            rdoCreatingSections.TextAlign = ContentAlignment.MiddleCenter;
            rdoCreatingSections.UseVisualStyleBackColor = false;
            rdoCreatingSections.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoAssigningServersToSections
            // 
            rdoAssigningServersToSections.Appearance = Appearance.Button;
            rdoAssigningServersToSections.BackColor = Color.FromArgb(100, 130, 180);
            rdoAssigningServersToSections.FlatAppearance.BorderSize = 0;
            rdoAssigningServersToSections.FlatStyle = FlatStyle.Flat;
            rdoAssigningServersToSections.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoAssigningServersToSections.ForeColor = Color.White;
            rdoAssigningServersToSections.Location = new Point(3, 233);
            rdoAssigningServersToSections.Name = "rdoAssigningServersToSections";
            rdoAssigningServersToSections.Size = new Size(200, 40);
            rdoAssigningServersToSections.TabIndex = 4;
            rdoAssigningServersToSections.Text = "Assigning Servers";
            rdoAssigningServersToSections.TextAlign = ContentAlignment.MiddleCenter;
            rdoAssigningServersToSections.UseVisualStyleBackColor = false;
            rdoAssigningServersToSections.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoUpdatingSalesData
            // 
            rdoUpdatingSalesData.Appearance = Appearance.Button;
            rdoUpdatingSalesData.BackColor = Color.FromArgb(100, 130, 180);
            rdoUpdatingSalesData.FlatAppearance.BorderSize = 0;
            rdoUpdatingSalesData.FlatStyle = FlatStyle.Flat;
            rdoUpdatingSalesData.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoUpdatingSalesData.ForeColor = Color.White;
            rdoUpdatingSalesData.Location = new Point(3, 279);
            rdoUpdatingSalesData.Name = "rdoUpdatingSalesData";
            rdoUpdatingSalesData.Size = new Size(200, 40);
            rdoUpdatingSalesData.TabIndex = 4;
            rdoUpdatingSalesData.Text = "Updating Sales";
            rdoUpdatingSalesData.TextAlign = ContentAlignment.MiddleCenter;
            rdoUpdatingSalesData.UseVisualStyleBackColor = false;
            rdoUpdatingSalesData.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoViewingSales
            // 
            rdoViewingSales.Appearance = Appearance.Button;
            rdoViewingSales.BackColor = Color.FromArgb(100, 130, 180);
            rdoViewingSales.FlatAppearance.BorderSize = 0;
            rdoViewingSales.FlatStyle = FlatStyle.Flat;
            rdoViewingSales.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoViewingSales.ForeColor = Color.White;
            rdoViewingSales.Location = new Point(3, 325);
            rdoViewingSales.Name = "rdoViewingSales";
            rdoViewingSales.Size = new Size(200, 40);
            rdoViewingSales.TabIndex = 4;
            rdoViewingSales.Text = "Viewing Sales";
            rdoViewingSales.TextAlign = ContentAlignment.MiddleCenter;
            rdoViewingSales.UseVisualStyleBackColor = false;
            rdoViewingSales.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoServerRatings
            // 
            rdoServerRatings.Appearance = Appearance.Button;
            rdoServerRatings.BackColor = Color.FromArgb(100, 130, 180);
            rdoServerRatings.FlatAppearance.BorderSize = 0;
            rdoServerRatings.FlatStyle = FlatStyle.Flat;
            rdoServerRatings.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoServerRatings.ForeColor = Color.White;
            rdoServerRatings.Location = new Point(3, 371);
            rdoServerRatings.Name = "rdoServerRatings";
            rdoServerRatings.Size = new Size(200, 40);
            rdoServerRatings.TabIndex = 4;
            rdoServerRatings.Text = "Server Ratings";
            rdoServerRatings.TextAlign = ContentAlignment.MiddleCenter;
            rdoServerRatings.UseVisualStyleBackColor = false;
            rdoServerRatings.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // rdoSavingTemplates
            // 
            rdoSavingTemplates.Appearance = Appearance.Button;
            rdoSavingTemplates.BackColor = Color.FromArgb(100, 130, 180);
            rdoSavingTemplates.FlatAppearance.BorderSize = 0;
            rdoSavingTemplates.FlatStyle = FlatStyle.Flat;
            rdoSavingTemplates.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            rdoSavingTemplates.ForeColor = Color.White;
            rdoSavingTemplates.Location = new Point(3, 417);
            rdoSavingTemplates.Name = "rdoSavingTemplates";
            rdoSavingTemplates.Size = new Size(200, 40);
            rdoSavingTemplates.TabIndex = 4;
            rdoSavingTemplates.Text = "Saving Templates";
            rdoSavingTemplates.TextAlign = ContentAlignment.MiddleCenter;
            rdoSavingTemplates.UseVisualStyleBackColor = false;
            rdoSavingTemplates.CheckedChanged += TutorialRadioCheckChanged;
            // 
            // frmTutorialVideos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1264, 1042);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel1);
            Controls.Add(flowThumbnails);
            Controls.Add(lblIndex);
            Controls.Add(btnPreviousPic);
            Controls.Add(btnNextPic);
            Controls.Add(btnClose);
            Controls.Add(pbTutorial);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmTutorialVideos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmTutorialVideos";
            Load += frmTutorialVideos_Load;
            ((System.ComponentModel.ISupportInitialize)pbTutorial).EndInit();
            flowThumbnails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            panel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private ComboBox comboBox1;
        private Label label1;
        private PictureBox pbTutorial;
        private RadioButton rdoGettingStarted;
        private RadioButton rdoCreatingAShiftWalkthrough;
        private Button btnClose;
        private Button btnNextPic;
        private Button btnPreviousPic;
        private Label lblIndex;
        private FlowLayoutPanel flowThumbnails;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private Panel panel1;
        private Panel pnlHighlight;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton rdoCreateNewShift;
        private RadioButton rdoDistributeServers;
        private RadioButton rdoCreatingSections;
        private RadioButton rdoAssigningServersToSections;
        private RadioButton rdoUpdatingSalesData;
        private RadioButton rdoViewingSales;
        private RadioButton rdoServerRatings;
        private RadioButton rdoSavingTemplates;
    }
}