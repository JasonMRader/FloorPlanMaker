using FloorplanClassLibrary;
using FloorPlanMaker;
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
            timer1.Tick += timer_Tick;
            timer1.Start();
            this.FormClosing += SplashScreen_FormClosing;
        }
        private int dotCount = 1;
        private void SplashScreen_Load(object sender, EventArgs e)
        {
           
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
        public frmEditStaff LoadEditStaffForm(EmployeeManager employeeManager, Shift shift, Form1 form)
        {
            frmEditStaff editStaffForm =  new frmEditStaff(employeeManager, shift, form) { TopLevel = false, AutoScroll = true };
            editStaffForm.UpdateUI();
            this.Close();
            return editStaffForm;
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //this.Close();
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
