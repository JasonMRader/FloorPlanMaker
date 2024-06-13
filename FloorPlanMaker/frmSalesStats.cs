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
            }

            return serverHistorys;
        }

        private void rdoAm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoServerShifts_CheckedChanged(object sender, EventArgs e)
        {

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
    }
}
