using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmNewShiftDatePicker : Form
    {
        private DateTime dateSelected = DateTime.Today;
        private bool isAM = true;
        public ShiftManager ShiftManagerCreated = new ShiftManager();

        public frmNewShiftDatePicker()
        {
            InitializeComponent();
        }
        private void SetColors()
        {
            btnBackDay.BackColor = AppColors.ButtonColor;
            btnForwardDay.BackColor = AppColors.ButtonColor;
            cbIsAm.BackColor = AppColors.ButtonColor;

            btnOK.BackColor = AppColors.CTAColor;

        }
        private void frmNewShiftDatePicker_Load(object sender, EventArgs e)
        {
            SetColors();
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
        }

        private void btnBackDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(-1);
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
            if (dateSelected.Date == DateTime.Today)
            {
                lblIsToday.Text = "(Today)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(-1))
            {
                lblIsToday.Text = "(Yesterday)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(1))
            {
                lblIsToday.Text = "(Tomorrow)";
            }
            else
            {
                lblIsToday.Text = "";
            }
        }

        private void btnForwardDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(1);
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
            if (dateSelected.Date == DateTime.Today)
            {
                lblIsToday.Text = "(Today)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(-1))
            {
                lblIsToday.Text = "(Yesterday)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(1))
            {
                lblIsToday.Text = "(Tomorrow)";
            }
            else
            {
                lblIsToday.Text = "";
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            this.ShiftManagerCreated.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
            this.ShiftManagerCreated.IsAM = isAM;
            this.DialogResult = DialogResult.OK;
        }

        private void cbIsAm_CheckedChanged(object sender, EventArgs e)
        {
            isAM = cbIsAm.Checked;
            if (isAM)
            {
                cbIsAm.Text = "AM";
            }
            else
            {
                cbIsAm.Text = "PM";
            }
        }
    }
}
