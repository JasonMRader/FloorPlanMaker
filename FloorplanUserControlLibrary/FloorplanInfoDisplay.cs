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
        //private ToolTip toolTip1 { get; set; } = new ToolTip();
        public AreaHistory AreaHistory { get; set; }
        public FloorplanInfoDisplay(Floorplan fp, int width)
        {
            width = width - 8;
            InitializeComponent();
            this.Width = width;
            this.Floorplan = fp;
            this.UpdateCurrentLabels(0);
            this.BackColor = UITheme.CanvasColor;

            this.Invalidate();
            //lblCrtlCoversPerServer.Font = this.infoFont;
            //lblCrtlServersOn.Font = this.infoFont;
            //lblCrtlServersLastWeek.Font = this.infoFont;
            //lblCrtlServersYesterday.Font = this.infoFont;
            //lblCrtlSalesPerServer.Font = this.infoFont;
            SetImageLabels();
            setLabelSizes();
            AreaHistory = new AreaHistory(fp.DiningArea, fp.DateOnly, fp.IsLunch);
        }
        private void SetImageLabels()
        {
            lblCrtlServersOn.SetProperties(Resources.trey, "Servers Assigned to this Floorplan", this.Width);
            lblCrtlServersYesterday.SetProperties(Resources._1Arrrow, "Servers Assigned Yesterday", this.Width / 2);
            lblCrtlServersLastWeek.SetProperties(Resources._3Arrows, "Servers Assigned Last Week", this.Width / 2);
            lblCrtlCoversPerServer.SetProperties(Resources.covers, "Covers per Server", Width / 2);
            lblCrtlSalesPerServer.SetProperties(Resources.SalesPerPerson_28px, "Sales Per Server", this.Width / 2);

        }
        public void UpdateCurrentLabels(int daysAgo)
        {

            lblCrtlCoversPerServer.UpdateText(this.Floorplan.MaxCoversPerServer.ToString("F0"));
            lblCrtlSalesPerServer.UpdateText(Section.FormatAsCurrencyWithoutParentheses(this.Floorplan.GetAvgSalesPerServerByDay(daysAgo)));
            lblCrtlServersOn.UpdateText(this.Floorplan.Servers.Count.ToString());
            lblCrtlSalesPerServer.SetTooltip("Sales Per Server" + "\n" + "Total Sales:" + Floorplan.DiningArea.ExpectedSales.ToString("C0"));

        }
        public void UpdateCurrentLabelsForLastFour()
        {
            float salesPerServer = Floorplan.DiningArea.ExpectedSales;
            if (Floorplan.Servers.Count > 0)
            {
                salesPerServer = this.Floorplan.DiningArea.ExpectedSales / (float)this.Floorplan.Servers.Count();
            }

            lblCrtlCoversPerServer.UpdateText(this.Floorplan.MaxCoversPerServer.ToString("F0"));
            lblCrtlSalesPerServer.UpdateText(Section.FormatAsCurrencyWithoutParentheses(salesPerServer));
            lblCrtlServersOn.UpdateText(this.Floorplan.Servers.Count.ToString());
            lblCrtlSalesPerServer.SetTooltip("Sales Per Server" + "\n" + "Total Sales:" + Floorplan.DiningArea.ExpectedSales.ToString("C0"));


        }
        public void SetSalesToLastFour()
        {
            this.AreaHistory.SetDatesToLastFourWeekdays();
        }
        public void UpdatePastCountLabels(int yesterdayCount, int LastWeekCount)
        {
            lblCrtlServersLastWeek.UpdateText(LastWeekCount.ToString());
            lblCrtlServersYesterday.UpdateText(yesterdayCount.ToString());

        }
        private void setLabelSizes()
        {
            lblCrtlServersOn.Width = (this.Width) - 8;
            lblCrtlServersOn.Location = new Point(4, 4);

            lblCrtlServersYesterday.Width = (this.Width / 2) - 6;
            lblCrtlServersYesterday.Location = new Point(4, 36);

            lblCrtlServersLastWeek.Width = (this.Width / 2) - 6;
            lblCrtlServersLastWeek.Location = new Point(lblCrtlServersYesterday.Right + 4, 36);

            lblCrtlCoversPerServer.Width = (this.Width / 2) - 6;
            lblCrtlCoversPerServer.Location = new Point(4, 69);

            lblCrtlSalesPerServer.Width = (this.Width / 2) - 6;
            lblCrtlSalesPerServer.Location = new Point(lblCrtlCoversPerServer.Right + 4, 69);

            if (Width > 400)
            {
                lblCrtlServersOn.UseLargeFont();
                lblCrtlServersYesterday.UseLargeFont();
                lblCrtlServersLastWeek.UseLargeFont();
                lblCrtlCoversPerServer.UseLargeFont();
                lblCrtlSalesPerServer.UseLargeFont();
            }
            if (Width < 400 && Width >= 300)
            {
                lblCrtlServersOn.SetFontSize(16f);
                lblCrtlServersOn.SetFontSize(16f);
                lblCrtlServersYesterday.SetFontSize(16f);
                lblCrtlServersLastWeek.SetFontSize(16f);
                lblCrtlCoversPerServer.SetFontSize(16f);
                lblCrtlSalesPerServer.SetFontSize(16f);
            }
            if (Width < 300 && Width >= 200)
            {
                lblCrtlServersOn.SetFontSize(16f);
                lblCrtlServersOn.SetFontSize(14f);
                lblCrtlServersYesterday.SetFontSize(14f);
                lblCrtlServersLastWeek.SetFontSize(14f);
                lblCrtlCoversPerServer.SetFontSize(14f);
                lblCrtlSalesPerServer.SetFontSize(14f);
            }
            if (Width < 200 && Width >= 100)
            {
                lblCrtlServersOn.SetFontSize(14f);
                lblCrtlServersOn.SetFontSize(12f);
                lblCrtlServersYesterday.SetFontSize(12f);
                lblCrtlServersLastWeek.SetFontSize(12f);
                lblCrtlCoversPerServer.SetFontSize(12f);
                lblCrtlSalesPerServer.SetFontSize(12f);
            }

        }

    }
}
