﻿namespace FloorplanUserControlLibrary
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
            pnlMainContainer = new Panel();
            picCutOrder = new PictureBox();
            flowServers = new FlowLayoutPanel();
            pnlAccent = new Panel();
            pnlMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCutOrder).BeginInit();
            flowServers.SuspendLayout();
            pnlAccent.SuspendLayout();
            SuspendLayout();
            // 
            // btnServerButton
            // 
            btnServerButton.AutoSize = true;
            btnServerButton.FlatStyle = FlatStyle.Flat;
            btnServerButton.Location = new Point(3, 3);
            btnServerButton.MinimumSize = new Size(100, 23);
            btnServerButton.Name = "btnServerButton";
            btnServerButton.Size = new Size(100, 27);
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
            lblSectionNumber.Size = new Size(30, 30);
            lblSectionNumber.TabIndex = 1;
            lblSectionNumber.Text = "#1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.FromArgb(103, 178, 216);
            pnlMainContainer.Controls.Add(picCutOrder);
            pnlMainContainer.Controls.Add(flowServers);
            pnlMainContainer.Controls.Add(lblSectionNumber);
            pnlMainContainer.Location = new Point(2, 2);
            pnlMainContainer.Margin = new Padding(1);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(175, 36);
            pnlMainContainer.TabIndex = 2;
            // 
            // picCutOrder
            // 
            picCutOrder.Image = Properties.Resources.Close;
            picCutOrder.Location = new Point(136, 0);
            picCutOrder.Name = "picCutOrder";
            picCutOrder.Size = new Size(36, 30);
            picCutOrder.SizeMode = PictureBoxSizeMode.Zoom;
            picCutOrder.TabIndex = 3;
            picCutOrder.TabStop = false;
            picCutOrder.Click += CycleCutOrder;
            // 
            // flowServers
            // 
            flowServers.AutoSize = true;
            flowServers.BackColor = Color.Silver;
            flowServers.Controls.Add(btnServerButton);
            flowServers.FlowDirection = FlowDirection.TopDown;
            flowServers.Location = new Point(30, 0);
            flowServers.Name = "flowServers";
            flowServers.Size = new Size(106, 33);
            flowServers.TabIndex = 2;
            // 
            // pnlAccent
            // 
            pnlAccent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlAccent.BackColor = Color.Gray;
            pnlAccent.Controls.Add(pnlMainContainer);
            pnlAccent.Location = new Point(1, 1);
            pnlAccent.Name = "pnlAccent";
            pnlAccent.Size = new Size(178, 39);
            pnlAccent.TabIndex = 3;
            pnlAccent.Paint += pnlAccent_Paint;
            // 
            // SectionLabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(103, 178, 216);
            Controls.Add(pnlAccent);
            Name = "SectionLabel";
            Size = new Size(182, 43);
            pnlMainContainer.ResumeLayout(false);
            pnlMainContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCutOrder).EndInit();
            flowServers.ResumeLayout(false);
            flowServers.PerformLayout();
            pnlAccent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnServerButton;
        private Label lblSectionNumber;
        private Panel pnlMainContainer;
        private PictureBox picCutOrder;
        private FlowLayoutPanel flowServers;
        private Panel pnlAccent;
    }
}
