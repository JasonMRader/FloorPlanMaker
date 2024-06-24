using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmTest : Form
    {
        private int ServerCount = 0;
        private Shift shift = new Shift();

        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shift = new Shift();
            ServerCount = (int)numericUpDown1.Value;
            GetAreas();
            float totalSales = shift.DiningAreasUsed.Sum(da => da.TestSales);
            lblTotalSales.Text = totalSales.ToString("C0");
            lblSalesPerServerTotal.Text = (totalSales / ServerCount).ToString();

        }
        private void GetAreas()
        {
            if (txtInsideSales.Text != "")
            {
                float sales = float.Parse(txtInsideSales.Text);
                DiningArea insideDining = new DiningArea("insideDining", true, false, 1, sales);

                shift.CreateFloorplanForDiningArea(insideDining, 0, 0);

            }
            if (txtOutsideSales.Text != "")
            {
                float sales = float.Parse(txtOutsideSales.Text);
                DiningArea OutsideDining = new DiningArea("outsideDining", false, false, 2, sales);

                shift.CreateFloorplanForDiningArea(OutsideDining, 0, 0);

            }
            if (txtOutsideCocktailSales.Text != "")
            {
                float sales = float.Parse(txtOutsideCocktailSales.Text);
                DiningArea outCocktail = new DiningArea("outCocktail", false, true, 3, sales);

                shift.CreateFloorplanForDiningArea(outCocktail, 0, 0);

            }
            if (txtInsideCocktailSales.Text != "")
            {
                float sales = float.Parse(txtInsideCocktailSales.Text);
                DiningArea inCocktail = new DiningArea("inCocktail", true, true, 4, sales);

                shift.CreateFloorplanForDiningArea(inCocktail, 0, 0);

            }
            if (txtUpperSales.Text != "")
            {
                float sales = float.Parse(txtUpperSales.Text);
                DiningArea upper = new DiningArea("upper", true, false, 5, sales);

                shift.CreateFloorplanForDiningArea(upper, 0, 0);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            
            int insideSales = rnd.Next(3000,9000 + 1);
            int outsideSales = rnd.Next(7000, 17000);
            int outCocktail = rnd.Next(4000, 15000);
            int inCocktail = rnd.Next(200, 3000);
            int upperSales = rnd.Next(0, 2000);
            txtInsideSales.Text = insideSales.ToString();
            txtOutsideSales.Text = outsideSales.ToString();
            txtOutsideCocktailSales.Text = outCocktail.ToString();
            txtInsideCocktailSales.Text = inCocktail.ToString();
            txtUpperSales.Text = upperSales.ToString();
            numericUpDown1.Value = rnd.Next(11, 30);
        }
    }
}
