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
        private void frmSalesStats_Load(object sender, EventArgs e)
        {

        }
        private List<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dateList = new List<DateTime>();


            if (startDate <= endDate)
            {
                DateTime currentDate = startDate;
                while (currentDate <= endDate)
                {
                    dateList.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }
            }
            else
            {
                MessageBox.Show("The start date must be earlier than or equal to the end date.");
            }

            return dateList;
        }
        public List<SalesData> GetSalesData(List<DiningArea> diningAreas, List<DateTime> dates)
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
                statList.AddRange(SqliteDataAccess.LoadTableStatsByDateAllDay( dateOnly));
            }
            
            
            return statList;
        }
        public void PopulateDataGridView(DataGridView dgvDiningAreas, List<DiningArea> diningAreas, List<SalesData> salesDataList)
        {
            dgvDiningAreas.Columns.Clear();
            dgvDiningAreas.Rows.Clear();

            // Add columns for each dining area and total sales
            dgvDiningAreas.Columns.Add("Date", "Date");

            foreach (var diningArea in diningAreas)
            {
                dgvDiningAreas.Columns.Add(diningArea.Name, diningArea.Name);
            }
            dgvDiningAreas.Columns.Add("Total", "Total");

            // Add rows for each date's sales data
            foreach (var salesData in salesDataList)
            {
                var row = new List<object> { salesData.Date.ToShortDateString() };

                foreach (var diningArea in diningAreas)
                {
                    row.Add(salesData.SalesByDiningArea[diningArea.Name]);
                }
                row.Add(salesData.TotalSales);

                dgvDiningAreas.Rows.Add(row.ToArray());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<DateTime> dateList = GetDateRange(dtpStartDate.Value, dtpEndDate.Value);
            

            List<SalesData> salesData = GetSalesData(areaManager.DiningAreas, dateList);
            PopulateDataGridView(dgvDiningAreas, areaManager.DiningAreas, salesData);

        }

        private void rdoAm_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
