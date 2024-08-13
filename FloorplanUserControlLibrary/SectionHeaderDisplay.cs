using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FloorplanUserControlLibrary
{
    public partial class SectionHeaderDisplay : UserControl, ISectionObserver
    {
        private Section section { get; set; }
        public Section Section { get { return section; } }
        Panel pnlNoSection { get; set; } = new Panel();
        private Floorplan floorplan { get; set; }
        private ToolTip toolTip { get; set; }
        public event EventHandler AssignServerClicked;
        public event EventHandler btnTeamWaitClicked;

        public SectionHeaderDisplay()
        {
            InitializeComponent();

        }
        public void SetSection(Section section, Floorplan floorplan)
        {
            if (this.section != section && this.section != null)
            {
                this.section.RemoveObserver(this);
            }
            this.section = section;
            this.floorplan = floorplan;
            this.lblTotalCovers.ForeColor = section.FontColor;
            this.lblAverageSales.ForeColor = section.FontColor;
            this.lblSectionNumber.ForeColor = section.FontColor;
            this.section.SubscribeObserver(this);
            SetControlsForSection();
            pnlNoSection.Visible = false;

        }
        public void SetSectionToNull()
        {
            if (this.section != null)
            {
                this.section.RemoveObserver(this);
                this.section = null;
            }


            pnlNoSection.Dock = DockStyle.Fill;
            this.Controls.Add(pnlNoSection);
            pnlNoSection.Visible = true;
            pnlNoSection.BackColor = UITheme.SecondColor;
            pnlNoSection.BringToFront();
        }
        public void SetStaticImages()
        {
            if (this.section.FontColor == Color.White)
            {
                pbAverageSales.Image = Resources.SalesPerPersonWhite;
                pbTotalCovers.Image = Resources.CoversWhite;
            }
            else
            {
                pbAverageSales.Image = Resources.SalesPerPerson_28px;
                pbTotalCovers.Image = Resources.covers;
            }
        }
        public void SetTeamWaitImages()
        {
            if (section.IsPickUp || section.IsBarSection)
            {
                btnTeamWaitToggle.Visible = false;
                return;
            }
            else
            {
                btnTeamWaitToggle.Visible = true;
            }
            if (section.IsTeamWait)
            {
                btnTeamWaitToggle.Image = Resources.waiters_28;
                btnTeamWaitToggle.BackColor = UITheme.WarningColor;
                //if (section.FontColor == Color.White)
                //{
                //    btnTeamWaitToggle.Image = Resources.TeamWaiterWhite_28;
                //    btnTeamWaitToggle.BackColor = UITheme.WarningColor;
                //}
                //else
                //{
                //    btnTeamWaitToggle.Image = Resources.waiters_28;
                //    btnTeamWaitToggle.BackColor = UITheme.WarningColor;
                //}
            }
            if (!section.IsTeamWait)
            {
                btnTeamWaitToggle.Image = Resources.waiter_28;
                btnTeamWaitToggle.BackColor = UITheme.CTAColor;
                //if (section.FontColor == Color.White)
                //{
                //    btnTeamWaitToggle.Image = Resources.SoloWaiterWhite_28;
                //}
                //else
                //{
                //    btnTeamWaitToggle.Image = Resources.waiter_28;
                //    btnTeamWaitToggle.BackColor = UITheme.CTAColor;
                //}
            }

        }
        private void SetSalesDifferenceControls()
        {


            lblSalesDifference.Text = Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(section));
            if (floorplan.GetSalesDifferenceForSection(section) >= 0)
            {
                lblSalesDifference.BackColor = Color.LightGreen;
                lblSalesDifference.ForeColor = Color.Black;
                if (section.FontColor == Color.White)
                {
                    pbSalesDifference.Image = Resources.SalesUpWhite;
                }
                else
                {
                    pbSalesDifference.Image = Resources.SalesUp;
                }
            }
            else
            {
                lblSalesDifference.BackColor = Color.Pink;
                lblSalesDifference.ForeColor = Color.Black;
                if (section.FontColor == Color.White)
                {
                    pbSalesDifference.Image = Resources.SalesDownWhite;
                }
                else
                {
                    pbSalesDifference.Image = Resources.SalesDown;
                }
            }
        }
        private void SetCoverDifferenceProperties()
        {

            this.lblCoverDifference.Text = ($"{floorplan.GetCoverDifferenceForSection(section).ToString("F0")}");
            if (floorplan.GetCoverDifferenceForSection(section) >= 0)
            {
                lblCoverDifference.BackColor = Color.LightGreen;
                lblCoverDifference.ForeColor = Color.Black;
                if (section.FontColor == Color.White)
                {
                    pbCoversDifference.Image = Resources.CoversUpWhite;
                }
                else
                {
                    pbCoversDifference.Image = Resources.CoversUpBlack;
                }
            }
            else
            {
                lblCoverDifference.BackColor = Color.Pink;
                lblCoverDifference.ForeColor = Color.Black;
                if (section.FontColor == Color.White)
                {
                    pbCoversDifference.Image = Resources.CoversDownWhite;
                }
                else
                {
                    pbCoversDifference.Image = Resources.CoversDownBlack;
                }

            }

        }
        private void SetSectionTotalControls()
        {
            lblTotalCovers.Text = section.MaxCovers.ToString("F0");
            lblAverageSales.Text = section.AverageSalesDisplay();
            lblTotalCovers.BackColor = section.Color;
            lblTotalCovers.ForeColor = section.FontColor;
            lblAverageSales.BackColor = section.Color;
            lblAverageSales.ForeColor = section.FontColor;
        }
        private void SetServerButtonProperties()
        {
            btnAssignedServer.Text = section.GetDisplayString();
            if (section.IsBarSection)
            {
                //btnAssignedServer.Text = "Bar Section";
                btnAssignedServer.BackColor = section.MuteColor(.5f);
                btnAssignedServer.ForeColor = section.FontColor;
                return;
            }
            if (section.IsPickUp && section.Server == null)
            {
                //btnAssignedServer.Text = "Pick-Up";
                btnAssignedServer.BackColor = section.MuteColor(.5f);
                btnAssignedServer.ForeColor = section.FontColor;
                return;
            }
            if (section.IsPickUp && section.Server != null)
            {
                // btnAssignedServer.Text = "Pick-Up ++";
                btnAssignedServer.BackColor = section.MuteColor(.5f);
                btnAssignedServer.ForeColor = section.FontColor;
                return;
            }
            if (section.Server != null)
            {
                //btnAssignedServer.Text = section.Server.ToString();
                btnAssignedServer.BackColor = section.MuteColor(.5f);
                btnAssignedServer.ForeColor = section.FontColor;
            }
            if (section.IsTeamWait)
            {
                string displayString = "";
                for (int i = 0; i < section.ServerCount; i++)
                {
                    if (i <= section.ServerTeam.Count - 1)
                    {
                        if (section.ServerTeam[i].isDouble)
                        {
                            displayString += section.ServerTeam[i].ToString() + "*";
                        }
                        else
                        {
                            displayString += section.ServerTeam[i].ToString();
                        }

                        if (i < section.ServerCount - 1)
                        {
                            displayString += " | ";
                        }
                    }
                    else
                    {
                        displayString += "Unassigned";
                        if (i < section.ServerCount - 1)
                        {
                            displayString += " | ";
                        }

                    }

                }
                if (section.IsPickUp || this.section.PairedSection != null)
                {
                    displayString += " ++";
                }
                btnAssignedServer.Text = displayString;
                if (section.ServerTeam.Count < section.ServerCount)
                {
                    btnAssignedServer.BackColor = Color.Gray;
                    btnAssignedServer.ForeColor = Color.Black;
                }
                else
                {
                    btnAssignedServer.BackColor = section.MuteColor(.5f);
                    btnAssignedServer.ForeColor = section.FontColor;
                }

            }
            if (!section.IsPickUp && !section.IsTeamWait && section.Server == null)
            {
                btnAssignedServer.BackColor = Color.Gray;
                btnAssignedServer.ForeColor = Color.Black;
            }


        }
        private void SetControlsForSection()
        {
            this.BackColor = section.Color;
            this.lblSectionNumber.Text = $"#{section.Number}";
            SetStaticImages();
            SetServerButtonProperties();
            SetTeamWaitImages();
            SetSalesDifferenceControls();
            SetCoverDifferenceProperties();
            SetSectionTotalControls();

        }
        //public void UpdateSalesAndCovers()
        //{
        //    if (this.Section.IsPickUp) { return; }
        //    lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
        //    lblSales.Text = Section.AverageSalesDisplay();
        //    if (floorplan.GetSalesDifferenceForSection(Section) >= 0)
        //    {
        //        lblSalesDif.Text =
        //             Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
        //        lblSalesDif.BackColor = Color.LightGreen;
        //        lblSalesDif.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        lblSalesDif.Text =
        //             Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
        //        lblSalesDif.BackColor = Color.Pink;
        //        lblSalesDif.ForeColor = Color.Black;
        //    }
        //}
        //public void UpdateLabels()
        //{

        //    if (section.IsPickUp)
        //    {

        //        btnAssignedServer.Text = "Pick-up";
        //        btnTeamWaitToggle.Visible = false;
        //        btnClearSection.Image = Resources.Trash;
        //        toolTip.SetToolTip(btnClearSection, "Delete Section");
        //        ilcCovers.UpdateText(section.MaxCovers.ToString("F0"));
        //        ilcSales.UpdateText(Section.FormatAsCurrencyWithoutParentheses(section.ExpectedTotalSales));
        //        ilcSalesDif.Visible = false;
        //        return;
        //    }

        //    lblSectionNumber.Text = "# " + this.section.Number.ToString();
        //    UpdateSalesAndCovers();

        //    if (this.Section.Server != null && Section.IsTeamWait == false)
        //    {
        //        if (this.Section.Server.isDouble)
        //        {
        //            lblDisplay.Text = Section.Server.Name + " (Dbl)";
        //        }
        //        else
        //        {
        //            lblDisplay.Text = Section.Server.Name;
        //        }
        //        lblDisplay.BackColor = Section.Color;
        //        lblDisplay.Click -= unassignedLabel_Click;
        //        picClearSection.Image = Resources.erase;
        //        toolTip.SetToolTip(picClearSection, "Clear Section");
        //        toolTip.SetToolTip(lblSales, "Estimated Sales");
        //    }
        //    else if (this.Section.Server == null && Section.IsTeamWait == false)
        //    {
        //        lblDisplay.Text = "Unassigned";
        //        lblDisplay.BackColor = UITheme.ButtonColor;
        //        lblDisplay.Click += unassignedLabel_Click;
        //        if (this.Section.IsEmpty())
        //        {
        //            picClearSection.Image = Resources.Trash;
        //            toolTip.SetToolTip(picClearSection, "Delete Section");
        //        }
        //        toolTip.SetToolTip(lblSales, "Estimated Sales");
        //    }
        //    else if (this.Section.IsTeamWait)
        //    {
        //        if (this.Section.IsBarSection)
        //        {

        //        }
        //        foreach (Label label in serverLabels) { label.Tag = null; label.Text = "Unassigned"; };
        //        foreach (PictureBox pb in removeServerPBs) { pb.Tag = null; };
        //        lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
        //        lblDisplay.BackColor = Section.Color;
        //        toolTip.SetToolTip(picClearSection, "Clear Section");
        //        toolTip.SetToolTip(lblSales, $"Estimated Sales Per Server: {Section.AverageSalesDisplay()}\n" +
        //            $"Total Section Sales: {Section.ExpectedSalesDisplay()}");
        //        if (this.serverLabels.Count < Section.ServerCount)
        //        {
        //            SetToTeamWait();
        //        }
        //        for (int i = 0; i < this.Section.ServerTeam.Count; i++)
        //        {
        //            SetLabelToAssigned(i);
        //            //serverLabels[i].Text = Section.ServerTeam[i].Name;
        //            //serverLabels[i].Tag = Section.ServerTeam[i];
        //            removeServerPBs[i].Tag = Section.ServerTeam[i];
        //        }

        //    }

        //}
        private void SectionHeaderDisplay_Load(object sender, EventArgs e)
        {

        }

        public void UpdateSection(Section section)
        {
            if (section == null)
            {
                SetSectionToNull();
            }
            else
            {
                SetControlsForSection();
            }

        }

        private void btnAssignedServer_Click(object sender, EventArgs e)
        {
            AssignServerClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnTeamWaitToggle_Click(object sender, EventArgs e)
        {
            btnTeamWaitClicked?.Invoke(this, EventArgs.Empty);
        }

        public void SetTeamWaitPictureBoxes()
        {

        }
    }
}
