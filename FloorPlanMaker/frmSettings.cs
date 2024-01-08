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
    }
}
