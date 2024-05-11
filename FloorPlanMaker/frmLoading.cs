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
    public partial class frmLoading : Form
    {
        public frmLoading()
        {
            InitializeComponent();
        }
        private int dotCount = 1;

        private void timer_Tick(object sender, EventArgs e)
        {
            // Update the text based on the dotCount
            switch (dotCount)
            {
                case 1:
                    label1.Text = "Parsing Data.";
                    dotCount++;
                    break;
                case 2:
                    label1.Text = "Parsing Data..";
                    dotCount++;
                    break;
                case 3:
                    label1.Text = "Parsing Data...";
                    dotCount++;  // Reset to start cycle over
                    break;
                case 4:
                    label1.Text = "Parsing Data....";
                    dotCount++;
                    break;
                case 5:
                    label1.Text = "Parsing Data.....";
                    dotCount = 1;
                    break;
            }
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void FormLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
