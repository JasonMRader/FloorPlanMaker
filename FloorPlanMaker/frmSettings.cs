using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQLite Database Files (*.db)|*.db";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                SqliteDataAccess.LoadDatabaseTables(filePath);
            }
        }

        private void btnChooseDataBase_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string newDbLocation = folderBrowserDialog.SelectedPath;
                    SqliteDataAccess.UpdateDatabaseLocation(newDbLocation);
                }
            }
        }

        private void btnBackUpDB_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.BackupDatabase();
            MessageBox.Show("Backup Created");
        }

        private void btnImportSalesData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";  // You can set this to a default directory
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    // Process the file
                    TableSalesManager tableSalesManager = new TableSalesManager();
                    var allTableStats = tableSalesManager.ProcessCsvFile(filePath);
                    var mondayLunchStats = tableSalesManager.GetStatsByShiftAndDayOfWeek(allTableStats, true, DayOfWeek.Monday);

                    // Get all dinner stats for Monday
                    var mondayDinnerStats = tableSalesManager.GetStatsByShiftAndDayOfWeek(allTableStats, false, DayOfWeek.Monday);

                    DateOnly startDate = new DateOnly(2024, 1, 1); // For example, January 1, 2024
                    DateOnly endDate = new DateOnly(2024, 1, 5);  // For example, January 31, 2024

                    // Get all stats within the date range
                    var statsInDateRange = tableSalesManager.GetStatsByDateRange(allTableStats, startDate, endDate);
                    SqliteDataAccess.SaveTableStat(allTableStats);
                    // Process the
                    // Further processing or display of tableStats
                    // For example, display the results in a ListView, DataGridView, etc.
                }
            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            DateOnly endDate = DateOnly.FromDateTime(DateTime.Today);
            endDate = endDate.AddDays(-1);
            DateOnly startDate = endDate.AddDays(-30);
           
            List<DateOnly> missingDates = SqliteDataAccess.GetMissingDates(startDate, endDate);
            List<string> missingDateRanges = new List<string>();

            DateOnly? rangeStart = null;
            for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (missingDates.Contains(date))
                {
                    // Start of a new range
                    if (rangeStart == null)
                    {
                        rangeStart = date;
                    }
                }
                else if (rangeStart != null)
                {
                    // End of a current range
                    string dateRange = $"{rangeStart.Value.ToString("MMM dd")} - {date.AddDays(-1).ToString("MMM dd")}";
                    missingDateRanges.Add(dateRange);
                    rangeStart = null; // Reset for the next range
                }
            }

            // Handle case where the last date is part of a missing range
            if (rangeStart != null)
            {

                if(rangeStart == endDate)
                {
                    string dateRange = $"{rangeStart.Value.ToString("MMM dd")}";
                    missingDateRanges.Add(dateRange);
                }
                else
                {
                    string dateRange = $"{rangeStart.Value.ToString("MMM dd")} - {endDate.ToString("MMM dd")}";
                    missingDateRanges.Add(dateRange);
                }
                
                
            }
            //for (int i = 0; i < missingDates.Count; i++)
            //{
            //    if (i + 1 == missingDates.Count)
            //    if (missingDates[i].AddDays(1) == missingDates[i + 1])
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        string dateRange = rangeStart.ToString() + " - " + missingDates[i].ToString();
            //        missingDateRanges.Add(dateRange);
            //        rangeStart = missingDates[i+1];
            //    }
            //}
            foreach (string dateRange in missingDateRanges)
            {
                lbMissingData.Items.Add(dateRange);
            }
        }
    }
}
