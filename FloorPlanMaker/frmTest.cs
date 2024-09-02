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
using static System.Runtime.InteropServices.JavaScript.JSType;
//using static System.Runtime.InteropServices.JavaScript.JSType;

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
            List<HourlyWeatherData> hourlyWeather = await WeatherApiDataAccess.GetWeatherForecast();
            //MessageBox.Show(weatherResult.WeatherFeelsLikeMax);
        }

        private void btnGetFloorplanData_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value.Date;
            DateTime end = dateTimePicker2.Value.Date;
            DateOnly startDateOnly = DateOnly.FromDateTime(start);
            DateOnly endDateOnly = DateOnly.FromDateTime(end);
            List<ShiftRecord> shiftRecords = new List<ShiftRecord>();
            for (DateOnly iDay = startDateOnly; iDay <= endDateOnly; iDay = iDay.AddDays(1))
            {
                shiftRecords.Add(SqliteDataAccess.LoadShiftRecord(iDay, false));
            }
            //ShiftAnalysis shiftAnalysis = new ShiftAnalysis(shiftRecords);
            MessageBox.Show("Complete");
        }
        //152, 39
        //Accent 144, 31 | 4,4
        //Main Container 140, 27 | 2,2
        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= Colors.Count; i++)
            {
                int spots = 2;
                int previous = i - spots;
                int next = i + spots;
                if (previous < 1)
                {
                    previous = Colors.Count - spots + i;
                }
                if (next > Colors.Count)
                {
                    next = i - Colors.Count + spots;
                }

                Label lbl = new Label()
                {
                    Text = i.ToString(),
                    Margin = new Padding(0),
                    Size = new Size(flowLayoutPanel1.Width, (flowLayoutPanel1.Height / 20)),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    BackColor = BackColor(i),
                    ForeColor = FontColor(i),
                    AllowDrop = true
                };
                Label num = new Label()
                {
                    Text = i.ToString(),
                    Margin = new Padding(0),
                    Size = new Size(flowNumber.Width, (flowNumber.Height / 20)),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.MainFont,
                    //BackColor = BackColor(i),
                    //ForeColor = FontColor(i),
                    AllowDrop = true
                };
                Label lblPrevious = new Label()
                {
                    Text = previous.ToString(),
                    Margin = new Padding(0),
                    Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    BackColor = BackColor(previous),
                    ForeColor = FontColor(previous),
                    Dock = DockStyle.Left,
                    AllowDrop = true
                };
                Label lblNext = new Label()
                {
                    Text = next.ToString(),
                    Margin = new Padding(0),
                    Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    BackColor = BackColor(next),
                    ForeColor = FontColor(next),
                    Dock = DockStyle.Right,
                    AllowDrop = true
                };

                lbl.MouseDown += Label_MouseDown;
                lbl.DragEnter += Label_DragEnter;
                lbl.DragDrop += Label_DragDrop;
                lbl.Controls.Add(lblPrevious);
                lbl.Controls.Add(lblNext);
                flowLayoutPanel1.Controls.Add(lbl);
                flowNumber.Controls.Add(num);
            }

        }
        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                lbl.DoDragDrop(lbl, DragDropEffects.Move);
            }
        }

        private void Label_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Label_DragDrop(object sender, DragEventArgs e)
        {
            Label sourceLabel = (Label)e.Data.GetData(typeof(Label));
            Label targetLabel = (Label)sender;

            if (sourceLabel != null && targetLabel != null && sourceLabel != targetLabel)
            {
                int sourceIndex = flowLayoutPanel1.Controls.IndexOf(sourceLabel);
                int targetIndex = flowLayoutPanel1.Controls.IndexOf(targetLabel);

                flowLayoutPanel1.Controls.SetChildIndex(sourceLabel, targetIndex);
                flowLayoutPanel1.Controls.SetChildIndex(targetLabel, sourceIndex);

                flowLayoutPanel1.Invalidate(); // Refresh the layout
            }
        }
        private Color BackColor(int i)
        {


            if (Colors.ContainsKey(i))
            {
                return Colors[i].BackgroundColor;
            }

            return Color.White;


        }
        private Color FontColor(int i)
        {
            if (Colors.ContainsKey(i))
            {
                return Colors[i].FontColor;
            }

            return Color.White;
        }
        public Dictionary<int, ColorPair> Colors { get; } = new Dictionary<int, ColorPair>
        {
            { 1, new ColorPair(Color.FromArgb(17,100,184), Color.White) },
            { 2, new ColorPair(Color.FromArgb(105,209,0), Color.Black) },
            { 3, new ColorPair(Color.FromArgb(176,46,12), Color.White) },
            { 4, new ColorPair(Color.FromArgb(103,178,216), Color.Black) },
            { 5, new ColorPair(Color.ForestGreen, Color.White) },
            { 6, new ColorPair(Color.FromArgb(240,246,0), Color.Black) },
            

            { 7, new ColorPair(Color.FromArgb(70,17,122), Color.White) },
            { 8, new ColorPair(Color.FromArgb(65, 234, 212), Color.Black) },
            { 9, new ColorPair(Color.FromArgb(244,192,149), Color.Black) },
            { 10, new ColorPair(Color.FromArgb(130,9,29), Color.White) },
            { 11, new ColorPair(Color.FromArgb(194, 178, 180), Color.White) },
            { 12, new ColorPair(Color.FromArgb(7,79,87), Color.White) },
            { 13, new ColorPair(Color.FromArgb(250,127,127), Color.Black) },
            { 14, new ColorPair(Color.FromArgb(84,92,82), Color.White) },
            { 15, new ColorPair(Color.FromArgb(180,134,159), Color.Black) },
            
            
            
           
            
            
           
           

            //{ 4, new ColorPair(Color.FromArgb(103,178,216), Color.Black) },
            //{ 3, new ColorPair(Color.FromArgb(130,9,29), Color.White) },
            //{ 5, new ColorPair(Color.ForestGreen, Color.White) },
            ////{ 3, new ColorPair(Color.FromArgb(242,124,5), Color.Black) },
            //{ 12, new ColorPair(Color.FromArgb(250,127,127), Color.Black) },

            //{ 1, new ColorPair(Color.FromArgb(17,100,184), Color.White) },

            //{ 8, new ColorPair(Color.FromArgb(65, 234, 212), Color.Black) },
            //{ 6, new ColorPair(Color.FromArgb(240,246,0), Color.Black) },

            // { 9, new ColorPair(Color.FromArgb(70,17,122), Color.White) },
            // { 2, new ColorPair(Color.FromArgb(105,209,0), Color.Black) },
              
           
            ////{ 10, new ColorPair(Color.FromArgb(243,227,124), Color.Black) },

            // { 13, new ColorPair(Color.FromArgb(176,46,12), Color.White) },
            //{ 14, new ColorPair(Color.FromArgb(194, 178, 180), Color.White) },
            //{ 7, new ColorPair(Color.FromArgb(84,92,82), Color.White) },
            //{ 11, new ColorPair(Color.FromArgb(7,79,87), Color.White) },
            ////{ 14, new ColorPair(Color.FromArgb(26,83,92), Color.White) },
            //{ 10, new ColorPair(Color.FromArgb(244,192,149), Color.Black) },
            //{ 15, new ColorPair(Color.FromArgb(180,134,159), Color.Black) },
            // //{ 16, new ColorPair(Color.FromArgb(87,61,28), Color.White) },


            
            //{ 17, new ColorPair(Color.FromArgb(88,44,77), Color.White) },
            //{ 100, new ColorPair(Color.LightGray, Color.Black) },
            //{ 101, new ColorPair(Color.Gray, Color.White) },
            //{ 102, new ColorPair(Color.DarkGray, Color.White) }
        };

    }
}
