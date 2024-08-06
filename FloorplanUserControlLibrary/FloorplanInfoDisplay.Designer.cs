namespace FloorplanUserControlLibrary
{
    partial class FloorplanInfoDisplay
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
            lblCrtlServersOn = new FloorPlanMakerUI.ImageLabelControl();
            lblCrtlServersYesterday = new FloorPlanMakerUI.ImageLabelControl();
            lblCrtlServersLastWeek = new FloorPlanMakerUI.ImageLabelControl();
            lblCrtlSalesPerServer = new FloorPlanMakerUI.ImageLabelControl();
            lblCrtlCoversPerServer = new FloorPlanMakerUI.ImageLabelControl();
            SuspendLayout();
            // 
            // lblCrtlServersOn
            // 
            lblCrtlServersOn.BackColor = Color.FromArgb(180, 190, 200);
            lblCrtlServersOn.Location = new Point(3, 3);
            lblCrtlServersOn.Name = "lblCrtlServersOn";
            lblCrtlServersOn.Size = new Size(258, 34);
            lblCrtlServersOn.TabIndex = 0;
            // 
            // lblCrtlServersYesterday
            // 
            lblCrtlServersYesterday.BackColor = Color.FromArgb(180, 190, 200);
            lblCrtlServersYesterday.Location = new Point(3, 43);
            lblCrtlServersYesterday.Name = "lblCrtlServersYesterday";
            lblCrtlServersYesterday.Size = new Size(125, 27);
            lblCrtlServersYesterday.TabIndex = 0;
            // 
            // lblCrtlServersLastWeek
            // 
            lblCrtlServersLastWeek.BackColor = Color.FromArgb(180, 190, 200);
            lblCrtlServersLastWeek.Location = new Point(134, 43);
            lblCrtlServersLastWeek.Name = "lblCrtlServersLastWeek";
            lblCrtlServersLastWeek.Size = new Size(127, 27);
            lblCrtlServersLastWeek.TabIndex = 0;
            // 
            // lblCrtlSalesPerServer
            // 
            lblCrtlSalesPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblCrtlSalesPerServer.Location = new Point(134, 76);
            lblCrtlSalesPerServer.Name = "lblCrtlSalesPerServer";
            lblCrtlSalesPerServer.Size = new Size(127, 27);
            lblCrtlSalesPerServer.TabIndex = 0;
            // 
            // lblCrtlCoversPerServer
            // 
            lblCrtlCoversPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblCrtlCoversPerServer.Location = new Point(3, 76);
            lblCrtlCoversPerServer.Name = "lblCrtlCoversPerServer";
            lblCrtlCoversPerServer.Size = new Size(125, 27);
            lblCrtlCoversPerServer.TabIndex = 0;
            // 
            // FloorplanInfoDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblCrtlCoversPerServer);
            Controls.Add(lblCrtlServersLastWeek);
            Controls.Add(lblCrtlSalesPerServer);
            Controls.Add(lblCrtlServersYesterday);
            Controls.Add(lblCrtlServersOn);
            Name = "FloorplanInfoDisplay";
            Size = new Size(267, 111);
            ResumeLayout(false);
        }

        #endregion

        private FloorPlanMakerUI.ImageLabelControl lblCrtlServersOn;
        private FloorPlanMakerUI.ImageLabelControl lblCrtlServersYesterday;
        private FloorPlanMakerUI.ImageLabelControl lblCrtlServersLastWeek;
        private FloorPlanMakerUI.ImageLabelControl lblCrtlSalesPerServer;
        private FloorPlanMakerUI.ImageLabelControl lblCrtlCoversPerServer;
    }
}
