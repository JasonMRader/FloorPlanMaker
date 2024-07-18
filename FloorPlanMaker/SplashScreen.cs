using FloorplanClassLibrary;
using FloorPlanMaker;
using System.Drawing;
using System.Drawing.Imaging;
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


            this.FormClosing += SplashScreen_FormClosing;
        }


        private void SplashScreen_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }


        private async void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Perform the background tasks asynchronously
            await Task.Run(() =>
            {
                SqliteDataAccess.CheckAndSetDatabaseLocation();
                SqliteDataAccess.BackupDatabase();
                SqliteDataAccess.DeleteOldBackups();
            });
        }
        public frmEditStaff LoadEditStaffForm(EmployeeManager employeeManager, Shift shift, Form1 form)
        {
            frmEditStaff editStaffForm = new frmEditStaff(employeeManager, shift, form) { TopLevel = false, AutoScroll = true };
            editStaffForm.UpdateUI();
            editStaffForm.OpenNewShiftForm();
            this.Close();
            return editStaffForm;
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //this.Close();
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {

        }
    }
}
