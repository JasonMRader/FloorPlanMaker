using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
        private string GetTestRecordData(List<ScheduledShift> shifts)
        {
            string s = "";
            foreach (ScheduledShift shift in shifts)
            {
                s += shift.ToString() + Environment.NewLine; // Use Environment.NewLine for new line
            }
            return s;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string defaultDirectory = @"C:\Users\Jason\OneDrive\Working On Now\misc";
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
                    frmLoading loadingForm = new frmLoading("Loading");
                    loadingForm.Show();
                    this.Enabled = false;

                    Task.Run(() =>
                    {
                        List<ScheduledShift> records = CsvScheduleReader.GetScheduledShifts(filePath);
                        //var records = CsvScheduleReader.InspectCsvFile(filePath);
                        records.OrderBy(r => r.Date).ToList();
                        string s = GetTestRecordData(records);

                        this.Invoke(new Action(() =>
                        {
                            // Close the loading form and re-enable the main form
                            loadingForm.Close();
                            this.Enabled = true;
                            textBox1.Text = s;

                        }));
                    });

                }
                
            }
           
        }
    }
}
