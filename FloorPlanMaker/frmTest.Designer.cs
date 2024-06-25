namespace FloorPlanMakerUI
{
    partial class frmTest
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
            button1 = new Button();
            txtInsideSales = new TextBox();
            txtOutsideSales = new TextBox();
            txtOutsideCocktailSales = new TextBox();
            txtInsideCocktailSales = new TextBox();
            txtUpperSales = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            label6 = new Label();
            lblArea1Servers = new Label();
            lblArea2Servers = new Label();
            lblArea3Servers = new Label();
            lblArea5Servers = new Label();
            lblArea4Servers = new Label();
            lblSalesPerServer1 = new Label();
            lblSalesPerServer2 = new Label();
            lblSalesPerServer3 = new Label();
            lblSalesPerServer4 = new Label();
            lblSalesPerServer5 = new Label();
            lblTotalSales = new Label();
            lblSalesPerServerTotal = new Label();
            button2 = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            lblServersAssigned = new Label();
            lblServersRemaining = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(32, 468);
            button1.Name = "button1";
            button1.Size = new Size(142, 23);
            button1.TabIndex = 0;
            button1.Text = "generate";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtInsideSales
            // 
            txtInsideSales.Location = new Point(74, 127);
            txtInsideSales.Name = "txtInsideSales";
            txtInsideSales.Size = new Size(100, 23);
            txtInsideSales.TabIndex = 1;
            txtInsideSales.TextChanged += button1_Click;
            // 
            // txtOutsideSales
            // 
            txtOutsideSales.Location = new Point(222, 127);
            txtOutsideSales.Name = "txtOutsideSales";
            txtOutsideSales.Size = new Size(100, 23);
            txtOutsideSales.TabIndex = 1;
            // 
            // txtOutsideCocktailSales
            // 
            txtOutsideCocktailSales.Location = new Point(347, 127);
            txtOutsideCocktailSales.Name = "txtOutsideCocktailSales";
            txtOutsideCocktailSales.Size = new Size(100, 23);
            txtOutsideCocktailSales.TabIndex = 1;
            // 
            // txtInsideCocktailSales
            // 
            txtInsideCocktailSales.Location = new Point(474, 127);
            txtInsideCocktailSales.Name = "txtInsideCocktailSales";
            txtInsideCocktailSales.Size = new Size(100, 23);
            txtInsideCocktailSales.TabIndex = 1;
            // 
            // txtUpperSales
            // 
            txtUpperSales.Location = new Point(607, 127);
            txtUpperSales.Name = "txtUpperSales";
            txtUpperSales.Size = new Size(100, 23);
            txtUpperSales.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 100);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "Inside";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(222, 100);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 2;
            label2.Text = "Outside";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(347, 100);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 2;
            label3.Text = "Outside Cocktail";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(474, 100);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 2;
            label4.Text = "Inside Cocktail";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(607, 100);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 2;
            label5.Text = "Upper";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(130, 30);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(71, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(74, 30);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 2;
            label6.Text = "Servers";
            // 
            // lblArea1Servers
            // 
            lblArea1Servers.AutoSize = true;
            lblArea1Servers.Location = new Point(74, 153);
            lblArea1Servers.Name = "lblArea1Servers";
            lblArea1Servers.Size = new Size(38, 15);
            lblArea1Servers.TabIndex = 2;
            lblArea1Servers.Text = "label1";
            // 
            // lblArea2Servers
            // 
            lblArea2Servers.AutoSize = true;
            lblArea2Servers.Location = new Point(224, 153);
            lblArea2Servers.Name = "lblArea2Servers";
            lblArea2Servers.Size = new Size(38, 15);
            lblArea2Servers.TabIndex = 2;
            lblArea2Servers.Text = "label1";
            // 
            // lblArea3Servers
            // 
            lblArea3Servers.AutoSize = true;
            lblArea3Servers.Location = new Point(347, 153);
            lblArea3Servers.Name = "lblArea3Servers";
            lblArea3Servers.Size = new Size(38, 15);
            lblArea3Servers.TabIndex = 2;
            lblArea3Servers.Text = "label1";
            // 
            // lblArea5Servers
            // 
            lblArea5Servers.AutoSize = true;
            lblArea5Servers.Location = new Point(607, 153);
            lblArea5Servers.Name = "lblArea5Servers";
            lblArea5Servers.Size = new Size(38, 15);
            lblArea5Servers.TabIndex = 2;
            lblArea5Servers.Text = "label1";
            // 
            // lblArea4Servers
            // 
            lblArea4Servers.AutoSize = true;
            lblArea4Servers.Location = new Point(474, 153);
            lblArea4Servers.Name = "lblArea4Servers";
            lblArea4Servers.Size = new Size(38, 15);
            lblArea4Servers.TabIndex = 2;
            lblArea4Servers.Text = "label1";
            // 
            // lblSalesPerServer1
            // 
            lblSalesPerServer1.AutoSize = true;
            lblSalesPerServer1.Location = new Point(74, 180);
            lblSalesPerServer1.Name = "lblSalesPerServer1";
            lblSalesPerServer1.Size = new Size(38, 15);
            lblSalesPerServer1.TabIndex = 2;
            lblSalesPerServer1.Text = "label1";
            // 
            // lblSalesPerServer2
            // 
            lblSalesPerServer2.AutoSize = true;
            lblSalesPerServer2.Location = new Point(222, 180);
            lblSalesPerServer2.Name = "lblSalesPerServer2";
            lblSalesPerServer2.Size = new Size(38, 15);
            lblSalesPerServer2.TabIndex = 2;
            lblSalesPerServer2.Text = "label1";
            // 
            // lblSalesPerServer3
            // 
            lblSalesPerServer3.AutoSize = true;
            lblSalesPerServer3.Location = new Point(347, 180);
            lblSalesPerServer3.Name = "lblSalesPerServer3";
            lblSalesPerServer3.Size = new Size(38, 15);
            lblSalesPerServer3.TabIndex = 2;
            lblSalesPerServer3.Text = "label1";
            // 
            // lblSalesPerServer4
            // 
            lblSalesPerServer4.AutoSize = true;
            lblSalesPerServer4.Location = new Point(474, 180);
            lblSalesPerServer4.Name = "lblSalesPerServer4";
            lblSalesPerServer4.Size = new Size(38, 15);
            lblSalesPerServer4.TabIndex = 2;
            lblSalesPerServer4.Text = "label1";
            // 
            // lblSalesPerServer5
            // 
            lblSalesPerServer5.AutoSize = true;
            lblSalesPerServer5.Location = new Point(608, 180);
            lblSalesPerServer5.Name = "lblSalesPerServer5";
            lblSalesPerServer5.Size = new Size(38, 15);
            lblSalesPerServer5.TabIndex = 2;
            lblSalesPerServer5.Text = "label1";
            // 
            // lblTotalSales
            // 
            lblTotalSales.AutoSize = true;
            lblTotalSales.Location = new Point(276, 257);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(60, 15);
            lblTotalSales.TabIndex = 2;
            lblTotalSales.Text = "total Sales";
            // 
            // lblSalesPerServerTotal
            // 
            lblSalesPerServerTotal.AutoSize = true;
            lblSalesPerServerTotal.Location = new Point(276, 284);
            lblSalesPerServerTotal.Name = "lblSalesPerServerTotal";
            lblSalesPerServerTotal.Size = new Size(87, 15);
            lblSalesPerServerTotal.TabIndex = 2;
            lblSalesPerServerTotal.Text = "sales Per Server";
            // 
            // button2
            // 
            button2.Location = new Point(222, 468);
            button2.Name = "button2";
            button2.Size = new Size(116, 23);
            button2.TabIndex = 4;
            button2.Text = "Randomize";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(235, 257);
            label7.Name = "label7";
            label7.Size = new Size(35, 15);
            label7.TabIndex = 2;
            label7.Text = "Total:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(208, 284);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 2;
            label8.Text = "Per Server:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(172, 308);
            label9.Name = "label9";
            label9.Size = new Size(98, 15);
            label9.TabIndex = 2;
            label9.Text = "Servers Assigned:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(163, 334);
            label10.Name = "label10";
            label10.Size = new Size(107, 15);
            label10.TabIndex = 2;
            label10.Text = "Servers Remaining:";
            // 
            // lblServersAssigned
            // 
            lblServersAssigned.AutoSize = true;
            lblServersAssigned.Location = new Point(276, 308);
            lblServersAssigned.Name = "lblServersAssigned";
            lblServersAssigned.Size = new Size(13, 15);
            lblServersAssigned.TabIndex = 2;
            lblServersAssigned.Text = "0";
            // 
            // lblServersRemaining
            // 
            lblServersRemaining.AutoSize = true;
            lblServersRemaining.Location = new Point(276, 334);
            lblServersRemaining.Name = "lblServersRemaining";
            lblServersRemaining.Size = new Size(13, 15);
            lblServersRemaining.TabIndex = 2;
            lblServersRemaining.Text = "0";
            // 
            // frmTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 519);
            Controls.Add(button2);
            Controls.Add(label6);
            Controls.Add(numericUpDown1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblArea4Servers);
            Controls.Add(lblArea5Servers);
            Controls.Add(lblArea3Servers);
            Controls.Add(lblArea2Servers);
            Controls.Add(label10);
            Controls.Add(lblServersRemaining);
            Controls.Add(lblServersAssigned);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(lblSalesPerServerTotal);
            Controls.Add(label7);
            Controls.Add(lblTotalSales);
            Controls.Add(lblSalesPerServer5);
            Controls.Add(lblSalesPerServer4);
            Controls.Add(lblSalesPerServer3);
            Controls.Add(lblSalesPerServer2);
            Controls.Add(lblSalesPerServer1);
            Controls.Add(lblArea1Servers);
            Controls.Add(label1);
            Controls.Add(txtUpperSales);
            Controls.Add(txtInsideCocktailSales);
            Controls.Add(txtOutsideCocktailSales);
            Controls.Add(txtOutsideSales);
            Controls.Add(txtInsideSales);
            Controls.Add(button1);
            Name = "frmTest";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmTest";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox txtInsideSales;
        private TextBox txtOutsideSales;
        private TextBox txtOutsideCocktailSales;
        private TextBox txtInsideCocktailSales;
        private TextBox txtUpperSales;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Label label6;
        private Label lblArea1Servers;
        private Label lblArea2Servers;
        private Label lblArea3Servers;
        private Label lblArea5Servers;
        private Label lblArea4Servers;
        private Label lblSalesPerServer1;
        private Label lblSalesPerServer2;
        private Label lblSalesPerServer3;
        private Label lblSalesPerServer4;
        private Label lblSalesPerServer5;
        private Label lblTotalSales;
        private Label lblSalesPerServerTotal;
        private Button button2;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label lblServersAssigned;
        private Label lblServersRemaining;
    }
}