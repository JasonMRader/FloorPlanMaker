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

namespace FloorPlanMakerUI
{
    public partial class frmSalesStats : Form
    {
        public frmSalesStats()
        {
            InitializeComponent();
        }
        private DiningAreaManager areaManager = new DiningAreaManager();

        private EmployeeManager employeeManager = new EmployeeManager();
        private DateOnly dateOnlyStart = new DateOnly();
        private DateOnly dateOnlyEnd = new DateOnly();
        private List<WeatherData> allWeatherData = new List<WeatherData>();
        private int MinFeelsLikeHi = -150;
        private int MaxFeelsLikeHi = 150;
        private ShiftAnalysis shiftAnalysis = new ShiftAnalysis();


        private List<DayOfWeek> FilteredDaysOfWeek = new List<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
        //private List<int> FilteredMonths = new List<int>
        //{
        //   1,2,3,4,5,6,7,8,9,10,11,12
        //};

        private void frmSalesStats_Load(object sender, EventArgs e)
        {

            allWeatherData = SqliteDataAccess.LoadAllWeatherData();
            dtpEndDate.Value = DateTime.Now.AddDays(-1);
            dtpStartDate.Value = DateTime.Now.AddDays(-30);
            employeeManager.LoadShiftsForActiveServers();
            flowDiningAreas.Controls.Add(CreateSelectAllAreaRadio());
            foreach (DiningArea area in areaManager.DiningAreas) {
                flowDiningAreas.Controls.Add(CreateAreaRadio(area));
            }
            //List<ServerShiftHistory> history = new List<ServerShiftHistory>();
            //foreach (Server server in employeeManager.ActiveServers)
            //{
            //    ServerShiftHistory shiftHistory = new ServerShiftHistory(server);
            //    history.Add(shiftHistory);
            //}
        }
        private RadioButton CreateSelectAllAreaRadio()
        {
            RadioButton btn = new RadioButton() {

                Text = "ALL",
                Size = new Size(flowDiningAreas.Width / (areaManager.DiningAreas.Count + 1), flowDiningAreas.Height),
                Margin = new Padding(0, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.Click += SelectedAllAreasButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.Font = UITheme.CustomFont(14, FontStyle.Bold);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = UITheme.DarkenColor(.3f, UITheme.ButtonColor);
            btn.Checked = true;
            //toolTip.SetToolTip(btn, area.Name);
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
                Margin = new Padding(0, 0, 0, 0),
                Tag = area


            };
            btn.Click += areaButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = UITheme.DarkenColor(.3f, UITheme.ButtonColor);
            //toolTip.SetToolTip(btn, area.Name);
            return btn;
        }

        private void areaButtonClicked(object? sender, EventArgs e)
        {

        }

        public List<DateTime> GetFilteredDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dateList = new List<DateTime>();

            if (startDate <= endDate) {
                DateTime currentDate = startDate;
                while (currentDate <= endDate) {
                    var weatherDataForDate = allWeatherData.FirstOrDefault(wd => wd.DateOnly == DateOnly.FromDateTime(currentDate));

                    if (weatherDataForDate != null &&
                        FilteredDaysOfWeek.Contains(currentDate.DayOfWeek) &&
                        //FilteredMonths.Contains(currentDate.Month) &&
                        weatherDataForDate.FeelsLikeHi >= MinFeelsLikeHi &&
                        weatherDataForDate.FeelsLikeHi <= MaxFeelsLikeHi &&
                        cbFilterByTempRange.Checked) {
                        dateList.Add(currentDate);
                    }
                    if (!cbFilterByTempRange.Checked &&
                        FilteredDaysOfWeek.Contains(currentDate.DayOfWeek))
                        //FilteredMonths.Contains(currentDate.Month)) {
                        {
                        dateList.Add(currentDate);
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            else {
                MessageBox.Show("The start date must be earlier than or equal to the end date.");
            }

            return dateList;
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
            if (rdoAm.Checked) {
                foreach (var server in servers) {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, true, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
            }
            if (rdoPm.Checked) {
                foreach (var server in servers) {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, false, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
            }
            if (rdoBoth.Checked) {
                foreach (var server in servers) {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
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
            }
            else {
                cboServerSelect.DataSource = areaManager.DiningAreas;
                lblComboLabel.Text = "Dining Areas";
                btnIndividualStats.Text = "Area Table History";
                btnIndividualServerShifts.Visible = false;
            }
        }

        private void rdoEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSpecialAndNormal.Checked) {
                shiftAnalysis.SetIsFilteredBySpecialEvent(false);
            }
            else if (rdoExcludeEvents.Checked) {
                shiftAnalysis.SetIsFilteredBySpecialEvent(true);
                shiftAnalysis.SetSpecialEvents(false);
            }
            else if (rdoEventsOnly.Checked) {
                shiftAnalysis.SetIsFilteredBySpecialEvent(true);
                shiftAnalysis.SetSpecialEvents(true);
            }
        }

        private void cbAllWeekdays_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllWeekdays.Checked) {
                cbMon.Checked = true;
                cbTues.Checked = true;
                cbWed.Checked = true;
                cbThurs.Checked = true;
                cbFri.Checked = true;
                cbSat.Checked = true;
                cbSun.Checked = true;
                cbAllWeekdays.Text = "No Days";
            }
            else {
                cbMon.Checked = false;
                cbTues.Checked = false;
                cbWed.Checked = false;
                cbThurs.Checked = false;
                cbFri.Checked = false;
                cbSat.Checked = false;
                cbSun.Checked = false;
                cbAllWeekdays.Text = "All Days";
            }

        }

        private void cbDayOfWeek_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            DayOfWeek day = DayOfWeek.Monday; // Default value, will be overwritten

            if (cb == cbMon) day = DayOfWeek.Monday;
            else if (cb == cbTues) day = DayOfWeek.Tuesday;
            else if (cb == cbWed) day = DayOfWeek.Wednesday;
            else if (cb == cbThurs) day = DayOfWeek.Thursday;
            else if (cb == cbFri) day = DayOfWeek.Friday;
            else if (cb == cbSat) day = DayOfWeek.Saturday;
            else if (cb == cbSun) day = DayOfWeek.Sunday;

            if (cb.Checked) {
                shiftAnalysis.AddDayOfWeek(day);
            }
            else {
                shiftAnalysis.RemoveDayOfWeek(day);
            }
            bool allChecked = cbMon.Checked && cbTues.Checked && cbWed.Checked && cbThurs.Checked &&
                              cbFri.Checked && cbSat.Checked && cbSun.Checked;


            shiftAnalysis.SetIsFilteredByDayOfWeek(!allChecked);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateOnlyStart = new DateOnly(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day);
            shiftAnalysis.SetDateOnly(dateOnlyStart, dateOnlyEnd);
        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateOnlyEnd = new DateOnly(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day);
            shiftAnalysis.SetDateOnly(dateOnlyStart, dateOnlyEnd);
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
            var serverShiftHistory = new ServerShiftHistory();
            if (rdoBoth.Checked) {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);
            }
            else {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, rdoAm.Checked, FilteredDaysOfWeek);
            }



            dataGridView1.Columns.Add("Server", "Server");
            var numericTableCounts = serverShiftHistory.TableCounts
                .Where(kvp => int.TryParse(kvp.Key, out _))
                .OrderBy(kvp => int.Parse(kvp.Key))
                .ToList();

            var nonNumericTableCounts = serverShiftHistory.TableCounts
                .Where(kvp => !int.TryParse(kvp.Key, out _))
                .OrderBy(kvp => kvp.Key)
                .ToList();

            // Combine the sorted numeric and non-numeric lists
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

            // Add columns for each dining area and total sales
            dataGridView1.Columns.Add("Date", "Date");
            var serverShiftHistory = new ServerShiftHistory();
            if (rdoBoth.Checked) {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);

            }
            else {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, rdoAm.Checked, FilteredDaysOfWeek);
            }



            var tablesColumn = new DataGridViewTextBoxColumn {
                Name = "Tables",
                HeaderText = "Tables",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "C0" // Format as currency with no decimals
                }
            };
            dataGridView1.Columns.Add(tablesColumn);

            // Add rows for each date's sales data
            foreach (var empShift in serverShiftHistory.filteredShifts) {
                var row = new List<object> { empShift.Date.ToString("ddd, M/d") };


                row.Add(serverShiftHistory.ShiftTables[empShift]);



                dataGridView1.Rows.Add(row.ToArray());
            }
        }



        private void cbAllMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllMonths.Checked) {
                cbJan.Checked = true;
                cbFeb.Checked = true;
                cbMar.Checked = true;
                cbApr.Checked = true;
                cbMay.Checked = true;
                cbJun.Checked = true;
                cbJul.Checked = true;
                cbAug.Checked = true;
                cbSep.Checked = true;
                cbOct.Checked = true;
                cbNov.Checked = true;
                cbDec.Checked = true;
                cbAllMonths.Text = "No Months";
            }
            if (!cbAllMonths.Checked) {
                cbJan.Checked = false;
                cbFeb.Checked = false;
                cbMar.Checked = false;
                cbApr.Checked = false;
                cbMay.Checked = false;
                cbJun.Checked = false;
                cbJul.Checked = false;
                cbAug.Checked = false;
                cbSep.Checked = false;
                cbOct.Checked = false;
                cbNov.Checked = false;
                cbDec.Checked = false;
                cbAllMonths.Text = "All Months";
            }
        }

        private void cbMonth_CheckChanged(object sender, System.EventArgs e)
        {

            UpdateMonthFilter(cbJan.Checked, 1);
            UpdateMonthFilter(cbFeb.Checked, 2);
            UpdateMonthFilter(cbMar.Checked, 3);
            UpdateMonthFilter(cbApr.Checked, 4);
            UpdateMonthFilter(cbMay.Checked, 5);
            UpdateMonthFilter(cbJun.Checked, 6);
            UpdateMonthFilter(cbJul.Checked, 7);
            UpdateMonthFilter(cbAug.Checked, 8);
            UpdateMonthFilter(cbSep.Checked, 9);
            UpdateMonthFilter(cbOct.Checked, 10);
            UpdateMonthFilter(cbNov.Checked, 11);
            UpdateMonthFilter(cbDec.Checked, 12);

            bool allChecked = cbJan.Checked && cbFeb.Checked && cbMar.Checked && cbApr.Checked &&
                              cbMay.Checked && cbJun.Checked && cbJul.Checked && cbAug.Checked &&
                              cbSep.Checked && cbOct.Checked && cbNov.Checked && cbDec.Checked;

            shiftAnalysis.SetIsFilteredByMonth(!allChecked);
        }
        private void UpdateMonthFilter(bool isChecked, int month)
        {
            if (isChecked) {
                shiftAnalysis.AddMonth(month);
            }
            else {
                shiftAnalysis.RemoveMonth(month);
            }
        }


        private void btnRefreshFilters_Click(object sender, EventArgs e)
        {
            frmLoading loadingForm = new frmLoading("Parsing");
            loadingForm.Show();
            this.Enabled = false;
            if (rdoDiningAreaSales.Checked) {
                Task.Run(() => {
                    shiftAnalysis.InitializetShiftsForDateRange();

                    this.Invoke(new Action(() => {
                        // Close the loading form and re-enable the main form
                        PopulateDGVForAreaSales(dataGridView1, areaManager.DiningAreas, shiftAnalysis.FilteredShifts);
                        loadingForm.Close();
                        this.Enabled = true;

                        this.BringToFront();

                    }));
                });

            }
            if (rdoServerShifts.Checked) {
                Task.Run(() => {
                    List<DateTime> dateList = GetFilteredDates(dtpStartDate.Value, dtpEndDate.Value);
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
                        Format = "C0" // Format as currency with no decimals
                    }
                };
                dgvDiningAreas.Columns.Add(column);
            }

            var totalColumn = new DataGridViewTextBoxColumn {
                Name = "Total",
                HeaderText = "Total",
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "C0" // Format as currency with no decimals
                }
            };
            dgvDiningAreas.Columns.Add(totalColumn);

            var tempHiColumn = new DataGridViewTextBoxColumn {
                Name = "TempHi",
                HeaderText = "Feels Like Hi",
                Width = 30,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(tempHiColumn);
            var tempAvgColumn = new DataGridViewTextBoxColumn {
                Name = "TempAvg",
                HeaderText = "Feels Like Avg",
                Width = 30,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(tempAvgColumn);

            var tempLoColumn = new DataGridViewTextBoxColumn {
                Name = "TempLo",
                HeaderText = "Feels Like Low",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" // Format as integer
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
                    Format = "N2" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(cloudsColumn);
            var windAvgColumn = new DataGridViewTextBoxColumn {
                Name = "windAvg",
                HeaderText = "Wind Avg",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(windAvgColumn);
            var windMaxColumn = new DataGridViewTextBoxColumn {
                Name = "windMax",
                HeaderText = "Wind Max",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle {
                    Format = "N0" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(windMaxColumn);
        }

        private void AddShiftRows(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<ShiftRecord> shiftRecords)
        {
            foreach (var shiftRecord in shiftRecords) {
                var row = new List<object> { shiftRecord.Date.ToString("ddd, M/d/yy") };

                if (shiftRecord.SpecialEventDate != null) {
                    row.Add(shiftRecord.SpecialEventDate.Name);
                }
                else {
                    row.Add("");
                }

                foreach (var diningArea in diningAreas) {
                    float diningAreaSales = shiftRecord.DiningAreaRecords
                        .Where(fp => fp.DiningAreaID == diningArea.ID)
                        .Sum(fp => fp.Sales);

                    row.Add(diningAreaSales);
                }

                row.Add(shiftRecord.Sales);

                //int feelsLikeHi = shiftRecord.HourlyWeatherData.Any() ?
                //                  shiftRecord.HourlyWeatherData.Max(hw => hw.FeelsLikeHi) :
                //                  0;
                int feelsLikeHi = 0;
                int feelsLikeAvg = 0;
                int feelsLikeLo = 0;
                float rainAmount = 0f;
                float clouds = 0f;
                int maxWind = 0;
                int avgWind = 0;

                if (shiftRecord.ShiftWeather != null) {
                    feelsLikeHi = shiftRecord.ShiftWeather.FeelsLikeHi;
                    feelsLikeAvg = shiftRecord.ShiftWeather.FeelsLikeAvg;
                    feelsLikeLo = shiftRecord.ShiftWeather.FeelsLikeLow;
                    rainAmount = shiftRecord.ShiftWeather.RainAmount;
                    clouds = shiftRecord.ShiftWeather.CloudCoverAverage;
                    maxWind = shiftRecord.ShiftWeather.WindMax;
                    avgWind = shiftRecord.ShiftWeather.WindAvg;
                }

                row.Add(feelsLikeHi);
                row.Add(feelsLikeAvg);
                row.Add(feelsLikeLo);
                row.Add(rainAmount);
                row.Add(clouds);
                row.Add(avgWind);
                row.Add(maxWind);


                dgvDiningAreas.Rows.Add(row.ToArray());
            }
        }

        private void cboServerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cbFilterByTempRange_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFilterByTempRange.Checked) {
                nudTempRange.Enabled = true;
                nudTempAnchor.Enabled = true;
                lblTo.Enabled = true;
                shiftAnalysis.SetIsFilteredByTemp(true);
                shiftAnalysis.SetTempRange((int)nudTempAnchor.Value, (int)nudTempRange.Value);
            }
            else {
                nudTempRange.Enabled = false;
                nudTempAnchor.Enabled = false;
                lblTo.Enabled = false;
                shiftAnalysis.SetIsFilteredByTemp(false);
            }
        }
        private void nudTemp_ValueChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetTempRange((int)nudTempAnchor.Value, (int)nudTempRange.Value);
        }
        private void cbFilterByRain_CheckedChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetIsFilteredbyRainAmount(cbFilterByRain.Checked);
            if (cbFilterByRain.Checked) {
                nudRainAnchor.Enabled = true;
                nudRainRange.Enabled = true;
                shiftAnalysis.SetRainRange((float)nudRainAnchor.Value, (float)nudRainRange.Value);
            }
            else {
                nudRainRange.Enabled = false;
                nudRainAnchor.Enabled = false;
            }
        }
        private void nudRain_ValueChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetRainRange((float)nudRainAnchor.Value, (float)nudRainRange.Value);
        }
        private void cbFilterByClouds_CheckedChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetIsFilteredByClouds(cbFilterByClouds.Checked);
            if (cbFilterByClouds.Checked) {
                nudCloudAnchor.Enabled = true;
                nudCloudRange.Enabled = true;
                shiftAnalysis.SetCloudRange((float)nudCloudAnchor.Value, (float)(nudCloudRange.Value));
            }
            else {
                nudCloudAnchor.Enabled = false;
                nudCloudRange.Enabled = false;
            }
        }
        private void nudCloud_ValueChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetCloudRange((float)nudCloudAnchor.Value, (float)(nudCloudRange.Value));
        }
        private void cbFilterByWindMax_CheckedChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetIsFilteredByWindMax(cbFilterByWindMax.Checked);
            if (cbFilterByWindMax.Checked) {
                nudWindMaxRange.Enabled = true;
                nudWindMaxAnchor.Enabled = true;
                shiftAnalysis.SetWindMaxRange((int)nudWindMaxAnchor.Value, (int)nudWindMaxRange.Value);
            }
            else {
                nudWindMaxRange.Enabled = false;
                nudWindMaxAnchor.Enabled = false;
            }
        }
        private void nudWindMax_ValueChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetWindMaxRange((int)nudWindMaxAnchor.Value, (int)nudWindMaxRange.Value);
        }
        private void cbFilterByWindAvg_CheckedChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetIsFilteredByWindAvg(cbFilterByWindAvg.Checked);
            if (cbFilterByWindAvg.Checked) {
                nudWindAvgAnchor.Enabled = true;
                nudWindAvgRange.Enabled = true;
                shiftAnalysis.SetWindAvgRange((int)nudWindAvgAnchor.Value, (int)nudWindAvgRange.Value);
            }
            else {
                nudWindAvgRange.Enabled = false;
                nudWindAvgAnchor.Enabled = false;
            }
        }
        private void nudWindAvg_ValueChanged(object sender, EventArgs e)
        {
            shiftAnalysis.SetWindAvgRange((int)nudWindAvgAnchor.Value, (int)nudWindAvgRange.Value);
        }
        private void rdoBoth_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoPm_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAm.Checked) {
                shiftAnalysis.SetIsAM(true);
            }
            else if (rdoPm.Checked) {

            }
            shiftAnalysis.SetIsAM(false);
        }

    }
}
