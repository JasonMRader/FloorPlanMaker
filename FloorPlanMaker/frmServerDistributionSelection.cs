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

namespace FloorPlanMakerUI
{
    public partial class frmServerDistributionSelection : Form
    {
        ShiftManager shiftManager;
        FloorplanGenerator floorplanGenerator = new FloorplanGenerator();
        public frmServerDistributionSelection(ShiftManager shiftManager)
        {
            InitializeComponent();
            this.shiftManager = shiftManager;
            floorplanGenerator = new FloorplanGenerator(shiftManager.SelectedShift);
        }

        private void frmServerDistributionSelection_Load(object sender, EventArgs e)
        {
            //Dictionary<DiningArea, int> distributions =
            //   FloorplanGenerator.GetServerDistribution(shiftManager.SelectedShift.DiningAreasUsed,
            //   shiftManager.SelectedShift.ServersOnShift.Count());
            Dictionary<DiningArea, int> distributions = floorplanGenerator.GetServerDistribution();
            string FloorplansString = "";

            foreach (DiningArea area in distributions.Keys)
            {
                FloorplansString += area + ": " + distributions[area].ToString() + "\n";
            }
            lblDistribution.Text = FloorplansString;
            lblServerCount.Text = floorplanGenerator.ServerCount.ToString();
            lblServerRemainder.Text = floorplanGenerator.ServerRemainder.ToString();
            floorplanGenerator.AssignCocktailers();
        }
    }
}
