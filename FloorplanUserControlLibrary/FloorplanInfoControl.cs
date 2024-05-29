using FloorplanClassLibrary;
using FloorplanUserControlLibrary.Properties;
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
        private Image lastWeek = Resources.small_LastWeek;
        private Image yesterday = Resources.smallyesterday;

        public FloorplanInfoControl(Floorplan fp, int width)
        {
            width = width - 8;
            InitializeComponent();
            this.Width = width;
            this.Floorplan = fp;
            this.UpdateCurrentLabels(0);
            this.BackColor = UITheme.CanvasColor;
            setLabelSizes();
            this.Invalidate();
            lblYesterdayCount.Font = this.infoFont;
            lblLastWeekCount.Font = this.infoFont;
            lblCurrentServerCount.Font = this.infoFont;
            lblCoversPerServer.Font = this.infoFont;
            lblSalesPerServer.Font = this.infoFont;


        }
        public FloorplanInfoControl(Panel panel)
        {

            InitializeComponent();
            this.Size = panel.Size;
            this.BackColor = UITheme.CanvasColor;
            setLabelSizes();
            this.Invalidate();
            lblYesterdayCount.Font = this.infoFont;
            lblLastWeekCount.Font = this.infoFont;
            lblCurrentServerCount.Font = this.infoFont;
            lblCoversPerServer.Font = this.infoFont;
            lblSalesPerServer.Font = this.infoFont;
            lblYesterdayCount.Text = "Yesterday";
            lblLastWeekCount.Text = "Last Week";
            lblCurrentServerCount.Text = "Server Count";
            lblCoversPerServer.Text = "Covers Each";
            lblSalesPerServer.Text = "Sales Each";


        }
        public void UpdateCurrentLabels(int daysAgo)
        {
            //if (this.Width < 200)
            //{
            //    lblCoversPerServer.Text = this.Floorplan.MaxCoversPerServer.ToString("F0");
            //    lblSalesPerServer.Text = Section.FormatAsCurrencyWithoutParentheses(this.Floorplan.AvgCoversPerServer);
            //    lblCurrentServerCount.Text = this.Floorplan.Servers.Count.ToString();
            //}
            //else
            //{
            //    lblCoversPerServer.Text = "Covers:   " + this.Floorplan.MaxCoversPerServer.ToString("F0");
            //    lblSalesPerServer.Text = "Sales:  " + Section.FormatAsCurrencyWithoutParentheses(this.Floorplan.AvgCoversPerServer);
            //    lblCurrentServerCount.Text = "Servers:   " + this.Floorplan.Servers.Count.ToString();
            //}
            lblCoversPerServer.Text = this.Floorplan.MaxCoversPerServer.ToString("F0");
            lblSalesPerServer.Text = Section.FormatAsCurrencyWithoutParentheses(this.Floorplan.GetAvgSalesPerServerByDay(daysAgo));
            lblCurrentServerCount.Text = this.Floorplan.Servers.Count.ToString();

        }
        public void UpdatePastLabels(int yesterdayCount, int LastWeekCount)
        {
            if (this.Width < 200)
            {
                lblLastWeekCount.Text = LastWeekCount.ToString();
                lblYesterdayCount.Text = yesterdayCount.ToString();
            }
            else
            {
                //lblLastWeekCount.Image = Resources.small_LastWeek;
                lblLastWeekCount.Text = LastWeekCount.ToString();
                lblYesterdayCount.Text = yesterdayCount.ToString();
            }

        }
        private void setLabelSizes()
        {
            lblCurrentServerCount.Width = (this.Width / 3) - 4;

            lblYesterdayCount.Width = (this.Width / 3) - 6;

            lblLastWeekCount.Width = (this.Width / 3) - 6;
            lblLastWeekCount.Location = new Point(lblYesterdayCount.Right + 4, 4);
            lblCurrentServerCount.Location = new Point(lblLastWeekCount.Right + 4, 4);

            lblCoversPerServer.Width = (this.Width / 2) - 6;
            lblCoversPerServer.Location = new Point(4, 28);
            lblSalesPerServer.Width = (this.Width / 2) - 6;
            lblSalesPerServer.Location = new Point(lblCoversPerServer.Right + 4, 28);

            pbLastWeek.Location = new Point(lblLastWeekCount.Left + (lblLastWeekCount.Width / 2) - 35, 4);
            pbYesterday.Location = new Point(lblYesterdayCount.Left + (lblYesterdayCount.Width / 2) - 35, 4);
            pbCovers.Location = new Point(lblCoversPerServer.Left + (lblCoversPerServer.Width / 2) - 35, lblCoversPerServer.Top);

            pbSales.Location = new Point(lblSalesPerServer.Left + (lblSalesPerServer.Width / 2) - 45, lblSalesPerServer.Top);

            pbServerCount.Location = new Point(lblCurrentServerCount.Left + (lblCurrentServerCount.Width / 2) - 35, lblCurrentServerCount.Top);

            pbLastWeek.BackColor = lblLastWeekCount.BackColor;
            pbYesterday.BackColor = lblYesterdayCount.BackColor;
            pbCovers.BackColor = lblCoversPerServer.BackColor;
            pbSales.BackColor = lblSalesPerServer.BackColor;
            pbServerCount.BackColor = lblCurrentServerCount.BackColor;
        }



    }
}
