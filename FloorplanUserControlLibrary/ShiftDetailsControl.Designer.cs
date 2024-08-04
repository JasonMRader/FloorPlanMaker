namespace FloorplanUserControlLibrary
{
    partial class ShiftDetailsControl
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
            components = new System.ComponentModel.Container();
            lblShiftDate = new Label();
            lblIsLunch = new Label();
            lblTotalServers = new Label();
            lblTotalSales = new Label();
            lblSalesPerServer = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // lblShiftDate
            // 
            lblShiftDate.BackColor = Color.FromArgb(180, 190, 200);
            lblShiftDate.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.Location = new Point(3, 50);
            lblShiftDate.Margin = new Padding(3);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(116, 22);
            lblShiftDate.TabIndex = 0;
            lblShiftDate.Text = "Jan 1, 2024";
            lblShiftDate.TextAlign = ContentAlignment.MiddleRight;
            lblShiftDate.Click += lblShiftDate_Click;
            // 
            // lblIsLunch
            // 
            lblIsLunch.BackColor = Color.FromArgb(180, 190, 200);
            lblIsLunch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblIsLunch.Location = new Point(124, 50);
            lblIsLunch.Margin = new Padding(0, 3, 3, 0);
            lblIsLunch.Name = "lblIsLunch";
            lblIsLunch.Size = new Size(73, 22);
            lblIsLunch.TabIndex = 0;
            lblIsLunch.Text = "AM";
            lblIsLunch.TextAlign = ContentAlignment.MiddleLeft;
            lblIsLunch.Click += label1_Click;
            // 
            // lblTotalServers
            // 
            lblTotalServers.BackColor = Color.FromArgb(180, 190, 200);
            lblTotalServers.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalServers.Location = new Point(26, 84);
            lblTotalServers.Margin = new Padding(0, 3, 0, 0);
            lblTotalServers.Name = "lblTotalServers";
            lblTotalServers.Size = new Size(61, 27);
            lblTotalServers.TabIndex = 0;
            lblTotalServers.Text = "19";
            lblTotalServers.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblTotalServers, "Servers (and Bartenders) on Shift");
            lblTotalServers.Click += label1_Click;
            // 
            // lblTotalSales
            // 
            lblTotalSales.BackColor = Color.FromArgb(180, 190, 200);
            lblTotalSales.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalSales.Location = new Point(26, 120);
            lblTotalSales.Margin = new Padding(0, 3, 0, 0);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(168, 27);
            lblTotalSales.TabIndex = 0;
            lblTotalSales.Text = "$30,000";
            lblTotalSales.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblTotalSales, "Total Sales in all Areas USED");
            lblTotalSales.Click += label1_Click;
            // 
            // lblSalesPerServer
            // 
            lblSalesPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblSalesPerServer.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSalesPerServer.Location = new Point(119, 84);
            lblSalesPerServer.Margin = new Padding(0, 3, 0, 0);
            lblSalesPerServer.Name = "lblSalesPerServer";
            lblSalesPerServer.Size = new Size(75, 27);
            lblSalesPerServer.TabIndex = 0;
            lblSalesPerServer.Text = "$1,500";
            lblSalesPerServer.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblSalesPerServer, "Average Sales for each Server / Bartender");
            lblSalesPerServer.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(180, 190, 200);
            pictureBox1.Image = Properties.Resources.sales;
            pictureBox1.Location = new Point(3, 120);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(27, 27);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "Total Sales in all Areas USED");
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(180, 190, 200);
            pictureBox2.Image = Properties.Resources.waiter;
            pictureBox2.Location = new Point(3, 84);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(27, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            toolTip1.SetToolTip(pictureBox2, "Servers (and Bartenders) on Shift");
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(180, 190, 200);
            pictureBox3.Image = Properties.Resources.SalesPerPerson_28px;
            pictureBox3.Location = new Point(92, 84);
            pictureBox3.Margin = new Padding(0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(27, 27);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            toolTip1.SetToolTip(pictureBox3, "Average Sales for each Server / Bartender");
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(180, 190, 200);
            label1.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 3);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(194, 37);
            label1.TabIndex = 0;
            label1.Text = "Shift Overview";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += lblShiftDate_Click;
            // 
            // ShiftDetailsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(lblSalesPerServer);
            Controls.Add(lblTotalSales);
            Controls.Add(lblTotalServers);
            Controls.Add(lblIsLunch);
            Controls.Add(label1);
            Controls.Add(lblShiftDate);
            Name = "ShiftDetailsControl";
            Size = new Size(200, 163);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblShiftDate;
        private Label lblIsLunch;
        private Label lblTotalServers;
        private Label lblTotalSales;
        private Label lblSalesPerServer;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label1;
        private ToolTip toolTip1;
    }
}
