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

                    SqliteDataAccess.SaveTableStat(allTableStats);
                    refreshMissingDateDisplay();
                    // Process the
                    // Further processing or display of tableStats
                    // For example, display the results in a ListView, DataGridView, etc.
                }
            }
        }
        private void refreshMissingDateDisplay()
        {
            lbMissingData.Items.Clear();
            DateOnly endDate = DateOnly.FromDateTime(dtpMissingDateEnd.Value);

            DateOnly startDate = DateOnly.FromDateTime(dtpMissingDateStart.Value);

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
                    if (date.AddDays(-1) != rangeStart)
                    {
                        // End of a current range
                        string dateRange = $"{rangeStart.Value.ToString("MMM dd")} - {date.AddDays(-1).ToString("MMM dd")}";
                        missingDateRanges.Add(dateRange);
                        rangeStart = null; // Reset for the next range
                    }
                    else
                    {
                        string dateRange = $"{rangeStart.Value.ToString("MMM dd")}";
                        missingDateRanges.Add(dateRange);
                        rangeStart = null; // Reset for the next range
                    }

                }
            }

            // Handle case where the last date is part of a missing range
            if (rangeStart != null)
            {

                if (rangeStart == endDate)
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

            foreach (string dateRange in missingDateRanges)
            {
                lbMissingData.Items.Add(dateRange);
            }
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            dtpMissingDateEnd.Value = DateTime.Now.AddDays(-1);
            dtpMissingDateStart.Value = DateTime.Now.AddDays(-60);
            refreshMissingDateDisplay();
        }

        private void dtpMissingDateStart_ValueChanged(object sender, EventArgs e)
        {
            refreshMissingDateDisplay();
        }

        private void dtpMissingDateEnd_ValueChanged(object sender, EventArgs e)
        {
            refreshMissingDateDisplay();
        }

        private void btnDeleteFloorplans_Click(object sender, EventArgs e)
        {
            frmConfirmation confirmationForm = new frmConfirmation();
            confirmationForm.ShowDialog();
        }
    }
}
