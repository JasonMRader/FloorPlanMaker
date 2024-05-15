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

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    var data = File.ReadAllLines(filePath)
                                    .Skip(1)
                                    .Select(line => line.Split(','))
                                    .Where(parts => parts.Length > 4 && DateTime.TryParse(parts[1], out _))
                                    .Select(parts => new
                                    {
                                        Date = DateTime.Parse(parts[1]).Date,
                                        Time = DateTime.Parse(parts[1]).TimeOfDay,
                                        Server = parts[3],
                                        Table = parts[4]
                                    })
                                    .ToList();

                    // Group by date
                    var dateGroups = data.GroupBy(x => x.Date).OrderBy(x => x.Key);

                    tvPastServerTables.Nodes.Clear();

                    foreach (var dateGroup in dateGroups)
                    {
                        var dateNode = tvPastServerTables.Nodes.Add(dateGroup.Key.ToString("yyyy-MM-dd"));

                        // AM and PM shifts
                        var amNode = dateNode.Nodes.Add("AM");
                        var pmNode = dateNode.Nodes.Add("PM");

                        // Group by AM and PM based on time
                        var amServers = dateGroup.Where(x => x.Time < new TimeSpan(16, 0, 0)).GroupBy(x => x.Server);
                        var pmServers = dateGroup.Where(x => x.Time >= new TimeSpan(16, 0, 0)).GroupBy(x => x.Server);

                        // Populate AM nodes
                        foreach (var server in amServers)
                        {
                            var serverNode = amNode.Nodes.Add(server.Key);
                            foreach (var table in server.Select(x => x.Table).Distinct())
                            {
                                serverNode.Nodes.Add("Table " + table);
                            }
                        }

                        // Populate PM nodes
                        foreach (var server in pmServers)
                        {
                            var serverNode = pmNode.Nodes.Add(server.Key);
                            foreach (var table in server.Select(x => x.Table).Distinct())
                            {
                                serverNode.Nodes.Add("Table " + table);
                            }
                        }
                    }
                }
            }


            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.InitialDirectory = "c:\\";
            //    openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            //    openFileDialog.FilterIndex = 1;
            //    openFileDialog.RestoreDirectory = true;

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string filePath = openFileDialog.FileName;

            //        var data = File.ReadAllLines(filePath)
            //                       .Skip(1)
            //                       .Select(line => line.Split(','))
            //                       .GroupBy(parts => parts[3]) // Assuming parts[3] is the server name
            //                       .ToDictionary(group => group.Key, group => group.Select(x => x[4]).Distinct()); // Assuming parts[4] is the table number

            //        listBox1.Items.Clear(); // Clear existing items

            //        foreach (var entry in data)
            //        {
            //            if (entry.Value.Any(x => !string.IsNullOrEmpty(x))) // Check if there are any non-empty tables
            //            {
            //                listBox1.Items.Add($"{entry.Key}: {string.Join(", ", entry.Value)}");
            //            }
            //        }
            //    }
            //}
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

                    TimeSpan timeBoundary = new TimeSpan(16, 0, 0);  // 4:00 PM

                    var data = File.ReadAllLines(filePath)
                                   .Skip(1)
                                   .Select(line => line.Split(','))
                                   .Where(parts => DateTime.TryParse(parts[1], out DateTime dateTime) &&
                                                   dateTime.Date == selectedDate &&
                                                   (rdoAM.Checked ? dateTime.TimeOfDay < timeBoundary : dateTime.TimeOfDay >= timeBoundary))
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

        private void btnWriteFileToSpreadSheet_Click(object sender, EventArgs e)
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

                    var data = File.ReadAllLines(filePath)
                                    .Skip(1)
                                    .Select(line => line.Split(','))
                                    .Where(parts => parts.Length > 4 && DateTime.TryParse(parts[1], out _))
                                    .Select(parts => new {
                                        Date = DateTime.Parse(parts[1]).Date,
                                        Time = DateTime.Parse(parts[1]).TimeOfDay,
                                        Server = parts[3],
                                        Table = parts[4]
                                    })
                                    .ToList();

                    // Group by date
                    var dateGroups = data.GroupBy(x => x.Date).OrderBy(x => x.Key);

                    // Setup file to write
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ServerTables.csv");
                    using (StreamWriter writer = new StreamWriter(savePath))
                    {
                        foreach (var dateGroup in dateGroups)
                        {
                            // Write the date row
                            writer.WriteLine(dateGroup.Key.ToString("yyyy-MM-dd"));

                            // AM and PM shifts
                            var amServers = dateGroup.Where(x => x.Time < new TimeSpan(16, 0, 0)).GroupBy(x => x.Server).ToList();
                            var pmServers = dateGroup.Where(x => x.Time >= new TimeSpan(16, 0, 0)).GroupBy(x => x.Server).ToList();

                            // Write AM servers
                            if (amServers.Any())
                            {
                                writer.WriteLine("AM Servers");
                                foreach (var server in amServers)
                                {
                                    writer.Write(server.Key); // Write server name
                                    foreach (var table in server.Select(x => x.Table).Distinct())
                                    {
                                        writer.Write($",{table}"); // Write tables next to server name
                                    }
                                    writer.WriteLine();
                                }
                            }

                            // Write PM servers
                            if (pmServers.Any())
                            {
                                writer.WriteLine("PM Servers");
                                foreach (var server in pmServers)
                                {
                                    writer.Write(server.Key); // Write server name
                                    foreach (var table in server.Select(x => x.Table).Distinct())
                                    {
                                        writer.Write($",{table}"); // Write tables next to server name
                                    }
                                    writer.WriteLine();
                                }
                            }

                            writer.WriteLine(); // Blank line after each date
                        }
                    }
                    MessageBox.Show($"File saved to {savePath}");
                }
            }
        }

    }
}
