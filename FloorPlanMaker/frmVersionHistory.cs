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
    public partial class frmVersionHistory : Form
    {
        public frmVersionHistory()
        {
            InitializeComponent();
        }
        string knownBugs = "1) Adding a new server to a floorplan, and removing an existing one (" +
            "after they were already assigned a section) at the same time makes it impossible to " +
            "add the new server to the existing Section\r\n" +
            "2) Floorplan lines that were saved to a template do not always populate \r\n" +
            "3) Changing the number of servers in the template page does not use the currently set filters for pickup / teamwait\r\n";

        string upcomingFeatures = "1) Open table / reservation integration\r\n" +
            "2) Toast Integration\r\n" +
            "3) More accurate Sales forecasting filtering shifts for similar weather conditions & reservation numbers, as well as" +
            " using a larger sample size of more shifts, and incorperating the year before trends of what time of the year sales go up or down\r\n" +
            "4) Sort floorplan templates by most used\r\n" +
            "5) Overall improvements to UI to make the application more user friendly\r\n" +
            "6) Performance improvements to make the application more responsive and faster\r\n" +
            "7) Add temperary dining areas for events / single use floorplans\r\n" +
            "8) Complete overhaul of Dining area / Table editing\r\n" +
            "9) Ability for user customization on how the auto assign algorithms work\r\n" +
            "10) Ability to make a Floorplan with 0 servers to assign pickup sections\r\n" +
            "11) Use a more accurate service for weather API data\r\n" +
            "12) Implement auto floorplan Line Generation\r\n" +
            "13) Ability to print blank floorplans";
        string Update8_8_24 =
            "Update: 8/8/24\r\n" +
            "1) Added Hotschedules integration\r\n" +
            "2) Added Color Printing to a PDF\r\n" +
            "3) Added indicator at the top of the main form warning if sales are not up to date\r\n" +
            "4) Added Weather Data API\r\n" +
            "5) Improved method of Drawing lines (using the right mouse button)\r\n" +
            "6) UI Improvements\r\n" +
            "7) Added % display to server label in Edit Staff form";
        string UpdateCurrent =
            "1) Fixed Bug in which Swaping servers to different floorplans if they were already " +
            "assigned sections made it impossible to assign them to a new section\r\n" +
            "2) Control + S now Saves the selected Floorplan without Printing it\r\n" +
            "3) Added Notification Banner for confirmation of Saving a floorplan";

        private void rdoKnownBugs_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoKnownBugs.Checked)
            {
                pnlUpdates.Visible = false;
                txtInfoBox.Text = knownBugs;
            }
            else if (rdoRecentUpdates.Checked)
            {
                pnlUpdates.Visible = true;
                txtInfoBox.Text = Update8_8_24;
            }
            else if (rdoUpcomingFeatures.Checked)
            {
                pnlUpdates.Visible = false;
                txtInfoBox.Text = upcomingFeatures;
            }

        }

        private void frmVersionHistory_Load(object sender, EventArgs e)
        {
            txtInfoBox.Text = knownBugs;
        }
    }
}
