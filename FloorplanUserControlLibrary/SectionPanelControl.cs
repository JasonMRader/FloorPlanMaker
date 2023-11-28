﻿using FloorplanClassLibrary;
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
//using static System.Collections.Specialized.BitVector32;


namespace FloorplanUserControlLibrary
{
    public partial class SectionPanelControl : UserControl, ISectionObserver
    {
        public event EventHandler CheckBoxChanged;
        public event EventHandler picEraseSectionClicked;
        public event EventHandler picTeamWaitClicked;
        private Floorplan floorplan { get; set; }
        public bool CheckBoxChecked
        {
            get { return cbSectionSelect.Checked; }
            set { cbSectionSelect.Checked = value; }
        }

        public SectionPanelControl(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            this.Section = section;
            this.Section.SubscribeObserver(this);
            this.BackColor = Section.Color;
            this.ForeColor = Section.FontColor;
            this.floorplan = floorplan;
            UpdateLabels();

        }
        public Section Section { get; set; }
        //public Floorplan Floorplan { get; set; }
        public void UpdateSection(Section section)
        {
            UpdateLabels();
            if(section.IsSelected)
            {
                this.CheckBoxChecked = true;
            }
            SetTeamWaitPictureBoxes();
        }
        private void picClearSection_Click(object sender, EventArgs e)
        {
            picEraseSectionClicked?.Invoke(this, e);
        }

        private void picTeamWait_Click(object sender, EventArgs e)
        {
            picTeamWaitClicked?.Invoke(this, e);
        }
        public void UpdateLabels()
        {
            lblServerNames.Text = "Unassigned";
            cbSectionSelect.Text = "Section " + this.Section.Number.ToString();
            lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
            //lblCovers.Text = (this.Section.MaxCovers - floorplan.MaxCoversPerServer).ToString("F0");
            if(floorplan.GetSalesDifferenceForSection(Section) > 0)
            {
                lblSales.Text = "+" + Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
            else
            {
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
            
            if (this.Section.Server != null && Section.IsTeamWait == false)
            {
                lblServerNames.Text = Section.Server.Name;
            }
            else if (this.Section.IsTeamWait && this.Section.ServerTeam != null)
            {
                lblServerNames.Text = "";
                foreach(Server server in this.Section.ServerTeam)
                {
                    lblServerNames.Text += server.AbbreviatedName + ", ";
                }
               
            }
            
           
        }
        private void SetTeamWaitPictureBoxes()
        {
            if(this.Section.IsTeamWait)
            {
                picTeamWait.BackColor = UITheme.WarningColor;
                picTeamWait.Image = Resources.waiters;
            }
            else
            {
                picTeamWait.BackColor = UITheme.CTAColor;
                picTeamWait.Image = Resources.waiter;
            }
        }
        public void CheckBox()
        {
            this.cbSectionSelect.Checked = true;
        }
        public void UnCheckBox()
        {
            this.cbSectionSelect.Checked = false;
        }
        public bool isChecked()
        {
            return this.cbSectionSelect.Checked;
        }
        private void cbSectionSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxChanged?.Invoke(this, e);
            
        }
        

    }
}
