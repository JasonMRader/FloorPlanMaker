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

namespace FloorPlanMakerUI
{
    public partial class frmSalesStats : Form
    {
        public frmSalesStats()
        {
            InitializeComponent();
        }
        private DiningAreaManager areaManager = new DiningAreaManager();
        private TableSalesManager tableSalesManager = new TableSalesManager();
        private EmployeeManager employeeManager = new EmployeeManager();
        private DateOnly dateOnlyStart = new DateOnly();
        private DateOnly dateOnlyEnd = new DateOnly();
        private List<WeatherData> allWeatherData = new List<WeatherData>();
        private int MinFeelsLikeHi = -150;
        private int MaxFeelsLikeHi = 150;



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

        private void frmSalesStats_Load(object sender, EventArgs e)
        {
            UITheme.FormatCTAButton(btnUpdate);
            allWeatherData = SqliteDataAccess.LoadAllWeatherData();
            dtpEndDate.Value = DateTime.Now.AddDays(-1);
            dtpStartDate.Value = DateTime.Now.AddDays(-8);
            employeeManager.LoadShiftsForActiveServers();
            //List<ServerShiftHistory> history = new List<ServerShiftHistory>();
            //foreach (Server server in employeeManager.ActiveServers)
            //{
            //    ServerShiftHistory shiftHistory = new ServerShiftHistory(server);
            //    history.Add(shiftHistory);
            //}
        }
        public List<DateTime> GetFilteredDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dateList = new List<DateTime>();

            if (startDate <= endDate)
            {
                DateTime currentDate = startDate;
                while (currentDate <= endDate)
                {
                    var weatherDataForDate = allWeatherData.FirstOrDefault(wd => wd.DateOnly == DateOnly.FromDateTime(currentDate));

                    if (weatherDataForDate != null &&
                        FilteredDaysOfWeek.Contains(currentDate.DayOfWeek) &&
                        FilteredMonths.Contains(currentDate.Month) &&
                        weatherDataForDate.FeelsLikeHi >= MinFeelsLikeHi &&
                        weatherDataForDate.FeelsLikeHi <= MaxFeelsLikeHi)
                    {
                        dateList.Add(currentDate);
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            else
            {
                MessageBox.Show("The start date must be earlier than or equal to the end date.");
            }

            return dateList;
        }

        private List<SalesData> GetSalesData(List<DiningArea> diningAreas, List<DateTime> dates)
        {
            var salesDataList = new List<SalesData>();

            foreach (var date in dates)
            {
                var salesData = new SalesData(date);

                float totalSalesForDate = 0;
                DateOnly dateOnly = new DateOnly(date.Year, date.Month, date.Day);
                List<TableStat> tableStats = GetTableStatsForFilters(dateOnly);
                foreach (var diningArea in diningAreas)
                {

                    diningArea.SetTableSales(tableStats);

                    salesData.SalesByDiningArea[diningArea.Name] = diningArea.ExpectedSales;
                    totalSalesForDate += diningArea.ExpectedSales;
                }

                salesData.TotalSales = totalSalesForDate;
                salesDataList.Add(salesData);
            }

            return salesDataList;
        }

        private List<TableStat> GetTableStatsForFilters(DateOnly dateOnly)
        {
            List<TableStat> statList = new List<TableStat>();


            if (rdoAm.Checked)
            {

                statList.AddRange(SqliteDataAccess.LoadTableStatsByDateAndLunch(true, dateOnly));
            }
            if (rdoPm.Checked)
            {
                statList.AddRange(SqliteDataAccess.LoadTableStatsByDateAndLunch(false, dateOnly));
            }
            if (rdoBoth.Checked)
            {
                statList.AddRange(SqliteDataAccess.LoadTableStatsByDateAllDay(dateOnly));
            }


            return statList;
        }

        public void PopulateDGVForAreaSales(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<SalesData> salesDataList)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();

            // Add columns for each dining area and total sales
            dgvDiningAreas.Columns.Add("Date", "Date");

            foreach (var diningArea in diningAreas)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = diningArea.Name,
                    HeaderText = diningArea.Name,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C0" // Format as currency with no decimals
                    }
                };
                dgvDiningAreas.Columns.Add(column);
            }

            var totalColumn = new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C0" // Format as currency with no decimals
                }
            };
            dgvDiningAreas.Columns.Add(totalColumn);

            var tempColumn = new DataGridViewTextBoxColumn
            {
                Name = "Temp",
                HeaderText = "Temp (FeelsLikeHi)",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0" // Format as integer
                }
            };
            dgvDiningAreas.Columns.Add(tempColumn);


            // Calculate averages for each dining area and the total sales
            var averageSalesByDiningArea = new Dictionary<string, float>();
            float totalSalesSum = 0;
            int dataCount = salesDataList.Count;

            foreach (var diningArea in diningAreas)
            {
                float areaSalesSum = salesDataList.Sum(sd => sd.SalesByDiningArea.ContainsKey(diningArea.Name) ? sd.SalesByDiningArea[diningArea.Name] : 0);
                averageSalesByDiningArea[diningArea.Name] = areaSalesSum / dataCount;
            }

            totalSalesSum = salesDataList.Sum(sd => sd.TotalSales);
            float averageTotalSales = totalSalesSum / dataCount;

            // Add the average row
            var averageRow = new List<object> { "Average" };
            foreach (var diningArea in diningAreas)
            {
                averageRow.Add(averageSalesByDiningArea[diningArea.Name]);
            }
            averageRow.Add(averageTotalSales);

            dgvDiningAreas.Rows.Add(averageRow.ToArray());

            // Add rows for each date's sales data
            foreach (var salesData in salesDataList)
            {
                var row = new List<object> { salesData.DateDisplay() };

                foreach (var diningArea in diningAreas)
                {
                    row.Add(salesData.SalesByDiningArea[diningArea.Name]);
                }
                row.Add(salesData.TotalSales);

                row.Add(salesData.WeatherData.FeelsLikeHi);

                dgvDiningAreas.Rows.Add(row.ToArray());
            }
        }

        private void PopulateDGVForServerHistory(List<ServerShiftHistory> serverHistory)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();

            // Add columns for each dining area and total sales
            dgvDiningAreas.Columns.Add("Server", "Server");

            var shiftColumn = new DataGridViewTextBoxColumn
            {
                Name = "ShiftCount",
                HeaderText = "Shifts",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "0"
                }
            };
            var outsideCountColumn = new DataGridViewTextBoxColumn
            {
                Name = "OutsideCount",
                HeaderText = "Outside",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "0"
                }
            };
            var outsidePercentageColumn = new DataGridViewTextBoxColumn
            {
                Name = "OutsidePercentage",
                HeaderText = "Outside %",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "P0"
                }
            };
            var cocktailCountColumn = new DataGridViewTextBoxColumn
            {
                Name = "CocktailCount",
                HeaderText = "Cocktail",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "0"
                }
            };
            var cocktailPercentageColumn = new DataGridViewTextBoxColumn
            {
                Name = "CocktailPercentage",
                HeaderText = "Cocktail %",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "P0"
                }
            };
            var closeCountColumn = new DataGridViewTextBoxColumn
            {
                Name = "CloseCount",
                HeaderText = "Close",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "0"
                }
            };
            var closePercentageColumn = new DataGridViewTextBoxColumn
            {
                Name = "closePercentage",
                HeaderText = "close %",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "P0"
                }
            };
            var TeamCountColumn = new DataGridViewTextBoxColumn
            {
                Name = "TeamCount",
                HeaderText = "Team",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "0"
                }
            };
            var TeamPercentageColumn = new DataGridViewTextBoxColumn
            {
                Name = "TeamPercentage",
                HeaderText = "Team %",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "P0"
                }
            };

            dgvDiningAreas.Columns.Add(shiftColumn);
            dgvDiningAreas.Columns.Add(outsideCountColumn);
            dgvDiningAreas.Columns.Add(outsidePercentageColumn);
            dgvDiningAreas.Columns.Add(cocktailCountColumn);
            dgvDiningAreas.Columns.Add(cocktailPercentageColumn);
            dgvDiningAreas.Columns.Add(closeCountColumn);
            dgvDiningAreas.Columns.Add(closePercentageColumn);
            dgvDiningAreas.Columns.Add(TeamCountColumn);
            dgvDiningAreas.Columns.Add(TeamPercentageColumn);



            //dgvDiningAreas.Columns.Add(totalColumn);

            // Add rows for each date's sales data
            foreach (var history in serverHistory)
            {
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


                dgvDiningAreas.Rows.Add(row.ToArray());
            }

        }
        public static Dictionary<DateOnly, int> GetFeelsLikeHiData(List<DateTime> dates)
        {
            List<WeatherData> weatherDataList = SqliteDataAccess.LoadWeatherDataByDateTimes(dates);
            Dictionary<DateOnly, int> feelsLikeHiData = weatherDataList.ToDictionary(wd => wd.DateOnly, wd => wd.FeelsLikeHi);
            return feelsLikeHiData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmLoading loadingForm = new frmLoading("Parsing");
            loadingForm.Show();
            this.Enabled = false;
            if (rdoDiningAreaSales.Checked)
            {
                Task.Run(() =>
                {
                    List<DateTime> dateList = GetFilteredDates(dtpStartDate.Value, dtpEndDate.Value);
                    List<SalesData> salesData = GetSalesData(areaManager.DiningAreas, dateList);

                    this.Invoke(new Action(() =>
                    {
                        // Close the loading form and re-enable the main form
                        PopulateDGVForAreaSales(dgvDiningAreas, areaManager.DiningAreas, salesData);
                        loadingForm.Close();
                        this.Enabled = true;
                        
                        //this.BringToFront();

                    }));
                });
                
            }
            if (rdoServerShifts.Checked)
            {
                Task.Run(() =>
                {
                    List<DateTime> dateList = GetFilteredDates(dtpStartDate.Value, dtpEndDate.Value);
                    List<ServerShiftHistory> serverHistory = GetServerHistory(employeeManager.ActiveServers);
                   
                    this.Invoke(new Action(() =>
                    {
                        // Close the loading form and re-enable the main form

                        loadingForm.Close();
                        this.Enabled = true;
                        PopulateDGVForServerHistory(serverHistory);


                    }));
                });
               
            }
            
            


        }



        private List<ServerShiftHistory> GetServerHistory(List<Server> servers)
        {
            var serverHistorys = new List<ServerShiftHistory>();
            if (rdoAm.Checked)
            {
                foreach (var server in servers)
                {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, true, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
            }
            if (rdoPm.Checked)
            {
                foreach (var server in servers)
                {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, false, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
            }
            if (rdoBoth.Checked)
            {
                foreach (var server in servers)
                {
                    var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);
                    serverHistorys.Add(serverShiftHistory);
                }
            }


            return serverHistorys;
        }

        private void rdoAm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoServerShifts_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked)
            {
                cboServerSelect.DataSource = employeeManager.ActiveServers;
                lblComboLabel.Text = "Servers";
                btnIndividualStats.Text = "Server Table History";
                btnIndividualServerShifts.Visible = true;
            }
            else
            {
                cboServerSelect.DataSource = areaManager.DiningAreas;
                lblComboLabel.Text = "Dining Areas";
                btnIndividualStats.Text = "Area Table History";
                btnIndividualServerShifts.Visible = false;


            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();
            //dtpEndDate.Focus();
        }

        private void rdoBoth_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbAllWeekdays_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllWeekdays.Checked)
            {
                cbMon.Checked = true;
                cbTues.Checked = true;
                cbWed.Checked = true;
                cbThurs.Checked = true;
                cbFri.Checked = true;
                cbSat.Checked = true;
                cbSun.Checked = true;
                cbAllWeekdays.Text = "No Days";
            }
            else
            {
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
        private void cbMon_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMon.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Monday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Monday);
                }
            }
            if (cbMon.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Monday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Monday);
                }
            }
        }
        private void cbTues_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbTues.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Tuesday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Tuesday);
                }
            }
            if (cbTues.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Tuesday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Tuesday);
                }
            }
        }
        private void cbWed_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbWed.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Wednesday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Wednesday);
                }
            }
            if (cbWed.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Wednesday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Wednesday);
                }
            }
        }
        private void cbThurs_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThurs.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Thursday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Thursday);
                }
            }
            if (cbThurs.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Thursday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Thursday);
                }
            }
        }
        private void cbFri_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFri.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Friday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Friday);
                }
            }
            if (cbFri.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Friday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Friday);
                }
            }
        }
        private void cbSat_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSat.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Saturday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Saturday);
                }
            }
            if (cbSat.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Saturday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Saturday);
                }
            }
        }
        private void cbSun_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSun.Checked)
            {
                if (FilteredDaysOfWeek.Contains(DayOfWeek.Sunday))
                {
                    FilteredDaysOfWeek.Remove(DayOfWeek.Sunday);
                }
            }
            if (cbSun.Checked)
            {
                if (!FilteredDaysOfWeek.Contains(DayOfWeek.Sunday))
                {
                    FilteredDaysOfWeek.Add(DayOfWeek.Sunday);
                }
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateOnlyStart = new DateOnly(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day);
        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateOnlyEnd = new DateOnly(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day);
        }
        private void btnIndividualStats_Click(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked)
            {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForIndividualServer(serverSelected);
            }
        }
        private void PopulateDGVForIndividualServer(Server serverSelected)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();
            var serverShiftHistory = new ServerShiftHistory();
            if (rdoBoth.Checked)
            {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);
            }
            else
            {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, rdoAm.Checked, FilteredDaysOfWeek);
            }



            dgvDiningAreas.Columns.Add("Server", "Server");
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

            foreach (var table in sortedTableCounts)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = table.Key,
                    HeaderText = table.Key,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0"
                    }
                };
                dgvDiningAreas.Columns.Add(column);
            }

            var row = new List<object> { serverShiftHistory.Server.ToString() };
            foreach (var table in sortedTableCounts)
            {
                row.Add(table.Value);
            }

            dgvDiningAreas.Rows.Add(row.ToArray());
        }

        private void btnIndividualServerShifts_Click(object sender, EventArgs e)
        {
            if (rdoServerShifts.Checked)
            {
                Server serverSelected = (Server)cboServerSelect.SelectedItem;
                PopulateDGVForServerShiftHistory(serverSelected);
            }
        }
        public void PopulateDGVForServerShiftHistory(Server serverSelected)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();

            // Add columns for each dining area and total sales
            dgvDiningAreas.Columns.Add("Date", "Date");
            var serverShiftHistory = new ServerShiftHistory();
            if (rdoBoth.Checked)
            {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, FilteredDaysOfWeek);

            }
            else
            {
                serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd, rdoAm.Checked, FilteredDaysOfWeek);
            }



            var tablesColumn = new DataGridViewTextBoxColumn
            {
                Name = "Tables",
                HeaderText = "Tables",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C0" // Format as currency with no decimals
                }
            };
            dgvDiningAreas.Columns.Add(tablesColumn);

            // Add rows for each date's sales data
            foreach (var empShift in serverShiftHistory.filteredShifts)
            {
                var row = new List<object> { empShift.Date.ToString("ddd, M/d") };


                row.Add(serverShiftHistory.ShiftTables[empShift]);



                dgvDiningAreas.Rows.Add(row.ToArray());
            }
        }

        private void cbFilterByTempRange_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFilterByTempRange.Checked)
            {
                nudHiTemp.Enabled = true;
                nudLowTemp.Enabled = true;
                lblTo.Enabled = true;
            }
            else
            {
                nudHiTemp.Enabled = false;
                nudLowTemp.Enabled = false;
                lblTo.Enabled = false;
            }
        }

        private void cbAllMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllMonths.Checked)
            {
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
            if (!cbAllMonths.Checked)
            {
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

        private void cbJan_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbJan.Checked)
            {
                if (FilteredMonths.Contains(1))
                {
                    FilteredMonths.Remove(1);
                }
            }
            if (cbJan.Checked)
            {
                if (!FilteredMonths.Contains(1))
                {
                    FilteredMonths.Add(1);
                }
            }
        }

        private void cbFeb_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFeb.Checked)
            {
                if (FilteredMonths.Contains(2))
                {
                    FilteredMonths.Remove(2);
                }
            }
            if (cbFeb.Checked)
            {
                if (!FilteredMonths.Contains(2))
                {
                    FilteredMonths.Add(2);
                }
            }
        }

        private void cbMar_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMar.Checked)
            {
                if (FilteredMonths.Contains(3))
                {
                    FilteredMonths.Remove(3);
                }
            }
            if (cbMar.Checked)
            {
                if (!FilteredMonths.Contains(3))
                {
                    FilteredMonths.Add(3);
                }
            }
        }

        private void cbApr_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbApr.Checked)
            {
                if (FilteredMonths.Contains(4))
                {
                    FilteredMonths.Remove(4);
                }
            }
            if (cbApr.Checked)
            {
                if (!FilteredMonths.Contains(4))
                {
                    FilteredMonths.Add(4);
                }
            }
        }

        private void cbMay_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMay.Checked)
            {
                if (FilteredMonths.Contains(5))
                {
                    FilteredMonths.Remove(5);
                }
            }
            if (cbMay.Checked)
            {
                if (!FilteredMonths.Contains(5))
                {
                    FilteredMonths.Add(5);
                }
            }
        }

        private void cbJun_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbJun.Checked)
            {
                if (FilteredMonths.Contains(6))
                {
                    FilteredMonths.Remove(6);
                }
            }
            if (cbJun.Checked)
            {
                if (!FilteredMonths.Contains(6))
                {
                    FilteredMonths.Add(6);
                }
            }
        }

        private void cbJul_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbJul.Checked)
            {
                if (FilteredMonths.Contains(7))
                {
                    FilteredMonths.Remove(7);
                }
            }
            if (cbJul.Checked)
            {
                if (!FilteredMonths.Contains(7))
                {
                    FilteredMonths.Add(7);
                }
            }
        }

        private void cbAug_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbAug.Checked)
            {
                if (FilteredMonths.Contains(8))
                {
                    FilteredMonths.Remove(8);
                }
            }
            if (cbAug.Checked)
            {
                if (!FilteredMonths.Contains(8))
                {
                    FilteredMonths.Add(8);
                }
            }
        }

        private void cbSep_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSep.Checked)
            {
                if (FilteredMonths.Contains(9))
                {
                    FilteredMonths.Remove(9);
                }
            }
            if (cbSep.Checked)
            {
                if (!FilteredMonths.Contains(9))
                {
                    FilteredMonths.Add(9);
                }
            }
        }

        private void cbOct_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOct.Checked)
            {
                if (FilteredMonths.Contains(10))
                {
                    FilteredMonths.Remove(10);
                }
            }
            if (cbOct.Checked)
            {
                if (!FilteredMonths.Contains(10))
                {
                    FilteredMonths.Add(10);
                }
            }
        }

        private void cbNov_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbNov.Checked)
            {
                if (FilteredMonths.Contains(11))
                {
                    FilteredMonths.Remove(11);
                }
            }
            if (cbNov.Checked)
            {
                if (!FilteredMonths.Contains(11))
                {
                    FilteredMonths.Add(11);
                }
            }
        }

        private void cbDec_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbDec.Checked)
            {
                if (FilteredMonths.Contains(12))
                {
                    FilteredMonths.Remove(12);
                }
            }
            if (cbDec.Checked)
            {
                if (!FilteredMonths.Contains(12))
                {
                    FilteredMonths.Add(12);
                }
            }
        }

        private void nudLowTemp_ValueChanged(object sender, EventArgs e)
        {
            if(cbFilterByTempRange.Checked)
            {
                MinFeelsLikeHi = (int)nudLowTemp.Value;
            }
            else
            {
                MinFeelsLikeHi = -150;
            }
            
        }

        private void nudHiTemp_ValueChanged(object sender, EventArgs e)
        {
            if (cbFilterByTempRange.Checked)
            {
                MaxFeelsLikeHi = ((int)nudHiTemp.Value);
            }
            else
            {
                MaxFeelsLikeHi = 150;
            }
               
        }
    }
}
