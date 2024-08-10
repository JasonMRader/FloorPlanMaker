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

namespace FloorplanUserControlLibrary
{
    public partial class DiningAreaInfoControl : UserControl
    {
        private DiningArea diningArea { get; set; }
        public DiningAreaInfoControl(DiningArea diningArea)
        {
            InitializeComponent();
            this.diningArea = diningArea;
            lblDiningAreaName.Text = diningArea.Name;
        }

        private void btnOpenManageTablesForm_Click(object sender, EventArgs e)
        {

        }
    }
}
