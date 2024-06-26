﻿using FloorplanClassLibrary;
using FloorPlanMaker;
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
    public partial class ServerHistoryControl : UserControl
    {
        public ServerHistoryControl(Server server, DateOnly start, DateOnly end, bool isLunch,
            bool isCollapsible, int width)
        {
            InitializeComponent();
            this.Server = server;
            this.shiftHistory = new ServerShiftHistory(server, start, end, isLunch);
            this.isLunch = isLunch;
            InitializeControls();
            SetShiftControls();
            SetIsCollapsible(isCollapsible);
            this.btnServer.Click += (sender, e) => this.OnClick(e);
            this.TabStop = false;
            this.Width = width;
            this.btnServer.Width = width;
            pnlInfo.Width = width;
        }
        public void SetWidth(int width)
        {
            this.Width = width;
            pnlInfo.Width = width;
        }
        private bool isLunch;
        private bool isCollapsible;
        private FlowLayoutPanel displayPanel = new FlowLayoutPanel();
        public void SetIsCollapsible(bool collapsible)
        {
            isCollapsible = collapsible;
            if (isCollapsible)
            {
                this.Height = btnServer.Height;
            }
            else
            {
                this.Height = 80;
            }
            SetCollapsibleDisplay();
        }
        public void SetCollapsibleDisplay()
        {
            if(isCollapsible)
            {
                
                this.lblOutsidePercentage.Size = new System.Drawing.Size(this.Width, 20);
                this.lblOutsidePercentage.Location = new System.Drawing.Point(0, 18);
                lblDescription.Size = new Size(this.Width, 15);
                lblDescription.Location = new Point(0, 3);

                //lblDescription.Dock = DockStyle.Top;
                //this.lblOutsidePercentage.Dock = DockStyle.Bottom;
                flowShiftDisplay.Visible = false;
                pnlInfo.BringToFront();
                lblDescription.BringToFront();
                lblOutsidePercentage.BringToFront();
            }
            else
            {
                //this.lblDescription.Dock = DockStyle.None;
                //this.lblOutsidePercentage.Dock = DockStyle.None;
                flowShiftDisplay.Visible = true;
                this.lblOutsidePercentage.Size = new System.Drawing.Size(94, 20);
                this.lblOutsidePercentage.Location = new System.Drawing.Point(199, 18);
                this.lblDescription.Size = new System.Drawing.Size(98, 15);
                this.lblDescription.Location = new System.Drawing.Point(199, 3);
                pnlInfo.SendToBack();
            }
            

        }
        private string GetIsLunchDisplay()
        {
            if (this.isLunch) { return "AM"; }
            else { return "PM"; }
        }
        public Server Server { get; private set; }
        public ServerShiftHistory shiftHistory { get; private set; }
        private void InitializeControls()
        {
            btnServer.Text = this.Server.ToString();

            lblDescription.Text = $"Last {shiftHistory.filteredShifts.Count} {GetIsLunchDisplay()} Shifts";
            lblOutsidePercentage.Text = $"{FormattedPercentage(shiftHistory.OutsidePercentage)} Outside";
        }
        private string FormattedPercentage(float num)
        {
            return $"{(int)(num * 100)}%";
        }
        private void SetShiftControls()
        {
            var lastFiveShifts = shiftHistory.filteredShifts.OrderByDescending(s => s.Date).ToList();
            lastFiveShifts = lastFiveShifts.Take(5).ToList();
            List<ShiftImgDisplay> shiftControls = new List<ShiftImgDisplay>();
            foreach (var shift in lastFiveShifts)
            {
                ShiftImgDisplay shiftControl = new ShiftImgDisplay(shift);
                shiftControls.Add(shiftControl);
            }
            if (shiftControls.Count > 0) { pnlShift5.Controls.Add(shiftControls[0]); }
            if (shiftControls.Count > 1) { pnlShift4.Controls.Add(shiftControls[1]); }
            if (shiftControls.Count > 2) { pnlShift3.Controls.Add(shiftControls[2]); }
            if (shiftControls.Count > 3) { pnlShift2.Controls.Add(shiftControls[3]); }
            if (shiftControls.Count > 4) { pnlShift1.Controls.Add(shiftControls[4]); }

        }
        private void ServerHistoryControl_Load(object sender, EventArgs e)
        {

        }

        private void btnServer_MouseHover(object sender, EventArgs e)
        {
            if (this.isCollapsible)
            {
                this.Height = 80;
            }
        }

        private void btnServer_MouseLeave(object sender, EventArgs e)
        {
            if (this.isCollapsible)
            {
                this.Height = btnServer.Height;
            }
        }
    }
}
