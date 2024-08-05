using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FloorplanUserControlLibrary
{
    public partial class FloorplanInfoDisplay : UserControl
    {
        public Floorplan Floorplan { get; set; }
        private Font infoFont = UITheme.MainFont;
        private Image lastWeek = Resources.small_LastWeek;
        private Image yesterday = Resources.smallyesterday;
        private ToolTip toolTip1 { get; set; }
        public AreaHistory AreaHistory { get; set; }
        public FloorplanInfoDisplay(Floorplan fp, int width)
        {
            width = width - 8;
            InitializeComponent();
            this.Width = width;
            this.Floorplan = fp;
            this.UpdateCurrentLabels(0);
            this.BackColor = UITheme.CanvasColor;
            setLabelSizes();
            this.Invalidate();
            lblCrtlCoversPerServer.Font = this.infoFont;
            lblCrtlServersOn.Font = this.infoFont;
            lblCrtlServersLastWeek.Font = this.infoFont;
            lblCrtlServersYesterday.Font = this.infoFont;
            lblCrtlSalesPerServer.Font = this.infoFont;
            AreaHistory = new AreaHistory(fp.DiningArea, fp.DateOnly, fp.IsLunch);
        }
        public void UpdateCurrentLabels(int daysAgo)
        {

            lblCrtlCoversPerServer.Text = this.Floorplan.MaxCoversPerServer.ToString("F0");
            lblCrtlSalesPerServer.Text = Section.FormatAsCurrencyWithoutParentheses(this.Floorplan.GetAvgSalesPerServerByDay(daysAgo));
            lblCrtlServersOn.Text = this.Floorplan.Servers.Count.ToString();
            toolTip1.SetToolTip(lblCrtlSalesPerServer, "Sales Per Server" + "\n" + "Total Sales:" + Floorplan.DiningArea.ExpectedSales.ToString("C0"));

        }
        public void UpdateCurrentLabelsForLastFour()
        {
            float salesPerServer = Floorplan.DiningArea.ExpectedSales;
            if (Floorplan.Servers.Count > 0)
            {
                salesPerServer = this.Floorplan.DiningArea.ExpectedSales / (float)this.Floorplan.Servers.Count();
            }

            lblCrtlCoversPerServer.Text = this.Floorplan.MaxCoversPerServer.ToString("F0");
            lblCrtlSalesPerServer.Text = Section.FormatAsCurrencyWithoutParentheses(salesPerServer);
            lblCrtlServersOn.Text = this.Floorplan.Servers.Count.ToString();
            toolTip1.SetToolTip(lblCrtlSalesPerServer, "Sales Per Server" + "\n" + "Total Sales:" + Floorplan.DiningArea.ExpectedSales.ToString("C0"));


        }
        public void SetSalesToLastFour()
        {
            this.AreaHistory.SetDatesToLastFourWeekdays();
        }
        public void UpdatePastCountLabels(int yesterdayCount, int LastWeekCount)
        {
            lblCrtlServersLastWeek.Text = LastWeekCount.ToString();
            lblCrtlServersYesterday.Text = yesterdayCount.ToString();           

        }
        private void setLabelSizes()
        {
            lblCrtlServersOn.Width = (this.Width) - 8;

            lblCrtlServersYesterday.Width = (this.Width / 3) - 6;

            lblCrtlServersLastWeek.Width = (this.Width / 3) - 6;
            lblCrtlServersLastWeek.Location = new Point(lblCrtlServersYesterday.Right + 4, 4);
            lblCrtlServersOn.Location = new Point(4, 4);

            lblCrtlCoversPerServer.Width = (this.Width / 2) - 6;
            lblCrtlCoversPerServer.Location = new Point(4, 28);
            lblCrtlSalesPerServer.Width = (this.Width / 2) - 6;
            lblCrtlSalesPerServer.Location = new Point(lblCrtlCoversPerServer.Right + 4, 28);

            

           
        }

    }
}
