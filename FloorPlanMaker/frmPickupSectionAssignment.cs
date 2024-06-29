﻿using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmPickupSectionAssignment : Form
    {
        Shift shift { get; set; }
        Section pickUpSection { get; set; }
        public frmPickupSectionAssignment(Section pickUpSection, Shift shift)
        {
            InitializeComponent();
            this.pickUpSection = pickUpSection;
            this.shift = shift;
        }

        private void frmPickupSectionAssignment_Load(object sender, EventArgs e)
        {
            foreach(Floorplan floorplan in shift.Floorplans)
            {
                RadioButton radioButton = CreateRadioForFloorplan(floorplan);
                flowDiningAreas.Controls.Add(radioButton);
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
            throw new NotImplementedException();
        }
    }
}
