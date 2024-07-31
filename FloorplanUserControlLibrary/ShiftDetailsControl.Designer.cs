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
            lblShiftDate = new Label();
            lblIsLunch = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            lblTotalServers = new Label();
            lblTotalSales = new Label();
            lblSalesPerServer = new Label();
            SuspendLayout();
            // 
            // lblShiftDate
            // 
            lblShiftDate.BackColor = Color.FromArgb(180, 190, 200);
            lblShiftDate.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.Location = new Point(3, 3);
            lblShiftDate.Margin = new Padding(3);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(118, 22);
            lblShiftDate.TabIndex = 0;
            lblShiftDate.Text = "Jan 1, 2024";
            lblShiftDate.TextAlign = ContentAlignment.MiddleRight;
            lblShiftDate.Click += lblShiftDate_Click;
            // 
            // lblIsLunch
            // 
            lblIsLunch.BackColor = Color.FromArgb(180, 190, 200);
            lblIsLunch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblIsLunch.Location = new Point(124, 3);
            lblIsLunch.Margin = new Padding(0, 3, 3, 0);
            lblIsLunch.Name = "lblIsLunch";
            lblIsLunch.Size = new Size(73, 22);
            lblIsLunch.TabIndex = 0;
            lblIsLunch.Text = "AM";
            lblIsLunch.TextAlign = ContentAlignment.MiddleLeft;
            lblIsLunch.Click += label1_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(180, 190, 200);
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(3, 49);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(118, 22);
            label2.TabIndex = 0;
            label2.Text = "Total Servers:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            label2.Click += lblShiftDate_Click;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(180, 190, 200);
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(3, 77);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Size = new Size(118, 22);
            label3.TabIndex = 0;
            label3.Text = "Total Sales:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            label3.Click += lblShiftDate_Click;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(180, 190, 200);
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(3, 105);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Size = new Size(118, 22);
            label4.TabIndex = 0;
            label4.Text = "Sales / Server:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            label4.Click += lblShiftDate_Click;
            // 
            // lblTotalServers
            // 
            lblTotalServers.BackColor = Color.FromArgb(180, 190, 200);
            lblTotalServers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalServers.Location = new Point(124, 49);
            lblTotalServers.Margin = new Padding(0, 3, 3, 0);
            lblTotalServers.Name = "lblTotalServers";
            lblTotalServers.Size = new Size(73, 22);
            lblTotalServers.TabIndex = 0;
            lblTotalServers.Text = "19";
            lblTotalServers.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalServers.Click += label1_Click;
            // 
            // lblTotalSales
            // 
            lblTotalSales.BackColor = Color.FromArgb(180, 190, 200);
            lblTotalSales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalSales.Location = new Point(124, 77);
            lblTotalSales.Margin = new Padding(0, 3, 3, 0);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(73, 22);
            lblTotalSales.TabIndex = 0;
            lblTotalSales.Text = "$30,000";
            lblTotalSales.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalSales.Click += label1_Click;
            // 
            // lblSalesPerServer
            // 
            lblSalesPerServer.BackColor = Color.FromArgb(180, 190, 200);
            lblSalesPerServer.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSalesPerServer.Location = new Point(124, 105);
            lblSalesPerServer.Margin = new Padding(0, 3, 3, 0);
            lblSalesPerServer.Name = "lblSalesPerServer";
            lblSalesPerServer.Size = new Size(73, 22);
            lblSalesPerServer.TabIndex = 0;
            lblSalesPerServer.Text = "$1,500";
            lblSalesPerServer.TextAlign = ContentAlignment.MiddleLeft;
            lblSalesPerServer.Click += label1_Click;
            // 
            // ShiftDetailsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(lblSalesPerServer);
            Controls.Add(lblTotalSales);
            Controls.Add(lblTotalServers);
            Controls.Add(lblIsLunch);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblShiftDate);
            Name = "ShiftDetailsControl";
            Size = new Size(200, 163);
            ResumeLayout(false);
        }

        #endregion

        private Label lblShiftDate;
        private Label lblIsLunch;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblTotalServers;
        private Label lblTotalSales;
        private Label lblSalesPerServer;
    }
}
