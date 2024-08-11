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
            button2 = new Button();
            button3 = new Button();
            ilcCovers = new FloorPlanMakerUI.ImageLabelControl();
            ilcSales = new FloorPlanMakerUI.ImageLabelControl();
            ilcSalesDif = new FloorPlanMakerUI.ImageLabelControl();
            SuspendLayout();
            // 
            // lblSectionNumber
            // 
            lblSectionNumber.Dock = DockStyle.Left;
            lblSectionNumber.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSectionNumber.Location = new Point(0, 0);
            lblSectionNumber.Name = "lblSectionNumber";
            lblSectionNumber.Size = new Size(49, 33);
            lblSectionNumber.TabIndex = 0;
            lblSectionNumber.Text = "#1";
            lblSectionNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAssignedServer
            // 
            btnAssignedServer.BackColor = SystemColors.ButtonShadow;
            btnAssignedServer.Dock = DockStyle.Left;
            btnAssignedServer.FlatAppearance.BorderSize = 0;
            btnAssignedServer.FlatStyle = FlatStyle.Flat;
            btnAssignedServer.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAssignedServer.Location = new Point(49, 0);
            btnAssignedServer.Name = "btnAssignedServer";
            btnAssignedServer.Size = new Size(176, 33);
            btnAssignedServer.TabIndex = 1;
            btnAssignedServer.Text = "Unassigned";
            btnAssignedServer.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = Properties.Resources.erase_Small;
            button2.Location = new Point(618, 0);
            button2.Name = "button2";
            button2.Size = new Size(48, 33);
            button2.TabIndex = 3;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Right;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = Properties.Resources.erase_Small;
            button3.Location = new Point(566, 0);
            button3.Name = "button3";
            button3.Size = new Size(52, 33);
            button3.TabIndex = 4;
            button3.UseVisualStyleBackColor = true;
            // 
            // ilcCovers
            // 
            ilcCovers.BackColor = Color.FromArgb(180, 190, 200);
            ilcCovers.Location = new Point(231, 5);
            ilcCovers.Name = "ilcCovers";
            ilcCovers.Size = new Size(100, 23);
            ilcCovers.TabIndex = 5;
            // 
            // ilcSales
            // 
            ilcSales.BackColor = Color.FromArgb(180, 190, 200);
            ilcSales.Location = new Point(337, 5);
            ilcSales.Name = "ilcSales";
            ilcSales.Size = new Size(100, 23);
            ilcSales.TabIndex = 5;
            // 
            // ilcSalesDif
            // 
            ilcSalesDif.BackColor = Color.FromArgb(180, 190, 200);
            ilcSalesDif.Location = new Point(443, 5);
            ilcSalesDif.Name = "ilcSalesDif";
            ilcSalesDif.Size = new Size(100, 23);
            ilcSalesDif.TabIndex = 5;
            // 
            // SectionHeaderDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(ilcSalesDif);
            Controls.Add(ilcSales);
            Controls.Add(ilcCovers);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(btnAssignedServer);
            Controls.Add(lblSectionNumber);
            Name = "SectionHeaderDisplay";
            Size = new Size(666, 33);
            Load += SectionHeaderDisplay_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label lblSectionNumber;
        private Button btnAssignedServer;
        private Button button2;
        private Button button3;
        private FloorPlanMakerUI.ImageLabelControl ilcCovers;
        private FloorPlanMakerUI.ImageLabelControl ilcSales;
        private FloorPlanMakerUI.ImageLabelControl ilcSalesDif;
    }
}
