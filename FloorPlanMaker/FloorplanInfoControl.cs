using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class FloorplanInfoControl : UserControl
    {
        public Floorplan Floorplan { get; set; }
        private Font infoFont = UITheme.MainFont;
        public FloorplanInfoControl(Floorplan fp, int width)
        {
            width = width - 25;
            InitializeComponent();
            this.Width = width;
            this.Floorplan = fp;
            this.UpdateCurrentLabels();
            this.BackColor = Color.Black;
            setLabelSizes();
            this.Invalidate();
            lblYesterdayCount.Font = this.infoFont;
            lblLastWeekCount.Font = this.infoFont;
            lblCurrentServerCount.Font = this.infoFont;
            lblCoversPerServer.Font = this.infoFont;
            lblSalesPerServer.Font = this.infoFont;


        }
        public void UpdateCurrentLabels()
        {
            lblCoversPerServer.Text = (this.Floorplan.DiningArea.GetMaxCovers() / this.Floorplan.Servers.Count).ToString("F0");
            lblSalesPerServer.Text = Section.FormatAsCurrencyWithoutParentheses((this.Floorplan.DiningArea.GetAverageCovers() / this.Floorplan.Servers.Count));
            lblCurrentServerCount.Text = this.Floorplan.Servers.Count.ToString();
        }
        public void UpdatePastLabels(int yesterdayCount, int LastWeekCount)
        {
            lblLastWeekCount.Text = LastWeekCount.ToString();
            lblYesterdayCount.Text = yesterdayCount.ToString();
        }
        private void setLabelSizes()
        {
            lblCurrentServerCount.Width = this.Width - 8;
            
            lblYesterdayCount.Width = (this.Width / 2) - 6;
           
            lblLastWeekCount.Width = (this.Width / 2) - 6;
            lblLastWeekCount.Location = new Point(lblYesterdayCount.Right + 4,4);

            lblCoversPerServer.Width = (this.Width / 2) - 6;
            
            lblSalesPerServer.Width = (this.Width / 2) - 6;
            lblSalesPerServer.Location = new Point(lblCoversPerServer.Right + 4,52);
        }



    }
}
