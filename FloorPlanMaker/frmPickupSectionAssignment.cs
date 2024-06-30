using FloorplanClassLibrary;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FloorPlanMakerUI
{
    public partial class frmPickupSectionAssignment : Form
    {
        Shift shift { get; set; }
        Section pickUpSection { get; set; }
        Section sectionAssigned { get; set; }
        public frmPickupSectionAssignment(Section pickUpSection, Shift shift)
        {
            InitializeComponent();
            this.pickUpSection = pickUpSection;
            this.shift = shift;
        }

        private void frmPickupSectionAssignment_Load(object sender, EventArgs e)
        {
            foreach (Floorplan floorplan in shift.Floorplans)
            {
                RadioButton radioButton = CreateRadioForFloorplan(floorplan);
                flowDiningAreas.Controls.Add(radioButton);
                
            }
            CheckAppropriateDiningArea();
           
        }
        private void CheckAppropriateDiningArea()
        {
            if(pickUpSection.PairedSection == null)
            {
                RadioButton rdo = (RadioButton)flowDiningAreas.Controls[0];
                rdo.PerformClick();
            }
            else
            {
                Floorplan floorplan = shift.Floorplans.FirstOrDefault(fp => fp.Sections.Contains(pickUpSection.PairedSection));
                foreach(Control control in flowDiningAreas.Controls)
                {
                    if(control is RadioButton rdo)
                    {
                        if(rdo.Tag == floorplan)
                        {
                            rdo.PerformClick();
                        }
                    }
                }
            }
        }
        private void CheckAppropriateSection()
        {
            if(pickUpSection.PairedSection != null)
            {
                foreach (Control control in flowSections.Controls)
                {
                    if (control is RadioButton rdo)
                    {
                        if (rdo.Tag == pickUpSection.PairedSection)
                        {
                            rdo.PerformClick();
                        }
                    }
                }
            }
        }
        private RadioButton CreateRadioForFloorplan(Floorplan floorplan)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Text = floorplan.DiningArea.Name;
            radioButton.Tag = floorplan;
            radioButton.Appearance = Appearance.Button;
            radioButton.AutoSize = false;
            radioButton.Size = new System.Drawing.Size(flowDiningAreas.Width - 10, 30);
            radioButton.TextAlign = ContentAlignment.MiddleCenter;
            radioButton.CheckedChanged += diningAreaRadio_CheckChanged;
            return radioButton;
        }

        private void diningAreaRadio_CheckChanged(object? sender, EventArgs e)
        {
            flowSections.Controls.Clear();
            RadioButton rdoBtn = (RadioButton)sender;
            Floorplan floorplan = (Floorplan)rdoBtn.Tag;
            foreach (Section section in floorplan.Sections)
            {
                if (!section.IsPickUp)
                {
                    RadioButton radioButton = CreateRadioForSection(section);
                    flowSections.Controls.Add(radioButton);
                }

            }
            CheckAppropriateSection();
        }

        private RadioButton CreateRadioForSection(Section section)
        {
            RadioButton radioButton = new RadioButton();
            string displayString = section.GetDisplayString().Replace("\n", " &&");
            radioButton.Text = displayString;
            radioButton.Tag = section;
            radioButton.Appearance = Appearance.Button;
            radioButton.AutoSize = false;
            radioButton.Size = new System.Drawing.Size(flowDiningAreas.Width - 10, 30);
            radioButton.TextAlign = ContentAlignment.MiddleCenter;
            radioButton.CheckedChanged += sectionRadio_CheckChanged;
            return radioButton;
        }

        private void sectionRadio_CheckChanged(object? sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            Section section = (Section)rdo.Tag;
            if (rdo.Checked)
            {
                string displayString = section.GetDisplayString().Replace("\n", " &&");
                sectionAssigned = section;
                btnOK.Text = "Assign to " + displayString;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            pickUpSection.AssignPickupSection(sectionAssigned);
            sectionAssigned.AssignPickupSection(pickUpSection);
            this.Close();
        }

        private void btnUnassignSection_Click(object sender, EventArgs e)
        {
            if(pickUpSection.PairedSection != null)
            {
                pickUpSection.PairedSection.RemovePairedSection();
                pickUpSection.RemovePairedSection();
                pickUpSection.Notify();
            }
           
            this.Close();
        }
    }
}
