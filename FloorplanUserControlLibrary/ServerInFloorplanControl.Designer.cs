namespace FloorplanUserControlLibrary
{
    partial class ServerInFloorplanControl
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
                if (_server != null)
                {
                    _server.Unsubscribe(this);
                }
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
            btnServer = new Button();
            flowShiftDisplay = new FlowLayoutPanel();
            ilcSectionRating = new FloorPlanMakerUI.ImageLabelControl();
            ilcCloseRating = new FloorPlanMakerUI.ImageLabelControl();
            ilcTeamWaitRating = new FloorPlanMakerUI.ImageLabelControl();
            SuspendLayout();
            // 
            // btnServer
            // 
            btnServer.BackColor = Color.FromArgb(100, 130, 180);
            btnServer.Dock = DockStyle.Top;
            btnServer.FlatAppearance.BorderSize = 0;
            btnServer.FlatStyle = FlatStyle.Flat;
            btnServer.Font = new Font("Segoe UI Semibold", 12.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnServer.ForeColor = Color.White;
            btnServer.Location = new Point(0, 0);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(265, 27);
            btnServer.TabIndex = 0;
            btnServer.Text = "Adrianna";
            btnServer.UseVisualStyleBackColor = false;
            btnServer.Click += btnServer_Click;
            // 
            // flowShiftDisplay
            // 
            flowShiftDisplay.BackColor = Color.FromArgb(225, 225, 225);
            flowShiftDisplay.Dock = DockStyle.Left;
            flowShiftDisplay.Location = new Point(0, 27);
            flowShiftDisplay.Name = "flowShiftDisplay";
            flowShiftDisplay.Padding = new Padding(3, 3, 0, 0);
            flowShiftDisplay.Size = new Size(209, 62);
            flowShiftDisplay.TabIndex = 2;
            // 
            // ilcSectionRating
            // 
            ilcSectionRating.Location = new Point(215, 27);
            ilcSectionRating.Name = "ilcSectionRating";
            ilcSectionRating.Size = new Size(47, 20);
            ilcSectionRating.TabIndex = 3;
            // 
            // ilcCloseRating
            // 
            ilcCloseRating.Location = new Point(215, 47);
            ilcCloseRating.Name = "ilcCloseRating";
            ilcCloseRating.Size = new Size(47, 20);
            ilcCloseRating.TabIndex = 3;
            // 
            // ilcTeamWaitRating
            // 
            ilcTeamWaitRating.Location = new Point(215, 67);
            ilcTeamWaitRating.Name = "ilcTeamWaitRating";
            ilcTeamWaitRating.Size = new Size(47, 20);
            ilcTeamWaitRating.TabIndex = 3;
            // 
            // ServerInFloorplanControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ilcTeamWaitRating);
            Controls.Add(ilcCloseRating);
            Controls.Add(ilcSectionRating);
            Controls.Add(flowShiftDisplay);
            Controls.Add(btnServer);
            Name = "ServerInFloorplanControl";
            Size = new Size(265, 89);
            ResumeLayout(false);
        }

        #endregion

        private Button btnServer;
        private FlowLayoutPanel flowShiftDisplay;
        private FloorPlanMakerUI.ImageLabelControl ilcSectionRating;
        private FloorPlanMakerUI.ImageLabelControl ilcCloseRating;
        private FloorPlanMakerUI.ImageLabelControl ilcTeamWaitRating;
    }
}
