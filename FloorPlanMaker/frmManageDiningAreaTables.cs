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
    public partial class frmManageDiningAreaTables : Form
    {
        private DiningArea diningArea { get; set; }
        public frmManageDiningAreaTables(DiningArea diningArea)
        {
            InitializeComponent();
            this.diningArea = diningArea;
        }

        private void frmManageDiningAreaTables_Load(object sender, EventArgs e)
        {

        }
    }
}
