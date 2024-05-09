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
    public partial class frmPastSections : Form
    {
        public frmPastSections()
        {
            InitializeComponent();
        }

        private void btnReadEntireFile_Click(object sender, EventArgs e)
        {

        }

        private void btnReadSpecificDate_Click(object sender, EventArgs e)
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

                    // Use the date selected in the DateTimePicker
                    DateTime selectedDate = dtpShiftDate.Value.Date;  // This ensures we are only comparing the date part

                    var data = File.ReadAllLines(filePath)
                                   .Skip(1)
                                   .Select(line => line.Split(','))
                                   .Where(parts => DateTime.TryParse(parts[1], out DateTime date) &&
                                                   date.Date == selectedDate)  // Filter for the selected date
                                   .GroupBy(parts => parts[3]) // Assuming parts[4] is the server name
                                   .ToDictionary(group => group.Key, group => group.Select(x => x[4]).Distinct()); // Assuming parts[5] is the table number

                    listBox1.Items.Clear(); // Clear existing items

                    foreach (var entry in data)
                    {
                        if (entry.Value.Any(x => !string.IsNullOrEmpty(x))) // Check if there are any non-empty tables
                        {
                            listBox1.Items.Add($"{entry.Key}:  {string.Join(", ", entry.Value)}");
                        }
                    }
                }
            }
        }
    }
}
