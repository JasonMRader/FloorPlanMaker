namespace FloorPlanMakerUI
{
    partial class frmSalesStats
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
            if (disposing && (components != null)) {
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
            rdoDiningAreaSales = new RadioButton();
            rdoServerShifts = new RadioButton();
            panel1 = new Panel();
            pnlServerSelect = new Panel();
            btnIndividualServerShifts = new Button();
            lblComboLabel = new Label();
            btnIndividualStats = new Button();
            cboServerSelect = new ComboBox();
            dataGridView1 = new DataGridView();
            flowDiningAreas = new FlowLayoutPanel();
            dgvAreaStats = new DataGridView();
            button1 = new Button();
            cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            btnLineGraph = new Button();
            btnAreaGraph = new Button();
            btnBoxChart = new Button();
            pnlFilters = new Panel();
            rdoCompareDates = new RadioButton();
            panel7 = new Panel();
            rdoDistribution = new RadioButton();
            rdoCompareClouds = new RadioButton();
            rdoCompareWind = new RadioButton();
            rdoCompareReservations = new RadioButton();
            rdoCompareTemp = new RadioButton();
            rdoCompareRain = new RadioButton();
            rdoCompareWeekDays = new RadioButton();
            rdoCompareMonths = new RadioButton();
            label7 = new Label();
            pnlContainer = new Panel();
            lblAreaDisplay = new Label();
            panel1.SuspendLayout();
            pnlServerSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAreaStats).BeginInit();
            panel7.SuspendLayout();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // rdoDiningAreaSales
            // 
            rdoDiningAreaSales.Appearance = Appearance.Button;
            rdoDiningAreaSales.BackColor = SystemColors.ButtonShadow;
            rdoDiningAreaSales.Checked = true;
            rdoDiningAreaSales.FlatAppearance.BorderSize = 0;
            rdoDiningAreaSales.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoDiningAreaSales.FlatStyle = FlatStyle.Flat;
            rdoDiningAreaSales.ForeColor = Color.White;
            rdoDiningAreaSales.Location = new Point(0, 0);
            rdoDiningAreaSales.Margin = new Padding(0);
            rdoDiningAreaSales.Name = "rdoDiningAreaSales";
            rdoDiningAreaSales.Size = new Size(210, 28);
            rdoDiningAreaSales.TabIndex = 1;
            rdoDiningAreaSales.TabStop = true;
            rdoDiningAreaSales.Text = "Dining Area Sales";
            rdoDiningAreaSales.TextAlign = ContentAlignment.MiddleCenter;
            rdoDiningAreaSales.UseVisualStyleBackColor = false;
            // 
            // rdoServerShifts
            // 
            rdoServerShifts.Appearance = Appearance.Button;
            rdoServerShifts.BackColor = SystemColors.ButtonShadow;
            rdoServerShifts.FlatAppearance.BorderSize = 0;
            rdoServerShifts.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoServerShifts.FlatStyle = FlatStyle.Flat;
            rdoServerShifts.ForeColor = Color.White;
            rdoServerShifts.Location = new Point(0, 28);
            rdoServerShifts.Margin = new Padding(0);
            rdoServerShifts.Name = "rdoServerShifts";
            rdoServerShifts.Size = new Size(210, 26);
            rdoServerShifts.TabIndex = 1;
            rdoServerShifts.Text = "Server Shift History";
            rdoServerShifts.TextAlign = ContentAlignment.MiddleCenter;
            rdoServerShifts.UseVisualStyleBackColor = false;
            rdoServerShifts.CheckedChanged += rdoServerShifts_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoServerShifts);
            panel1.Controls.Add(rdoDiningAreaSales);
            panel1.Location = new Point(1251, 819);
            panel1.Name = "panel1";
            panel1.Size = new Size(210, 56);
            panel1.TabIndex = 4;
            // 
            // pnlServerSelect
            // 
            pnlServerSelect.Controls.Add(btnIndividualServerShifts);
            pnlServerSelect.Controls.Add(lblComboLabel);
            pnlServerSelect.Controls.Add(btnIndividualStats);
            pnlServerSelect.Controls.Add(cboServerSelect);
            pnlServerSelect.Location = new Point(1251, 881);
            pnlServerSelect.Name = "pnlServerSelect";
            pnlServerSelect.Size = new Size(206, 62);
            pnlServerSelect.TabIndex = 8;
            // 
            // btnIndividualServerShifts
            // 
            btnIndividualServerShifts.Location = new Point(98, 32);
            btnIndividualServerShifts.Name = "btnIndividualServerShifts";
            btnIndividualServerShifts.Size = new Size(86, 23);
            btnIndividualServerShifts.TabIndex = 3;
            btnIndividualServerShifts.Text = "See Server Shifts";
            btnIndividualServerShifts.UseVisualStyleBackColor = true;
            btnIndividualServerShifts.Visible = false;
            btnIndividualServerShifts.Click += btnIndividualServerShifts_Click;
            // 
            // lblComboLabel
            // 
            lblComboLabel.AutoSize = true;
            lblComboLabel.Location = new Point(6, 0);
            lblComboLabel.Name = "lblComboLabel";
            lblComboLabel.Size = new Size(74, 15);
            lblComboLabel.TabIndex = 2;
            lblComboLabel.Text = "Dining Areas";
            // 
            // btnIndividualStats
            // 
            btnIndividualStats.Location = new Point(98, 3);
            btnIndividualStats.Name = "btnIndividualStats";
            btnIndividualStats.Size = new Size(86, 23);
            btnIndividualStats.TabIndex = 1;
            btnIndividualStats.Text = "Server Table History";
            btnIndividualStats.UseVisualStyleBackColor = true;
            btnIndividualStats.Click += btnIndividualStats_Click;
            // 
            // cboServerSelect
            // 
            cboServerSelect.FormattingEnabled = true;
            cboServerSelect.Location = new Point(6, 21);
            cboServerSelect.Name = "cboServerSelect";
            cboServerSelect.Size = new Size(86, 23);
            cboServerSelect.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(36, 716);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(890, 249);
            dataGridView1.TabIndex = 12;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(36, 444);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(890, 53);
            flowDiningAreas.TabIndex = 13;
            // 
            // dgvAreaStats
            // 
            dgvAreaStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAreaStats.Location = new Point(36, 503);
            dgvAreaStats.Name = "dgvAreaStats";
            dgvAreaStats.RowTemplate.Height = 25;
            dgvAreaStats.Size = new Size(890, 201);
            dgvAreaStats.TabIndex = 14;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(190, 80, 70);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = Properties.Resources.X15x;
            button1.Location = new Point(1433, 3);
            button1.Name = "button1";
            button1.Size = new Size(28, 28);
            button1.TabIndex = 17;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // cartesianChart1
            // 
            cartesianChart1.BackColor = Color.WhiteSmoke;
            cartesianChart1.Location = new Point(36, 67);
            cartesianChart1.Name = "cartesianChart1";
            cartesianChart1.Size = new Size(1074, 362);
            cartesianChart1.TabIndex = 18;
            // 
            // btnLineGraph
            // 
            btnLineGraph.BackColor = Color.FromArgb(100, 130, 180);
            btnLineGraph.FlatAppearance.BorderSize = 0;
            btnLineGraph.FlatStyle = FlatStyle.Flat;
            btnLineGraph.ForeColor = Color.White;
            btnLineGraph.Location = new Point(36, 8);
            btnLineGraph.Name = "btnLineGraph";
            btnLineGraph.Size = new Size(98, 23);
            btnLineGraph.TabIndex = 19;
            btnLineGraph.Text = "Line";
            btnLineGraph.UseVisualStyleBackColor = false;
            btnLineGraph.Click += btnLineGraph_Click;
            // 
            // btnAreaGraph
            // 
            btnAreaGraph.BackColor = Color.FromArgb(100, 130, 180);
            btnAreaGraph.FlatAppearance.BorderSize = 0;
            btnAreaGraph.FlatStyle = FlatStyle.Flat;
            btnAreaGraph.ForeColor = Color.White;
            btnAreaGraph.Location = new Point(140, 8);
            btnAreaGraph.Name = "btnAreaGraph";
            btnAreaGraph.Size = new Size(98, 23);
            btnAreaGraph.TabIndex = 19;
            btnAreaGraph.Text = "Area";
            btnAreaGraph.UseVisualStyleBackColor = false;
            btnAreaGraph.Click += btnAreaGraph_Click;
            // 
            // btnBoxChart
            // 
            btnBoxChart.BackColor = Color.FromArgb(100, 130, 180);
            btnBoxChart.FlatAppearance.BorderSize = 0;
            btnBoxChart.FlatStyle = FlatStyle.Flat;
            btnBoxChart.ForeColor = Color.White;
            btnBoxChart.Location = new Point(244, 8);
            btnBoxChart.Name = "btnBoxChart";
            btnBoxChart.Size = new Size(98, 23);
            btnBoxChart.TabIndex = 19;
            btnBoxChart.Text = "Box";
            btnBoxChart.UseVisualStyleBackColor = false;
            btnBoxChart.Click += btnBoxChart_Click;
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.FromArgb(180, 190, 200);
            pnlFilters.Location = new Point(1251, 66);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(5, 5, 0, 0);
            pnlFilters.Size = new Size(210, 747);
            pnlFilters.TabIndex = 20;
            // 
            // rdoCompareDates
            // 
            rdoCompareDates.Appearance = Appearance.Button;
            rdoCompareDates.BackColor = SystemColors.ButtonShadow;
            rdoCompareDates.Checked = true;
            rdoCompareDates.FlatAppearance.BorderSize = 0;
            rdoCompareDates.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareDates.FlatStyle = FlatStyle.Flat;
            rdoCompareDates.ForeColor = Color.White;
            rdoCompareDates.Location = new Point(3, 3);
            rdoCompareDates.Name = "rdoCompareDates";
            rdoCompareDates.Size = new Size(87, 24);
            rdoCompareDates.TabIndex = 21;
            rdoCompareDates.TabStop = true;
            rdoCompareDates.Text = "Dates";
            rdoCompareDates.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareDates.UseVisualStyleBackColor = false;
            rdoCompareDates.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // panel7
            // 
            panel7.Controls.Add(rdoDistribution);
            panel7.Controls.Add(rdoCompareClouds);
            panel7.Controls.Add(rdoCompareWind);
            panel7.Controls.Add(rdoCompareReservations);
            panel7.Controls.Add(rdoCompareTemp);
            panel7.Controls.Add(rdoCompareRain);
            panel7.Controls.Add(rdoCompareWeekDays);
            panel7.Controls.Add(rdoCompareMonths);
            panel7.Controls.Add(rdoCompareDates);
            panel7.Location = new Point(1125, 95);
            panel7.Name = "panel7";
            panel7.Size = new Size(95, 277);
            panel7.TabIndex = 22;
            // 
            // rdoDistribution
            // 
            rdoDistribution.Appearance = Appearance.Button;
            rdoDistribution.BackColor = SystemColors.ButtonShadow;
            rdoDistribution.FlatAppearance.BorderSize = 0;
            rdoDistribution.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoDistribution.FlatStyle = FlatStyle.Flat;
            rdoDistribution.ForeColor = Color.White;
            rdoDistribution.Location = new Point(3, 243);
            rdoDistribution.Name = "rdoDistribution";
            rdoDistribution.Size = new Size(87, 24);
            rdoDistribution.TabIndex = 21;
            rdoDistribution.Text = "Distribution";
            rdoDistribution.TextAlign = ContentAlignment.MiddleCenter;
            rdoDistribution.UseVisualStyleBackColor = false;
            rdoDistribution.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareClouds
            // 
            rdoCompareClouds.Appearance = Appearance.Button;
            rdoCompareClouds.BackColor = SystemColors.ButtonShadow;
            rdoCompareClouds.Enabled = false;
            rdoCompareClouds.FlatAppearance.BorderSize = 0;
            rdoCompareClouds.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareClouds.FlatStyle = FlatStyle.Flat;
            rdoCompareClouds.ForeColor = Color.White;
            rdoCompareClouds.Location = new Point(3, 213);
            rdoCompareClouds.Name = "rdoCompareClouds";
            rdoCompareClouds.Size = new Size(87, 24);
            rdoCompareClouds.TabIndex = 21;
            rdoCompareClouds.Text = "Clouds";
            rdoCompareClouds.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareClouds.UseVisualStyleBackColor = false;
            rdoCompareClouds.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareWind
            // 
            rdoCompareWind.Appearance = Appearance.Button;
            rdoCompareWind.BackColor = SystemColors.ButtonShadow;
            rdoCompareWind.Enabled = false;
            rdoCompareWind.FlatAppearance.BorderSize = 0;
            rdoCompareWind.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareWind.FlatStyle = FlatStyle.Flat;
            rdoCompareWind.ForeColor = Color.White;
            rdoCompareWind.Location = new Point(3, 183);
            rdoCompareWind.Name = "rdoCompareWind";
            rdoCompareWind.Size = new Size(87, 24);
            rdoCompareWind.TabIndex = 21;
            rdoCompareWind.Text = "Wind";
            rdoCompareWind.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareWind.UseVisualStyleBackColor = false;
            rdoCompareWind.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareReservations
            // 
            rdoCompareReservations.Appearance = Appearance.Button;
            rdoCompareReservations.BackColor = SystemColors.ButtonShadow;
            rdoCompareReservations.Enabled = false;
            rdoCompareReservations.FlatAppearance.BorderSize = 0;
            rdoCompareReservations.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareReservations.FlatStyle = FlatStyle.Flat;
            rdoCompareReservations.ForeColor = Color.White;
            rdoCompareReservations.Location = new Point(3, 93);
            rdoCompareReservations.Name = "rdoCompareReservations";
            rdoCompareReservations.Size = new Size(87, 24);
            rdoCompareReservations.TabIndex = 21;
            rdoCompareReservations.Text = "Reservations";
            rdoCompareReservations.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareReservations.UseVisualStyleBackColor = false;
            rdoCompareReservations.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareTemp
            // 
            rdoCompareTemp.Appearance = Appearance.Button;
            rdoCompareTemp.BackColor = SystemColors.ButtonShadow;
            rdoCompareTemp.FlatAppearance.BorderSize = 0;
            rdoCompareTemp.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareTemp.FlatStyle = FlatStyle.Flat;
            rdoCompareTemp.ForeColor = Color.White;
            rdoCompareTemp.Location = new Point(3, 153);
            rdoCompareTemp.Name = "rdoCompareTemp";
            rdoCompareTemp.Size = new Size(87, 24);
            rdoCompareTemp.TabIndex = 21;
            rdoCompareTemp.Text = "Temperature";
            rdoCompareTemp.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareTemp.UseVisualStyleBackColor = false;
            rdoCompareTemp.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareRain
            // 
            rdoCompareRain.Appearance = Appearance.Button;
            rdoCompareRain.BackColor = SystemColors.ButtonShadow;
            rdoCompareRain.Enabled = false;
            rdoCompareRain.FlatAppearance.BorderSize = 0;
            rdoCompareRain.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareRain.FlatStyle = FlatStyle.Flat;
            rdoCompareRain.ForeColor = Color.White;
            rdoCompareRain.Location = new Point(3, 123);
            rdoCompareRain.Name = "rdoCompareRain";
            rdoCompareRain.Size = new Size(87, 24);
            rdoCompareRain.TabIndex = 21;
            rdoCompareRain.Text = "Rain";
            rdoCompareRain.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareRain.UseVisualStyleBackColor = false;
            rdoCompareRain.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareWeekDays
            // 
            rdoCompareWeekDays.Appearance = Appearance.Button;
            rdoCompareWeekDays.BackColor = SystemColors.ButtonShadow;
            rdoCompareWeekDays.FlatAppearance.BorderSize = 0;
            rdoCompareWeekDays.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareWeekDays.FlatStyle = FlatStyle.Flat;
            rdoCompareWeekDays.ForeColor = Color.White;
            rdoCompareWeekDays.Location = new Point(3, 63);
            rdoCompareWeekDays.Name = "rdoCompareWeekDays";
            rdoCompareWeekDays.Size = new Size(87, 24);
            rdoCompareWeekDays.TabIndex = 21;
            rdoCompareWeekDays.Text = "Week Days";
            rdoCompareWeekDays.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareWeekDays.UseVisualStyleBackColor = false;
            rdoCompareWeekDays.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // rdoCompareMonths
            // 
            rdoCompareMonths.Appearance = Appearance.Button;
            rdoCompareMonths.BackColor = SystemColors.ButtonShadow;
            rdoCompareMonths.FlatAppearance.BorderSize = 0;
            rdoCompareMonths.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 130, 180);
            rdoCompareMonths.FlatStyle = FlatStyle.Flat;
            rdoCompareMonths.ForeColor = Color.White;
            rdoCompareMonths.Location = new Point(3, 33);
            rdoCompareMonths.Name = "rdoCompareMonths";
            rdoCompareMonths.Size = new Size(87, 24);
            rdoCompareMonths.TabIndex = 21;
            rdoCompareMonths.Text = "Months";
            rdoCompareMonths.TextAlign = ContentAlignment.MiddleCenter;
            rdoCompareMonths.UseVisualStyleBackColor = false;
            rdoCompareMonths.CheckedChanged += rdoChartDisplayType_CheckChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(1125, 67);
            label7.Name = "label7";
            label7.Size = new Size(90, 25);
            label7.TabIndex = 15;
            label7.Text = "Compare";
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.FromArgb(225, 225, 225);
            pnlContainer.Controls.Add(lblAreaDisplay);
            pnlContainer.Controls.Add(panel1);
            pnlContainer.Controls.Add(button1);
            pnlContainer.Controls.Add(cartesianChart1);
            pnlContainer.Controls.Add(dataGridView1);
            pnlContainer.Controls.Add(dgvAreaStats);
            pnlContainer.Controls.Add(panel7);
            pnlContainer.Controls.Add(pnlServerSelect);
            pnlContainer.Controls.Add(btnBoxChart);
            pnlContainer.Controls.Add(pnlFilters);
            pnlContainer.Controls.Add(label7);
            pnlContainer.Controls.Add(btnAreaGraph);
            pnlContainer.Controls.Add(flowDiningAreas);
            pnlContainer.Controls.Add(btnLineGraph);
            pnlContainer.Location = new Point(12, 12);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1477, 1018);
            pnlContainer.TabIndex = 23;
            pnlContainer.Paint += pnlContainer_Paint;
            // 
            // lblAreaDisplay
            // 
            lblAreaDisplay.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblAreaDisplay.Location = new Point(36, 34);
            lblAreaDisplay.Name = "lblAreaDisplay";
            lblAreaDisplay.Size = new Size(1074, 30);
            lblAreaDisplay.TabIndex = 23;
            lblAreaDisplay.Text = "All Areas";
            lblAreaDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 190, 200);
            ClientSize = new Size(1501, 1042);
            ControlBox = false;
            Controls.Add(pnlContainer);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "frmSalesStats";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmSalesStats";
            Load += frmSalesStats_Load;
            panel1.ResumeLayout(false);
            pnlServerSelect.ResumeLayout(false);
            pnlServerSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAreaStats).EndInit();
            panel7.ResumeLayout(false);
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RadioButton rdoDiningAreaSales;
        private RadioButton rdoServerShifts;
        private Panel panel1;
        private Panel pnlServerSelect;
        private ComboBox cboServerSelect;
        private Label lblComboLabel;
        private Button btnIndividualStats;
        private Button btnIndividualServerShifts;
        private DataGridView dataGridView1;
        private FlowLayoutPanel flowDiningAreas;
        private DataGridView dgvAreaStats;

        private Button button1;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private Button btnLineGraph;
        private Button btnAreaGraph;
        private Button btnBoxChart;
        private Panel pnlFilters;
        private RadioButton rdoCompareDates;
        private Panel panel7;
        private RadioButton rdoCompareReservations;
        private RadioButton rdoCompareTemp;
        private RadioButton rdoCompareRain;
        private RadioButton rdoCompareWeekDays;
        private RadioButton rdoCompareMonths;
        private Label label7;
        private RadioButton rdoCompareClouds;
        private RadioButton rdoCompareWind;
        private Panel pnlContainer;
        private RadioButton rdoDistribution;
        private Label lblAreaDisplay;
        //private SplitContainer splitContainer1;
    }
}