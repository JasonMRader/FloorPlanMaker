using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloorplanUserControlLibrary;

namespace FloorPlanMakerUI
{
    public partial class frmDateSelect : Form
    {
        public DateTime dateSelected = DateTime.MinValue;
        public frmDateSelect(DateTime dateTime)
        {
            InitializeComponent();
            this.dateSelected = dateTime;
        }

        private void calDateSelected_DateChanged(object sender, DateRangeEventArgs e)
        {


        }

        private void frmDateSelect_Load(object sender, EventArgs e)
        {
            calDateSelected.SetDate(dateSelected);


        }

        private void calDateSelected_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.dateSelected = calDateSelected.SelectionStart;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void calDateSelected_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            this.dateSelected = calDateSelected.SelectionStart;

        }

        private void picButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
