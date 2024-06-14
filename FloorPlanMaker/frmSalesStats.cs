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
        private void frmSalesStats_Load(object sender, EventArgs e)
        {
            UITheme.FormatCTAButton(btnUpdate);
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
        private List<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dateList = new List<DateTime>();

            if (startDate <= endDate)
            {
                DateTime currentDate = startDate;
                while (currentDate <= endDate)
                {
                    if (FilteredDaysOfWeek.Contains(currentDate.DayOfWeek))
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
                var salesData = new SalesData
                {
                    Date = date,
                    SalesByDiningArea = new Dictionary<string, float>()
                };

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

        public void PopulateDGVForAreaSales(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<SalesData> salesDataList,
            Dictionary<DateOnly, int> feelsLikeHiData)
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
                var row = new List<object> { salesData.Date.ToString("ddd, M/d") };

                foreach (var diningArea in diningAreas)
                {
                    row.Add(salesData.SalesByDiningArea[diningArea.Name]);
                }
                row.Add(salesData.TotalSales);
                feelsLikeHiData.TryGetValue(DateOnly.FromDateTime(salesData.Date), out int feelsLikeHi);
                row.Add(feelsLikeHi);

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
            if (rdoDiningAreaSales.Checked)
            {
                List<DateTime> dateList = GetDateRange(dtpStartDate.Value, dtpEndDate.Value);
                List<SalesData> salesData = GetSalesData(areaManager.DiningAreas, dateList);
                Dictionary<DateOnly, int> weatherData = GetFeelsLikeHiData(dateList);
                PopulateDGVForAreaSales(dgvDiningAreas, areaManager.DiningAreas, salesData, weatherData);
            }
            if (rdoServerShifts.Checked)
            {
                List<DateTime> dateList = GetDateRange(dtpStartDate.Value, dtpEndDate.Value);
                List<ServerShiftHistory> serverHistory = GetServerHistory(employeeManager.ActiveServers);
                PopulateDGVForServerHistory(serverHistory);
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
                cbAllWeekdays.Text = "Uncheck All";
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
                cbAllWeekdays.Text = "Check All";
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
    }
}
