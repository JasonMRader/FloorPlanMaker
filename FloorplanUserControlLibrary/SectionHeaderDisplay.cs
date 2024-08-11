using FloorplanClassLibrary;
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

namespace FloorplanUserControlLibrary
{
    public partial class SectionHeaderDisplay : UserControl, ISectionObserver
    {
        private Section section { get; set; }
        private Floorplan floorplan { get; set; }
        public SectionHeaderDisplay()
        {
            InitializeComponent();
            ilcCovers.SetProperties(Properties.Resources.covers, "Covers in this Section", 100);
            ilcSales.SetProperties(Properties.Resources.salesSMall, "Sales Per Server", 100);
            ilcSalesDif.SetProperties(Properties.Resources.SalesPerPerson_28px, "Sales needed to equal Average", 100);

        }
        public void SetSection(Section section, Floorplan floorplan)
        {
            this.section = section;
            this.floorplan = floorplan;
            this.ilcCovers.ForeColor = section.FontColor;
            this.ilcSales.ForeColor = section.FontColor;
            this.ilcSalesDif.ForeColor = section.FontColor;
            this.lblSectionNumber.ForeColor = section.FontColor;
            SetControlsForSection();
        }

        private void SetControlsForSection()
        {
            this.BackColor = section.Color;
            this.lblSectionNumber.Text = $"#{section.Number}";
            if (section.Server != null)
            {
                btnAssignedServer.Text = section.Server.ToString();
                btnAssignedServer.BackColor = section.MuteColor(.5f);
                btnAssignedServer.ForeColor = section.FontColor;
            }
            else
            {
                btnAssignedServer.Text = "Unassigned";
                btnAssignedServer.BackColor = Color.Gray;
                btnAssignedServer.ForeColor = Color.Black;
            }
            this.ilcCovers.UpdateText($"{section.MaxCovers} ({floorplan.GetCoverDifferenceForSection(section).ToString("F0")})");
        }

        private void SectionHeaderDisplay_Load(object sender, EventArgs e)
        {

        }

        public void UpdateSection(Section section)
        {
            RefreshControls();
        }

        private void RefreshControls()
        {
            this.ilcCovers.UpdateText(floorplan.GetCoverDifferenceForSection(section).ToString("F0"));


        }
    }
}
