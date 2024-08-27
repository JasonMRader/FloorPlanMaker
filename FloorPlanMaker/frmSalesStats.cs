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

namespace FloorPlanMakerUI {
    public partial class frmSalesStats : Form {
        public frmSalesStats() {
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
        private List<int> FilteredMonths = new List<int>
        {
           1,2,3,4,5,6,7,8,9,10,11,12
        };

        private void frmSalesStats_Load(object sender, EventArgs e) {

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
        private RadioButton CreateSelectAllAreaRadio() {
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

        private void SelectedAllAreasButtonClicked(object? sender, EventArgs e) {

        }

        private RadioButton CreateAreaRadio(DiningArea area) {
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

        private void areaButtonClicked(object? sender, EventArgs e) {

        }

        public List<DateTime> GetFilteredDates(DateTime startDate, DateTime endDate) {
            List<DateTime> dateList = new List<DateTime>();

            if (startDate <= endDate) {
                DateTime currentDate = startDate;
                while (currentDate <= endDate) {
                    var weatherDataForDate = allWeatherData.FirstOrDefault(wd => wd.DateOnly == DateOnly.FromDateTime(currentDate));

                    if (weatherDataForDate != null &&
                        FilteredDaysOfWeek.Contains(currentDate.DayOfWeek) &&
                        FilteredMonths.Contains(currentDate.Month) &&
                        weatherDataForDate.FeelsLikeHi >= MinFeelsLikeHi &&
                        weatherDataForDate.FeelsLikeHi <= MaxFeelsLikeHi &&
                        cbFilterByTempRange.Checked) {
                        dateList.Add(currentDate);
                    }
                    if (!cbFilterByTempRange.Checked &&
                        FilteredDaysOfWeek.Contains(currentDate.DayOfWeek) &&
                        FilteredMonths.Contains(currentDate.Month)) {
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


        private void PopulateDGVForServerHistory(List<ServerShiftHistory> serverHistory) {
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
       

        private List<ServerShiftHistory> GetServerHistory(List<Server> servers) {
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

        private void rdoAm_CheckedChanged(object sender, EventArgs e) {

        }

        private void rdoServerShifts_CheckedChanged(object sender, EventArgs e) {
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

        private void rdoEvents_CheckedChanged(object sender, EventArgs e) {

        }

        private void cbAllWeekdays_CheckedChanged(object sender, EventArgs e) {
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
        private void cbMon_CheckedChanged(object sender, EventArgs e) {
            if (!cbMon.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Monday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Monday);
                    shiftAnalysis.RemoveDayOfWeek(DayOfWeek.Monday);
                }
            }
            if (cbMon.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Monday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Monday);
                    shiftAnalysis.AddDayOfWeek(DayOfWeek.Monday);
                }
            }
        }
        private void cbTues_CheckedChanged(object sender, EventArgs e) {
            if (!cbTues.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Tuesday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Tuesday);
                    shiftAnalysis.RemoveDayOfWeek(DayOfWeek.Tuesday);
                }
            }
            if (cbTues.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Tuesday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Tuesday);
                    shiftAnalysis.AddDayOfWeek((DayOfWeek)DayOfWeek.Tuesday);
                }
            }
        }
        private void cbWed_CheckedChanged(object sender, EventArgs e) {
            if (!cbWed.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Wednesday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Wednesday);
                    shiftAnalysis.RemoveDayOfWeek((DayOfWeek)DayOfWeek.Wednesday);
                }
            }
            if (cbWed.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Wednesday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Wednesday);
                    shiftAnalysis.AddDayOfWeek(DayOfWeek.Wednesday);
                }
            }
        }
        private void cbThurs_CheckedChanged(object sender, EventArgs e) {
            if (!cbThurs.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Thursday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Thursday);
                    shiftAnalysis.RemoveDayOfWeek(DayOfWeek.Thursday);
                }
            }
            if (cbThurs.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Thursday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Thursday);
                    shiftAnalysis.AddDayOfWeek((DayOfWeek)DayOfWeek.Thursday);
                }
            }
        }
        private void cbFri_CheckedChanged(object sender, EventArgs e) {
            if (!cbFri.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Friday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Friday);
                    shiftAnalysis.RemoveDayOfWeek((DayOfWeek)DayOfWeek.Friday);
                }
            }
            if (cbFri.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Friday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Friday);
                    shiftAnalysis.AddDayOfWeek(DayOfWeek.Friday);
                }
            }
        }
        private void cbSat_CheckedChanged(object sender, EventArgs e) {
            if (!cbSat.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Saturday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Saturday);
                    shiftAnalysis.RemoveDayOfWeek(DayOfWeek.Saturday);
                }
            }
            if (cbSat.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Saturday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Saturday);
                    shiftAnalysis.AddDayOfWeek((DayOfWeek)DayOfWeek.Saturday);
                }
            }
        }
        private void cbSun_CheckedChanged(object sender, EventArgs e) {
            if (!cbSun.Checked) {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Sunday)) {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Sunday);
                    shiftAnalysis.RemoveDayOfWeek((DayOfWeek)DayOfWeek.Sunday);
                }
            }
            if (cbSun.Checked) {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Sunday)) {
                    FilteredDaysOfWeek.Add(DayOfWeek.Sunday);
                    shiftAnalysis.AddDayOfWeek(DayOfWeek.Sunday);
                }
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e) {
            dateOnlyStart = new DateOnly(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day);
            shiftAnalysis.SetDateOnly(dateOnlyStart, dateOnlyEnd);
        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e) {
            dateOnlyEnd = new DateOnly(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day);
            shiftAnalysis.SetDateOnly(dateOnlyStart, dateOnlyEnd);
        }
        private void btnIndividualStats_Click(object sender, EventArgs e) {
            if (rdoServerShifts.Checked) {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForIndividualServer(serverSelected);
            }
        }
        private void PopulateDGVForIndividualServer(Server serverSelected) {
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

        private void btnIndividualServerShifts_Click(object sender, EventArgs e) {
            if (rdoServerShifts.Checked) {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForServerShiftHistory(serverSelected);
            }
        }
        public void PopulateDGVForServerShiftHistory(Server serverSelected) {
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

        private void cbFilterByTempRange_CheckedChanged(object sender, EventArgs e) {
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

        private void cbAllMonths_CheckedChanged(object sender, EventArgs e) {
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

        private void cbMonth_CheckChanged(object sender, System.EventArgs e) {

        }
        private void cbJan_CheckedChanged(object sender, EventArgs e) {
            if (!cbJan.Checked) {
                if (FilteredMonths.Contains(1)) {
                    FilteredMonths.Remove(1);
                }
            }
            if (cbJan.Checked) {
                if (!FilteredMonths.Contains(1)) {
                    FilteredMonths.Add(1);
                }
            }
        }

        private void cbFeb_CheckedChanged(object sender, EventArgs e) {
            if (!cbFeb.Checked) {
                if (FilteredMonths.Contains(2)) {
                    FilteredMonths.Remove(2);
                }
            }
            if (cbFeb.Checked) {
                if (!FilteredMonths.Contains(2)) {
                    FilteredMonths.Add(2);
                }
            }
        }

        private void cbMar_CheckedChanged(object sender, EventArgs e) {
            if (!cbMar.Checked) {
                if (FilteredMonths.Contains(3)) {
                    FilteredMonths.Remove(3);
                }
            }
            if (cbMar.Checked) {
                if (!FilteredMonths.Contains(3)) {
                    FilteredMonths.Add(3);
                }
            }
        }

        private void cbApr_CheckedChanged(object sender, EventArgs e) {
            if (!cbApr.Checked) {
                if (FilteredMonths.Contains(4)) {
                    FilteredMonths.Remove(4);
                }
            }
            if (cbApr.Checked) {
                if (!FilteredMonths.Contains(4)) {
                    FilteredMonths.Add(4);
                }
            }
        }

        private void cbMay_CheckedChanged(object sender, EventArgs e) {
            if (!cbMay.Checked) {
                if (FilteredMonths.Contains(5)) {
                    FilteredMonths.Remove(5);
                }
            }
            if (cbMay.Checked) {
                if (!FilteredMonths.Contains(5)) {
                    FilteredMonths.Add(5);
                }
            }
        }

        private void cbJun_CheckedChanged(object sender, EventArgs e) {
            if (!cbJun.Checked) {
                if (FilteredMonths.Contains(6)) {
                    FilteredMonths.Remove(6);
                }
            }
            if (cbJun.Checked) {
                if (!FilteredMonths.Contains(6)) {
                    FilteredMonths.Add(6);
                }
            }
        }

        private void cbJul_CheckedChanged(object sender, EventArgs e) {
            if (!cbJul.Checked) {
                if (FilteredMonths.Contains(7)) {
                    FilteredMonths.Remove(7);
                }
            }
            if (cbJul.Checked) {
                if (!FilteredMonths.Contains(7)) {
                    FilteredMonths.Add(7);
                }
            }
        }

        private void cbAug_CheckedChanged(object sender, EventArgs e) {
            if (!cbAug.Checked) {
                if (FilteredMonths.Contains(8)) {
                    FilteredMonths.Remove(8);
                }
            }
            if (cbAug.Checked) {
                if (!FilteredMonths.Contains(8)) {
                    FilteredMonths.Add(8);
                }
            }
        }

        private void cbSep_CheckedChanged(object sender, EventArgs e) {
            if (!cbSep.Checked) {
                if (FilteredMonths.Contains(9)) {
                    FilteredMonths.Remove(9);
                }
            }
            if (cbSep.Checked) {
                if (!FilteredMonths.Contains(9)) {
                    FilteredMonths.Add(9);
                }
            }
        }

        private void cbOct_CheckedChanged(object sender, EventArgs e) {
            if (!cbOct.Checked) {
                if (FilteredMonths.Contains(10)) {
                    FilteredMonths.Remove(10);
                }
            }
            if (cbOct.Checked) {
                if (!FilteredMonths.Contains(10)) {
                    FilteredMonths.Add(10);
                }
            }
        }

        private void cbNov_CheckedChanged(object sender, EventArgs e) {
            if (!cbNov.Checked) {
                if (FilteredMonths.Contains(11)) {
                    FilteredMonths.Remove(11);
                }
            }
            if (cbNov.Checked) {
                if (!FilteredMonths.Contains(11)) {
                    FilteredMonths.Add(11);
                }
            }
        }

        private void cbDec_CheckedChanged(object sender, EventArgs e) {
            if (!cbDec.Checked) {
                if (FilteredMonths.Contains(12)) {
                    FilteredMonths.Remove(12);
                }
            }
            if (cbDec.Checked) {
                if (!FilteredMonths.Contains(12)) {
                    FilteredMonths.Add(12);
                }
            }
        }

        private void nudLowTemp_ValueChanged(object sender, EventArgs e) {
            
            shiftAnalysis.SetTempRange((int)nudTempAnchor.Value, (int)nudTempRange.Value);

        }

        private void nudHiTemp_ValueChanged(object sender, EventArgs e) {
            
            shiftAnalysis.SetTempRange((int)nudTempAnchor.Value, (int)nudTempRange.Value);

        }

        private void btnRefreshFilters_Click(object sender, EventArgs e) {
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
        public void PopulateDGVForAreaSales(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<ShiftRecord> shiftRecords) {
            InitializeDataGridView(dgvDiningAreas, diningAreas);
            AddShiftRows(dgvDiningAreas, diningAreas, shiftRecords);
        }
        private void InitializeDataGridView(DataGridView dgvDiningAreas, List<DiningArea> diningAreas) {
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

        private void AddShiftRows(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<ShiftRecord> shiftRecords) {
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
                int feelsLikeLo = 0;
                float rainAmount = 0f;
                float clouds = 0f;
                int maxWind = 0;
                int avgWind = 0;

                if (shiftRecord.ShiftWeather != null) {
                    feelsLikeHi = shiftRecord.ShiftWeather.FeelsLikeHi;
                    feelsLikeLo = shiftRecord.ShiftWeather.FeelsLikeLow;
                    rainAmount = shiftRecord.ShiftWeather.RainAmount;
                    clouds = shiftRecord.ShiftWeather.CloudCoverAverage;
                    maxWind = shiftRecord.ShiftWeather.WindMax;
                    avgWind = shiftRecord.ShiftWeather.WindAvg;
                }

                row.Add(feelsLikeHi);
                row.Add(feelsLikeLo);
                row.Add(rainAmount);
                row.Add(clouds);
                row.Add(avgWind);
                row.Add(maxWind);


                dgvDiningAreas.Rows.Add(row.ToArray());
            }
        }

        private void cboServerSelect_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
