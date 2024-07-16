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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
        }
        private int dotCount = 1;
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
            backgroundWorker1.RunWorkerAsync();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // Update the text based on the dotCount
            switch (dotCount)
            {
                case 1:
                    lblLoading.Text = "Loading.";
                    dotCount++;
                    break;
                case 2:
                    lblLoading.Text = "Loading..";
                    dotCount++;
                    break;
                case 3:
                    lblLoading.Text = "Loading...";
                    dotCount++;  // Reset to start cycle over
                    break;
                case 4:
                    lblLoading.Text = "Loading....";
                    dotCount++;
                    break;
                case 5:
                    lblLoading.Text = "Loading.....";
                    dotCount = 1;
                    break;
            }
        }
        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SqliteDataAccess.CheckAndSetDatabaseLocation();
            SqliteDataAccess.BackupDatabase();
            SqliteDataAccess.DeleteOldBackups();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
