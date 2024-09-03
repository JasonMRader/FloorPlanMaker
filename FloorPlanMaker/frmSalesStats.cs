using FloorplanClassLibrary;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using FloorplanUserControlLibrary;
//using MediaGeometry = System.Windows.Media;

//using System.Windows.Media;

namespace FloorPlanMakerUI
{
    public partial class frmSalesStats : Form
    {
        public frmSalesStats()
        {
            InitializeComponent();
            shiftFilterControl = new ShiftFilterControl();
            shiftFilterControl.Location = new Point(5,5);
            shiftAnalysis.SetStandardFiltersForDateAndShiftType(false, DateOnly.FromDateTime(DateTime.Today));
            pnlFilters.Controls.Add(shiftFilterControl);
            shiftFilterControl.SetShiftAnalysis(shiftAnalysis);
            shiftFilterControl.UpdateShift += PopulateUI;
        }
        public frmSalesStats(ShiftFilterControl shiftFilterControl)
        {
            InitializeComponent();
            this.shiftFilterControl = new ShiftFilterControl();
            this.shiftFilterControl.SetShiftAnalysis(shiftFilterControl.ShiftAnalysis);
            this.shiftFilterControl.Location = new Point(5, 5);
            this.shiftAnalysis = shiftFilterControl.ShiftAnalysis;
            //shiftAnalysis.SetStandardFiltersForDateAndShiftType(false, DateOnly.FromDateTime(DateTime.Today));
            pnlFilters.Controls.Add(this.shiftFilterControl);
            //shiftFilterControl.SetShiftAnalysis(shiftAnalysis);
            this.shiftFilterControl.UpdateShift += PopulateUI;
        }

        private ShiftFilterControl shiftFilterControl;
        private DiningAreaManager areaManager = new DiningAreaManager();
        private EmployeeManager employeeManager = new EmployeeManager();       
        private List<WeatherData> allWeatherData = new List<WeatherData>();       
        private ShiftAnalysis shiftAnalysis = new ShiftAnalysis();
        private void frmSalesStats_Load(object sender, EventArgs e)
        {
            allWeatherData = SqliteDataAccess.LoadAllWeatherData();
            employeeManager.LoadShiftsForActiveServers();
            flowDiningAreas.Controls.Add(CreateSelectAllAreaRadio());
            foreach (DiningArea area in areaManager.DiningAreas) {
                flowDiningAreas.Controls.Add(CreateAreaRadio(area));
            }           
        }
        private RadioButton CreateSelectAllAreaRadio()
        {
            RadioButton btn = new RadioButton() {

                Text = "ALL",
                Size = new Size(flowDiningAreas.Width / (areaManager.DiningAreas.Count + 1), flowDiningAreas.Height),
                Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.Click += SelectedAllAreasButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.Font = UITheme.CustomFont(14, FontStyle.Bold);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = UITheme.DarkenColor(.3f, UITheme.ButtonColor);
            btn.Checked = true;
           
            return btn;
        }

        private void SelectedAllAreasButtonClicked(object? sender, EventArgs e)
        {

        }

        private RadioButton CreateAreaRadio(DiningArea area)
        {
            RadioButton btn = new RadioButton() {

                Image = UITheme.GetDiningAreaImage(area),
                Size = new Size(flowDiningAreas.Width / (areaManager.DiningAreas.Count + 1), flowDiningAreas.Height),
                Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                Tag = area
            };
            btn.Click += areaButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = UITheme.DarkenColor(.3f, UITheme.ButtonColor);
            //toolTip.SetToolTip(btn, area.Name);
            return btn;
        }

        private void PopulateDGVForServerHistory(List<ServerShiftHistory> serverHistory)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            //// Add columns for each dining area and total sales
            dataGridView1.Columns.Add("Server", "Server");

            var shiftColumn = new DataGridViewTextBoxColumn {
                Name = "ShiftCount",
                HeaderText = "Shifts",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "0"
                }
            };
            var outsideCountColumn = new DataGridViewTextBoxColumn {
                Name = "OutsideCount",
                HeaderText = "Outside",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "0"
                }
            };
            var outsidePercentageColumn = new DataGridViewTextBoxColumn {
                Name = "OutsidePercentage",
                HeaderText = "Outside %",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "P0"
                }
            };
            var cocktailCountColumn = new DataGridViewTextBoxColumn {
                Name = "CocktailCount",
                HeaderText = "Cocktail",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "0"
                }
            };
            var cocktailPercentageColumn = new DataGridViewTextBoxColumn {
                Name = "CocktailPercentage",
                HeaderText = "Cocktail %",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "P0"
                }
            };
            var closeCountColumn = new DataGridViewTextBoxColumn {
                Name = "CloseCount",
                HeaderText = "Close",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "0"
                }
            };
            var closePercentageColumn = new DataGridViewTextBoxColumn {
                Name = "closePercentage",
                HeaderText = "close %",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "P0"
                }
            };
            var TeamCountColumn = new DataGridViewTextBoxColumn {
                Name = "TeamCount",
                HeaderText = "Team",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "0"
                }
            };
            var TeamPercentageColumn = new DataGridViewTextBoxColumn {
                Name = "TeamPercentage",
                HeaderText = "Team %",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "P0"
                }
            };

            dataGridView1.Columns.Add(shiftColumn);
            dataGridView1.Columns.Add(outsideCountColumn);
            dataGridView1.Columns.Add(outsidePercentageColumn);
            dataGridView1.Columns.Add(cocktailCountColumn);
            dataGridView1.Columns.Add(cocktailPercentageColumn);
            dataGridView1.Columns.Add(closeCountColumn);
            dataGridView1.Columns.Add(closePercentageColumn);
            dataGridView1.Columns.Add(TeamCountColumn);
            dataGridView1.Columns.Add(TeamPercentageColumn);



            //dataGridView1.Columns.Add(totalColumn);

            // Add rows for each date's sales data
            foreach (var history in serverHistory) {
                var row = new List<object> { history.Server.ToString() };
                row.Add(history.filteredShifts.Count);
                row.Add(history.OutsideShiftCount);
                row.Add(history.OutsidePercentage);
                row.Add(history.CocktailShiftCount);
                row.Add(history.CocktailShiftPercentage);
                row.Add(history.ClosingShiftCount);
                row.Add(history.ClosingPercentage);
                row.Add(history.TeamWaitShiftCount);
                row.Add(history.TeamWaitPercentage);


                dataGridView1.Rows.Add(row.ToArray());
            }

        }


        private List<ServerShiftHistory> GetServerHistory(List<Server> servers)
        {
            var serverHistorys = new List<ServerShiftHistory>();
            foreach (var server in servers) {
                var serverShiftHistory = new ServerShiftHistory(server, shiftAnalysis.StartDate, shiftAnalysis.EndDate, shiftAnalysis.IsAM, shiftAnalysis.FilteredDaysOfWeek);
                serverHistorys.Add(serverShiftHistory);
            }
            return serverHistorys;
        }



        private void rdoServerShifts_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked) {
                cboServerSelect.DataSource = employeeManager.ActiveServers;
                lblComboLabel.Text = "Servers";
                btnIndividualStats.Text = "Server Table History";
                btnIndividualServerShifts.Visible = true;
                pnlServerSelect.Visible = true;
            }
            else {
                cboServerSelect.DataSource = areaManager.DiningAreas;
                lblComboLabel.Text = "Dining Areas";
                btnIndividualStats.Text = "Area Table History";
                btnIndividualServerShifts.Visible = false;
                pnlServerSelect.Visible = false;
            }
        }
        
        private void btnIndividualStats_Click(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked) {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForIndividualServer(serverSelected);
            }
        }
        private void PopulateDGVForIndividualServer(Server serverSelected)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            var serverShiftHistory = new ServerShiftHistory(serverSelected, shiftAnalysis.StartDate, shiftAnalysis.EndDate, shiftAnalysis.IsAM, shiftAnalysis.FilteredDaysOfWeek);
            dataGridView1.Columns.Add("Server", "Server");

            var numericTableCounts = serverShiftHistory.TableCounts
                .Where(kvp => int.TryParse(kvp.Key, out _))
                .OrderBy(kvp => int.Parse(kvp.Key))
                .ToList();

            var nonNumericTableCounts = serverShiftHistory.TableCounts
                .Where(kvp => !int.TryParse(kvp.Key, out _))
                .OrderBy(kvp => kvp.Key)
                .ToList();

           
            var sortedTableCounts = numericTableCounts.Concat(nonNumericTableCounts);

            foreach (var table in sortedTableCounts) {
                var column = new DataGridViewTextBoxColumn {
                    Name = table.Key,
                    HeaderText = table.Key,
                    DefaultCellStyle = new DataGridViewCellStyle {
                        Format = "N0"
                    }
                };
                dataGridView1.Columns.Add(column);
            }

            var row = new List<object> { serverShiftHistory.Server.ToString() };
            foreach (var table in sortedTableCounts) {
                row.Add(table.Value);
            }

            dataGridView1.Rows.Add(row.ToArray());
        }

        private void btnIndividualServerShifts_Click(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked) {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForServerShiftHistory(serverSelected);
            }
        }
        public void PopulateDGVForServerShiftHistory(Server serverSelected)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Date", "Date");
           
            var serverShiftHistory = new ServerShiftHistory(serverSelected, shiftAnalysis.StartDate, shiftAnalysis.EndDate, shiftAnalysis.IsAM, shiftAnalysis.FilteredDaysOfWeek);

            var tablesColumn = new DataGridViewTextBoxColumn {
                Name = "Tables",
                HeaderText = "Tables",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "C0" // Format as currency with no decimals
                }
            };
            dataGridView1.Columns.Add(tablesColumn);

            
            foreach (var empShift in serverShiftHistory.filteredShifts) {
                var row = new List<object> { empShift.Date.ToString("ddd, M/d") };


                row.Add(serverShiftHistory.ShiftTables[empShift]);



                dataGridView1.Rows.Add(row.ToArray());
            }
        }
        
        private void PopulateUI()
        {
            frmLoading loadingForm = new frmLoading("Parsing");
            loadingForm.Show();
            this.Enabled = false;
            if (rdoDiningAreaSales.Checked) {
                Task.Run(() => {
                    shiftAnalysis.InitializetShiftsForDateRange();

                    this.Invoke(new Action(() => {
                        
                        PopulateDGVForAreaSales(dataGridView1, areaManager.DiningAreas, shiftAnalysis.FilteredShifts);
                        PopulateAveragesDataGridView(areaManager.DiningAreas);
                       
                        
                        loadingForm.Close();
                       
                        UpdateNewChart();
                        this.Enabled = true;

                        this.BringToFront();

                    }));
                });

            }
            if (rdoServerShifts.Checked) {
                Task.Run(() => {
                    
                    List<ServerShiftHistory> serverHistory = GetServerHistory(employeeManager.ActiveServers);

                    this.Invoke(new Action(() => {
                        // Close the loading form and re-enable the main form

                        loadingForm.Close();
                        this.Enabled = true;
                        PopulateDGVForServerHistory(serverHistory);
                        this.BringToFront();


                    }));
                });

            }
        }
        private void UpdateNewChart()
        {
            var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);          
            chartManager.SetUpStackedArea(areaManager.DiningAreas);
        }



        public void PopulateDGVForAreaSales(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<ShiftRecord> shiftRecords)
        {
            InitializeDataGridView(dgvDiningAreas, diningAreas);
            AddShiftRows(dgvDiningAreas, diningAreas, shiftRecords);
        }
        private void InitializeDataGridView(DataGridView dgvDiningAreas, List<DiningArea> diningAreas)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();

            dgvDiningAreas.Columns.Add("Date", "Date");
            var eventColumn = new DataGridViewTextBoxColumn {
                Name = "Event",
                HeaderText = "Event",
                Width = 60,

            };
            dgvDiningAreas.Columns.Add(eventColumn);


            foreach (var diningArea in diningAreas) {
                var column = new DataGridViewTextBoxColumn {
                    Name = diningArea.Name,
                    HeaderText = diningArea.Name,
                    Width = 70,
                    DefaultCellStyle = new DataGridViewCellStyle {
                        Format = "C0" 
                    }
                };
                dgvDiningAreas.Columns.Add(column);
            }

            var totalColumn = new DataGridViewTextBoxColumn {
                Name = "Total",
                HeaderText = "Total",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "C0" 
                }
            };
            dgvDiningAreas.Columns.Add(totalColumn);

            var tempHiColumn = new DataGridViewTextBoxColumn {
                Name = "TempHi",
                HeaderText = "Feels Like Hi",
                Width = 30,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" 
                }
            };
            dgvDiningAreas.Columns.Add(tempHiColumn);
            var tempAvgColumn = new DataGridViewTextBoxColumn {
                Name = "TempAvg",
                HeaderText = "Feels Like Avg",
                Width = 30,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" 
                }
            };
            dgvDiningAreas.Columns.Add(tempAvgColumn);

            var tempLoColumn = new DataGridViewTextBoxColumn {
                Name = "TempLo",
                HeaderText = "Feels Like Low",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" 
                }
            };
            dgvDiningAreas.Columns.Add(tempLoColumn);
            var rainColumn = new DataGridViewTextBoxColumn {
                Name = "rain",
                HeaderText = "Rain",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N2"
                }
            };
            dgvDiningAreas.Columns.Add(rainColumn);
            var cloudsColumn = new DataGridViewTextBoxColumn {
                Name = "clouds",
                HeaderText = "Clouds",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N2" 
                }
            };
            dgvDiningAreas.Columns.Add(cloudsColumn);
            var windAvgColumn = new DataGridViewTextBoxColumn {
                Name = "windAvg",
                HeaderText = "Wind Avg",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" 
                }
            };
            dgvDiningAreas.Columns.Add(windAvgColumn);
            var windMaxColumn = new DataGridViewTextBoxColumn {
                Name = "windMax",
                HeaderText = "Wind Max",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" 
                }
            };
            dgvDiningAreas.Columns.Add(windMaxColumn);
        }

        
        private void AddShiftRows(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<ShiftRecord> shiftRecords)
        {
            foreach (var shiftRecord in shiftRecords) {
                var row = new DataGridViewRow();
                row.CreateCells(dgvDiningAreas);               
                row.Cells[0].Value = shiftRecord.Date.ToString("ddd, M/d/yy");               
                row.Cells[1].Value = shiftRecord.SpecialEventDate?.Name ?? "";
                int cellIndex = 2; 
              
                foreach (var diningArea in diningAreas) {
                    var diningAreaRecord = shiftRecord.DiningAreaRecords
                        .FirstOrDefault(fp => fp.DiningAreaID == diningArea.ID);

                    float diningAreaSales = diningAreaRecord?.Sales ?? 0f;
                    float percentageOfSales = diningAreaRecord?.PercentageOfSales ?? 0f;

                    row.Cells[cellIndex].Value = diningAreaSales;

                    
                    row.Cells[cellIndex].ToolTipText = $"{percentageOfSales:F1}% of total sales";

                    cellIndex++;
                }
                row.Cells[cellIndex].Value = shiftRecord.Sales;
               
                cellIndex++;
                if (shiftRecord.ShiftWeather != null) {
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.FeelsLikeHi ?? 0;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.FeelsLikeAvg ?? 0;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.FeelsLikeLow ?? 0;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.RainAmount ?? 0f;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.CloudCoverAverage ?? 0f;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.WindAvg ?? 0;
                    row.Cells[cellIndex++].Value = shiftRecord.ShiftWeather?.WindMax ?? 0;
                }
              
                dgvDiningAreas.Rows.Add(row);
            }
        }

        public void PopulateAveragesDataGridView(List<DiningArea> diningAreas)
        {            
            dgvAreaStats.Columns.Clear();
            dgvAreaStats.Rows.Clear();
            
            foreach (var stat in shiftAnalysis.DiningAreaStats) {
                DiningArea area = diningAreas.FirstOrDefault(d => d.ID == stat.DiningAreaID);
                string columnHead = $"Area {stat.DiningAreaID}";
                if (area != null) {
                    columnHead = area.Name;
                }
                dgvAreaStats.Columns.Add($"Area_{stat.DiningAreaID}", columnHead);
            }
            dgvAreaStats.Columns.Add("Total", "Total");

            var totalStats = new DiningAreaStats {
                MaxSales = shiftAnalysis.FilteredShiftMaxSales,
                MinSales = shiftAnalysis.FilteredShiftMinSales,
                AvgSales = shiftAnalysis.FilteredShiftAvgSales,

            };
           
            var avgRow = new DataGridViewRow();
            avgRow.CreateCells(dgvAreaStats);
            var minRow = new DataGridViewRow();
            minRow.CreateCells(dgvAreaStats);
            var maxRow = new DataGridViewRow();
            maxRow.CreateCells(dgvAreaStats);
            var percRow = new DataGridViewRow();
            percRow.CreateCells(dgvAreaStats);
            var minPercentRow = new DataGridViewRow();
            minPercentRow.CreateCells(dgvAreaStats);
            var maxPercentRow = new DataGridViewRow();
            maxPercentRow.CreateCells(dgvAreaStats);
          
            for (int i = 0; i < shiftAnalysis.DiningAreaStats.Count; i++) {
                avgRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].AvgSales:C0}";

                minRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].MinSales:C0}";
                maxRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].MaxSales:C0}";
                percRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].PercentageOfTotalSales:F1}%";
                minPercentRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].MinPercentage:F1}%";
                maxPercentRow.Cells[i].Value = $"{shiftAnalysis.DiningAreaStats[i].MaxPercentage:F1}%";
            }
                       
            avgRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = totalStats.AvgSales;
            minRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = totalStats.MinSales;
            maxRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = totalStats.MaxSales;
            percRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = "";
            minPercentRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = "";
            maxPercentRow.Cells[shiftAnalysis.DiningAreaStats.Count].Value = "";

           
            foreach (DataGridViewRow row in new DataGridViewRow[] { avgRow, minRow, maxRow }) {
                for (int i = 0; i <= shiftAnalysis.DiningAreaStats.Count; i++) {
                    row.Cells[i].Style.Format = "C"; 
                }
            }

           
            avgRow.HeaderCell.Value = "Average";
            minRow.HeaderCell.Value = "Min";
            maxRow.HeaderCell.Value = "Max";
            percRow.HeaderCell.Value = "%";
            minPercentRow.HeaderCell.Value = "Min %";
            maxPercentRow.HeaderCell.Value = "Max %";
            dgvAreaStats.RowHeadersWidth = 100;

            dgvAreaStats.Rows.Add(avgRow);
            dgvAreaStats.Rows.Add(minRow);
            dgvAreaStats.Rows.Add(maxRow);
            dgvAreaStats.Rows.Add(percRow);
            dgvAreaStats.Rows.Add(minPercentRow);
            dgvAreaStats.Rows.Add(maxPercentRow);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnLineGraph_Click(object sender, EventArgs e)
        {
            var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);
            chartManager.SetupLineChart(areaManager.DiningAreas);
        }

        private void btnAreaGraph_Click(object sender, EventArgs e)
        {
            var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);
            chartManager.SetUpStackedArea(areaManager.DiningAreas);
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);
            chartManager.SetUpBarChart(1);
        }
        private void areaButtonClicked(object? sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            DiningArea area = (DiningArea)radioButton.Tag;
            if (radioButton.Checked) {
                var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);
                chartManager.SetUpBarChart(area.ID);
            }

        }
        private void btnBoxChart_Click(object sender, EventArgs e)
        {
            var chartManager = new ChartManager(shiftAnalysis.FilteredShifts, cartesianChart1);
            chartManager.SetUpBoxChart(areaManager.DiningAreas);
        }
    }
}
