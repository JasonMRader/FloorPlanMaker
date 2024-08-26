using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
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

namespace FloorplanUserControlLibrary
{
    public partial class DateControl : UserControl
    {

        public DateControl()
        {
            InitializeComponent();
           

        }
        private List<DiningArea> _diningAreas { get; set; } = new List<DiningArea>();
        private DateOnly dateOnly { get; set; }
        private DayOfWeek dayOfWeek { get; set; }
        private List<AreaHistory> _areaHistory { get; set; }= new List<AreaHistory>();
        public void SetDiningAreas(List<DiningArea> diningAreas)
        {
            _diningAreas = diningAreas;
        }
        public void SetDayOfWeek(DayOfWeek dayOfWeek)
        {
            this.dayOfWeek = dayOfWeek;
        }
        public void SetDateOnly(DateOnly dateOnly)
        {
            this.dateOnly = dateOnly;
            lblDate.Text = dateOnly.Day.ToString();
            if (dateOnly == DateOnly.FromDateTime(DateTime.Now))
            {
                lblDate.BackColor = Color.LightBlue;
                lblDate.ForeColor = Color.Black;
            }
            else if(dateOnly > DateOnly.FromDateTime(DateTime.Now))
            {
                lblDate.BackColor = Color.LightGray;
                lblDate.ForeColor = Color.White;
            }
            else
            {
                lblDate.BackColor = UITheme.ButtonColor;
                lblDate.ForeColor = Color.Black;
            }
        }
        public void ShowFloorplansForAmAndPM()
        {
            flowInfo.Controls.Clear();
            int amCount = SqliteDataAccess.GetFloorplanCountsForShift(dateOnly, true);
            int pmCount = SqliteDataAccess.GetFloorplanCountsForShift(dateOnly, false);
            Label lblAM = new Label()
            {
                AutoSize = false,
                Text = "AM: " + amCount,
                Height = this.flowInfo.Height / 2,
                Width = this.flowInfo.Width,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont

            };
            if(this.dateOnly > DateOnly.FromDateTime(DateTime.Now))
            {
                lblAM.BackColor = Color.LightGray;
            }
            else if (amCount > 0)
            {
                lblAM.BackColor = UITheme.YesColor;
            }
            else
            {
                lblAM.BackColor = UITheme.NoColor;
            }
            
            Label lblPM = new Label()
            {
                AutoSize = false,
                Text = "PM: " + pmCount,
                Height = this.flowInfo.Height / 2,
                Width = this.flowInfo.Width,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont

            };
            if (this.dateOnly > DateOnly.FromDateTime(DateTime.Now))
            {
                lblPM.BackColor = Color.LightGray;
            }
            else if (pmCount > 0)
            {
                lblPM.BackColor = UITheme.YesColor;
            }
            else
            {
                lblPM.BackColor = UITheme.NoColor;
            }
            flowInfo.Controls.Add(lblAM);
            flowInfo.Controls.Add(lblPM);

        }

        public void SetAreaHistories(bool isAm, List<DiningArea> ignoredAreas)
        {
            _areaHistory.Clear();
            foreach (var area in _diningAreas)
            {
                AreaHistory areaHistory = new AreaHistory(area, this.dateOnly, isAm);
                _areaHistory.Add(areaHistory);
            }
            PopulateFlowWithAreaHistories(ignoredAreas);
        }

        private void PopulateFlowWithAreaHistories(List<DiningArea> ignoredAreas)
        {
            flowInfo.Controls.Clear();
            
            List<ImageLabelControl> images = new List<ImageLabelControl>();
            float TotalSales = 0f;
            List<int> diningAreaIDs = ignoredAreas.Select(a => a.ID).ToList();
            foreach(var areaHistory in _areaHistory)
            {
                if(diningAreaIDs.Contains(areaHistory.DiningArea.ID))
                {
                    continue;
                }
                if(areaHistory.Sales > 100)
                {
                    
                    ImageLabelControl imageLabelControl = new ImageLabelControl() { };
                    imageLabelControl.Margin = new Padding(0);
                    imageLabelControl.SetFontSize(11);
                    imageLabelControl.SetProperties(UITheme.GetDiningAreaImage(areaHistory.DiningArea),
                        $"{areaHistory.DiningArea.Name} Sales For Shift", areaHistory.Sales.ToString("C0"));
                    images.Add(imageLabelControl);
                    TotalSales += areaHistory.Sales;
                }
            }
            if(images.Count > 1) {
                ImageLabelControl imageLabelTotal = new ImageLabelControl() { };
                imageLabelTotal.Margin = new Padding(0, 5, 0, 0);
                imageLabelTotal.SetFontSize(11);
                imageLabelTotal.SetProperties(Resources.sales,
                    $"Total Sales For Shift", TotalSales.ToString("C0"));
                images.Add(imageLabelTotal);
            }
            
            foreach ( ImageLabelControl labelControl in images)
            {
                labelControl.SetSizeAndLeftMostImage(this.flowInfo.Width, (int)(flowInfo.Height / (images.Count + 1)));
                flowInfo.Controls.Add(labelControl);
               
            }
            if (images.Count > 1)
            {
                var lastControl = images.Last();
                var totalHeight = images.Sum(c => c.Height);  
                int remainingHeight = flowInfo.Height - totalHeight; 

                lastControl.Margin = new Padding(0, remainingHeight, 0, 0);  
            }
        }
    }
    
}
