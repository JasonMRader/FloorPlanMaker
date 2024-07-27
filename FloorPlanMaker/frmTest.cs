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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace FloorPlanMakerUI
{
    public partial class frmTest : Form
    {
        private int ServerCount = 0;
        private Shift shift = new Shift();
        private FloorplanGenerator floorplanGenerator = new FloorplanGenerator();
        private List<Label> serverCountLabels = new List<Label>();
        private List<Label> salesPerServerLabels = new List<Label>();

        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateDistribution();

        }

        private void SetServerCountLabels(FloorplanGenerator floorplanGenerator)
        {
            foreach (DiningArea diningArea in floorplanGenerator.shift.DiningAreasUsed)
            {
                foreach (Label label in serverCountLabels)
                {
                    if (label.Tag == (DiningArea)diningArea)
                    {
                        label.Text = floorplanGenerator.ServerDistribution[diningArea].ToString();
                    }
                }
                foreach (Label label in salesPerServerLabels)
                {
                    if (label.Tag == (DiningArea)diningArea)
                    {
                        label.Text = floorplanGenerator.AreaPerServerSales[diningArea].ToString("C0");
                    }
                }
            }
        }

        private void GetAreas()
        {
            if (txtInsideSales.Text != "")
            {
                float sales = float.Parse(txtInsideSales.Text);
                DiningArea insideDining = new DiningArea("insideDining", true, false, 1, sales);

                shift.CreateFloorplanForDiningArea(insideDining, 0, 0);
                lblArea1Servers.Tag = insideDining;
                serverCountLabels.Add(lblArea1Servers);
                lblSalesPerServer1.Tag = insideDining;
                salesPerServerLabels.Add(lblSalesPerServer1);

            }
            if (txtOutsideSales.Text != "")
            {
                float sales = float.Parse(txtOutsideSales.Text);
                DiningArea OutsideDining = new DiningArea("outsideDining", false, false, 2, sales);

                shift.CreateFloorplanForDiningArea(OutsideDining, 0, 0);
                lblArea2Servers.Tag = OutsideDining;
                serverCountLabels.Add(lblArea2Servers);
                lblSalesPerServer2.Tag = OutsideDining;
                salesPerServerLabels.Add(lblSalesPerServer2);
            }
            if (txtOutsideCocktailSales.Text != "")
            {
                float sales = float.Parse(txtOutsideCocktailSales.Text);
                DiningArea outCocktail = new DiningArea("outCocktail", false, true, 3, sales);

                shift.CreateFloorplanForDiningArea(outCocktail, 0, 0);
                lblArea3Servers.Tag = outCocktail;
                serverCountLabels.Add(lblArea3Servers);
                lblSalesPerServer3.Tag = outCocktail;
                salesPerServerLabels.Add(lblSalesPerServer3);
            }
            if (txtInsideCocktailSales.Text != "")
            {
                float sales = float.Parse(txtInsideCocktailSales.Text);
                DiningArea inCocktail = new DiningArea("inCocktail", true, true, 4, sales);

                shift.CreateFloorplanForDiningArea(inCocktail, 0, 0);
                lblArea4Servers.Tag = inCocktail;
                serverCountLabels.Add(lblArea4Servers);
                lblSalesPerServer4.Tag = inCocktail;
                salesPerServerLabels.Add(lblSalesPerServer4);
            }
            if (txtUpperSales.Text != "")
            {
                float sales = float.Parse(txtUpperSales.Text);
                DiningArea upper = new DiningArea("upper", true, false, 5, sales);

                shift.CreateFloorplanForDiningArea(upper, 0, 0);
                lblArea5Servers.Tag = upper;
                serverCountLabels.Add(lblArea5Servers);
                lblSalesPerServer5.Tag = upper;
                salesPerServerLabels.Add(lblSalesPerServer5);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            GetRandomNumbers();
            CalculateDistribution();

        }
        private void CalculateDistribution()
        {
            shift = new Shift();
            ServerCount = (int)numericUpDown1.Value;
            GetAreas();
            float totalSales = shift.DiningAreasUsed.Sum(da => da.TestSales);
            lblTotalSales.Text = totalSales.ToString("C0");
            lblSalesPerServerTotal.Text = (totalSales / ServerCount).ToString("C0");
            floorplanGenerator = new FloorplanGenerator(shift);
            floorplanGenerator.TestAddServers(ServerCount);
            floorplanGenerator.TESTGetServerDistribution();
            SetServerCountLabels(floorplanGenerator);
            lblServersAssigned.Text = floorplanGenerator.minimumServersAssigned.ToString();
            lblServersRemaining.Text = floorplanGenerator.ServerRemainder.ToString();
        }
        private void GetRandomNumbers()
        {
            Random rnd = new Random();

            int insideSales = rnd.Next(3000, 9000 + 1);
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
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CalculateDistribution();
        }

        private async void btnWeather_Click(object sender, EventArgs e)
        {
            DateOnly dateOnly = new DateOnly(dtpWeatherDay.Value.Year, dtpWeatherDay.Value.Month, dtpWeatherDay.Value.Day); 
            WeatherData weatherResult = await WeatherApiDataAccess.GetWeatherForSingleDate(dtpWeatherDay.Value);
            MessageBox.Show(weatherResult.WeatherFeelsLikeMax);
        }
    }
}
