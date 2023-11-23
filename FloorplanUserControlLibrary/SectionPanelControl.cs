using FloorplanClassLibrary;
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
    public partial class SectionPanelControl : UserControl
    {
        public event EventHandler CheckBoxChanged;
        public bool CheckBoxChecked
        {
            get { return cbSectionSelect.Checked; }
            set { cbSectionSelect.Checked = value; }
        }

        public SectionPanelControl(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            this.Section = section;
            this.BackColor = Section.Color;
            this.ForeColor = Section.FontColor;
            UpdateLabels(floorplan);

        }
        public Section Section { get; set; }
        //public Floorplan Floorplan { get; set; }
        private void picClearSection_Click(object sender, EventArgs e)
        {

        }

        private void picTeamWait_Click(object sender, EventArgs e)
        {

        }
        public void UpdateLabels(Floorplan sectionsFloorplan)
        {
            lblServerNames.Text = "Unassigned";
            cbSectionSelect.Text = "Section " + this.Section.Number.ToString();
            lblCovers.Text = (this.Section.MaxCovers - sectionsFloorplan.MaxCoversPerServer).ToString("F0");
            if(this.Section.AverageCovers - sectionsFloorplan.AvgSalesPerServer > 0)
            {
                lblSales.Text = "+" + Section.FormatAsCurrencyWithoutParentheses(this.Section.AverageCovers - sectionsFloorplan.AvgSalesPerServer);
            }
            else
            {
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(this.Section.AverageCovers - sectionsFloorplan.AvgSalesPerServer);
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
        public void CheckBox()
        {
            this.cbSectionSelect.Checked = true;
        }
        public void UnCheckBox()
        {
            this.cbSectionSelect.Checked = false;
        }

        private void cbSectionSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxChanged?.Invoke(this, e);
            
        }
    }
}
