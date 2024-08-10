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
        public event EventHandler OpenTableManagerClicked;
        public DiningAreaInfoControl()
        {
            InitializeComponent();
           
        }
        public void SetDiningArea(DiningArea diningArea)
        {
            this.diningArea = diningArea;
            setControlsForDiningArea();
        }

        private void setControlsForDiningArea()
        {
            lblDiningAreaName.Text = diningArea.Name;
        }

        

        private void btnOpenManageTablesForm_Click(object sender, EventArgs e)
        {
            OpenTableManagerClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
