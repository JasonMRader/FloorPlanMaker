namespace FloorPlanMakerUI
{
    partial class FloorplanInfoControl
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
            lblYesterdayCount = new Label();
            lblLastWeekCount = new Label();
            lblCurrentServerCount = new Label();
            lblCoversPerServer = new Label();
            lblSalesPerServer = new Label();
            pbLastWeek = new PictureBox();
            pbYesterday = new PictureBox();
            pbServerCount = new PictureBox();
            pbCovers = new PictureBox();
            pbSales = new PictureBox();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)pbLastWeek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbYesterday).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbServerCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCovers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSales).BeginInit();
            SuspendLayout();
            // 
            // lblYesterdayCount
            // 
            lblYesterdayCount.BackColor = Color.FromArgb(180, 190, 200);
            lblYesterdayCount.Location = new Point(4, 4);
            lblYesterdayCount.Margin = new Padding(5, 3, 0, 3);
            lblYesterdayCount.Name = "lblYesterdayCount";
            lblYesterdayCount.Size = new Size(92, 20);
            lblYesterdayCount.TabIndex = 0;
            lblYesterdayCount.Text = "17";
            lblYesterdayCount.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblYesterdayCount, "Servers in this Area Yesterday");
            // 
            // lblLastWeekCount
            // 
            lblLastWeekCount.BackColor = Color.FromArgb(180, 190, 200);
            lblLastWeekCount.ImageAlign = ContentAlignment.MiddleLeft;
            lblLastWeekCount.Location = new Point(102, 4);
            lblLastWeekCount.Margin = new Padding(5, 3, 5, 3);
            lblLastWeekCount.Name = "lblLastWeekCount";
            lblLastWeekCount.Size = new Size(92, 20);
            lblLastWeekCount.TabIndex = 0;
            lblLastWeekCount.Text = "17";
            lblLastWeekCount.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblLastWeekCount, "Servers in this Area Last Week");
            // 
            // lblCurrentServerCount
            // 
            lblCurrentServerCount.BackColor = Color.FromArgb(100, 130, 180);
            lblCurrentServerCount.ForeColor = Color.White;
            lblCurrentServerCount.Location = new Point(202, 4);
            lblCurrentServerCount.Margin = new Padding(3);
            lblCurrentServerCount.Name = "lblCurrentServerCount";
            lblCurrentServerCount.Size = new Size(92, 20);
            lblCurrentServerCount.TabIndex = 0;
            lblCurrentServerCount.Text = "17";
            lblCurrentServerCount.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblCurrentServerCount, "Current Count of Servers Assigned");
            // 
            // lblCoversPerServer
            // 
            lblCoversPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblCoversPerServer.Location = new Point(4, 30);
            lblCoversPerServer.Margin = new Padding(3);
            lblCoversPerServer.Name = "lblCoversPerServer";
            lblCoversPerServer.Size = new Size(144, 20);
            lblCoversPerServer.TabIndex = 0;
            lblCoversPerServer.Text = "Covers";
            lblCoversPerServer.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblCoversPerServer, "Max Covers per Server, per Turn");
            // 
            // lblSalesPerServer
            // 
            lblSalesPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblSalesPerServer.Location = new Point(152, 30);
            lblSalesPerServer.Margin = new Padding(3);
            lblSalesPerServer.Name = "lblSalesPerServer";
            lblSalesPerServer.Size = new Size(144, 20);
            lblSalesPerServer.TabIndex = 0;
            lblSalesPerServer.Text = "Sales";
            lblSalesPerServer.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblSalesPerServer, "Expected Sales per Server");
            // 
            // pbLastWeek
            // 
            pbLastWeek.BackColor = Color.Transparent;
            pbLastWeek.Image = Properties.Resources._3Arrows;
            pbLastWeek.Location = new Point(113, 4);
            pbLastWeek.Name = "pbLastWeek";
            pbLastWeek.Size = new Size(20, 20);
            pbLastWeek.SizeMode = PictureBoxSizeMode.Zoom;
            pbLastWeek.TabIndex = 1;
            pbLastWeek.TabStop = false;
            // 
            // pbYesterday
            // 
            pbYesterday.BackColor = Color.Transparent;
            pbYesterday.Image = Properties.Resources._1Arrrow;
            pbYesterday.Location = new Point(4, 4);
            pbYesterday.Name = "pbYesterday";
            pbYesterday.Size = new Size(20, 20);
            pbYesterday.SizeMode = PictureBoxSizeMode.Zoom;
            pbYesterday.TabIndex = 2;
            pbYesterday.TabStop = false;
            // 
            // pbServerCount
            // 
            pbServerCount.BackColor = Color.Transparent;
            pbServerCount.Image = Properties.Resources.trayReeversedLeessSmall;
            pbServerCount.Location = new Point(220, 4);
            pbServerCount.Name = "pbServerCount";
            pbServerCount.Size = new Size(20, 20);
            pbServerCount.SizeMode = PictureBoxSizeMode.Zoom;
            pbServerCount.TabIndex = 3;
            pbServerCount.TabStop = false;
            // 
            // pbCovers
            // 
            pbCovers.BackColor = Color.Transparent;
            pbCovers.Image = Properties.Resources.covers;
            pbCovers.Location = new Point(31, 30);
            pbCovers.Name = "pbCovers";
            pbCovers.Size = new Size(20, 20);
            pbCovers.SizeMode = PictureBoxSizeMode.Zoom;
            pbCovers.TabIndex = 4;
            pbCovers.TabStop = false;
            // 
            // pbSales
            // 
            pbSales.BackColor = Color.Transparent;
            pbSales.Image = Properties.Resources.sales;
            pbSales.Location = new Point(174, 30);
            pbSales.Name = "pbSales";
            pbSales.Size = new Size(20, 20);
            pbSales.SizeMode = PictureBoxSizeMode.Zoom;
            pbSales.TabIndex = 5;
            pbSales.TabStop = false;
            // 
            // FloorplanInfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbSales);
            Controls.Add(pbCovers);
            Controls.Add(pbServerCount);
            Controls.Add(pbYesterday);
            Controls.Add(pbLastWeek);
            Controls.Add(lblSalesPerServer);
            Controls.Add(lblCoversPerServer);
            Controls.Add(lblCurrentServerCount);
            Controls.Add(lblLastWeekCount);
            Controls.Add(lblYesterdayCount);
            Name = "FloorplanInfoControl";
            Size = new Size(300, 52);
            ((System.ComponentModel.ISupportInitialize)pbLastWeek).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbYesterday).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbServerCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCovers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSales).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblYesterdayCount;
        private Label lblLastWeekCount;
        private Label lblCurrentServerCount;
        private Label lblCoversPerServer;
        private Label lblSalesPerServer;
        private PictureBox pbLastWeek;
        private PictureBox pbYesterday;
        private PictureBox pbServerCount;
        private PictureBox pbCovers;
        private PictureBox pbSales;
        private ToolTip toolTip1;
    }
}
