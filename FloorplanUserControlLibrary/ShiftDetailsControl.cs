using System;
using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class ShiftDetailsControl : UserControl
    {
        private Shift? shift { get; set; }
        public ShiftDetailsControl()
        {
            InitializeComponent();
            lblIsLunch.Text = string.Empty;
            lblSalesPerServer.Text = string.Empty;
            lblShiftDate.Text = string.Empty;
            lblTotalSales.Text = string.Empty;
            lblTotalServers.Text = string.Empty;
        }
        public void SetLabelsForShift(Shift shift)
        {
            if (shift == null) return;
            lblIsLunch.Text = "AM";
            if (shift.IsAM)
            {
                lblIsLunch.Text = "PM";
            }
            if(shift.ServersOnShift.Count == 0)
            {
                lblSalesPerServer.Text = shift.TotalExpectedShiftSales().ToString("C0");
            }
            else
            {
                float SalesPerServer = (float)(shift.TotalExpectedShiftSales() / shift.ServersOnShift.Count());
                lblSalesPerServer.Text = SalesPerServer.ToString("C0");
            }            
            lblShiftDate.Text = shift.DateTime.ToString("M");
            lblTotalSales.Text = shift.TotalExpectedShiftSales().ToString("C0");
            lblTotalServers.Text = shift.ServersOnShift.Count().ToString();
        }
        private void lblShiftDate_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
