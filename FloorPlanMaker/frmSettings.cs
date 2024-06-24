using CsvHelper.Configuration;
using CsvHelper;
using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmSettings : Form
    {
        private DiningAreaManager areaCreationManager = new DiningAreaManager();
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
                    SqliteDataAccess.SelectNewDatabaseLocation(newDbLocation);
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
                string defaultDirectory = @"C:\Users\Jason\OneDrive\Source\Databases\LiveDB\Order History";
                string fallbackDirectory = @"C:\";

                // Check if the default directory exists
                if (Directory.Exists(defaultDirectory))
                {
                    openFileDialog.InitialDirectory = defaultDirectory;
                }
                else
                {
                    openFileDialog.InitialDirectory = fallbackDirectory;
                }
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string filePath = openFileDialog.FileName;
                    frmLoading loadingForm = new frmLoading("Parsing");
                    loadingForm.Show();
                    this.Enabled = false;

                    Task.Run(() =>
                    {
                        TableSalesManager tableSalesManager = new TableSalesManager();
                        var allTableStats = tableSalesManager.ProcessCsvFile(filePath);
                        SqliteDataAccess.SaveTableStat(allTableStats);

                        this.Invoke(new Action(() =>
                        {
                            // Close the loading form and re-enable the main form
                            loadingForm.Close();
                            this.Enabled = true;
                            refreshMissingDateDisplay();
                        }));
                    });

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
            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";
            RefreshSpecialEventListBoxes();
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
            DialogResult result = confirmationForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                SqliteDataAccess.DeleteAllFloorplans();
            }
        }

        private void btnPastSection_Click(object sender, EventArgs e)
        {
            frmPastSections pastSectionsForm = new frmPastSections();
            pastSectionsForm.ShowDialog();
        }

        private void btnEditServers_Click(object sender, EventArgs e)
        {
            frmAddRemoveStaff addRemoveStaffForm = new frmAddRemoveStaff();
            addRemoveStaffForm.ShowDialog();
        }

        private void btnCreateTestData_Click(object sender, EventArgs e)
        {
            List<TableStat> newStats = new List<TableStat>();
            DiningArea area = cboDiningAreas.SelectedItem as DiningArea;
            DateTime dateSelected = dtpTestDataDate.Value;
            DateOnly dateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
            bool isLunch = rdoAM.Checked;
            float sales = float.Parse(txtSales.Text);
            foreach (Table table in area.Tables)
            {
                TableStat stat = new TableStat(table.TableNumber, dateSelected.DayOfWeek, dateOnly, isLunch, sales);
                newStats.Add(stat);
            }
            SqliteDataAccess.SaveTableStat(newStats);

            //SqliteDataAccess.DeleteTableStatsByDateRange(new DateTime(2024, 5, 22), new DateTime(2024, 5, 22));

        }

        private void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            int templateID = (int)nudTemplateID.Value;
            SqliteDataAccess.DeleteFloorplanTemplate(templateID);
            SqliteDataAccess.TestDeleteLines(templateID);

        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {

        }

        private void btnSaleStats_Click(object sender, EventArgs e)
        {
            frmSalesStats statsForm = new frmSalesStats();
            statsForm.ShowDialog();
        }

        private void btnEventDates_Click(object sender, EventArgs e)
        {
            frmSpecialDates frmEventDates = new frmSpecialDates();
            frmEventDates.ShowDialog();
        }
        private void RefreshSpecialEventListBoxes()
        {
            lbUpcomingEvents.Items.Clear();
            DateOnly today = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            List<SpecialEventDate> allEvents = SqliteDataAccess.LoadSpecialEvents();
            List<SpecialEventDate> futureEvents = allEvents.Where(e => e.DateOnly >= today).OrderBy(e => e.DateOnly).ToList();
            foreach (SpecialEventDate specialEventDate in futureEvents)
            {
                lbUpcomingEvents.Items.Add(specialEventDate.GetUpcomingEventString());
            }
        }

        private void btnImportScheduleData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string filePath = openFileDialog.FileName;
                    frmLoading loadingForm = new frmLoading("Importing");
                    loadingForm.Show();
                    this.Enabled = false;

                    Task.Run(() =>
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            PrepareHeaderForMatch = args => args.Header.ToLower(),
                        };

                        using (var reader = new StreamReader(filePath))
                        using (var csv = new CsvReader(reader, config))
                        {
                            var records = csv.GetRecords<dynamic>();

                            foreach (var record in records)
                            {
                                var weatherData = new WeatherData
                                {
                                    Date = DateOnly.Parse(record.datetime).ToString("yyyy-MM-dd"),
                                    TempHi = (int)Math.Round(decimal.Parse(record.tempmax)),
                                    TempLow = (int)Math.Round(decimal.Parse(record.tempmin)),
                                    TempAvg = (int)Math.Round(decimal.Parse(record.temp)),
                                    FeelsLikeHi = (int)Math.Round(decimal.Parse(record.feelslikemax)),
                                    FeelsLikeLow = (int)Math.Round(decimal.Parse(record.feelslikemin)),
                                    FeelsLikeAvg = (int)Math.Round(decimal.Parse(record.feelslike))
                                };

                                SqliteDataAccess.SaveOrUpdateWeatherData(weatherData);
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            // Close the loading form and re-enable the main form
                            loadingForm.Close();
                            this.Enabled = true;

                        }));
                    });

                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmTest testForm = new frmTest();
            testForm.ShowDialog();
        }
    }
}
