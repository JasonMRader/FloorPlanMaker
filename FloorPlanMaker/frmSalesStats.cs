using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            // Add rows for each date's sales data
            foreach (var salesData in salesDataList)
            {
                var row = new List<object> { salesData.Date.ToString("ddd, M/d") };

                foreach (var diningArea in diningAreas)
                {
                    row.Add(salesData.SalesByDiningArea[diningArea.Name]);
                }
                row.Add(salesData.TotalSales);

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rdoDiningAreaSales.Checked)
            {
                List<DateTime> dateList = GetDateRange(dtpStartDate.Value, dtpEndDate.Value);
                List<SalesData> salesData = GetSalesData(areaManager.DiningAreas, dateList);
                PopulateDGVForAreaSales(dgvDiningAreas, areaManager.DiningAreas, salesData);
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

            foreach (var server in servers)
            {
                var serverShiftHistory = new ServerShiftHistory(server, dateOnlyStart, dateOnlyEnd);
                serverHistorys.Add(serverShiftHistory);
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
            }
            else
            {
                cboServerSelect.DataSource = areaManager.DiningAreas;
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
            //dtpEndDate.Value = dtpStartDate.Value.AddDays(7);
            //timer1.Start();
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

            // Create a ServerShiftHistory instance
            var serverShiftHistory = new ServerShiftHistory(serverSelected, dateOnlyStart, dateOnlyEnd);

            // Add the Server column
            dgvDiningAreas.Columns.Add("Server", "Server");

            // Add a column for each unique table
            foreach (var table in serverShiftHistory.TableCounts)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = table.Key,
                    HeaderText = table.Key,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0" // Format as number with no decimals
                    }
                };
                dgvDiningAreas.Columns.Add(column);
            }

            // Add a row for the server and populate the counts for each table
            var row = new List<object> { serverShiftHistory.Server.ToString() };
            foreach (var table in serverShiftHistory.TableCounts)
            {
                row.Add(table.Value);
            }

            dgvDiningAreas.Rows.Add(row.ToArray());
        }

    }
}
