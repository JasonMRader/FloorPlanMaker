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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnChooseDataBase_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQLite Database Files (*.db)|*.db";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                SqliteDataAccess.LoadDatabaseTables(filePath);
            }
        }

        private void btnBackUpDB_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string newDbLocation = folderBrowserDialog.SelectedPath;
                    UpdateDatabaseLocation(newDbLocation);
                }
            }
        }
        private void UpdateDatabaseLocation(string newLocation)
        {
            // Update configuration and handle database file relocation
            // Similar to the UpdateAppConfig method
            // You may also need to handle moving the existing database file to the new location
        }
    }
}
